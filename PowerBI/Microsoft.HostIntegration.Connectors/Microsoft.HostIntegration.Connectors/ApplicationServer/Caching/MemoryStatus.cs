using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200033E RID: 830
	internal class MemoryStatus
	{
		// Token: 0x06001DC2 RID: 7618 RVA: 0x00059B80 File Offset: 0x00057D80
		public static void GetStatus(out ulong physicalMemory, out ulong availableMemory)
		{
			MEMORYSTATUSEX memorystatusex = default(MEMORYSTATUSEX);
			memorystatusex.Init();
			NativeMethods.GlobalMemoryStatusEx(ref memorystatusex);
			physicalMemory = memorystatusex.ullTotalPhys;
			availableMemory = memorystatusex.ullAvailPhys;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00059BB6 File Offset: 0x00057DB6
		public static void GetFullMemoryStatus(ref MEMORYSTATUSEX ms)
		{
			ms.Init();
			NativeMethods.GlobalMemoryStatusEx(ref ms);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00059BC8 File Offset: 0x00057DC8
		private static bool TryGetProcessMemoryStatus(out PROCESS_MEMORY_COUNTERS counters)
		{
			IntPtr currentProcess = NativeMethods.GetCurrentProcess();
			bool processMemoryInfo;
			using (new SafeFileHandle(currentProcess, true))
			{
				processMemoryInfo = NativeMethods.GetProcessMemoryInfo(currentProcess, out counters, Marshal.SizeOf(typeof(PROCESS_MEMORY_COUNTERS)));
			}
			return processMemoryInfo;
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00059C18 File Offset: 0x00057E18
		public static bool TryGetProcessWorkingSet(out ulong workingSet)
		{
			PROCESS_MEMORY_COUNTERS process_MEMORY_COUNTERS;
			if (MemoryStatus.TryGetProcessMemoryStatus(out process_MEMORY_COUNTERS))
			{
				workingSet = process_MEMORY_COUNTERS.WorkingSetSize.ToUInt64();
				return true;
			}
			workingSet = 0UL;
			return false;
		}
	}
}
