using System;
using System.Globalization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200028E RID: 654
	internal class SerializedCacheItemFactory : ICacheItemFactory
	{
		// Token: 0x060017FC RID: 6140 RVA: 0x00048B30 File Offset: 0x00046D30
		public SerializedCacheItemFactory(long initialPoolSize, long maxPoolSize)
		{
			this.Pool = new SerialCacheItemPool(initialPoolSize, maxPoolSize);
			this.Pool.LogSource = "DistributedCache.ItemPool";
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x00048B58 File Offset: 0x00046D58
		private SerializedCacheItem GetItemFromPool()
		{
			SerializedCacheItem objectFromPool = this.Pool.GetObjectFromPool();
			if (objectFromPool == null)
			{
				throw new DataCacheException("CacheItemPool", 18001, string.Format(CultureInfo.CurrentUICulture, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 18001), new object[] { this.Pool.PoolName }));
			}
			return objectFromPool;
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x00048BB4 File Offset: 0x00046DB4
		public AOMCacheItem GetCacheItem(IOMRegion region, Key key)
		{
			bool flag = false;
			SerializedCacheItem itemFromPool = this.GetItemFromPool();
			AOMCacheItem aomcacheItem;
			try
			{
				itemFromPool.Init(region, key);
				flag = true;
				aomcacheItem = itemFromPool;
			}
			finally
			{
				if (!flag)
				{
					itemFromPool.Clean();
					this.Pool.PutObjectInPool(itemFromPool);
				}
			}
			return aomcacheItem;
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00003CAB File Offset: 0x00001EAB
		public AOMCacheItem GetCacheItem(Key key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00048C00 File Offset: 0x00046E00
		public AOMCacheItem GetCacheItem(IOMRegion region, Key key, object value, long TTL, long extnTimeout, object[] tags)
		{
			bool flag = false;
			SerializedCacheItem itemFromPool = this.GetItemFromPool();
			AOMCacheItem aomcacheItem;
			try
			{
				itemFromPool.Init(region, key, value, TTL, extnTimeout, tags);
				flag = true;
				aomcacheItem = itemFromPool;
			}
			finally
			{
				if (!flag)
				{
					itemFromPool.Clean();
					this.Pool.PutObjectInPool(itemFromPool);
				}
			}
			return aomcacheItem;
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x00048C54 File Offset: 0x00046E54
		public AOMCacheItem GetCacheItem(IOMRegion region, Key key, object value, long TTL, long extnTimeout, object[] tags, ObjectType oType)
		{
			return this.GetCacheItem(region, key, value, TTL, extnTimeout, tags);
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00003CAB File Offset: 0x00001EAB
		public AOMCacheItem GetCacheItem(AOMCacheItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00048C68 File Offset: 0x00046E68
		public AOMCacheItem GetCacheItem(OMCacheItem item, OMRegion region)
		{
			bool flag = false;
			SerializedCacheItem itemFromPool = this.GetItemFromPool();
			try
			{
				itemFromPool.Init(region, (Key)item.Key, item.Value, item.TimeToLive, item.ExtensionTimeout, item.Tags);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					itemFromPool.Clean();
					this.Pool.PutObjectInPool(itemFromPool);
				}
			}
			itemFromPool.Flags = item.Flags;
			itemFromPool.LockExpirationTime = item.LockExpirationTime;
			itemFromPool.Version = item.Version;
			itemFromPool.SetLockHandle(item.GetDMLockHandle());
			itemFromPool.LastAccess = item.LastAccess;
			return itemFromPool;
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00048D10 File Offset: 0x00046F10
		public long AvailableObjects
		{
			get
			{
				return this.Pool.AvailableObjects;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x00048D1D File Offset: 0x00046F1D
		public long CurrentCapacity
		{
			get
			{
				return this.Pool.CurrentCapacity;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00048D2A File Offset: 0x00046F2A
		public long MaxCapacity
		{
			get
			{
				return this.Pool.MaxCapacity;
			}
		}

		// Token: 0x04000D3D RID: 3389
		internal ExpandablePool<SerializedCacheItem> Pool;
	}
}
