using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos
{
	// Token: 0x0200019B RID: 411
	internal class LsaBufferSafeHandle : SafeHandle
	{
		// Token: 0x06001302 RID: 4866 RVA: 0x0004028F File Offset: 0x0003E48F
		public LsaBufferSafeHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0004029D File Offset: 0x0003E49D
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000402AF File Offset: 0x0003E4AF
		protected override bool ReleaseHandle()
		{
			NativeMethods.LsaThrowIfError(NativeMethods.LsaFreeReturnBuffer(this.handle));
			this.handle = IntPtr.Zero;
			return true;
		}
	}
}
