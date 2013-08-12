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
      _context.LogMessage += (s, e) =>
      {
        Action appendToLog = () => SynchronizationContext.Current.Send(x =>
        {
          var addedIndex = lbxEvents.Items.Add(e.Message);
          lbxEvents.SelectedIndex = addedIndex;
        },
                                                                       null);
        if (InvokeRequired)
        {
          Invoke(appendToLog);
        }
        else
        {
          appendToLog();
        }
      };
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == NativeMethods.WM_SHOWME)
      {
        RestoreWindow();
      }

      base.WndProc(ref m);
    }

    void RestoreWindow()
    {
      if (WindowState == FormWindowState.Minimized)
      {
        WindowState = FormWindowState.Normal;
      }
      var topMost = TopMost;
      TopMost = true;
      TopMost = topMost;
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

    void btnlFastForward10Percent_Click(object sender, EventArgs e)
    {
      var command = new FastForwardCommand(0.1);

      Execute(command);
    }

    void Execute(ICommand command)
    {
      _context.Execute(command);
    }
  }
}
