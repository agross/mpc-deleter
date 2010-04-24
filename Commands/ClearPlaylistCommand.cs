namespace MpcDeleter.Commands
{
	internal class ClearPlaylistCommand : SendMessageCommand
	{
		public ClearPlaylistCommand() : base(NativeConstants.CMD_CLEARPLAYLIST, null)
		{
		}
	}
}