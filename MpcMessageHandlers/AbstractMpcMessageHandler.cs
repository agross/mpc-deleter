using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MpcDelete.MpcMessageHandlers
{
	public abstract class AbstractMpcMessageHandler : IMessageHandler
	{
		public abstract bool CanHandle(Message message);
		public abstract void Handle(Message message, IContext context);

		protected static COPYDATASTRUCT GetCopiedData(Message message)
		{
			return (COPYDATASTRUCT) Marshal.PtrToStructure(message.LParam, typeof(COPYDATASTRUCT));
		}
	}
}