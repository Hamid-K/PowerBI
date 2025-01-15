using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C16 RID: 7190
	public static class ProcessInfo
	{
		// Token: 0x0600B376 RID: 45942 RVA: 0x00247C09 File Offset: 0x00245E09
		public static int GetProcessId(SafeHandle processHandle)
		{
			return ProcessInfo.Interop.GetProcessId(processHandle);
		}

		// Token: 0x0600B377 RID: 45943 RVA: 0x00247C14 File Offset: 0x00245E14
		public static TimeSpan? GetTotalProcessorTime(SafeHandle processHandle)
		{
			global::System.Runtime.InteropServices.ComTypes.FILETIME filetime;
			global::System.Runtime.InteropServices.ComTypes.FILETIME filetime2;
			global::System.Runtime.InteropServices.ComTypes.FILETIME filetime3;
			global::System.Runtime.InteropServices.ComTypes.FILETIME filetime4;
			if (processHandle.IsClosed || processHandle.IsInvalid || !ProcessInfo.Interop.GetProcessTimes(processHandle, out filetime, out filetime2, out filetime3, out filetime4))
			{
				return null;
			}
			return new TimeSpan?(ProcessInfo.FileTimeToTimeSpan(filetime3) + ProcessInfo.FileTimeToTimeSpan(filetime4));
		}

		// Token: 0x0600B378 RID: 45944 RVA: 0x00247C64 File Offset: 0x00245E64
		public static string GetProcessName(SafeHandle processHandle)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			if (ProcessInfo.Interop.GetModuleFileNameEx(processHandle, IntPtr.Zero, stringBuilder, stringBuilder.Capacity) > 0U)
			{
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x0600B379 RID: 45945 RVA: 0x00247C9C File Offset: 0x00245E9C
		public static long? GetCommit(SafeHandle processHandle = null)
		{
			processHandle = processHandle ?? ProcessHelpers.ProcessHandle;
			ProcessInfo.Interop.PROCESS_MEMORY_COUNTERS process_MEMORY_COUNTERS;
			if (ProcessInfo.Interop.GetProcessMemoryInfo(processHandle, out process_MEMORY_COUNTERS, Marshal.SizeOf(typeof(ProcessInfo.Interop.PROCESS_MEMORY_COUNTERS))))
			{
				return new long?((long)process_MEMORY_COUNTERS.PagefileUsage);
			}
			return null;
		}

		// Token: 0x0600B37A RID: 45946 RVA: 0x00247CE8 File Offset: 0x00245EE8
		private static TimeSpan FileTimeToTimeSpan(global::System.Runtime.InteropServices.ComTypes.FILETIME fileTime)
		{
			return TimeSpan.FromTicks((long)(((ulong)fileTime.dwHighDateTime << 32) | (ulong)fileTime.dwLowDateTime));
		}

		// Token: 0x02001C17 RID: 7191
		private static class Interop
		{
			// Token: 0x0600B37B RID: 45947
			[DllImport("kernel32.dll")]
			public static extern int GetProcessId(SafeHandle hProcess);

			// Token: 0x0600B37C RID: 45948
			[DllImport("kernel32.dll")]
			public static extern bool GetProcessTimes(SafeHandle hProcess, out global::System.Runtime.InteropServices.ComTypes.FILETIME lpCreationTime, out global::System.Runtime.InteropServices.ComTypes.FILETIME lpExitTime, out global::System.Runtime.InteropServices.ComTypes.FILETIME lpKernelTime, out global::System.Runtime.InteropServices.ComTypes.FILETIME lpUserTime);

			// Token: 0x0600B37D RID: 45949
			[DllImport("psapi.dll", CharSet = CharSet.Unicode)]
			public static extern uint GetModuleFileNameEx(SafeHandle hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, int nSize);

			// Token: 0x0600B37E RID: 45950
			[DllImport("psapi.dll")]
			public static extern bool GetProcessMemoryInfo(SafeHandle hProcess, out ProcessInfo.Interop.PROCESS_MEMORY_COUNTERS counters, int size);

			// Token: 0x02001C18 RID: 7192
			public struct PROCESS_MEMORY_COUNTERS
			{
				// Token: 0x04005B85 RID: 23429
				public uint cb;

				// Token: 0x04005B86 RID: 23430
				public uint PageFaultCount;

				// Token: 0x04005B87 RID: 23431
				public IntPtr PeakWorkingSetSize;

				// Token: 0x04005B88 RID: 23432
				public IntPtr WorkingSetSize;

				// Token: 0x04005B89 RID: 23433
				public IntPtr QuotaPeakPagedPoolUsage;

				// Token: 0x04005B8A RID: 23434
				public IntPtr QuotaPagedPoolUsage;

				// Token: 0x04005B8B RID: 23435
				public IntPtr QuotaPeakNonPagedPoolUsage;

				// Token: 0x04005B8C RID: 23436
				public IntPtr QuotaNonPagedPoolUsage;

				// Token: 0x04005B8D RID: 23437
				public IntPtr PagefileUsage;

				// Token: 0x04005B8E RID: 23438
				public IntPtr PeakPagefileUsage;
			}
		}
	}
}
