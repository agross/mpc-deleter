namespace MpcDeleter.Commands
{
  class SendMessage
  {
    public uint Message { get; private set; }
    public string Data { get; private set; }

    public SendMessage(uint message, string data = null)
    {
      Message = message;
      Data = data;
    }

    public string Info()
    {
      return string.Format("0x{0:X} ({1}) with data {2}",
                           Message,
                           NativeConstants.Lookup(Message),
                           Data ?? "(null)");
    }

    public override string ToString()
    {
      return string.Format("Sending message {0}", Info());
    }

    protected bool Equals(SendMessage other)
    {
      return Message == other.Message && string.Equals(Data, other.Data);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj))
      {
        return false;
      }
      if (ReferenceEquals(this, obj))
      {
        return true;
      }
      if (obj.GetType() != GetType())
      {
        return false;
      }
      return Equals((SendMessage) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((int) Message * 397) ^ (Data != null ? Data.GetHashCode() : 0);
      }
    }
  }
}
