using System;
using System.Runtime.InteropServices;

namespace MpcDeleter
{
	internal static class NativeConstants
	{
		public static uint CMD_CONNECT = 0x50000000;

		// Not implemented -- great!
		// public static uint CMD_GETNOWPLAYING = 0xA0003002;
		public static uint CMD_NOWPLAYING = 0x50000003;
		public static uint CMD_PLAYPAUSE = 0xA0000003;
		public static uint CMD_SETPOSITION = 0xA0002000;
		public static uint CMD_STOP = 0xA0000001;
		public static uint CMD_CLEARPLAYLIST = 0xA0001001;
		public static uint CMD_ADDTOPLAYLIST = 0xA0001000;
		public static uint WM_COPYDATA = 0x004A;
		public static uint CMD_CURRENTPOSITION = 0x50000007;
		public static uint CMD_GETCURRENTPOSITION = 0xA0003004;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct COPYDATASTRUCT
	{
		public UIntPtr dwData;
		public int cbData;
		public IntPtr lpData;
	}
}