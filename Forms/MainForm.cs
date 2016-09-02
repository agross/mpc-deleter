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

namespace MpcDeleter.Forms
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
      components = new NestedContainer(this);

      var uiThread = new SynchronizationContextScheduler(SynchronizationContext.Current);

      var subscriptions = new CompositeDisposable(
        RxMessageBrokerMinimod.Default.Register<Log>(m =>
        {
          Debug.WriteLine(m.Message);

          var addedIndex = lbxEvents.Items.Add(m.Message);
          lbxEvents.SelectedIndex = addedIndex;
        }, uiThread),
        RxMessageBrokerMinimod.Default.Register<CurrentFile>(m =>
        {
          lblCurrentFile.Text = m.FileName;
          lblLength.Text = m.Length.ToString();
        }, uiThread));

      components.Add(new Disposer(_ => subscriptions.Dispose()));
    }

    protected override void WndProc(ref Message m)
    {
      if (m.Msg == NativeConstants.WM_SHOWME)
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
      RxMessageBrokerMinimod.Default.Send(new PlayPause());
    }

    void btnDeleteCurrent_Click(object sender, EventArgs e)
    {
      RxMessageBrokerMinimod.Default.Send(new DeleteCurrentFile(chkWhatIf.Checked));
    }

    void btnArchiveCurrent_Click(object sender, EventArgs e)
    {
      RxMessageBrokerMinimod.Default.Send(new ArchiveCurrentFile(chkWhatIf.Checked));
    }

    void btnlFastForward10Percent_Click(object sender, EventArgs e)
    {
      RxMessageBrokerMinimod.Default.Send(new FastForward(0.1));
    }

    void btnNext_Click(object sender, EventArgs e)
    {
      RxMessageBrokerMinimod.Default.Send(new AdvanceToNextFile());
    }
  }
}
