using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MpcDeleter.Commands
{
  class RetryDeleteFile : ICommand
  {
    readonly string _file;
    readonly int _retriesLeft;

    public RetryDeleteFile(int retriesLeft, string file)
    {
      _retriesLeft = retriesLeft;
      _file = file;
    }

    public void Execute(IContext context)
    {
      Task.Factory.StartNew(() =>
      {
        try
        {
          Thread.Sleep(TimeSpan.FromSeconds(3));
          File.Delete(_file);
          context.Log("Deleted file {0}", _file);
        }
        catch (Exception ex)
        {
          var retriesLeft = _retriesLeft - 1;
          if (retriesLeft <= 0)
          {
            context.Log("All retries used up deleting {0}", _file);
            return;
          }

          context.Log("Failed to delete file, going to retry {0} times, {1}, {2}", retriesLeft, _file, ex.Message);
          context.Execute(new RetryDeleteFile(retriesLeft, _file));
        }
      });
    }
  }
}
