using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FF1 RID: 8177
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeBCryptHashHandle : SafeHandleWithBuffer
	{
		// Token: 0x0600C758 RID: 51032
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("bcrypt.dll")]
		private static extern BCryptNative.ErrorCode BCryptDestroyHash(IntPtr hHash);

		// Token: 0x0600C759 RID: 51033 RVA: 0x0027AFCD File Offset: 0x002791CD
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseNativeHandle()
		{
			return SafeBCryptHashHandle.BCryptDestroyHash(this.handle) == BCryptNative.ErrorCode.Success;
		}
	}
}
