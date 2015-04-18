using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Threading;
using System.Windows.Forms;

using Minimod.RxMessageBroker;

using MpcDeleter.Commands;
using MpcDeleter.Messages;
using MpcDeleter.Properties;

namespace MpcDeleter
{
  public partial class MainForm : Form
  {
    readonly IArchivePathSelector _pathSelector;

    public MainForm(IArchivePathSelector pathSelector)
    {
      InitializeComponent();
      components = new NestedContainer(this);

      _pathSelector = pathSelector;

      var subscriptions = new CompositeDisposable(
        RxMessageBrokerMinimod.Default.Register<Log>(m =>
        {
          Debug.WriteLine(m.Message);

          var addedIndex = lbxEvents.Items.Add(m.Message);
          lbxEvents.SelectedIndex = addedIndex;
        }, new SynchronizationContextScheduler(SynchronizationContext.Current)));

      components.Add(new Disposer(_ => subscriptions.Dispose()));
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
      RxMessageBrokerMinimod.Default.Send(new StartMpc(Settings.Default.MpcPath));
    }

    void btnPlayPause_Click(object sender, EventArgs e)
    {
      RxMessageBrokerMinimod.Default.Send(new PlayPauseCommand());
    }

    void btnDeleteCurrent_Click(object sender, EventArgs e)
    {
      RxMessageBrokerMinimod.Default.Send(new DeleteCurrentFileCommand(chkWhatIf.Checked));
    }

    void btnArchiveCurrent_Click(object sender, EventArgs e)
    {
      RxMessageBrokerMinimod.Default.Send(new ArchiveCurrentFileCommand(_pathSelector, chkWhatIf.Checked));
    }

    void btnlFastForward10Percent_Click(object sender, EventArgs e)
    {
      RxMessageBrokerMinimod.Default.Send(new FastForwardCommand(0.1));
    }
  }
}
