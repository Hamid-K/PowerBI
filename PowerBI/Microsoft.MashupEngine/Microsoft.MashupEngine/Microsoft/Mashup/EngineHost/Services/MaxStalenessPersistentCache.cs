using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A11 RID: 6673
	public class MaxStalenessPersistentCache : CacheDelegatingPersistentCache
	{
		// Token: 0x0600A8ED RID: 43245 RVA: 0x0022F586 File Offset: 0x0022D786
		public MaxStalenessPersistentCache(PersistentCache cache, DateTime maxStaleness)
			: base(cache)
		{
			this.maxStaleness = maxStaleness;
		}

		// Token: 0x0600A8EE RID: 43246 RVA: 0x0022F596 File Offset: 0x0022D796
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			maxStaleness = ((this.maxStaleness > maxStaleness) ? this.maxStaleness : maxStaleness);
			return base.TryGetStorage(key, maxStaleness, minVersion, out storage);
		}

		// Token: 0x0600A8EF RID: 43247 RVA: 0x0022F5BC File Offset: 0x0022D7BC
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			maxStaleness = ((this.maxStaleness > maxStaleness) ? this.maxStaleness : maxStaleness);
			return base.OpenStorage(key, maxStaleness, minVersion, pageSize, maxPageCount);
		}

		// Token: 0x040057DD RID: 22493
		private readonly DateTime maxStaleness;
	}
}
