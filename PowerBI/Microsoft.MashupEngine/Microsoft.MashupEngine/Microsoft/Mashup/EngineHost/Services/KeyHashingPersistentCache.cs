using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019F5 RID: 6645
	public sealed class KeyHashingPersistentCache : CacheDelegatingPersistentCache
	{
		// Token: 0x0600A80B RID: 43019 RVA: 0x0022BC43 File Offset: 0x00229E43
		public KeyHashingPersistentCache(PersistentCache cache, KeyHasher keyHasher)
			: base(cache)
		{
			this.keyHasher = keyHasher;
		}

		// Token: 0x0600A80C RID: 43020 RVA: 0x0022BC53 File Offset: 0x00229E53
		public override bool TryGetStorage(string key, DateTime maxStaleness, CacheVersion minVersion, out IStorage storage)
		{
			return base.TryGetStorage(this.keyHasher.HashKey(key), maxStaleness, minVersion, out storage);
		}

		// Token: 0x0600A80D RID: 43021 RVA: 0x0022BC6B File Offset: 0x00229E6B
		public override void CommitStorage(string key, CacheVersion maxVersion, IStorage storage)
		{
			base.CommitStorage(this.keyHasher.HashKey(key), maxVersion, storage);
		}

		// Token: 0x0600A80E RID: 43022 RVA: 0x0022BC81 File Offset: 0x00229E81
		public override IPagedStorage OpenStorage(string key, DateTime maxStaleness, CacheVersion minVersion, int pageSize, int maxPageCount)
		{
			return base.OpenStorage(this.keyHasher.HashKey(key), maxStaleness, minVersion, pageSize, maxPageCount);
		}

		// Token: 0x0400577D RID: 22397
		private readonly KeyHasher keyHasher;
	}
}
