using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos
{
	// Token: 0x02000199 RID: 409
	internal class CredentialHandle : SafeHandle
	{
		// Token: 0x060012FF RID: 4863 RVA: 0x0004026B File Offset: 0x0003E46B
		public unsafe CredentialHandle(void* cred)
			: base(new IntPtr(cred), true)
		{
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x0004027A File Offset: 0x0003E47A
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0004028C File Offset: 0x0003E48C
		protected override bool ReleaseHandle()
		{
			return true;
		}
	}
}
