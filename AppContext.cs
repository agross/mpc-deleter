using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Windows.Forms;

using Minimod.RxMessageBroker;

using MpcDeleter.Commands;
using MpcDeleter.Handlers;
using MpcDeleter.Handlers.Commands;
using MpcDeleter.Handlers.Lirc;
using MpcDeleter.Handlers.Mpc;
using MpcDeleter.Messages;
using MpcDeleter.Properties;

namespace MpcDeleter
{
  class AppContext : ApplicationContext, IContext
  {
    readonly PlayerContext _player = new PlayerContext();
    RegexBasedArchivePathSelector _archivePathSelector;
    readonly IDisposable _subscriptions;

    public AppContext()
    {
      LoadSettings();

      MainForm = new MainForm(_archivePathSelector);

      _subscriptions = new CompositeDisposable(
        new LogMessagesAndCommands().SetUp(),
        SetUpCommandHandlers(),
        SetUpLirc(_archivePathSelector),
        SetUpMessageExchange());


      RxMessageBrokerMinimod.Default.Send(new StartMpc(Settings.Default.MpcPath));
      MainForm.Show();
    }

    // TODO
    public PlayerContext Player
    {
      get
      {
        return _player;
      }
    }

    // TODO
    public void Execute(ICommand command)
    {
      RxMessageBrokerMinimod.Default.Send(command);
    }

    // TODO
    public void Log(string message, params object[] args)
    {
      RxMessageBrokerMinimod.Default.Send(new Log(message, args));
    }

    void LoadSettings()
    {
      ApplicationSettings.Load();

      _archivePathSelector = new RegexBasedArchivePathSelector(Settings.Default.DefaultArchivePath,
                                                               ApplicationSettings.ArchivePathOverrides);
    }

    static IDisposable SetUpCommandHandlers()
    {
      var handlers = new ICommandHandler[]
      {
        new StartMpcHandler(),
        new SendMessageHandler(),
        new AdvanceToNextFileHandler(),
        new FastForwardHandler() 
      }
        .Select(x => x.SetUp(new EventLoopScheduler()));

      return new CompositeDisposable(handlers);
    }

    IDisposable SetUpLirc(IArchivePathSelector archivePathSelector)
    {
      var lirc = new LircClient(this, Settings.Default.LircServer, Settings.Default.LircPort);

      var handlers = new ILircKeyHandler[]
      {
        new UpKey(),
        new ShiftKey(),
        new SleepKey(archivePathSelector)
      }
        .Select(x => x.SetUp(lirc));

      return new CompositeDisposable(handlers);
    }

    static IDisposable SetUpMessageExchange()
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
