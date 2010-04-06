using System;

using MpcDeleter.Commands;

namespace MpcDeleter.LircKeyHandlers
{
	internal class ShiftKeyHandler : ILircKeyHandler
	{
		DateTime _lastKeyReceivedAt = DateTime.MinValue;

		public bool CanHandle(string message)
		{
			return message.Contains(" shift ");
		}

		public void Handle(string message, IContext context)
		{
			if (DateTime.Now.Subtract(_lastKeyReceivedAt) < TimeSpan.FromSeconds(10))
			{
				context.Log("You pressed the Shift key in quick succession. This key press will be ignored.");
				return;
			}

			_lastKeyReceivedAt = DateTime.Now;

			context.Log("You pressed the Shift key, will now attempt to delete the current file");
			context.Execute(new DeleteCurrentFileCommand(false));
		}
	}
}