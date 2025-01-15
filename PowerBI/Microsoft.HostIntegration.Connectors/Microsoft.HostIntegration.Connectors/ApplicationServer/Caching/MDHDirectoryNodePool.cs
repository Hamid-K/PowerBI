using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200035A RID: 858
	internal class MDHDirectoryNodePool : ExpandablePool<PoolableMDHDirectoryNode>
	{
		// Token: 0x06001E31 RID: 7729 RVA: 0x0005A62C File Offset: 0x0005882C
		public MDHDirectoryNodePool(long initialPoolSize, long maxPoolSize, int percentGrowth)
			: base(initialPoolSize, maxPoolSize, percentGrowth)
		{
			this.PoolName = "Directory";
			for (long num = 0L; num < initialPoolSize; num += 1L)
			{
				base.PutObjectInPool(new PoolableMDHDirectoryNode(this));
			}
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0005A668 File Offset: 0x00058868
		public MDHDirectoryNodePool(long maxPoolSize)
			: this(maxPoolSize, maxPoolSize, 10)
		{
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0005A674 File Offset: 0x00058874
		public MDHDirectoryNodePool(long initialPoolSize, long maxPoolSize)
			: this(initialPoolSize, maxPoolSize, 10)
		{
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0005A680 File Offset: 0x00058880
		internal override void LoadPool(long itemCount)
		{
			for (long num = 0L; num < itemCount; num += 1L)
			{
				base.PutObjectInPool(new PoolableMDHDirectoryNode(this));
			}
		}
	}
}
