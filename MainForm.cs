using System;
using System.Threading;
using System.Windows.Forms;

using MpcDeleter.Commands;

namespace MpcDeleter
{
	public partial class MainForm : Form
	{
		readonly IContext _context;
		readonly IArchivePathSelector _pathSelector;

		public MainForm(IContext context, IArchivePathSelector pathSelector)
		{
			InitializeComponent();

			_context = context;
			_pathSelector = pathSelector;
			_context.LogMessage += (s, e) => SynchronizationContext.Current.Send(x =>
				{
					var addedIndex = lbxEvents.Items.Add(e.Message);
					lbxEvents.SelectedIndex = addedIndex;
				},
			                                                                     null);
		}

		void btnStartMpc_Click(object sender, EventArgs e)
		{
			Execute(new StartMpcCommand());
		}

		void btnPlayPause_Click(object sender, EventArgs e)
		{
			Execute(new PlayPauseCommand());
		}

		void btnDeleteCurrent_Click(object sender, EventArgs e)
		{
			var command = new DeleteCurrentFileCommand(chkWhatIf.Checked);

			Execute(command);
		}

		void btnArchiveCurrent_Click(object sender, EventArgs e)
		{
			var command = new ArchiveCurrentFileCommand(_pathSelector, chkWhatIf.Checked);

			Execute(command);
		}

		void Execute(ICommand command)
		{
			_context.Execute(command);
		}

		void btnClearPlaylist_Click(object sender, EventArgs e)
		{
			Execute(new ClearPlaylistCommand());
		}

		private void btnlLoadPlaylist_Click(object sender, EventArgs e)
		{
			Execute(new AutoLoadPlaylistCommand(ApplicationSettings.PlaylistFolders));
		}
	}
}