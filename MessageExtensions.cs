using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using MpcDeleter.MpcMessageHandlers;

namespace MpcDeleter
{
	static class MessageExtensions
	{
		static UIntPtr ExtractMpcCommand(this Message message)
		{
			if (message.Msg != NativeConstants.WM_COPYDATA)
			{
				return UIntPtr.Zero;
			}

			var copiedData = AbstractMpcMessageHandler.GetCopiedData(message);
			return copiedData.dwData;
		}

		public static bool Matches(this Message message, uint command)
		{
			return message.ExtractMpcCommand() == new UIntPtr(command);
		}

		public static COPYDATASTRUCT GetCopiedData(this Message message)
		{
			return (COPYDATASTRUCT) Marshal.PtrToStructure(message.LParam, typeof(COPYDATASTRUCT));
		}
	}
}