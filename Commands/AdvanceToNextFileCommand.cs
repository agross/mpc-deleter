using System.Globalization;

namespace MpcDeleter.Commands
{
	internal class AdvanceToNextFileCommand : ICommand
	{
		public void Execute(IContext context)
		{
			context.Execute(new SendMessageCommand(NativeConstants.CMD_SETPOSITION, context.Player.CurrentFileLength.ToString(CultureInfo.InvariantCulture)));
		}
	}
}