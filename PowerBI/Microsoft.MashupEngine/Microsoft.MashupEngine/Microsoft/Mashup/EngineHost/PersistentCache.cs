using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x02001975 RID: 6517
	public abstract class PersistentCache : IPersistentCache, IDisposable, ICacheClock
	{
		// Token: 0x17002A3D RID: 10813
		// (get) Token: 0x0600A560 RID: 42336
		public abstract long MaxEntryLength { get; }

		// Token: 0x17002A3E RID: 10814
		// (get) Token: 0x0600A561 RID: 42337
		// (set) Token: 0x0600A562 RID: 42338
		public abstract DateTime Staleness { get; set; }

		// Token: 0x17002A3F RID: 10815
		// (get) Token: 0x0600A563 RID: 42339
		public abstract CacheSize CacheSize { get; }

		// Token: 0x17002A40 RID: 10816
		// (get) Token: 0x0600A564 RID: 42340
		public abstract bool? UserSpecific { get; }

		// Token: 0x17002A41 RID: 10817
		// (get) Token: 0x0600A565 RID: 42341 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public ICacheClock CacheClock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600A566 RID: 42342
		public abstract bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage);

		// Token: 0x0600A567 RID: 42343
		public abstract IStorage CreateStorage();

		// Token: 0x0600A568 RID: 42344
		public abstract void CommitStorage(string key, CacheVersion maxVersion, IStorage storage);

		// Token: 0x0600A569 RID: 42345
		public abstract IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount);

		// Token: 0x0600A56A RID: 42346
		public abstract void Purge();

		// Token: 0x0600A56B RID: 42347
		public abstract void Dispose();

		// Token: 0x17002A42 RID: 10818
		// (get) Token: 0x0600A56C RID: 42348
		public abstract CacheVersion Current { get; }

		// Token: 0x0600A56D RID: 42349
		public abstract CacheVersion Increment();

		// Token: 0x0600A56E RID: 42350 RVA: 0x00223860 File Offset: 0x00221A60
		public bool TryGetStorage(StructuredCacheKey key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			return (this.UserSpecific == null && this.TryGetStorage(key.GetCacheKey(true), maxStaleness, minVersion, out storage)) || this.TryGetStorage(key.GetCacheKey(this.UserSpecific.GetValueOrDefault()), maxStaleness, minVersion, out storage);
		}

		// Token: 0x0600A56F RID: 42351 RVA: 0x002238B0 File Offset: 0x00221AB0
		public void CommitStorage(StructuredCacheKey key, CacheVersion maxVersion, IStorage storage)
		{
			this.CommitStorage(key.GetCacheKey(this.UserSpecific.GetValueOrDefault(true)), maxVersion, storage);
		}

		// Token: 0x0600A570 RID: 42352 RVA: 0x002238DC File Offset: 0x00221ADC
		public IPagedStorage OpenStorage(StructuredCacheKey key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			return this.OpenStorage(key.GetCacheKey(this.UserSpecific.GetValueOrDefault(true)), maxStaleness, minVersion, pageSize, maxPageCount);
		}
	}
}
