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

      if (StopTrying(command))
      {
        bus.Send(new Log("Giving up deleting file {0}", file));
        return;
      }

      try
      {
        File.Delete(file);

        bus.Send(new Log("Deleted file {0}", file));
      }
      catch (Exception ex)
      {
        bus.Send(new Log("Failed to delete file {0}: {1}", file, ex.Message));
        Thread.Sleep(TimeSpan.FromSeconds(5));
        bus.Send(new DeleteFile(command.FileName, command.WhatIf, command.NumberOfTriesLeft - 1));
      }
    }

    static bool StopTrying(DeleteFile command)
    {
      return command.NumberOfTriesLeft <= 0;
    }
  }
}
