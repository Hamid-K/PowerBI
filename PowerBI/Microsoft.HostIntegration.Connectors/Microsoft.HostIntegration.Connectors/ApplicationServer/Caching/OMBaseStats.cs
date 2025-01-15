using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200027F RID: 639
	internal class OMBaseStats
	{
		// Token: 0x06001627 RID: 5671 RVA: 0x00044399 File Offset: 0x00042599
		internal OMBaseStats(ICacheStatsContainer statsContainer)
		{
			this._statsContainer = statsContainer;
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001628 RID: 5672 RVA: 0x000443A8 File Offset: 0x000425A8
		internal ICacheStatsContainer ExternalStatsContainer
		{
			get
			{
				return this._statsContainer;
			}
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000443B0 File Offset: 0x000425B0
		protected long GetTotalMemoryFromCacheStats(string statsName)
		{
			CacheStats cacheStats = this._statsContainer.GetCacheStats(statsName);
			if (cacheStats != null)
			{
				return cacheStats.TotalMemory;
			}
			return 0L;
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x000443D8 File Offset: 0x000425D8
		protected long GetPrimaryMemoryFromCacheStats(string statsName)
		{
			CacheStats cacheStats = this._statsContainer.GetCacheStats(statsName);
			if (cacheStats != null)
			{
				return cacheStats.PrimaryMemory;
			}
			return 0L;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00044400 File Offset: 0x00042600
		protected long GetSecodaryMemoryFromCacheStats(string statsName)
		{
			CacheStats cacheStats = this._statsContainer.GetCacheStats(statsName);
			if (cacheStats != null)
			{
				return cacheStats.SecondaryMemory;
			}
			return 0L;
		}

		// Token: 0x04000C83 RID: 3203
		private ICacheStatsContainer _statsContainer;
	}
}
