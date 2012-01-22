using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MpcDeleter.MpcMessageHandlers
{
	class ConnectHandler : AbstractMpcMessageHandler
	{
		public override bool CanHandle(Message message)
		{
			return message.Matches(NativeConstants.CMD_CONNECT);
		}

		public override void Handle(Message message, IContext context)
		{
			var copiedData = message.GetCopiedData();

			var handleAsString = Marshal.PtrToStringUni(copiedData.lpData);
			var handle = new IntPtr(Int64.Parse(handleAsString));

			Bus.Publish(new ConnectionEstablished(handle));
		}
	}

	class ConnectionEstablished : IMessage
	{
		public ConnectionEstablished(IntPtr handle)
		{
			Handle = handle;
		}

		public IntPtr Handle
		{
			get;
			private set;
		}
	}
}