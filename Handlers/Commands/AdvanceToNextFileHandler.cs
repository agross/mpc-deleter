using System;
using System.Globalization;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

using Minimod.RxMessageBroker;

using MpcDeleter.Commands;
using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Commands
{
  class AdvanceToNextFileHandler : ICommandHandler
  {
    public IDisposable SetUp(IScheduler scheduler)
    {
      var bus = RxMessageBrokerMinimod.Default;

      return AdvanceToNextFile(bus.Stream)
        .ObserveOn(scheduler)
        .Subscribe(m => bus.Send(m));
    }

    static IObservable<SendMessage> AdvanceToNextFile(IObservable<object> stream)
    {
      var advance = stream.OfType<AdvanceToNextFile>();
      var file = stream.OfType<CurrentFile>();

      return file.CombineLatest(advance, (f, a) => new { File = f, Command = a })
                 .DistinctUntilChanged(data => data.Command)
                 .Select(x => new SendMessage(NativeConstants.CMD_SETPOSITION,
                                              OneSecondBeforeEnd(x.File)));
    }

    static string OneSecondBeforeEnd(CurrentFile file)
    {
      var length = file.Length - 1;
      if (length < 0)
      {
        length = 0;
      }

      return length.ToString(CultureInfo.InvariantCulture);
    }
  }
}
