using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019CB RID: 6603
	public abstract class DualPersistentCache : PersistentCache
	{
		// Token: 0x0600A72B RID: 42795 RVA: 0x002295E0 File Offset: 0x002277E0
		public DualPersistentCache(PersistentCache primaryCache, PersistentCache secondaryCache)
		{
			this.primaryCache = primaryCache;
			this.secondaryCache = secondaryCache;
		}

		// Token: 0x17002A92 RID: 10898
		// (get) Token: 0x0600A72C RID: 42796 RVA: 0x002295F6 File Offset: 0x002277F6
		public override long MaxEntryLength
		{
			get
			{
				return Math.Min(this.primaryCache.MaxEntryLength, this.secondaryCache.MaxEntryLength);
			}
		}

		// Token: 0x17002A93 RID: 10899
		// (get) Token: 0x0600A72D RID: 42797 RVA: 0x00229614 File Offset: 0x00227814
		public override CacheSize CacheSize
		{
			get
			{
				CacheSize cacheSize = this.primaryCache.CacheSize;
				CacheSize cacheSize2 = this.secondaryCache.CacheSize;
				return new CacheSize(cacheSize.EntryCount + cacheSize2.EntryCount, cacheSize.TotalSize + cacheSize2.TotalSize);
			}
		}

		// Token: 0x17002A94 RID: 10900
		// (get) Token: 0x0600A72E RID: 42798 RVA: 0x0022965C File Offset: 0x0022785C
		public override bool? UserSpecific
		{
			get
			{
				bool? userSpecific = this.primaryCache.UserSpecific;
				bool? userSpecific2 = this.secondaryCache.UserSpecific;
				if (!((userSpecific.GetValueOrDefault() == userSpecific2.GetValueOrDefault()) & (userSpecific != null == (userSpecific2 != null))))
				{
					return null;
				}
				return this.primaryCache.UserSpecific;
			}
		}

		// Token: 0x0600A72F RID: 42799 RVA: 0x002296BC File Offset: 0x002278BC
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			CacheVersion version = MultiLevelCacheVersion.GetVersion1(minVersion);
			CacheVersion version2 = MultiLevelCacheVersion.GetVersion2(minVersion);
			return this.TryGetStorage(true, key, maxStaleness, version, version2, out storage);
		}

		// Token: 0x0600A730 RID: 42800
		protected abstract bool TryGetStorage(bool migrate, string key, DateTime maxStaleness, CacheVersion minVersion1, CacheVersion minVersion2, out IStorage storage);

		// Token: 0x0600A731 RID: 42801 RVA: 0x002296E4 File Offset: 0x002278E4
		public override void Purge()
		{
			this.primaryCache.Purge();
			this.secondaryCache.Purge();
		}

		// Token: 0x0600A732 RID: 42802 RVA: 0x002296FC File Offset: 0x002278FC
		public override void Dispose()
		{
			this.primaryCache.Dispose();
			this.secondaryCache.Dispose();
		}

		// Token: 0x17002A95 RID: 10901
		// (get) Token: 0x0600A733 RID: 42803 RVA: 0x00229714 File Offset: 0x00227914
		public override CacheVersion Current
		{
			get
			{
				return new MultiLevelCacheVersion(this.primaryCache.CacheClock.Current, this.secondaryCache.CacheClock.Current);
			}
		}

		// Token: 0x0600A734 RID: 42804 RVA: 0x0022973B File Offset: 0x0022793B
		public override CacheVersion Increment()
		{
			return new MultiLevelCacheVersion(this.primaryCache.CacheClock.Increment(), this.secondaryCache.CacheClock.Increment());
		}

		// Token: 0x04005704 RID: 22276
		protected readonly PersistentCache primaryCache;

		// Token: 0x04005705 RID: 22277
		protected readonly PersistentCache secondaryCache;
	}
}
