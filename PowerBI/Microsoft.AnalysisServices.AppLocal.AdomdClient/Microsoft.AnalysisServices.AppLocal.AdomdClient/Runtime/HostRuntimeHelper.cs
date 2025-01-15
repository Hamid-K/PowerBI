using System;
using System.Diagnostics;

namespace Microsoft.AnalysisServices.AdomdClient.Runtime
{
	// Token: 0x0200015B RID: 347
	internal static class HostRuntimeHelper
	{
		// Token: 0x060010EA RID: 4330 RVA: 0x0003AE3C File Offset: 0x0003903C
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

		// Token: 0x04000B48 RID: 2888
		private static HostRuntimeHelper.HostProcessInfo? hostInfo;

		// Token: 0x04000B49 RID: 2889
		private static object hostInfoLock = new object();

		// Token: 0x02000207 RID: 519
		private struct HostProcessInfo
		{
			// Token: 0x04000F0E RID: 3854
			public int Id;

			// Token: 0x04000F0F RID: 3855
			public string Name;

			// Token: 0x04000F10 RID: 3856
			public string Version;

			// Token: 0x04000F11 RID: 3857
			public bool Is64Bit;
		}
	}
}
