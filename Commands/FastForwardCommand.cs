using System.Globalization;

namespace MpcDeleter.Commands
{
	public class FastForwardCommand : ICommand
	{
		readonly long _newPosition;

		public FastForwardCommand(long newPosition)
		{
			_newPosition = newPosition;
		}

		public void Execute(IContext context)
		{
			context.Execute(new SendMessageCommand(NativeConstants.CMD_SETPOSITION, _newPosition.ToString(CultureInfo.InvariantCulture)));
		}
	}
}