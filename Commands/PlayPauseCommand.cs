namespace MpcDelete.Commands
{
	internal class PlayPauseCommand : SendMessageCommand
	{
		public PlayPauseCommand() : base(NativeConstants.CMD_PLAYPAUSE, null)
		{
		}
	}
}