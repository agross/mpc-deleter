using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MpcDeleter.Commands
{
	class SendMessageCommand : ICommand
	{
		readonly string _data;
		readonly uint _message;

		public SendMessageCommand(uint message, string data)
		{
			_message = message;
			_data = data;
		}

		public void Execute(IContext context)
		{
			IntPtr copyDataMem = IntPtr.Zero;
			IntPtr dataPointer = IntPtr.Zero;
			try
			{
				var dataSize = 0;
				dataPointer = IntPtr.Zero;
				if (_data != null)
				{
					// Same algorithm to determine size as in Marshal.StringToHGlobalUni.
					dataSize = (_data.Length + 1) * 2;
					dataPointer = Marshal.StringToHGlobalUni(_data);
				}

				var copyData = new COPYDATASTRUCT
				               {
				               	dwData = new UIntPtr(_message),
				               	cbData = dataSize,
				               	lpData = dataPointer
				               };

				copyDataMem = Marshal.AllocHGlobal(Marshal.SizeOf(copyData));
				Marshal.StructureToPtr(copyData, copyDataMem, true);
				var result = NativeMethods.SendMessage(context.MediaPlayerClassic,
				                                       NativeConstants.WM_COPYDATA,
				                                       context.MessageExchange,
				                                       copyDataMem);
				if (result == IntPtr.Zero)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
				context.Log("Sent message to MPC");
			}
			catch (Exception ex)
			{
				context.Log("Failed to send message to MPC: {0}", ex.Message);
			}
			finally
			{
				if (copyDataMem != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(copyDataMem);
				}
				if (dataPointer != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(dataPointer);
				}
			}
		}
	}
}