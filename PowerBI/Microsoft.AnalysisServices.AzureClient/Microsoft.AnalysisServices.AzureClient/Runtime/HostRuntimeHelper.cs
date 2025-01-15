using System;
using System.Diagnostics;

namespace Microsoft.AnalysisServices.AzureClient.Runtime
{
	// Token: 0x0200003E RID: 62
	internal static class HostRuntimeHelper
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00009604 File Offset: 0x00007804
		public static bool TryGetHostingProcessInfo(out int id, out string processName, out string version, out bool is64Bit)
		{
			object obj = HostRuntimeHelper.hostInfoLock;
			lock (obj)
			{
				if (HostRuntimeHelper.hostInfo != null)
				{
					id = HostRuntimeHelper.hostInfo.Value.Id;
					processName = HostRuntimeHelper.hostInfo.Value.Name;
					version = HostRuntimeHelper.hostInfo.Value.Version;
					is64Bit = HostRuntimeHelper.hostInfo.Value.Is64Bit;
					return true;
				}
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					try
					{
						id = currentProcess.Id;
					}
					catch (Exception)
					{
						id = -1;
					}
					try
					{
						processName = currentProcess.ProcessName;
					}
					catch (Exception)
					{
						processName = null;
					}
					try
					{
						version = currentProcess.MainModule.FileVersionInfo.FileVersion ?? string.Empty;
					}
					catch (Exception)
					{
						version = null;
					}
				}
				is64Bit = IntPtr.Size == 8;
				if (id != -1 && !string.IsNullOrEmpty(processName) && version != null)
				{
					HostRuntimeHelper.hostInfo = new HostRuntimeHelper.HostProcessInfo?(new HostRuntimeHelper.HostProcessInfo
					{
						Id = id,
						Name = processName,
						Version = version,
						Is64Bit = is64Bit
					});
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000FD RID: 253
		private static HostRuntimeHelper.HostProcessInfo? hostInfo;

		// Token: 0x040000FE RID: 254
		private static object hostInfoLock = new object();

		// Token: 0x02000079 RID: 121
		private struct HostProcessInfo
		{
			// Token: 0x04000236 RID: 566
			public int Id;

			// Token: 0x04000237 RID: 567
			public string Name;

			// Token: 0x04000238 RID: 568
			public string Version;

			// Token: 0x04000239 RID: 569
			public bool Is64Bit;
		}
	}
}
