using System;
using System.Reactive.Concurrency;

namespace MpcDeleter.Handlers.Commands
{
  interface ICommandHandler
  {
    IDisposable SetUp(IScheduler scheduler);
  }
}