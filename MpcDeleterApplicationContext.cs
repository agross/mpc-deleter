using System;
using System.Linq;
using System.Windows.Forms;

using MpcDelete.Commands;
using MpcDelete.LircKeyHandlers;
using MpcDelete.MpcMessageHandlers;
using MpcDelete.Properties;

namespace MpcDelete
{
	internal class MpcDeleterApplicationContext : ApplicationContext, IContext
	{
		readonly PlayerContext _player = new PlayerContext();
		LircClient _lirc;
		MessageProcessingWindow _messageExchange;

		public MpcDeleterApplicationContext()
		{
			MainForm = new MainForm(this);

			SetUpLirc();
			SetUpMessageExchange();

            Execute(new StartMpcCommand());          

			MainForm.Show();
		}

		public event EventHandler<MessageEventArgs> LogMessage;

		public PlayerContext Player
		{
			get { return _player; }
		}

		public void Execute(ICommand command)
		{
			command.Execute(this);
		}

		public IntPtr MessageExchange
		{
			get { return _messageExchange.Handle; }
		}

		public IntPtr MediaPlayerClassic
		{
			get;
			private set;
		}

		public void InitializeConnectionToMediaPlayerClassic(IntPtr window)
		{
			MediaPlayerClassic = window;
		}

		public void Log(string message, params object[] args)
		{
			var @event = LogMessage;
			if (@event != null)
			{
				@event(this, new MessageEventArgs(String.Format(message, args)));
			}
		}

		void SetUpLirc()
		{
			var lircKeyHandlers = new ILircKeyHandler[] { new ShiftKeyHandler() };

			_lirc = new LircClient();
			_lirc.KeyPressed += (s, e) =>
				{
					var handler = lircKeyHandlers.FirstOrDefault(x => x.CanHandle(e.Message));
					if (handler != null)
					{
						handler.Handle(e.Message, this);
					}
				};
			_lirc.Connect(this, Settings.Default.LircServer, Settings.Default.LircPort);
		}

		void SetUpMessageExchange()
		{
			var messageHandlers = new IMessageHandler[] { new ConnectHandler(), new NowPlayingMessageHandler() };

			_messageExchange = new MessageProcessingWindow(x => x.Msg == NativeConstants.WM_COPYDATA);
			_messageExchange.MessageReceived += (s, e) =>
				{
					var handler = messageHandlers.FirstOrDefault(x => x.CanHandle(e.Message));
					if (handler != null)
					{
						handler.Handle(e.Message, this);
					}
				};
		}

		protected override void OnMainFormClosed(object sender, EventArgs e)
		{
			if (_lirc != null)
			{
				_lirc.Dispose();
			}

			if (_messageExchange != null)
			{
				_messageExchange.Dispose();
			}

			base.OnMainFormClosed(sender, e);
		}
	}
}