using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000057 RID: 87
	public interface IPersistentCache : IDisposable
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600016D RID: 365
		long MaxEntryLength { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600016E RID: 366
		DateTime Staleness { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600016F RID: 367
		CacheSize CacheSize { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000170 RID: 368
		ICacheClock CacheClock { get; }

		// Token: 0x06000171 RID: 369
		bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage);

		// Token: 0x06000172 RID: 370
		bool TryGetStorage(StructuredCacheKey key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage);

		// Token: 0x06000173 RID: 371
		IStorage CreateStorage();

		// Token: 0x06000174 RID: 372
		void CommitStorage(string key, CacheVersion maxVersion, IStorage storage);

		// Token: 0x06000175 RID: 373
		void CommitStorage(StructuredCacheKey key, CacheVersion maxVersion, IStorage storage);

		// Token: 0x06000176 RID: 374
		IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount);

		// Token: 0x06000177 RID: 375
		IPagedStorage OpenStorage(StructuredCacheKey key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount);
	}
}
