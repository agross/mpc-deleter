namespace MpcDeleter.Commands
{
  class FastForward
  {
    public double PercentOfLength { get; private set; }

    public FastForward(double percentOfLength)
    {
      PercentOfLength = percentOfLength;
    }

    public override string ToString()
    {
      return string.Format("Fast-forwarding {0:P}", PercentOfLength);
    }
  }
}
