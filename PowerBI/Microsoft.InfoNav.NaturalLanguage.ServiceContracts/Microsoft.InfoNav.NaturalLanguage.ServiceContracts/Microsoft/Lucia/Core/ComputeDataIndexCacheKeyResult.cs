using System;
using System.ComponentModel;
using Microsoft.Lucia.Core.TermIndex;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000047 RID: 71
	[ImmutableObject(true)]
	public sealed class ComputeDataIndexCacheKeyResult
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00003F32 File Offset: 0x00002132
		public ComputeDataIndexCacheKeyResult(DataIndexCacheKey cacheKey)
		{
			this.CacheKey = cacheKey;
			this.Warnings = ComputeDataIndexCacheKeyWarnings.None;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00003F48 File Offset: 0x00002148
		public ComputeDataIndexCacheKeyResult(ComputeDataIndexCacheKeyWarnings warnings)
		{
			this.Warnings = warnings;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00003F57 File Offset: 0x00002157
		public DataIndexCacheKey CacheKey { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00003F5F File Offset: 0x0000215F
		public ComputeDataIndexCacheKeyWarnings Warnings { get; }
	}
}
