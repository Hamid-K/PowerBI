using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000213 RID: 531
	internal interface IDMContainer
	{
		// Token: 0x0600112E RID: 4398
		void AddIndex(int level, GetKeyFromCacheItemDelegate keyExtractor);

		// Token: 0x0600112F RID: 4399
		void RegisterLockPlaceHolderObject(GetLockPlaceHolderObject callback);

		// Token: 0x06001130 RID: 4400
		void RegisterCallBack(DMCallBackType callBackType, DMOperationCallBack callBack);

		// Token: 0x06001131 RID: 4401
		void Add(ADMCacheItem obj);

		// Token: 0x06001132 RID: 4402
		void Add(ADMCacheItem obj, object opState);

		// Token: 0x06001133 RID: 4403
		ADMCacheItem Get(object key);

		// Token: 0x06001134 RID: 4404
		ADMCacheItem Upsert(ADMCacheItem obj);

		// Token: 0x06001135 RID: 4405
		ADMCacheItem Upsert(ADMCacheItem obj, object opState);

		// Token: 0x06001136 RID: 4406
		void ForceUpsert(ADMCacheItem obj, object opState);

		// Token: 0x06001137 RID: 4407
		ADMCacheItem ForceDelete(object key);

		// Token: 0x06001138 RID: 4408
		ADMCacheItem ForceDelete(object key, object opState);

		// Token: 0x06001139 RID: 4409
		void ForceLockUpdate(object key, TimeSpan lockTimeOut, DataCacheLockHandle lockHandle, bool lockKey, InternalCacheItemVersion version, object opState);

		// Token: 0x0600113A RID: 4410
		bool ForceResetTimeOut(object key, DateTime timeOut);

		// Token: 0x0600113B RID: 4411
		bool ForcedUnlock(object key, DataCacheLockHandle lockHandle, DateTime objectTimeout, object opState);

		// Token: 0x0600113C RID: 4412
		ADMCacheItem Upsert(ADMCacheItem obj, InternalCacheItemVersion version, ProtocolType protocolType, object opState);

		// Token: 0x0600113D RID: 4413
		ADMCacheItem Replace(ADMCacheItem obj, InternalCacheItemVersion version, object opState);

		// Token: 0x0600113E RID: 4414
		ADMCacheItem IncrementDecrement(object key, ADMCacheItem obj, object value, object initialValue, SerializationCategory serializationCategory, InternalCacheItemVersion version, object opState);

		// Token: 0x0600113F RID: 4415
		ADMCacheItem Concatenate(object key, ADMCacheItem obj, object value, bool isAppend, SerializationCategory serializationCategory, InternalCacheItemVersion version, object opState);

		// Token: 0x06001140 RID: 4416
		ADMCacheItem InternalUpsert(object key, ADMCacheItem obj);

		// Token: 0x06001141 RID: 4417
		ADMCacheItem InternalPutAndUnlock(ADMCacheItem dmCacheItem, DataCacheLockHandle lockHandle);

		// Token: 0x06001142 RID: 4418
		ADMCacheItem Delete(object key);

		// Token: 0x06001143 RID: 4419
		ADMCacheItem Delete(object key, DataCacheLockHandle lockHandle);

		// Token: 0x06001144 RID: 4420
		ADMCacheItem Delete(object key, DataCacheLockHandle lockHandle, object opState);

		// Token: 0x06001145 RID: 4421
		ADMCacheItem Delete(object key, object opState);

		// Token: 0x06001146 RID: 4422
		ADMCacheItem InternalDelete(object key, InternalCacheItemVersion version, object state);

		// Token: 0x06001147 RID: 4423
		ADMCacheItem Delete(object key, InternalCacheItemVersion version, object opState);

		// Token: 0x06001148 RID: 4424
		IHashtableEnumerator Enumerate();

		// Token: 0x06001149 RID: 4425
		int GetVersion(object key);

		// Token: 0x0600114A RID: 4426
		IEnumerator Find(object[] LevelwiseKeys);

		// Token: 0x0600114B RID: 4427
		IEnumerator FindUnion(List<object[]> listLevelwiseKeys);

		// Token: 0x0600114C RID: 4428
		IEnumerator FindIntersection(List<object[]> listLevelwiseKeys);

		// Token: 0x0600114D RID: 4429
		IContainerSchema GetSchema();

		// Token: 0x0600114E RID: 4430
		ADMCacheItem GetAndLock(object key, TimeSpan lockTimeOut, bool lockKey, object opState);

		// Token: 0x0600114F RID: 4431
		ADMCacheItem PutAndUnlock(ADMCacheItem DMCacheItem, DataCacheLockHandle lockHandle, object opState);

		// Token: 0x06001150 RID: 4432
		bool Unlock(object key, DataCacheLockHandle lockHandle, object opState);

		// Token: 0x06001151 RID: 4433
		bool Unlock(object key, DataCacheLockHandle lockHandle, DateTime objectTimeOut, object opState);

		// Token: 0x06001152 RID: 4434
		ADMCacheItem Commit(object key, object item);

		// Token: 0x06001153 RID: 4435
		ADMCacheItem ReadThroughLock(object key, TimeSpan lockTimeOut, object opState);

		// Token: 0x06001154 RID: 4436
		void ReadThroughUnlock(object key, DataCacheLockHandle lockHandle, object opState);

		// Token: 0x06001155 RID: 4437
		ADMCacheItem ReadThroughPutAndUnlock(ADMCacheItem dmCacheItem, DataCacheLockHandle lockHandle, object opState);

		// Token: 0x06001156 RID: 4438
		ADMCacheItem GetIfVersionMismatch(object key, InternalCacheItemVersion version);

		// Token: 0x06001157 RID: 4439
		bool ResetTimeOut(object key, DateTime timeOut);

		// Token: 0x06001158 RID: 4440
		bool ResetTimeOut(object key, DateTime timeOut, object opState);

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001159 RID: 4441
		// (set) Token: 0x0600115A RID: 4442
		CommitType CommitType { get; set; }

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600115B RID: 4443
		// (set) Token: 0x0600115C RID: 4444
		ExpirationType ExpirationType { get; set; }

		// Token: 0x0600115D RID: 4445
		EnumeratorState GetStatelessEnumeratorState();

		// Token: 0x0600115E RID: 4446
		EnumeratorState FindWithStatelessEnumerator(object[] levelwiseKeys);

		// Token: 0x0600115F RID: 4447
		EnumeratorState FindUnionAllWithStatelessEnumerator(List<object[]> listofLevelwiseKeys);

		// Token: 0x06001160 RID: 4448
		bool GetBatch(IScanner scanner, EnumeratorState state);

		// Token: 0x06001161 RID: 4449
		int DoCompaction();

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001162 RID: 4450
		int SplitCount { get; }

		// Token: 0x06001163 RID: 4451
		bool BeginDelete(object key, InternalCacheItemVersion version);

		// Token: 0x06001164 RID: 4452
		ADMCacheItem CommitDelete(object key, object opState);

		// Token: 0x06001165 RID: 4453
		void AbortDelete(object key, InternalCacheItemVersion version);
	}
}
