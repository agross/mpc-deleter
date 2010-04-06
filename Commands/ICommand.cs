namespace MpcDeleter.Commands
{
	public interface ICommand
	{
		void Execute(IContext context);
	}
}