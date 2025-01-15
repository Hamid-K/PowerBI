using System;
using System.Diagnostics;

namespace Microsoft.Mashup.Tracing
{
	// Token: 0x020020AF RID: 8367
	internal static class CurrentProcess
	{
		// Token: 0x0600CCD1 RID: 52433 RVA: 0x0028B513 File Offset: 0x00289713
		static CurrentProcess()
		{
			Process currentProcess = Process.GetCurrentProcess();
			CurrentProcess.processName = currentProcess.ProcessName;
			CurrentProcess.processID = currentProcess.Id;
		}

		// Token: 0x0600CCD2 RID: 52434 RVA: 0x0028B52F File Offset: 0x0028972F
		public static int GetProcessID()
		{
			return CurrentProcess.processID;
		}

		// Token: 0x0600CCD3 RID: 52435 RVA: 0x0028B536 File Offset: 0x00289736
		public static string GetProcessName()
		{
			return CurrentProcess.processName;
		}

		// Token: 0x040067B1 RID: 26545
		private static readonly int processID;

		// Token: 0x040067B2 RID: 26546
		private static readonly string processName;
	}
}
