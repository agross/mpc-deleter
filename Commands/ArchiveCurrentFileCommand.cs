using System;
using System.IO;
using System.Threading;

namespace MpcDeleter.Commands
{
	internal class ArchiveCurrentFileCommand : ICommand
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

			context.Execute(new AdvanceToNextFileCommand());

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
				File.Move(file, archiveFile);

				context.Log("Archived file {0} to {1}", file, archiveFile);
			}
			catch (Exception ex)
			{
				context.Log("Failed to archive file {0} to {1}, {2}", file, archiveFile, ex.Message);
			}
		}
	}
}