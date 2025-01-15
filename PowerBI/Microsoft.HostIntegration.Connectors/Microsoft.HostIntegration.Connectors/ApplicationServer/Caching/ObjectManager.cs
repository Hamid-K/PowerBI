using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200027B RID: 635
	internal sealed class ObjectManager : IObjectManager
	{
		// Token: 0x06001589 RID: 5513 RVA: 0x000413F4 File Offset: 0x0003F5F4
		internal void Initialize(OMCacheNodeProperties props)
		{
			this._prePostOperations = new DMOperationCallBack[42];
			this._userPrePostOperations = new OMOperationCallBack[42];
			this._prePostOperations[0] = new DMOperationCallBack(this.OMPreCacheItemAdd);
			this._prePostOperations[1] = new DMOperationCallBack(this.OMPostCacheItemAdd);
			this._prePostOperations[2] = new DMOperationCallBack(this.OMPreCacheItemPut);
			this._prePostOperations[3] = new DMOperationCallBack(this.OMPostCacheItemPut);
			this._prePostOperations[4] = new DMOperationCallBack(this.OMPreCacheItemRemove);
			this._prePostOperations[5] = new DMOperationCallBack(this.OMPostCacheItemRemove);
			this._prePostOperations[18] = new DMOperationCallBack(this.OMPreCacheItemGetAndLock);
			this._prePostOperations[19] = new DMOperationCallBack(this.OMPostCacheItemGetAndLock);
			this._prePostOperations[20] = new DMOperationCallBack(this.OMPreCacheItemPutAndUnlock);
			this._prePostOperations[21] = new DMOperationCallBack(this.OMPostCacheItemPutAndUnlock);
			this._prePostOperations[22] = new DMOperationCallBack(this.OMPreCacheItemResetTimeOut);
			this._prePostOperations[23] = new DMOperationCallBack(this.OMPostCacheItemResetTimeOut);
			this._prePostOperations[24] = new DMOperationCallBack(this.OMPreCacheItemUnlock);
			this._prePostOperations[25] = new DMOperationCallBack(this.OMPostCacheItemUnlock);
			this._prePostOperations[26] = new DMOperationCallBack(this.OMPostCacheItemForcedUpsert);
			this._prePostOperations[27] = new DMOperationCallBack(this.OMPostCacheItemInternalDelete);
			this._prePostOperations[28] = new DMOperationCallBack(this.OMPostCacheItemForcedDelete);
			this._prePostOperations[31] = new DMOperationCallBack(this.OMPostCacheItemInternalLockUpdate);
			this._prePostOperations[32] = new DMOperationCallBack(this.OMPostCacheItemForcedUnlock);
			this._prePostOperations[29] = new DMOperationCallBack(this.OMPreCacheItemInternalUpsert);
			this._prePostOperations[30] = new DMOperationCallBack(this.OMPostCacheItemInternalUpsert);
			this._prePostOperations[39] = new DMOperationCallBack(this.OMPostCacheItemIncrementDecrement);
			this._prePostOperations[41] = new DMOperationCallBack(this.OMPostCacheItemConcatenate);
			this._prePostOperations[38] = new DMOperationCallBack(this.OMPreCacheItemIncrementDecrement);
			this._prePostOperations[40] = new DMOperationCallBack(this.OMPreCacheItemConcatenate);
			this._prePostOperations[36] = new DMOperationCallBack(this.OMPreCacheItemInternalPutAndUnlock);
			this._prePostOperations[37] = new DMOperationCallBack(this.OMPostCacheItemInternalPutAndUnlock);
			this._prePostOperations[33] = new DMOperationCallBack(this.OMPostCacheItemReadThroughLock);
			this._prePostOperations[34] = new DMOperationCallBack(this.OMPostCacheItemReadThroughUnlock);
			this._prePostOperations[35] = new DMOperationCallBack(this.OMPostCacheItemReadThroughPutAndUnlock);
			this._prePostOperations[12] = new DMOperationCallBack(this.OMPreRegionCreate);
			this._prePostOperations[13] = new DMOperationCallBack(this.OMPostRegionCreate);
			this._prePostOperations[16] = new DMOperationCallBack(this.OMPreRegionClear);
			this._prePostOperations[17] = new DMOperationCallBack(this.OMPostRegionClear);
			this._prePostOperations[14] = new DMOperationCallBack(this.OMPreRegionDelete);
			this._prePostOperations[15] = new DMOperationCallBack(this.OMPostRegionDelete);
			this._prePostOperations[6] = new DMOperationCallBack(this.OMPreNamedCacheCreate);
			this._prePostOperations[7] = new DMOperationCallBack(this.OMPostNamedCacheCreate);
			this._prePostOperations[10] = new DMOperationCallBack(this.OMPreNamedCacheClear);
			this._prePostOperations[11] = new DMOperationCallBack(this.OMPostNamedCacheClear);
			this._prePostOperations[8] = new DMOperationCallBack(this.OMPreNamedCacheDelete);
			this._prePostOperations[9] = new DMOperationCallBack(this.OMPostNamedCacheDelete);
			this._props = new OMCacheNodeProperties(props);
			this._cacheSchema = this.PrepareSchema();
			this._dm = DataManagerFactory.CreateDataManager(this._memoryManager.GetDirectoryNodeFactory());
			this._cache = new DMPartitionedContainer(this._cacheSchema, this._dm);
			this._stats = new OMCacheNodeStats(props.PerfMonCounterRequired, this._statsContainer);
			this._namedCacheTable = new BaseHashTable(new ObjectDirectoryNodeFactory());
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x000417F0 File Offset: 0x0003F9F0
		internal IDMContainer Cache
		{
			get
			{
				return this._cache;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x000417F8 File Offset: 0x0003F9F8
		internal Array NamedCacheList
		{
			get
			{
				List<OMNamedCache> list = new List<OMNamedCache>(this.Props.MaxNamedCacheCount);
				foreach (object obj in this._namedCacheTable)
				{
					OMNamedCache omnamedCache = (OMNamedCache)obj;
					list.Add(omnamedCache);
				}
				return list.ToArray();
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00041868 File Offset: 0x0003FA68
		public IMemoryManager MemoryManager
		{
			get
			{
				return this._memoryManager;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x00041870 File Offset: 0x0003FA70
		public OMCacheNodeStats Stats
		{
			get
			{
				return this._stats;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x00041878 File Offset: 0x0003FA78
		public OMCacheNodeProperties Props
		{
			get
			{
				return new OMCacheNodeProperties(this._props);
			}
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00041885 File Offset: 0x0003FA85
		private static bool IntOMPostAdd(object oldCacheItem, object newCacheItem)
		{
			ObjectManager.IntOMCommonPostUpsertStatsUpdate(oldCacheItem, newCacheItem);
			return true;
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00041890 File Offset: 0x0003FA90
		private static bool IntOMPostPut(object oldCacheItem, object newCacheItem)
		{
			return ObjectManager.IntOMPostUpsert(oldCacheItem, newCacheItem);
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00041890 File Offset: 0x0003FA90
		private static bool IntOMPostConcatenate(object oldCacheItem, object newCacheItem)
		{
			return ObjectManager.IntOMPostUpsert(oldCacheItem, newCacheItem);
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00041890 File Offset: 0x0003FA90
		private static bool IntOMPostIncrementDecrement(object oldCacheItem, object newCacheItem)
		{
			return ObjectManager.IntOMPostUpsert(oldCacheItem, newCacheItem);
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00041885 File Offset: 0x0003FA85
		private static bool IntOMPostUpsert(object oldCacheItem, object newCacheItem)
		{
			ObjectManager.IntOMCommonPostUpsertStatsUpdate(oldCacheItem, newCacheItem);
			return true;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00041885 File Offset: 0x0003FA85
		private static bool IntOMPostForcedUpsert(object oldCacheItem, object newCacheItem)
		{
			ObjectManager.IntOMCommonPostUpsertStatsUpdate(oldCacheItem, newCacheItem);
			return true;
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0004189C File Offset: 0x0003FA9C
		private static OMRegionStats IntOMCommonPostUpsertStatsUpdate(object oldCacheItem, object newCacheItem)
		{
			OMRegionStats omregionStats = null;
			AOMCacheItem aomcacheItem = oldCacheItem as AOMCacheItem;
			AOMCacheItem aomcacheItem2 = newCacheItem as AOMCacheItem;
			if (oldCacheItem != null && newCacheItem == null)
			{
				omregionStats = aomcacheItem.Region.Stats;
				omregionStats.DecrSize(aomcacheItem.Size);
				omregionStats.DecrCount();
			}
			else if (oldCacheItem != null && newCacheItem != null)
			{
				omregionStats = aomcacheItem2.Region.Stats;
				omregionStats.IncrSize(aomcacheItem2.Size - aomcacheItem.Size);
			}
			else if (oldCacheItem == null && newCacheItem != null)
			{
				omregionStats = aomcacheItem2.Region.Stats;
				omregionStats.IncrSize(aomcacheItem2.Size);
				omregionStats.IncrCount();
			}
			return omregionStats;
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0004192C File Offset: 0x0003FB2C
		private static bool IntOMPostDelete(object oldItem, object newItem)
		{
			AOMCacheItem aomcacheItem = oldItem as AOMCacheItem;
			OMRegionStats stats = aomcacheItem.Region.Stats;
			stats.DecrSize(aomcacheItem.Size);
			stats.DecrCount();
			return true;
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00041960 File Offset: 0x0003FB60
		private static bool IntOMPostForceDelete(object oldItem, object newItem)
		{
			AOMCacheItem aomcacheItem = oldItem as AOMCacheItem;
			OMRegionStats stats = aomcacheItem.Region.Stats;
			stats.DecrSize(aomcacheItem.Size);
			stats.DecrCount();
			return true;
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00041994 File Offset: 0x0003FB94
		private static bool IntOMPostInternalDelete(object oldItem, object newItem)
		{
			AOMCacheItem aomcacheItem = oldItem as AOMCacheItem;
			OMRegionStats stats = aomcacheItem.Region.Stats;
			stats.DecrSize(aomcacheItem.Size);
			stats.DecrCount();
			return true;
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x000419C8 File Offset: 0x0003FBC8
		private static bool IntOMPostCreateRegion(object oldRegion, object newRegion)
		{
			OMRegion omregion = newRegion as OMRegion;
			OMNamedCacheStats stats = omregion.NamedCache.Stats;
			stats.IncrRegionCount();
			return true;
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x000419F0 File Offset: 0x0003FBF0
		private static bool IntOMPostDeleteRegion(object oldRegion, object newRegion)
		{
			OMRegion omregion = oldRegion as OMRegion;
			OMNamedCacheStats stats = omregion.NamedCache.Stats;
			stats.DecrRegionCount();
			return true;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00002B16 File Offset: 0x00000D16
		private static bool IntOMPostClearRegion(object oldRegion, object newRegion)
		{
			return true;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00002B16 File Offset: 0x00000D16
		private static bool IntOMPostClearNamedCache(object oldCache, object newCache)
		{
			return true;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00041A18 File Offset: 0x0003FC18
		private static bool IntOMPostCreateNamedCache(object oldCache, object newCache)
		{
			OMNamedCache omnamedCache = newCache as OMNamedCache;
			OMCacheNodeStats nodeStats = omnamedCache.Stats.NodeStats;
			nodeStats.IncrNamedCacheCount();
			return true;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00041A40 File Offset: 0x0003FC40
		private static bool IntOMPostDeleteNamedCache(object oldCache, object newCache)
		{
			OMNamedCache omnamedCache = (OMNamedCache)oldCache;
			OMCacheNodeStats nodeStats = omnamedCache.Stats.NodeStats;
			nodeStats.DecrNamedCacheCount();
			return true;
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00041A67 File Offset: 0x0003FC67
		private bool OMPreCacheItemAdd(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[0] != null)
			{
				this._userPrePostOperations[0](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x00041A85 File Offset: 0x0003FC85
		private bool OMPostCacheItemAdd(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[1] != null)
			{
				this._userPrePostOperations[1](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostAdd(oldItem, newItem);
			return true;
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00041AAB File Offset: 0x0003FCAB
		private bool OMPreCacheItemRemove(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[4] != null)
			{
				this._userPrePostOperations[4](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00041AC9 File Offset: 0x0003FCC9
		private bool OMPostCacheItemRemove(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[5] != null)
			{
				this._userPrePostOperations[5](oldItem, null, opState);
			}
			ObjectManager.IntOMPostDelete(oldItem, null);
			return true;
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00041AEF File Offset: 0x0003FCEF
		private bool OMPostCacheItemForcedDelete(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[28] != null)
			{
				this._userPrePostOperations[28](oldItem, null, opState);
			}
			ObjectManager.IntOMPostForceDelete(oldItem, null);
			return true;
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00041B18 File Offset: 0x0003FD18
		private bool OMPostCacheItemInternalLockUpdate(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[31] != null)
			{
				this._userPrePostOperations[31](oldItem, newItem, opState);
			}
			AOMCacheItem aomcacheItem = oldItem as AOMCacheItem;
			if (aomcacheItem.IsLockPlaceHolderObject)
			{
				ObjectManager.IntOMCommonPostUpsertStatsUpdate(null, aomcacheItem);
			}
			return true;
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00041B5C File Offset: 0x0003FD5C
		private bool OMPostCacheItemForcedUnlock(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[32] != null)
			{
				this._userPrePostOperations[32](oldItem, newItem, opState);
			}
			AOMCacheItem aomcacheItem = oldItem as AOMCacheItem;
			if (aomcacheItem.IsLockPlaceHolderObject)
			{
				ObjectManager.IntOMCommonPostUpsertStatsUpdate(aomcacheItem, null);
			}
			return true;
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00041B9E File Offset: 0x0003FD9E
		private bool OMPreCacheItemPut(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[2] != null)
			{
				this._userPrePostOperations[2](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x00041BBC File Offset: 0x0003FDBC
		private bool OMPostCacheItemPut(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[3] != null)
			{
				this._userPrePostOperations[3](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostPut(oldItem, newItem);
			return true;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x00041BE2 File Offset: 0x0003FDE2
		private bool OMPreCacheItemInternalUpsert(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[29] != null)
			{
				this._userPrePostOperations[29](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x00041C02 File Offset: 0x0003FE02
		private bool OMPostCacheItemInternalUpsert(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[30] != null)
			{
				this._userPrePostOperations[30](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostPut(oldItem, newItem);
			return true;
		}

		// Token: 0x060015AA RID: 5546 RVA: 0x00041C2A File Offset: 0x0003FE2A
		private bool OMPreCacheItemInternalPutAndUnlock(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[36] != null)
			{
				this._userPrePostOperations[36](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015AB RID: 5547 RVA: 0x00041C4A File Offset: 0x0003FE4A
		private bool OMPostCacheItemInternalPutAndUnlock(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[37] != null)
			{
				this._userPrePostOperations[37](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostPut(oldItem, newItem);
			return true;
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x00041C72 File Offset: 0x0003FE72
		private bool OMPreCacheItemIncrementDecrement(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[38] != null)
			{
				this._userPrePostOperations[38](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x00041C92 File Offset: 0x0003FE92
		private bool OMPreCacheItemConcatenate(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[40] != null)
			{
				this._userPrePostOperations[40](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00041CB2 File Offset: 0x0003FEB2
		private bool OMPreCacheItemGetAndLock(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[18] != null)
			{
				this._userPrePostOperations[18](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00041CD2 File Offset: 0x0003FED2
		private bool OMPostCacheItemIncrementDecrement(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[39] != null)
			{
				this._userPrePostOperations[39](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostIncrementDecrement(oldItem, newItem);
			return true;
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00041CFA File Offset: 0x0003FEFA
		private bool OMPostCacheItemConcatenate(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[41] != null)
			{
				this._userPrePostOperations[41](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostConcatenate(oldItem, newItem);
			return true;
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00041D24 File Offset: 0x0003FF24
		private bool OMPostCacheItemGetAndLock(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[19] != null)
			{
				this._userPrePostOperations[19](oldItem, newItem, opState);
			}
			AOMCacheItem aomcacheItem = oldItem as AOMCacheItem;
			if (aomcacheItem.IsLockPlaceHolderObject)
			{
				ObjectManager.IntOMCommonPostUpsertStatsUpdate(null, aomcacheItem);
			}
			return true;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00041D68 File Offset: 0x0003FF68
		private bool OMPostCacheItemReadThroughLock(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[33] != null)
			{
				this._userPrePostOperations[33](null, newItem, opState);
			}
			AOMCacheItem aomcacheItem = oldItem as AOMCacheItem;
			ObjectManager.IntOMCommonPostUpsertStatsUpdate(null, aomcacheItem);
			return true;
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00041DA2 File Offset: 0x0003FFA2
		private bool OMPreCacheItemPutAndUnlock(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[20] != null)
			{
				this._userPrePostOperations[20](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00041DC2 File Offset: 0x0003FFC2
		private bool OMPostCacheItemPutAndUnlock(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[21] != null)
			{
				this._userPrePostOperations[21](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostPut(oldItem, newItem);
			return true;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00041DEA File Offset: 0x0003FFEA
		private bool OMPostCacheItemReadThroughPutAndUnlock(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[35] != null)
			{
				this._userPrePostOperations[35](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostPut(oldItem, newItem);
			return true;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00041E12 File Offset: 0x00040012
		private bool OMPreCacheItemResetTimeOut(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[22] != null)
			{
				this._userPrePostOperations[22](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00041E32 File Offset: 0x00040032
		private bool OMPostCacheItemResetTimeOut(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[23] != null)
			{
				this._userPrePostOperations[23](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00041E52 File Offset: 0x00040052
		private bool OMPreCacheItemUnlock(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[24] != null)
			{
				this._userPrePostOperations[24](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00041E74 File Offset: 0x00040074
		private bool OMPostCacheItemUnlock(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[25] != null)
			{
				this._userPrePostOperations[25](oldItem, newItem, opState);
			}
			AOMCacheItem aomcacheItem = oldItem as AOMCacheItem;
			if (aomcacheItem.IsLockPlaceHolderObject)
			{
				ObjectManager.IntOMCommonPostUpsertStatsUpdate(aomcacheItem, null);
			}
			return true;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00041EB8 File Offset: 0x000400B8
		private bool OMPostCacheItemReadThroughUnlock(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[34] != null)
			{
				this._userPrePostOperations[34](oldItem, null, opState);
			}
			AOMCacheItem aomcacheItem = oldItem as AOMCacheItem;
			ObjectManager.IntOMCommonPostUpsertStatsUpdate(aomcacheItem, null);
			return true;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00041EF2 File Offset: 0x000400F2
		private bool OMPreRegionCreate(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[12] != null)
			{
				this._userPrePostOperations[12](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00041F12 File Offset: 0x00040112
		private bool OMPostRegionCreate(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[13] != null)
			{
				this._userPrePostOperations[13](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostCreateRegion(oldItem, newItem);
			return true;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00041F3A File Offset: 0x0004013A
		private bool OMPreRegionDelete(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[14] != null)
			{
				this._userPrePostOperations[14](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x00041F5A File Offset: 0x0004015A
		private bool OMPostRegionDelete(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[15] != null)
			{
				this._userPrePostOperations[15](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostDeleteRegion(oldItem, newItem);
			return true;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00041F82 File Offset: 0x00040182
		private bool OMPreRegionClear(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[16] != null)
			{
				this._userPrePostOperations[16](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00041FA2 File Offset: 0x000401A2
		private bool OMPostRegionClear(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[17] != null)
			{
				this._userPrePostOperations[17](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostClearRegion(oldItem, newItem);
			return true;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00041FCA File Offset: 0x000401CA
		private bool OMPreNamedCacheCreate(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[6] != null)
			{
				this._userPrePostOperations[6](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00041FE8 File Offset: 0x000401E8
		private bool OMPostNamedCacheCreate(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[7] != null)
			{
				this._userPrePostOperations[7](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostCreateNamedCache(oldItem, newItem);
			return true;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x0004200E File Offset: 0x0004020E
		private bool OMPreNamedCacheDelete(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[8] != null)
			{
				this._userPrePostOperations[8](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0004202C File Offset: 0x0004022C
		private bool OMPostNamedCacheDelete(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[9] != null)
			{
				this._userPrePostOperations[9](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostDeleteNamedCache(oldItem, newItem);
			return true;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00042054 File Offset: 0x00040254
		private bool OMPreNamedCacheClear(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[10] != null)
			{
				this._userPrePostOperations[10](oldItem, newItem, opState);
			}
			return true;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00042074 File Offset: 0x00040274
		private bool OMPostNamedCacheClear(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[11] != null)
			{
				this._userPrePostOperations[11](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostClearNamedCache(oldItem, newItem);
			return true;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0004209C File Offset: 0x0004029C
		private bool OMPostCacheItemForcedUpsert(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[26] != null)
			{
				this._userPrePostOperations[26](oldItem, newItem, opState);
			}
			ObjectManager.IntOMPostForcedUpsert(oldItem, newItem);
			return true;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x000420C4 File Offset: 0x000402C4
		private bool OMPostCacheItemInternalDelete(object oldItem, object newItem, object opState)
		{
			if (this._userPrePostOperations[27] != null)
			{
				this._userPrePostOperations[27](oldItem, null, opState);
			}
			ObjectManager.IntOMPostInternalDelete(oldItem, null);
			return true;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000420EC File Offset: 0x000402EC
		private static DataCacheException GetException(int errorCode)
		{
			return new DataCacheException(ObjectManager.LogSource, errorCode, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, errorCode));
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00042104 File Offset: 0x00040304
		public ObjectManager(OMCacheNodeProperties scm)
		{
			this.Initialize(scm);
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0004211E File Offset: 0x0004031E
		public ObjectManager(OMCacheNodeProperties scm, IMemoryManager memoryManager, ICacheStatsContainer cacheStatsContainer)
		{
			this._memoryManager = memoryManager;
			this._statsContainer = cacheStatsContainer;
			this.Initialize(scm);
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x00042146 File Offset: 0x00040346
		internal ICacheItemFactory CacheItemFactory
		{
			get
			{
				return this._memoryManager.GetCacheItemFactory();
			}
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00042153 File Offset: 0x00040353
		public OMNamedCache GetNamedCache(string cacheName)
		{
			return (OMNamedCache)this._namedCacheTable.Get(cacheName);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00042166 File Offset: 0x00040366
		public OMRegion GetRegion(string cacheName, string regionName)
		{
			if (regionName == null || cacheName == null)
			{
				return null;
			}
			return (OMRegion)this._cache.Get(new CacheRegion(cacheName, regionName));
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00042188 File Offset: 0x00040388
		public OMRegion GetRegion(RequestBody request, string cacheName, string regionName)
		{
			if (request == null || request.Region == null)
			{
				return this.GetRegion(cacheName, regionName);
			}
			OMRegion omregion = (OMRegion)request.Region;
			if (!omregion.IsDeleted)
			{
				return omregion;
			}
			return null;
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000421C0 File Offset: 0x000403C0
		private IContainerSchema PrepareSchema()
		{
			IContainerSchema containerSchema = DataManagerFactory.CreateContainerSchema();
			IStoreSchema storeSchema = DataManagerFactory.CreateStoreSchema();
			containerSchema.AddCallBack(DMCallBackType.PreAdd, new DMOperationCallBack(this.OMPreRegionCreate));
			containerSchema.AddCallBack(DMCallBackType.PostAdd, new DMOperationCallBack(this.OMPostRegionCreate));
			containerSchema.AddCallBack(DMCallBackType.PreDelete, new DMOperationCallBack(this.OMPreRegionDelete));
			containerSchema.AddCallBack(DMCallBackType.PostDelete, new DMOperationCallBack(this.OMPostRegionDelete));
			containerSchema.AddCallBack(DMCallBackType.PreUpsert, new DMOperationCallBack(this.OMPreRegionClear));
			containerSchema.AddCallBack(DMCallBackType.PostUpsert, new DMOperationCallBack(this.OMPostRegionClear));
			containerSchema.BaseStoreSchema = storeSchema;
			return containerSchema;
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00042254 File Offset: 0x00040454
		public OMOperationCallBack RegisterCallBack(OMCallBackType type, OMOperationCallBack callBack)
		{
			OMOperationCallBack omoperationCallBack = this._userPrePostOperations[(int)type];
			this._userPrePostOperations[(int)type] = callBack;
			return omoperationCallBack;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00042274 File Offset: 0x00040474
		public OMOperationCallBack UnRegisterCallBack(OMCallBackType type)
		{
			OMOperationCallBack omoperationCallBack = this._userPrePostOperations[(int)type];
			this._userPrePostOperations[(int)type] = null;
			return omoperationCallBack;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00041870 File Offset: 0x0003FA70
		public OMCacheNodeStats GetStats()
		{
			return this._stats;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00042294 File Offset: 0x00040494
		public string[] ListAllNamedCaches()
		{
			List<string> list = new List<string>(this.Props.MaxNamedCacheCount);
			foreach (object obj in this._namedCacheTable.Keys)
			{
				string text = (string)obj;
				list.Add(text);
			}
			return list.ToArray();
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0004230C File Offset: 0x0004050C
		public void CreateNamedCache(string cacheName, INamedCacheConfiguration config, StoreProperties storeProperties)
		{
			CreateNamedCacheCallback createNamedCacheCallback = () => new OMNamedCache(this._cache, cacheName, config, this._props.OType, this._prePostOperations, this._stats, this);
			this.CreateNamedCache(cacheName, createNamedCacheCallback, null);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00042350 File Offset: 0x00040550
		public void CreateNamedCache(string cacheName, OMNamedCacheProperties properties)
		{
			properties.OType = this._props.OType;
			CreateNamedCacheCallback createNamedCacheCallback = () => new OMNamedCache(this._cache, cacheName, properties, this._prePostOperations, this._stats, this);
			this.CreateNamedCache(cacheName, createNamedCacheCallback, null);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x000423AC File Offset: 0x000405AC
		private void CreateNamedCache(string cacheName, CreateNamedCacheCallback createCallback, object opState)
		{
			lock (this._namedCacheTable)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(ObjectManager.LogSource, "Trying to create {0}.", new object[] { cacheName });
				}
				if (this._namedCacheTable.ContainsKey(cacheName))
				{
					throw ObjectManager.GetException(3005);
				}
				if (!CloudUtility.IsVASDeployment && this._namedCacheTable.Keys.Count >= this.Props.MaxNamedCacheCount)
				{
					throw ObjectManager.GetException(3004);
				}
				OMNamedCache omnamedCache = createCallback();
				if (this.OMPreNamedCacheCreate(null, omnamedCache, opState))
				{
					this._namedCacheTable.Add(cacheName, omnamedCache);
					this.OMPostNamedCacheCreate(null, omnamedCache, opState);
				}
			}
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x0004247C File Offset: 0x0004067C
		public OMNamedCache GetAndLockCache()
		{
			lock (this._namedCacheTable)
			{
				foreach (object obj in this._namedCacheTable)
				{
					OMNamedCache omnamedCache = (OMNamedCache)obj;
					OMCacheStoreState storeState = omnamedCache.GetStoreState();
					if (storeState.WriteBehindEnabled && !storeState.WriteInProgress && storeState.LastWriteOlderThan(1) && storeState.TryEnterWriteLock())
					{
						return omnamedCache;
					}
				}
			}
			return null;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00042530 File Offset: 0x00040730
		public void UnlockCache(OMNamedCache cache)
		{
			lock (this._namedCacheTable)
			{
				OMCacheStoreState storeState = cache.GetStoreState();
				storeState.ReleaseWriteLock();
			}
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00042578 File Offset: 0x00040778
		public void DropNamedCache(string cacheName)
		{
			this.DeleteNamedCache(cacheName, null, true);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00042583 File Offset: 0x00040783
		public void DeleteNamedCache(string cacheName)
		{
			this.DeleteNamedCache(cacheName, null, false);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0004258E File Offset: 0x0004078E
		public void DeleteNamedCache(string cacheName, object opState)
		{
			this.DeleteNamedCache(cacheName, opState, false);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x0004259C File Offset: 0x0004079C
		private void DeleteNamedCache(string cacheName, object opState, bool syncDelete)
		{
			lock (this._namedCacheTable)
			{
				OMNamedCache omnamedCache = (OMNamedCache)this._namedCacheTable.Get(cacheName);
				if (omnamedCache != null)
				{
					if (this.OMPreNamedCacheDelete(omnamedCache, null, opState))
					{
						this._namedCacheTable.Delete(cacheName);
						omnamedCache.DeleteAllRegions(opState, syncDelete);
						this.OMPostNamedCacheDelete(omnamedCache, null, opState);
						omnamedCache.Stats.Close(omnamedCache.CacheName);
					}
					return;
				}
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00042634 File Offset: 0x00040834
		public bool ContainsNamedCache(string cacheName)
		{
			return this._namedCacheTable.ContainsKey(cacheName);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00042642 File Offset: 0x00040842
		public void ClearNamedCache(string cacheName)
		{
			this.ClearNamedCache(cacheName, null);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0004264C File Offset: 0x0004084C
		public void ClearNamedCache(string cacheName, object opState)
		{
			lock (this._namedCacheTable)
			{
				OMNamedCache omnamedCache = (OMNamedCache)this._namedCacheTable.Get(cacheName);
				if (omnamedCache != null)
				{
					OMNamedCache omnamedCache2 = omnamedCache.Clear();
					if (this.OMPreNamedCacheClear(omnamedCache, omnamedCache2, opState))
					{
						omnamedCache.DeleteAllRegions(opState);
						this._namedCacheTable.Upsert(cacheName, omnamedCache2);
						this.OMPostNamedCacheClear(omnamedCache, omnamedCache2, opState);
					}
					return;
				}
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x000426DC File Offset: 0x000408DC
		public OMNamedCacheStats GetNamedCacheStats(string cacheName)
		{
			if (cacheName == null)
			{
				return null;
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				return namedCache.Stats;
			}
			return null;
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00042704 File Offset: 0x00040904
		public IEnumerable<KeyValuePair<string, OMNamedCacheStats>> GetAllNamedCacheStats()
		{
			List<KeyValuePair<string, OMNamedCacheStats>> list = new List<KeyValuePair<string, OMNamedCacheStats>>();
			foreach (object obj in this._namedCacheTable)
			{
				OMNamedCache omnamedCache = (OMNamedCache)obj;
				list.Add(new KeyValuePair<string, OMNamedCacheStats>(omnamedCache.CacheName, omnamedCache.Stats));
			}
			return list;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00042774 File Offset: 0x00040974
		public string[] ListAllRegions(string cacheName)
		{
			return this.ListAllRegions(cacheName, int.MaxValue);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00042784 File Offset: 0x00040984
		public long ActualItemCount()
		{
			long num = 0L;
			foreach (object obj in this._namedCacheTable)
			{
				OMNamedCache omnamedCache = (OMNamedCache)obj;
				num += omnamedCache.ActualItemCount();
			}
			return num;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x000427E4 File Offset: 0x000409E4
		public string[] ListAllRegions(string cacheName, int maxRegions)
		{
			OMNamedCache omnamedCache = (OMNamedCache)this._namedCacheTable.Get(cacheName);
			if (omnamedCache != null)
			{
				return omnamedCache.ListAllRegions(maxRegions);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00042818 File Offset: 0x00040A18
		public void RefreshNamedCache(string cacheName, INamedCacheConfiguration config)
		{
			lock (this._namedCacheTable)
			{
				OMNamedCache omnamedCache = (OMNamedCache)this._namedCacheTable.Get(cacheName);
				if (omnamedCache != null)
				{
					omnamedCache.ReCreateCacheStoreState(cacheName, config.BackingStore);
					this._namedCacheTable.Upsert(cacheName, omnamedCache);
				}
			}
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00042884 File Offset: 0x00040A84
		public int GetRegionCount(string cacheName)
		{
			OMNamedCache omnamedCache = (OMNamedCache)this._namedCacheTable.Get(cacheName);
			if (omnamedCache != null)
			{
				return omnamedCache.RegionCount;
			}
			return -1;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000428AE File Offset: 0x00040AAE
		public void DeleteRegion(string cacheName, string regionName)
		{
			this.DeleteRegion(cacheName, regionName, null);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x000428B9 File Offset: 0x00040AB9
		public void DeleteRegion(string cacheName, string regionName, object opState)
		{
			this.DeleteRegionInternal(cacheName, regionName, opState, false);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000428C6 File Offset: 0x00040AC6
		public long DropRegion(string cacheName, string regionName)
		{
			return this.DeleteRegionInternal(cacheName, regionName, null, true);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000428D4 File Offset: 0x00040AD4
		private long DeleteRegionInternal(string cacheName, string regionName, object opState, bool syncDelete)
		{
			long num2;
			lock (this._namedCacheTable)
			{
				OMRegion region = this.GetRegion(cacheName, regionName);
				if (region != null)
				{
					OMNamedCache namedCache = region.NamedCache;
					if (namedCache == null)
					{
						if (Provider.IsEnabled(TraceLevel.Error))
						{
							EventLogWriter.WriteError(ObjectManager.LogSource, "Fatal Error:Region exists without named cacheRegionName = {0}:CacheName = {1}", new object[] { regionName, cacheName });
						}
						throw ObjectManager.GetException(3001);
					}
					long num = namedCache.DeleteRegion(region, opState, syncDelete);
					namedCache.Stats.IncrTotalReqs();
					num2 = num;
				}
				else
				{
					OMNamedCache namedCache2 = this.GetNamedCache(cacheName);
					if (namedCache2 != null)
					{
						namedCache2.Stats.IncrTotalReqs();
						throw ObjectManager.GetException(3002);
					}
					throw ObjectManager.GetException(3006);
				}
			}
			return num2;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000429A8 File Offset: 0x00040BA8
		public void ClearRegion(RequestBody request, string cacheName, string regionName)
		{
			lock (this._namedCacheTable)
			{
				OMRegion region = this.GetRegion(request, cacheName, regionName);
				if (region != null)
				{
					OMNamedCache namedCache = region.NamedCache;
					if (namedCache == null)
					{
						if (Provider.IsEnabled(TraceLevel.Error))
						{
							EventLogWriter.WriteError(ObjectManager.LogSource, "Fatal Error:Region exists without named cacheRegionName = {0}:CacheName = {1}", new object[] { regionName, cacheName });
						}
						throw ObjectManager.GetException(3001);
					}
					namedCache.ClearRegion(region, (request == null) ? null : request.Partition);
				}
				else
				{
					if (this.ContainsNamedCache(cacheName))
					{
						throw ObjectManager.GetException(3002);
					}
					throw ObjectManager.GetException(3006);
				}
			}
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00042A68 File Offset: 0x00040C68
		public bool ContainsRegion(string cacheName, string regionName)
		{
			CacheRegion cacheRegion = new CacheRegion(cacheName, regionName);
			return this._cache.Get(cacheRegion) != null;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00042A90 File Offset: 0x00040C90
		public OMRegionStats GetRegionStats(string cacheName, string regionName)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				return region.Stats;
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(ObjectManager.LogSource, "GetRegionStats::Region doesnot exist cacheName = {0} regionName = {1}", new object[] { cacheName, regionName });
			}
			return null;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00042AD8 File Offset: 0x00040CD8
		public bool Remove(RequestBody request, string cacheName, string regionName, object key, InternalCacheItemVersion ver, object opState)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.NamedCache.Stats.IncrWriteRequestBy(1L);
				if (request != null && request.protocolType == ProtocolType.Rest)
				{
					region.NamedCache.Stats.IncrTotalRestReqs();
				}
				AOMCacheItem aomcacheItem = region.Remove(key, ver, opState);
				if (aomcacheItem != null)
				{
					return true;
				}
				region.Stats.IncrMiss();
				return false;
			}
			else
			{
				OMNamedCache namedCache = this.GetNamedCache(cacheName);
				if (namedCache != null)
				{
					namedCache.Stats.IncrDelReqs();
					namedCache.Stats.IncrMiss();
					namedCache.Stats.IncrWriteRequestBy(1L);
					if (request != null && request.protocolType == ProtocolType.Rest)
					{
						namedCache.Stats.IncrTotalRestReqs();
					}
					throw ObjectManager.GetException(3002);
				}
				this.Stats.IncrDelReqs();
				this.Stats.IncrMiss();
				if (request != null && request.protocolType == ProtocolType.Rest)
				{
					this.Stats.IncrRestTotalReqs();
				}
				throw ObjectManager.GetException(3006);
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00042BCC File Offset: 0x00040DCC
		public object InternalDelete(string cacheName, string regionName, object key, InternalCacheItemVersion ver, object opState)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				AOMCacheItem aomcacheItem = region.InternalDelete(key, ver, opState);
				if (aomcacheItem != null)
				{
					return aomcacheItem.Value;
				}
				region.Stats.IncrMiss();
			}
			else
			{
				OMNamedCache namedCache = this.GetNamedCache(cacheName);
				if (namedCache != null)
				{
					namedCache.Stats.IncrDelReqs();
					namedCache.Stats.IncrMiss();
				}
				else
				{
					this.Stats.IncrDelReqs();
					this.Stats.IncrMiss();
				}
			}
			return null;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00042C44 File Offset: 0x00040E44
		public bool Remove(RequestBody request, string cacheName, string regionName, object key, DataCacheLockHandle lockHandle)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.NamedCache.Stats.IncrWriteRequestBy(1L);
				if (request != null && request.protocolType == ProtocolType.Rest)
				{
					region.NamedCache.Stats.IncrTotalRestReqs();
				}
				AOMCacheItem aomcacheItem = region.Remove(key, lockHandle, request);
				if (aomcacheItem != null)
				{
					return true;
				}
				region.Stats.IncrMiss();
				return false;
			}
			else
			{
				OMNamedCache namedCache = this.GetNamedCache(cacheName);
				if (namedCache != null)
				{
					namedCache.Stats.IncrDelReqs();
					namedCache.Stats.IncrMiss();
					namedCache.Stats.IncrWriteRequestBy(1L);
					if (request != null && request.protocolType == ProtocolType.Rest)
					{
						namedCache.Stats.IncrTotalRestReqs();
					}
					throw ObjectManager.GetException(3002);
				}
				this.Stats.IncrDelReqs();
				this.Stats.IncrMiss();
				if (request != null && request.protocolType == ProtocolType.Rest)
				{
					this.Stats.IncrRestTotalReqs();
				}
				throw ObjectManager.GetException(3006);
			}
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00042D38 File Offset: 0x00040F38
		public void Put(RequestBody request, string cacheName, string regionName, object key, object value, DateTime timeToLive, object[] tags, ProtocolType protocolType, ref InternalCacheItemVersion version)
		{
			version = this.PutReplaceInternal(cacheName, regionName, key, request, value, timeToLive, tags, version, false, protocolType);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00042D68 File Offset: 0x00040F68
		public void Replace(RequestBody request, string cacheName, string regionName, object key, object value, DateTime timeToLive, object[] tags, ref InternalCacheItemVersion version)
		{
			version = this.PutReplaceInternal(cacheName, regionName, key, request, value, timeToLive, tags, version, true, ProtocolType.Memcache);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00042D98 File Offset: 0x00040F98
		public object Add(RequestBody request, string cacheName, string regionName, object key, object value, DateTime timeToLive, object[] tags)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.NamedCache.Stats.IncrWriteRequestBy(1L);
				try
				{
					AOMCacheItem aomcacheItem = region.Add(key, value, timeToLive, tags, request);
					return aomcacheItem.Version;
				}
				catch (DataCacheException)
				{
					region.Stats.IncrMiss();
					throw;
				}
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				if (!RegionNameProvider.IsSystemRegion(regionName))
				{
					namedCache.Stats.IncrAddReqs();
					namedCache.Stats.IncrMiss();
					namedCache.Stats.IncrWriteRequestBy(1L);
				}
				throw ObjectManager.GetException(3002);
			}
			this.Stats.IncrAddReqs();
			this.Stats.IncrMiss();
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00042E64 File Offset: 0x00041064
		public object Get(string cacheName, string regionName, object key, ref InternalCacheItemVersion ver)
		{
			IOMCacheItem cacheItem = this.GetCacheItem(null, cacheName, regionName, key, ver);
			if (cacheItem != null)
			{
				ver = cacheItem.Version;
				return cacheItem.Value;
			}
			return null;
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00042E9C File Offset: 0x0004109C
		public object IncrementDecrement(string cacheName, string regionName, object key, object value, object initialValue, DateTime ttl, SerializationCategory serializationCategory, InternalCacheItemVersion version, object opState)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				region.NamedCache.Stats.IncrWriteRequestBy(1L);
				try
				{
					AOMCacheItem aomcacheItem = region.IncrementDecrement(key, value, initialValue, opState, ttl, serializationCategory, version);
					return aomcacheItem.Value;
				}
				catch (DataCacheException)
				{
					region.Stats.IncrMiss();
					throw;
				}
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				if (!RegionNameProvider.IsSystemRegion(regionName))
				{
					namedCache.Stats.IncrMiss();
					namedCache.Stats.IncrWriteRequestBy(1L);
				}
				throw ObjectManager.GetException(3002);
			}
			this.Stats.IncrAddReqs();
			this.Stats.IncrMiss();
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00042F5C File Offset: 0x0004115C
		public object Concatenate(string cacheName, string regionName, object key, object value, bool isAppend, DateTime ttl, SerializationCategory serializationCategory, InternalCacheItemVersion version, object opState)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				region.NamedCache.Stats.IncrWriteRequestBy(1L);
				try
				{
					AOMCacheItem aomcacheItem = region.Concatenate(key, value, isAppend, opState, ttl, serializationCategory, version);
					return aomcacheItem.Value;
				}
				catch (DataCacheException)
				{
					region.Stats.IncrMiss();
					throw;
				}
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				if (!RegionNameProvider.IsSystemRegion(regionName))
				{
					namedCache.Stats.IncrMiss();
					namedCache.Stats.IncrWriteRequestBy(1L);
				}
				throw ObjectManager.GetException(3002);
			}
			this.Stats.IncrAddReqs();
			this.Stats.IncrMiss();
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x0004301C File Offset: 0x0004121C
		public IOMCacheItem GetCacheItem(RequestBody request, string cacheName, string regionName, object key, InternalCacheItemVersion ver)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.NamedCache.Stats.IncrReadRequestBy(1L);
				AOMCacheItem aomcacheItem = region.Get(key);
				if (request != null && request.protocolType == ProtocolType.Rest)
				{
					region.NamedCache.Stats.IncrTotalRestReqs();
				}
				region.Stats.IncrGetReqs();
				if (aomcacheItem != null)
				{
					if (!(ver != InternalCacheItemVersion.Null))
					{
						region.NamedCache.Stats.IncrObjectReturnedCountBy(1L);
						return aomcacheItem;
					}
					if (aomcacheItem.Version.Equals(ver))
					{
						region.NamedCache.Stats.IncrObjectReturnedCountBy(1L);
						return aomcacheItem;
					}
				}
				region.Stats.IncrMiss();
				region.NamedCache.Stats.IncrementGetRequestMiss();
				return null;
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				if (request != null && request.protocolType == ProtocolType.Rest)
				{
					namedCache.Stats.IncrTotalRestReqs();
				}
				namedCache.Stats.IncrGetReqs();
				namedCache.Stats.IncrMiss();
				namedCache.Stats.IncrReadRequestBy(1L);
				namedCache.Stats.IncrementGetRequestMiss();
				throw ObjectManager.GetException(3002);
			}
			this.Stats.IncrGetReqs();
			this.Stats.IncrMiss();
			if (request != null && request.protocolType == ProtocolType.Rest)
			{
				this.Stats.IncrRestTotalReqs();
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00043178 File Offset: 0x00041378
		public void Commit(RequestBody request, string cacheName, string regionName, object key, AOMCacheItem item)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				if (region.ConsistencyType == ConsistencyType.EventualConsistency)
				{
					return;
				}
				region.Commit(key, item);
				return;
			}
			else
			{
				if (this.ContainsNamedCache(cacheName))
				{
					throw ObjectManager.GetException(3002);
				}
				throw ObjectManager.GetException(3006);
			}
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000431C8 File Offset: 0x000413C8
		public EnumeratorState GetStatelessEnumeratorState(RequestBody request, string cacheName, string regionName)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				return region.GetStatelessEnumeratorState();
			}
			if (this.ContainsNamedCache(cacheName))
			{
				throw ObjectManager.GetException(3002);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00043208 File Offset: 0x00041408
		public EnumeratorState FindIntersectionAllWithStatelessEnumerator(RequestBody request, string cacheName, string regionName, DataCacheTag[] tags)
		{
			EnumeratorState enumeratorState = this.FindWithStatelessEnumerator(request, cacheName, regionName, tags[tags.Length - 1]);
			if (tags.Length > 1)
			{
				DataCacheTag[] array = new DataCacheTag[tags.Length - 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = tags[i];
				}
				enumeratorState = new OmEnumeratorStateForTagsIntersection(enumeratorState, array);
			}
			return enumeratorState;
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0004325C File Offset: 0x0004145C
		public EnumeratorState FindWithStatelessEnumerator(RequestBody request, string cacheName, string regionName, DataCacheTag tag)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				return region.FindWithStatelessEnumerator(tag);
			}
			if (this.ContainsNamedCache(cacheName))
			{
				throw ObjectManager.GetException(3002);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000432A0 File Offset: 0x000414A0
		public EnumeratorState FindUnionAllWithStatelessEnumerator(RequestBody request, string cacheName, string regionName, DataCacheTag[] tags)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				return region.FindUnionAllWithStatelessEnumerator(tags);
			}
			if (this.ContainsNamedCache(cacheName))
			{
				throw ObjectManager.GetException(3002);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000432E4 File Offset: 0x000414E4
		public List<KeyValuePair<string, object>> GetBatch(RequestBody request, string cacheName, string regionName, int batchSize, EnumeratorState state, ref bool toContinue)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.NamedCache.Stats.IncrTotalReqs();
				region.NamedCache.Stats.IncrReadRequestBy(1L);
				OmEnumeratorStateForTagsIntersection omEnumeratorStateForTagsIntersection = state as OmEnumeratorStateForTagsIntersection;
				SizeBasedScannerForUser sizeBasedScannerForUser;
				if (omEnumeratorStateForTagsIntersection == null)
				{
					sizeBasedScannerForUser = new SizeBasedScannerForUser(batchSize);
				}
				else
				{
					sizeBasedScannerForUser = new SizeBasedScannerForTagIntersection(batchSize, omEnumeratorStateForTagsIntersection.Tags);
					state = omEnumeratorStateForTagsIntersection.EnumState;
				}
				toContinue = !region.GetBatchFromRegion(sizeBasedScannerForUser, state);
				region.NamedCache.Stats.IncrObjectReturnedCountBy((long)sizeBasedScannerForUser.Batch.Count);
				return sizeBasedScannerForUser.Batch;
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				namedCache.Stats.IncrTotalReqs();
				namedCache.Stats.IncrReadRequestBy(1L);
				throw ObjectManager.GetException(3002);
			}
			this.Stats.IncrTotalReqs();
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x000433C0 File Offset: 0x000415C0
		public List<string> GetKeyBatch(RequestBody request, string cacheName, string regionName, int batchSize, EnumeratorState state, ref bool toContinue)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.NamedCache.Stats.IncrTotalReqs();
				region.NamedCache.Stats.IncrReadRequestBy(1L);
				OmEnumeratorStateForTagsIntersection omEnumeratorStateForTagsIntersection = state as OmEnumeratorStateForTagsIntersection;
				SizeBasedScannerForUser sizeBasedScannerForUser;
				if (omEnumeratorStateForTagsIntersection == null)
				{
					sizeBasedScannerForUser = new SizeBasedScannerForUser(batchSize, true);
				}
				else
				{
					sizeBasedScannerForUser = new SizeBasedScannerForTagIntersection(batchSize, omEnumeratorStateForTagsIntersection.Tags, true);
					state = omEnumeratorStateForTagsIntersection.EnumState;
				}
				toContinue = !region.GetBatchFromRegion(sizeBasedScannerForUser, state);
				region.NamedCache.Stats.IncrObjectReturnedCountBy((long)sizeBasedScannerForUser.KeyBatch.Count);
				return sizeBasedScannerForUser.KeyBatch;
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				namedCache.Stats.IncrTotalReqs();
				namedCache.Stats.IncrReadRequestBy(1L);
				throw ObjectManager.GetException(3002);
			}
			this.Stats.IncrTotalReqs();
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x000434A4 File Offset: 0x000416A4
		public DataCacheLockHandle ReadThroughLock(string cacheName, string regionName, object key, TimeSpan lockTimeOut, OMRegion cachedRegion, object opState)
		{
			OMRegion omregion = cachedRegion ?? this.GetRegion(cacheName, regionName);
			if (omregion != null)
			{
				AOMCacheItem aomcacheItem = omregion.ReadThroughLock(key, lockTimeOut, opState);
				return aomcacheItem.GetLockHandle();
			}
			if (this.GetNamedCache(cacheName) == null)
			{
				throw ObjectManager.GetException(3006);
			}
			throw ObjectManager.GetException(3007);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x000434F8 File Offset: 0x000416F8
		public void ReadThroughUnlock(string cacheName, string regionName, object key, DataCacheLockHandle lHandle, object opState)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				region.ReadThroughUnlock(key, lHandle, opState);
				return;
			}
			if (this.GetNamedCache(cacheName) == null)
			{
				throw ObjectManager.GetException(3006);
			}
			throw ObjectManager.GetException(3002);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00043540 File Offset: 0x00041740
		public object ReadThroughPutAndUnlock(string cacheName, string regionName, object key, object value, DateTime timeToLive, object[] tags, DataCacheLockHandle lHandle, object opState)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				return region.ReadThroughPutAndLock(key, value, timeToLive, tags, lHandle, opState);
			}
			if (this.GetNamedCache(cacheName) == null)
			{
				throw ObjectManager.GetException(3006);
			}
			throw ObjectManager.GetException(3002);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0004358C File Offset: 0x0004178C
		public object GetAndLock(RequestBody request, string cacheName, string regionName, object key, TimeSpan lockTimeOut, ref DataCacheLockHandle lHandle, bool lockKey)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.Stats.IncrTotalReqs();
				region.NamedCache.Stats.IncrGetAndLock();
				region.NamedCache.Stats.IncrReadRequestBy(1L);
				try
				{
					AOMCacheItem andLock = region.GetAndLock(key, lockTimeOut, lockKey, request);
					region.NamedCache.Stats.IncrSuccessfulGetAndLock();
					if (andLock != null)
					{
						lHandle = andLock.GetLockHandle();
						if (andLock.Value == null)
						{
							region.Stats.IncrMiss();
						}
						else
						{
							region.NamedCache.Stats.IncrObjectReturnedCountBy(1L);
						}
						return andLock.Value;
					}
					region.Stats.IncrMiss();
					return null;
				}
				catch (DataCacheException)
				{
					region.Stats.IncrMiss();
					throw;
				}
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				if (!RegionNameProvider.IsSystemRegion(regionName) || !lockKey)
				{
					namedCache.Stats.IncrTotalReqs();
					namedCache.Stats.IncrMiss();
					namedCache.Stats.IncrGetAndLock();
					namedCache.Stats.IncrReadRequestBy(1L);
				}
				throw ObjectManager.GetException(3002);
			}
			this.Stats.IncrTotalReqs();
			this.Stats.IncrMiss();
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x000436D4 File Offset: 0x000418D4
		public void PutAndUnlock(RequestBody request, string cacheName, string regionName, object key, object value, DateTime timeToLive, object[] tags, DataCacheLockHandle lHandle)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.NamedCache.Stats.IncrWriteRequestBy(1L);
				try
				{
					region.PutAndUnlock(key, value, timeToLive, tags, lHandle, request);
					return;
				}
				catch (DataCacheException)
				{
					region.Stats.IncrMiss();
					throw;
				}
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				namedCache.Stats.IncrUpsertReqs();
				namedCache.Stats.IncrMiss();
				namedCache.Stats.IncrWriteRequestBy(1L);
				throw ObjectManager.GetException(3002);
			}
			this.Stats.IncrUpsertReqs();
			this.Stats.IncrMiss();
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0004378C File Offset: 0x0004198C
		public bool Unlock(RequestBody request, string cacheName, string regionName, object key, DataCacheLockHandle lHandle, DateTime ttl)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.Stats.IncrTotalReqs();
				try
				{
					return region.Unlock(key, lHandle, ttl, request);
				}
				catch (DataCacheException)
				{
					region.Stats.IncrMiss();
					throw;
				}
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				namedCache.Stats.IncrTotalReqs();
				namedCache.Stats.IncrMiss();
				throw ObjectManager.GetException(3002);
			}
			this.Stats.IncrTotalReqs();
			this.Stats.IncrMiss();
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00043830 File Offset: 0x00041A30
		public bool ResetTimeOut(RequestBody request, string cacheName, string regionName, object key, DateTime timeOut)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.Stats.IncrTotalReqs();
				try
				{
					return region.ResetTimeOut(key, timeOut, request);
				}
				catch (DataCacheException)
				{
					region.Stats.IncrMiss();
					throw;
				}
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				namedCache.Stats.IncrTotalReqs();
				namedCache.Stats.IncrMiss();
				throw ObjectManager.GetException(3002);
			}
			this.Stats.IncrTotalReqs();
			this.Stats.IncrMiss();
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000438D0 File Offset: 0x00041AD0
		public IOMCacheItem GetIfNewer(RequestBody request, string cacheName, string regionName, object key, InternalCacheItemVersion version, out bool oldVersionExists)
		{
			oldVersionExists = false;
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.NamedCache.Stats.IncrTotalReqs();
				region.NamedCache.Stats.IncrReadRequestBy(1L);
				AOMCacheItem aomcacheItem = region.Get(key);
				if (aomcacheItem != null)
				{
					oldVersionExists = true;
					if (version.CompareTo(aomcacheItem.Version) < 0)
					{
						region.NamedCache.Stats.IncrObjectReturnedCountBy(1L);
						return aomcacheItem;
					}
				}
				region.Stats.IncrMiss();
				return null;
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				namedCache.Stats.IncrTotalReqs();
				namedCache.Stats.IncrMiss();
				namedCache.Stats.IncrReadRequestBy(1L);
				throw ObjectManager.GetException(3002);
			}
			this.Stats.IncrMiss();
			this.Stats.IncrTotalReqs();
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000439AC File Offset: 0x00041BAC
		public void ForceUpsert(string cacheName, string regionName, object key, object value, InternalCacheItemVersion ver, DateTime ttl, DataCacheTag[] tags, object opState)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				region.Stats.NamedCacheStats.IncrWriteRequestBy(1L);
				region.ForceUpsert(key, value, ver, ttl, tags, opState);
				return;
			}
			if (this.ContainsNamedCache(cacheName))
			{
				throw ObjectManager.GetException(3002);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00043A08 File Offset: 0x00041C08
		public void ForceUpsertOMCacheItem(OMCacheItem cacheItem, object opState)
		{
			OMRegion region = this.GetRegion(cacheItem.CacheName, cacheItem.RegionName);
			if (region != null)
			{
				AOMCacheItem cacheItem2 = this.CacheItemFactory.GetCacheItem(cacheItem, region);
				region.Stats.NamedCacheStats.IncrWriteRequestBy(1L);
				region.ForceUpsert(cacheItem2, opState);
				return;
			}
			if (this.ContainsNamedCache(cacheItem.CacheName))
			{
				throw ObjectManager.GetException(3002);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00043A78 File Offset: 0x00041C78
		public void ForceUpsert(IOMCacheItem cacheItem, object opState)
		{
			OMRegion region = this.GetRegion(cacheItem.CacheName, cacheItem.RegionName);
			if (region != null)
			{
				cacheItem.Region = region;
				region.Stats.NamedCacheStats.IncrWriteRequestBy(1L);
				region.ForceUpsert(cacheItem, opState);
				return;
			}
			if (this.ContainsNamedCache(cacheItem.CacheName))
			{
				throw ObjectManager.GetException(3002);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00043AE0 File Offset: 0x00041CE0
		public object GetState(string cacheName, string regionName)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				return region.State;
			}
			return null;
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00043B04 File Offset: 0x00041D04
		public List<AOMCacheItem> GetBatch(string cacheName, string regionName, int batchSize, EnumeratorState state, InternalCacheItemVersion version, ref bool toContinue)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				SizeBasedScannerForCopy sizeBasedScannerForCopy = new SizeBasedScannerForCopy(batchSize, version);
				toContinue = !region.GetBatchFromRegion(sizeBasedScannerForCopy, state);
				return sizeBasedScannerForCopy.Batch;
			}
			if (this.ContainsNamedCache(cacheName))
			{
				throw ObjectManager.GetException(3002);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00043B5C File Offset: 0x00041D5C
		public AOMCacheItem ForceDelete(RequestBody request, string cacheName, string regionName, object key)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.Stats.NamedCacheStats.IncrWriteRequestBy(1L);
				return region.ForceDelete(key, (request == null) ? null : request.Partition);
			}
			if (this.ContainsNamedCache(cacheName))
			{
				throw ObjectManager.GetException(3002);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00043BBC File Offset: 0x00041DBC
		public void ForceLockUpdate(RequestBody request, string cacheName, string regionName, object key, DataCacheLockHandle handle, TimeSpan timeout, bool lockKey, InternalCacheItemVersion version)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.ForceLockUpdate(key, handle, timeout, lockKey, version, request);
				return;
			}
			if (this.ContainsNamedCache(cacheName))
			{
				throw ObjectManager.GetException(3002);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00043C08 File Offset: 0x00041E08
		public void ForceUnlock(RequestBody request, string cacheName, string regionName, object key, DataCacheLockHandle handle, DateTime timeout)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.ForceUnlock(key, handle, timeout, request);
				return;
			}
			if (this.ContainsNamedCache(cacheName))
			{
				throw ObjectManager.GetException(3002);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00043C50 File Offset: 0x00041E50
		public void ForceResetTimeout(RequestBody request, string cacheName, string regionName, object key, DateTime timeout)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.ForceResetTimeout(key, timeout);
				return;
			}
			if (this.ContainsNamedCache(cacheName))
			{
				throw ObjectManager.GetException(3002);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00043C94 File Offset: 0x00041E94
		public void CreateRegion(string cacheName, string regionName, OMRegionProperties props, object state, object opState)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				region.Stats.NamedCacheStats.IncrTotalReqs();
				throw ObjectManager.GetException(3003);
			}
			lock (this._namedCacheTable)
			{
				OMNamedCache namedCache = this.GetNamedCache(cacheName);
				if (namedCache == null)
				{
					throw ObjectManager.GetException(3006);
				}
				namedCache.CreateRegion(this._dm, regionName, props, state, opState);
				if (!RegionNameProvider.IsSystemRegion(regionName))
				{
					namedCache.Stats.IncrTotalReqs();
				}
			}
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00043D34 File Offset: 0x00041F34
		public int DoCompaction()
		{
			int num = 0;
			foreach (object obj in this._namedCacheTable)
			{
				OMNamedCache omnamedCache = (OMNamedCache)obj;
				num += omnamedCache.DoCompaction();
			}
			num += this._cache.DoCompaction();
			return num;
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00043DA0 File Offset: 0x00041FA0
		public int DoCompaction(string cacheName, string regionName)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(ObjectManager.LogSource, "DoCompaction::Compaction started on CacheName = {0} and regionName {1} ", new object[] { cacheName, regionName });
				}
				int num = region.DoCompaction();
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo(ObjectManager.LogSource, "DoCompaction::Compaction ended on CacheName = {0} and regionName {1} and compacted {2} directories", new object[] { cacheName, regionName, num });
				}
				return num;
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(ObjectManager.LogSource, "GetSubDirectoriesCount::Region doesnot exist cacheName = {0} regionName = {1}", new object[] { cacheName, regionName });
			}
			return -1;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00043E45 File Offset: 0x00042045
		public int GetSubDirectoriesCount()
		{
			return this._cache.SplitCount;
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00043E54 File Offset: 0x00042054
		public int GetSubDirectoriesCount(string cacheName, string regionName)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				return region.GetSubdirectoriesCount();
			}
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo(ObjectManager.LogSource, "GetSubDirectoriesCount::Region doesnot exist cacheName = {0} regionName = {1}", new object[] { cacheName, regionName });
			}
			return -1;
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00043E9C File Offset: 0x0004209C
		public void InternalUpsert(string cacheName, string regionName, object key, object value, InternalCacheItemVersion version, DateTime ttl, DataCacheTag[] tags)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				region.InternalUpsert(key, value, version, ttl, tags);
				return;
			}
			if (this.ContainsNamedCache(cacheName))
			{
				throw ObjectManager.GetException(3002);
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00043EE4 File Offset: 0x000420E4
		public void InternalPutAndUnlock(string cacheName, string regionName, object key, object value, InternalCacheItemVersion version, DateTime timeToLive, object[] tags, DataCacheLockHandle lHandle)
		{
			OMRegion region = this.GetRegion(cacheName, regionName);
			if (region != null)
			{
				region.InternalPutAndLock(key, value, version, timeToLive, tags, lHandle);
				return;
			}
			if (this.GetNamedCache(cacheName) == null)
			{
				throw ObjectManager.GetException(3006);
			}
			throw ObjectManager.GetException(3002);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00043F2F File Offset: 0x0004212F
		public void RollbackMissCount()
		{
			this.Stats.DecrMiss();
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00043F3C File Offset: 0x0004213C
		public void RollbackTotalCount()
		{
			this.Stats.DecrTotal();
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00043F4C File Offset: 0x0004214C
		public void InternalBatchDelete(string cacheName, EvictionReplicationBatch batch, object opState)
		{
			foreach (string text in batch.RegionNames)
			{
				OMRegion region = this.GetRegion(cacheName, text);
				if (region != null)
				{
					region.InternalDelete(batch, opState);
				}
			}
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00043F90 File Offset: 0x00042190
		public void BeginBatchDelete(string cacheName, EvictionReplicationBatch batch, out long sizeofLatchedItems)
		{
			sizeofLatchedItems = 0L;
			foreach (EvictedElement evictedElement in batch)
			{
				OMRegion omregion = evictedElement.CacheItem.Region as OMRegion;
				try
				{
					if (omregion.BeginDelete(evictedElement.Key, evictedElement.Version))
					{
						sizeofLatchedItems += (long)evictedElement.CacheItem.Size;
					}
					else
					{
						batch.IgnoreElement(evictedElement);
					}
				}
				catch (DataCacheException ex)
				{
					if (ex.ErrorCode != 2009)
					{
						throw;
					}
					batch.IgnoreElement(evictedElement);
				}
			}
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0004402C File Offset: 0x0004222C
		public void RollbackBatchDelete(string cacheName, EvictionReplicationBatch batch)
		{
			foreach (EvictedElement evictedElement in batch)
			{
				OMRegion omregion = evictedElement.CacheItem.Region as OMRegion;
				omregion.RollbackDelete(evictedElement.Key, evictedElement.Version);
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00044078 File Offset: 0x00042278
		public void CommitBatchDelete(string cacheName, EvictionReplicationBatch batch, object opState)
		{
			foreach (EvictedElement evictedElement in batch)
			{
				OMRegion omregion = evictedElement.CacheItem.Region as OMRegion;
				omregion.CommitDelete(evictedElement.Key, opState);
			}
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x000440C0 File Offset: 0x000422C0
		public OMWriteBehindStats GetWriteBehindStats()
		{
			OMWriteBehindStats omwriteBehindStats = new OMWriteBehindStats();
			lock (this._namedCacheTable)
			{
				foreach (object obj in this._namedCacheTable)
				{
					OMNamedCache omnamedCache = (OMNamedCache)obj;
					omwriteBehindStats.FlushedItemCount += omnamedCache.Stats.WriteBehindStats.GetFlushedItemCount();
					omwriteBehindStats.DroppedItemCount += omnamedCache.Stats.WriteBehindStats.GetDroppedItemCount();
					omwriteBehindStats.WBQueueCount += omnamedCache.Stats.WriteBehindStats.GetWBQueueCount();
				}
			}
			return omwriteBehindStats;
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x000441A0 File Offset: 0x000423A0
		private InternalCacheItemVersion PutReplaceInternal(string cacheName, string regionName, object key, RequestBody request, object value, DateTime timeToLive, object[] tags, InternalCacheItemVersion version, bool isReplace, ProtocolType protocolType)
		{
			OMRegion region = this.GetRegion(request, cacheName, regionName);
			if (region != null)
			{
				region.NamedCache.Stats.IncrWriteRequestBy(1L);
				if (request != null && request.protocolType == ProtocolType.Rest)
				{
					region.NamedCache.Stats.IncrTotalRestReqs();
				}
				try
				{
					AOMCacheItem aomcacheItem = (isReplace ? region.Replace(key, value, timeToLive, tags, ref version, request) : region.Put(key, value, timeToLive, tags, protocolType, ref version, request));
					if (aomcacheItem != null)
					{
						return version;
					}
				}
				catch (DataCacheException)
				{
					region.Stats.IncrMiss();
					throw;
				}
				region.Stats.IncrMiss();
				return version;
			}
			OMNamedCache namedCache = this.GetNamedCache(cacheName);
			if (namedCache != null)
			{
				if (!RegionNameProvider.IsSystemRegion(regionName))
				{
					if (request != null && request.protocolType == ProtocolType.Rest)
					{
						namedCache.Stats.IncrTotalRestReqs();
					}
					namedCache.Stats.IncrUpsertReqs();
					namedCache.Stats.IncrMiss();
					namedCache.Stats.IncrWriteRequestBy(1L);
				}
				throw ObjectManager.GetException(3002);
			}
			this.Stats.IncrUpsertReqs();
			this.Stats.IncrMiss();
			if (request != null && request.protocolType == ProtocolType.Rest)
			{
				this.Stats.IncrRestTotalReqs();
			}
			throw ObjectManager.GetException(3006);
		}

		// Token: 0x04000C72 RID: 3186
		private IMemoryManager _memoryManager = new ObjectMemoryManager();

		// Token: 0x04000C73 RID: 3187
		private IDataManager _dm;

		// Token: 0x04000C74 RID: 3188
		private BaseHashTable _namedCacheTable;

		// Token: 0x04000C75 RID: 3189
		private IContainerSchema _cacheSchema;

		// Token: 0x04000C76 RID: 3190
		private IDMContainer _cache;

		// Token: 0x04000C77 RID: 3191
		private OMCacheNodeStats _stats;

		// Token: 0x04000C78 RID: 3192
		private OMCacheNodeProperties _props;

		// Token: 0x04000C79 RID: 3193
		private DMOperationCallBack[] _prePostOperations;

		// Token: 0x04000C7A RID: 3194
		private OMOperationCallBack[] _userPrePostOperations;

		// Token: 0x04000C7B RID: 3195
		private ICacheStatsContainer _statsContainer;

		// Token: 0x04000C7C RID: 3196
		internal static string LogSource = "DistributedCache.ObjectManager";
	}
}
