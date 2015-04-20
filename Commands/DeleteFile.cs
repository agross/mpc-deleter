namespace MpcDeleter.Commands
{
  class DeleteFile
  {
    public string FileName { get; private set; }
    public bool WhatIf { get; private set; }

    public DeleteFile(string fileName, bool whatIf)
    {
      FileName = fileName;
      WhatIf = whatIf;
    }

    public override string ToString()
    {
      return string.Format("Attempting to delete {0} (simulate: {1})", FileName, WhatIf);
    }
  }
}
