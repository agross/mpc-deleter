using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Threading;
using System.Windows.Forms;

using Minimod.RxMessageBroker;

using MpcDeleter.Archiving;
using MpcDeleter.Commands;
using MpcDeleter.Forms;
using MpcDeleter.Handlers;
using MpcDeleter.Handlers.Commands;
using MpcDeleter.Handlers.Lirc;
using MpcDeleter.Handlers.Mpc;
using MpcDeleter.Lirc;
using MpcDeleter.Properties;

namespace MpcDeleter
{
  class AppContext : ApplicationContext
  {
    readonly IDisposable _subscriptions;

    public AppContext()
    {
      var archivePathSelector = LoadSettings();

      MainForm = new MainForm();

      _subscriptions = new CompositeDisposable(
        new LogMessagesAndCommands().SetUp(),
        SetUpCommandHandlers(archivePathSelector),
        SetUpLirc(),
        SetUpMessageReceiver());

      RxMessageBrokerMinimod.Default.Send(new StartMpc(Settings.Default.MpcPath));
      MainForm.Show();
    }

    static IArchivePathSelector LoadSettings()
    {
      ApplicationSettings.Load();

      return new RegexBasedArchivePathSelector(Settings.Default.DefaultArchivePath,
                                               ApplicationSettings.ArchivePathOverrides);
    }

    static IDisposable SetUpCommandHandlers(IArchivePathSelector archivePathSelector)
    {
      var handlers = new ICommandHandler[]
      {
        new StartMpcHandler(),
        new SendMessageHandler(),
        new AdvanceToNextFileHandler(),
        new FastForwardHandler(),
        new ArchiveCurrentFileHandler(archivePathSelector),
        new DeleteCurrentFileHandler(), 
        new DeleteFileHandler()
      }
        .Select(x => x.SetUp(new EventLoopScheduler(ts => new Thread(ts)
        {
          Name = "Command Handler Thread",
          IsBackground = true
        })));

      return new CompositeDisposable(handlers);
    }

    static IDisposable SetUpLirc()
    {
      var lirc = new LircClient(Settings.Default.LircServer, Settings.Default.LircPort);

      var handlers = new ILircKeyHandler[]
      {
        new FastForwardKey(),
        new DeleteCurrentFileKey(),
        new ArchiveCurrentFileKey()
      }
        .Select(x => x.SetUp(lirc));

      return new CompositeDisposable(handlers);
    }

    static IDisposable SetUpMessageReceiver()
    {
      var receiver = new MpcMessageReceiver();

      var handlers = new IMessageHandler[]
      {
        new Connect(),
        new NowPlaying()
      }
        .Select(x => x.SetUp(receiver));

      return new CompositeDisposable(handlers);
    }

    protected override void OnMainFormClosed(object sender, EventArgs e)
    {
      if (_subscriptions != null)
      {
        _subscriptions.Dispose();
      }

      base.OnMainFormClosed(sender, e);
    }
  }
}
