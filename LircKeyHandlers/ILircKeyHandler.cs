namespace MpcDelete.LircKeyHandlers
{
	public interface ILircKeyHandler
	{
		bool CanHandle(string message);
		void Handle(string message, IContext context);
	}
}