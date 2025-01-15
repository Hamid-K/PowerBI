using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000058 RID: 88
	public interface IObjectCache : IDisposable
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000178 RID: 376
		DateTime Staleness { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000179 RID: 377
		ICacheClock CacheClock { get; }

		// Token: 0x0600017A RID: 378
		bool TryGetValue(string key, DateTime maxStaleness, CacheVersion minVersion, out object value);

		// Token: 0x0600017B RID: 379
		void CommitValue(string key, CacheVersion maxVersion, int size, object value);

		// Token: 0x0600017C RID: 380
		bool TryGetValue(StructuredCacheKey key, DateTime maxStaleness, CacheVersion minVersion, out object value);

		// Token: 0x0600017D RID: 381
		void CommitValue(StructuredCacheKey key, CacheVersion maxVersion, int size, object value);
	}
}
