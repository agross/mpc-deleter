namespace MpcDeleter
{
	internal interface IArchivePathSelector
	{
		string GetArchivePathFor(string file);
	}
}