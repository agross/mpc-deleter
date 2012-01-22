using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MpcDeleter.MpcMessageHandlers
{
	public abstract class AbstractMpcMessageHandler : IMessageHandler
	{
		public abstract bool CanHandle(Message message);
		public abstract void Handle(Message message, IContext context);

		internal static COPYDATASTRUCT GetCopiedData(Message message)
		{
			return (COPYDATASTRUCT) Marshal.PtrToStructure(message.LParam, typeof(COPYDATASTRUCT));
		}
	}
}