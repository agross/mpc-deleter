using System;
using System.Threading;
using System.Windows.Forms;

namespace MpcDeleter
{
  static class Program
  {
    static readonly Mutex Mutex = new Mutex(true, "{8535db41-c8d1-4b6a-be49-25fe7832df18}");

    [STAThread]
    static void Main()
    {
      if (!Mutex.WaitOne(TimeSpan.Zero, true))
      {
        ActivateOtherInstance();
        return;
      }

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(true);
      Application.Run(new MpcDeleterApplicationContext());
    }

    static void ActivateOtherInstance()
    {
      NativeMethods.PostMessage((IntPtr) NativeMethods.HWND_BROADCAST,
                                NativeMethods.WM_SHOWME,
                                IntPtr.Zero,
                                IntPtr.Zero);
    }
  }
}
