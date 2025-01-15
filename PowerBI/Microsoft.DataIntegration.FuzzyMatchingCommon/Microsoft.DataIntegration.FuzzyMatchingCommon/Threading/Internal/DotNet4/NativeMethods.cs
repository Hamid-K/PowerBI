using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Threading.Internal.DotNet4
{
	// Token: 0x0200002B RID: 43
	[SuppressUnmanagedCodeSecurity]
	internal static class NativeMethods
	{
		// Token: 0x060000F4 RID: 244
		[DllImport("kernel32.dll")]
		internal static extern int SwitchToThread();

		// Token: 0x060000F5 RID: 245
		[DllImport("kernel32.dll")]
		internal static extern IntPtr GetCurrentProcess();

		// Token: 0x060000F6 RID: 246
		[DllImport("kernel32.dll")]
		internal static extern IntPtr GetCurrentThread();

		// Token: 0x060000F7 RID: 247
		[DllImport("kernel32.dll")]
		internal static extern UIntPtr SetThreadAffinityMask(IntPtr hThread, IntPtr dwThreadAffinityMask);

		// Token: 0x060000F8 RID: 248
		[DllImport("kernel32.dll")]
		[return: MarshalAs(2)]
		internal static extern bool GetProcessAffinityMask(IntPtr hProcess, out UIntPtr lpProcessAffinityMask, out UIntPtr lpSystemAffinityMask);

		// Token: 0x060000F9 RID: 249
		[DllImport("kernel32.dll")]
		internal static extern void GetSystemInfo(ref NativeMethods.SYSTEM_INFO lpSystemInfo);

		// Token: 0x020000D2 RID: 210
		internal struct SYSTEM_INFO
		{
			// Token: 0x040001EF RID: 495
			internal int dwOemId;

			// Token: 0x040001F0 RID: 496
			internal int dwPageSize;

			// Token: 0x040001F1 RID: 497
			internal IntPtr lpMinimumApplicationAddress;

			// Token: 0x040001F2 RID: 498
			internal IntPtr lpMaximumApplicationAddress;

			// Token: 0x040001F3 RID: 499
			internal IntPtr dwActiveProcessorMask;

			// Token: 0x040001F4 RID: 500
			internal int dwNumberOfProcessors;

			// Token: 0x040001F5 RID: 501
			internal int dwProcessorType;

			// Token: 0x040001F6 RID: 502
			internal int dwAllocationGranularity;

			// Token: 0x040001F7 RID: 503
			internal short wProcessorLevel;

			// Token: 0x040001F8 RID: 504
			internal short wProcessorRevision;
		}
	}
}
