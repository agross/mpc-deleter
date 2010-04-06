using MpcDeleter.Commands;

namespace MpcDeleter.LircKeyHandlers
{
	internal class SomeKeyHandler : TimedKeyHandler, ILircKeyHandler
	{
		IArchivePathSelector _archivePathSelector;

		public SomeKeyHandler(IArchivePathSelector archivePathSelector)
		{
			_archivePathSelector = archivePathSelector;
		}

		protected override string KeyName
		{
			get { return "Some"; }
		}

		public bool CanHandle(string message)
		{
			return message.Contains(" some ");
		}

		protected override void HandleKey(IContext context)
		{
			context.Log("You pressed the Some key, will now attempt to delete the current file");
			context.Execute(new ArchiveCurrentFileCommand(_archivePathSelector, false));
		}
	}
}