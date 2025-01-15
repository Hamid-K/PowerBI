using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FF2 RID: 8178
	[SecurityCritical(SecurityCriticalScope.Everything)]
	internal sealed class SafeBCryptKeyHandle : SafeHandleWithBuffer
	{
		// Token: 0x0600C75B RID: 51035
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("bcrypt.dll")]
		private static extern BCryptNative.ErrorCode BCryptDestroyKey(IntPtr hKey);

		// Token: 0x0600C75C RID: 51036 RVA: 0x0027AFE5 File Offset: 0x002791E5
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected override bool ReleaseNativeHandle()
		{
			return SafeBCryptKeyHandle.BCryptDestroyKey(this.handle) == BCryptNative.ErrorCode.Success;
		}
	}
}
