using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MpcDeleter.Archiving
{
  class RegexBasedArchivePathSelector : IArchivePathSelector
  {
    readonly IEnumerable<KeyValuePair<Regex, string>> _overrides;

    public RegexBasedArchivePathSelector(string defaultPath, IEnumerable<KeyValuePair<Regex, string>> overrides)
    {
      _overrides = overrides.Append(ApplicationSettings.CreateOverride(".*", defaultPath));
    }

    public string GetArchivePathFor(string file)
    {
      var match = _overrides
        .Where(x => x.Key.Match(file).Success)
        .First();

      return Path.Combine(match.Value, Path.GetFileName(file));
    }
  }
}
