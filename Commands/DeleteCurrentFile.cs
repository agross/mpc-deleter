namespace MpcDeleter.Commands
{
  class DeleteCurrentFile
  {
    public bool WhatIf { get; private set; }

    public DeleteCurrentFile(bool whatIf)
    {
      WhatIf = whatIf;
    }

    public override string ToString()
    {
      return string.Format("Attempting to delete the current file (simulate: {0})", WhatIf);
    }
  }
}
