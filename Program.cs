using System;
using System.Windows.Forms;

namespace MpcDeleter
{
	internal static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(true);
			Application.Run(new MpcDeleterApplicationContext());
		}
	}
}