using System;
using System.Diagnostics;
using System.Globalization;

using MpcDeleter.Properties;

namespace MpcDeleter.Commands
{
	public class StartMpcCommand : ICommand
	{
		public void Execute(IContext context)
		{
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