namespace MpcDeleter.Commands
{
	class GetCurrentPostionCommand : ICommand
	{
		public void Execute(IContext context)
		{
			context.Execute(new SendMessageCommand(NativeConstants.CMD_GETCURRENTPOSITION, null));
		}
	}
}