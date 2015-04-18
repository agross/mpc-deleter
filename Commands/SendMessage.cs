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

    public override string ToString()
    {
      return string.Format("Sending message {0} with data {1}", Message, Data ?? "(null)");
    }
  }
}
