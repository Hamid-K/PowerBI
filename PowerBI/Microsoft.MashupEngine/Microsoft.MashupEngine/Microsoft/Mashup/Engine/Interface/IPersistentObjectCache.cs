using System;
using System.IO;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000059 RID: 89
	public interface IPersistentObjectCache : IDisposable
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600017E RID: 382
		DateTime Staleness { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600017F RID: 383
		ICacheClock CacheClock { get; }

		// Token: 0x06000180 RID: 384
		bool TryGetValue(string key, DateTime maxStaleness, CacheVersion minVersion, Func<Stream, object> deserializer, out object value);

		// Token: 0x06000181 RID: 385
		void CommitValue(string key, CacheVersion maxVersion, Action<Stream, object> serializer, object value);

		// Token: 0x06000182 RID: 386
		bool TryGetValue(StructuredCacheKey key, DateTime maxStaleness, CacheVersion minVersion, Func<Stream, object> deserializer, out object value);

		// Token: 0x06000183 RID: 387
		void CommitValue(StructuredCacheKey keykey, CacheVersion maxVersion, Action<Stream, object> serializer, object value);
	}
}
