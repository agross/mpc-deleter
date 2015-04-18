using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

using MpcDeleter.Commands;

namespace MpcDeleter.LircKeyHandlers
{
  class ShiftKeyHandler : ILircKeyHandler
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

    static bool CanHandle(string message)
    {
      return message.Contains(" shift ") || message.Contains(" guide ");
    }

    static void Handle(IContext context)
    {
      context.Log("You pressed the Shift key, will now attempt to delete the current file");
      context.Execute(new DeleteCurrentFileCommand(false));
    }
  }
}
