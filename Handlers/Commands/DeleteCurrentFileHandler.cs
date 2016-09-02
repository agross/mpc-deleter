using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

using Minimod.RxMessageBroker;

using MpcDeleter.Commands;
using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Commands
{
  class DeleteCurrentFileHandler : ICommandHandler
  {
    class Params
    {
      public Params(CurrentFile file, DeleteCurrentFile command)
      {
        File = file;
        Command = command;
      }

      public CurrentFile File { get; set; }
      public DeleteCurrentFile Command { get; set; }
    }

    public IDisposable SetUp(IScheduler scheduler)
    {
      var bus = RxMessageBrokerMinimod.Default;

      return DeleteCurrentFile(bus.Stream)
        .ObserveOn(scheduler)
        .Subscribe(x => DeleteFile(bus, x));
    }

    static IObservable<Params> DeleteCurrentFile(IObservable<object> stream)
    {
      var delete = stream.OfType<DeleteCurrentFile>();
      var file = stream.OfType<CurrentFile>();

      return file.CombineLatest(delete, (f, d) => new { File = f, Command = d })
                 .DistinctUntilChanged(data => data.Command)
                 .Select(x => new Params(x.File, x.Command));
    }

    static void DeleteFile(RxMessageBrokerMinimod bus, Params @params)
    {
      var file = @params.File.FileName;

      bus.Send(new AdvanceToNextFile());
      bus.Send(new DeleteFile(file, @params.Command.WhatIf));
    }
  }
}
