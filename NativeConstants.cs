using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace MpcDeleter
{
  static class NativeConstants
  {
    public const uint CMD_CONNECT = 0x50000000;

    // Not implemented -- great!
    // public const uint CMD_GETNOWPLAYING = 0xA0003002;
    public const uint CMD_NOWPLAYING = 0x50000003;
    public const uint CMD_PLAYPAUSE = 0xA0000003;
    public const uint CMD_SETPOSITION = 0xA0002000;
    public const uint WM_COPYDATA = 0x004A;
    public const uint CMD_JUMPOFNSECONDS = 0xA0003005;

    public static string Lookup(uint message)
    {
      var found = typeof(NativeConstants)
        .GetFields()
        .Where(x => x.FieldType == message.GetType())
        .Select(x => new { Field = x, Value = x.GetRawConstantValue() })
        .FirstOrDefault(x => x.Value.Equals(message));

      if (found == null)
      {
        return "unknown";
      }

      return found.Field.Name;
    }
  }

  [StructLayout(LayoutKind.Sequential)]
  public struct COPYDATASTRUCT
  {
    public UIntPtr dwData;
    public int cbData;
    public IntPtr lpData;
  }
}
