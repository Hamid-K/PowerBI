using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x0200087B RID: 2171
	public abstract class BaseScalabilityCache : MemoryAuditProxy, IScalabilityCache, PersistenceHelper, IDisposable
	{
		// Token: 0x06007784 RID: 30596 RVA: 0x001EDA7C File Offset: 0x001EBC7C
		internal BaseScalabilityCache(IStorage storage, long cacheExpansionIntervalMs, double cacheExpansionRatio, ComponentType ownerComponent, long minReservedMemoryBytes)
		{
			this.m_cacheSizeBytes = 0L;
			this.m_storage = storage;
			this.m_referenceCreator = storage.ReferenceCreator;
			storage.ScalabilityCache = this;
			Global.Tracer.Assert(cacheExpansionIntervalMs > 0L, "CacheExpansionIntervalMs must be greater than 0");
			this.m_expansionIntervalMs = cacheExpansionIntervalMs;
			this.m_cacheExpansionRatio = cacheExpansionRatio;
			this.m_ownerComponent = ownerComponent;
			this.m_minReservedMemoryKB = minReservedMemoryBytes / 1024L;
		}

		// Token: 0x06007785 RID: 30597
		public abstract IReference<T> Allocate<T>(T obj, int priority) where T : IStorable;

		// Token: 0x06007786 RID: 30598
		public abstract IReference<T> Allocate<T>(T obj, int priority, int initialSize) where T : IStorable;

		// Token: 0x06007787 RID: 30599
		public abstract IReference<T> AllocateAndPin<T>(T obj, int priority) where T : IStorable;

		// Token: 0x06007788 RID: 30600
		public abstract IReference<T> AllocateAndPin<T>(T obj, int priority, int initialSize) where T : IStorable;

		// Token: 0x06007789 RID: 30601
		public abstract IReference<T> GenerateFixedReference<T>(T obj) where T : IStorable;

		// Token: 0x0600778A RID: 30602 RVA: 0x001EDB1B File Offset: 0x001EBD1B
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x0600778B RID: 30603
		public abstract int StoreStaticReference(object item);

		// Token: 0x0600778C RID: 30604
		public abstract object FetchStaticReference(int id);

		// Token: 0x0600778D RID: 30605
		public abstract IReference PoolReference(IReference reference);

		// Token: 0x0600778E RID: 30606
		public abstract void DisableStorageUpdates();

		// Token: 0x170027D0 RID: 10192
		// (get) Token: 0x0600778F RID: 30607 RVA: 0x001EDB23 File Offset: 0x001EBD23
		public IStorage Storage
		{
			get
			{
				return this.m_storage;
			}
		}

		// Token: 0x170027D1 RID: 10193
		// (get) Token: 0x06007790 RID: 30608
		public abstract ScalabilityCacheType CacheType { get; }

		// Token: 0x170027D2 RID: 10194
		// (get) Token: 0x06007791 RID: 30609 RVA: 0x001EDB2B File Offset: 0x001EBD2B
		public ComponentType OwnerComponent
		{
			get
			{
				return this.m_ownerComponent;
			}
		}

		// Token: 0x170027D3 RID: 10195
		// (get) Token: 0x06007792 RID: 30610 RVA: 0x001EDB33 File Offset: 0x001EBD33
		public long SerializationDurationMs
		{
			get
			{
				return this.ReadTime(this.m_serializationTimer);
			}
		}

		// Token: 0x170027D4 RID: 10196
		// (get) Token: 0x06007793 RID: 30611 RVA: 0x001EDB41 File Offset: 0x001EBD41
		public long DeserializationDurationMs
		{
			get
			{
				return this.ReadTime(this.m_deserializationTimer);
			}
		}

		// Token: 0x170027D5 RID: 10197
		// (get) Token: 0x06007794 RID: 30612 RVA: 0x001EDB4F File Offset: 0x001EBD4F
		public long ScalabilityDurationMs
		{
			get
			{
				return this.SerializationDurationMs + this.DeserializationDurationMs;
			}
		}

		// Token: 0x170027D6 RID: 10198
		// (get) Token: 0x06007795 RID: 30613 RVA: 0x001EDB5E File Offset: 0x001EBD5E
		public long PeakMemoryUsageKBytes
		{
			get
			{
				return Math.Max(this.m_peakCacheSizeBytes, this.m_cacheSizeBytes) / 1024L;
			}
		}

		// Token: 0x170027D7 RID: 10199
		// (get) Token: 0x06007796 RID: 30614 RVA: 0x001EDB78 File Offset: 0x001EBD78
		public long CacheSizeKBytes
		{
			get
			{
				return this.m_cacheSizeBytes / 1024L;
			}
		}

		// Token: 0x06007797 RID: 30615 RVA: 0x001EDB88 File Offset: 0x001EBD88
		public override void Dispose()
		{
			try
			{
				if (!this.m_disposed)
				{
					this.m_disposed = true;
					if (this.m_serializationTimer != null && Global.Tracer.TraceVerbose)
					{
						long num = -1L;
						if (this.m_storage != null)
						{
							num = this.m_storage.StreamSize;
						}
						long num2 = this.m_totalBytesSerialized / 1024L;
						double num3 = (double)this.SerializationDurationMs / 1000.0;
						string text = string.Format("ScalabilityCache {0}|{1} Done. ", this.OwnerComponent, this.CacheType) + string.Format("AuditedHeapSerialized: {0} KB. Serialization Time {1} s. ", num2, num3) + string.Format("AvgSpeed {0:F2} KB/s. StreamSize {1} MB. ", (double)num2 / num3, num / 1048576L) + string.Format("FinalAuditedHeapSize {0} KB. LifetimeFreedHeapSize {1} KB.", this.m_totalAuditedBytes / 1024L, this.m_totalFreedBytes / 1024L);
						Global.Tracer.Trace(TraceLevel.Verbose, text);
						RSTrace.SanitizedRdlEngineHostTracer.Trace(text);
					}
					if (this.m_storage != null)
					{
						this.m_storage.Close();
						this.m_storage = null;
					}
					this.m_serializationTimer = null;
					this.m_deserializationTimer = null;
				}
			}
			finally
			{
				this.m_cacheSizeBytes = 0L;
				this.m_cacheCapacityBytes = 0L;
				base.Dispose();
			}
		}

		// Token: 0x06007798 RID: 30616
		internal abstract void Free(BaseReference reference);

		// Token: 0x06007799 RID: 30617
		internal abstract IStorable Retrieve(BaseReference reference);

		// Token: 0x0600779A RID: 30618 RVA: 0x001EDCF8 File Offset: 0x001EBEF8
		[DebuggerStepThrough]
		internal virtual void ReferenceValueCallback(BaseReference reference)
		{
			this.PeriodicOperationCheck();
		}

		// Token: 0x0600779B RID: 30619
		internal abstract void UnPin(BaseReference reference);

		// Token: 0x0600779C RID: 30620
		internal abstract void Pin(BaseReference reference);

		// Token: 0x0600779D RID: 30621
		internal abstract void ReferenceSerializeCallback(BaseReference reference);

		// Token: 0x0600779E RID: 30622
		internal abstract void UpdateTargetSize(BaseReference reference, int sizeDeltaBytes);

		// Token: 0x0600779F RID: 30623
		internal abstract BaseReference TransferTo(BaseReference reference);

		// Token: 0x060077A0 RID: 30624 RVA: 0x001EDD00 File Offset: 0x001EBF00
		[DebuggerStepThrough]
		internal void PeriodicOperationCheck()
		{
			if (this.m_pendingNotificationCount > 0)
			{
				this.FreeCacheSpace(0, this.InternalFreeableBytes);
			}
		}

		// Token: 0x170027D8 RID: 10200
		// (get) Token: 0x060077A1 RID: 30625 RVA: 0x001EDD18 File Offset: 0x001EBF18
		protected long MinReservedMemoryKB
		{
			get
			{
				return this.m_minReservedMemoryKB;
			}
		}

		// Token: 0x170027D9 RID: 10201
		// (get) Token: 0x060077A2 RID: 30626
		protected abstract long InternalFreeableBytes { get; }

		// Token: 0x060077A3 RID: 30627
		protected abstract void FulfillInProgressFree();

		// Token: 0x170027DA RID: 10202
		// (get) Token: 0x060077A4 RID: 30628 RVA: 0x001EDD20 File Offset: 0x001EBF20
		internal long TotalSerializedHeapBytes
		{
			get
			{
				return this.m_totalBytesSerialized;
			}
		}

		// Token: 0x060077A5 RID: 30629 RVA: 0x001EDD28 File Offset: 0x001EBF28
		protected BaseReference CreateReference(IStorable storable)
		{
			BaseReference baseReference;
			if (!this.m_referenceCreator.TryCreateReference(storable, out baseReference))
			{
				Global.Tracer.Assert(false, "Cannot create reference to: {0}", new object[] { storable });
			}
			return baseReference;
		}

		// Token: 0x060077A6 RID: 30630 RVA: 0x001EDD60 File Offset: 0x001EBF60
		[Conditional("DEBUG")]
		protected void CheckDisposed(string opName)
		{
			bool disposed = this.m_disposed;
		}

		// Token: 0x060077A7 RID: 30631 RVA: 0x001EDD69 File Offset: 0x001EBF69
		protected void UpdatePeakCacheSize()
		{
			this.m_peakCacheSizeBytes = Math.Max(this.m_peakCacheSizeBytes, this.m_cacheSizeBytes);
		}

		// Token: 0x060077A8 RID: 30632 RVA: 0x001EDD84 File Offset: 0x001EBF84
		protected void FreeCacheSpace(int count, long freeableBytes)
		{
			if (this.m_freeingSpace || this.m_inStreamOper)
			{
				return;
			}
			int num = 0;
			try
			{
				this.m_inStreamOper = true;
				this.m_freeingSpace = true;
				long num2 = Interlocked.Read(ref this.m_cacheCapacityBytes);
				if (num2 > 0L)
				{
					num = Interlocked.Exchange(ref this.m_pendingNotificationCount, 0);
					Interlocked.Exchange(ref this.m_pendingFreeBytes, 0L);
					long num3 = freeableBytes + (long)count - num2;
					if (num3 > 0L)
					{
						if (num == 0 && this.m_cacheLifetimeTimer != null && this.m_cacheLifetimeTimer.ElapsedMilliseconds - this.m_expansionIntervalMs > this.m_lastExpansionOrNotificationMs)
						{
							this.ResetExpansionOrNotificationInterval();
							long num4 = (long)((double)(this.m_totalAuditedBytes - num2) * this.m_cacheExpansionRatio);
							if (num4 > BaseScalabilityCache.CacheExpansionMaxBytes)
							{
								num4 = BaseScalabilityCache.CacheExpansionMaxBytes;
							}
							else if (num4 < 1048576L)
							{
								num4 = 1048576L;
								this.m_totalAuditedBytes += 1048576L;
							}
							if (num4 > 0L)
							{
								num2 = Interlocked.Add(ref this.m_cacheCapacityBytes, num4);
								if (Global.Tracer.TraceVerbose)
								{
									Global.Tracer.Trace(TraceLevel.Verbose, "ScalabilityCache {0}|{1} expanding cache. ExpansionKB: {2} TotalAllocation: {3} CacheSizeKB: {4} CacheCapacityKB: {5}", new object[]
									{
										this.OwnerComponent,
										this.CacheType,
										num4 / 1024L,
										this.m_totalAuditedBytes / 1024L,
										this.m_cacheSizeBytes / 1024L,
										this.m_cacheCapacityBytes / 1024L
									});
								}
								num3 -= num4;
								if (num3 <= 0L)
								{
									return;
								}
							}
						}
						Stopwatch stopwatch = null;
						if (num > 0 && Global.Tracer.TraceVerbose)
						{
							Global.Tracer.Trace(TraceLevel.Verbose, "ScalabilityCache {0}|{1} responding to pressure.  PendingNotifications: {2} CacheSizeKB: {3} CacheCapacityKB: {4}", new object[]
							{
								this.OwnerComponent,
								this.CacheType,
								num,
								this.m_cacheSizeBytes / 1024L,
								this.m_cacheCapacityBytes / 1024L
							});
							stopwatch = new Stopwatch();
							stopwatch.Start();
						}
						this.m_serializationTimer.Start();
						this.m_inProgressFreeBytes = num3;
						this.FulfillInProgressFree();
						this.m_serializationTimer.Stop();
						long num5 = num3 - this.m_inProgressFreeBytes;
						this.m_totalBytesSerialized += num5;
						this.UpdatePeakCacheSize();
						if (num > 0 && Global.Tracer.TraceVerbose)
						{
							stopwatch.Stop();
							double num6 = (double)stopwatch.ElapsedMilliseconds / 1000.0;
							long num7 = num5 / 1024L;
							double num8 = (double)num7 / num6;
							Global.Tracer.Trace(TraceLevel.Verbose, "ScalabilityCache {0}|{1} done responding to pressure.  Freed: {2} KB. Speed: {3:F2} KB/Sec.  CacheSizeKB {4} CacheCapacityKB {5}", new object[]
							{
								this.OwnerComponent,
								this.CacheType,
								num7,
								num8,
								this.m_cacheSizeBytes / 1024L,
								this.m_cacheCapacityBytes / 1024L
							});
						}
					}
				}
			}
			finally
			{
				this.m_freeingSpace = false;
				this.m_inStreamOper = false;
				this.m_inProgressFreeBytes = 0L;
				if (num > 0)
				{
					this.ResetExpansionOrNotificationInterval();
				}
			}
		}

		// Token: 0x060077A9 RID: 30633 RVA: 0x001EE0E0 File Offset: 0x001EC2E0
		private long ReadTime(Stopwatch timer)
		{
			long num = 0L;
			if (timer != null)
			{
				num = timer.ElapsedMilliseconds;
			}
			if (num < 0L)
			{
				num = -1L;
			}
			return num;
		}

		// Token: 0x060077AA RID: 30634 RVA: 0x001EE104 File Offset: 0x001EC304
		private void ResetExpansionOrNotificationInterval()
		{
			if (this.m_cacheLifetimeTimer == null)
			{
				this.m_cacheLifetimeTimer = new Stopwatch();
				this.m_cacheLifetimeTimer.Start();
				Random random = new Random();
				this.m_expansionIntervalMs += (long)random.Next(1500);
			}
			this.m_lastExpansionOrNotificationMs = this.m_cacheLifetimeTimer.ElapsedMilliseconds;
		}

		// Token: 0x170027DB RID: 10203
		// (get) Token: 0x060077AB RID: 30635 RVA: 0x001EE160 File Offset: 0x001EC360
		public sealed override long CurrentMemoryUsageKBytes
		{
			get
			{
				long num = Interlocked.Read(ref this.m_cacheCapacityBytes);
				long num2 = Interlocked.Read(ref this.m_cacheSizeBytes);
				if (num < 0L)
				{
					num = num2;
				}
				else
				{
					num = Math.Min(num, num2);
				}
				return num / 1024L;
			}
		}

		// Token: 0x060077AC RID: 30636 RVA: 0x001EE1A0 File Offset: 0x001EC3A0
		public sealed override void SetNewMemoryTarget(long memoryTargetKBytes)
		{
			long minReservedMemoryKB = this.MinReservedMemoryKB;
			if (memoryTargetKBytes < minReservedMemoryKB)
			{
				memoryTargetKBytes = this.MinReservedMemoryKB;
			}
			long num = Interlocked.Read(ref this.m_cacheCapacityBytes);
			if (Global.Tracer.TraceVerbose)
			{
				Global.Tracer.Trace(TraceLevel.Verbose, "ScalabilityCache.SetNewMemoryTarget: CacheType: {0}|{1} NewTargetKB: {2} CacheSizeKB: {3} CacheCapacityKB: {4} CurrentFreeableKB: {5} CurrentUsageKB: {6}", new object[]
				{
					this.OwnerComponent,
					this.CacheType,
					memoryTargetKBytes,
					Interlocked.Read(ref this.m_cacheSizeBytes) / 1024L,
					num / 1024L,
					this.CurrentFreeableMemoryKBytes,
					this.CurrentMemoryUsageKBytes
				});
			}
			RSTrace.SanitizedRdlEngineHostTracer.Trace(string.Format("ScalabilityCache.SetNewMemoryTarget: CacheType: {0}|{1} ", this.OwnerComponent, this.CacheType) + string.Format("NewTargetKB: {0} CacheSizeKB: {1} ", memoryTargetKBytes, Interlocked.Read(ref this.m_cacheSizeBytes) / 1024L) + string.Format("CacheCapacityKB: {0} CurrentFreeableKB: {1} CurrentUsageKB: {2}", num / 1024L, this.CurrentFreeableMemoryKBytes, this.CurrentMemoryUsageKBytes));
			Interlocked.Add(ref this.m_pendingNotificationCount, 1);
			long num2 = memoryTargetKBytes * 1024L;
			Interlocked.Exchange(ref this.m_cacheCapacityBytes, num2);
			long num3 = num - num2;
			if (num3 < 0L)
			{
				num3 = 0L;
			}
			Interlocked.Add(ref this.m_pendingFreeBytes, num3);
			if (!this.m_receivedShrinkRequest && num3 > 0L)
			{
				this.m_receivedShrinkRequest = true;
				ComponentType ownerComponent = this.OwnerComponent;
				if (ownerComponent == ComponentType.Pagination)
				{
					RSTrace.RenderingTracer.Trace(TraceLevel.Warning, "Pagination Scalability -- Memory Shrink Request Received");
					return;
				}
				if (ownerComponent == ComponentType.Rendering)
				{
					RSTrace.ExcelRendererTracer.Trace(TraceLevel.Warning, "Rendering Scalability -- Memory Shrink Request Received");
					return;
				}
				Global.Tracer.Trace(TraceLevel.Warning, "Processing Scalability -- Memory Shrink Request Received");
			}
		}

		// Token: 0x170027DC RID: 10204
		// (get) Token: 0x060077AD RID: 30637 RVA: 0x001EE375 File Offset: 0x001EC575
		public sealed override bool CanPerformPartialRelease
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170027DD RID: 10205
		// (get) Token: 0x060077AE RID: 30638 RVA: 0x001EE378 File Offset: 0x001EC578
		public sealed override double FreeOverhead
		{
			get
			{
				return Microsoft.ReportingServices.Diagnostics.FreeOverhead.High;
			}
		}

		// Token: 0x170027DE RID: 10206
		// (get) Token: 0x060077AF RID: 30639 RVA: 0x001EE37F File Offset: 0x001EC57F
		public sealed override long PendingFreeKBytes
		{
			get
			{
				return (Interlocked.Read(ref this.m_inProgressFreeBytes) + Interlocked.Read(ref this.m_pendingFreeBytes)) / 1024L;
			}
		}

		// Token: 0x060077B0 RID: 30640 RVA: 0x001EE39F File Offset: 0x001EC59F
		private static void SetDefaultCacheCapacityBytes(long defaultCapacityBytes)
		{
			BaseScalabilityCache.DefaultCacheCapacityBytes = defaultCapacityBytes;
		}

		// Token: 0x060077B1 RID: 30641 RVA: 0x001EE3A8 File Offset: 0x001EC5A8
		internal static long ComputeMaxExpansionBytes(int processorCount)
		{
			long num = (long)Math.Max(1, processorCount / 4 * 2);
			return Math.Max(15728640L / num, 1048576L);
		}

		// Token: 0x04003C43 RID: 15427
		protected bool m_disposed;

		// Token: 0x04003C44 RID: 15428
		protected long m_minReservedMemoryKB;

		// Token: 0x04003C45 RID: 15429
		protected long m_cacheSizeBytes;

		// Token: 0x04003C46 RID: 15430
		protected long m_cacheCapacityBytes = BaseScalabilityCache.DefaultCacheCapacityBytes;

		// Token: 0x04003C47 RID: 15431
		protected long m_totalAuditedBytes;

		// Token: 0x04003C48 RID: 15432
		protected long m_totalFreedBytes;

		// Token: 0x04003C49 RID: 15433
		protected int m_pendingNotificationCount;

		// Token: 0x04003C4A RID: 15434
		protected long m_inProgressFreeBytes;

		// Token: 0x04003C4B RID: 15435
		protected long m_pendingFreeBytes;

		// Token: 0x04003C4C RID: 15436
		protected bool m_freeingSpace;

		// Token: 0x04003C4D RID: 15437
		protected bool m_inStreamOper;

		// Token: 0x04003C4E RID: 15438
		protected IStorage m_storage;

		// Token: 0x04003C4F RID: 15439
		protected IReferenceCreator m_referenceCreator;

		// Token: 0x04003C50 RID: 15440
		protected ComponentType m_ownerComponent;

		// Token: 0x04003C51 RID: 15441
		protected Stopwatch m_serializationTimer = new Stopwatch();

		// Token: 0x04003C52 RID: 15442
		protected Stopwatch m_deserializationTimer = new Stopwatch();

		// Token: 0x04003C53 RID: 15443
		protected long m_totalBytesSerialized;

		// Token: 0x04003C54 RID: 15444
		protected long m_lastExpansionOrNotificationMs = -1L;

		// Token: 0x04003C55 RID: 15445
		protected long m_expansionIntervalMs;

		// Token: 0x04003C56 RID: 15446
		protected Stopwatch m_cacheLifetimeTimer;

		// Token: 0x04003C57 RID: 15447
		protected double m_cacheExpansionRatio;

		// Token: 0x04003C58 RID: 15448
		protected bool m_receivedShrinkRequest;

		// Token: 0x04003C59 RID: 15449
		protected long m_peakCacheSizeBytes = -1L;

		// Token: 0x04003C5A RID: 15450
		protected static long DefaultCacheCapacityBytes = -1L;

		// Token: 0x04003C5B RID: 15451
		protected static readonly long CacheExpansionMaxBytes = BaseScalabilityCache.ComputeMaxExpansionBytes(Environment.ProcessorCount);

		// Token: 0x04003C5C RID: 15452
		internal const long DefaultCacheExpansionMaxBytes = 15728640L;

		// Token: 0x04003C5D RID: 15453
		internal const long CacheExpansionMinBytes = 1048576L;
	}
}
