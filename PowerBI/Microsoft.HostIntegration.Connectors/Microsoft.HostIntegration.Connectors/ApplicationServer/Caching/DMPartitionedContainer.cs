using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000225 RID: 549
	internal class DMPartitionedContainer : IDMContainer
	{
		// Token: 0x0600121F RID: 4639 RVA: 0x00039ADC File Offset: 0x00037CDC
		public DMPartitionedContainer(IContainerSchema schema, IDataManager dm)
		{
			int num = Math.Min(DMPartitionedContainer.RoundToPowerOf2Ceiling(Environment.ProcessorCount * 2), 16);
			this._shiftRightToReachMSB = 32 - (int)Math.Log((double)num, 2.0);
			this._containerCollection = new IDMContainer[num];
			for (int i = 0; i < num; i++)
			{
				this._containerCollection[i] = ((schema != null) ? dm.CreateContainer(schema) : dm.CreateContainer());
			}
			StaticRoots.Initialize(this._containerCollection);
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00039B5A File Offset: 0x00037D5A
		public DMPartitionedContainer(IDataManager dm)
			: this(null, dm)
		{
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00039B64 File Offset: 0x00037D64
		public void AddIndex(int level, GetKeyFromCacheItemDelegate keyExtractor)
		{
			foreach (IDMContainer idmcontainer in this._containerCollection)
			{
				idmcontainer.AddIndex(level, keyExtractor);
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00039B94 File Offset: 0x00037D94
		public void RegisterLockPlaceHolderObject(GetLockPlaceHolderObject callback)
		{
			foreach (IDMContainer idmcontainer in this._containerCollection)
			{
				idmcontainer.RegisterLockPlaceHolderObject(callback);
			}
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00039BC4 File Offset: 0x00037DC4
		public void RegisterCallBack(DMCallBackType callBackType, DMOperationCallBack callBack)
		{
			foreach (IDMContainer idmcontainer in this._containerCollection)
			{
				idmcontainer.RegisterCallBack(callBackType, callBack);
			}
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00039BF2 File Offset: 0x00037DF2
		public void Add(ADMCacheItem obj)
		{
			this.GetContainer(obj.HashCode).Add(obj);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00039C06 File Offset: 0x00037E06
		public void Add(ADMCacheItem obj, object opState)
		{
			this.GetContainer(obj.HashCode).Add(obj, opState);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00039C1B File Offset: 0x00037E1B
		public ADMCacheItem Get(object key)
		{
			return this.GetContainer(key.GetHashCode()).Get(key);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00039C2F File Offset: 0x00037E2F
		public ADMCacheItem Upsert(ADMCacheItem obj)
		{
			return this.GetContainer(obj.HashCode).Upsert(obj);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00039C43 File Offset: 0x00037E43
		public ADMCacheItem Upsert(ADMCacheItem obj, object opState)
		{
			return this.GetContainer(obj.HashCode).Upsert(obj, opState);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00039C58 File Offset: 0x00037E58
		public void ForceUpsert(ADMCacheItem obj, object opState)
		{
			this.GetContainer(obj.HashCode).ForceUpsert(obj, opState);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00039C6D File Offset: 0x00037E6D
		public ADMCacheItem ForceDelete(object key)
		{
			return this.GetContainer(key.GetHashCode()).ForceDelete(key);
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00039C81 File Offset: 0x00037E81
		public ADMCacheItem ForceDelete(object key, object opState)
		{
			return this.GetContainer(key.GetHashCode()).ForceDelete(key, opState);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00039C96 File Offset: 0x00037E96
		public void ForceLockUpdate(object key, TimeSpan lockTimeOut, DataCacheLockHandle lockHandle, bool lockKey, InternalCacheItemVersion version, object opState)
		{
			this.GetContainer(key.GetHashCode()).ForceLockUpdate(key, lockTimeOut, lockHandle, lockKey, version, opState);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00039CB2 File Offset: 0x00037EB2
		public bool ForceResetTimeOut(object key, DateTime timeOut)
		{
			return this.GetContainer(key.GetHashCode()).ForceResetTimeOut(key, timeOut);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00039CC7 File Offset: 0x00037EC7
		public bool ForcedUnlock(object key, DataCacheLockHandle lockHandle, DateTime objectTimeout, object opState)
		{
			return this.GetContainer(key.GetHashCode()).ForcedUnlock(key, lockHandle, objectTimeout, opState);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00039CDF File Offset: 0x00037EDF
		public ADMCacheItem Upsert(ADMCacheItem obj, InternalCacheItemVersion version, ProtocolType protocolType, object opState)
		{
			return this.GetContainer(obj.HashCode).Upsert(obj, version, protocolType, opState);
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00039CF7 File Offset: 0x00037EF7
		public ADMCacheItem Replace(ADMCacheItem obj, InternalCacheItemVersion version, object opState)
		{
			return this.GetContainer(obj.HashCode).Replace(obj, version, opState);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00039D0D File Offset: 0x00037F0D
		public ADMCacheItem IncrementDecrement(object key, ADMCacheItem obj, object value, object initialValue, SerializationCategory serializationCategory, InternalCacheItemVersion version, object opState)
		{
			return this.GetContainer(obj.HashCode).IncrementDecrement(key, obj, value, initialValue, serializationCategory, version, opState);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00039D2B File Offset: 0x00037F2B
		public ADMCacheItem Concatenate(object key, ADMCacheItem obj, object value, bool isAppend, SerializationCategory serializationCategory, InternalCacheItemVersion version, object opState)
		{
			return this.GetContainer(obj.HashCode).Concatenate(key, obj, value, isAppend, serializationCategory, version, opState);
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00039D49 File Offset: 0x00037F49
		public ADMCacheItem InternalUpsert(object key, ADMCacheItem obj)
		{
			return this.GetContainer(key.GetHashCode()).InternalUpsert(key, obj);
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00039D5E File Offset: 0x00037F5E
		public ADMCacheItem InternalPutAndUnlock(ADMCacheItem dmCacheItem, DataCacheLockHandle lockHandle)
		{
			return this.GetContainer(dmCacheItem.HashCode).InternalPutAndUnlock(dmCacheItem, lockHandle);
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00039D73 File Offset: 0x00037F73
		public ADMCacheItem Delete(object key)
		{
			return this.GetContainer(key.GetHashCode()).Delete(key);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00039D87 File Offset: 0x00037F87
		public ADMCacheItem Delete(object key, DataCacheLockHandle lockHandle)
		{
			return this.GetContainer(key.GetHashCode()).Delete(key, lockHandle);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00039D9C File Offset: 0x00037F9C
		public ADMCacheItem Delete(object key, DataCacheLockHandle lockHandle, object opState)
		{
			return this.GetContainer(key.GetHashCode()).Delete(key, lockHandle, opState);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x00039DB2 File Offset: 0x00037FB2
		public ADMCacheItem Delete(object key, object opState)
		{
			return this.GetContainer(key.GetHashCode()).Delete(key, opState);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00039DC7 File Offset: 0x00037FC7
		public ADMCacheItem InternalDelete(object key, InternalCacheItemVersion version, object state)
		{
			return this.GetContainer(key.GetHashCode()).InternalDelete(key, version, state);
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00039DDD File Offset: 0x00037FDD
		public ADMCacheItem Delete(object key, InternalCacheItemVersion version, object opState)
		{
			return this.GetContainer(key.GetHashCode()).Delete(key, version, opState);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00003CAB File Offset: 0x00001EAB
		public IHashtableEnumerator Enumerate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00039DF3 File Offset: 0x00037FF3
		public int GetVersion(object key)
		{
			return this.GetContainer(key.GetHashCode()).GetVersion(key);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00003CAB File Offset: 0x00001EAB
		public IEnumerator Find(object[] LevelwiseKeys)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00003CAB File Offset: 0x00001EAB
		public IEnumerator FindUnion(List<object[]> listLevelwiseKeys)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00003CAB File Offset: 0x00001EAB
		public IEnumerator FindIntersection(List<object[]> listLevelwiseKeys)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00039E07 File Offset: 0x00038007
		public IContainerSchema GetSchema()
		{
			return this._containerCollection[0].GetSchema();
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00039E16 File Offset: 0x00038016
		public ADMCacheItem GetAndLock(object key, TimeSpan lockTimeOut, bool lockKey, object opState)
		{
			return this.GetContainer(key.GetHashCode()).GetAndLock(key, lockTimeOut, lockKey, opState);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00039E2E File Offset: 0x0003802E
		public ADMCacheItem PutAndUnlock(ADMCacheItem DMCacheItem, DataCacheLockHandle lockHandle, object opState)
		{
			return this.GetContainer(DMCacheItem.HashCode).PutAndUnlock(DMCacheItem, lockHandle, opState);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00039E44 File Offset: 0x00038044
		public bool Unlock(object key, DataCacheLockHandle lockHandle, object opState)
		{
			return this.GetContainer(key.GetHashCode()).Unlock(key, lockHandle, opState);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00039E5A File Offset: 0x0003805A
		public bool Unlock(object key, DataCacheLockHandle lockHandle, DateTime objectTimeOut, object opState)
		{
			return this.GetContainer(key.GetHashCode()).Unlock(key, lockHandle, objectTimeOut, opState);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00039E72 File Offset: 0x00038072
		public ADMCacheItem Commit(object key, object item)
		{
			return this.GetContainer(key.GetHashCode()).Commit(key, item);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00039E87 File Offset: 0x00038087
		public ADMCacheItem ReadThroughLock(object key, TimeSpan lockTimeOut, object opState)
		{
			return this.GetContainer(key.GetHashCode()).ReadThroughLock(key, lockTimeOut, opState);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00039E9D File Offset: 0x0003809D
		public void ReadThroughUnlock(object key, DataCacheLockHandle lockHandle, object opState)
		{
			this.GetContainer(key.GetHashCode()).ReadThroughUnlock(key, lockHandle, opState);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00039EB3 File Offset: 0x000380B3
		public ADMCacheItem ReadThroughPutAndUnlock(ADMCacheItem dmCacheItem, DataCacheLockHandle lockHandle, object opState)
		{
			return this.GetContainer(dmCacheItem.HashCode).ReadThroughPutAndUnlock(dmCacheItem, lockHandle, opState);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00039EC9 File Offset: 0x000380C9
		public ADMCacheItem GetIfVersionMismatch(object key, InternalCacheItemVersion version)
		{
			return this.GetContainer(key.GetHashCode()).GetIfVersionMismatch(key, version);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00039EDE File Offset: 0x000380DE
		public bool ResetTimeOut(object key, DateTime timeOut)
		{
			return this.GetContainer(key.GetHashCode()).ResetTimeOut(key, timeOut);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00039EF3 File Offset: 0x000380F3
		public bool ResetTimeOut(object key, DateTime timeOut, object opState)
		{
			return this.GetContainer(key.GetHashCode()).ResetTimeOut(key, timeOut, opState);
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x00039F09 File Offset: 0x00038109
		// (set) Token: 0x0600124D RID: 4685 RVA: 0x00039F18 File Offset: 0x00038118
		public CommitType CommitType
		{
			get
			{
				return this._containerCollection[0].CommitType;
			}
			set
			{
				foreach (IDMContainer idmcontainer in this._containerCollection)
				{
					idmcontainer.CommitType = value;
				}
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x00039F45 File Offset: 0x00038145
		// (set) Token: 0x0600124F RID: 4687 RVA: 0x00039F54 File Offset: 0x00038154
		public ExpirationType ExpirationType
		{
			get
			{
				return this._containerCollection[0].ExpirationType;
			}
			set
			{
				foreach (IDMContainer idmcontainer in this._containerCollection)
				{
					idmcontainer.ExpirationType = value;
				}
			}
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00003CAB File Offset: 0x00001EAB
		public EnumeratorState GetStatelessEnumeratorState()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00003CAB File Offset: 0x00001EAB
		public EnumeratorState FindWithStatelessEnumerator(object[] levelwiseKeys)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00003CAB File Offset: 0x00001EAB
		public EnumeratorState FindUnionAllWithStatelessEnumerator(List<object[]> listofLevelwiseKeys)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00003CAB File Offset: 0x00001EAB
		public bool GetBatch(IScanner scanner, EnumeratorState state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00039F84 File Offset: 0x00038184
		public int DoCompaction()
		{
			int num = 0;
			foreach (IDMContainer idmcontainer in this._containerCollection)
			{
				num += idmcontainer.DoCompaction();
			}
			return num;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00039FB8 File Offset: 0x000381B8
		public int SplitCount
		{
			get
			{
				int num = 0;
				foreach (IDMContainer idmcontainer in this._containerCollection)
				{
					num += idmcontainer.SplitCount;
				}
				return num;
			}
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00039FEA File Offset: 0x000381EA
		public bool BeginDelete(object key, InternalCacheItemVersion version)
		{
			return this.GetContainer(key.GetHashCode()).BeginDelete(key, version);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00039FFF File Offset: 0x000381FF
		public ADMCacheItem CommitDelete(object key, object opState)
		{
			return this.GetContainer(key.GetHashCode()).CommitDelete(key, opState);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0003A014 File Offset: 0x00038214
		public void AbortDelete(object key, InternalCacheItemVersion version)
		{
			this.GetContainer(key.GetHashCode()).AbortDelete(key, version);
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x0003A029 File Offset: 0x00038229
		internal IDMContainer[] ContainerCollection
		{
			get
			{
				return this._containerCollection;
			}
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x0003A031 File Offset: 0x00038231
		private IDMContainer GetContainer(int hashCode)
		{
			return this._containerCollection[(int)((UIntPtr)((uint)hashCode >> this._shiftRightToReachMSB))];
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x0003A048 File Offset: 0x00038248
		private static int RoundToPowerOf2Ceiling(int n)
		{
			int i;
			for (i = 1; i < n; i <<= 1)
			{
			}
			return i;
		}

		// Token: 0x04000B22 RID: 2850
		private IDMContainer[] _containerCollection;

		// Token: 0x04000B23 RID: 2851
		private readonly int _shiftRightToReachMSB;
	}
}
