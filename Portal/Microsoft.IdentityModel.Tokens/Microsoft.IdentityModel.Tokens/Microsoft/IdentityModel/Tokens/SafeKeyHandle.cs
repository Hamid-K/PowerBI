using System;
using System.Runtime.InteropServices;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200016D RID: 365
	internal sealed class SafeKeyHandle : SafeBCryptHandle
	{
		// Token: 0x06001080 RID: 4224 RVA: 0x000402B8 File Offset: 0x0003E4B8
		public void SetParentHandle(SafeAlgorithmHandle parentHandle)
		{
			bool flag = false;
			parentHandle.DangerousAddRef(ref flag);
			this._parentHandle = parentHandle;
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x000402D6 File Offset: 0x0003E4D6
		protected sealed override bool ReleaseHandle()
		{
			if (this._parentHandle != null)
			{
				this._parentHandle.DangerousRelease();
				this._parentHandle = null;
			}
			return SafeKeyHandle.BCryptDestroyKey(this.handle) == 0U;
		}

		// Token: 0x06001082 RID: 4226
		[DllImport("BCrypt.dll")]
		private static extern uint BCryptDestroyKey(IntPtr hKey);

		// Token: 0x04000616 RID: 1558
		private SafeAlgorithmHandle _parentHandle;
	}
}
