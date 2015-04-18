using System;
using System.Globalization;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Minimod.RxMessageBroker;

using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Mpc
{
  class CurrentPosition : IMessageHandler
  {
    public IDisposable SetUp(IObservable<Message> source)
    {
      return source.Where(CanHandle).Subscribe(Handle);
    }

    static bool CanHandle(Message message)
    {
      return message.Matches(NativeConstants.CMD_CURRENTPOSITION);
    }

    static void Handle(Message message)
    {
      var data = message.GetCopiedData();

      var pos = Marshal.PtrToStringUni(data.lpData);
      
      double position;
      if (!double.TryParse(pos, NumberStyles.Any, CultureInfo.InvariantCulture, out position))
      {
        RxMessageBrokerMinimod.Default.Send(new Log("Could not determine current position. Received: {0}", pos));
        return;
      }
      
      var roundedPosition = Convert.ToInt32(Math.Round(position));

      RxMessageBrokerMinimod.Default.Send(new Messages.CurrentPosition(roundedPosition));
    }
  }
}
