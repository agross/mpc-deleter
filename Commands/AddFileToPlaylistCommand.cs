namespace MpcDeleter.Commands
{
	internal class AddFileToPlaylistCommand : ICommand
	{
		readonly string _fileName;

		public AddFileToPlaylistCommand(string fileName)
		{
			_fileName = fileName;
		}

		public void Execute(IContext context)
		{
			new SendMessageCommand(NativeConstants.CMD_ADDTOPLAYLIST, _fileName);
		}
	}
}