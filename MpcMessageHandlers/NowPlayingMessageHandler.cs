using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MpcDeleter.MpcMessageHandlers
{
	internal class NowPlayingMessageHandler : AbstractMpcMessageHandler
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

		public override bool CanHandle(Message message)
		{
			return message.Matches(NativeConstants.CMD_NOWPLAYING);
		}

		public override void Handle(Message message, IContext context)
		{
			var cds = GetCopiedData(message);

			var nowPlaying = Marshal.PtrToStringUni(cds.lpData);
			var data = Splitter.Split(nowPlaying)
				.Select(x => x.Replace("\\|", "\\"));

			var fileName = data.Skip(3).First();
			
      var length = data.Skip(4).First();
      var roundedLength = Convert.ToInt32(decimal.Parse(length, CultureInfo.InvariantCulture));

		  context.Player.UpdateCurrentFile(fileName, roundedLength);

			context.Log(String.Format("Now playing: {0}, length {1} seconds",
			                          context.Player.CurrentFile,
			                          context.Player.CurrentFileLength));
		}
	}
}