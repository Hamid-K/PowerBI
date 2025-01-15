using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A10 RID: 6672
	public class MaxStalenessObjectCache : CacheDelegatingObjectCache
	{
		// Token: 0x0600A8EB RID: 43243 RVA: 0x0022F550 File Offset: 0x0022D750
		public MaxStalenessObjectCache(IObjectCache cache, DateTime maxStaleness)
			: base(cache)
		{
			this.maxStaleness = maxStaleness;
		}

		// Token: 0x0600A8EC RID: 43244 RVA: 0x0022F560 File Offset: 0x0022D760
		public override bool TryGetValue(string key, DateTime maxStaleness, CacheVersion minVersion, out object value)
		{
			maxStaleness = ((this.maxStaleness > maxStaleness) ? this.maxStaleness : maxStaleness);
			return base.TryGetValue(key, maxStaleness, minVersion, out value);
		}

		// Token: 0x040057DC RID: 22492
		private readonly DateTime maxStaleness;
	}
}
