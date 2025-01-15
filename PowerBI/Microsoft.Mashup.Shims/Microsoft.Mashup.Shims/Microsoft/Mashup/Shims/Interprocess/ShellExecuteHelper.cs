using System;
using System.Diagnostics;
using System.IO;

namespace Microsoft.Mashup.Shims.Interprocess
{
	// Token: 0x02000017 RID: 23
	public static class ShellExecuteHelper
	{
		// Token: 0x06000034 RID: 52 RVA: 0x000024CE File Offset: 0x000006CE
		public static void Execute(string name, string arguments = null)
		{
			ShellExecuteHelper.ExecuteCore(name, arguments);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000024D7 File Offset: 0x000006D7
		public static void OpenFolder(string path)
		{
			ShellExecuteHelper.OpenFolderCore(path);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000024DF File Offset: 0x000006DF
		private static void ExecuteCore(string name, string arguments = null)
		{
			if (arguments != null)
			{
				Process.Start(name, arguments);
				return;
			}
			Process.Start(name);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000024F4 File Offset: 0x000006F4
		private static void OpenFolderCore(string path)
		{
			string environmentVariable = Environment.GetEnvironmentVariable("SystemRoot");
			if (!string.IsNullOrEmpty(environmentVariable))
			{
				Process.Start(Path.Combine(environmentVariable, "explorer.exe"), path);
			}
		}
	}
}
