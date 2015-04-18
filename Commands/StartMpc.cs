namespace MpcDeleter.Commands
{
  class StartMpc
  {
    public string Path { get; private set; }

    public StartMpc(string path)
    {
      Path = path;
    }

    public override string ToString()
    {
      return string.Format("Starting MPC at {0}", Path);
    }
  }
}
