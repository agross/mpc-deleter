using System;
using System.IO;
using System.Net.Sockets;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

using Minimod.RxMessageBroker;

using MpcDeleter.Messages;

namespace MpcDeleter.Lirc
{
  class LircClient : IObservable<string>
  {
    readonly IObservable<string> _subscription;

    public LircClient(string server, int port)
    {
      _subscription = ConnectAndWaitForData(server, port);
    }

    public IDisposable Subscribe(IObserver<string> observer)
    {
      return _subscription.Subscribe(observer);
    }

    static IObservable<string> ConnectAndWaitForData(string server, int port)
    {
      return Observable
        .Using(() => new TcpClient(),
               client =>
               {
                 var connectToServer = Observable.FromAsyncPattern<string, int>(client.BeginConnect, client.EndConnect);

                 return connectToServer(server, port)
                   .Catch<Unit, Exception>(ex =>
                   {
                     RxMessageBrokerMinimod
                       .Default
                       .Send(new Log("Error while communicating with LIRC: {0}", ex.Message));

                     return Observable.Empty<Unit>();
                   })
                   .Do(_ => RxMessageBrokerMinimod
                              .Default
                              .Send(new Log("Connected to LIRC server at {0}:{1}", server, port)))
                   .SelectMany(Observable.Using(client.GetStream, stream => ReadMessage(client, stream)));
               })
        .Publish()
        .RefCount(); // Subscribe once for all subscribers.
    }

    static IObservable<string> ReadMessage(TcpClient client, Stream stream)
    {
      var buffer = new byte[256];
      var reader = Observable.FromAsyncPattern<byte[], int, int, int>(stream.BeginRead, stream.EndRead);

      return Observable
        .While(() => client.Connected && stream.CanRead,
               Observable.Defer(() => reader(buffer, 0, buffer.Length))
                         .Where(bytesRead => bytesRead > 0)
                         .Select(bytesRead => Encoding.ASCII.GetString(buffer, 0, bytesRead))
                         .Do(x => RxMessageBrokerMinimod
                                    .Default
                                    .Send(new Log("Received '{0}' from LIRC", x))));
    }
  }
}
