using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A23 RID: 6691
	public class MinVersionPersistentCache : CacheDelegatingPersistentCache
	{
		// Token: 0x0600A949 RID: 43337 RVA: 0x002302B0 File Offset: 0x0022E4B0
		public MinVersionPersistentCache(PersistentCache cache, CacheVersion minVersion)
			: base(cache)
		{
			this.minVersion = minVersion;
		}

		// Token: 0x0600A94A RID: 43338 RVA: 0x002302C0 File Offset: 0x0022E4C0
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			minVersion = CacheVersion.GetMaxVersion(this.minVersion, minVersion);
			return base.TryGetStorage(key, maxStaleness, minVersion, out storage);
		}

		// Token: 0x0600A94B RID: 43339 RVA: 0x002302DB File Offset: 0x0022E4DB
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			minVersion = CacheVersion.GetMaxVersion(this.minVersion, minVersion);
			return base.OpenStorage(key, maxStaleness, minVersion, pageSize, maxPageCount);
		}

		// Token: 0x04005815 RID: 22549
		private readonly CacheVersion minVersion;
	}
}
