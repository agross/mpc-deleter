namespace MpcDeleter.Messages
{
  class Log
  {
    public Log(string message, params object[] args)
    {
      Message = string.Format(message, args);
    }

    public string Message { get; private set; }
  }
}
