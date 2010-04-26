namespace MpcDeleter
{
	public class PlayerContext
	{
		public string CurrentFile
		{
			get;
			private set;
		}

		public int CurrentFileLength
		{
			get;
			private set;
		}

		public void UpdateCurrentFile(string fileName, int length)
		{
			CurrentFile = fileName;
			CurrentFileLength = length == 0 ? int.MaxValue : length;
		}
	}
}