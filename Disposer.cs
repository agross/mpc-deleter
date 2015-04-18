using System;
using System.ComponentModel;

namespace MpcDeleter
{
  class Disposer : Component
  {
    readonly Action<bool> _callback;

    internal Disposer(Action<bool> callback)
    {
      _callback = callback;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);

      _callback(disposing);
    }
  }
}
