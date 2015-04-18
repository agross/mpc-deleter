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

      return AdvanceCurrentFile(bus.Stream)
        .ObserveOn(scheduler)
        .Subscribe(m => bus.Send(m));
    }

    static IObservable<SendMessage> AdvanceCurrentFile(IObservable<object> stream)
    {
      var advance = stream.OfType<AdvanceToNextFile>();
      var file = stream.OfType<CurrentFile>();

      var match = advance
          .And(file)
          .Then((command, f) => new SendMessage(NativeConstants.CMD_SETPOSITION, f.Length.ToString(CultureInfo.InvariantCulture)));

      return Observable.When(match);
    }
  }
}
