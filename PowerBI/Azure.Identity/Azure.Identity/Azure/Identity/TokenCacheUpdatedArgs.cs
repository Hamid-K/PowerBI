using System;

namespace Azure.Identity
{
	// Token: 0x02000086 RID: 134
	public class TokenCacheUpdatedArgs
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000DBC0 File Offset: 0x0000BDC0
		internal TokenCacheUpdatedArgs(ReadOnlyMemory<byte> cacheData, bool enableCae)
		{
			this.UnsafeCacheData = cacheData;
			this.IsCaeEnabled = enableCae;
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000DBD6 File Offset: 0x0000BDD6
		public ReadOnlyMemory<byte> UnsafeCacheData { get; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000DBDE File Offset: 0x0000BDDE
		public bool IsCaeEnabled { get; }
	}
}
