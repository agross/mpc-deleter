using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

using Minimod.RxMessageBroker;

using MpcDeleter.Commands;
using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Lirc
{
  class SleepKey : ILircKeyHandler
  {
    public IDisposable SetUp(IObservable<string> source)
    {
      return source
        .Where(CanHandle)
        .DelayBetweenValues(TimeSpan.FromSeconds(10),
                            TaskPoolScheduler.Default,
                            () => RxMessageBrokerMinimod.Default.Send(new Log("You pressed the Sleep key in quick succession. " +
                                                                              "This key press will be ignored.")))
        .Subscribe(x => Handle());
    }

    readonly IArchivePathSelector _archivePathSelector;

    // TODO move to handler
    public SleepKey(IArchivePathSelector archivePathSelector)
    {
      _archivePathSelector = archivePathSelector;
    }

    static bool CanHandle(string message)
    {
      return message.Contains(" sleep ") || message.Contains(" ok ");
    }

    void Handle()
    {
      RxMessageBrokerMinimod.Default.Send(new ArchiveCurrentFileCommand(_archivePathSelector, false));
    }
  }
}
