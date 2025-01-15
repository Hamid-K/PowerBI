using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A22 RID: 6690
	public class MinVersionObjectCache : CacheDelegatingObjectCache
	{
		// Token: 0x0600A947 RID: 43335 RVA: 0x00230285 File Offset: 0x0022E485
		public MinVersionObjectCache(IObjectCache cache, CacheVersion minVersion)
			: base(cache)
		{
			this.minVersion = minVersion;
		}

		// Token: 0x0600A948 RID: 43336 RVA: 0x00230295 File Offset: 0x0022E495
		public override bool TryGetValue(string key, DateTime maxStaleness, CacheVersion minVersion, out object value)
		{
			minVersion = CacheVersion.GetMaxVersion(this.minVersion, minVersion);
			return base.TryGetValue(key, maxStaleness, minVersion, out value);
		}

		// Token: 0x04005814 RID: 22548
		private readonly CacheVersion minVersion;
	}
}
