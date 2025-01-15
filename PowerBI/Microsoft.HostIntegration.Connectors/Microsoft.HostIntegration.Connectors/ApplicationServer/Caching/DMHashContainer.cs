using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000214 RID: 532
	internal sealed class DMHashContainer : IDMContainer
	{
		// Token: 0x06001166 RID: 4454 RVA: 0x00038400 File Offset: 0x00036600
		public DMHashContainer(IDirectoryNodeFactory directoryNodeFactory)
		{
			this._directoryNodeFactory = directoryNodeFactory;
			this._hashTableStore = DataStructureFactory.CreateHashtable(this._directoryNodeFactory);
			this.Initialize();
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00038433 File Offset: 0x00036633
		public DMHashContainer(IContainerSchema schema, IDirectoryNodeFactory directorNodeFactory)
		{
			this.InitializeWithSchema(schema, directorNodeFactory);
			this.Initialize();
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x00038456 File Offset: 0x00036656
		// (set) Token: 0x06001169 RID: 4457 RVA: 0x00038463 File Offset: 0x00036663
		public CommitType CommitType
		{
			get
			{
				return this._hashTableStore.CommitType;
			}
			set
			{
				this._hashTableStore.CommitType = value;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x00038471 File Offset: 0x00036671
		// (set) Token: 0x0600116B RID: 4459 RVA: 0x0003847E File Offset: 0x0003667E
		public ExpirationType ExpirationType
		{
			get
			{
				return this._hashTableStore.ExpirationType;
			}
			set
			{
				this._hashTableStore.ExpirationType = value;
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0003848C File Offset: 0x0003668C
		private void Initialize()
		{
			this.PostAddCallBack = new DMOperationCallBack(this.PostAdd);
			this.PostUpsertCallBack = new DMOperationCallBack(this.PostUpsert);
			this.PostForceUpsertCallBack = new DMOperationCallBack(this.PostForceUpsert);
			this.PostForceDeleteCallBack = new DMOperationCallBack(this.PostForcedDelete);
			this.PostDeleteCallBack = new DMOperationCallBack(this.PostDelete);
			this.PostInternalDeleteCallBack = new DMOperationCallBack(this.PostInternalDelete);
			this.PostInternalUpsertCallBack = new DMOperationCallBack(this.PostInternalUpsert);
			this.PostInternalLockUpdateCallback = new DMOperationCallBack(this.PostInternalLockUpdate);
			this.PostForcedUnlockCallback = new DMOperationCallBack(this.PostForcedUnlock);
			this.PostCommitDeleteCallback = new DMOperationCallBack(this.PostCommitDelete);
			this.PostInternalPutAndUnlockCallback = new DMOperationCallBack(this.PostInternalPutAndUnlock);
			this.PostInternalIncrementDecrementCallBack = new DMOperationCallBack(this.PostIncrement);
			this.PostInternalConcatenateCallBack = new DMOperationCallBack(this.PostConcatenate);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00038584 File Offset: 0x00036784
		private void InitializeWithSchema(IContainerSchema schema, IDirectoryNodeFactory directorNodeFactory)
		{
			DMHashContainerSchema dmhashContainerSchema = schema as DMHashContainerSchema;
			this._operationCallBackArray = (DMOperationCallBack[])dmhashContainerSchema.OperationCallBackArray.Clone();
			this._hashTableStore = DataStructureFactory.CreateHashtable(dmhashContainerSchema.BaseStoreSchema, directorNodeFactory);
			this._hashTableStore.CommitType = schema.CommitType;
			this._hashTableStore.ExpirationType = schema.ExpirationType;
			this._directoryNodeFactory = directorNodeFactory;
			IIndexSchema indexSchema = dmhashContainerSchema.GetIndexSchema();
			if (indexSchema != null)
			{
				this._indexInfo = new IndexInfo();
				this._indexInfo.KeyExtractDelegate = indexSchema.TagExtractorDelegate;
				this._indexInfo.MultiHashTable = DataStructureFactory.CreateMultiLevelHashTable(indexSchema.IndexStoreSchema, directorNodeFactory);
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x00038628 File Offset: 0x00036828
		public void AddIndex(int level, GetKeyFromCacheItemDelegate keyExtractor)
		{
			IMultiLevelHashTable multiLevelHashTable = DataStructureFactory.CreateMultiLevelHashTable(level, this._directoryNodeFactory);
			IndexInfo indexInfo = new IndexInfo(multiLevelHashTable, keyExtractor);
			if (Interlocked.CompareExchange<IndexInfo>(ref this._indexInfo, indexInfo, null) != null)
			{
				throw DMGlobal.GetException(2005);
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00038664 File Offset: 0x00036864
		public void RegisterCallBack(DMCallBackType callBackType, DMOperationCallBack callBack)
		{
			this._operationCallBackArray[(int)callBackType] = callBack;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0003866F File Offset: 0x0003686F
		public ADMCacheItem Get(object key)
		{
			return this._hashTableStore.Get(key);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0003867D File Offset: 0x0003687D
		public void Add(ADMCacheItem obj)
		{
			this.Add(obj, null);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00038687 File Offset: 0x00036887
		public void Add(ADMCacheItem obj, object opState)
		{
			this._hashTableStore.Add(obj, this._operationCallBackArray[0], this.PostAddCallBack, opState);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000386A4 File Offset: 0x000368A4
		public ADMCacheItem Upsert(ADMCacheItem obj)
		{
			return this.Upsert(obj, null);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000386AE File Offset: 0x000368AE
		public void RegisterLockPlaceHolderObject(GetLockPlaceHolderObject callback)
		{
			this._hashTableStore.RegisterLockPlaceHolderObject(callback);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000386BC File Offset: 0x000368BC
		public ADMCacheItem Upsert(ADMCacheItem obj, object opState)
		{
			return this._hashTableStore.Upsert(obj, this._operationCallBackArray[2], this.PostUpsertCallBack, opState);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000386DC File Offset: 0x000368DC
		public ADMCacheItem IncrementDecrement(object key, ADMCacheItem obj, object value, object initialValue, SerializationCategory serializationCategory, InternalCacheItemVersion version, object opState)
		{
			return this._hashTableStore.IncrementDecrement(key, obj, value, initialValue, serializationCategory, version, this._operationCallBackArray[29], this.PostInternalIncrementDecrementCallBack, opState);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00038710 File Offset: 0x00036910
		public ADMCacheItem Concatenate(object key, ADMCacheItem obj, object value, bool isAppend, SerializationCategory serializationCategory, InternalCacheItemVersion version, object opState)
		{
			return this._hashTableStore.Concatenate(key, obj, value, isAppend, serializationCategory, version, this._operationCallBackArray[31], this.PostInternalConcatenateCallBack, opState);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00038742 File Offset: 0x00036942
		public void ForceUpsert(ADMCacheItem obj, object opState)
		{
			this._hashTableStore.ForceUpsert(obj, this.PostForceUpsertCallBack, opState);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00038757 File Offset: 0x00036957
		public ADMCacheItem ForceDelete(object key)
		{
			return this.ForceDelete(key, null);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00038761 File Offset: 0x00036961
		public ADMCacheItem ForceDelete(object key, object opState)
		{
			return this._hashTableStore.ForceDelete(key, this.PostForceDeleteCallBack, opState);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00038776 File Offset: 0x00036976
		public void ForceLockUpdate(object key, TimeSpan lockTimeOut, DataCacheLockHandle lockHandle, bool lockKey, InternalCacheItemVersion version, object opState)
		{
			this._hashTableStore.InternalLockUpdate(key, lockTimeOut, lockHandle, lockKey, version, this.PostInternalLockUpdateCallback, opState);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00038792 File Offset: 0x00036992
		public bool ForceResetTimeOut(object key, DateTime timeOut)
		{
			return this._hashTableStore.ForceResetTimeOut(key, timeOut);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000387A1 File Offset: 0x000369A1
		public bool ForcedUnlock(object key, DataCacheLockHandle lockHandle, DateTime objectTimeout, object opState)
		{
			return this._hashTableStore.ForcedUnlock(key, lockHandle, objectTimeout, this.PostForcedUnlockCallback, opState);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000387B9 File Offset: 0x000369B9
		public void VerifyState()
		{
			((MultiDirectoryHashtable)this._hashTableStore).VerifyState();
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000387CB File Offset: 0x000369CB
		public ADMCacheItem Upsert(ADMCacheItem obj, InternalCacheItemVersion version, ProtocolType protocolType, object opState)
		{
			return this._hashTableStore.Upsert(obj, version, protocolType, this._operationCallBackArray[2], new DMOperationCallBack(this.PostUpsert), opState);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x000387F1 File Offset: 0x000369F1
		public ADMCacheItem Replace(ADMCacheItem obj, InternalCacheItemVersion version, object opState)
		{
			return this._hashTableStore.Replace(obj, version, this._operationCallBackArray[2], new DMOperationCallBack(this.PostUpsert), opState);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00038815 File Offset: 0x00036A15
		public ADMCacheItem InternalUpsert(object key, ADMCacheItem obj)
		{
			return this._hashTableStore.InternalUpsert(obj, this._operationCallBackArray[14], this.PostInternalUpsertCallBack);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00038832 File Offset: 0x00036A32
		public ADMCacheItem InternalPutAndUnlock(ADMCacheItem dmCacheItem, DataCacheLockHandle lockHandle)
		{
			return this._hashTableStore.InternalPutAndUnlock(dmCacheItem, lockHandle, this._operationCallBackArray[27], this.PostInternalPutAndUnlockCallback);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00038850 File Offset: 0x00036A50
		public ADMCacheItem Delete(object key)
		{
			return this.Delete(key, null);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0003885A File Offset: 0x00036A5A
		public ADMCacheItem Delete(object key, object opState)
		{
			return this._hashTableStore.Delete(key, this._operationCallBackArray[4], this.PostDeleteCallBack, opState);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00038877 File Offset: 0x00036A77
		public ADMCacheItem Delete(object key, InternalCacheItemVersion version, object opState)
		{
			return this._hashTableStore.Delete(key, version, this._operationCallBackArray[4], this.PostDeleteCallBack, opState);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00038895 File Offset: 0x00036A95
		public ADMCacheItem Delete(object key, DataCacheLockHandle lockHandle)
		{
			return this.Delete(key, lockHandle, null);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000388A0 File Offset: 0x00036AA0
		public ADMCacheItem Delete(object key, DataCacheLockHandle lockHandle, object opState)
		{
			return this._hashTableStore.Delete(key, lockHandle, this._operationCallBackArray[4], this.PostDeleteCallBack, opState);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000388BE File Offset: 0x00036ABE
		public ADMCacheItem InternalDelete(object key, InternalCacheItemVersion version, object state)
		{
			return this._hashTableStore.InternalDelete(key, version, null, this.PostInternalDeleteCallBack, state);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000388D5 File Offset: 0x00036AD5
		public IHashtableEnumerator Enumerate()
		{
			return this._hashTableStore.Enumerate();
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000388E4 File Offset: 0x00036AE4
		private bool PostDelete(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			DMOperationCallBack dmoperationCallBack = this._operationCallBackArray[5];
			if (dmoperationCallBack != null)
			{
				dmoperationCallBack(oldItem, newItem, opState);
			}
			if (this._indexInfo != null)
			{
				List<object[]> list = this._indexInfo.KeyExtractDelegate(oldItem);
				if (list != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						this._indexInfo.MultiHashTable.Delete(list[i]);
					}
				}
			}
			return true;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00038950 File Offset: 0x00036B50
		private bool PostForcedDelete(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			DMOperationCallBack dmoperationCallBack = this._operationCallBackArray[18];
			if (dmoperationCallBack != null)
			{
				dmoperationCallBack(oldItem, newItem, opState);
			}
			if (this._indexInfo != null)
			{
				List<object[]> list = this._indexInfo.KeyExtractDelegate(oldItem);
				if (list != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						this._indexInfo.MultiHashTable.Delete(list[i]);
					}
				}
			}
			return true;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x000389BB File Offset: 0x00036BBB
		private bool PostUpsert(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			return this.PostUpsertCommon(oldItem, newItem, this._operationCallBackArray[3], opState);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x000389CE File Offset: 0x00036BCE
		private bool PostForceUpsert(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			return this.PostUpsertCommon(oldItem, newItem, this._operationCallBackArray[16], opState);
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000389E2 File Offset: 0x00036BE2
		private bool PostInternalDelete(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			return this.PostUpsertCommon(oldItem, newItem, this._operationCallBackArray[17], opState);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x000389F8 File Offset: 0x00036BF8
		private bool PostInternalLockUpdate(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			DMOperationCallBack dmoperationCallBack = this._operationCallBackArray[20];
			if (dmoperationCallBack != null)
			{
				dmoperationCallBack(oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00038A20 File Offset: 0x00036C20
		private bool PostForcedUnlock(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			DMOperationCallBack dmoperationCallBack = this._operationCallBackArray[21];
			if (dmoperationCallBack != null)
			{
				dmoperationCallBack(oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00038A45 File Offset: 0x00036C45
		private bool PostInternalUpsert(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			return this.PostUpsertCommon(oldItem, newItem, this._operationCallBackArray[15], opState);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00038A59 File Offset: 0x00036C59
		private bool PostPutAndUnlock(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			return this.PostUpsertCommon(oldItem, newItem, this._operationCallBackArray[9], opState);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00038A6D File Offset: 0x00036C6D
		private bool PostInternalPutAndUnlock(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			return this.PostUpsertCommon(oldItem, newItem, this._operationCallBackArray[28], opState);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00038A84 File Offset: 0x00036C84
		private bool PostUpsertCommon(ADMCacheItem oldItem, ADMCacheItem newItem, DMOperationCallBack callBack, object opState)
		{
			if (callBack != null)
			{
				callBack(oldItem, newItem, opState);
			}
			if (this._indexInfo != null)
			{
				List<object[]> list = this._indexInfo.KeyExtractDelegate(oldItem);
				if (list != null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						this._indexInfo.MultiHashTable.Delete(list[i]);
					}
				}
				List<object[]> list2 = this._indexInfo.KeyExtractDelegate(newItem);
				if (list2 != null)
				{
					for (int j = 0; j < list2.Count; j++)
					{
						this._indexInfo.MultiHashTable.Add(list2[j], newItem);
					}
				}
			}
			return true;
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00038B24 File Offset: 0x00036D24
		private bool PostAdd(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			return this.PostUpsertCommon(oldItem, newItem, this._operationCallBackArray[1], opState);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00038B38 File Offset: 0x00036D38
		private bool PostIncrement(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			DMOperationCallBack dmoperationCallBack = this._operationCallBackArray[30];
			return dmoperationCallBack == null || dmoperationCallBack(oldItem, newItem, opState);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00038B60 File Offset: 0x00036D60
		private bool PostConcatenate(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			DMOperationCallBack dmoperationCallBack = this._operationCallBackArray[32];
			return dmoperationCallBack == null || dmoperationCallBack(oldItem, newItem, opState);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00038B85 File Offset: 0x00036D85
		private bool PostCommitDelete(ADMCacheItem oldItem, ADMCacheItem newItem, object opState)
		{
			return this.PostUpsertCommon(oldItem, newItem, this._operationCallBackArray[19], opState);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00038B99 File Offset: 0x00036D99
		public IEnumerator Find(object[] LevelwiseKeys)
		{
			return this._indexInfo.MultiHashTable.Find(LevelwiseKeys);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00038BAC File Offset: 0x00036DAC
		public bool ResetTimeOut(object key, DateTime timeOut)
		{
			return this.ResetTimeOut(key, timeOut, null);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00038BB7 File Offset: 0x00036DB7
		public bool ResetTimeOut(object key, DateTime timeOut, object opState)
		{
			return this._hashTableStore.ResetTimeOut(key, timeOut, this._operationCallBackArray[12], this._operationCallBackArray[13], opState);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00038BDC File Offset: 0x00036DDC
		public ADMCacheItem GetIfVersionMismatch(object key, InternalCacheItemVersion version)
		{
			ADMCacheItem admcacheItem = this.Get(key);
			if (admcacheItem == null)
			{
				throw DMHashContainer.GetException(2002);
			}
			if (version.Equals(admcacheItem.Version))
			{
				return null;
			}
			return admcacheItem;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00038C11 File Offset: 0x00036E11
		private static DataCacheException GetException(int errorCode)
		{
			return DMGlobal.GetException(errorCode);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00038C19 File Offset: 0x00036E19
		public IEnumerator FindUnion(List<object[]> listLevelwiseKeys)
		{
			return this._indexInfo.MultiHashTable.FindUnion(listLevelwiseKeys);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00038C2C File Offset: 0x00036E2C
		public IEnumerator FindIntersection(List<object[]> listLevelwiseKeys)
		{
			return this._indexInfo.MultiHashTable.FindIntersection(listLevelwiseKeys);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00038C40 File Offset: 0x00036E40
		public IContainerSchema GetSchema()
		{
			DMHashContainerSchema dmhashContainerSchema = new DMHashContainerSchema();
			dmhashContainerSchema.OperationCallBackArray = (DMOperationCallBack[])this._operationCallBackArray.Clone();
			if (this._indexInfo != null)
			{
				dmhashContainerSchema.AddIndex(this.GetIndexSchema());
			}
			if (this._hashTableStore != null)
			{
				dmhashContainerSchema.BaseStoreSchema = this._hashTableStore.GetSchema();
			}
			dmhashContainerSchema.CommitType = this._hashTableStore.CommitType;
			dmhashContainerSchema.ExpirationType = this._hashTableStore.ExpirationType;
			return dmhashContainerSchema;
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00038CBC File Offset: 0x00036EBC
		private IIndexSchema GetIndexSchema()
		{
			return new DMIndexSchema
			{
				TagExtractorDelegate = this._indexInfo.KeyExtractDelegate,
				IndexStoreSchema = this._indexInfo.MultiHashTable.GetSchema()
			};
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00038CF7 File Offset: 0x00036EF7
		public ADMCacheItem GetAndLock(object key, TimeSpan lockTimeOut, bool lockKey, object opState)
		{
			return this._hashTableStore.GetAndLock(key, lockTimeOut, lockKey, this._operationCallBackArray[6], this._operationCallBackArray[7], opState);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00038D19 File Offset: 0x00036F19
		public ADMCacheItem PutAndUnlock(ADMCacheItem DMCacheItem, DataCacheLockHandle lockHandle, object opState)
		{
			return this._hashTableStore.PutAndUnlock(DMCacheItem, lockHandle, this._operationCallBackArray[8], new DMOperationCallBack(this.PostPutAndUnlock), opState);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00038D3D File Offset: 0x00036F3D
		public bool Unlock(object key, DataCacheLockHandle lockHandle, object opState)
		{
			return this._hashTableStore.Unlock(key, lockHandle, this._operationCallBackArray[10], this._operationCallBackArray[11], opState);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00038D5F File Offset: 0x00036F5F
		public bool Unlock(object key, DataCacheLockHandle lockHandle, DateTime objectTimeOut, object opState)
		{
			return this._hashTableStore.Unlock(key, lockHandle, objectTimeOut, this._operationCallBackArray[10], this._operationCallBackArray[11], opState);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00038D83 File Offset: 0x00036F83
		public ADMCacheItem Commit(object key, object item)
		{
			return this._hashTableStore.Commit(key, item);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00038D92 File Offset: 0x00036F92
		public ADMCacheItem ReadThroughLock(object key, TimeSpan lockTimeOut, object opState)
		{
			return this._hashTableStore.ReadThroughLock(key, lockTimeOut, this._operationCallBackArray[23], opState);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00038DAB File Offset: 0x00036FAB
		public void ReadThroughUnlock(object key, DataCacheLockHandle lockHandle, object opState)
		{
			this._hashTableStore.ReadThroughUnlock(key, lockHandle, this._operationCallBackArray[24], opState);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00038DC4 File Offset: 0x00036FC4
		public ADMCacheItem ReadThroughPutAndUnlock(ADMCacheItem dmCacheItem, DataCacheLockHandle lockHandle, object opState)
		{
			return this._hashTableStore.ReadThroughPutAndUnlock(dmCacheItem, lockHandle, this._operationCallBackArray[25], this._operationCallBackArray[26], opState);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00038DE6 File Offset: 0x00036FE6
		public EnumeratorState GetStatelessEnumeratorState()
		{
			return new BaseEnumeratorState(this._hashTableStore, this._hashTableStore.LastCompactionEpoch);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00038DFE File Offset: 0x00036FFE
		public EnumeratorState FindWithStatelessEnumerator(object[] levelwiseKeys)
		{
			return this._indexInfo.MultiHashTable.FindWithStatelessEnumerator(levelwiseKeys);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00038E11 File Offset: 0x00037011
		public EnumeratorState FindUnionAllWithStatelessEnumerator(List<object[]> listofLevelwiseKeys)
		{
			return this._indexInfo.MultiHashTable.UnionAllWithStatelessEnumerator(listofLevelwiseKeys);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x00038E24 File Offset: 0x00037024
		public bool GetBatch(IScanner scanner, EnumeratorState state)
		{
			BaseEnumeratorState baseEnumeratorState = state as BaseEnumeratorState;
			if (baseEnumeratorState != null)
			{
				return this._hashTableStore.GetBatch(scanner, baseEnumeratorState);
			}
			return this._indexInfo.MultiHashTable.GetBatch(scanner, state);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00003CAB File Offset: 0x00001EAB
		public int GetVersion(object key)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x00038E5C File Offset: 0x0003705C
		public int DoCompaction()
		{
			int num = 0;
			if (this._indexInfo != null)
			{
				num = this._indexInfo.MultiHashTable.DoCompaction();
			}
			return num + this._hashTableStore.DoCompaction();
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x00038E93 File Offset: 0x00037093
		public int SplitCount
		{
			get
			{
				return this._hashTableStore.SplitCount;
			}
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00038EA0 File Offset: 0x000370A0
		public bool BeginDelete(object key, InternalCacheItemVersion version)
		{
			return this._hashTableStore.BeginDelete(key, version);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00038EAF File Offset: 0x000370AF
		public ADMCacheItem CommitDelete(object key, object opState)
		{
			return this._hashTableStore.CommitDelete(key, null, this.PostCommitDeleteCallback, opState);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00038EC5 File Offset: 0x000370C5
		public void AbortDelete(object key, InternalCacheItemVersion version)
		{
			this._hashTableStore.AbortDelete(key, version);
		}

		// Token: 0x04000AEE RID: 2798
		private DMOperationCallBack[] _operationCallBackArray = new DMOperationCallBack[33];

		// Token: 0x04000AEF RID: 2799
		private IndexInfo _indexInfo;

		// Token: 0x04000AF0 RID: 2800
		private IDirectoryNodeFactory _directoryNodeFactory;

		// Token: 0x04000AF1 RID: 2801
		private IHashtable _hashTableStore;

		// Token: 0x04000AF2 RID: 2802
		private DMOperationCallBack PostAddCallBack;

		// Token: 0x04000AF3 RID: 2803
		private DMOperationCallBack PostUpsertCallBack;

		// Token: 0x04000AF4 RID: 2804
		private DMOperationCallBack PostForceUpsertCallBack;

		// Token: 0x04000AF5 RID: 2805
		private DMOperationCallBack PostForceDeleteCallBack;

		// Token: 0x04000AF6 RID: 2806
		private DMOperationCallBack PostDeleteCallBack;

		// Token: 0x04000AF7 RID: 2807
		private DMOperationCallBack PostInternalDeleteCallBack;

		// Token: 0x04000AF8 RID: 2808
		private DMOperationCallBack PostInternalUpsertCallBack;

		// Token: 0x04000AF9 RID: 2809
		private DMOperationCallBack PostInternalLockUpdateCallback;

		// Token: 0x04000AFA RID: 2810
		private DMOperationCallBack PostForcedUnlockCallback;

		// Token: 0x04000AFB RID: 2811
		private DMOperationCallBack PostCommitDeleteCallback;

		// Token: 0x04000AFC RID: 2812
		private DMOperationCallBack PostInternalPutAndUnlockCallback;

		// Token: 0x04000AFD RID: 2813
		private DMOperationCallBack PostInternalIncrementDecrementCallBack;

		// Token: 0x04000AFE RID: 2814
		private DMOperationCallBack PostInternalConcatenateCallBack;
	}
}
