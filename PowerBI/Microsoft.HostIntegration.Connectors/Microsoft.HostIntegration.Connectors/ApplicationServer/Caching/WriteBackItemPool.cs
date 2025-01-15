using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001F7 RID: 503
	internal class WriteBackItemPool : ExpandablePool<WriteBackItem>
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x00036F30 File Offset: 0x00035130
		public WriteBackItemPool(long initialPoolSize, long maxPoolSize, int percentGrowth)
			: base(initialPoolSize, maxPoolSize, percentGrowth)
		{
			this.PoolName = "WBItemPool";
			for (long num = 0L; num < initialPoolSize; num += 1L)
			{
				base.PutObjectInPool(new PoolableWriteBackItem(this));
			}
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00036F6C File Offset: 0x0003516C
		internal override void LoadPool(long itemCount)
		{
			for (long num = 0L; num < itemCount; num += 1L)
			{
				base.PutObjectInPool(new PoolableWriteBackItem(this));
			}
		}
	}
}
