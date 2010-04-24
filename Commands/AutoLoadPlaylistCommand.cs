using System.IO;
using System.Linq;

namespace MpcDeleter.Commands
{
	internal class AutoLoadPlaylistCommand : ICommand
	{
		readonly string[] _folders;

		public AutoLoadPlaylistCommand(string[] folders)
		{
			_folders = folders;
		}

		public void Execute(IContext context)
		{
			_folders
				.Select(x => Directory.GetFiles(x, "*.*", SearchOption.TopDirectoryOnly))
				.SelectMany(x => x)
				.Distinct()
				.Select(x => new SendMessageCommand(NativeConstants.CMD_ADDTOPLAYLIST, x))
				.Each(x => x.Execute(context));
		}
	}
}