using System;
using System.Windows.Forms;

namespace MpcDelete
{
	public class WindowMessageEventArgs : EventArgs
	{
		public WindowMessageEventArgs(Message message)
		{
			Message = message;
		}

		public Message Message
		{
			get;
			private set;
		}
	}
}