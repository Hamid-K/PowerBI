using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000147 RID: 327
	public enum CacheRefreshReason
	{
		// Token: 0x040004FE RID: 1278
		NotApplicable,
		// Token: 0x040004FF RID: 1279
		ForceRefreshOrClaims,
		// Token: 0x04000500 RID: 1280
		NoCachedAccessToken,
		// Token: 0x04000501 RID: 1281
		Expired,
		// Token: 0x04000502 RID: 1282
		ProactivelyRefreshed
	}
}
