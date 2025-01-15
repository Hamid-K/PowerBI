using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FFF RID: 8191
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600C79C RID: 51100 RVA: 0x0000EDD3 File Offset: 0x0000CFD3
		private SafeLibraryHandle()
			: base(true)
		{
		}

		// Token: 0x0600C79D RID: 51101
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x0600C79E RID: 51102 RVA: 0x0027B764 File Offset: 0x00279964
		protected override bool ReleaseHandle()
		{
			return SafeLibraryHandle.FreeLibrary(this.handle);
		}
	}
}
