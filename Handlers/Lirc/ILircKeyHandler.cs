using System;

namespace MpcDeleter.Handlers.Lirc
{
  interface ILircKeyHandler
  {
    IDisposable SetUp(IObservable<string> source);
  }
}
