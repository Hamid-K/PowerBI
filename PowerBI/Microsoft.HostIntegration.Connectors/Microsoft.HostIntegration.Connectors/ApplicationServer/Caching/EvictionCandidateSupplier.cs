using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000319 RID: 793
	internal class EvictionCandidateSupplier
	{
		// Token: 0x06001CCF RID: 7375 RVA: 0x00057748 File Offset: 0x00055948
		public EvictionCandidateSupplier(IEnumerator<CacheRegionName> regions, IObjectManager om, EvictionGenerations generations, CheckItemInUseDelegate checkItemInUse, GetEvictionGenerationDelegate getEvictionGenerationEviction)
		{
			this._regions = regions;
			this._om = om;
			this._currGenStats = generations;
			this._newGenerationStats = this._currGenStats;
			this._itemInUseCallback = checkItemInUse;
			this._getEvictionGenerationsCallback = getEvictionGenerationEviction;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x00057798 File Offset: 0x00055998
		public IEnumerator<AOMCacheItem> StartEnumeration(long sizeToEvict, EvictExpireState typeOfEviction)
		{
			this._now = Stopwatch.GetTimestamp();
			if (typeOfEviction == EvictExpireState.EVICT)
			{
				DateTime dateTime = this._currGenStats.DistributeSizeToEvict(sizeToEvict);
				this._newGenerationStats = EvictionGenerations.CreateTemplateForNewScan(dateTime, this._getEvictionGenerationsCallback);
			}
			else
			{
				this._newGenerationStats = this._currGenStats.CreateTemplateForNewScan(this._getEvictionGenerationsCallback);
			}
			while (this._regions.MoveNext())
			{
				CacheRegionName pair = this._regions.Current;
				OMRegion region = this._om.GetRegion(pair.CacheName, pair.RegionName);
				if (region != null)
				{
					IHashtableEnumerator en = region.Enumerate();
					bool regionEvictable = region.IsEvictable;
					while (en.MoveNext() && !region.IsDeleted && (region.IsExpirableItemsFound || region.IsEvictable))
					{
						AOMCacheItem cand = (AOMCacheItem)en.Current;
						int gen = this._currGenStats.GetGeneration(cand);
						if (this.CanBeEvicted(cand, regionEvictable, gen, typeOfEviction))
						{
							this._currGenStats.DecrEvictableSize(gen, cand.Size);
							yield return cand;
						}
						else if (regionEvictable)
						{
							this._newGenerationStats.AddCandidate(cand);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x000577C2 File Offset: 0x000559C2
		public EvictionGenerations GetUpdatedStatistics()
		{
			return this._newGenerationStats;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x000577CA File Offset: 0x000559CA
		public void ResetUtcNow()
		{
			this._now = Stopwatch.GetTimestamp();
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x000577D8 File Offset: 0x000559D8
		private bool CanBeEvicted(AOMCacheItem item, bool regionEvictable, int gen, EvictExpireState type)
		{
			if (item.IsItemExpiredForEviction(this._now) || (regionEvictable && type == EvictExpireState.EVICT && this._currGenStats.GenerationNeedsEviction(gen)))
			{
				bool flag = this._itemInUseCallback != null && this._itemInUseCallback(item);
				return !flag;
			}
			return false;
		}

		// Token: 0x04000FF8 RID: 4088
		private IEnumerator<CacheRegionName> _regions;

		// Token: 0x04000FF9 RID: 4089
		private IObjectManager _om;

		// Token: 0x04000FFA RID: 4090
		private EvictionGenerations _currGenStats;

		// Token: 0x04000FFB RID: 4091
		private EvictionGenerations _newGenerationStats;

		// Token: 0x04000FFC RID: 4092
		private CheckItemInUseDelegate _itemInUseCallback;

		// Token: 0x04000FFD RID: 4093
		private GetEvictionGenerationDelegate _getEvictionGenerationsCallback;

		// Token: 0x04000FFE RID: 4094
		private long _now = Stopwatch.GetTimestamp();
	}
}
