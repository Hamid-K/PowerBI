using System;
using System.Runtime.InteropServices;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200016C RID: 364
	internal abstract class SafeBCryptHandle : SafeHandle
	{
		// Token: 0x0600107D RID: 4221 RVA: 0x00040298 File Offset: 0x0003E498
		protected SafeBCryptHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x000402A6 File Offset: 0x0003E4A6
		public sealed override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x0600107F RID: 4223
		protected abstract override bool ReleaseHandle();
	}
}
