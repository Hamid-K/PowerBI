using System;
using System.Diagnostics;

namespace Microsoft.AnalysisServices.Runtime
{
	// Token: 0x02000151 RID: 337
	internal static class HostRuntimeHelper
	{
		// Token: 0x06001179 RID: 4473 RVA: 0x0003D7CC File Offset: 0x0003B9CC
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

		// Token: 0x04000B01 RID: 2817
		private static HostRuntimeHelper.HostProcessInfo? hostInfo;

		// Token: 0x04000B02 RID: 2818
		private static object hostInfoLock = new object();

		// Token: 0x020001E4 RID: 484
		private struct HostProcessInfo
		{
			// Token: 0x040011C4 RID: 4548
			public int Id;

			// Token: 0x040011C5 RID: 4549
			public string Name;

			// Token: 0x040011C6 RID: 4550
			public string Version;

			// Token: 0x040011C7 RID: 4551
			public bool Is64Bit;
		}
	}
}
