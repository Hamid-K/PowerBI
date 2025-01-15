using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200028F RID: 655
	internal class SerialCacheItemPool : ExpandablePool<SerializedCacheItem>
	{
		// Token: 0x06001807 RID: 6151 RVA: 0x00048D38 File Offset: 0x00046F38
		public SerialCacheItemPool(long initialPoolSize, long maxPoolSize, int percentGrowth)
			: base(initialPoolSize, maxPoolSize, percentGrowth)
		{
			this.PoolName = "CacheItemPool";
			for (long num = 0L; num < initialPoolSize; num += 1L)
			{
				base.PutObjectInPool(new SerializedCacheItem());
			}
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00048D73 File Offset: 0x00046F73
		public SerialCacheItemPool(long maxPoolSize)
			: this(maxPoolSize, maxPoolSize, 10)
		{
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00048D7F File Offset: 0x00046F7F
		public SerialCacheItemPool(long initialPoolSize, long maxPoolSize)
			: this(initialPoolSize, maxPoolSize, 10)
		{
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00048D8C File Offset: 0x00046F8C
		internal override void LoadPool(long itemCount)
		{
			for (long num = 0L; num < itemCount; num += 1L)
			{
				base.PutObjectInPool(new SerializedCacheItem());
			}
		}
	}
}
