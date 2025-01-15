using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FF0 RID: 8176
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeBCryptAlgorithmHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600C755 RID: 51029 RVA: 0x0000EDD3 File Offset: 0x0000CFD3
		private SafeBCryptAlgorithmHandle()
			: base(true)
		{
		}

		// Token: 0x0600C756 RID: 51030
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("bcrypt.dll")]
		private static extern BCryptNative.ErrorCode BCryptCloseAlgorithmProvider(IntPtr hAlgorithm, int flags);

		// Token: 0x0600C757 RID: 51031 RVA: 0x0027AFBC File Offset: 0x002791BC
		protected override bool ReleaseHandle()
		{
			return SafeBCryptAlgorithmHandle.BCryptCloseAlgorithmProvider(this.handle, 0) == BCryptNative.ErrorCode.Success;
		}
	}
}
