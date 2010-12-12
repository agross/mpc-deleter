using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

using MpcDeleter.Properties;

namespace MpcDeleter
{
	internal static class ApplicationSettings
	{
		internal static IEnumerable<KeyValuePair<Regex, string>> ArchivePathOverrides
		{
			get;
			private set;
		}

		public static string[] PlaylistFolders
		{
			get;
			private set;
		}

		internal static void Load()
		{
			UpgradeSettingsToNewApplicationVersion();
			LoadOverrides();
			LoadPlaylistFolders();
		}

		static void UpgradeSettingsToNewApplicationVersion()
		{
			// Only possible for per-user settings. We have none of these.
//			if (Settings.Default.ShouldUpgrade)
//			{
//				Settings.Default.Upgrade();
//				Settings.Default.ShouldUpgrade = false;
//				Settings.Default.Save();
//			}
		}

		static void LoadOverrides()
		{
			var deserialized = new JavaScriptSerializer().Deserialize<Override[]>(Settings.Default.ArchivePathOverrides);

			var overrides = new List<KeyValuePair<Regex, string>>();
			deserialized
				.Select(x => CreateOverride(x.Expression, x.Path))
				.Each(overrides.Add);

			ArchivePathOverrides = overrides;
		}

		static void LoadPlaylistFolders()
		{
			var deserialized = new JavaScriptSerializer().Deserialize<string[]>(Settings.Default.PlaylistFolders);
			PlaylistFolders = deserialized;
		}

		internal static KeyValuePair<Regex, string> CreateOverride(string expression, string path)
		{
			return new KeyValuePair<Regex, string>(new Regex(expression,
			                                                 RegexOptions.Compiled |
			                                                 RegexOptions.CultureInvariant |
			                                                 RegexOptions.Singleline |
			                                                 RegexOptions.IgnoreCase),
			                                       path);
		}

		class Override
		{
			public string Expression
			{
				get;
				set;
			}

			public string Path
			{
				get;
				set;
			}
		}
	}
}