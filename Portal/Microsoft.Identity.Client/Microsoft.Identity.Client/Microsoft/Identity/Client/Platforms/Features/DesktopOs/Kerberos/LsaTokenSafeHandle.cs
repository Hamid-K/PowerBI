using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos
{
	// Token: 0x0200019D RID: 413
	internal class LsaTokenSafeHandle : SafeHandle
	{
		// Token: 0x06001308 RID: 4872 RVA: 0x0004030B File Offset: 0x0003E50B
		public LsaTokenSafeHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00040319 File Offset: 0x0003E519
		// (set) Token: 0x0600130A RID: 4874 RVA: 0x00040321 File Offset: 0x0003E521
		public bool Impersonating { get; private set; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0004032A File Offset: 0x0003E52A
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0004033C File Offset: 0x0003E53C
		protected override bool ReleaseHandle()
		{
			this.Revert();
			if (!NativeMethods.CloseHandle(this.handle))
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return true;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0004035D File Offset: 0x0003E55D
		private void Revert()
		{
			if (!this.Impersonating)
			{
				return;
			}
			if (!NativeMethods.RevertToSelf())
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			this.Impersonating = false;
		}
	}
}
