using System;

using MpcDeleter.Commands;

namespace MpcDeleter
{
	public interface IContext
	{
		IntPtr MessageExchange
		{
			get;
		}

		IntPtr MediaPlayerClassic
		{
			get;
		}

		PlayerContext Player
		{
			get;
		}

		void Execute(ICommand command);

		void InitializeConnectionToMediaPlayerClassic(IntPtr window);
		void Log(string message, params object[] args);
		event EventHandler<MessageEventArgs> LogMessage;
	}
}