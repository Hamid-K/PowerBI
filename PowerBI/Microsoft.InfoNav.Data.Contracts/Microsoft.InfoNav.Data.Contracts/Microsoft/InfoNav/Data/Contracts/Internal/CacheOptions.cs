using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000181 RID: 385
	[Flags]
	public enum CacheOptions
	{
		// Token: 0x04000599 RID: 1433
		None = 0,
		// Token: 0x0400059A RID: 1434
		AllowCache = 1,
		// Token: 0x0400059B RID: 1435
		AllowQuery = 2,
		// Token: 0x0400059C RID: 1436
		CacheResult = 4,
		// Token: 0x0400059D RID: 1437
		PreventRefresh = 8
	}
}
