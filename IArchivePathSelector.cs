namespace MpcDeleter
{
	public interface IArchivePathSelector
	{
		string GetArchivePathFor(string file);
	}
}