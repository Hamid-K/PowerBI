using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A3B RID: 6715
	public class PartiallyReadOnlyPersistentCache : DualPersistentCache
	{
		// Token: 0x0600A9D5 RID: 43477 RVA: 0x00231666 File Offset: 0x0022F866
		public PartiallyReadOnlyPersistentCache(PersistentCache primaryCache, PersistentCache secondaryCache)
			: base(primaryCache, secondaryCache)
		{
		}

		// Token: 0x17002B25 RID: 11045
		// (get) Token: 0x0600A9D6 RID: 43478 RVA: 0x00231670 File Offset: 0x0022F870
		// (set) Token: 0x0600A9D7 RID: 43479 RVA: 0x0023167D File Offset: 0x0022F87D
		public override DateTime Staleness
		{
			get
			{
				return this.secondaryCache.Staleness;
			}
			set
			{
				this.secondaryCache.Staleness = value;
			}
		}

		// Token: 0x17002B26 RID: 11046
		// (get) Token: 0x0600A9D8 RID: 43480 RVA: 0x0023168C File Offset: 0x0022F88C
		public override CacheSize CacheSize
		{
			get
			{
				CacheSize cacheSize = this.primaryCache.CacheSize;
				CacheSize cacheSize2 = this.secondaryCache.CacheSize;
				return new CacheSize(cacheSize.EntryCount + cacheSize2.EntryCount, cacheSize.TotalSize + cacheSize2.TotalSize);
			}
		}

		// Token: 0x0600A9D9 RID: 43481 RVA: 0x002316D4 File Offset: 0x0022F8D4
		protected override bool TryGetStorage(bool migrate, string key, DateTime maxStaleness, CacheVersion minVersion1, CacheVersion minVersion2, out IStorage storage)
		{
			return this.primaryCache.TryGetStorage(key, maxStaleness, minVersion1, out storage) || this.secondaryCache.TryGetStorage(key, maxStaleness, minVersion2, out storage);
		}

		// Token: 0x0600A9DA RID: 43482 RVA: 0x002316FC File Offset: 0x0022F8FC
		public override IStorage CreateStorage()
		{
			return this.secondaryCache.CreateStorage();
		}

		// Token: 0x0600A9DB RID: 43483 RVA: 0x0023170C File Offset: 0x0022F90C
		public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
		{
			CacheVersion version = MultiLevelCacheVersion.GetVersion2(maxVersion);
			this.secondaryCache.CommitStorage(key, version, storage);
		}

		// Token: 0x0600A9DC RID: 43484 RVA: 0x0023092C File Offset: 0x0022EB2C
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			return this.secondaryCache.OpenStorage(key, maxStaleness, MultiLevelCacheVersion.GetVersion2(minVersion), pageSize, maxPageCount);
		}
	}
}
