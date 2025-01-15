using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020000A4 RID: 164
	internal sealed class LocalCacheStore : IDisposable
	{
		// Token: 0x060003CD RID: 973 RVA: 0x0001358C File Offset: 0x0001178C
		public LocalCacheStore(EvictionParametrs evictionParams)
		{
			bool flag = false;
			this.ObjectManager = ObjectManagerFactory.GetObjectManager(new OMCacheNodeProperties(null, -1, flag)
			{
				OType = ObjectType.CLRObjectRefType,
				ConsistencyType = ConsistencyType.EventualConsistency
			});
			this.ObjectManager.RegisterCallBack(OMCallBackType.PostDelete, new OMOperationCallBack(this.OmCacheItemDeletePostCallback));
			this.ObjectManager.RegisterCallBack(OMCallBackType.PostInternalDelete, new OMOperationCallBack(this.OmCacheItemDeletePostCallback));
			this.ObjectManager.RegisterCallBack(OMCallBackType.PostForcedDelete, new OMOperationCallBack(this.OmCacheItemDeletePostCallback));
			this.ObjectManager.RegisterCallBack(OMCallBackType.PostGetAndLock, new OMOperationCallBack(this.OmCacheItemGetAndLockPostCallback));
			this.ObjectManager.RegisterCallBack(OMCallBackType.PostRegionDelete, new OMOperationCallBack(this.OmRegionDeleteCallback));
			this.ObjectManager.RegisterCallBack(OMCallBackType.PostInternalPutAndUnlock, new OMOperationCallBack(this.OmCacheItemInternalPutAndUnlockPostCallback));
			this._OMRegionProps = new OMRegionProperties();
			this._OMRegionProps.ConsistencyType = ConsistencyType.EventualConsistency;
			this._OMRegionProps.OType = ObjectType.CLRObjectRefType;
			this._OMCacheProps = new OMNamedCacheProperties();
			this._OMCacheProps.OType = ObjectType.CLRObjectRefType;
			this._OMCacheProps.ConsistencyType = ConsistencyType.EventualConsistency;
			this.EvictionManager = new EvictionManager(this, evictionParams);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose("DistributedCache.LocalCacheStore", "Initialize: Instantiated ObjectManager");
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000136C9 File Offset: 0x000118C9
		internal LocalCacheStore()
			: this(new EvictionParametrs())
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000136D6 File Offset: 0x000118D6
		public void Dispose()
		{
			this.EvictionManager.Dispose();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000136E4 File Offset: 0x000118E4
		public void InitializeNewLocalCache(string cacheName, DataCacheLocalCacheInvalidationPolicy syncPolicy, DataCache cache)
		{
			this.ObjectManager.CreateNamedCache(cacheName, this._OMCacheProps);
			this.ObjectManager.CreateRegion(cacheName, "Default_Region_", this._OMRegionProps, null, null);
			if (syncPolicy == DataCacheLocalCacheInvalidationPolicy.NotificationBased)
			{
				try
				{
					cache.AddCacheLevelBulkCallback(new DataCacheBulkNotificationCallback(this.BulkNotificationCallback));
				}
				catch (Exception ex)
				{
					if (Provider.IsEnabled(TraceLevel.Warning))
					{
						EventLogWriter.WriteWarning("DistributedCache.LocalCacheStore", "InitializeNewLocalCache: Add callabck for the cache = {0} failed: Exception: {1}", new object[] { cacheName, ex });
					}
					this.ObjectManager.DeleteNamedCache(cacheName);
					throw;
				}
				cache.AddFailureNotificationCallback(new DataCacheFailureNotificationCallback(this.NotificationMissDelegate));
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00013790 File Offset: 0x00011990
		public object Get(string cacheName, string regionName, Key key, ref InternalCacheItemVersion version)
		{
			if (regionName == null || RegionNameProvider.IsSystemRegion(regionName))
			{
				regionName = "Default_Region_";
			}
			object obj = null;
			try
			{
				obj = this.ObjectManager.Get(cacheName, regionName, key, ref version);
			}
			catch (DataCacheException)
			{
			}
			return obj;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x000137D8 File Offset: 0x000119D8
		public void Remove(string cacheName, string regionName, Key key)
		{
			if (regionName == null || RegionNameProvider.IsSystemRegion(regionName))
			{
				regionName = "Default_Region_";
			}
			bool flag;
			do
			{
				flag = false;
				try
				{
					this.ObjectManager.ForceDelete(null, cacheName, regionName, key);
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<string, string, Key>("DistributedCache.LocalCacheStore", "Remove: key {0}/{1}/{2} removed from Local Cache", cacheName, regionName, key);
					}
				}
				catch (DataCacheException ex)
				{
					int errorCode = ex.ErrorCode;
					if (errorCode <= 2009)
					{
						if (errorCode == 2002)
						{
							goto IL_0080;
						}
						if (errorCode == 2009)
						{
							Thread.Sleep(0);
							flag = true;
							goto IL_0080;
						}
					}
					else if (errorCode == 3002 || errorCode == 3006)
					{
						goto IL_0080;
					}
					throw;
					IL_0080:;
				}
			}
			while (flag);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001387C File Offset: 0x00011A7C
		public void InternalPutAndUnlock(string cacheName, string regionName, Key key, object value, InternalCacheItemVersion version, TimeSpan ttl, DataCacheLockHandle handle)
		{
			if (regionName == null || RegionNameProvider.IsSystemRegion(regionName))
			{
				regionName = "Default_Region_";
			}
			bool flag;
			do
			{
				flag = false;
				try
				{
					this.ObjectManager.InternalPutAndUnlock(cacheName, regionName, key, value, version, Utility.AddTimeSpanToDateTime(ttl), null, handle);
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<string, string, Key>("DistributedCache.LocalCacheStore", "InternalPutAndUnlock: Key {0}/{1}/{2} put in local cache", cacheName, regionName, key);
					}
				}
				catch (DataCacheException ex)
				{
					int errorCode = ex.ErrorCode;
					if (errorCode != 2009)
					{
						if (errorCode == 3002)
						{
							this.CreateRegion(cacheName, regionName);
							flag = true;
						}
					}
					else
					{
						flag = true;
						Thread.Sleep(0);
					}
				}
			}
			while (flag);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00013918 File Offset: 0x00011B18
		public void CreateRegion(string cacheName, string regionName)
		{
			try
			{
				this.ObjectManager.CreateRegion(cacheName, regionName, this._OMRegionProps, null, null);
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<string, string>("DistributedCache.LocalCacheStore", "CreateRegion: RegionName {0}/{1} Created in Local Cache", cacheName, regionName);
				}
			}
			catch (DataCacheException ex)
			{
				if (ex.ErrorCode != 3003)
				{
					throw;
				}
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00013978 File Offset: 0x00011B78
		public void DeleteRegion(string cacheName, string regionName)
		{
			try
			{
				if (regionName == null || RegionNameProvider.IsSystemRegion(regionName))
				{
					regionName = "Default_Region_";
					this.ObjectManager.ClearRegion(null, cacheName, regionName);
				}
				else
				{
					this.ObjectManager.DropRegion(cacheName, regionName);
				}
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<string, string>("DistributedCache.LocalCacheStore", "DeleteRegion: RegionName {0}/{1} Deleted in Local Cache", cacheName, regionName);
				}
			}
			catch (DataCacheException ex)
			{
				if (Provider.IsEnabled(TraceLevel.Verbose))
				{
					EventLogWriter.WriteVerbose<string, string, int>("DistributedCache.LocalCacheStore", "DeleteRegion: Delete failed for RegionName {0}/{1} with code: {2}", cacheName, regionName, ex.ErrorCode);
				}
				if (ex.ErrorCode != 3002)
				{
					throw;
				}
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00013A14 File Offset: 0x00011C14
		public void ClearCache(string cacheName)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.LocalCacheStore", "Starting clear cache: {0}", cacheName);
			}
			string[] array = this.ObjectManager.ListAllRegions(cacheName);
			foreach (string text in array)
			{
				this.DeleteRegion(cacheName, text);
			}
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.LocalCacheStore", "Local cache is cleared for cache: {0}", cacheName);
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00013A7C File Offset: 0x00011C7C
		public DataCacheLockHandle GetAndLock(string cacheName, string regionName, Key key)
		{
			DataCacheLockHandle dataCacheLockHandle = null;
			if (regionName == null || RegionNameProvider.IsSystemRegion(regionName))
			{
				regionName = "Default_Region_";
			}
			bool flag;
			do
			{
				flag = false;
				try
				{
					TimeSpan timeSpan = TimeSpan.FromMinutes(1.0);
					this.ObjectManager.GetAndLock(null, cacheName, regionName, key, timeSpan, ref dataCacheLockHandle, true);
					if (Provider.IsEnabled(TraceLevel.Verbose))
					{
						EventLogWriter.WriteVerbose<string, string, Key>("DistributedCache.LocalCacheStore", "GetAndLock: Key {0}/{1}/{2} locked in local cache", cacheName, regionName, key);
					}
				}
				catch (DataCacheException ex)
				{
					int errorCode = ex.ErrorCode;
					if (errorCode == 3002)
					{
						this.CreateRegion(cacheName, regionName);
						flag = true;
					}
				}
			}
			while (flag);
			return dataCacheLockHandle;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00013B10 File Offset: 0x00011D10
		public void BulkNotificationCallback(string cacheName, IEnumerable<DataCacheOperationDescriptor> operations, DataCacheNotificationDescriptor nd)
		{
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<string>("DistributedCache.LocalCacheStore", "Local Cache Notification Delegate is Called  {0}", cacheName);
			}
			foreach (DataCacheOperationDescriptor dataCacheOperationDescriptor in operations)
			{
				DataCacheOperations operationType = dataCacheOperationDescriptor.OperationType;
				switch (operationType)
				{
				case DataCacheOperations.ReplaceItem:
				case DataCacheOperations.RemoveItem:
					this.Remove(cacheName, dataCacheOperationDescriptor.RegionName, new Key(dataCacheOperationDescriptor.Key));
					break;
				case DataCacheOperations.AddItem | DataCacheOperations.ReplaceItem:
					break;
				default:
					if (operationType == DataCacheOperations.RemoveRegion || operationType == DataCacheOperations.ClearRegion)
					{
						this.DeleteRegion(cacheName, dataCacheOperationDescriptor.RegionName);
					}
					break;
				}
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00013BB8 File Offset: 0x00011DB8
		private void NotificationMissDelegate(string cacheName, DataCacheNotificationDescriptor nd)
		{
			string[] array = this.ObjectManager.ListAllRegions(cacheName);
			for (int i = 0; i < array.Length; i++)
			{
				this.DeleteRegion(cacheName, array[i]);
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.LocalCacheStore", "NotificationMissDelegate: Local cache cleared because notification miss detected: {0}", new object[] { cacheName });
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00013C10 File Offset: 0x00011E10
		private bool OmCacheItemGetAndLockPostCallback(object oldcacheitem, object newcacheitem, object opstate)
		{
			AOMCacheItem aomcacheItem = oldcacheitem as AOMCacheItem;
			if (aomcacheItem.IsLockPlaceHolderObject)
			{
				this.UpdateLocalCacheStats(null, aomcacheItem);
			}
			return true;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00013C35 File Offset: 0x00011E35
		private bool OmCacheItemInternalPutAndUnlockPostCallback(object oldcacheitem, object newcacheitem, object opstate)
		{
			this.UpdateLocalCacheStats(oldcacheitem, newcacheitem);
			return true;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00013C40 File Offset: 0x00011E40
		private bool OmRegionDeleteCallback(object oldCacheItemObject, object newCacheItemObject, object state)
		{
			OMRegion omregion = oldCacheItemObject as OMRegion;
			Interlocked.Add(ref this._countLocalCacheItem, -omregion.Stats.Count);
			return true;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00013C70 File Offset: 0x00011E70
		private bool OmCacheItemDeletePostCallback(object oldCacheItemObject, object newCacheItemObject, object state)
		{
			AOMCacheItem aomcacheItem = (AOMCacheItem)oldCacheItemObject;
			string regionName = aomcacheItem.RegionName;
			string cacheName = aomcacheItem.CacheName;
			if (aomcacheItem.Region.Stats.Count == 1L)
			{
				try
				{
					if (!regionName.Equals("Default_Region_"))
					{
						this.ObjectManager.DropRegion(cacheName, regionName);
					}
				}
				catch (DataCacheException ex)
				{
					if (ex.ErrorCode != 3002)
					{
						throw;
					}
				}
			}
			if (!aomcacheItem.Region.IsDeleted)
			{
				this.UpdateLocalCacheStats(oldCacheItemObject, newCacheItemObject);
			}
			return true;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00013CFC File Offset: 0x00011EFC
		private void UpdateLocalCacheStats(object oldItem, object newItem)
		{
			int num = ((oldItem == null) ? 0 : 1);
			int num2 = ((newItem == null) ? 0 : 1);
			if (num == num2)
			{
				return;
			}
			Interlocked.Add(ref this._countLocalCacheItem, (long)(num2 - num));
			if (newItem == null)
			{
				Interlocked.Increment(ref this._countItemRemovedSinceCompaction);
			}
			this.EvictionManager.OnCacheItemCountChange();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00013D48 File Offset: 0x00011F48
		public long ActualItemCount()
		{
			long num = this.ObjectManager.ActualItemCount();
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DistributedCache.LocalCacheStore", "Item count {0} , Actual item count {1}", new object[] { this._countLocalCacheItem, num });
			}
			return num;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00013D98 File Offset: 0x00011F98
		internal IEnumerator<CacheRegionName> GetRegionNames()
		{
			string[] caches = this.ObjectManager.ListAllNamedCaches();
			CacheRegionName pair = default(CacheRegionName);
			string[] regions = null;
			int i = 0;
			while (i < caches.Length)
			{
				pair.CacheName = caches[i];
				try
				{
					regions = this.ObjectManager.ListAllRegions(caches[i]);
				}
				catch (DataCacheException ex)
				{
					if (ex.ErrorCode != 9)
					{
						throw;
					}
					goto IL_0101;
				}
				goto IL_00A6;
				IL_0101:
				i++;
				continue;
				IL_00A6:
				for (int j = 0; j < regions.Length; j++)
				{
					pair.RegionName = regions[j];
					yield return pair;
				}
				goto IL_0101;
			}
			yield break;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00013DB4 File Offset: 0x00011FB4
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00013DBC File Offset: 0x00011FBC
		public long CountItemRemovedSinceCompaction
		{
			get
			{
				return this._countItemRemovedSinceCompaction;
			}
			set
			{
				this._countItemRemovedSinceCompaction = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00013DC5 File Offset: 0x00011FC5
		internal OMCacheNodeStats OMStats
		{
			get
			{
				return this.ObjectManager.GetStats();
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00013DD2 File Offset: 0x00011FD2
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00013DDA File Offset: 0x00011FDA
		internal long CountLocalCacheItem
		{
			get
			{
				return this._countLocalCacheItem;
			}
			set
			{
				this._countLocalCacheItem = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00013DE3 File Offset: 0x00011FE3
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00013DEB File Offset: 0x00011FEB
		internal IObjectManager ObjectManager { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00013DF4 File Offset: 0x00011FF4
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00013DFC File Offset: 0x00011FFC
		internal EvictionManager EvictionManager { get; private set; }

		// Token: 0x040002F6 RID: 758
		private const string _localcacheComponentName = "DistributedCache.LocalCacheStore";

		// Token: 0x040002F7 RID: 759
		private OMRegionProperties _OMRegionProps;

		// Token: 0x040002F8 RID: 760
		private OMNamedCacheProperties _OMCacheProps;

		// Token: 0x040002F9 RID: 761
		private long _countLocalCacheItem;

		// Token: 0x040002FA RID: 762
		private long _countItemRemovedSinceCompaction;
	}
}
