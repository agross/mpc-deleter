using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using MpcDeleter.Commands;

namespace MpcDeleter.MpcMessageHandlers
{
	public class CurrentPositionHandler : AbstractMpcMessageHandler
	{
		public override bool CanHandle(Message message)
		{
			if (message.Msg != NativeConstants.WM_COPYDATA)
			{
				return false;
			}

			var cds = GetCopiedData(message);
			return cds.dwData == new UIntPtr(NativeConstants.CMD_CURRENTPOSITION);
		}

		public override void Handle(Message message, IContext context)
		{
			var cds = GetCopiedData(message);

			var pos = Marshal.PtrToStringUni(cds.lpData);
			var position = int.Parse(pos);
			var advance = (int) (context.Player.CurrentFileLength * 0.1);

			context.Log("Current position {0} seconds", position);
			context.Execute(new FastForwardCommand(position + advance));
		}
	}
}