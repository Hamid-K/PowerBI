using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.Utils
{
	// Token: 0x02000142 RID: 322
	public static class MemoryUtilizationUtil
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x00046A75 File Offset: 0x00044C75
		static MemoryUtilizationUtil()
		{
			MemoryUtilizationUtil.TryGetStatusAndUpdateTotalPhysicalMemoryInMB();
			MemoryUtilizationUtil.rollingAverageMemoryUsageInMB = 0.0;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00046A98 File Offset: 0x00044C98
		public static double GetMemoryUtilization(out double currentProcessMemoryUtilization, double degreeOfweightDecrease)
		{
			Process currentProcess = Process.GetCurrentProcess();
			currentProcessMemoryUtilization = (double)currentProcess.WorkingSet64 / 1048576.0;
			object @lock = MemoryUtilizationUtil._lock;
			double num;
			lock (@lock)
			{
				MemoryUtilizationUtil.rollingAverageMemoryUsageInMB = currentProcessMemoryUtilization + degreeOfweightDecrease * (MemoryUtilizationUtil.rollingAverageMemoryUsageInMB - currentProcessMemoryUtilization);
				num = MemoryUtilizationUtil.rollingAverageMemoryUsageInMB / MemoryUtilizationUtil.totalMemoryInMB;
			}
			return num;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00046B0C File Offset: 0x00044D0C
		public static bool TryGetStatusAndUpdateTotalPhysicalMemoryInMB()
		{
			object @lock = MemoryUtilizationUtil._lock;
			bool flag2;
			lock (@lock)
			{
				MemoryUtilizationUtil.totalMemoryInMB = -1.0;
				MEMORYSTATUSEX memorystatusex = new MEMORYSTATUSEX();
				if (MemoryUtilizationUtil.GlobalMemoryStatusEx(memorystatusex))
				{
					MemoryUtilizationUtil.totalMemoryInMB = memorystatusex.ullTotalPhys / 1048576.0;
					flag2 = true;
				}
				else
				{
					TraceSourceBase<ANCommonTrace>.Tracer.TraceWarning("Getting total physical memory in MB failed");
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0600115F RID: 4447
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GlobalMemoryStatusEx([In] [Out] MEMORYSTATUSEX lpBuffer);

		// Token: 0x040003DF RID: 991
		public static double totalMemoryInMB;

		// Token: 0x040003E0 RID: 992
		public static double rollingAverageMemoryUsageInMB;

		// Token: 0x040003E1 RID: 993
		private static readonly object _lock = new object();
	}
}
