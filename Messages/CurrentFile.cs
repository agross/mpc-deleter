namespace MpcDeleter.Messages
{
  class CurrentFile
  {
    public CurrentFile(string fileName, int length)
    {
      FileName = fileName;
      Length = length;
    }

    public string FileName { get; private set; }
    public int Length { get; private set; }

    public override string ToString()
    {
      return string.Format("Now playing: {0}, length {1} seconds",
                           FileName,
                           Length);
    }
  }
}
