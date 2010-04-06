using System.Windows.Forms;

namespace MpcDeleter.MpcMessageHandlers
{
	public interface IMessageHandler
	{
		bool CanHandle(Message message);
		void Handle(Message message, IContext context);
	}
}