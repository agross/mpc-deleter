using System;

using Minimod.RxMessageBroker;

using MpcDeleter.Messages;

namespace MpcDeleter.Handlers
{
  class LogMessagesAndCommands
  {
    public IDisposable SetUp()
    {
      var bus = RxMessageBrokerMinimod.Default;

      return bus.Register(x => bus.Send(new Log(x.ToString())), IsMessageOrCommandButNotLog());
    }

    static Func<object, bool> IsMessageOrCommandButNotLog()
    {
      return o =>
      {
        var ns = (o.GetType().Namespace ?? "");
        return o.GetType() != typeof(Log) &&
               (ns.Contains(".Messages") || ns.Contains(".Commands"));
      };
    }
  }
}
