using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

using Minimod.RxMessageBroker;

using MpcDeleter.Commands;
using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Lirc
{
  class ArchiveCurrentFileKey : ILircKeyHandler
  {
    public IDisposable SetUp(IObservable<string> source)
    {
      return source
        .Where(Matches)
        .DelayBetweenValues(TimeSpan.FromSeconds(10),
                            TaskPoolScheduler.Default,
                            () => RxMessageBrokerMinimod.Default.Send(new Log("You pressed the 'Archive current file' key in quick succession. " +
                                                                              "This key press will be ignored.")))
        .Subscribe(x => Handle());
    }

    static bool Matches(string message)
    {
      return message.Contains(" sleep ") || message.Contains(" ok ");
    }

    static void Handle()
    {
      RxMessageBrokerMinimod.Default.Send(new ArchiveCurrentFile(false));
    }
  }
}
