using System;
using System.Net.Sockets;
using System.Text;

namespace MpcDeleter
{
	public class LircClient : IDisposable
	{
		TcpClient _client;
		NetworkStream _stream;

		public void Dispose()
		{
			if (_stream != null)
			{
				_stream.Close();
			}

			if (_client != null && _client.Connected)
			{
				_client.Close();
			}
		}

		public event EventHandler<MessageEventArgs> KeyPressed;

		public void Connect(IContext context, string server, int port)
		{
			try
			{
				_client = new TcpClient();
				_client.Connect(server, port);

				context.Log("Connected to LIRC server at {0}:{1}", server, port);

				_stream = _client.GetStream();

				var buffer = new byte[256];
				AsyncCallback callback = Callback(context, buffer);
				BeginRead(buffer, callback);
			}
			catch (Exception ex)
			{
				context.Log("Error while communicating with LIRC: {0}", ex.Message);
			}
		}

		void BeginRead(byte[] buffer, AsyncCallback callback)
		{
			_stream.BeginRead(buffer,
			                  0,
			                  buffer.Length,
			                  callback,
			                  null);
		}

		AsyncCallback Callback(IContext context, byte[] buffer)
		{
			return asyncResult =>
				{
					if (!_stream.CanRead)
					{
						return;
					}

					var bytesRead = _stream.EndRead(asyncResult);
					var message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
					context.Log("Received '{0}' from LIRC", message);

					var @event = KeyPressed;
					if (@event != null)
					{
						@event(this, new MessageEventArgs(message));
					}

					BeginRead(buffer, Callback(context, buffer));
				};
		}
	}
}