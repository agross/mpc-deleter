using System;

namespace MpcDeleter.Messages
{
  class ReceiverDestroyed
  {
    public ReceiverDestroyed(IntPtr handle)
    {
      Handle = handle;
    }

    public IntPtr Handle { get; private set; }

    public override string ToString()
    {
      return string.Format("Destroyed message receiver window handle {0:X}", Handle.ToInt64());
    }
  }
}