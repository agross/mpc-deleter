using System;

namespace MpcDeleter.LircKeyHandlers
{
	internal abstract class TimedKeyHandler
	{
		DateTime _lastKeyReceivedAt = DateTime.MinValue;

		protected abstract string KeyName
		{
			get;
		}

		public void Handle(string message, IContext context)
		{
			if (DateTime.Now.Subtract(_lastKeyReceivedAt) < TimeSpan.FromSeconds(10))
			{
				context.Log("You pressed the {0} key in quick succession. This key press will be ignored.", KeyName);
				return;
			}

			_lastKeyReceivedAt = DateTime.Now;

			HandleKey(context);
		}

		protected abstract void HandleKey(IContext context);
	}
}