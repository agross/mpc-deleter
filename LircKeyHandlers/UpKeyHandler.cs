using MpcDeleter.Commands;

namespace MpcDeleter.LircKeyHandlers
{
	class UpKeyHandler : ILircKeyHandler
	{
		public bool CanHandle(string message)
		{
			return message.Contains(" up ");
		}

		public void Handle(string message, IContext context)
		{
			context.Log("Fast-forwarding 10%");
			context.Execute(new FastForwardCommand(.1));
		}
	}
}