namespace MpcDelete.Commands
{
	public interface ICommand
	{
		void Execute(IContext context);
	}
}