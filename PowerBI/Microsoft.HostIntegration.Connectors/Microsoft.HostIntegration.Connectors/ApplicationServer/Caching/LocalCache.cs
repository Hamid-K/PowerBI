using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000013 RID: 19
	internal class LocalCache
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008C RID: 140 RVA: 0x000043DB File Offset: 0x000025DB
		private string CacheName
		{
			get
			{
				return this.parent.Name;
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000043E8 File Offset: 0x000025E8
		public LocalCache(DataCache cache, DataCacheFactory factory)
		{
			this.parent = cache;
			this.localCacheStore = factory.LocalCacheInstance;
			DataCacheLocalCacheProperties localCacheProperties = factory.Configuration.LocalCacheProperties;
			this.defaultTTL = localCacheProperties.DefaultTimeout;
			this.SetSyncPolicy(localCacheProperties.InvalidationPolicy);
			this.localCacheStore.InitializeNewLocalCache(this.CacheName, this.syncPolicy, this.parent);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000444F File Offset: 0x0000264F
		private void SetSyncPolicy(DataCacheLocalCacheInvalidationPolicy policy)
		{
			if (policy == DataCacheLocalCacheInvalidationPolicy.NotificationBased && !this.parent.Configuration.Notification.IsEnabled)
			{
				throw DataCache.NewException(15);
			}
			this.syncPolicy = policy;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000447A File Offset: 0x0000267A
		public DataCacheLockHandle Lock(string regionName, Key key)
		{
			return this.localCacheStore.GetAndLock(this.CacheName, regionName, key);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000448F File Offset: 0x0000268F
		public void Unlock(string regionName, Key key)
		{
			this.localCacheStore.Remove(this.CacheName, regionName, key);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000044A4 File Offset: 0x000026A4
		public void PutAndUnlock(string region, Key key, object value, InternalCacheItemVersion internalVersion, TimeSpan ttl, TimeSpan roundTripTime, DataCacheLockHandle dataCacheLockHandle)
		{
			TimeSpan timeSpan = this.CalculateTtlForGetOps(ttl, roundTripTime);
			if (timeSpan > TimeSpan.Zero)
			{
				this.localCacheStore.InternalPutAndUnlock(this.CacheName, region, key, value, internalVersion, timeSpan, dataCacheLockHandle);
				return;
			}
			this.Unlock(region, key);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000044EB File Offset: 0x000026EB
		public object Get(string region, Key key, ref InternalCacheItemVersion version)
		{
			return this.localCacheStore.Get(this.CacheName, region, key, ref version);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004501 File Offset: 0x00002701
		public void Clear()
		{
			this.localCacheStore.ClearCache(this.CacheName);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004514 File Offset: 0x00002714
		public void DeleteRegion(string region)
		{
			this.localCacheStore.DeleteRegion(this.CacheName, region);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000448F File Offset: 0x0000268F
		public void Remove(string region, Key key)
		{
			this.localCacheStore.Remove(this.CacheName, region, key);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004528 File Offset: 0x00002728
		internal TimeSpan CalculateTtlForGetOps(TimeSpan opTtl, TimeSpan roundTripTime)
		{
			if (roundTripTime < TimeSpan.Zero)
			{
				roundTripTime = TimeSpan.Zero;
			}
			TimeSpan timeSpan = opTtl - roundTripTime;
			if (!(timeSpan > this.defaultTTL))
			{
				return timeSpan;
			}
			return this.defaultTTL;
		}

		// Token: 0x04000062 RID: 98
		private const string Source = "DistributedCache.LocalCache";

		// Token: 0x04000063 RID: 99
		private readonly LocalCacheStore localCacheStore;

		// Token: 0x04000064 RID: 100
		private readonly DataCache parent;

		// Token: 0x04000065 RID: 101
		private readonly TimeSpan defaultTTL;

		// Token: 0x04000066 RID: 102
		private DataCacheLocalCacheInvalidationPolicy syncPolicy;
	}
}
