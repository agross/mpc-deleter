using System;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;

using Minimod.RxMessageBroker;

using MpcDeleter.Archiving;
using MpcDeleter.Commands;
using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Commands
{
  class ArchiveCurrentFileHandler : ICommandHandler
  {
    class Params
    {
      public Params(CurrentFile file, ArchiveCurrentFile command)
      {
        File = file;
        Command = command;
      }

      public CurrentFile File { get; set; }
      public ArchiveCurrentFile Command { get; set; }
    }

    readonly IArchivePathSelector _archivePathSelector;

    public ArchiveCurrentFileHandler(IArchivePathSelector archivePathSelector)
    {
      _archivePathSelector = archivePathSelector;
    }

    public IDisposable SetUp(IScheduler scheduler)
    {
      var bus = RxMessageBrokerMinimod.Default;

      return ArchiveCurrentFile(bus.Stream)
        .ObserveOn(new EventLoopScheduler(ts => new Thread(ts)
        {
          Name = string.Format("{0} Thread", GetType().Name),
          IsBackground = true
        }))
        .Subscribe(x => MoveToArchive(bus, x));
    }

    static IObservable<Params> ArchiveCurrentFile(IObservable<object> stream)
    {
      var archive = stream.OfType<ArchiveCurrentFile>();
      var file = stream.OfType<CurrentFile>();

      return file.CombineLatest(archive, (f, arch) => new Params(f, arch))
                 .DistinctUntilChanged(data => data.Command);
    }

    void MoveToArchive(RxMessageBrokerMinimod bus, Params @params)
    {
      var file = @params.File.FileName;
      var archiveFile = _archivePathSelector.GetArchivePathFor(file);

      bus.Send(new AdvanceToNextFile());

      if (@params.Command.WhatIf)
      {
        bus.Send(new Log("WHATIF: Archiving file {0} to {1}", file, archiveFile));
        return;
      }

      try
      {
        archiveFile = CopyFile(file, archiveFile);

        bus.Send(new Log("Archived file {0} to {1}, now going to delete", file, archiveFile));
        bus.Send(new DeleteFile(@params.File.FileName, @params.Command.WhatIf));
      }
      catch (Exception ex)
      {
        bus.Send(new Log("Failed to archive file {0} to {1}: {2}", file, archiveFile, ex.Message));
      }
    }

    static string CopyFile(string file, string archiveFile)
    {
      var originalArchiveFile = archiveFile;

      var counter = 1;
      while (counter < 20)
      {
        try
        {
          Directory.CreateDirectory(Path.GetDirectoryName(archiveFile));
          File.Copy(file, archiveFile);

          return archiveFile;
        }
        catch (IOException)
        {
          archiveFile = AppendCounter(originalArchiveFile, counter);
          counter++;
        }
      }

      throw new Exception("Failed to copy without overwriting");
    }

    static string AppendCounter(string archiveFile, int counter)
    {
      var directory = Path.GetDirectoryName(archiveFile);
      var fileName = Path.GetFileNameWithoutExtension(archiveFile);
      var extension = Path.GetExtension(archiveFile);

      return Path.Combine(directory, fileName + counter + extension);
    }
  }
}
