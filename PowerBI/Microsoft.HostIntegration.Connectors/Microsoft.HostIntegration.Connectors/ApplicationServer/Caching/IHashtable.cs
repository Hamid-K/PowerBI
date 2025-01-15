using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000230 RID: 560
	internal interface IHashtable
	{
		// Token: 0x06001282 RID: 4738
		ADMCacheItem Get(object key);

		// Token: 0x06001283 RID: 4739
		ADMCacheItem IncrementDecrement(object key, ADMCacheItem dmCacheItem, object value, object initialValue, SerializationCategory serializationCategory, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x06001284 RID: 4740
		ADMCacheItem Concatenate(object key, ADMCacheItem dmCacheItem, object value, bool isAppend, SerializationCategory serializationCategory, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x06001285 RID: 4741
		void Add(ADMCacheItem DMCacheItem, DMOperationCallBack preOperation, DMOperationCallBack postOperation);

		// Token: 0x06001286 RID: 4742
		void Add(ADMCacheItem DMCacheItem, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x06001287 RID: 4743
		ADMCacheItem Upsert(ADMCacheItem DMCacheItem, DMOperationCallBack preOperation, DMOperationCallBack postOperation);

		// Token: 0x06001288 RID: 4744
		ADMCacheItem Upsert(ADMCacheItem DMCacheItem, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x06001289 RID: 4745
		void ForceUpsert(ADMCacheItem DMCacheItem, DMOperationCallBack postOperation, object opState);

		// Token: 0x0600128A RID: 4746
		bool ForceResetTimeOut(object key, DateTime timeOut);

		// Token: 0x0600128B RID: 4747
		ADMCacheItem ForceDelete(object key, DMOperationCallBack postOperation);

		// Token: 0x0600128C RID: 4748
		ADMCacheItem ForceDelete(object key, DMOperationCallBack postOperation, object opstate);

		// Token: 0x0600128D RID: 4749
		bool ForcedUnlock(object key, DataCacheLockHandle lockHandle, DateTime objectTimeOut, DMOperationCallBack postOp, object opState);

		// Token: 0x0600128E RID: 4750
		ADMCacheItem Delete(object key, DMOperationCallBack preOperation, DMOperationCallBack postOperation);

		// Token: 0x0600128F RID: 4751
		ADMCacheItem Delete(object key, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x06001290 RID: 4752
		ADMCacheItem InternalUpsert(ADMCacheItem DMCacheItem, DMOperationCallBack preOperation, DMOperationCallBack postOperation);

		// Token: 0x06001291 RID: 4753
		ADMCacheItem InternalPutAndUnlock(ADMCacheItem dmCacheItem, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation);

		// Token: 0x06001292 RID: 4754
		IStoreSchema GetSchema();

		// Token: 0x06001293 RID: 4755
		ADMCacheItem Upsert(ADMCacheItem DMCacheItem, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation);

		// Token: 0x06001294 RID: 4756
		ADMCacheItem Upsert(ADMCacheItem dmCacheItem, InternalCacheItemVersion version, ProtocolType protocolType, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x06001295 RID: 4757
		ADMCacheItem Replace(ADMCacheItem dmCacheItem, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x06001296 RID: 4758
		ADMCacheItem Delete(object key, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation);

		// Token: 0x06001297 RID: 4759
		ADMCacheItem Delete(object key, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x06001298 RID: 4760
		ADMCacheItem InternalDelete(object key, InternalCacheItemVersion version, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x06001299 RID: 4761
		ADMCacheItem GetAndLock(object key, TimeSpan lockTimeOut, DMOperationCallBack preOperation, DMOperationCallBack postOperation);

		// Token: 0x0600129A RID: 4762
		ADMCacheItem GetAndLock(object key, TimeSpan lockTimeOut, bool lockKey, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x0600129B RID: 4763
		ADMCacheItem PutAndUnlock(ADMCacheItem DMCacheItem, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation);

		// Token: 0x0600129C RID: 4764
		ADMCacheItem PutAndUnlock(ADMCacheItem DMCacheItem, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x0600129D RID: 4765
		bool Unlock(object key, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation);

		// Token: 0x0600129E RID: 4766
		bool Unlock(object key, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x0600129F RID: 4767
		bool Unlock(object key, DataCacheLockHandle lockHandle, DateTime objectTimeOut, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x060012A0 RID: 4768
		ADMCacheItem Delete(object key, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation);

		// Token: 0x060012A1 RID: 4769
		ADMCacheItem Delete(object key, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x060012A2 RID: 4770
		ADMCacheItem ReadThroughLock(object key, TimeSpan lockTimeOut, DMOperationCallBack postOperation, object opState);

		// Token: 0x060012A3 RID: 4771
		void ReadThroughUnlock(object key, DataCacheLockHandle lockHandle, DMOperationCallBack postOperation, object opState);

		// Token: 0x060012A4 RID: 4772
		ADMCacheItem ReadThroughPutAndUnlock(ADMCacheItem dmCacheItem, DataCacheLockHandle lockHandle, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x060012A5 RID: 4773
		void InternalLockUpdate(object key, TimeSpan lockTimeOut, DataCacheLockHandle lockHandle, bool lockKey, InternalCacheItemVersion version, DMOperationCallBack postOp, object opState);

		// Token: 0x060012A6 RID: 4774
		bool ResetTimeOut(object key, DateTime timeOut, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x060012A7 RID: 4775
		void RegisterLockPlaceHolderObject(GetLockPlaceHolderObject callback);

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060012A8 RID: 4776
		// (set) Token: 0x060012A9 RID: 4777
		CommitType CommitType { get; set; }

		// Token: 0x060012AA RID: 4778
		ADMCacheItem Commit(object key, object item);

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060012AB RID: 4779
		// (set) Token: 0x060012AC RID: 4780
		ExpirationType ExpirationType { get; set; }

		// Token: 0x060012AD RID: 4781
		IHashtableEnumerator Enumerate();

		// Token: 0x060012AE RID: 4782
		bool GetBatch(IScanner scanner, EnumeratorState state);

		// Token: 0x060012AF RID: 4783
		int DoCompaction();

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060012B0 RID: 4784
		int SplitCount { get; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060012B1 RID: 4785
		long LastCompactionEpoch { get; }

		// Token: 0x060012B2 RID: 4786
		bool BeginDelete(object key, InternalCacheItemVersion version);

		// Token: 0x060012B3 RID: 4787
		ADMCacheItem CommitDelete(object key, DMOperationCallBack preOperation, DMOperationCallBack postOperation, object opState);

		// Token: 0x060012B4 RID: 4788
		void AbortDelete(object key, InternalCacheItemVersion version);
	}
}
