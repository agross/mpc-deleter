using System;

namespace MpcDeleter.LircKeyHandlers
{
  interface ILircKeyHandler
  {
    IDisposable SetUp(IObservable<string> source, IContext context);
  }
}
