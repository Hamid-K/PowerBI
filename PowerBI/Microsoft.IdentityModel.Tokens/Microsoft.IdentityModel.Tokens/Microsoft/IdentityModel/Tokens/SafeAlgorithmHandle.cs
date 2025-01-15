using System;
using System.Runtime.InteropServices;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200016B RID: 363
	internal sealed class SafeAlgorithmHandle : SafeBCryptHandle
	{
		// Token: 0x0600107A RID: 4218 RVA: 0x0004027F File Offset: 0x0003E47F
		protected sealed override bool ReleaseHandle()
		{
			return SafeAlgorithmHandle.BCryptCloseAlgorithmProvider(this.handle, 0) == 0U;
		}

		// Token: 0x0600107B RID: 4219
		[DllImport("BCrypt.dll")]
		private static extern uint BCryptCloseAlgorithmProvider(IntPtr hAlgorithm, int dwFlags);
	}
}
