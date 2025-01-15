using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000014 RID: 20
	internal class DelegateStore
	{
		// Token: 0x06000097 RID: 151 RVA: 0x00004567 File Offset: 0x00002767
		public DelegateStore()
		{
			this._delegateStore = new Dictionary<string, CacheDelegateStore>();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004588 File Offset: 0x00002788
		public DataCacheNotificationDescriptor AddDelegate(string cacheName, string regionName, string key, int filterMask, DataCacheNotificationCallback cacheDelegate)
		{
			ItemNotificationDescriptor itemNotificationDescriptor = new ItemNotificationDescriptor(cacheName, regionName, key);
			itemNotificationDescriptor.DelegateId = Interlocked.Increment(ref this._lastReturnedId);
			DataCacheNotificationDescriptor dataCacheNotificationDescriptor;
			lock (this._lockObject)
			{
				CacheDelegateStore cacheDelegateStore;
				this._delegateStore.TryGetValue(cacheName, out cacheDelegateStore);
				if (cacheDelegateStore == null)
				{
					cacheDelegateStore = new CacheDelegateStore();
					this._delegateStore.Add(cacheName, cacheDelegateStore);
				}
				dataCacheNotificationDescriptor = cacheDelegateStore.AddDelegate(itemNotificationDescriptor, filterMask, cacheDelegate);
			}
			return dataCacheNotificationDescriptor;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004610 File Offset: 0x00002810
		public DataCacheNotificationDescriptor AddDelegate(string cacheName, string regionName, int filterMask, DataCacheNotificationCallback cacheDelegate)
		{
			RegionNotificationDescriptor regionNotificationDescriptor = new RegionNotificationDescriptor(cacheName, regionName);
			regionNotificationDescriptor.DelegateId = Interlocked.Increment(ref this._lastReturnedId);
			DataCacheNotificationDescriptor dataCacheNotificationDescriptor;
			lock (this._lockObject)
			{
				CacheDelegateStore cacheDelegateStore;
				this._delegateStore.TryGetValue(cacheName, out cacheDelegateStore);
				if (cacheDelegateStore == null)
				{
					cacheDelegateStore = new CacheDelegateStore();
					this._delegateStore.Add(cacheName, cacheDelegateStore);
				}
				dataCacheNotificationDescriptor = cacheDelegateStore.AddDelegate(regionNotificationDescriptor, filterMask, cacheDelegate);
			}
			return dataCacheNotificationDescriptor;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004698 File Offset: 0x00002898
		public DataCacheNotificationDescriptor AddDelegate(string cacheName, int filterMask, object cacheDelegate)
		{
			DataCacheNotificationDescriptor dataCacheNotificationDescriptor = new DataCacheNotificationDescriptor(cacheName);
			dataCacheNotificationDescriptor.DelegateId = Interlocked.Increment(ref this._lastReturnedId);
			DataCacheNotificationDescriptor dataCacheNotificationDescriptor2;
			lock (this._lockObject)
			{
				CacheDelegateStore cacheDelegateStore;
				this._delegateStore.TryGetValue(cacheName, out cacheDelegateStore);
				if (cacheDelegateStore == null)
				{
					cacheDelegateStore = new CacheDelegateStore();
					this._delegateStore.Add(cacheName, cacheDelegateStore);
				}
				dataCacheNotificationDescriptor2 = cacheDelegateStore.AddDelegate(dataCacheNotificationDescriptor, filterMask, cacheDelegate);
			}
			return dataCacheNotificationDescriptor2;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000471C File Offset: 0x0000291C
		public DataCacheNotificationDescriptor AddBulkDelegate(string cacheName, int filterMask, object cacheDelegate)
		{
			DataCacheNotificationDescriptor dataCacheNotificationDescriptor = new DataCacheNotificationDescriptor(cacheName);
			dataCacheNotificationDescriptor.DelegateId = Interlocked.Increment(ref this._lastReturnedId);
			DataCacheNotificationDescriptor dataCacheNotificationDescriptor2;
			lock (this._lockObject)
			{
				CacheDelegateStore cacheDelegateStore;
				this._delegateStore.TryGetValue(cacheName, out cacheDelegateStore);
				if (cacheDelegateStore == null)
				{
					cacheDelegateStore = new CacheDelegateStore();
					this._delegateStore.Add(cacheName, cacheDelegateStore);
				}
				dataCacheNotificationDescriptor2 = cacheDelegateStore.AddBulkDelegate(dataCacheNotificationDescriptor, filterMask, cacheDelegate);
			}
			return dataCacheNotificationDescriptor2;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000047A0 File Offset: 0x000029A0
		public PerDelegateInfo RemoveDelegate(DataCacheNotificationDescriptor nd)
		{
			PerDelegateInfo perDelegateInfo = null;
			PerDelegateInfo perDelegateInfo2;
			lock (this._lockObject)
			{
				CacheDelegateStore cacheDelegateStore;
				this._delegateStore.TryGetValue(nd.CacheName, out cacheDelegateStore);
				if (cacheDelegateStore != null)
				{
					perDelegateInfo = cacheDelegateStore.RemoveDelegate(nd);
					if (cacheDelegateStore.Count == 0)
					{
						this._delegateStore.Remove(nd.CacheName);
					}
				}
				perDelegateInfo2 = perDelegateInfo;
			}
			return perDelegateInfo2;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000481C File Offset: 0x00002A1C
		public Queue<PerDelegateInfo> GetInvocationList(DataCacheOperationDescriptor cacheEvent)
		{
			Queue<PerDelegateInfo> queue = new Queue<PerDelegateInfo>();
			lock (this._lockObject)
			{
				CacheDelegateStore cacheDelegateStore;
				this._delegateStore.TryGetValue(cacheEvent.CacheName, out cacheDelegateStore);
				if (cacheDelegateStore != null)
				{
					cacheDelegateStore.GetInvocationList(cacheEvent, queue);
				}
			}
			if (queue.Count == 0)
			{
				return null;
			}
			return queue;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004888 File Offset: 0x00002A88
		public Queue<PerDelegateInfo> GetBulkInvocationList(string cacheName)
		{
			Queue<PerDelegateInfo> queue = new Queue<PerDelegateInfo>();
			lock (this._lockObject)
			{
				CacheDelegateStore cacheDelegateStore;
				this._delegateStore.TryGetValue(cacheName, out cacheDelegateStore);
				if (cacheDelegateStore != null)
				{
					cacheDelegateStore.GetBulkInvocationList(63, queue);
				}
			}
			if (queue.Count == 0)
			{
				return null;
			}
			return queue;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000048F0 File Offset: 0x00002AF0
		public bool IsNonBulkNotificationsPresent(string cacheName)
		{
			lock (this._lockObject)
			{
				CacheDelegateStore cacheDelegateStore;
				this._delegateStore.TryGetValue(cacheName, out cacheDelegateStore);
				if (cacheDelegateStore != null)
				{
					return cacheDelegateStore.IsNonBulkNotificationsPresent();
				}
			}
			return false;
		}

		// Token: 0x04000067 RID: 103
		private Dictionary<string, CacheDelegateStore> _delegateStore;

		// Token: 0x04000068 RID: 104
		private object _lockObject = new object();

		// Token: 0x04000069 RID: 105
		private long _lastReturnedId;
	}
}
