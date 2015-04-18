using System;

namespace MpcDeleter.Messages
{
  class ConnectionEstablished
  {
    public ConnectionEstablished(IntPtr handle)
    {
      Handle = handle;
    }

    public IntPtr Handle { get; private set; }

    public override string ToString()
    {
      return string.Format("Started MPC with main window handle {0:X}", Handle.ToInt64());
    }
  }
}
