namespace MpcDeleter.Commands
{
  class ArchiveCurrentFile
  {
    public bool WhatIf { get; private set; }

    public ArchiveCurrentFile(bool whatIf)
    {
      WhatIf = whatIf;
    }

    public override string ToString()
    {
      return string.Format("Attempting to archive the current file (simulate: {0})", WhatIf);
    }
  }
}
