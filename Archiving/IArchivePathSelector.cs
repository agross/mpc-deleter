namespace MpcDeleter.Archiving
{
  interface IArchivePathSelector
  {
    string GetArchivePathFor(string file);
  }
}
