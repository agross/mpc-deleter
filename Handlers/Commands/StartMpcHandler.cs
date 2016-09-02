using System;
using System.Diagnostics;
using System.Globalization;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

using Minimod.RxMessageBroker;

using MpcDeleter.Commands;
using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Commands
{
  class StartMpcHandler : ICommandHandler
  {
    class Params
    {
      public Params(StartMpc start, ReceiverCreated receiver)
      {
        Start = start;
        Receiver = receiver;
      }

      public StartMpc Start { get; set; }
      public ReceiverCreated Receiver { get; set; }
    }

    public IDisposable SetUp(IScheduler scheduler)
    {
      var stream = RxMessageBrokerMinimod.Default.Stream;

      var start = stream.OfType<StartMpc>();
      var receiver = stream.OfType<ReceiverCreated>();

      return start
        .CombineLatest(receiver, (s, r) => new Params(s, r))
        .ObserveOn(scheduler)
        .Subscribe(StartMpcInSlaveMode);
    }

    static void StartMpcInSlaveMode(Params @params)
    {
      try
      {
        var psi = new ProcessStartInfo(@params.Start.Path)
        {
          Arguments = string.Format(CultureInfo.InvariantCulture, "/slave {0}", @params.Receiver.Handle),
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
