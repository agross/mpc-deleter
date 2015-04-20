namespace MpcDeleter.Messages
{
  class CurrentFile
  {
    public string FileName { get; private set; }
    public int Length { get; private set; }

    public CurrentFile(string fileName, int length)
    {
      FileName = fileName;
      Length = length;
    }

    public override string ToString()
    {
      return string.Format("Now playing: {0}, length {1} seconds",
                           FileName,
                           Length);
    }

    protected bool Equals(CurrentFile other)
    {
      return string.Equals(FileName, other.FileName) && Length == other.Length;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj))
      {
        return false;
      }
      if (ReferenceEquals(this, obj))
      {
        return true;
      }
      if (obj.GetType() != GetType())
      {
        return false;
      }
      return Equals((CurrentFile) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((FileName != null ? FileName.GetHashCode() : 0) * 397) ^ Length;
      }
    }
  }
}
