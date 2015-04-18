using System;
using System.Reactive.Concurrency;

namespace MpcDeleter.Handlers.Commands
{
  public interface ICommandHandler
  {
    IDisposable SetUp(IScheduler scheduler);
  }
}