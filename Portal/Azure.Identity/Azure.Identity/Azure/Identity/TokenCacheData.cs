using System;

namespace Azure.Identity
{
	// Token: 0x02000083 RID: 131
	public struct TokenCacheData
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x0000DB3B File Offset: 0x0000BD3B
		public TokenCacheData(ReadOnlyMemory<byte> cacheBytes)
		{
			this.CacheBytes = cacheBytes;
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000DB44 File Offset: 0x0000BD44
		public readonly ReadOnlyMemory<byte> CacheBytes { get; }
	}
}
