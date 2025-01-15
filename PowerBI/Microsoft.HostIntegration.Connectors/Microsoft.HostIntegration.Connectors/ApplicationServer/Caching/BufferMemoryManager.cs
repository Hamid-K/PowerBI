using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002DD RID: 733
	internal class BufferMemoryManager : IMemoryManager
	{
		// Token: 0x06001B08 RID: 6920 RVA: 0x00051D44 File Offset: 0x0004FF44
		internal BufferMemoryManager(ServiceConfigurationManager config)
		{
			this.MemorySize = config.NodeProperties.Size * 1024L * 1024L;
			this.BufferSize = config.BufferSize;
			this.MinCacheItem = config.MinObjectPoolSize;
			this.MaxCacheItem = config.MaxObjectPoolSize;
			if (this.MaxCacheItem <= 0L)
			{
				long num;
				if (config.AverageCacheItemSizeInBytes <= 0L)
				{
					num = this.BufferSize;
				}
				else
				{
					num = config.AverageCacheItemSizeInBytes;
				}
				this.MaxCacheItem = this.MemorySize / num;
			}
			if (this.MinCacheItem <= 0L || this.MinCacheItem > this.MaxCacheItem)
			{
				this.MinCacheItem = this.MaxCacheItem / 3L;
			}
			this.MinDirectoryNode = (long)((double)this.MinCacheItem / 2.5) + 200L;
			this.MaxDirectoryNode = (long)((double)this.MaxCacheItem / 2.0) + 200L;
			this.MinWBItem = this.MaxCacheItem * (long)config.AdvancedProperties.MemoryPressureMonitorProperties.PercentItemInFinalizerQueue / 100L;
			this._cacheItemFactory = new SerializedCacheItemFactory(this.MinCacheItem, this.MaxCacheItem);
			this._directoryNodeFactory = new PoolableDirectoryNodeFactory(this.MinDirectoryNode, this.MaxDirectoryNode);
			this.Allocator = new MemoryAllocator(this.MemorySize, (int)this.BufferSize);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00051E9C File Offset: 0x0005009C
		internal BufferMemoryManager()
		{
			this.MemorySize = 1048576L;
			this.BufferSize = 1024L;
			this.MinCacheItem = 100L;
			this.MaxCacheItem = 100L;
			if (this.MaxCacheItem <= 0L)
			{
				this.MaxCacheItem = this.MemorySize / this.BufferSize;
			}
			if (this.MinCacheItem <= 0L || this.MinCacheItem > this.MaxCacheItem)
			{
				this.MinCacheItem = this.MaxCacheItem / 3L;
			}
			this._cacheItemFactory = new SerializedCacheItemFactory(this.MinCacheItem, this.MaxCacheItem);
			this.Allocator = new MemoryAllocator(this.MemorySize, (int)this.BufferSize);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00051F4C File Offset: 0x0005014C
		internal BufferMemoryManager(MemoryAllocator Rtable, SerializedCacheItemFactory cacheItemFactory, PoolableDirectoryNodeFactory dfactory, PoolableWriteBackItemFactory writeBackItemFactory)
		{
			this._cacheItemFactory = cacheItemFactory;
			this._directoryNodeFactory = dfactory;
			this._writeBackItemFactory = writeBackItemFactory;
			this.Allocator = Rtable;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x00051F71 File Offset: 0x00050171
		public ICacheItemFactory GetCacheItemFactory()
		{
			return this._cacheItemFactory;
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x00051F79 File Offset: 0x00050179
		public IDirectoryNodeFactory GetDirectoryNodeFactory()
		{
			return this._directoryNodeFactory;
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x00051F81 File Offset: 0x00050181
		public IWriteBackItemFactory GetWriteBackItemFactory()
		{
			return this._writeBackItemFactory;
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x00051F8C File Offset: 0x0005018C
		public object GetStats()
		{
			BufferMemoryManagerStats bufferMemoryManagerStats = new BufferMemoryManagerStats();
			bufferMemoryManagerStats.CurrentMemoryUsage = this.Allocator.MemoryUsage();
			bufferMemoryManagerStats.MaxMemoryCapacity = this.Allocator.MaxMemoryCapacity();
			bufferMemoryManagerStats.CacheItemPoolStats.AvailableObjects = this._cacheItemFactory.AvailableObjects;
			bufferMemoryManagerStats.CacheItemPoolStats.CurrentCapacity = this._cacheItemFactory.CurrentCapacity;
			bufferMemoryManagerStats.CacheItemPoolStats.MaxCapacity = this._cacheItemFactory.MaxCapacity;
			bufferMemoryManagerStats.DirectoryPoolStats.AvailableObjects = this._directoryNodeFactory.AvailableObjects;
			bufferMemoryManagerStats.DirectoryPoolStats.CurrentCapacity = this._directoryNodeFactory.CurrentCapacity;
			bufferMemoryManagerStats.DirectoryPoolStats.MaxCapacity = this._directoryNodeFactory.MaxCapacity;
			if (!this.IsWriteBackItemPoolAllocated)
			{
				bufferMemoryManagerStats.WriteBackPoolStats.AvailableObjects = 0L;
				bufferMemoryManagerStats.WriteBackPoolStats.CurrentCapacity = 0L;
				bufferMemoryManagerStats.WriteBackPoolStats.MaxCapacity = 1L;
			}
			else
			{
				bufferMemoryManagerStats.WriteBackPoolStats.AvailableObjects = this._writeBackItemFactory.AvailableObjects;
				bufferMemoryManagerStats.WriteBackPoolStats.CurrentCapacity = this._writeBackItemFactory.CurrentCapacity;
				bufferMemoryManagerStats.WriteBackPoolStats.MaxCapacity = this._writeBackItemFactory.MaxCapacity;
			}
			return bufferMemoryManagerStats;
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000520B9 File Offset: 0x000502B9
		public long FreeCacheItemCount
		{
			get
			{
				return this._cacheItemFactory.Pool.Count;
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x000520CB File Offset: 0x000502CB
		public long FreeRecordCount
		{
			get
			{
				return this.Allocator.FreeBufferCount();
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x000520D8 File Offset: 0x000502D8
		public bool IsWriteBackItemPoolAllocated
		{
			get
			{
				return this._writeBackItemFactory != null;
			}
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000520E6 File Offset: 0x000502E6
		internal void GrowDirectoryNodePool()
		{
			this._directoryNodeFactory.Pool.Grow();
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000520F9 File Offset: 0x000502F9
		internal void GrowCacheItemPool()
		{
			this._cacheItemFactory.Pool.Grow();
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0005210C File Offset: 0x0005030C
		internal void GrowWriteBackItemPool()
		{
			this._writeBackItemFactory.Pool.Grow();
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0005211F File Offset: 0x0005031F
		internal long GetAvailableMemory()
		{
			return this.Allocator.AvailableMemory();
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0005212C File Offset: 0x0005032C
		internal long GetMaxMemory()
		{
			return this.Allocator.MaxMemoryCapacity();
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x00052139 File Offset: 0x00050339
		internal long GetAvailableCacheItemCount()
		{
			return this._cacheItemFactory.AvailableObjects;
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x00052146 File Offset: 0x00050346
		internal long GetAllocatedCaceItemCount()
		{
			return this._cacheItemFactory.CurrentCapacity;
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x00052153 File Offset: 0x00050353
		internal long GetMaxCacheItemCapacityCount()
		{
			return this._cacheItemFactory.MaxCapacity;
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x00052160 File Offset: 0x00050360
		internal long GetAvailableDirectoryCount()
		{
			return this._directoryNodeFactory.AvailableObjects;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0005216D File Offset: 0x0005036D
		internal long GetAllocatedDirectoryCount()
		{
			return this._directoryNodeFactory.CurrentCapacity;
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0005217A File Offset: 0x0005037A
		internal long GetMaxDirectoryCapacityCount()
		{
			return this._directoryNodeFactory.MaxCapacity;
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x00052187 File Offset: 0x00050387
		internal void AllocateWriteBackItemPool()
		{
			this._writeBackItemFactory = new PoolableWriteBackItemFactory(this.MinWBItem, this.MaxCacheItem);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000521A0 File Offset: 0x000503A0
		internal long GetAvailableWBItemCount()
		{
			if (!this.IsWriteBackItemPoolAllocated)
			{
				return 0L;
			}
			return this._writeBackItemFactory.AvailableObjects;
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000521B8 File Offset: 0x000503B8
		internal long GetAllocatedWBItemCount()
		{
			if (!this.IsWriteBackItemPoolAllocated)
			{
				return 0L;
			}
			return this._writeBackItemFactory.CurrentCapacity;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000521D0 File Offset: 0x000503D0
		internal long GetMaxWBItemCapacityCount()
		{
			if (!this.IsWriteBackItemPoolAllocated)
			{
				return 0L;
			}
			return this._writeBackItemFactory.MaxCapacity;
		}

		// Token: 0x04000E56 RID: 3670
		private SerializedCacheItemFactory _cacheItemFactory;

		// Token: 0x04000E57 RID: 3671
		private PoolableDirectoryNodeFactory _directoryNodeFactory;

		// Token: 0x04000E58 RID: 3672
		private PoolableWriteBackItemFactory _writeBackItemFactory;

		// Token: 0x04000E59 RID: 3673
		private readonly long MemorySize;

		// Token: 0x04000E5A RID: 3674
		private readonly long BufferSize;

		// Token: 0x04000E5B RID: 3675
		private readonly long MinCacheItem;

		// Token: 0x04000E5C RID: 3676
		private readonly long MaxCacheItem;

		// Token: 0x04000E5D RID: 3677
		private readonly long MinDirectoryNode;

		// Token: 0x04000E5E RID: 3678
		private readonly long MaxDirectoryNode;

		// Token: 0x04000E5F RID: 3679
		private readonly long MinWBItem;

		// Token: 0x04000E60 RID: 3680
		internal readonly MemoryAllocator Allocator;
	}
}
