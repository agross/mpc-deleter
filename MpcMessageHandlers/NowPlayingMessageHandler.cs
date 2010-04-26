using System;
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
			if (message.Msg != NativeConstants.WM_COPYDATA)
			{
				return false;
			}

			var cds = GetCopiedData(message);
			return cds.dwData == new UIntPtr(NativeConstants.CMD_NOWPLAYING);
		}

		public override void Handle(Message message, IContext context)
		{
			var cds = GetCopiedData(message);

			var nowPlaying = Marshal.PtrToStringUni(cds.lpData);
			var data = Splitter.Split(nowPlaying)
				.Select(x => x.Replace("\\|", "\\"));

			var fileName = data.Skip(3).First();
			var length = data.Skip(4).First();

			context.Player.UpdateCurrentFile(fileName, int.Parse(length));

			context.Log(String.Format("Now playing: {0}, length {1} seconds",
			                          context.Player.CurrentFile,
			                          context.Player.CurrentFileLength));
		}
	}
}