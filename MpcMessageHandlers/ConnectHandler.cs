using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MpcDelete.MpcMessageHandlers
{
	public class ConnectHandler : AbstractMpcMessageHandler
	{
		public override bool CanHandle(Message message)
		{
			if (message.Msg != NativeConstants.WM_COPYDATA)
			{
				return false;
			}

			var cds = GetCopiedData(message);
			return cds.dwData == new UIntPtr(NativeConstants.CMD_CONNECT);
		}

		public override void Handle(Message message, IContext context)
		{
			var cds = GetCopiedData(message);

			var handle = Marshal.PtrToStringUni(cds.lpData);

			context.InitializeConnectionToMediaPlayerClassic(new IntPtr(Int64.Parse(handle)));

			context.Log("Connected to MPC at {0:X}", context.MediaPlayerClassic.ToInt64());
		}
	}
}