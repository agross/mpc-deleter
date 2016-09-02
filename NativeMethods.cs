using System;
using System.Runtime.InteropServices;

namespace MpcDeleter
{
  class NativeMethods
  {
    [DllImport("user32.dll")]
    internal static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32")]
    internal static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

    [DllImport("user32")]
    internal static extern int RegisterWindowMessage(string message);
  }
}
