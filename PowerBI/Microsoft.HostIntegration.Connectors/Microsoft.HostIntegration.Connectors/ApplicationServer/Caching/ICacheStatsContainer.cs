using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000270 RID: 624
	internal interface ICacheStatsContainer
	{
		// Token: 0x060014DB RID: 5339
		CacheStats GetCacheStats(string statsName);
	}
}
