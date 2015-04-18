using System;
using System.Windows.Forms;

namespace MpcDeleter.Handlers.Mpc
{
  public interface IMessageHandler
  {
    IDisposable SetUp(IObservable<Message> source);
  }
}
