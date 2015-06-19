using System;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

using Minimod.RxMessageBroker;

using MpcDeleter.Commands;
using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Commands
{
  class DeleteFileHandler : ICommandHandler
  {
    public IDisposable SetUp(IScheduler scheduler)
    {
      var bus = RxMessageBrokerMinimod.Default;

      return bus.Stream
                .OfType<DeleteFile>()
                .ObserveOn(new EventLoopScheduler(ts => new Thread(ts)
                {
                  Name = string.Format("{0} Thread", GetType().Name),
                  IsBackground = true
                }))
                .Subscribe(x => DeleteFile(bus, x));
    }

    static void DeleteFile(RxMessageBrokerMinimod bus, DeleteFile command)
    {
      var file = command.FileName;

      if (command.WhatIf)
      {
        bus.Send(new Log("WHATIF: Deleting file {0}", file));
        return;
      }

      var attempt = 0;
      while (true)
      {
        if (StopTrying(attempt, 10))
        {
          bus.Send(new Log("Giving up deleting file {0} after {1} attempts", file, attempt));
          return;
        }

        try
        {
          attempt++;
          File.Delete(file);

          bus.Send(new Log("Deleted file {0}", file));
          return;
        }
        catch (Exception ex)
        {
          bus.Send(new Log("Failed to delete file {0}, attempt {1}: {2}", file, attempt, ex.Message));
          Thread.Sleep(TimeSpan.FromSeconds(1));
        }
      }
    }

    static bool StopTrying(int attempt, int maxAttempts)
    {
      return attempt >= maxAttempts;
    }
  }
}
