using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000273 RID: 627
	internal interface IOMNamedCache
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600152B RID: 5419
		OMNamedCacheStats Stats { get; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600152C RID: 5420
		OMNamedCacheProperties Props { get; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600152D RID: 5421
		int RegionCount { get; }
	}
}
