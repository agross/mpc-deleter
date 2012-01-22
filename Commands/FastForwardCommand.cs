using System;
using System.Globalization;

using MpcDeleter.MpcMessageHandlers;

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
			_unsubscriber = Bus.Subscribe<CurrentPositionChanged>(m => CurrentPositionChanged(context, m));

			context.Execute(new SendMessageCommand(NativeConstants.CMD_GETCURRENTPOSITION, null));
		}

		void CurrentPositionChanged(IContext context, CurrentPositionChanged message)
		{
			var newPosition = message.Position + context.Player.CurrentFileLength * _percentOfLength;

			context.Execute(new SendMessageCommand(NativeConstants.CMD_SETPOSITION,
			                                       newPosition.ToString(CultureInfo.InvariantCulture)));

			_unsubscriber.Dispose();
		}
	}
}