using System;
using System.ComponentModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.InteropServices;

using Minimod.RxMessageBroker;

using MpcDeleter.Commands;
using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Commands
{
  class SendMessageHandler : ICommandHandler
  {
    class Params
    {
      public SendMessage Send { get; set; }
      public ReceiverCreated Receiver { get; set; }
      public ConnectionEstablished Connection { get; set; }

      public Params(SendMessage send, ReceiverCreated receiver, ConnectionEstablished connection)
      {
        Send = send;
        Receiver = receiver;
        Connection = connection;
      }
    }

    public IDisposable SetUp(IScheduler scheduler)
    {
      var stream = RxMessageBrokerMinimod.Default.Stream;

      var sendMessage = stream.OfType<SendMessage>();
      var receiver = stream.OfType<ReceiverCreated>();
      var connection = stream.OfType<ConnectionEstablished>();

      return sendMessage
        .CombineLatest(receiver, connection, (s, r, c) => new Params(s, r, c))
        .ObserveOn(scheduler)
        .Subscribe(Send);
    }

    static void Send(Params @params)
    {
      var copyDataMem = IntPtr.Zero;
      var dataPointer = IntPtr.Zero;
      try
      {
        var dataSize = 0;
        dataPointer = IntPtr.Zero;
        if (@params.Send.Data != null)
        {
          // Same algorithm to determine size as in Marshal.StringToHGlobalUni.
          dataSize = (@params.Send.Data.Length + 1) * 2;
          dataPointer = Marshal.StringToHGlobalUni(@params.Send.Data);
        }

        var copyData = new COPYDATASTRUCT
        {
          dwData = new UIntPtr(@params.Send.Message),
          cbData = dataSize,
          lpData = dataPointer
        };

        copyDataMem = Marshal.AllocHGlobal(Marshal.SizeOf(copyData));
        Marshal.StructureToPtr(copyData, copyDataMem, true);
        var result = NativeMethods.SendMessage(@params.Connection.Handle,
                                               NativeConstants.WM_COPYDATA,
                                               @params.Receiver.Handle,
                                               copyDataMem);
        if (result == IntPtr.Zero)
        {
          throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        RxMessageBrokerMinimod.Default.Send(new Log("Sent message to MPC"));
      }
      catch (Exception ex)
      {
        RxMessageBrokerMinimod.Default.Send(new Log("Failed to send message to MPC: {0}", ex.Message));
      }
      finally
      {
        if (copyDataMem != IntPtr.Zero)
        {
          Marshal.FreeHGlobal(copyDataMem);
        }
        if (dataPointer != IntPtr.Zero)
        {
          Marshal.FreeHGlobal(dataPointer);
        }
      }
    }
  }
}
