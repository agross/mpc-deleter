using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MpcDeleter.MpcMessageHandlers
{
	class CurrentPositionHandler : AbstractMpcMessageHandler
	{
		public override bool CanHandle(Message message)
		{
			return message.Matches(NativeConstants.CMD_CURRENTPOSITION);
		}

		public override void Handle(Message message, IContext context)
		{
			var cds = GetCopiedData(message);

			var pos = Marshal.PtrToStringUni(cds.lpData);
			var position = int.Parse(pos);
			context.Log("Current position is at {0} seconds", position);

			Bus.Publish(new CurrentPositionChanged(position));
		}
	}

	class CurrentPositionChanged : IMessage
	{
		public CurrentPositionChanged(int position)
		{
			Position = position;
		}

		public int Position
		{
			get;
			private set;
		}
	}
}