using System;
using System.Globalization;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

using Minimod.RxMessageBroker;

using MpcDeleter.Commands;
using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Commands
{
  class FastForwardHandler : ICommandHandler
  {
    public IDisposable SetUp(IScheduler scheduler)
    {
      var bus = RxMessageBrokerMinimod.Default;

      return FastForwardCurrentFilePosition(bus.Stream)
        .ObserveOn(scheduler)
        .Subscribe(m => bus.Send(m));
    }

    static IObservable<SendMessage> FastForwardCurrentFilePosition(IObservable<object> stream)
    {
      var ffw = stream.OfType<FastForward>();
      var file = stream.OfType<CurrentFile>();

      return file.CombineLatest(ffw, (f, ff) => new { File = f, Command = ff })
                 .DistinctUntilChanged(data => data.Command)
                 .Select(x => new SendMessage(NativeConstants.CMD_JUMPOFNSECONDS,
                                              TenPercentOfLength(x.File)));
    }

    static string TenPercentOfLength(CurrentFile file)
    {
      var tenPercent = file.Length / 10;
      return tenPercent.ToString(CultureInfo.InvariantCulture);
    }
  }
}
