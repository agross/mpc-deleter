namespace MpcDeleter.Commands
{
  class DeleteFile
  {
    public string FileName { get; private set; }
    public int NumberOfTriesLeft { get; private set; }
    public bool WhatIf { get; private set; }

    public DeleteFile(string fileName, bool whatIf, int numberOfTriesLeft = 30)
    {
      FileName = fileName;
      NumberOfTriesLeft = numberOfTriesLeft;
      WhatIf = whatIf;
    }

    public override string ToString()
    {
      return string.Format("Attempting to delete {0} with {1} tries (simulate: {2})", FileName, NumberOfTriesLeft, WhatIf);
    }
  }
}
