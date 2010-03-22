using System;

namespace MpcDelete
{
	public class MessageEventArgs : EventArgs
	{
		public MessageEventArgs(string message)
		{
			Message = message;
		}

		public string Message
		{
			get;
			private set;
		}
	}
}