using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002D2 RID: 722
	internal sealed class SegmentCache
	{
		// Token: 0x060019E4 RID: 6628 RVA: 0x00068628 File Offset: 0x00066828
		public SegmentCache()
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				this.m_stats = new Dictionary<Guid, SegmentCache.CacheStatistics>();
			}
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00068647 File Offset: 0x00066847
		public void ClearEntry(Guid segmentId)
		{
			RSLocalCacheManager.Current.RemoveFromCache(SegmentCacheEntry.BuildCacheKey(segmentId));
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x0006865C File Offset: 0x0006685C
		public void SetCache(Guid segmentId, byte[] buffer)
		{
			SegmentCacheEntry segmentCacheEntry = new SegmentCacheEntry(segmentId, buffer);
			RSLocalCacheManager.Current.SaveCacheItem(segmentCacheEntry);
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00068680 File Offset: 0x00066880
		public bool TryGetCache(Guid segmentId, out byte[] buffer)
		{
			buffer = null;
			string text = SegmentCacheEntry.BuildCacheKey(segmentId);
			SegmentCacheEntry segmentCacheEntry = RSLocalCacheManager.Current.GetCacheItem(text) as SegmentCacheEntry;
			bool flag = segmentCacheEntry != null;
			if (flag)
			{
				this.MarkCacheHit(segmentId);
				buffer = segmentCacheEntry.SegmentBytes;
				return flag;
			}
			this.MarkCacheMiss(segmentId);
			return flag;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x000686C8 File Offset: 0x000668C8
		public void DumpStatistics()
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				Dictionary<Guid, SegmentCache.CacheStatistics> stats = this.m_stats;
				lock (stats)
				{
					foreach (KeyValuePair<Guid, SegmentCache.CacheStatistics> keyValuePair in this.m_stats)
					{
						RSTrace.ChunkTracer.Trace(TraceLevel.Verbose, "Cache Stats for Segment '{0}': {1} Hits, {2} Misses", new object[]
						{
							keyValuePair.Key,
							keyValuePair.Value.Hits,
							keyValuePair.Value.Misses
						});
					}
				}
			}
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00068798 File Offset: 0x00066998
		private void MarkCacheHit(Guid segmentId)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				Dictionary<Guid, SegmentCache.CacheStatistics> stats = this.m_stats;
				lock (stats)
				{
					if (!this.m_stats.ContainsKey(segmentId))
					{
						this.m_stats.Add(segmentId, new SegmentCache.CacheStatistics());
					}
					this.m_stats[segmentId].Hits++;
				}
			}
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00068818 File Offset: 0x00066A18
		private void MarkCacheMiss(Guid segmentId)
		{
			if (RSTrace.ChunkTracer.TraceVerbose)
			{
				Dictionary<Guid, SegmentCache.CacheStatistics> stats = this.m_stats;
				lock (stats)
				{
					if (!this.m_stats.ContainsKey(segmentId))
					{
						this.m_stats.Add(segmentId, new SegmentCache.CacheStatistics());
					}
					this.m_stats[segmentId].Misses++;
				}
			}
		}

		// Token: 0x04000969 RID: 2409
		private readonly Dictionary<Guid, SegmentCache.CacheStatistics> m_stats;

		// Token: 0x020004E5 RID: 1253
		private sealed class CacheStatistics
		{
			// Token: 0x060024A4 RID: 9380 RVA: 0x00086BEC File Offset: 0x00084DEC
			public CacheStatistics()
			{
				this.Hits = 0;
				this.Misses = 0;
			}

			// Token: 0x04001144 RID: 4420
			public int Hits;

			// Token: 0x04001145 RID: 4421
			public int Misses;
		}
	}
}
