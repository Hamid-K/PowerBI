using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000288 RID: 648
	[DataContract]
	internal sealed class OMRegion : ADMCacheItem, IOMRegion
	{
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x00046923 File Offset: 0x00044B23
		internal int EmptyRegionSize
		{
			get
			{
				return this._emptyRegionSize;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x0004692B File Offset: 0x00044B2B
		public object State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00046934 File Offset: 0x00044B34
		private static List<object[]> MyKeyGeneratorForTags(object item)
		{
			if (item == null)
			{
				return null;
			}
			AOMCacheItem aomcacheItem = (AOMCacheItem)item;
			Key key = aomcacheItem.Key;
			object[] tags = aomcacheItem.Tags;
			if (tags == null)
			{
				return null;
			}
			int num = tags.Length;
			List<object[]> list = new List<object[]>(num);
			for (int i = 0; i < num; i++)
			{
				if (tags[i] != null)
				{
					object[] array = new object[]
					{
						tags[i],
						key
					};
					list.Add(array);
				}
			}
			return list;
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x000469A8 File Offset: 0x00044BA8
		private IContainerSchema PrepareSchema(OMRegionProperties props)
		{
			IContainerSchema containerSchema = DataManagerFactory.CreateContainerSchema();
			IStoreSchema storeSchema = DataManagerFactory.CreateStoreSchema();
			IIndexSchema indexSchema = DataManagerFactory.CreateIndexSchema();
			IIndexStoreSchema indexStoreSchema = DataManagerFactory.CreateIndexStoreSchema();
			if (props.ConsistencyType == ConsistencyType.EventualConsistency)
			{
				containerSchema.CommitType = CommitType.ImmediateCommit;
			}
			else
			{
				containerSchema.CommitType = CommitType.DeferredCommit;
			}
			containerSchema.ExpirationType = props.ExpirationType;
			containerSchema.BaseStoreSchema = storeSchema;
			indexSchema.TagExtractorDelegate = OMRegion._keyGeneratorForTags;
			indexStoreSchema.Level = props.IndexLevel;
			indexSchema.IndexStoreSchema = indexStoreSchema;
			containerSchema.AddCallBack(DMCallBackType.PreAdd, this._omPrePostOperations[0]);
			containerSchema.AddCallBack(DMCallBackType.PostAdd, this._omPrePostOperations[1]);
			containerSchema.AddCallBack(DMCallBackType.PreUpsert, this._omPrePostOperations[2]);
			containerSchema.AddCallBack(DMCallBackType.PostUpsert, this._omPrePostOperations[3]);
			containerSchema.AddCallBack(DMCallBackType.PreConcatenate, this._omPrePostOperations[40]);
			containerSchema.AddCallBack(DMCallBackType.PreIncrementDecrement, this._omPrePostOperations[38]);
			containerSchema.AddCallBack(DMCallBackType.PostIncrementDecrement, this._omPrePostOperations[39]);
			containerSchema.AddCallBack(DMCallBackType.PostConcatenate, this._omPrePostOperations[41]);
			containerSchema.AddCallBack(DMCallBackType.PreDelete, this._omPrePostOperations[4]);
			containerSchema.AddCallBack(DMCallBackType.PostDelete, this._omPrePostOperations[5]);
			containerSchema.AddCallBack(DMCallBackType.PreGetAndLock, this._omPrePostOperations[18]);
			containerSchema.AddCallBack(DMCallBackType.PostGetAndLock, this._omPrePostOperations[19]);
			containerSchema.AddCallBack(DMCallBackType.PrePutAndUnlock, this._omPrePostOperations[20]);
			containerSchema.AddCallBack(DMCallBackType.PostPutAndUnlock, this._omPrePostOperations[21]);
			containerSchema.AddCallBack(DMCallBackType.PreUnlock, this._omPrePostOperations[24]);
			containerSchema.AddCallBack(DMCallBackType.PostUnlock, this._omPrePostOperations[25]);
			containerSchema.AddCallBack(DMCallBackType.PreResetTimeOut, this._omPrePostOperations[22]);
			containerSchema.AddCallBack(DMCallBackType.PostResetTimeOut, this._omPrePostOperations[23]);
			containerSchema.AddCallBack(DMCallBackType.PostReadThroughLock, this._omPrePostOperations[33]);
			containerSchema.AddCallBack(DMCallBackType.PostReadThroughUnlock, this._omPrePostOperations[34]);
			containerSchema.AddCallBack(DMCallBackType.PostReadThroughPutAndUnlock, this._omPrePostOperations[35]);
			containerSchema.AddCallBack(DMCallBackType.PostForcedUpsert, this._omPrePostOperations[26]);
			containerSchema.AddCallBack(DMCallBackType.PostInternalDelete, this._omPrePostOperations[27]);
			containerSchema.AddCallBack(DMCallBackType.PostForcedDelete, this._omPrePostOperations[28]);
			containerSchema.AddCallBack(DMCallBackType.PostInternalLockUpdate, this._omPrePostOperations[31]);
			containerSchema.AddCallBack(DMCallBackType.PostForcedUnlock, this._omPrePostOperations[32]);
			containerSchema.AddCallBack(DMCallBackType.PreInternalUpsert, this._omPrePostOperations[29]);
			containerSchema.AddCallBack(DMCallBackType.PostInternalUpsert, this._omPrePostOperations[30]);
			containerSchema.AddCallBack(DMCallBackType.PreInternalPutAndUnlock, this._omPrePostOperations[36]);
			containerSchema.AddCallBack(DMCallBackType.PostInternalPutAndUnlock, this._omPrePostOperations[37]);
			containerSchema.AddCallBack(DMCallBackType.PostCommitDelete, this._omPrePostOperations[27]);
			containerSchema.AddIndex(indexSchema);
			containerSchema.BaseStoreSchema = storeSchema;
			return containerSchema;
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x00046C25 File Offset: 0x00044E25
		public IMemoryManager MemoryManager
		{
			get
			{
				return this._namedCache.MemoryManager;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x00046C32 File Offset: 0x00044E32
		// (set) Token: 0x06001741 RID: 5953 RVA: 0x00046C3A File Offset: 0x00044E3A
		public OMRegionStats Stats { get; internal set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00046C43 File Offset: 0x00044E43
		public ConsistencyType ConsistencyType
		{
			get
			{
				return this._props.ConsistencyType;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x00046C50 File Offset: 0x00044E50
		public bool IsEvictable
		{
			get
			{
				return this._props.IsEvictable;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x00046C5D File Offset: 0x00044E5D
		public bool IsExpirableItemsFound
		{
			get
			{
				return this._props.IsExpirableItemsFound;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x00046C6A File Offset: 0x00044E6A
		public ExpirationType ExpirationType
		{
			get
			{
				return this._props.ExpirationType;
			}
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x00046C77 File Offset: 0x00044E77
		internal OMRegion(string regionName, string namedCache)
		{
			this._namedCache = new OMNamedCache(namedCache);
			this._regionName = regionName;
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00046C94 File Offset: 0x00044E94
		internal OMRegion(OMRegion region)
			: base(InternalCacheItemVersion.Null, region.TimeToLive, region.ExtensionTimeout)
		{
			this._key = region._key;
			this._regionName = region.RegionName;
			this._dm = region._dm;
			this._namedCache = region._namedCache;
			this._props = region._props;
			this._omPrePostOperations = region._omPrePostOperations;
			this._regionSchema = region._regionSchema;
			this._region = this._dm.CreateContainer(this._regionSchema);
			this._region.RegisterLockPlaceHolderObject(new GetLockPlaceHolderObject(this.GetCacheItem));
			this.Stats = new OMRegionStats(region.NamedCache.Stats);
			this._state = region._state;
			this._emptyRegionSize = OMRegion.GetSize(this._regionName, this._props.OType, this._key.ToString());
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00046D84 File Offset: 0x00044F84
		internal OMRegion(IDataManager dm, string regionName, OMNamedCache namedCache, OMRegionProperties props, DMOperationCallBack[] prePostOPs, object state)
			: base(InternalCacheItemVersion.Null, long.MaxValue, 0L)
		{
			this._key = new CacheRegion(namedCache.CacheName, regionName);
			this._dm = dm;
			this._namedCache = namedCache;
			this._regionName = regionName;
			if (props != null)
			{
				this._props = new OMRegionProperties(props);
			}
			else
			{
				this._props = new OMRegionProperties();
			}
			if (prePostOPs != null)
			{
				this._omPrePostOperations = prePostOPs;
			}
			this._regionSchema = this.PrepareSchema(props);
			this._region = dm.CreateContainer(this._regionSchema);
			this._region.RegisterLockPlaceHolderObject(new GetLockPlaceHolderObject(this.GetCacheItem));
			this.Stats = new OMRegionStats(namedCache.Stats);
			this._state = state;
			this._emptyRegionSize = OMRegion.GetSize(regionName, props.OType, this.ToString());
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00046E64 File Offset: 0x00045064
		private ADMCacheItem GetCacheItem(object key)
		{
			return this.CacheItemFactory.GetCacheItem(this, (Key)OMRegion.GetKey(key));
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00046E8A File Offset: 0x0004508A
		private static object GetKey(object key)
		{
			if (key is Key || key is ADMCacheItem)
			{
				return key;
			}
			return new Key(key.ToString());
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00046EA9 File Offset: 0x000450A9
		internal ICacheItemFactory CacheItemFactory
		{
			get
			{
				return this._namedCache.CacheItemFactory;
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00046EB6 File Offset: 0x000450B6
		private static int GetSize(string regionName, ObjectType oType, string key)
		{
			if (oType != ObjectType.CLRObjectRefType)
			{
				return regionName.Length + key.Length;
			}
			return 1;
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600174D RID: 5965 RVA: 0x00046ECA File Offset: 0x000450CA
		internal OMNamedCache NamedCache
		{
			get
			{
				return this._namedCache;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x00046ED2 File Offset: 0x000450D2
		// (set) Token: 0x0600174F RID: 5967 RVA: 0x00046EDA File Offset: 0x000450DA
		internal int RegionListIndex { get; set; }

		// Token: 0x06001750 RID: 5968 RVA: 0x00046EE4 File Offset: 0x000450E4
		internal AOMCacheItem Get(object key)
		{
			AOMCacheItem aomcacheItem = (AOMCacheItem)this._region.Get(OMRegion.GetKey(key));
			if (aomcacheItem == null)
			{
				return null;
			}
			if (!this.IsDeleted)
			{
				return aomcacheItem;
			}
			return null;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x00046F18 File Offset: 0x00045118
		internal List<KeyValuePair<string, object>> GetByTag(object[] tagsKey)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			IEnumerator enumerator = this._region.Find(tagsKey);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				AOMCacheItem aomcacheItem = (AOMCacheItem)obj;
				if (!this.IsDeleted && !aomcacheItem.IsItemExpired())
				{
					KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(aomcacheItem.Key.ToString(), aomcacheItem.Value);
					list.Add(keyValuePair);
				}
			}
			return list;
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x00046F80 File Offset: 0x00045180
		internal List<KeyValuePair<string, object>> GetByTagsByIntersection(List<object[]> list)
		{
			List<KeyValuePair<string, object>> list2 = new List<KeyValuePair<string, object>>();
			IEnumerator enumerator = this._region.FindIntersection(list);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				AOMCacheItem aomcacheItem = (AOMCacheItem)obj;
				if (!this.IsDeleted && !aomcacheItem.IsItemExpired())
				{
					KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(aomcacheItem.Key.ToString(), aomcacheItem.Value);
					list2.Add(keyValuePair);
				}
			}
			return list2;
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x00046FE8 File Offset: 0x000451E8
		internal List<KeyValuePair<string, object>> GetByTagsByUnion(List<object[]> list)
		{
			List<KeyValuePair<string, object>> list2 = new List<KeyValuePair<string, object>>();
			IEnumerator enumerator = this._region.FindUnion(list);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				AOMCacheItem aomcacheItem = (AOMCacheItem)obj;
				if (!this.IsDeleted && !aomcacheItem.IsItemExpired())
				{
					KeyValuePair<string, object> keyValuePair = new KeyValuePair<string, object>(aomcacheItem.Key.ToString(), aomcacheItem.Value);
					list2.Add(keyValuePair);
				}
			}
			return list2;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x00047050 File Offset: 0x00045250
		internal EnumeratorState GetStatelessEnumeratorState()
		{
			return this._region.GetStatelessEnumeratorState();
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x00047060 File Offset: 0x00045260
		internal EnumeratorState FindWithStatelessEnumerator(DataCacheTag tag)
		{
			object[] array = new object[this._props.IndexLevel - 1];
			for (int i = 0; i < this._props.IndexLevel - 1; i++)
			{
				array[i] = tag;
			}
			return this._region.FindWithStatelessEnumerator(array);
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x000470A8 File Offset: 0x000452A8
		internal EnumeratorState FindUnionAllWithStatelessEnumerator(DataCacheTag[] tags)
		{
			List<object[]> list = new List<object[]>(tags.Length);
			for (int i = 0; i < tags.Length; i++)
			{
				object[] array = new object[this._props.IndexLevel - 1];
				for (int j = 0; j < this._props.IndexLevel - 1; j++)
				{
					array[j] = tags[i];
				}
				list.Add(array);
			}
			return this._region.FindUnionAllWithStatelessEnumerator(list);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x00047110 File Offset: 0x00045310
		internal bool GetBatchFromRegion(IScanner scanner, EnumeratorState enumeratorState)
		{
			return this._region.GetBatch(scanner, enumeratorState);
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x00047120 File Offset: 0x00045320
		internal AOMCacheItem Remove(object key, InternalCacheItemVersion ver, object opState)
		{
			key = OMRegion.GetKey(key);
			this.Stats.IncrDelReqs();
			if (ver == InternalCacheItemVersion.Null)
			{
				return (AOMCacheItem)this._region.Delete(key, opState);
			}
			return (AOMCacheItem)this._region.Delete(key, ver, opState);
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x00047173 File Offset: 0x00045373
		internal AOMCacheItem Remove(object key, DataCacheLockHandle lockHandle, object opState)
		{
			key = OMRegion.GetKey(key);
			this.Stats.IncrDelReqs();
			return (AOMCacheItem)this._region.Delete(key, lockHandle, opState);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0004719B File Offset: 0x0004539B
		internal AOMCacheItem InternalDelete(object key, InternalCacheItemVersion ver, object opState)
		{
			key = OMRegion.GetKey(key);
			return (AOMCacheItem)this._region.InternalDelete(key, ver, opState);
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x000471B8 File Offset: 0x000453B8
		internal AOMCacheItem Put(object key, object value, DateTime ttl, object[] tags, ProtocolType protocolType, ref InternalCacheItemVersion version, object opState)
		{
			key = OMRegion.GetKey(key);
			this.Stats.IncrUpsertReqs();
			AOMCacheItem cacheItem = this.CacheItemFactory.GetCacheItem(this, (Key)key, value, this.ValidateAndUpdateTTL(ttl), this.GetExtensionTimeout(ttl), tags, this._props.OType);
			AOMCacheItem aomcacheItem;
			if (version == InternalCacheItemVersion.Null)
			{
				aomcacheItem = (AOMCacheItem)this._region.Upsert(cacheItem, opState);
			}
			else
			{
				aomcacheItem = (AOMCacheItem)this._region.Upsert(cacheItem, version, protocolType, opState);
			}
			version = cacheItem.Version;
			return aomcacheItem;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00047260 File Offset: 0x00045460
		internal AOMCacheItem Replace(object key, object value, DateTime ttl, object[] tags, ref InternalCacheItemVersion version, object opState)
		{
			key = OMRegion.GetKey(key);
			this.Stats.IncrUpsertReqs();
			AOMCacheItem cacheItem = this.CacheItemFactory.GetCacheItem(this, (Key)key, value, this.ValidateAndUpdateTTL(ttl), this.GetExtensionTimeout(ttl), tags, this._props.OType);
			AOMCacheItem aomcacheItem = (AOMCacheItem)this._region.Replace(cacheItem, version, opState);
			version = cacheItem.Version;
			return aomcacheItem;
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x000472D8 File Offset: 0x000454D8
		internal AOMCacheItem IncrementDecrement(object key, object value, object initialValue, object opState, DateTime ttl, SerializationCategory serializationCategory, InternalCacheItemVersion version)
		{
			this.Stats.IncrUpsertReqs();
			AOMCacheItem cacheItem = this.CacheItemFactory.GetCacheItem(this, (Key)key);
			cacheItem.TimeToLive = this.ValidateAndUpdateTTL(ttl);
			cacheItem.ExtensionTimeout = this.GetExtensionTimeout(ttl);
			return (AOMCacheItem)this._region.IncrementDecrement(key, cacheItem, value, initialValue, serializationCategory, version, opState);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0004733C File Offset: 0x0004553C
		internal AOMCacheItem Concatenate(object key, object value, bool isAppend, object opState, DateTime ttl, SerializationCategory serializationCategory, InternalCacheItemVersion version)
		{
			this.Stats.IncrUpsertReqs();
			AOMCacheItem cacheItem = this.CacheItemFactory.GetCacheItem(this, (Key)key);
			cacheItem.TimeToLive = this.ValidateAndUpdateTTL(ttl);
			cacheItem.ExtensionTimeout = this.GetExtensionTimeout(ttl);
			return (AOMCacheItem)this._region.Concatenate(key, cacheItem, value, isAppend, serializationCategory, version, opState);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0004739F File Offset: 0x0004559F
		private void UpdateIsExpirable(DateTime ttl)
		{
			if (ttl != DateTime.MaxValue)
			{
				this._props.IsExpirableItemsFound = true;
			}
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000473BC File Offset: 0x000455BC
		internal long ValidateAndUpdateTTL(DateTime ttl)
		{
			this.UpdateIsExpirable(ttl);
			if (ttl == DateTime.MinValue)
			{
				TimeSpan defaultTTL = this.DefaultTTL;
				return Utility.AddTimeSpanToCurrentCounter(defaultTTL);
			}
			return Utility.GetTimeStampCounterFromDateTime(ttl);
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x000473F4 File Offset: 0x000455F4
		internal long GetExtensionTimeout(DateTime ttl)
		{
			if (ExpirationType.SlidingExpiration != this.ExpirationType)
			{
				return 0L;
			}
			if (ttl == DateTime.MinValue)
			{
				TimeSpan defaultTTL = this.DefaultTTL;
				return Utility.ConvertTimeSpanToCurrentCounter(defaultTTL);
			}
			return Utility.ConvertToTimeStampCounterFromDateTime(ttl);
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00047430 File Offset: 0x00045630
		internal AOMCacheItem Add(object key, object value, DateTime ttl, object[] tags, object opState)
		{
			key = OMRegion.GetKey(key);
			this.Stats.IncrAddReqs();
			AOMCacheItem cacheItem = this.CacheItemFactory.GetCacheItem(this, (Key)key, value, this.ValidateAndUpdateTTL(ttl), this.GetExtensionTimeout(ttl), tags, this._props.OType);
			this._region.Add(cacheItem, opState);
			return cacheItem;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00047490 File Offset: 0x00045690
		internal OMRegion Clear()
		{
			return new OMRegion(this);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x000474A5 File Offset: 0x000456A5
		internal IHashtableEnumerator Enumerate()
		{
			return this._region.Enumerate();
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x000474B2 File Offset: 0x000456B2
		internal AOMCacheItem Commit(object key, AOMCacheItem item)
		{
			key = OMRegion.GetKey(key);
			return (AOMCacheItem)this._region.Commit(key, item);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000474D0 File Offset: 0x000456D0
		internal AOMCacheItem GetAndLock(object key, TimeSpan lockTimeOut, bool lockKey, object opState)
		{
			key = OMRegion.GetKey(key);
			ADMCacheItem andLock = this._region.GetAndLock(key, lockTimeOut, lockKey, opState);
			if (lockKey && andLock.IsLockPlaceHolderObject)
			{
				this.UpdateIsExpirable(Utility.AddTimeSpanToDateTime(lockTimeOut));
			}
			return (AOMCacheItem)andLock;
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00047514 File Offset: 0x00045714
		internal AOMCacheItem PutAndUnlock(object key, object value, DateTime timeToLive, object[] tags, DataCacheLockHandle lHandle, object opState)
		{
			key = OMRegion.GetKey(key);
			this.Stats.IncrUpsertReqs();
			AOMCacheItem cacheItem = this.CacheItemFactory.GetCacheItem(this, (Key)key, value, this.ValidateAndUpdateTTL(timeToLive), this.GetExtensionTimeout(timeToLive), tags, this._props.OType);
			return (AOMCacheItem)this._region.PutAndUnlock(cacheItem, lHandle, opState);
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00047578 File Offset: 0x00045778
		internal bool Unlock(object key, DataCacheLockHandle lHandle, DateTime ttl, object opState)
		{
			key = OMRegion.GetKey(key);
			return this._region.Unlock(key, lHandle, ttl, opState);
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00047592 File Offset: 0x00045792
		internal bool ResetTimeOut(object key, DateTime timeOut, object opState)
		{
			key = OMRegion.GetKey(key);
			return this._region.ResetTimeOut(key, timeOut, opState);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000475AA File Offset: 0x000457AA
		internal AOMCacheItem ReadThroughLock(object key, TimeSpan lockTimeOut, object opState)
		{
			key = OMRegion.GetKey(key);
			return (AOMCacheItem)this._region.ReadThroughLock(key, lockTimeOut, opState);
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000475C7 File Offset: 0x000457C7
		internal void ReadThroughUnlock(object key, DataCacheLockHandle lockHandle, object opState)
		{
			key = OMRegion.GetKey(key);
			this._region.ReadThroughUnlock(key, lockHandle, opState);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x000475E0 File Offset: 0x000457E0
		internal AOMCacheItem ReadThroughPutAndLock(object key, object value, DateTime timeToLive, object[] tags, DataCacheLockHandle lockHandle, object opState)
		{
			key = OMRegion.GetKey(key);
			AOMCacheItem cacheItem = this.CacheItemFactory.GetCacheItem(this, (Key)key, value, this.ValidateAndUpdateTTL(timeToLive), this.GetExtensionTimeout(timeToLive), tags, this._props.OType);
			return (AOMCacheItem)this._region.ReadThroughPutAndUnlock(cacheItem, lockHandle, opState);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0004763C File Offset: 0x0004583C
		internal void ForceUpsert(object key, object value, InternalCacheItemVersion ver, DateTime ttl, DataCacheTag[] tags, object opState)
		{
			key = OMRegion.GetKey(key);
			AOMCacheItem aomcacheItem = this.CreateOMCacheItem(key, value, ver, ttl, tags);
			this._region.ForceUpsert(aomcacheItem, opState);
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00047670 File Offset: 0x00045870
		internal AOMCacheItem CreateOMCacheItem(object key, object value, InternalCacheItemVersion ver, DateTime ttl, DataCacheTag[] tags)
		{
			key = OMRegion.GetKey(key);
			AOMCacheItem cacheItem = this.CacheItemFactory.GetCacheItem(this, (Key)key, value, this.ValidateAndUpdateTTL(ttl), this.GetExtensionTimeout(ttl), tags, this._props.OType);
			cacheItem.Version = ver;
			return cacheItem;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x000476BE File Offset: 0x000458BE
		internal void ForceUpsert(IOMCacheItem cacheItem, object opState)
		{
			this._region.ForceUpsert((ADMCacheItem)cacheItem, opState);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x000476D4 File Offset: 0x000458D4
		internal void InternalUpsert(object key, object value, InternalCacheItemVersion ver, DateTime ttl, DataCacheTag[] tags)
		{
			key = OMRegion.GetKey(key);
			AOMCacheItem cacheItem = this.CacheItemFactory.GetCacheItem(this, (Key)key, value, this.ValidateAndUpdateTTL(ttl), this.GetExtensionTimeout(ttl), tags, ObjectType.CLRObjectRefType);
			cacheItem.Version = ver;
			this._region.InternalUpsert(cacheItem.Key, cacheItem);
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0004772C File Offset: 0x0004592C
		internal void InternalPutAndLock(object key, object value, InternalCacheItemVersion version, DateTime ttl, object[] tags, DataCacheLockHandle lockHandle)
		{
			key = OMRegion.GetKey(key);
			AOMCacheItem cacheItem = this.CacheItemFactory.GetCacheItem(this, (Key)key, value, this.ValidateAndUpdateTTL(ttl), this.GetExtensionTimeout(ttl), tags, ObjectType.CLRObjectRefType);
			cacheItem.Version = version;
			this._region.InternalPutAndUnlock(cacheItem, lockHandle);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0004777E File Offset: 0x0004597E
		internal AOMCacheItem ForceDelete(object key)
		{
			key = OMRegion.GetKey(key);
			return this.ForceDelete(key, null);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00047790 File Offset: 0x00045990
		internal AOMCacheItem ForceDelete(object key, object opstate)
		{
			key = OMRegion.GetKey(key);
			return (AOMCacheItem)this._region.ForceDelete(key, opstate);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000477AC File Offset: 0x000459AC
		internal void ForceLockUpdate(object key, DataCacheLockHandle handle, TimeSpan timeout, bool lockKey, InternalCacheItemVersion version, object opState)
		{
			key = OMRegion.GetKey(key);
			this._region.ForceLockUpdate(key, timeout, handle, lockKey, version, opState);
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000477CA File Offset: 0x000459CA
		internal void ForceUnlock(object key, DataCacheLockHandle handle, DateTime timeout, object opState)
		{
			key = OMRegion.GetKey(key);
			this._region.ForcedUnlock(key, handle, timeout, opState);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000477E5 File Offset: 0x000459E5
		internal void ForceResetTimeout(object key, DateTime timeout)
		{
			key = OMRegion.GetKey(key);
			this._region.ForceResetTimeOut(key, timeout);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00047800 File Offset: 0x00045A00
		internal bool TryEnterCompactionExclusionLock()
		{
			bool flag = Interlocked.CompareExchange(ref this._compactionExclusionLock, 1, 0) == 0;
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(ObjectManager.LogSource, "TryEnterCompactionLock - {0},{1} Status {2}", new object[]
				{
					this._namedCache.CacheName,
					this._regionName,
					flag
				});
			}
			return flag;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00047860 File Offset: 0x00045A60
		internal void EnterCompactionExclusionLock()
		{
			int num = 0;
			while (Interlocked.CompareExchange(ref this._compactionExclusionLock, 1, 0) != 0)
			{
				num++;
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(ObjectManager.LogSource, "{0}th attempt to acquire compaction lock failed - {1},{2}.", new object[]
					{
						num,
						this._namedCache.CacheName,
						this._regionName
					});
				}
				Thread.Sleep(100);
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(ObjectManager.LogSource, "EnteredCompactionLock - {0},{1}.", new object[]
				{
					this._namedCache.CacheName,
					this._regionName
				});
			}
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00047900 File Offset: 0x00045B00
		internal void ExitCompactionExclusionLock()
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(ObjectManager.LogSource, "ExitCompactionLock {0},{1}", new object[]
				{
					this._namedCache.CacheName,
					this._regionName
				});
			}
			ReleaseAssert.IsTrue<OMRegion>(Interlocked.Exchange(ref this._compactionExclusionLock, 0) != 0, "Exit done by multiple threads - {0}", this);
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x00047960 File Offset: 0x00045B60
		public string RegionName
		{
			get
			{
				return this._regionName;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x00047968 File Offset: 0x00045B68
		public string CacheName
		{
			get
			{
				return this._namedCache.CacheName;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600177C RID: 6012 RVA: 0x00047975 File Offset: 0x00045B75
		public bool IsDeleted
		{
			get
			{
				return this._isDeleted;
			}
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0004797D File Offset: 0x00045B7D
		internal void MarkAsDeleted()
		{
			this._isDeleted = true;
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600177E RID: 6014 RVA: 0x00047986 File Offset: 0x00045B86
		public TimeSpan DefaultTTL
		{
			get
			{
				return this._namedCache.DefaultTTL;
			}
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00047994 File Offset: 0x00045B94
		internal int DoCompaction()
		{
			int num = 0;
			if (this.TryEnterCompactionExclusionLock())
			{
				try
				{
					num = this._region.DoCompaction();
				}
				finally
				{
					this.ExitCompactionExclusionLock();
				}
			}
			return num;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x000479D4 File Offset: 0x00045BD4
		internal int GetSubdirectoriesCount()
		{
			return this._region.SplitCount;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000479E4 File Offset: 0x00045BE4
		internal void InternalDelete(EvictionReplicationBatch batch, object opState)
		{
			IEnumerator enumerator = batch.GetEnumerator(this.RegionName);
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				EvictedElement evictedElement = (EvictedElement)obj;
				try
				{
					this._region.InternalDelete(evictedElement.Key, evictedElement.Version, opState);
				}
				catch (DataCacheException ex)
				{
					if (ex.ErrorCode != 2009)
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x00047A54 File Offset: 0x00045C54
		internal bool BeginDelete(object key, InternalCacheItemVersion version)
		{
			key = OMRegion.GetKey(key);
			return this._region.BeginDelete(key, version);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00047A6B File Offset: 0x00045C6B
		internal void RollbackDelete(object key, InternalCacheItemVersion version)
		{
			key = OMRegion.GetKey(key);
			this._region.AbortDelete(key, version);
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x00047A82 File Offset: 0x00045C82
		internal void CommitDelete(object key, object opState)
		{
			key = OMRegion.GetKey(key);
			this._region.CommitDelete(key, opState);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00047A9A File Offset: 0x00045C9A
		public override int GetHashCode()
		{
			return this._key.GetHashCode();
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x00047AA8 File Offset: 0x00045CA8
		public override bool Equals(object obj)
		{
			CacheRegion cacheRegion = obj as CacheRegion;
			if (cacheRegion == null)
			{
				OMRegion omregion = obj as OMRegion;
				cacheRegion = omregion._key;
			}
			return cacheRegion.Equals(this._key);
		}

		// Token: 0x04000D07 RID: 3335
		private static GetKeyFromCacheItemDelegate _keyGeneratorForTags = new GetKeyFromCacheItemDelegate(OMRegion.MyKeyGeneratorForTags);

		// Token: 0x04000D08 RID: 3336
		[DataMember]
		private string _regionName;

		// Token: 0x04000D09 RID: 3337
		[DataMember]
		private OMNamedCache _namedCache;

		// Token: 0x04000D0A RID: 3338
		private OMRegionProperties _props;

		// Token: 0x04000D0B RID: 3339
		private IContainerSchema _regionSchema;

		// Token: 0x04000D0C RID: 3340
		private IDMContainer _region;

		// Token: 0x04000D0D RID: 3341
		private readonly DMOperationCallBack[] _omPrePostOperations;

		// Token: 0x04000D0E RID: 3342
		private readonly IDataManager _dm;

		// Token: 0x04000D0F RID: 3343
		private object _state;

		// Token: 0x04000D10 RID: 3344
		private bool _isDeleted;

		// Token: 0x04000D11 RID: 3345
		private readonly int _emptyRegionSize;

		// Token: 0x04000D12 RID: 3346
		private int _compactionExclusionLock;

		// Token: 0x04000D13 RID: 3347
		private CacheRegion _key;
	}
}
