using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001F2 RID: 498
	internal class PoolableWriteBackItemFactory : IWriteBackItemFactory
	{
		// Token: 0x06001058 RID: 4184 RVA: 0x00036B0C File Offset: 0x00034D0C
		public PoolableWriteBackItemFactory(long initialPoolSize, long maxPoolSize)
		{
			this._pool = new WriteBackItemPool(initialPoolSize, maxPoolSize, 10);
			this._pool.LogSource = "DistributedCache.WriteBackItemPool";
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00036B34 File Offset: 0x00034D34
		public WriteBackItem GetWriteBackItem(AOMCacheItem item, StoreOperation operation, int writeFailCount, long firstWrite)
		{
			WriteBackItem objectFromPool = this._pool.GetObjectFromPool();
			if (objectFromPool == null)
			{
				throw new DataCacheException("WriteBackItemPool", 18001, string.Format(CultureInfo.CurrentUICulture, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 18001), new object[] { this.Pool.PoolName }));
			}
			objectFromPool.Init(item, operation, writeFailCount, firstWrite);
			return objectFromPool;
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00036B9B File Offset: 0x00034D9B
		public WriteBackItem GetWriteBackItem(WriteBackItem item)
		{
			return this.GetWriteBackItem(item.OmItem, item.Operation, item.WriteFailCount, item.FirstWrite);
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x00036BBB File Offset: 0x00034DBB
		internal ExpandablePool<WriteBackItem> Pool
		{
			get
			{
				return this._pool;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00036BC3 File Offset: 0x00034DC3
		public long AvailableObjects
		{
			get
			{
				return this._pool.AvailableObjects;
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x00036BD0 File Offset: 0x00034DD0
		public long CurrentCapacity
		{
			get
			{
				return this._pool.CurrentCapacity;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00036BDD File Offset: 0x00034DDD
		public long MaxCapacity
		{
			get
			{
				return this._pool.MaxCapacity;
			}
		}

		// Token: 0x04000ABA RID: 2746
		private ExpandablePool<WriteBackItem> _pool;
	}
}
