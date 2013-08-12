using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MpcDeleter.Commands
{
  class RetryDeleteFile : ICommand
  {
    readonly int _retriesLeft;
    readonly string _file;

    public RetryDeleteFile(int retriesLeft, string file)
    {
      _retriesLeft = retriesLeft;
      _file = file;
    }

    public void Execute(IContext context)
    {
      Task.Factory.StartNew(() =>
      {
        if (_retriesLeft <= 0)
        {
          context.Log("All retries used up deleting {0}", _file);
          return;
        }

        try
        {
          Thread.Sleep(TimeSpan.FromSeconds(3));
          File.Delete(_file);
          context.Log("Deleted file {0}", _file);
        }
        catch (Exception ex)
        {
          context.Log("Failed to delete file, going to retry {0}, {1}", _file, ex.Message);
          context.Execute(new RetryDeleteFile(_retriesLeft - 1, _file));
        }
      }).Start();
    }
  }
}