namespace MpcDeleter.Commands
{
	public class FindMessageExchangeCommand : ICommand
	{
		public void Execute(IContext context)
		{
			context.Log("Exchanging messages through window handle {0:X}", context.MessageExchange.ToInt64());
		}
	}
}