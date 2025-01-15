using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200011B RID: 283
	public abstract class AsymmetricSecurityKey : SecurityKey
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x00037C81 File Offset: 0x00035E81
		public AsymmetricSecurityKey()
		{
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00037C89 File Offset: 0x00035E89
		internal AsymmetricSecurityKey(SecurityKey key)
			: base(key)
		{
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000E2C RID: 3628
		[Obsolete("HasPrivateKey method is deprecated, please use PrivateKeyStatus instead.")]
		public abstract bool HasPrivateKey { get; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000E2D RID: 3629
		public abstract PrivateKeyStatus PrivateKeyStatus { get; }
	}
}
