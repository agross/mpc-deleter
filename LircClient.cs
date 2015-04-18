using System;
using System.IO;
using System.Net.Sockets;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;

namespace MpcDeleter
{
  public class LircClient : IObservable<string>
  {
    readonly IObservable<string> _subscription;

    public LircClient(IContext context, string server, int port)
    {
      _subscription = ConnectAndWaitForData(server, port, context);
    }

    public IDisposable Subscribe(IObserver<string> observer)
    {
      return _subscription.Subscribe(observer);
    }

    static IObservable<string> ConnectAndWaitForData(string server, int port, IContext context)
    {
      return Observable
        .Using(() => new TcpClient(),
               client =>
               {
                 var connectToServer = Observable.FromAsyncPattern<string, int>(client.BeginConnect, client.EndConnect);

                 return connectToServer(server, port)
                   .Catch<Unit, Exception>(ex =>
                   {
                     context.Log("Error while communicating with LIRC: {0}", ex.Message);
                     return Observable.Empty<Unit>();
                   })
                   .Do(_ => context.Log("Connected to LIRC server at {0}:{1}", server, port))
                   .SelectMany(Observable.Using(client.GetStream, stream => ReadMessage(client, stream, context)));
               })
        .Publish()
        .RefCount(); // Subscribe once for all subscribers.
    }

    static IObservable<string> ReadMessage(TcpClient client, Stream stream, IContext context)
    {
      var buffer = new byte[256];
      var reader = Observable.FromAsyncPattern<byte[], int, int, int>(stream.BeginRead, stream.EndRead);

      return Observable
        .While(() => client.Connected && stream.CanRead,
               Observable.Defer(() => reader(buffer, 0, buffer.Length))
                         .Where(bytesRead => bytesRead > 0)
                         .Select(bytesRead => Encoding.ASCII.GetString(buffer, 0, bytesRead))
                         .Do(x => context.Log("Received '{0}' from LIRC", x)));
    }
  }
}
