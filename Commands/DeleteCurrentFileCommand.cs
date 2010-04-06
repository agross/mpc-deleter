using System;
using System.IO;

namespace MpcDeleter.Commands
{
	internal class DeleteCurrentFileCommand : ICommand
	{
		readonly bool _whatIf;

		public DeleteCurrentFileCommand(bool whatIf)
		{
			_whatIf = whatIf;
		}

		public void Execute(IContext context)
		{
			var file = context.Player.CurrentFile;

			context.Execute(new AdvanceToNextFileCommand());

			DeleteFile(context, file);
		}

		void DeleteFile(IContext context, string file)
		{
			if (_whatIf)
			{
				context.Log("WHATIF: Deleting file {0}", file);
				return;
			}

			try
			{
				File.Delete(file);
				context.Log("Deleted file {0}", file);
			}
			catch (Exception ex)
			{
				context.Log("Failed to delete file {0}, {1}", file, ex.Message);
			}
		}
	}
}