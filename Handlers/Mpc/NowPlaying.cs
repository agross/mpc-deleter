using System;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Minimod.RxMessageBroker;

using MpcDeleter.Messages;

namespace MpcDeleter.Handlers.Mpc
{
  class NowPlaying : IMessageHandler
  {
    /// <summary>
    /// 	A description of the regular expression:
    ///  
    /// 	Match if prefix is absent. [\\]
    /// 	Literal \
    /// 	Literal |
    /// </summary>
    static readonly Regex Splitter = new Regex("(?<!\\\\)\\|",
                                               RegexOptions.CultureInvariant
                                               | RegexOptions.Compiled);

    public IDisposable SetUp(IObservable<Message> source)
    {
      return source
        .Where(Matches)
        .Select(ToCurrentFile)
        .DistinctUntilChanged()
        .Subscribe(Publish);
    }

    static bool Matches(Message message)
    {
      return message.Matches(NativeConstants.CMD_NOWPLAYING);
    }

    static CurrentFile ToCurrentFile(Message message)
    {
      var data = message.GetCopiedData();

      var nowPlaying = Marshal.PtrToStringUni(data.lpData);
      var fileAndLength = Splitter.Split(nowPlaying)
                                  .Select(x => x.Replace("\\|", "\\"));

      var fileName = fileAndLength.Skip(3).First();

      var length = fileAndLength.Skip(4).First();
      var roundedLength = Convert.ToInt32(decimal.Parse(length, CultureInfo.InvariantCulture));

      return new CurrentFile(fileName, roundedLength);
    }

    static void Publish(CurrentFile message)
    {
      RxMessageBrokerMinimod.Default.Send(message);
    }
  }
}
