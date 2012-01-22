using System;
using System.Windows.Forms;

namespace MpcDeleter
{
	internal sealed class MessageProcessingWindow : NativeWindow, IDisposable
	{
		readonly Func<Message, bool> _signalMessage;

		public MessageProcessingWindow(Func<Message, bool> signalMessage)
		{
			_signalMessage = signalMessage ?? (x => true);

			var createParams = new CreateParams { Caption = "Message Exchange Window" };
			CreateHandle(createParams);
		}

		public void Dispose()
		{
			DestroyHandle();
		}

		public event EventHandler<WindowMessageEventArgs> MessageReceived;
		public event EventHandler<WindowMessageEventArgs> Connected;

		protected override void WndProc(ref Message m)
		{
			if (_signalMessage(m))
			{
				if (MessageReceived != null)
				{
					MessageReceived(this, new WindowMessageEventArgs(m));
				}
			}

			base.WndProc(ref m);
		}
	}
}