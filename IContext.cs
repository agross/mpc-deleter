using MpcDeleter.Commands;

namespace MpcDeleter
{
	public interface IContext
	{
	  PlayerContext Player
		{
			get;
		}

		void Execute(ICommand command);

	  void Log(string message, params object[] args);
	}
}