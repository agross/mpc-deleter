using MpcDeleter.Commands;

namespace MpcDeleter.LircKeyHandlers
{
	internal class ShiftKeyHandler : TimedKeyHandler, ILircKeyHandler
	{
		protected override string KeyName
		{
			get { return "Shift"; }
		}

		public bool CanHandle(string message)
		{
			return message.Contains(" shift ") || message.Contains(" 3 ");
		}

		protected override void HandleKey(IContext context)
		{
			context.Log("You pressed the Shift key, will now attempt to delete the current file");
			context.Execute(new DeleteCurrentFileCommand(false));
		}
	}
}