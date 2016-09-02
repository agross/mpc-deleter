using System;

namespace MpcDeleter.Messages
{
  class ReceiverCreated
  {
    public ReceiverCreated(IntPtr handle)
    {
      Handle = handle;
    }

    public IntPtr Handle { get; private set; }

    public override string ToString()
    {
      return string.Format("Receiving message from MPC through window handle {0:X}", Handle.ToInt64());
    }
  }
}
