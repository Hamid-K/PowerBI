using System;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000127 RID: 295
	public abstract class CryptoProviderCache
	{
		// Token: 0x06000E81 RID: 3713
		protected abstract string GetCacheKey(SignatureProvider signatureProvider);

		// Token: 0x06000E82 RID: 3714
		protected abstract string GetCacheKey(SecurityKey securityKey, string algorithm, string typeofProvider);

		// Token: 0x06000E83 RID: 3715
		public abstract bool TryAdd(SignatureProvider signatureProvider);

		// Token: 0x06000E84 RID: 3716
		public abstract bool TryGetSignatureProvider(SecurityKey securityKey, string algorithm, string typeofProvider, bool willCreateSignatures, out SignatureProvider signatureProvider);

		// Token: 0x06000E85 RID: 3717
		public abstract bool TryRemove(SignatureProvider signatureProvider);
	}
}
