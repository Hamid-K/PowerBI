using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200034A RID: 842
	internal static class NativeMethods
	{
		// Token: 0x06001DE5 RID: 7653 RVA: 0x00059DF6 File Offset: 0x00057FF6
		static NativeMethods()
		{
			NativeMethods._osVersion.dwOSVersionInfoSize = Marshal.SizeOf(NativeMethods._osVersion);
			NativeMethods.GetVersionEx(ref NativeMethods._osVersion);
		}

		// Token: 0x06001DE6 RID: 7654
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		internal static extern int GetCurrentProcessorNumber();

		// Token: 0x06001DE7 RID: 7655
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GlobalMemoryStatusEx([MarshalAs(UnmanagedType.Struct)] ref MEMORYSTATUSEX ms);

		// Token: 0x06001DE8 RID: 7656
		[DllImport("psapi.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetProcessMemoryInfo(IntPtr hProcess, out PROCESS_MEMORY_COUNTERS counters, int size);

		// Token: 0x06001DE9 RID: 7657
		[DllImport("kernel32.dll")]
		internal static extern IntPtr GetCurrentProcess();

		// Token: 0x06001DEA RID: 7658
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr OpenSCManager(string lpMachineName, string lpDatabaseName, uint dwDesiredAccess);

		// Token: 0x06001DEB RID: 7659
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);

		// Token: 0x06001DEC RID: 7660
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseServiceHandle(IntPtr hSCObject);

		// Token: 0x06001DED RID: 7661
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ChangeServiceConfig2(IntPtr hService, int dwInfoLevel, IntPtr lpInfo);

		// Token: 0x06001DEE RID: 7662
		[DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int NetGetJoinInformation(string server, out IntPtr domain, out NetJoinStatus status);

		// Token: 0x06001DEF RID: 7663
		[DllImport("Netapi32.dll")]
		internal static extern int NetApiBufferFree(IntPtr Buffer);

		// Token: 0x06001DF0 RID: 7664
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetProcessWorkingSetSizeEx(IntPtr hProcess, IntPtr dwMinimumWorkingSetSize, IntPtr dwMaximumWorkingSetSize, QuotaLimitsHardWS dwFlags);

		// Token: 0x06001DF1 RID: 7665
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool LookupAccountSidW([MarshalAs(UnmanagedType.LPWStr)] string lpSystemName, [MarshalAs(UnmanagedType.LPArray)] byte[] Sid, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpName, ref uint cchName, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder ReferencedDomainName, ref uint cchReferencedDomainName, out SID_NAME_USE peUse);

		// Token: 0x06001DF2 RID: 7666
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool LookupAccountNameW([MarshalAs(UnmanagedType.LPWStr)] string lpSystemName, [MarshalAs(UnmanagedType.LPWStr)] string lpAccountName, [MarshalAs(UnmanagedType.LPArray)] byte[] Sid, ref uint cbSid, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder ReferencedDomainName, ref uint cchReferencedDomainName, out SID_NAME_USE peUse);

		// Token: 0x06001DF3 RID: 7667
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetVersionEx(ref OSVERSIONINFOEX osvi);

		// Token: 0x06001DF4 RID: 7668 RVA: 0x00059E27 File Offset: 0x00058027
		internal static bool IsHighAvailabilityEnabledEdition()
		{
			return (NativeMethods._osVersion.wSuiteMask & 2) != 0 || (NativeMethods._osVersion.wSuiteMask & 128) != 0;
		}

		// Token: 0x040010D8 RID: 4312
		internal const int SC_MANAGER_ALL_ACCESS = 983103;

		// Token: 0x040010D9 RID: 4313
		private const short VER_SUITE_DATACENTER = 128;

		// Token: 0x040010DA RID: 4314
		private const short VER_SUITE_ENTERPRISE = 2;

		// Token: 0x040010DB RID: 4315
		private static OSVERSIONINFOEX _osVersion = default(OSVERSIONINFOEX);
	}
}
