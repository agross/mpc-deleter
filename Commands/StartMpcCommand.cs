using System;
using System.Diagnostics;
using System.Globalization;

using MpcDeleter.MpcMessageHandlers;
using MpcDeleter.Properties;

namespace MpcDeleter.Commands
{
	class StartMpcCommand : ICommand
	{
		IDisposable _unsubscriber;

		public void Execute(IContext context)
		{
			StartMpcInSlaveMode(context);
		}

		void SetUpConnection(IContext context, ConnectionEstablished message)
		{
			context.InitializeConnectionToMediaPlayerClassic(message.Handle);

			context.Log("Connected to MPC at {0:X}", context.MediaPlayerClassic.ToInt64());

			_unsubscriber.Dispose();
		}

		void StartMpcInSlaveMode(IContext context)
		{
			_unsubscriber = Bus.Subscribe<ConnectionEstablished>(m => SetUpConnection(context, m));

			context.Log("Exchanging messages through window handle {0:X}", context.MessageExchange.ToInt64());

			try
			{
				var psi = new ProcessStartInfo(Settings.Default.MpcPath)
				          {
				          	Arguments = String.Format(CultureInfo.InvariantCulture, "/slave {0}", context.MessageExchange),
				          	ErrorDialog = true
				          };

				Process.Start(psi);
			}
			catch (Exception ex)
			{
				context.Log("Could not start Media Player Classic: {0}", ex.Message);
			}
		}
	}
}