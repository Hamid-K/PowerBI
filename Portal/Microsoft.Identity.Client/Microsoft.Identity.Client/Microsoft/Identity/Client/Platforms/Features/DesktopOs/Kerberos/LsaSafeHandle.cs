using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos
{
	// Token: 0x0200019C RID: 412
	internal class LsaSafeHandle : SafeHandle
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x000402CD File Offset: 0x0003E4CD
		public LsaSafeHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x000402DB File Offset: 0x0003E4DB
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000402ED File Offset: 0x0003E4ED
		protected override bool ReleaseHandle()
		{
			NativeMethods.LsaThrowIfError(NativeMethods.LsaDeregisterLogonProcess(this.handle));
			this.handle = IntPtr.Zero;
			return true;
		}
	}
}
