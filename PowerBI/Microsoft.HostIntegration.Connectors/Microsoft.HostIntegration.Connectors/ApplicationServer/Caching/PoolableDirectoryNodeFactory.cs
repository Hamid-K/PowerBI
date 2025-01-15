using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000359 RID: 857
	internal class PoolableDirectoryNodeFactory : IDirectoryNodeFactory
	{
		// Token: 0x06001E2B RID: 7723 RVA: 0x0005A573 File Offset: 0x00058773
		public PoolableDirectoryNodeFactory(long initialPoolSize, long maxPoolSize)
		{
			this._pool = new MDHDirectoryNodePool(initialPoolSize, maxPoolSize);
			this._pool.LogSource = "DistributedCache.DirectoryPool";
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x0005A598 File Offset: 0x00058798
		public MDHDirectoryNode GetDirectory(short maskOffset, MDHDirectoryNode parent, short parentIndex)
		{
			PoolableMDHDirectoryNode objectFromPool = this._pool.GetObjectFromPool();
			if (objectFromPool == null)
			{
				throw new DataCacheException("DirectoryPool", 18001, string.Format(CultureInfo.CurrentUICulture, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 18001), new object[] { this.Pool.PoolName }));
			}
			objectFromPool.Init(maskOffset, parent, parentIndex);
			return objectFromPool;
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x0005A5FD File Offset: 0x000587FD
		internal ExpandablePool<PoolableMDHDirectoryNode> Pool
		{
			get
			{
				return this._pool;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x0005A605 File Offset: 0x00058805
		public long AvailableObjects
		{
			get
			{
				return this._pool.AvailableObjects;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x0005A612 File Offset: 0x00058812
		public long CurrentCapacity
		{
			get
			{
				return this._pool.CurrentCapacity;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001E30 RID: 7728 RVA: 0x0005A61F File Offset: 0x0005881F
		public long MaxCapacity
		{
			get
			{
				return this._pool.MaxCapacity;
			}
		}

		// Token: 0x040010FE RID: 4350
		private ExpandablePool<PoolableMDHDirectoryNode> _pool;
	}
}
