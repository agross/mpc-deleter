using System;
using System.Runtime.InteropServices;

namespace MpcDeleter
{
	internal class NativeMethods
	{
		[DllImport("user32.dll")]
		internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

	  internal const int HWND_BROADCAST = 0xffff;
	  internal static readonly int WM_SHOWME = RegisterWindowMessage("WM_SHOWME");
    
    [DllImport("user32")]
    internal static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

    [DllImport("user32")]
    internal static extern int RegisterWindowMessage(string message);
  }
}