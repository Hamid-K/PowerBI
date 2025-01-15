using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200087E RID: 2174
	internal sealed class ScalabilityCache : BaseScalabilityCache
	{
		// Token: 0x060077B6 RID: 30646 RVA: 0x001EE418 File Offset: 0x001EC618
		public ScalabilityCache(IStorage storage, IIndexStrategy indexStrategy, ComponentType ownerComponent, long minReservedMemoryBytes)
			: base(storage, 3000L, 0.2, ownerComponent, minReservedMemoryBytes)
		{
			this.m_cachePriority = new LinkedLRUCache<StorageItem>();
			this.m_offsetMap = indexStrategy;
		}

		// Token: 0x060077B7 RID: 30647 RVA: 0x001EE445 File Offset: 0x001EC645
		public override IReference<T> Allocate<T>(T obj, int priority)
		{
			return this.InternalAllocate<T>(obj, priority, false, ItemSizes.SizeOf(obj));
		}

		// Token: 0x060077B8 RID: 30648 RVA: 0x001EE45B File Offset: 0x001EC65B
		public override IReference<T> Allocate<T>(T obj, int priority, int initialSize)
		{
			return this.InternalAllocate<T>(obj, priority, false, initialSize);
		}

		// Token: 0x060077B9 RID: 30649 RVA: 0x001EE467 File Offset: 0x001EC667
		public override IReference<T> AllocateAndPin<T>(T obj, int priority)
		{
			return this.InternalAllocate<T>(obj, priority, true, ItemSizes.SizeOf(obj));
		}

		// Token: 0x060077BA RID: 30650 RVA: 0x001EE47D File Offset: 0x001EC67D
		public override IReference<T> AllocateAndPin<T>(T obj, int priority, int initialSize)
		{
			return this.InternalAllocate<T>(obj, priority, true, initialSize);
		}

		// Token: 0x060077BB RID: 30651 RVA: 0x001EE48C File Offset: 0x001EC68C
		private IReference<T> InternalAllocate<T>(T obj, int priority, bool startPinned, int initialSize) where T : IStorable
		{
			Global.Tracer.Assert(obj != null, "Cannot allocate reference to null");
			BaseReference baseReference = base.CreateReference(obj);
			baseReference.Init(this, this.m_offsetMap.GenerateTempId());
			this.CacheItem(baseReference, obj, priority, initialSize);
			if (startPinned)
			{
				baseReference.PinValue();
			}
			return (IReference<T>)baseReference;
		}

		// Token: 0x060077BC RID: 30652 RVA: 0x001EE4F0 File Offset: 0x001EC6F0
		public override void Dispose()
		{
			try
			{
				if (this.m_offsetMap != null)
				{
					this.m_offsetMap.Close();
					this.m_offsetMap = null;
				}
				this.m_cacheLookup = null;
				this.m_cachePriority = null;
				this.m_staticIdLookup = null;
				this.m_staticReferences = null;
			}
			finally
			{
				base.Dispose();
			}
		}

		// Token: 0x060077BD RID: 30653 RVA: 0x001EE54C File Offset: 0x001EC74C
		public override IReference<T> GenerateFixedReference<T>(T obj)
		{
			BaseReference baseReference = base.CreateReference(obj);
			baseReference.Init(this, this.m_offsetMap.GenerateTempId());
			StorageItem storageItem = new StorageItem(baseReference.Id, -1, obj, ItemSizes.SizeOf(obj));
			baseReference.Item = storageItem;
			storageItem.AddReference(baseReference);
			storageItem.InQueue = InQueueState.Exempt;
			storageItem.HasBeenUnPinned = true;
			ISelfReferential selfReferential = obj as ISelfReferential;
			if (selfReferential != null)
			{
				selfReferential.SetReference(baseReference);
			}
			return (IReference<T>)baseReference;
		}

		// Token: 0x060077BE RID: 30654 RVA: 0x001EE5D0 File Offset: 0x001EC7D0
		public override int StoreStaticReference(object item)
		{
			int num;
			if (item == null)
			{
				num = -2147483647;
			}
			else
			{
				IStaticReferenceable staticReferenceable = item as IStaticReferenceable;
				if (staticReferenceable != null)
				{
					num = this.InternalStoreStaticReference(staticReferenceable.ID, item);
					staticReferenceable.SetID(num);
				}
				else
				{
					bool flag = true;
					if (this.m_staticIdLookup == null || !this.m_staticIdLookup.TryGetValue(item, out num))
					{
						num = int.MinValue;
						flag = false;
					}
					num = this.InternalStoreStaticReference(num, item);
					if (!flag)
					{
						if (this.m_staticIdLookup == null)
						{
							this.m_staticIdLookup = new Dictionary<object, int>();
						}
						this.m_staticIdLookup[item] = num;
					}
				}
			}
			return num;
		}

		// Token: 0x060077BF RID: 30655 RVA: 0x001EE658 File Offset: 0x001EC858
		private int InternalStoreStaticReference(int id, object item)
		{
			if (this.m_staticReferences == null)
			{
				this.m_staticReferences = new Dictionary<int, StaticReferenceHolder>();
			}
			int num = id;
			if (id == -2147483648)
			{
				num = (int)this.m_offsetMap.GenerateTempId().Value;
			}
			StaticReferenceHolder staticReferenceHolder;
			while (this.m_staticReferences.TryGetValue(num, out staticReferenceHolder) && item != staticReferenceHolder.Value)
			{
				num = (int)this.m_offsetMap.GenerateTempId().Value;
			}
			if (staticReferenceHolder != null)
			{
				staticReferenceHolder.RefCount++;
			}
			else
			{
				staticReferenceHolder = new StaticReferenceHolder();
				staticReferenceHolder.Value = item;
				staticReferenceHolder.RefCount = 1;
				this.m_staticReferences[num] = staticReferenceHolder;
			}
			return num;
		}

		// Token: 0x060077C0 RID: 30656 RVA: 0x001EE6FC File Offset: 0x001EC8FC
		public override object FetchStaticReference(int id)
		{
			if (id == -2147483647)
			{
				return null;
			}
			StaticReferenceHolder staticReferenceHolder;
			object obj;
			if (this.m_staticReferences.TryGetValue(id, out staticReferenceHolder))
			{
				obj = staticReferenceHolder.Value;
				staticReferenceHolder.RefCount--;
				if (staticReferenceHolder.RefCount <= 0)
				{
					this.m_staticReferences.Remove(id);
				}
			}
			else
			{
				Global.Tracer.Assert(false, "Missing static reference");
				obj = null;
			}
			return obj;
		}

		// Token: 0x060077C1 RID: 30657 RVA: 0x001EE764 File Offset: 0x001EC964
		public override IReference PoolReference(IReference reference)
		{
			StorageItem storageItem;
			if (this.CacheTryGetValue(reference.Id, out storageItem) && storageItem.Reference != null)
			{
				reference = storageItem.Reference;
			}
			return reference;
		}

		// Token: 0x060077C2 RID: 30658 RVA: 0x001EE798 File Offset: 0x001EC998
		public override void DisableStorageUpdates()
		{
			this.m_storage.FreezeAllocations = true;
			this.FlushLastRecentlyUsedItem();
			this.m_storage.FreezeAllocations = false;
			this.m_storageUpdatesDisabled = true;
		}

		// Token: 0x060077C3 RID: 30659 RVA: 0x001EE7BF File Offset: 0x001EC9BF
		internal override void UpdateTargetSize(BaseReference reference, int sizeDeltaBytes)
		{
			((StorageItem)reference.Item).UpdateSize(sizeDeltaBytes);
			this.m_cacheSizeBytes += (long)sizeDeltaBytes;
			this.m_totalAuditedBytes += (long)sizeDeltaBytes;
		}

		// Token: 0x170027DF RID: 10207
		// (get) Token: 0x060077C4 RID: 30660 RVA: 0x001EE7F0 File Offset: 0x001EC9F0
		public override ScalabilityCacheType CacheType
		{
			get
			{
				return ScalabilityCacheType.Standard;
			}
		}

		// Token: 0x060077C5 RID: 30661 RVA: 0x001EE7F3 File Offset: 0x001EC9F3
		internal override BaseReference TransferTo(BaseReference reference)
		{
			Global.Tracer.Assert(false, "ScalabilityCache does not support the TransferTo operation");
			return null;
		}

		// Token: 0x060077C6 RID: 30662 RVA: 0x001EE808 File Offset: 0x001ECA08
		internal override void ReferenceSerializeCallback(BaseReference reference)
		{
			ReferenceID id = reference.Id;
			if (id.IsTemporary)
			{
				StorageItem storageItem = (StorageItem)reference.Item;
				ReferenceID referenceID = this.m_offsetMap.GenerateId(id);
				if (id != referenceID)
				{
					reference.Id = referenceID;
					storageItem.Id = referenceID;
					this.CacheRemoveValue(id);
				}
				this.CacheSetValue(reference.Id, storageItem);
			}
		}

		// Token: 0x060077C7 RID: 30663 RVA: 0x001EE86C File Offset: 0x001ECA6C
		internal override void Free(BaseReference reference)
		{
			if (reference == null)
			{
				return;
			}
			ReferenceID id = reference.Id;
			StorageItem storageItem;
			if (this.CacheTryGetValue(id, out storageItem))
			{
				this.CacheRemoveValue(id);
			}
			if (storageItem == null)
			{
				storageItem = (StorageItem)reference.Item;
			}
			if (storageItem != null)
			{
				if (storageItem.InQueue == InQueueState.InQueue)
				{
					this.m_cachePriority.Remove(storageItem);
				}
				int num = ItemSizes.SizeOf(storageItem);
				this.m_cacheSizeBytes -= (long)num;
				this.m_totalAuditedBytes -= (long)num;
				this.m_totalFreedBytes += (long)num;
				base.UpdatePeakCacheSize();
				storageItem.Item = null;
				storageItem.UnlinkReferences(false);
			}
			reference.Item = null;
		}

		// Token: 0x060077C8 RID: 30664 RVA: 0x001EE914 File Offset: 0x001ECB14
		internal override IStorable Retrieve(BaseReference reference)
		{
			StorageItem storageItem;
			if (!this.CacheTryGetValue(reference.Id, out storageItem))
			{
				storageItem = this.LoadItem(reference);
			}
			base.PeriodicOperationCheck();
			return storageItem.Item;
		}

		// Token: 0x060077C9 RID: 30665 RVA: 0x001EE948 File Offset: 0x001ECB48
		internal override void Pin(BaseReference reference)
		{
			StorageItem storageItem = (StorageItem)reference.Item;
			if (storageItem == null)
			{
				if (this.CacheTryGetValue(reference.Id, out storageItem))
				{
					reference.Item = storageItem;
					storageItem.AddReference(reference);
					if (storageItem.InQueue == InQueueState.InQueue)
					{
						this.m_cachePriority.Bump(storageItem);
					}
				}
				else
				{
					storageItem = this.LoadItem(reference);
				}
			}
			else if (storageItem.InQueue == InQueueState.InQueue)
			{
				this.m_cachePriority.Bump(storageItem);
			}
			storageItem.PinCount++;
		}

		// Token: 0x060077CA RID: 30666 RVA: 0x001EE9C8 File Offset: 0x001ECBC8
		internal override void UnPin(BaseReference reference)
		{
			StorageItem storageItem = (StorageItem)reference.Item;
			int num = storageItem.PinCount - 1;
			storageItem.PinCount = num;
			if (num == 0)
			{
				if (storageItem.InQueue == InQueueState.None)
				{
					this.EnqueueItem(storageItem);
				}
				if (!storageItem.HasBeenUnPinned)
				{
					int num2 = storageItem.UpdateSize();
					this.m_cacheSizeBytes += (long)num2;
					this.m_totalAuditedBytes += (long)num2;
					storageItem.HasBeenUnPinned = true;
				}
			}
		}

		// Token: 0x170027E0 RID: 10208
		// (get) Token: 0x060077CB RID: 30667 RVA: 0x001EEA37 File Offset: 0x001ECC37
		protected override long InternalFreeableBytes
		{
			get
			{
				return this.m_cacheSizeBytes;
			}
		}

		// Token: 0x060077CC RID: 30668 RVA: 0x001EEA40 File Offset: 0x001ECC40
		internal bool CacheTryGetValue(ReferenceID id, out StorageItem item)
		{
			item = null;
			bool flag = false;
			if (this.m_cacheLookup != null)
			{
				flag = this.m_cacheLookup.TryGetValue(id, out item);
			}
			return flag;
		}

		// Token: 0x060077CD RID: 30669 RVA: 0x001EEA6C File Offset: 0x001ECC6C
		internal bool CacheRemoveValue(ReferenceID id)
		{
			bool flag = false;
			if (this.m_cacheLookup != null)
			{
				flag = this.m_cacheLookup.Remove(id);
			}
			return flag;
		}

		// Token: 0x060077CE RID: 30670 RVA: 0x001EEA91 File Offset: 0x001ECC91
		internal void CacheSetValue(ReferenceID id, StorageItem value)
		{
			if (this.m_cacheLookup == null)
			{
				this.m_cacheLookup = new SegmentedDictionary<ReferenceID, StorageItem>(503, 17, ReferenceIDEqualityComparer.Instance);
			}
			this.m_cacheLookup[id] = value;
		}

		// Token: 0x060077CF RID: 30671 RVA: 0x001EEAC0 File Offset: 0x001ECCC0
		private StorageItem LoadItem(BaseReference reference)
		{
			if (this.m_inStreamOper)
			{
				Global.Tracer.Assert(false, "ScalabilityCache should not Load during serialization");
			}
			StorageItem storageItem = null;
			try
			{
				this.m_inStreamOper = true;
				this.m_deserializationTimer.Start();
				long num = this.m_offsetMap.Retrieve(reference.Id);
				if (num >= 0L)
				{
					long num2;
					storageItem = (StorageItem)this.m_storage.Retrieve(num, out num2);
					storageItem.Offset = num;
					storageItem.PersistedSize = num2;
					storageItem.UpdateSize();
					storageItem.HasBeenUnPinned = true;
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
			finally
			{
				this.m_inStreamOper = false;
				this.m_deserializationTimer.Stop();
			}
			this.CacheItem(reference, storageItem, true);
			return storageItem;
		}

		// Token: 0x060077D0 RID: 30672 RVA: 0x001EEB7C File Offset: 0x001ECD7C
		private void CacheItem(BaseReference reference, StorageItem item, bool fromDeserialize)
		{
			reference.Item = item;
			item.AddReference(reference);
			int num = ItemSizes.SizeOf(item);
			base.FreeCacheSpace(num, this.m_cacheSizeBytes);
			if (fromDeserialize)
			{
				this.CacheSetValue(reference.Id, item);
			}
			else
			{
				this.m_totalAuditedBytes += (long)num;
			}
			this.m_cacheSizeBytes += (long)num;
			this.EnqueueItem(item);
			ISelfReferential selfReferential = item.Item as ISelfReferential;
			if (selfReferential != null)
			{
				selfReferential.SetReference(reference);
			}
		}

		// Token: 0x060077D1 RID: 30673 RVA: 0x001EEBF8 File Offset: 0x001ECDF8
		private void CacheItem(BaseReference reference, IStorable value, int priority, int initialSize)
		{
			StorageItem storageItem = new StorageItem(reference.Id, priority, value, initialSize);
			this.CacheItem(reference, storageItem, false);
		}

		// Token: 0x060077D2 RID: 30674 RVA: 0x001EEC1E File Offset: 0x001ECE1E
		private void EnqueueItem(StorageItem item)
		{
			this.m_cachePriority.Add(item);
			item.InQueue = InQueueState.InQueue;
		}

		// Token: 0x060077D3 RID: 30675 RVA: 0x001EEC33 File Offset: 0x001ECE33
		protected override void FulfillInProgressFree()
		{
			this.m_storage.FreezeAllocations = true;
			while (this.m_inProgressFreeBytes > 0L && this.m_cachePriority.Count > 0)
			{
				this.FlushLastRecentlyUsedItem();
			}
			this.m_storage.FreezeAllocations = false;
		}

		// Token: 0x060077D4 RID: 30676 RVA: 0x001EEC70 File Offset: 0x001ECE70
		private void FlushLastRecentlyUsedItem()
		{
			StorageItem storageItem = this.m_cachePriority.ExtractLRU();
			storageItem.InQueue = InQueueState.None;
			if (storageItem.Item != null && storageItem.PinCount == 0)
			{
				this.CacheRemoveValue(storageItem.Id);
				int num = ItemSizes.SizeOf(storageItem);
				if (!this.m_storageUpdatesDisabled)
				{
					storageItem.Flush(this.m_storage, this.m_offsetMap);
				}
				this.m_cacheSizeBytes -= (long)num;
				if (this.m_cacheSizeBytes < 0L)
				{
					this.m_cacheSizeBytes = 0L;
				}
				this.m_inProgressFreeBytes -= (long)num;
				if (this.m_inProgressFreeBytes < 0L)
				{
					this.m_inProgressFreeBytes = 0L;
				}
			}
		}

		// Token: 0x170027E1 RID: 10209
		// (get) Token: 0x060077D5 RID: 30677 RVA: 0x001EED10 File Offset: 0x001ECF10
		public override long CurrentFreeableMemoryKBytes
		{
			get
			{
				long currentMemoryUsageKBytes = this.CurrentMemoryUsageKBytes;
				return Math.Max(0L, currentMemoryUsageKBytes - base.MinReservedMemoryKB);
			}
		}

		// Token: 0x04003C67 RID: 15463
		private LinkedLRUCache<StorageItem> m_cachePriority;

		// Token: 0x04003C68 RID: 15464
		private SegmentedDictionary<ReferenceID, StorageItem> m_cacheLookup;

		// Token: 0x04003C69 RID: 15465
		private IIndexStrategy m_offsetMap;

		// Token: 0x04003C6A RID: 15466
		private Dictionary<int, StaticReferenceHolder> m_staticReferences;

		// Token: 0x04003C6B RID: 15467
		private Dictionary<object, int> m_staticIdLookup;

		// Token: 0x04003C6C RID: 15468
		private bool m_storageUpdatesDisabled;

		// Token: 0x04003C6D RID: 15469
		private const long CacheExpansionIntervalMs = 3000L;

		// Token: 0x04003C6E RID: 15470
		private const double CacheExpansionRatio = 0.2;

		// Token: 0x04003C6F RID: 15471
		private const int ID_NULL = -2147483647;

		// Token: 0x04003C70 RID: 15472
		public const int ID_NOREF = -2147483648;
	}
}
