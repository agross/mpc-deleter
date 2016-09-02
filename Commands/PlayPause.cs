namespace MpcDeleter.Commands
{
  class PlayPause : SendMessage
  {
    public PlayPause() : base(NativeConstants.CMD_PLAYPAUSE)
    {
    }
  }
}
