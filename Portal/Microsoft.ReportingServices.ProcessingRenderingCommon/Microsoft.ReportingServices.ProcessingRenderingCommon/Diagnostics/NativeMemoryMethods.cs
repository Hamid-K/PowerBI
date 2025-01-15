using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000026 RID: 38
	internal class NativeMemoryMethods
	{
		// Token: 0x0600011B RID: 283
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern SafeLocalFree LocalAlloc(int uFlags, UIntPtr sizetdwBytes);

		// Token: 0x0600011C RID: 284
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr LocalFree(IntPtr handle);

		// Token: 0x0600011D RID: 285
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool AllocateAndInitializeSid(SafeLocalFree pSidAuthPtr, byte nSubAuthorityCount, uint nSubAuthority0, uint nSubAuthority1, uint nSubAuthority2, uint nSubAuthority3, uint nSubAuthority4, uint nSubAuthority5, uint nSubAuthority6, uint nSubAuthority7, out SafeSidPtr pSid);

		// Token: 0x0600011E RID: 286
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern IntPtr FreeSid(IntPtr pSid);

		// Token: 0x0600011F RID: 287
		[DllImport("ole32.dll")]
		internal static extern SafeCoTaskMem CoTaskMemAlloc(int cb);

		// Token: 0x06000120 RID: 288
		[DllImport("ole32.dll")]
		internal static extern void CoTaskMemFree(IntPtr ptr);
	}
}
