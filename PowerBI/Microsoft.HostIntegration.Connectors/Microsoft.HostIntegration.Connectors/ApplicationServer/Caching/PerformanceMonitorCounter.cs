using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000355 RID: 853
	internal static class PerformanceMonitorCounter
	{
		// Token: 0x06001E17 RID: 7703 RVA: 0x0005A01C File Offset: 0x0005821C
		internal static void Register(PerfCounter counter)
		{
			if (PerformanceMonitorCounter.IsPerfMonEnabled)
			{
				PerformanceMonitorCounter._counterTable.Add(counter, counter);
			}
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x0005A034 File Offset: 0x00058234
		internal static void Update()
		{
			if (PerformanceMonitorCounter.IsPerfMonEnabled && Monitor.TryEnter(PerformanceMonitorCounter.UpdateProgressLock))
			{
				try
				{
					foreach (object obj in PerformanceMonitorCounter._counterTable)
					{
						PerfCounter perfCounter = obj as PerfCounter;
						perfCounter.Update();
					}
				}
				finally
				{
					Monitor.Exit(PerformanceMonitorCounter.UpdateProgressLock);
				}
			}
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x0005A0BC File Offset: 0x000582BC
		internal static void UnRegister(PerfCounter counter)
		{
			if (PerformanceMonitorCounter.IsPerfMonEnabled)
			{
				PerformanceMonitorCounter._counterTable.Delete(counter);
			}
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x0005A0D4 File Offset: 0x000582D4
		internal static bool InitializeCounterSet()
		{
			bool flag = false;
			if (HostPerfCounter.InitializeCounterSet() && CachePerfCounter.InitializeCounterSet() && DistributedCachePerSecodaryMachineCounter.InitializeCounterSet())
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x0005A0FC File Offset: 0x000582FC
		internal static bool IsInstalled()
		{
			if (!HostPerfCounter.IsInstalled())
			{
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(PerfCounter.LogSource, "Host Category is not installed", new object[0]);
				}
				return false;
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(PerfCounter.LogSource, "Host Category is installed", new object[0]);
			}
			if (!CachePerfCounter.IsInstalled())
			{
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(PerfCounter.LogSource, "Cache Category is not installed", new object[0]);
				}
				return false;
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(PerfCounter.LogSource, "Cache Category is installed", new object[0]);
			}
			if (!DistributedCachePerSecodaryMachineCounter.IsInstalled())
			{
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(PerfCounter.LogSource, "Secodary Host Category is not installed", new object[0]);
				}
				return false;
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(PerfCounter.LogSource, "Secodary Host Category is installed", new object[0]);
			}
			return true;
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x0005A1D4 File Offset: 0x000583D4
		internal static bool CheckException(Exception e)
		{
			Type type = e.GetType();
			return type == typeof(InvalidOperationException) || type == typeof(ArgumentNullException) || type == typeof(Win32Exception) || type == typeof(PlatformNotSupportedException) || type == typeof(UnauthorizedAccessException);
		}

		// Token: 0x040010EE RID: 4334
		internal static bool IsPerfMonEnabled = true;

		// Token: 0x040010EF RID: 4335
		private static object UpdateProgressLock = new object();

		// Token: 0x040010F0 RID: 4336
		private static IBaseHashTable _counterTable = DataStructureFactory.CreateBaseHashTable(new ObjectDirectoryNodeFactory());
	}
}
