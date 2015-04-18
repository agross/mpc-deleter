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

      bus.Register<FastForward>(forward => GetPosition(bus));

      return FastForwardCurrentFilePosition(bus.Stream)
        .ObserveOn(scheduler)
        .Subscribe(m => bus.Send(m));
    }

    static void GetPosition(RxMessageBrokerMinimod bus)
    {
      bus.Send(new SendMessage(NativeConstants.CMD_GETCURRENTPOSITION));
    }

    static IObservable<SendMessage> FastForwardCurrentFilePosition(IObservable<object> stream)
    {
      var ffw = stream.OfType<FastForward>();
      var file = stream.OfType<CurrentFile>();
      var position = stream.OfType<CurrentPosition>();

      var match = ffw
        .And(file)
        .And(position)
        .Then((command, f, p) => new SendMessage(NativeConstants.CMD_SETPOSITION, NewPosition(command, f, p)));

      return Observable.When(match);
    }

    static string NewPosition(FastForward command, CurrentFile currentFile, CurrentPosition currentPosition)
    {
      var newPosition = currentPosition.Length + currentFile.Length * command.PercentOfLength;
      return newPosition.ToString(CultureInfo.InvariantCulture);
    }
  }
}
