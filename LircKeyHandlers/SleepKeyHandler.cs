using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

using MpcDeleter.Commands;

namespace MpcDeleter.LircKeyHandlers
{
  class SleepKeyHandler : ILircKeyHandler
  {
    public IDisposable SetUp(IObservable<string> source, IContext context)
    {
      return source
        .Where(CanHandle)
        .DelayBetweenValues(TimeSpan.FromSeconds(10),
                            TaskPoolScheduler.Default,
                            () => context.Log("You pressed the Sleep key in quick succession. " +
                                              "This key press will be ignored."))
        .Subscribe(x => Handle(context));
    }

    readonly IArchivePathSelector _archivePathSelector;

    public SleepKeyHandler(IArchivePathSelector archivePathSelector)
    {
      _archivePathSelector = archivePathSelector;
    }

    static bool CanHandle(string message)
    {
      return message.Contains(" sleep ") || message.Contains(" ok ");
    }

    void Handle(IContext context)
    {
      context.Log("You pressed the Sleep key, will now attempt to archive the current file");
      context.Execute(new ArchiveCurrentFileCommand(_archivePathSelector, false));
    }
  }
}
