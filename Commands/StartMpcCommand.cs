using System;
using System.Diagnostics;
using System.Globalization;

using MpcDelete.Properties;

namespace MpcDelete.Commands
{
	public class StartMpcCommand : ICommand
	{
		public void Execute(IContext context)
		{
			context.Execute(new FindMessageExchangeCommand());

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