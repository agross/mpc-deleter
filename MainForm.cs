using System;
using System.Threading;
using System.Windows.Forms;

using MpcDelete.Commands;

namespace MpcDelete
{
	public partial class MainForm : Form
	{
		readonly IContext _context;

		public MainForm(IContext context)
		{
			InitializeComponent();

			_context = context;
			_context.LogMessage += (s, e) => SynchronizationContext.Current.Send(x =>
				{
					var addedIndex = lbxEvents.Items.Add(e.Message);
					lbxEvents.SelectedIndex = addedIndex;
				},
			                                                                     null);
		}

		void btnStartMpc_Click(object sender, EventArgs e)
		{
			_context.Execute(new StartMpcCommand());
		}

		void btnPlayPause_Click(object sender, EventArgs e)
		{
			_context.Execute(new PlayPauseCommand());
		}

		void btnDeleteCurrent_Click(object sender, EventArgs e)
		{
			_context.Execute(new DeleteCurrentFileCommand(false));
		}

		void btnDeleteCurrentWhatIf_Click(object sender, EventArgs e)
		{
			_context.Execute(new DeleteCurrentFileCommand(true));
		}
	}
}