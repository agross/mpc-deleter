using System;
using System.Globalization;
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
			double position;
			if (double.TryParse(pos, NumberStyles.Any, CultureInfo.InvariantCulture, out position))
			{
				var roundedPosition = Convert.ToInt32(Math.Round(position));
				context.Log("Current position is at {0} seconds", roundedPosition);

				Bus.Publish(new CurrentPositionChanged(roundedPosition));
			}
			else
			{
				context.Log("Could not determine current position. Received: {0}", pos);
			}
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