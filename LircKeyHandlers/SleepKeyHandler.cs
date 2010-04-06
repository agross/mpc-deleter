using MpcDeleter.Commands;

namespace MpcDeleter.LircKeyHandlers
{
	internal class SleepKeyHandler : TimedKeyHandler, ILircKeyHandler
	{
		readonly IArchivePathSelector _archivePathSelector;

		public SleepKeyHandler(IArchivePathSelector archivePathSelector)
		{
			_archivePathSelector = archivePathSelector;
		}

		protected override string KeyName
		{
			get { return "Sleep"; }
		}

		public bool CanHandle(string message)
		{
			return message.Contains(" sleep ");
		}

		protected override void HandleKey(IContext context)
		{
			context.Log("You pressed the Sleep key, will now attempt to archive the current file");
			context.Execute(new ArchiveCurrentFileCommand(_archivePathSelector, false));
		}
	}
}