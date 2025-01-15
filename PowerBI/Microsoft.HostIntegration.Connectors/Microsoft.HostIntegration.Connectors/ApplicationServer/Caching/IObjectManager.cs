using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000271 RID: 625
	internal interface IObjectManager
	{
		// Token: 0x060014DC RID: 5340
		int GetRegionCount(string cacheName);

		// Token: 0x060014DD RID: 5341
		void CreateNamedCache(string cacheName, OMNamedCacheProperties properties);

		// Token: 0x060014DE RID: 5342
		void CreateNamedCache(string cacheName, INamedCacheConfiguration config, StoreProperties storeProperties);

		// Token: 0x060014DF RID: 5343
		void DropNamedCache(string cacheName);

		// Token: 0x060014E0 RID: 5344
		void DeleteNamedCache(string cacheName);

		// Token: 0x060014E1 RID: 5345
		void DeleteNamedCache(string cacheName, object opState);

		// Token: 0x060014E2 RID: 5346
		void ClearNamedCache(string cacheName);

		// Token: 0x060014E3 RID: 5347
		void ClearNamedCache(string cacheName, object opState);

		// Token: 0x060014E4 RID: 5348
		bool ContainsNamedCache(string cacheName);

		// Token: 0x060014E5 RID: 5349
		OMNamedCache GetNamedCache(string cacheName);

		// Token: 0x060014E6 RID: 5350
		OMWriteBehindStats GetWriteBehindStats();

		// Token: 0x060014E7 RID: 5351
		OMNamedCacheStats GetNamedCacheStats(string cacheName);

		// Token: 0x060014E8 RID: 5352
		IEnumerable<KeyValuePair<string, OMNamedCacheStats>> GetAllNamedCacheStats();

		// Token: 0x060014E9 RID: 5353
		string[] ListAllRegions(string cacheName);

		// Token: 0x060014EA RID: 5354
		string[] ListAllRegions(string cacheName, int maxRegions);

		// Token: 0x060014EB RID: 5355
		void RefreshNamedCache(string cacheName, INamedCacheConfiguration newConfig);

		// Token: 0x060014EC RID: 5356
		void CreateRegion(string cacheName, string regionName, OMRegionProperties rProps, object opState, object state);

		// Token: 0x060014ED RID: 5357
		void DeleteRegion(string cacheName, string regionName, object opState);

		// Token: 0x060014EE RID: 5358
		void DeleteRegion(string cacheName, string regionName);

		// Token: 0x060014EF RID: 5359
		long DropRegion(string cacheName, string regionName);

		// Token: 0x060014F0 RID: 5360
		bool ContainsRegion(string cacheName, string regionName);

		// Token: 0x060014F1 RID: 5361
		void ClearRegion(RequestBody request, string cacheName, string regionName);

		// Token: 0x060014F2 RID: 5362
		object InternalDelete(string cacheName, string regionName, object key, InternalCacheItemVersion ver, object opState);

		// Token: 0x060014F3 RID: 5363
		OMRegionStats GetRegionStats(string cacheName, string regionName);

		// Token: 0x060014F4 RID: 5364
		void Put(RequestBody request, string cacheName, string regionName, object key, object value, DateTime timeToLive, object[] tags, ProtocolType protocolType, ref InternalCacheItemVersion ver);

		// Token: 0x060014F5 RID: 5365
		void Replace(RequestBody request, string cacheName, string regionName, object key, object value, DateTime timeToLive, object[] tags, ref InternalCacheItemVersion ver);

		// Token: 0x060014F6 RID: 5366
		object Add(RequestBody request, string cacheName, string regionName, object key, object value, DateTime timeToLive, object[] tags);

		// Token: 0x060014F7 RID: 5367
		object Get(string cacheName, string regionName, object key, ref InternalCacheItemVersion version);

		// Token: 0x060014F8 RID: 5368
		IOMCacheItem GetCacheItem(RequestBody request, string cacheName, string regionName, object key, InternalCacheItemVersion version);

		// Token: 0x060014F9 RID: 5369
		IOMCacheItem GetIfNewer(RequestBody request, string cacheName, string regionName, object key, InternalCacheItemVersion version, out bool oldVersionExists);

		// Token: 0x060014FA RID: 5370
		void Commit(RequestBody request, string cacheName, string regionName, object key, AOMCacheItem item);

		// Token: 0x060014FB RID: 5371
		object GetAndLock(RequestBody request, string cacheName, string regionName, object key, TimeSpan lockTimeOut, ref DataCacheLockHandle lHandle, bool lockKey);

		// Token: 0x060014FC RID: 5372
		void PutAndUnlock(RequestBody request, string cacheName, string regionName, object key, object value, DateTime timeToLive, object[] tags, DataCacheLockHandle lHandle);

		// Token: 0x060014FD RID: 5373
		bool Remove(RequestBody request, string cacheName, string regionName, object key, DataCacheLockHandle lockHandle);

		// Token: 0x060014FE RID: 5374
		bool Remove(RequestBody request, string cacheName, string regionName, object key, InternalCacheItemVersion ver, object opState);

		// Token: 0x060014FF RID: 5375
		bool Unlock(RequestBody request, string cacheName, string regionName, object key, DataCacheLockHandle lockHandle, DateTime ttl);

		// Token: 0x06001500 RID: 5376
		bool ResetTimeOut(RequestBody request, string cacheName, string regionName, object key, DateTime timeOut);

		// Token: 0x06001501 RID: 5377
		object IncrementDecrement(string cacheName, string regionName, object key, object value, object initialValue, DateTime ttl, SerializationCategory serializationCategory, InternalCacheItemVersion version, object opState);

		// Token: 0x06001502 RID: 5378
		object Concatenate(string cacheName, string regionName, object key, object value, bool isAppend, DateTime ttl, SerializationCategory serializationCategory, InternalCacheItemVersion version, object opState);

		// Token: 0x06001503 RID: 5379
		DataCacheLockHandle ReadThroughLock(string cacheName, string regionName, object key, TimeSpan lockTimeOut, OMRegion cachedRegion, object opState);

		// Token: 0x06001504 RID: 5380
		void ReadThroughUnlock(string cacheName, string regionName, object key, DataCacheLockHandle lHandle, object opState);

		// Token: 0x06001505 RID: 5381
		object ReadThroughPutAndUnlock(string cacheName, string regionName, object key, object value, DateTime timeToLive, object[] tags, DataCacheLockHandle lHandle, object opState);

		// Token: 0x06001506 RID: 5382
		OMOperationCallBack RegisterCallBack(OMCallBackType type, OMOperationCallBack callBack);

		// Token: 0x06001507 RID: 5383
		OMOperationCallBack UnRegisterCallBack(OMCallBackType type);

		// Token: 0x06001508 RID: 5384
		OMCacheNodeStats GetStats();

		// Token: 0x06001509 RID: 5385
		string[] ListAllNamedCaches();

		// Token: 0x0600150A RID: 5386
		EnumeratorState GetStatelessEnumeratorState(RequestBody request, string cacheName, string regionName);

		// Token: 0x0600150B RID: 5387
		EnumeratorState FindWithStatelessEnumerator(RequestBody request, string cacheName, string regionName, DataCacheTag tag);

		// Token: 0x0600150C RID: 5388
		EnumeratorState FindUnionAllWithStatelessEnumerator(RequestBody request, string cacheName, string regionName, DataCacheTag[] tags);

		// Token: 0x0600150D RID: 5389
		EnumeratorState FindIntersectionAllWithStatelessEnumerator(RequestBody request, string cacheName, string regionName, DataCacheTag[] tags);

		// Token: 0x0600150E RID: 5390
		List<KeyValuePair<string, object>> GetBatch(RequestBody request, string cacheName, string regionName, int batchSize, EnumeratorState state, ref bool toContinue);

		// Token: 0x0600150F RID: 5391
		List<string> GetKeyBatch(RequestBody request, string cacheName, string regionName, int batchSize, EnumeratorState state, ref bool toContinue);

		// Token: 0x06001510 RID: 5392
		void ForceUpsert(string cacheName, string regionName, object key, object value, InternalCacheItemVersion ver, DateTime ttl, DataCacheTag[] tags, object opState);

		// Token: 0x06001511 RID: 5393
		void ForceUpsert(IOMCacheItem cacheItem, object opState);

		// Token: 0x06001512 RID: 5394
		void ForceUpsertOMCacheItem(OMCacheItem item, object opState);

		// Token: 0x06001513 RID: 5395
		OMRegion GetRegion(string cacheName, string regionName);

		// Token: 0x06001514 RID: 5396
		OMRegion GetRegion(RequestBody request, string cacheName, string regionName);

		// Token: 0x06001515 RID: 5397
		object GetState(string cacheName, string regionName);

		// Token: 0x06001516 RID: 5398
		List<AOMCacheItem> GetBatch(string cacheName, string regionName, int batchSize, EnumeratorState state, InternalCacheItemVersion version, ref bool toContinue);

		// Token: 0x06001517 RID: 5399
		AOMCacheItem ForceDelete(RequestBody request, string cacheName, string regionName, object key);

		// Token: 0x06001518 RID: 5400
		void ForceLockUpdate(RequestBody request, string cacheName, string regionName, object key, DataCacheLockHandle handle, TimeSpan timeout, bool lockKey, InternalCacheItemVersion version);

		// Token: 0x06001519 RID: 5401
		void ForceResetTimeout(RequestBody request, string cacheName, string regionName, object key, DateTime timeout);

		// Token: 0x0600151A RID: 5402
		void ForceUnlock(RequestBody request, string cacheName, string regionName, object key, DataCacheLockHandle handle, DateTime timeout);

		// Token: 0x0600151B RID: 5403
		void InternalUpsert(string cacheName, string regionName, object key, object value, InternalCacheItemVersion version, DateTime ttl, DataCacheTag[] tags);

		// Token: 0x0600151C RID: 5404
		void InternalPutAndUnlock(string cacheName, string regionName, object key, object value, InternalCacheItemVersion version, DateTime ttl, object[] tags, DataCacheLockHandle lHandle);

		// Token: 0x0600151D RID: 5405
		int GetSubDirectoriesCount();

		// Token: 0x0600151E RID: 5406
		int GetSubDirectoriesCount(string cacheName, string regionName);

		// Token: 0x0600151F RID: 5407
		void RollbackMissCount();

		// Token: 0x06001520 RID: 5408
		void RollbackTotalCount();

		// Token: 0x06001521 RID: 5409
		long ActualItemCount();

		// Token: 0x06001522 RID: 5410
		int DoCompaction();

		// Token: 0x06001523 RID: 5411
		int DoCompaction(string cacheName, string regionName);

		// Token: 0x06001524 RID: 5412
		void InternalBatchDelete(string cacheName, EvictionReplicationBatch batch, object opState);

		// Token: 0x06001525 RID: 5413
		void BeginBatchDelete(string cacheName, EvictionReplicationBatch batch, out long sizeofLatchedItems);

		// Token: 0x06001526 RID: 5414
		void RollbackBatchDelete(string cacheName, EvictionReplicationBatch batch);

		// Token: 0x06001527 RID: 5415
		void CommitBatchDelete(string cacheName, EvictionReplicationBatch batch, object opState);

		// Token: 0x06001528 RID: 5416
		OMNamedCache GetAndLockCache();

		// Token: 0x06001529 RID: 5417
		void UnlockCache(OMNamedCache cache);
	}
}
