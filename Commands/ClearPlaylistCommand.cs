namespace MpcDeleter.Commands
{
	class ClearPlaylistCommand : SendMessageCommand
	{
		public ClearPlaylistCommand() : base(NativeConstants.CMD_CLEARPLAYLIST, null)
		{
		}
	}
}