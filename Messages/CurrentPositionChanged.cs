namespace MpcDeleter.Messages
{
  class CurrentPositionChanged
  {
    public CurrentPositionChanged(int position)
    {
      Position = position;
    }

    public int Position { get; private set; }

    public override string ToString()
    {
      return string.Format("Current position is at {0} seconds", Position);
    }
  }
}
