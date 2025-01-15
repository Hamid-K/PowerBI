using System;
using System.Diagnostics;
using System.IO;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001F3 RID: 499
	public static class CurrentProcess
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0002DF78 File Offset: 0x0002C178
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x0002DF7F File Offset: 0x0002C17F
		public static string MainModuleFileName { get; private set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0002DF87 File Offset: 0x0002C187
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x0002DF8E File Offset: 0x0002C18E
		public static string MainModuleShortFileName { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0002DF96 File Offset: 0x0002C196
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x0002DF9D File Offset: 0x0002C19D
		public static string MainModuleDirectory { get; private set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x0002DFA5 File Offset: 0x0002C1A5
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x0002DFAC File Offset: 0x0002C1AC
		public static string Name { get; private set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0002DFB4 File Offset: 0x0002C1B4
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x0002DFBB File Offset: 0x0002C1BB
		public static int Id { get; private set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0002DFC3 File Offset: 0x0002C1C3
		// (set) Token: 0x06000D20 RID: 3360 RVA: 0x0002DFCA File Offset: 0x0002C1CA
		public static ProcessWellKnownHost WellKnownHost { get; private set; }

		// Token: 0x06000D21 RID: 3361 RVA: 0x0002DFD4 File Offset: 0x0002C1D4
		static CurrentProcess()
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				CurrentProcess.MainModuleFileName = currentProcess.MainModule.FileName;
				CurrentProcess.MainModuleShortFileName = Path.GetFileName(CurrentProcess.MainModuleFileName);
				CurrentProcess.MainModuleDirectory = Path.GetDirectoryName(CurrentProcess.MainModuleFileName);
				CurrentProcess.Name = currentProcess.ProcessName;
				CurrentProcess.Id = currentProcess.Id;
				CurrentProcess.WellKnownHost = CurrentProcess.GetCurrentProcessWellKnownHost();
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00009B3B File Offset: 0x00007D3B
		public static void Initialize()
		{
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0002E054 File Offset: 0x0002C254
		private static ProcessWellKnownHost GetCurrentProcessWellKnownHost()
		{
			string mainModuleShortFileName = CurrentProcess.MainModuleShortFileName;
			if (mainModuleShortFileName.Equals("testhost.exe", StringComparison.OrdinalIgnoreCase) || mainModuleShortFileName.Equals("testhost.x86.exe", StringComparison.OrdinalIgnoreCase) || mainModuleShortFileName.Equals("vstesthost.exe", StringComparison.OrdinalIgnoreCase) || mainModuleShortFileName.StartsWith("mstest", StringComparison.OrdinalIgnoreCase) || mainModuleShortFileName.StartsWith("QTAgent", StringComparison.OrdinalIgnoreCase) || mainModuleShortFileName.StartsWith("vstest.executionengine", StringComparison.OrdinalIgnoreCase) || mainModuleShortFileName.StartsWith("JetBrains.ReSharper.TaskRunner", StringComparison.OrdinalIgnoreCase) || mainModuleShortFileName.Equals("vstest.console.exe", StringComparison.OrdinalIgnoreCase))
			{
				return ProcessWellKnownHost.MSTest;
			}
			if (mainModuleShortFileName.Equals("WebDev.WebServer.exe", StringComparison.OrdinalIgnoreCase) || mainModuleShortFileName.Equals("WebDev.WebServer40.exe", StringComparison.OrdinalIgnoreCase))
			{
				return ProcessWellKnownHost.WebDev;
			}
			if (mainModuleShortFileName.Equals("WaWorkerHost.exe", StringComparison.OrdinalIgnoreCase))
			{
				return ProcessWellKnownHost.AzureWorker;
			}
			if (mainModuleShortFileName.Equals("WaIISHost.exe", StringComparison.OrdinalIgnoreCase))
			{
				return ProcessWellKnownHost.AzureWeb;
			}
			if (mainModuleShortFileName.StartsWith("uiafcmd", StringComparison.OrdinalIgnoreCase))
			{
				return ProcessWellKnownHost.UIAF;
			}
			return ProcessWellKnownHost.None;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0002E128 File Offset: 0x0002C328
		public static void Dump(TraceDump traceDump)
		{
			traceDump.Add("Current process:");
			traceDump.Add("  MainModuleFileName=" + CurrentProcess.MainModuleFileName);
			traceDump.Add("  MainModuleShortFileName=" + CurrentProcess.MainModuleShortFileName);
			traceDump.Add("  MainModuleDirectory=" + CurrentProcess.MainModuleDirectory);
			traceDump.Add("  Name=" + CurrentProcess.Name);
			traceDump.Add("  ID=" + CurrentProcess.Id);
			traceDump.Add("  WellKnownHost=" + CurrentProcess.GetCurrentProcessWellKnownHost());
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0002E1C8 File Offset: 0x0002C3C8
		public static bool IsWatchdogProcess
		{
			get
			{
				return CurrentProcess.MainModuleShortFileName.IndexOf("watchdog", StringComparison.OrdinalIgnoreCase) >= 0;
			}
		}
	}
}
