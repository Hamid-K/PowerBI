using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000284 RID: 644
	[DataContract]
	internal sealed class OMNamedCache : IOMNamedCache
	{
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x000452F2 File Offset: 0x000434F2
		internal int EmptyNamedCacheSize
		{
			get
			{
				return this._emptyNCSize;
			}
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000452FA File Offset: 0x000434FA
		internal OMCacheStoreState GetStoreState()
		{
			return this._cacheStoreState;
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x00045302 File Offset: 0x00043502
		internal IMemoryManager MemoryManager
		{
			get
			{
				return this._om.MemoryManager;
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00045310 File Offset: 0x00043510
		public void ReCreateCacheStoreState(string cacheName, BackingStoreConfig bsConfig)
		{
			OMCacheStoreState omcacheStoreState = new OMCacheStoreState(cacheName, bsConfig, this._cacheStoreState);
			this._cacheStoreState = omcacheStoreState;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00045334 File Offset: 0x00043534
		private bool OMPostRegionCreate(object oldItem, object newitem, object opState)
		{
			bool flag;
			try
			{
				OMRegion omregion = newitem as OMRegion;
				if (omregion != null)
				{
					this.Cache.Add(omregion, opState);
				}
				flag = true;
			}
			catch (DataCacheException ex)
			{
				if (ex.ErrorCode == 2001)
				{
					throw OMNamedCache.GetException(3003);
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00045388 File Offset: 0x00043588
		private bool OMPreRegionDelete(object oldItem, object newItem, object opState)
		{
			if (oldItem != null)
			{
				OMRegion omregion = oldItem as OMRegion;
				if (omregion != null)
				{
					this.Cache.Delete(omregion, opState);
				}
				return true;
			}
			throw OMNamedCache.GetException(3002);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x000453BC File Offset: 0x000435BC
		private bool OMPreRegionClear(object oldItem, object newItem, object opState)
		{
			OMRegion omregion = newItem as OMRegion;
			if (omregion != null)
			{
				this.Cache.Upsert(omregion, opState);
			}
			return true;
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x000453E4 File Offset: 0x000435E4
		internal IContainerSchema PrepareSchema()
		{
			IContainerSchema containerSchema = DataManagerFactory.CreateContainerSchema();
			IStoreSchema storeSchema = DataManagerFactory.CreateStoreSchema();
			containerSchema.AddCallBack(DMCallBackType.PostAdd, new DMOperationCallBack(this.OMPostRegionCreate));
			containerSchema.AddCallBack(DMCallBackType.PreDelete, new DMOperationCallBack(this.OMPreRegionDelete));
			containerSchema.AddCallBack(DMCallBackType.PreUpsert, new DMOperationCallBack(this.OMPreRegionClear));
			containerSchema.BaseStoreSchema = storeSchema;
			return containerSchema;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00045440 File Offset: 0x00043640
		internal OMNamedCache(IDMContainer cache, string cacheName, OMNamedCacheProperties props, DMOperationCallBack[] prePostOps, OMCacheNodeStats nodeStats, ObjectManager OM)
		{
			this._props = props;
			this._cache = cache;
			this._cacheName = cacheName;
			this._om = OM;
			IContainerSchema containerSchema = this.PrepareSchema();
			IDataManager dataManager = DataManagerFactory.CreateDataManager(this._om.MemoryManager.GetDirectoryNodeFactory());
			this._regionTable = dataManager.CreateContainer(containerSchema);
			this._prePostOperations = prePostOps;
			this._stats = new OMNamedCacheStats(nodeStats, this._cacheName);
			this._emptyNCSize = OMNamedCache.GetSize(this._cacheName, this._props.OType);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x000454D1 File Offset: 0x000436D1
		internal OMNamedCache(IDMContainer cache, string cacheName, INamedCacheConfiguration config, ObjectType objectType, DMOperationCallBack[] prePostOps, OMCacheNodeStats nodeStats, ObjectManager OM)
			: this(cache, cacheName, OMNamedCache.GetOMNamedCacheProperties(config, objectType), prePostOps, nodeStats, OM)
		{
			this._cacheStoreState = new OMCacheStoreState(cacheName, config.BackingStore);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x000454FC File Offset: 0x000436FC
		private static OMNamedCacheProperties GetOMNamedCacheProperties(INamedCacheConfiguration config, ObjectType objectType)
		{
			return new OMNamedCacheProperties(config)
			{
				OType = objectType
			};
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00045518 File Offset: 0x00043718
		internal OMNamedCache(string cacheName)
		{
			this._cacheName = cacheName;
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00002061 File Offset: 0x00000261
		private OMNamedCache()
		{
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00045528 File Offset: 0x00043728
		private OMNamedCache(OMNamedCache namedCache)
		{
			this._cacheName = namedCache._cacheName;
			this._cache = namedCache._cache;
			this._props = namedCache._props;
			this._stats = new OMNamedCacheStats(namedCache.Stats.NodeStats, this._cacheName);
			this._om = namedCache._om;
			IContainerSchema schema = namedCache._regionTable.GetSchema();
			IDataManager dataManager = DataManagerFactory.CreateDataManager(this._om.MemoryManager.GetDirectoryNodeFactory());
			this._regionTable = dataManager.CreateContainer(schema);
			this._prePostOperations = namedCache._prePostOperations;
			this._emptyNCSize = OMNamedCache.GetSize(this._cacheName, this._props.OType);
			this._cacheStoreState = namedCache._cacheStoreState;
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000455EA File Offset: 0x000437EA
		private static int GetSize(string cacheName, ObjectType otype)
		{
			if (otype == ObjectType.CLRObjectRefType)
			{
				return 1;
			}
			return cacheName.Length;
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060016B9 RID: 5817 RVA: 0x000455F7 File Offset: 0x000437F7
		internal ICacheItemFactory CacheItemFactory
		{
			get
			{
				return this._om.CacheItemFactory;
			}
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000420EC File Offset: 0x000402EC
		private static DataCacheException GetException(int errorCode)
		{
			return new DataCacheException(ObjectManager.LogSource, errorCode, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, errorCode));
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00045604 File Offset: 0x00043804
		internal OMRegion GetRegion(string regionName)
		{
			return this._regionTable.Get(new CacheRegion(this._cacheName, regionName)) as OMRegion;
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00045622 File Offset: 0x00043822
		internal OMRegion GetRegion(AOMCacheItem item)
		{
			return this._regionTable.Get(new CacheRegion(item.CacheName, item.RegionName)) as OMRegion;
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060016BD RID: 5821 RVA: 0x00045645 File Offset: 0x00043845
		public int RegionCount
		{
			get
			{
				return this._stats.RegionCount;
			}
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00045654 File Offset: 0x00043854
		internal string[] ListAllRegions(int maxRegions)
		{
			if (this._stats.RegionCount < maxRegions)
			{
				maxRegions = this._stats.RegionCount;
			}
			List<string> list = new List<string>(maxRegions);
			EnumeratorState statelessEnumeratorState = this._regionTable.GetStatelessEnumeratorState();
			CountBasedScanner countBasedScanner = new CountBasedScanner();
			countBasedScanner.InvalidateOnChange = false;
			this._regionTable.GetBatch(countBasedScanner, statelessEnumeratorState);
			foreach (object obj in countBasedScanner.Batch)
			{
				if (list.Count >= maxRegions)
				{
					break;
				}
				OMRegion omregion = obj as OMRegion;
				list.Add(omregion.RegionName);
			}
			return list.ToArray();
		}

		// Token: 0x060016BF RID: 5823 RVA: 0x00045710 File Offset: 0x00043910
		internal long ActualItemCount()
		{
			long num = 0L;
			EnumeratorState statelessEnumeratorState = this._regionTable.GetStatelessEnumeratorState();
			CountBasedScanner countBasedScanner = new CountBasedScanner();
			countBasedScanner.InvalidateOnChange = false;
			this._regionTable.GetBatch(countBasedScanner, statelessEnumeratorState);
			foreach (object obj in countBasedScanner.Batch)
			{
				OMRegion omregion = obj as OMRegion;
				num += omregion.Stats.Count;
			}
			return num;
		}

		// Token: 0x060016C0 RID: 5824 RVA: 0x000457A0 File Offset: 0x000439A0
		internal bool ContainsRegion(string regionName)
		{
			return this._regionTable.Get(new CacheRegion(this._cacheName, regionName)) != null;
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x000457C0 File Offset: 0x000439C0
		internal OMRegion CreateRegion(IDataManager dm, string regionName, OMRegionProperties props, object state, object opState)
		{
			props.OType = this._props.OType;
			OMRegion omregion = new OMRegion(dm, regionName, this, props, this._prePostOperations, state);
			try
			{
				this._regionTable.Add(omregion, opState);
			}
			catch (DataCacheException ex)
			{
				if (ex.ErrorCode == 2001)
				{
					throw OMNamedCache.GetException(3003);
				}
				if (Provider.IsEnabled(TraceLevel.Warning))
				{
					EventLogWriter.WriteWarning(ObjectManager.LogSource, "Error creating region {0} for cache {1}. Exception: {2}", new object[] { regionName, this._cacheName, ex });
				}
				throw;
			}
			return omregion;
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x0004585C File Offset: 0x00043A5C
		internal long DeleteRegion(OMRegion region, object opState, bool syncDelete)
		{
			this._regionTable.Delete(region, opState);
			long num = OMNamedCache.DeleteAllItemFromRegion(region, syncDelete);
			region.MarkAsDeleted();
			return num;
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00045886 File Offset: 0x00043A86
		internal void ClearRegion(OMRegion region, object opState)
		{
			this._regionTable.Upsert(region.Clear(), opState);
			OMNamedCache.DeleteAllItemFromRegion(region, false);
			region.MarkAsDeleted();
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000458A9 File Offset: 0x00043AA9
		internal OMNamedCache Clear()
		{
			return new OMNamedCache(this);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x000458B4 File Offset: 0x00043AB4
		internal void DeleteAllRegions(object opState, bool syncDelete)
		{
			EnumeratorState statelessEnumeratorState = this._regionTable.GetStatelessEnumeratorState();
			CountBasedScanner countBasedScanner = new CountBasedScanner();
			this._regionTable.GetBatch(countBasedScanner, statelessEnumeratorState);
			foreach (object obj in countBasedScanner.Batch)
			{
				OMRegion omregion = obj as OMRegion;
				this.DeleteRegion(omregion, opState, syncDelete);
			}
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00045934 File Offset: 0x00043B34
		internal void DeleteAllRegions(object opState)
		{
			this.DeleteAllRegions(opState, false);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x000189CC File Offset: 0x00016BCC
		internal static IHashtableEnumerator Enumerate()
		{
			return null;
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0004593E File Offset: 0x00043B3E
		internal int DoCompaction()
		{
			return this._regionTable.DoCompaction();
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0004594C File Offset: 0x00043B4C
		internal static long DeleteAllItemFromRegion(object oldRegion, bool syncDelete)
		{
			if (syncDelete)
			{
				OMRegion omregion = oldRegion as OMRegion;
				OMNamedCacheStats stats = omregion.NamedCache.Stats;
				long size = omregion.Stats.Size;
				stats.DecrSize(size);
				stats.DecrCount(omregion.Stats.Count);
				return size;
			}
			ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(OMNamedCache.DeleteAllItemFromRegionWorker), oldRegion);
			return -1L;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x000459AC File Offset: 0x00043BAC
		internal static void DeleteAllItemFromRegionWorker(object oldRegion)
		{
			OMRegion omregion = oldRegion as OMRegion;
			IHashtableEnumerator hashtableEnumerator = omregion.Enumerate();
			while (hashtableEnumerator.MoveNext())
			{
				ADMCacheItem admcacheItem = hashtableEnumerator.Current;
				try
				{
					if (omregion.InternalDelete(admcacheItem, admcacheItem.Version, omregion.State) == null && Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo(ObjectManager.LogSource, "DeleteAllItemFromRegionWorker Internal delete {0} failed.", new object[] { admcacheItem });
					}
				}
				catch (DataCacheException ex)
				{
					if (Provider.IsEnabled(TraceLevel.Info))
					{
						EventLogWriter.WriteInfo(ObjectManager.LogSource, "DeleteAllItemFromRegionWorker Internal delete {0} caused exception {1}.", new object[] { admcacheItem, ex });
					}
				}
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060016CB RID: 5835 RVA: 0x00045A54 File Offset: 0x00043C54
		// (set) Token: 0x060016CC RID: 5836 RVA: 0x00045A5C File Offset: 0x00043C5C
		public OMNamedCacheStats Stats
		{
			get
			{
				return this._stats;
			}
			internal set
			{
				this._stats = value;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060016CD RID: 5837 RVA: 0x00045A65 File Offset: 0x00043C65
		public OMNamedCacheProperties Props
		{
			get
			{
				return this._props;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060016CE RID: 5838 RVA: 0x00045A6D File Offset: 0x00043C6D
		public string CacheName
		{
			get
			{
				return this._cacheName;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060016CF RID: 5839 RVA: 0x00045A75 File Offset: 0x00043C75
		public TimeSpan DefaultTTL
		{
			get
			{
				return this._props.DefaultTTL;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060016D0 RID: 5840 RVA: 0x00045A82 File Offset: 0x00043C82
		public ExpirationType ExpirationType
		{
			get
			{
				return this._props.ExpirationType;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060016D1 RID: 5841 RVA: 0x00045A8F File Offset: 0x00043C8F
		internal IDMContainer Cache
		{
			get
			{
				return this._cache;
			}
		}

		// Token: 0x04000CBE RID: 3262
		private OMCacheStoreState _cacheStoreState;

		// Token: 0x04000CBF RID: 3263
		private readonly ObjectManager _om;

		// Token: 0x04000CC0 RID: 3264
		private readonly IDMContainer _cache;

		// Token: 0x04000CC1 RID: 3265
		private OMNamedCacheProperties _props;

		// Token: 0x04000CC2 RID: 3266
		private OMNamedCacheStats _stats;

		// Token: 0x04000CC3 RID: 3267
		[DataMember]
		private string _cacheName;

		// Token: 0x04000CC4 RID: 3268
		private readonly DMOperationCallBack[] _prePostOperations;

		// Token: 0x04000CC5 RID: 3269
		private IDMContainer _regionTable;

		// Token: 0x04000CC6 RID: 3270
		private readonly int _emptyNCSize;
	}
}
