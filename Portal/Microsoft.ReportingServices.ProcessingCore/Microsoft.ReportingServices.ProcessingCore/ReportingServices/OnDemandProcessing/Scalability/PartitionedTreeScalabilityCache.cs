using System;
using System.Threading;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000840 RID: 2112
	internal abstract class PartitionedTreeScalabilityCache : BaseScalabilityCache
	{
		// Token: 0x060075F9 RID: 30201 RVA: 0x001E9048 File Offset: 0x001E7248
		public PartitionedTreeScalabilityCache(TreePartitionManager partitionManager, IStorage storage, long cacheExpansionIntervalMs, double cacheExpansionRatio, long minReservedMemoryBytes)
			: base(storage, cacheExpansionIntervalMs, cacheExpansionRatio, ComponentType.Processing, minReservedMemoryBytes)
		{
			this.m_serializationQueue = new LinkedBucketedQueue<BaseReference>(100);
			this.m_cachePriority = new LinkedLRUCache<ItemHolder>();
			this.m_cacheFreeableBytes = 0L;
			this.m_partitionManager = partitionManager;
		}

		// Token: 0x060075FA RID: 30202 RVA: 0x001E9086 File Offset: 0x001E7286
		public sealed override IReference<T> Allocate<T>(T obj, int priority)
		{
			Global.Tracer.Assert(false, "Allocate should not be used on PartitionedTreeScalabilityCache.  Use AllocateAndPin instead.");
			return null;
		}

		// Token: 0x060075FB RID: 30203 RVA: 0x001E9099 File Offset: 0x001E7299
		public sealed override IReference<T> Allocate<T>(T obj, int priority, int initialSize)
		{
			Global.Tracer.Assert(false, "Allocate should not be used on PartitionedTreeScalabilityCache.  Use AllocateAndPin instead.");
			return null;
		}

		// Token: 0x060075FC RID: 30204 RVA: 0x001E90AC File Offset: 0x001E72AC
		public sealed override IReference<T> AllocateAndPin<T>(T obj, int priority)
		{
			return (IReference<T>)this.AllocateAndPin(obj, ItemSizes.SizeOf(obj));
		}

		// Token: 0x060075FD RID: 30205 RVA: 0x001E90CA File Offset: 0x001E72CA
		public sealed override IReference<T> AllocateAndPin<T>(T obj, int priority, int initialSize)
		{
			return (IReference<T>)this.AllocateAndPin(obj, initialSize);
		}

		// Token: 0x060075FE RID: 30206 RVA: 0x001E90E0 File Offset: 0x001E72E0
		protected BaseReference AllocateAndPin(IStorable obj, int initialSize)
		{
			Global.Tracer.Assert(obj != null, "Cannot allocate reference to null");
			BaseReference baseReference = base.CreateReference(obj);
			baseReference.Init(this, this.GenerateTempId());
			this.CacheItem(baseReference, obj, false, initialSize);
			baseReference.PinValue();
			return baseReference;
		}

		// Token: 0x060075FF RID: 30207 RVA: 0x001E9128 File Offset: 0x001E7328
		public sealed override IReference<T> GenerateFixedReference<T>(T obj)
		{
			BaseReference baseReference = base.CreateReference(obj);
			baseReference.Init(this, this.GenerateTempId());
			baseReference.Item = new ItemHolder
			{
				Reference = baseReference,
				Item = obj
			};
			baseReference.InQueue = InQueueState.InQueue;
			return (IReference<T>)baseReference;
		}

		// Token: 0x06007600 RID: 30208 RVA: 0x001E917C File Offset: 0x001E737C
		public sealed override int StoreStaticReference(object item)
		{
			Global.Tracer.Assert(false, "Static references are not supported in the PartitionedTreeScalabilityCache");
			return -1;
		}

		// Token: 0x06007601 RID: 30209 RVA: 0x001E918F File Offset: 0x001E738F
		public sealed override object FetchStaticReference(int id)
		{
			Global.Tracer.Assert(false, "Static references are not supported in the PartitionedTreeScalabilityCache");
			return null;
		}

		// Token: 0x06007602 RID: 30210 RVA: 0x001E91A4 File Offset: 0x001E73A4
		public sealed override IReference PoolReference(IReference reference)
		{
			BaseReference baseReference;
			if (this.CacheTryGetValue(reference.Id, out baseReference))
			{
				reference = baseReference;
			}
			return reference;
		}

		// Token: 0x06007603 RID: 30211 RVA: 0x001E91C5 File Offset: 0x001E73C5
		public sealed override void DisableStorageUpdates()
		{
			Global.Tracer.Assert(false, "Disabling storage update is not supported in the PartitionedTreeScalabilityCache");
		}

		// Token: 0x170027A3 RID: 10147
		// (get) Token: 0x06007604 RID: 30212
		public abstract override ScalabilityCacheType CacheType { get; }

		// Token: 0x06007605 RID: 30213 RVA: 0x001E91D8 File Offset: 0x001E73D8
		internal IReference<T> AllocateEmptyTreePartition<T>(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType referenceObjectType)
		{
			BaseReference baseReference;
			if (!this.m_referenceCreator.TryCreateReference(referenceObjectType, out baseReference))
			{
				Global.Tracer.Assert(false, "Cannot create reference of type: {0}", new object[] { referenceObjectType });
			}
			baseReference.Init(this, this.m_partitionManager.AllocateNewTreePartition());
			return (IReference<T>)baseReference;
		}

		// Token: 0x06007606 RID: 30214 RVA: 0x001E922C File Offset: 0x001E742C
		internal void SetTreePartitionContentsAndPin<T>(IReference<T> emptyPartitionRef, T contents) where T : IStorable
		{
			BaseReference baseReference = (BaseReference)emptyPartitionRef;
			this.m_partitionManager.TreeHasChanged = true;
			this.CacheItem(baseReference, contents, false, ItemSizes.SizeOf(contents));
			baseReference.PinValue();
			this.CacheSetValue(baseReference.Id, baseReference);
		}

		// Token: 0x06007607 RID: 30215 RVA: 0x001E927C File Offset: 0x001E747C
		internal void Flush()
		{
			foreach (BaseReference baseReference in this.m_serializationQueue)
			{
				this.WriteItem(baseReference);
			}
			this.m_serializationQueue.Clear();
			this.m_cachePriority.Clear();
			this.m_cacheLookup = null;
			this.m_cacheCapacityBytes = 0L;
			this.m_cacheFreeableBytes = 0L;
			this.m_cacheSizeBytes = 0L;
			this.m_storage.Flush();
		}

		// Token: 0x06007608 RID: 30216 RVA: 0x001E930C File Offset: 0x001E750C
		internal void PrepareForFlush()
		{
			this.m_cachePriority.Clear();
			this.m_cacheLookup = null;
			this.m_cacheFreeableBytes = 0L;
			this.m_pendingFreeBytes = 0L;
			this.m_lockedDownForFlush = true;
		}

		// Token: 0x06007609 RID: 30217 RVA: 0x001E9337 File Offset: 0x001E7537
		public sealed override void Dispose()
		{
			this.m_cacheLookup = null;
			this.m_cachePriority = null;
			this.m_serializationQueue = null;
			this.m_pinnedItems = null;
			base.Dispose();
		}

		// Token: 0x0600760A RID: 30218 RVA: 0x001E935B File Offset: 0x001E755B
		internal override BaseReference TransferTo(BaseReference reference)
		{
			Global.Tracer.Assert(false, "PartitionedTreeScalabilityCache does not support the TransferTo operation");
			return null;
		}

		// Token: 0x0600760B RID: 30219 RVA: 0x001E936E File Offset: 0x001E756E
		internal sealed override void Free(BaseReference reference)
		{
			Global.Tracer.Assert(false, "PartitionedTreeScalabilityCache does not support Free");
		}

		// Token: 0x0600760C RID: 30220 RVA: 0x001E9380 File Offset: 0x001E7580
		internal sealed override IStorable Retrieve(BaseReference reference)
		{
			if (reference.Item == null)
			{
				ReferenceID id = reference.Id;
				BaseReference baseReference;
				if (this.CacheTryGetValue(id, out baseReference) && baseReference.Item != null)
				{
					IStorable item = baseReference.Item.Item;
					this.CacheItem(reference, item, true, ItemSizes.SizeOf(item));
				}
				else
				{
					this.LoadItem(reference);
				}
			}
			IStorable storable = null;
			if (reference.Item != null)
			{
				storable = reference.Item.Item;
			}
			return storable;
		}

		// Token: 0x0600760D RID: 30221 RVA: 0x001E93EB File Offset: 0x001E75EB
		internal sealed override void ReferenceValueCallback(BaseReference reference)
		{
			if (reference.InQueue == InQueueState.Exempt)
			{
				this.m_cachePriority.Bump(reference.Item);
			}
			base.ReferenceValueCallback(reference);
		}

		// Token: 0x0600760E RID: 30222 RVA: 0x001E940E File Offset: 0x001E760E
		internal sealed override void Pin(BaseReference reference)
		{
			this.Retrieve(reference);
		}

		// Token: 0x0600760F RID: 30223 RVA: 0x001E9418 File Offset: 0x001E7618
		internal sealed override void UnPin(BaseReference reference)
		{
			if (reference.PinCount == 0 && (reference.Id.IsTemporary || reference.Id.HasMultiPart) && reference.InQueue == InQueueState.None)
			{
				reference.InQueue = InQueueState.InQueue;
				this.m_serializationQueue.Enqueue(reference);
				if (!this.m_lockedDownForFlush)
				{
					ItemHolder item = reference.Item;
					IStorable storable = null;
					if (item != null)
					{
						storable = item.Item;
					}
					this.m_cacheFreeableBytes += (long)ItemSizes.SizeOf(storable);
				}
			}
			if (!this.m_lockedDownForFlush)
			{
				base.PeriodicOperationCheck();
			}
		}

		// Token: 0x06007610 RID: 30224 RVA: 0x001E94A5 File Offset: 0x001E76A5
		internal sealed override void ReferenceSerializeCallback(BaseReference reference)
		{
		}

		// Token: 0x06007611 RID: 30225 RVA: 0x001E94A8 File Offset: 0x001E76A8
		internal sealed override void UpdateTargetSize(BaseReference reference, int sizeDeltaBytes)
		{
			this.m_cacheSizeBytes += (long)sizeDeltaBytes;
			this.m_totalAuditedBytes += (long)sizeDeltaBytes;
			if (!reference.Id.IsTemporary)
			{
				this.m_cacheFreeableBytes += (long)sizeDeltaBytes;
			}
		}

		// Token: 0x170027A4 RID: 10148
		// (get) Token: 0x06007612 RID: 30226 RVA: 0x001E94F2 File Offset: 0x001E76F2
		protected sealed override long InternalFreeableBytes
		{
			get
			{
				return this.m_cacheFreeableBytes;
			}
		}

		// Token: 0x06007613 RID: 30227 RVA: 0x001E94FC File Offset: 0x001E76FC
		internal bool CacheTryGetValue(ReferenceID id, out BaseReference item)
		{
			item = null;
			bool flag = false;
			if (this.m_cacheLookup != null)
			{
				flag = this.m_cacheLookup.TryGetValue(id, out item);
			}
			return flag;
		}

		// Token: 0x06007614 RID: 30228 RVA: 0x001E9528 File Offset: 0x001E7728
		internal bool CacheRemoveValue(ReferenceID id)
		{
			bool flag = false;
			if (this.m_cacheLookup != null)
			{
				flag = this.m_cacheLookup.Remove(id);
			}
			return flag;
		}

		// Token: 0x06007615 RID: 30229 RVA: 0x001E954D File Offset: 0x001E774D
		internal void CacheSetValue(ReferenceID id, BaseReference value)
		{
			if (this.m_cacheLookup == null)
			{
				this.m_cacheLookup = new SegmentedDictionary<ReferenceID, BaseReference>(503, 17, ReferenceIDEqualityComparer.Instance);
			}
			this.m_cacheLookup[id] = value;
		}

		// Token: 0x06007616 RID: 30230 RVA: 0x001E957C File Offset: 0x001E777C
		private IStorable LoadItem(BaseReference reference)
		{
			if (this.m_inStreamOper)
			{
				Global.Tracer.Assert(false, "PartitionedTreeScalabilityCache should not Load during serialization");
			}
			IStorable storable = null;
			try
			{
				this.m_inStreamOper = true;
				this.m_deserializationTimer.Start();
				ReferenceID id = reference.Id;
				long num;
				if (!id.IsTemporary && id.HasMultiPart)
				{
					num = this.m_partitionManager.GetTreePartitionOffset(id);
					if (num < 0L)
					{
						return null;
					}
				}
				else
				{
					num = reference.Id.Value;
				}
				if (num < 0L)
				{
					Global.Tracer.Assert(false, "Invalid offset for item.  ReferenceID: {0}, Offset: {1}", new object[] { id, num });
				}
				long num2;
				storable = (IStorable)this.m_storage.Retrieve(num, out num2);
			}
			finally
			{
				this.m_inStreamOper = false;
				this.m_deserializationTimer.Stop();
			}
			this.CacheItem(reference, storable, true, ItemSizes.SizeOf(storable));
			return storable;
		}

		// Token: 0x06007617 RID: 30231 RVA: 0x001E9670 File Offset: 0x001E7870
		private void CacheItem(BaseReference reference, IStorable item, bool fromDeserialize, int newItemSize)
		{
			reference.Item = new ItemHolder
			{
				Reference = reference,
				Item = item
			};
			base.FreeCacheSpace(newItemSize, this.m_cacheFreeableBytes);
			if (fromDeserialize)
			{
				this.CacheSetValue(reference.Id, reference);
				this.m_cacheFreeableBytes += (long)newItemSize;
			}
			else
			{
				this.m_totalAuditedBytes += (long)newItemSize;
			}
			this.m_cacheSizeBytes += (long)newItemSize;
			this.EnqueueItem(reference);
		}

		// Token: 0x06007618 RID: 30232 RVA: 0x001E96F0 File Offset: 0x001E78F0
		private void EnqueueItem(BaseReference itemRef)
		{
			if (itemRef.GetObjectType() == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInstanceReference)
			{
				if (this.m_pinnedItems == null)
				{
					this.m_pinnedItems = new LinkedBucketedQueue<BaseReference>(25);
				}
				this.m_pinnedItems.Enqueue(itemRef);
				return;
			}
			ReferenceID id = itemRef.Id;
			if (!id.IsTemporary && (!id.HasMultiPart || this.m_partitionManager.GetTreePartitionOffset(id) != TreePartitionManager.EmptyTreePartitionOffset))
			{
				this.m_cachePriority.Add(itemRef.Item);
				itemRef.InQueue = InQueueState.Exempt;
			}
		}

		// Token: 0x06007619 RID: 30233 RVA: 0x001E9770 File Offset: 0x001E7970
		protected sealed override void FulfillInProgressFree()
		{
			int num = this.m_cachePriority.Count;
			while (this.m_inProgressFreeBytes > 0L && num > 0)
			{
				num--;
				ItemHolder itemHolder = this.m_cachePriority.Peek();
				BaseReference reference = itemHolder.Reference;
				if (reference.PinCount == 0)
				{
					this.m_cachePriority.ExtractLRU();
					reference.InQueue = InQueueState.None;
					if (itemHolder.Item != null)
					{
						this.UpdateStatsForRemovedItem(reference, ref this.m_inProgressFreeBytes);
						this.CacheRemoveValue(reference.Id);
						itemHolder.Item = null;
						itemHolder.Reference = null;
						reference.Item = null;
					}
				}
				else
				{
					this.m_cachePriority.Bump(itemHolder);
				}
			}
			if (this.m_inProgressFreeBytes > 0L)
			{
				using (IDecumulator<BaseReference> decumulator = this.m_serializationQueue.GetDecumulator())
				{
					while (this.m_inProgressFreeBytes > 0L && decumulator.MoveNext())
					{
						BaseReference baseReference = decumulator.Current;
						decumulator.RemoveCurrent();
						if (baseReference.Item != null)
						{
							if (baseReference.PinCount == 0)
							{
								this.UpdateStatsForRemovedItem(baseReference, ref this.m_inProgressFreeBytes);
							}
							this.WriteItem(baseReference);
							if (baseReference.PinCount > 0)
							{
								this.EnqueueItem(baseReference);
								this.CacheSetValue(baseReference.Id, baseReference);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600761A RID: 30234 RVA: 0x001E98B4 File Offset: 0x001E7AB4
		private void UpdateStatsForRemovedItem(BaseReference itemRef, ref long bytesToFree)
		{
			long num = (long)ItemSizes.SizeOf(itemRef.Item.Item);
			long num2 = this.m_cacheSizeBytes - num;
			long num3 = this.m_cacheFreeableBytes - num;
			if (num3 < 0L)
			{
				num3 = 0L;
			}
			if (num2 < num3)
			{
				num2 = num3;
			}
			this.m_cacheFreeableBytes = num3;
			this.m_cacheSizeBytes = num2;
			bytesToFree -= num;
		}

		// Token: 0x0600761B RID: 30235 RVA: 0x001E9908 File Offset: 0x001E7B08
		private void WriteItem(BaseReference itemRef)
		{
			ItemHolder item = itemRef.Item;
			IStorable item2 = item.Item;
			ReferenceID referenceID = itemRef.Id;
			long num = this.m_storage.Allocate(item2);
			if (referenceID.HasMultiPart && !referenceID.IsTemporary)
			{
				this.m_partitionManager.UpdateTreePartitionOffset(referenceID, num);
				if (itemRef.PinCount == 0)
				{
					this.CacheRemoveValue(referenceID);
				}
			}
			else
			{
				referenceID = new ReferenceID(num)
				{
					IsTemporary = false,
					HasMultiPart = false
				};
				itemRef.Id = referenceID;
			}
			if (itemRef.PinCount == 0)
			{
				item.Item = null;
				item.Reference = null;
				itemRef.Item = null;
			}
		}

		// Token: 0x170027A5 RID: 10149
		// (get) Token: 0x0600761C RID: 30236 RVA: 0x001E99A4 File Offset: 0x001E7BA4
		internal long CacheSizeBytes
		{
			get
			{
				return this.m_cacheSizeBytes;
			}
		}

		// Token: 0x170027A6 RID: 10150
		// (get) Token: 0x0600761D RID: 30237 RVA: 0x001E99AC File Offset: 0x001E7BAC
		internal long CacheFreeableBytes
		{
			get
			{
				return this.m_cacheFreeableBytes;
			}
		}

		// Token: 0x170027A7 RID: 10151
		// (get) Token: 0x0600761E RID: 30238 RVA: 0x001E99B4 File Offset: 0x001E7BB4
		// (set) Token: 0x0600761F RID: 30239 RVA: 0x001E99BC File Offset: 0x001E7BBC
		internal long CacheCapacityBytes
		{
			get
			{
				return this.m_cacheCapacityBytes;
			}
			set
			{
				this.m_cacheCapacityBytes = value;
			}
		}

		// Token: 0x06007620 RID: 30240 RVA: 0x001E99C8 File Offset: 0x001E7BC8
		private ReferenceID GenerateTempId()
		{
			long nextId = this.m_nextId;
			this.m_nextId = nextId - 1L;
			return new ReferenceID(nextId);
		}

		// Token: 0x170027A8 RID: 10152
		// (get) Token: 0x06007621 RID: 30241 RVA: 0x001E99EC File Offset: 0x001E7BEC
		public sealed override long CurrentFreeableMemoryKBytes
		{
			get
			{
				long num = Interlocked.Read(ref this.m_cacheFreeableBytes) / 1024L;
				return Math.Max(0L, num - base.MinReservedMemoryKB);
			}
		}

		// Token: 0x04003BA9 RID: 15273
		private long m_cacheFreeableBytes;

		// Token: 0x04003BAA RID: 15274
		private LinkedBucketedQueue<BaseReference> m_serializationQueue;

		// Token: 0x04003BAB RID: 15275
		private LinkedBucketedQueue<BaseReference> m_pinnedItems;

		// Token: 0x04003BAC RID: 15276
		private LinkedLRUCache<ItemHolder> m_cachePriority;

		// Token: 0x04003BAD RID: 15277
		private SegmentedDictionary<ReferenceID, BaseReference> m_cacheLookup;

		// Token: 0x04003BAE RID: 15278
		private long m_nextId = -1L;

		// Token: 0x04003BAF RID: 15279
		private TreePartitionManager m_partitionManager;

		// Token: 0x04003BB0 RID: 15280
		private bool m_lockedDownForFlush;
	}
}
