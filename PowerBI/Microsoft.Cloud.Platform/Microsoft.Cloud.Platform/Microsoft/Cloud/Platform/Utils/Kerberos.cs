using System;
using System.Diagnostics;
using System.IO;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200023D RID: 573
	public static class Kerberos
	{
		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003331C File Offset: 0x0003151C
		public static void PurgeTickets()
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "klist.exe");
			if (!File.Exists(text))
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "klist.exe was not found in {0} ", new object[] { Path.GetDirectoryName(text) });
				return;
			}
			TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Info, "Purging kerberos tickets from current session, user domain name: {0}", new object[] { Environment.UserDomainName });
			using (Process process = new Process())
			{
				process.StartInfo.FileName = text;
				process.StartInfo.Arguments = "purge";
				process.StartInfo.Verb = "Open";
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.UseShellExecute = false;
				process.Start();
				process.WaitForExit(30000);
			}
		}
	}
}
