using System;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Minimod.RxMessageBroker;

using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Mpc
{
  class Connect : IMessageHandler
  {
    public IDisposable SetUp(IObservable<Message> source)
    {
      return source.Where(Matches).Subscribe(Handle);
    }

    static bool Matches(Message message)
    {
      return message.Matches(NativeConstants.CMD_CONNECT);
    }

    static void Handle(Message message)
    {
      var data = message.GetCopiedData();

      var handleAsString = Marshal.PtrToStringUni(data.lpData);
      var handle = new IntPtr(long.Parse(handleAsString));

      RxMessageBrokerMinimod.Default.Send(new ConnectionEstablished(handle));
    }
  }
}
