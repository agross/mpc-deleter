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
      return source.Where(Matches).Subscribe(x => Handle());
    }

    static bool Matches(string message)
    {
      return message.Contains(" up ");
    }

    static void Handle()
    {
      RxMessageBrokerMinimod.Default.Send(new FastForward(.1));
    }
  }
}
