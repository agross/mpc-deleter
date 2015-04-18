using System;
using System.Diagnostics;
using System.Globalization;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

using Minimod.RxMessageBroker;

using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Commands
{
  class StartMpc : ICommandHandler
  {
    class Params
    {
      public Params(string path, IntPtr handle)
      {
        Path = path;
        Handle = handle;
      }

      public string Path { get; set; }
      public IntPtr Handle { get; set; }
    }

    public IDisposable SetUp(IScheduler scheduler)
    {
      var stream = RxMessageBrokerMinimod.Default.Stream;

      var receiver = stream.OfType<ReceiverCreated>();
      var start = stream.OfType<MpcDeleter.Commands.StartMpc>();

      return receiver
        .CombineLatest(start, (r, s) => new Params(s.Path, r.Handle))
        .ObserveOn(scheduler)
        .Subscribe(StartMpcInSlaveMode);
    }

    static void StartMpcInSlaveMode(Params message)
    {
      try
      {
        var psi = new ProcessStartInfo(message.Path)
        {
          Arguments = string.Format(CultureInfo.InvariantCulture, "/slave {0}", message.Handle),
          ErrorDialog = true
        };

        Process.Start(psi);
      }
      catch (Exception ex)
      {
        RxMessageBrokerMinimod.Default.Send(new Log("Could not start Media Player Classic: {0}", ex.Message));
      }
    }
  }
}
