using System;
using System.Runtime.InteropServices;

namespace MpcDelete
{
	internal class NativeMethods
	{
		[DllImport("user32.dll")]
		internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
	}
}