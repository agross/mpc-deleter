using System;
using System.Globalization;
using System.Reactive.Disposables;

using MpcDeleter.Messages;

namespace MpcDeleter.Commands
{
  class FastForwardCommand : ICommand
  {
    readonly double _percentOfLength;
    IDisposable _unsubscriber;

    public FastForwardCommand(double percentOfLength)
    {
      _percentOfLength = percentOfLength;
    }

    public void Execute(IContext context)
    {
      // TODO _unsubscriber = Bus.Register<CurrentPositionChanged>(m => CurrentPositionChanged(context, m));
      _unsubscriber = new CompositeDisposable();

      context.Execute(new SendMessageCommand(NativeConstants.CMD_GETCURRENTPOSITION, null));
    }

    void CurrentPositionChanged(IContext context, CurrentPositionChanged message)
    {
      var newPosition = message.Position + context.Player.CurrentFileLength * _percentOfLength;

      context.Execute(new SendMessageCommand(NativeConstants.CMD_SETPOSITION,
                                             newPosition.ToString(CultureInfo.InvariantCulture)));

      _unsubscriber.Dispose();
    }

    public override string ToString()
    {
      return string.Format("Fast-forwarding {0}%", _percentOfLength);
    }
  }
}
