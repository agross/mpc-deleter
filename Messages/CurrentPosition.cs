namespace MpcDeleter.Messages
{
  class CurrentPosition
  {
    public CurrentPosition(int length)
    {
      Length = length;
    }

    public int Length { get; private set; }

    public override string ToString()
    {
      return string.Format("Current position is at {0} seconds", Length);
    }
  }
}
