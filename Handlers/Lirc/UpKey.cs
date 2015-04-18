using System;
using System.Reactive.Linq;

using Minimod.RxMessageBroker;

using MpcDeleter.Commands;

namespace MpcDeleter.Handlers.Lirc
{
  class UpKey : ILircKeyHandler
  {
    public IDisposable SetUp(IObservable<string> source)
    {
      return source.Where(CanHandle).Subscribe(x => Handle());
    }

    static bool CanHandle(string message)
    {
      return message.Contains(" up ");
    }

    static void Handle()
    {
      // TODO RxMessageBrokerMinimod.Default.Send(new FastForward(.1));
    }
  }
}
