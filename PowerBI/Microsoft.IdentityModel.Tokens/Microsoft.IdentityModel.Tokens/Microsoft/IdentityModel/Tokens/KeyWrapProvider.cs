using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000135 RID: 309
	public abstract class KeyWrapProvider : IDisposable
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000F14 RID: 3860
		public abstract string Algorithm { get; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000F15 RID: 3861
		// (set) Token: 0x06000F16 RID: 3862
		public abstract string Context { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000F17 RID: 3863
		public abstract SecurityKey Key { get; }

		// Token: 0x06000F18 RID: 3864 RVA: 0x0003C352 File Offset: 0x0003A552
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F19 RID: 3865
		protected abstract void Dispose(bool disposing);

		// Token: 0x06000F1A RID: 3866
		public abstract byte[] UnwrapKey(byte[] keyBytes);

		// Token: 0x06000F1B RID: 3867
		public abstract byte[] WrapKey(byte[] keyBytes);
	}
}
