using System;
using System.IO;
using System.Threading;

namespace MpcDeleter.Commands
{
	class ArchiveCurrentFileCommand : ICommand
	{
		readonly IArchivePathSelector _archivePathSelector;
		readonly bool _whatIf;

		public ArchiveCurrentFileCommand(IArchivePathSelector archivePathSelector, bool whatIf)
		{
			_archivePathSelector = archivePathSelector;
			_whatIf = whatIf;
		}

		public void Execute(IContext context)
		{
			var file = context.Player.CurrentFile;

			// TODO context.Execute(new AdvanceToNextFileHandler());

			MoveToArchive(context, file);
		}

		void MoveToArchive(IContext context, string file)
		{
			var archiveFile = _archivePathSelector.GetArchivePathFor(file);
			if (_whatIf)
			{
				context.Log("WHATIF: Archiving file {0} to {1}", file, archiveFile);
				return;
			}

			try
			{
				Thread.Sleep(TimeSpan.FromSeconds(3));
				Directory.CreateDirectory(Path.GetDirectoryName(archiveFile));

				var originalArchiveFile = archiveFile;
				var retry = true;
				var counter = 1;
				while (retry)
				{
					try
					{
						File.Copy(file, archiveFile);
						retry = false;
					}
					catch (IOException)
					{
						archiveFile = IncrementFileName(originalArchiveFile, ref counter);
					}
				}

				context.Log("Archived file {0} to {1}, now going to delete", file, archiveFile);
        context.Execute(new DeleteFileWithRetry(4, file));
			}
			catch (Exception ex)
			{
				context.Log("Failed to archive file {0} to {1}, {2}", file, archiveFile, ex.Message);
			}
		}

		static string IncrementFileName(string archiveFile, ref int counter)
		{
			var path = Path.GetDirectoryName(archiveFile);
			var fileName = Path.GetFileNameWithoutExtension(archiveFile);
			var extension = Path.GetExtension(archiveFile);

			fileName = fileName + counter;
			counter += 1;

			return Path.Combine(path, fileName + extension);
		}

	  public override string ToString()
	  {
	    return string.Format("Attempting to archive the current file {0}", "TODO");
	  }
	}
}