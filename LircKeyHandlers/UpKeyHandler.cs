using System;
using System.Reactive.Linq;

using MpcDeleter.Commands;

namespace MpcDeleter.LircKeyHandlers
{
  class UpKeyHandler : ILircKeyHandler
  {
    public IDisposable SetUp(IObservable<string> source, IContext context)
    {
      return source.Where(CanHandle).Subscribe(x => Handle(context));
    }

    static bool CanHandle(string message)
    {
      return message.Contains(" up ");
    }

    static void Handle(IContext context)
    {
      context.Log("Fast-forwarding 10%");
      context.Execute(new FastForwardCommand(.1));
    }
  }
}
