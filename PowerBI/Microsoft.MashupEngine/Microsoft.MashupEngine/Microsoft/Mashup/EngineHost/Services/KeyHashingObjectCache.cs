using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019F4 RID: 6644
	public sealed class KeyHashingObjectCache : CacheDelegatingObjectCache
	{
		// Token: 0x0600A808 RID: 43016 RVA: 0x0022BBF9 File Offset: 0x00229DF9
		public KeyHashingObjectCache(IObjectCache cache, KeyHasher keyHasher)
			: base(cache)
		{
			this.keyHasher = keyHasher;
		}

		// Token: 0x0600A809 RID: 43017 RVA: 0x0022BC09 File Offset: 0x00229E09
		public override bool TryGetValue(string key, DateTime maxStaleness, CacheVersion minVersion, out object value)
		{
			return this.Cache.TryGetValue(this.keyHasher.HashKey(key), maxStaleness, minVersion, out value);
		}

		// Token: 0x0600A80A RID: 43018 RVA: 0x0022BC26 File Offset: 0x00229E26
		public override void CommitValue(string key, CacheVersion maxVersion, int size, object value)
		{
			this.Cache.CommitValue(this.keyHasher.HashKey(key), maxVersion, size, value);
		}

		// Token: 0x0400577C RID: 22396
		private readonly KeyHasher keyHasher;
	}
}
