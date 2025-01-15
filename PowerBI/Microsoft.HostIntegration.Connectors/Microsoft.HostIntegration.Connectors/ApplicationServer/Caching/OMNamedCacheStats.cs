using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000286 RID: 646
	internal sealed class OMNamedCacheStats : OMBaseStats
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060016E0 RID: 5856 RVA: 0x00045BF2 File Offset: 0x00043DF2
		internal OMCacheNodeStats NodeStats
		{
			get
			{
				return this._nodeStats;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x00045BFA File Offset: 0x00043DFA
		// (set) Token: 0x060016E2 RID: 5858 RVA: 0x00045C07 File Offset: 0x00043E07
		public long Size
		{
			get
			{
				return this._size.GetValue();
			}
			internal set
			{
				this._size.Add(-this._size.GetValue() + value);
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060016E3 RID: 5859 RVA: 0x00045C22 File Offset: 0x00043E22
		// (set) Token: 0x060016E4 RID: 5860 RVA: 0x00045C2F File Offset: 0x00043E2F
		public long Count
		{
			get
			{
				return this._count.GetValue();
			}
			internal set
			{
				this._count.SetValue(value);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00045C3D File Offset: 0x00043E3D
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x00045C45 File Offset: 0x00043E45
		public int RegionCount
		{
			get
			{
				return this._rCount;
			}
			internal set
			{
				this._rCount = value;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x00045C4E File Offset: 0x00043E4E
		// (set) Token: 0x060016E8 RID: 5864 RVA: 0x00045C5B File Offset: 0x00043E5B
		public long TotalReqs
		{
			get
			{
				return this._totalReqs.GetValue();
			}
			internal set
			{
				this._totalReqs.SetValue(value);
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060016E9 RID: 5865 RVA: 0x00045C69 File Offset: 0x00043E69
		// (set) Token: 0x060016EA RID: 5866 RVA: 0x00045C76 File Offset: 0x00043E76
		public long RestTotalRequests
		{
			get
			{
				return this._restTotalRequests.GetValue();
			}
			internal set
			{
				this._restTotalRequests.SetValue(value);
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00045C84 File Offset: 0x00043E84
		// (set) Token: 0x060016EC RID: 5868 RVA: 0x00045C91 File Offset: 0x00043E91
		public long IncomingBandwidth
		{
			get
			{
				return this._incomingBandwidth.GetValue();
			}
			internal set
			{
				this._incomingBandwidth.SetValue(value);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060016ED RID: 5869 RVA: 0x00045C9F File Offset: 0x00043E9F
		// (set) Token: 0x060016EE RID: 5870 RVA: 0x00045CAC File Offset: 0x00043EAC
		public long OutgoingBandwidth
		{
			get
			{
				return this._outgoingBandwidth.GetValue();
			}
			internal set
			{
				this._outgoingBandwidth.SetValue(value);
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x00045CBA File Offset: 0x00043EBA
		public long TotalBandwidth
		{
			get
			{
				return this._incomingBandwidth.GetValue() + this._outgoingBandwidth.GetValue();
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060016F0 RID: 5872 RVA: 0x00045CD3 File Offset: 0x00043ED3
		public long Miss
		{
			get
			{
				return this._miss.GetValue();
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00045CE0 File Offset: 0x00043EE0
		// (set) Token: 0x060016F2 RID: 5874 RVA: 0x00045CED File Offset: 0x00043EED
		public long GetReqs
		{
			get
			{
				return this._getReqs.GetValue();
			}
			internal set
			{
				this._getReqs.SetValue(value);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00045CFB File Offset: 0x00043EFB
		// (set) Token: 0x060016F4 RID: 5876 RVA: 0x00045D08 File Offset: 0x00043F08
		public long AddReqs
		{
			get
			{
				return this._addReqs.GetValue();
			}
			internal set
			{
				this._addReqs.SetValue(value);
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00045D16 File Offset: 0x00043F16
		internal long GetRequestMiss
		{
			get
			{
				return this._getRequestMiss.GetValue();
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00045D23 File Offset: 0x00043F23
		// (set) Token: 0x060016F7 RID: 5879 RVA: 0x00045D30 File Offset: 0x00043F30
		public long UpsertReqs
		{
			get
			{
				return this._upsertReqs.GetValue();
			}
			internal set
			{
				this._upsertReqs.SetValue(value);
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x00045D3E File Offset: 0x00043F3E
		// (set) Token: 0x060016F9 RID: 5881 RVA: 0x00045D4B File Offset: 0x00043F4B
		public long DelReqs
		{
			get
			{
				return this._delReqs.GetValue();
			}
			internal set
			{
				this._delReqs.SetValue(value);
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x00045D59 File Offset: 0x00043F59
		// (set) Token: 0x060016FB RID: 5883 RVA: 0x00045D66 File Offset: 0x00043F66
		public long EvictReqs
		{
			get
			{
				return this._evictReqs.GetValue();
			}
			internal set
			{
				this._evictReqs.SetValue(value);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x00045D74 File Offset: 0x00043F74
		internal OMWriteBehindCacheStats WriteBehindStats
		{
			get
			{
				return this._wbStats;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00045D7C File Offset: 0x00043F7C
		public long PrimarySize
		{
			get
			{
				return this._primaryDataSize.GetValue();
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x00045D89 File Offset: 0x00043F89
		public long SecondarySize
		{
			get
			{
				return this._secondaryDataSize.GetValue();
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x00045D96 File Offset: 0x00043F96
		public OMReadThroughStats ReadThroughStats
		{
			get
			{
				return this._readThroughStats;
			}
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00045DA0 File Offset: 0x00043FA0
		internal OMNamedCacheStats(OMCacheNodeStats nodeStats, string cacheName)
			: base(nodeStats.ExternalStatsContainer)
		{
			if (base.ExternalStatsContainer == null)
			{
				this._size = new SafeValueStore();
				this._primaryDataSize = new NormalValueStore();
				this._secondaryDataSize = new NormalValueStore();
			}
			else
			{
				this._size = new ExternalStatsBasedValueStore(cacheName, new StatsValue(base.GetTotalMemoryFromCacheStats));
				this._primaryDataSize = new ExternalStatsBasedValueStore(cacheName, new StatsValue(base.GetPrimaryMemoryFromCacheStats));
				this._secondaryDataSize = new ExternalStatsBasedValueStore(cacheName, new StatsValue(base.GetSecodaryMemoryFromCacheStats));
			}
			this._addReqs = new SafeValueStore();
			this._count = new SafeValueStore();
			this._delReqs = new SafeValueStore();
			this._evictReqs = new SafeValueStore();
			this._getAndLockCount = new SafeValueStore();
			this._getReqs = new SafeValueStore();
			this._getRequestMiss = new SafeValueStore();
			this._incomingBandwidth = new SafeValueStore();
			this._miss = new SafeValueStore();
			this._objectReturned = new SafeValueStore();
			this._outgoingBandwidth = new SafeValueStore();
			this._readRequest = new SafeValueStore();
			this._restTotalRequests = new SafeValueStore();
			this._successfulgetAndLockCount = new SafeValueStore();
			this._totalEvictionCount = new SafeValueStore();
			this._totalReqs = new SafeValueStore();
			this._upsertReqs = new SafeValueStore();
			this._wbCount = new SafeValueStore();
			this._writeRequest = new SafeValueStore();
			this._nodeStats = nodeStats;
			this._wbStats = new OMWriteBehindCacheStats(cacheName);
			this._readThroughStats = new OMReadThroughStats(cacheName, nodeStats);
			if (this._nodeStats.PerfMonCounterRequired)
			{
				if (CachePerfCounter.CreateInstance(cacheName))
				{
					this._totalDataSizeCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_DATA_SIZE, cacheName, new PerfCounterValue(this._size.GetValue));
					this._totalPrimaryDataSizeCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_PRIMARY_DATA_SIZE, cacheName, new PerfCounterValue(this._primaryDataSize.GetValue));
					this._totalSecondarDataSizeCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_SECONDARY_DATA_SIZE, cacheName, new PerfCounterValue(this._secondaryDataSize.GetValue));
					this._totalObjectCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_OBJECT_COUNT, cacheName, new PerfCounterValue(this.GetObjectCount));
					this._missCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_MISS_COUNT, cacheName, new PerfCounterValue(this.GetMiss));
					this._missPerSecCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_MISS_COUNT_PER_SEC, cacheName, new PerfCounterValue(this.GetMiss));
					this._percentageMiss = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.PERCENTAGE_MISS, cacheName, new PerfCounterValue(this.GetMiss));
					this._percentageMissBase = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.PERCENTAGE_MISS_BASE, cacheName, new PerfCounterValue(this.GetTotalRequest));
					this._readRequestCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_READ_REQUEST, cacheName, new PerfCounterValue(this.GetReadRequestCount));
					this._readRequestPerSecCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_READ_REQUEST_PER_SEC, cacheName, new PerfCounterValue(this.GetReadRequestCount));
					this._writeRequestCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_WRITE_REQUEST, cacheName, new PerfCounterValue(this.GetWriteRequestCount));
					this._writeRequestPerSecCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_WRITE_REQUEST_PER_SEC, cacheName, new PerfCounterValue(this.GetWriteRequestCount));
					this._objectReturnedCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_OBJECTS_RETURNED, cacheName, new PerfCounterValue(this.GetObjectReturned));
					this._objectReturnedPerSecCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_OBJECTS_RETURNED_PER_SEC, cacheName, new PerfCounterValue(this.GetObjectReturned));
					this._getAndLockCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_GETANDLOCK_REQUEST, cacheName, new PerfCounterValue(this.GetGetAndLock));
					this._getAndLockPerSecCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_GETANDLOCK_REQUEST_PER_SEC, cacheName, new PerfCounterValue(this.GetGetAndLock));
					this._successfulgetAndLockCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_SUCCESSFULGETANDLOCK_REQUEST, cacheName, new PerfCounterValue(this.GetSuccessfulGetAndLock));
					this._successfulgetAndLockPerSecCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_SUCCESSFULGETANDLOCK_REQUEST_PER_SEC, cacheName, new PerfCounterValue(this.GetSuccessfulGetAndLock));
					this._totalRequestCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_REQUEST, cacheName, new PerfCounterValue(this.GetTotalRequest));
					this._totalRequestPerSecCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_REQUEST_PER_SEC, cacheName, new PerfCounterValue(this.GetTotalRequest));
					this._totalEvictioRuns = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.TOTAL_EVICTION_RUNS, cacheName, new PerfCounterValue(this.GetTotalEvictionRuns));
					this._wbStats.InitializePerfCounters();
					this._readThroughStats.InitializePerfCounters();
					this._countersInitialized = true;
					return;
				}
				if (Provider.IsEnabled(TraceLevel.Error))
				{
					EventLogWriter.WriteError(PerfCounter.LogSource, "Failed to create cache instance = " + cacheName, new object[0]);
				}
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00045C22 File Offset: 0x00043E22
		internal long GetObjectCount()
		{
			return this._count.GetValue();
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000461A4 File Offset: 0x000443A4
		internal long GetGetAndLock()
		{
			return this._getAndLockCount.GetValue();
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000461B1 File Offset: 0x000443B1
		internal void IncrGetAndLock()
		{
			this._getAndLockCount.Increment();
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x000461BE File Offset: 0x000443BE
		internal long GetSuccessfulGetAndLock()
		{
			return this._successfulgetAndLockCount.GetValue();
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000461CB File Offset: 0x000443CB
		internal void IncrSuccessfulGetAndLock()
		{
			this._successfulgetAndLockCount.Increment();
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x000461D8 File Offset: 0x000443D8
		internal long GetObjectReturned()
		{
			return this._objectReturned.GetValue();
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x000461E5 File Offset: 0x000443E5
		internal void IncrObjectReturnedCountBy(long count)
		{
			this._objectReturned.Add(count);
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x000461F3 File Offset: 0x000443F3
		internal long GetWriteRequestCount()
		{
			return this._writeRequest.GetValue();
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00046200 File Offset: 0x00044400
		internal void IncrWriteRequestBy(long count)
		{
			this._writeRequest.Add(count);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0004620E File Offset: 0x0004440E
		internal long GetReadRequestCount()
		{
			return this._readRequest.GetValue();
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0004621B File Offset: 0x0004441B
		internal void IncrReadRequestBy(long count)
		{
			this._readRequest.Add(count);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x00045CD3 File Offset: 0x00043ED3
		internal long GetMiss()
		{
			return this._miss.GetValue();
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00045C4E File Offset: 0x00043E4E
		internal long GetTotalRequest()
		{
			return this._totalReqs.GetValue();
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00045C69 File Offset: 0x00043E69
		internal long GetTotalRestRequest()
		{
			return this._restTotalRequests.GetValue();
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00045CBA File Offset: 0x00043EBA
		internal long GetTotalBandwidth()
		{
			return this._incomingBandwidth.GetValue() + this._outgoingBandwidth.GetValue();
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00046229 File Offset: 0x00044429
		internal void IncrEvictionRuns()
		{
			this._totalEvictionCount.Increment();
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00046236 File Offset: 0x00044436
		internal long GetTotalEvictionRuns()
		{
			return this._totalEvictionCount.GetValue();
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00046243 File Offset: 0x00044443
		internal void IncrCount()
		{
			this._count.Increment();
			this._nodeStats.IncrCount();
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0004625B File Offset: 0x0004445B
		internal void IncrWBCount()
		{
			this._wbCount.Increment();
			this._nodeStats.AddWBCount(1);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00046274 File Offset: 0x00044474
		internal void IncrCount(int count)
		{
			this._count.Add((long)count);
			this._nodeStats.IncrCount(count);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0004628F File Offset: 0x0004448F
		internal void IncrMiss()
		{
			this._miss.Increment();
			this._nodeStats.IncrMiss();
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x000462A7 File Offset: 0x000444A7
		internal void DecrMiss()
		{
			this._miss.Decrement();
			this._nodeStats.DecrMiss();
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000462BF File Offset: 0x000444BF
		internal void DecrTotal()
		{
			this._totalReqs.Decrement();
			this._nodeStats.DecrTotal();
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000462D7 File Offset: 0x000444D7
		internal void IncrTotalReqs()
		{
			this._totalReqs.Increment();
			this._nodeStats.IncrTotalReqs();
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000462EF File Offset: 0x000444EF
		internal void IncrTotalRestReqs()
		{
			this._restTotalRequests.Increment();
			this._nodeStats.IncrRestTotalReqs();
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00046307 File Offset: 0x00044507
		internal void IncrIncomingBandwidthBy(long count)
		{
			this._incomingBandwidth.Add(count);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00046315 File Offset: 0x00044515
		internal void IncrOutgoingBandwidthBy(long count)
		{
			this._outgoingBandwidth.Add(count);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00046323 File Offset: 0x00044523
		internal void IncrGetReqs()
		{
			this._getReqs.Increment();
			this._totalReqs.Increment();
			this._nodeStats.IncrGetReqs();
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00046346 File Offset: 0x00044546
		internal void IncrAddReqs()
		{
			this._addReqs.Increment();
			this._totalReqs.Increment();
			this._nodeStats.IncrAddReqs();
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00046369 File Offset: 0x00044569
		internal void IncrDelReqs()
		{
			this._delReqs.Increment();
			this._totalReqs.Increment();
			this._nodeStats.IncrDelReqs();
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0004638C File Offset: 0x0004458C
		internal void IncrUpsertReqs()
		{
			this._upsertReqs.Increment();
			this._totalReqs.Increment();
			this._nodeStats.IncrUpsertReqs();
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x000463AF File Offset: 0x000445AF
		internal void IncrementGetRequestMiss()
		{
			this._getRequestMiss.Increment();
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x000463BC File Offset: 0x000445BC
		internal void IncrEvictReqs()
		{
			this._evictReqs.Increment();
			this._nodeStats.IncrEvictReqs();
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x000463D4 File Offset: 0x000445D4
		internal void IncrRegionCount()
		{
			Interlocked.Increment(ref this._rCount);
			this._nodeStats.IncrRegionCount();
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000463ED File Offset: 0x000445ED
		internal void IncrRegionCount(int count)
		{
			Interlocked.Add(ref this._rCount, count);
			this._nodeStats.IncrRegionCount(count);
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00046408 File Offset: 0x00044608
		internal void DecrRegionCount()
		{
			Interlocked.Decrement(ref this._rCount);
			if (this._rCount < 0 && Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning("OMNamedCacheStats", "RegionCount is negative. Current: {0}, Last increment: -1", new object[] { this._rCount });
			}
			this._nodeStats.DecrRegionCount();
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00046464 File Offset: 0x00044664
		internal void DecrRegionCount(int count)
		{
			Interlocked.Add(ref this._rCount, -count);
			if (this._rCount < 0 && Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning("OMNamedCacheStats", "RegionCount is negative. Current: {0}, Last increment: {1}", new object[]
				{
					this._rCount,
					-count
				});
			}
			this._nodeStats.DecrRegionCount(count);
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x000464CC File Offset: 0x000446CC
		internal void DecrCount()
		{
			this._count.Decrement();
			if (this._count.GetValue() < 0L && Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning("OMNamedCacheStats", "Count is negative. Current: {0}, Last increment: -1", new object[] { this._count.GetValue() });
			}
			this._nodeStats.DecrCount();
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x00046530 File Offset: 0x00044730
		internal void DecrWBCount()
		{
			this._count.Decrement();
			this._nodeStats.DecrWBCount(1L);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0004654C File Offset: 0x0004474C
		internal void DecrCount(long count)
		{
			this._count.Add(-count);
			if (this._count.GetValue() < 0L && Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning("OMNamedCacheStats", "Count is negative. Current: {0}, Last increment: {1}", new object[]
				{
					this._count.GetValue(),
					-count
				});
			}
			this._nodeStats.DecrCount(count);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x000465BD File Offset: 0x000447BD
		internal void IncrSize(long size)
		{
			this._size.Add(size);
			this._nodeStats.IncrSize(size);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000465D8 File Offset: 0x000447D8
		internal void DecrSize(long size)
		{
			this._size.Add(-size);
			if (this._size.GetValue() < 0L && Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("OMNamedCacheStats", "Size is negative. Current: {0}, Last increment: {1}", new object[]
				{
					this._size.GetValue(),
					-size
				});
			}
			this._nodeStats.DecrSize(size);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0004664C File Offset: 0x0004484C
		internal void Close(string cacheName)
		{
			if (this._nodeStats.PerfMonCounterRequired && this._countersInitialized)
			{
				this._totalDataSizeCounter.Delete();
				this._missCounter.Delete();
				this._missPerSecCounter.Delete();
				this._percentageMiss.Delete();
				this._percentageMissBase.Delete();
				this._readRequestCounter.Delete();
				this._readRequestPerSecCounter.Delete();
				this._writeRequestCounter.Delete();
				this._writeRequestPerSecCounter.Delete();
				this._objectReturnedCounter.Delete();
				this._objectReturnedPerSecCounter.Delete();
				this._getAndLockCounter.Delete();
				this._getAndLockPerSecCounter.Delete();
				this._successfulgetAndLockCounter.Delete();
				this._successfulgetAndLockPerSecCounter.Delete();
				this._totalRequestCounter.Delete();
				this._totalRequestPerSecCounter.Delete();
				this._totalObjectCounter.Delete();
				this._totalPrimaryDataSizeCounter.Delete();
				this._totalSecondarDataSizeCounter.Delete();
				this._wbStats.Delete();
				this._readThroughStats.Delete();
				this._totalEvictioRuns.Delete();
				CachePerfCounter.RemoveInstance(cacheName);
				this._countersInitialized = false;
			}
		}

		// Token: 0x04000CCD RID: 3277
		private bool _countersInitialized;

		// Token: 0x04000CCE RID: 3278
		private OMCacheNodeStats _nodeStats;

		// Token: 0x04000CCF RID: 3279
		private IValueStore _size;

		// Token: 0x04000CD0 RID: 3280
		private DelegateBasedCachePerfCounter _totalDataSizeCounter;

		// Token: 0x04000CD1 RID: 3281
		private IValueStore _count;

		// Token: 0x04000CD2 RID: 3282
		private IValueStore _wbCount;

		// Token: 0x04000CD3 RID: 3283
		private DelegateBasedCachePerfCounter _totalObjectCounter;

		// Token: 0x04000CD4 RID: 3284
		private int _rCount;

		// Token: 0x04000CD5 RID: 3285
		private IValueStore _totalReqs;

		// Token: 0x04000CD6 RID: 3286
		private IValueStore _restTotalRequests;

		// Token: 0x04000CD7 RID: 3287
		private IValueStore _incomingBandwidth;

		// Token: 0x04000CD8 RID: 3288
		private IValueStore _outgoingBandwidth;

		// Token: 0x04000CD9 RID: 3289
		private IValueStore _miss;

		// Token: 0x04000CDA RID: 3290
		private DelegateBasedCachePerfCounter _missCounter;

		// Token: 0x04000CDB RID: 3291
		private DelegateBasedCachePerfCounter _missPerSecCounter;

		// Token: 0x04000CDC RID: 3292
		private DelegateBasedCachePerfCounter _percentageMiss;

		// Token: 0x04000CDD RID: 3293
		private DelegateBasedCachePerfCounter _percentageMissBase;

		// Token: 0x04000CDE RID: 3294
		private IValueStore _getReqs;

		// Token: 0x04000CDF RID: 3295
		private IValueStore _addReqs;

		// Token: 0x04000CE0 RID: 3296
		private IValueStore _getRequestMiss;

		// Token: 0x04000CE1 RID: 3297
		private IValueStore _upsertReqs;

		// Token: 0x04000CE2 RID: 3298
		private IValueStore _delReqs;

		// Token: 0x04000CE3 RID: 3299
		private IValueStore _evictReqs;

		// Token: 0x04000CE4 RID: 3300
		private IValueStore _readRequest;

		// Token: 0x04000CE5 RID: 3301
		private DelegateBasedCachePerfCounter _readRequestCounter;

		// Token: 0x04000CE6 RID: 3302
		private DelegateBasedCachePerfCounter _readRequestPerSecCounter;

		// Token: 0x04000CE7 RID: 3303
		private IValueStore _writeRequest;

		// Token: 0x04000CE8 RID: 3304
		private DelegateBasedCachePerfCounter _writeRequestCounter;

		// Token: 0x04000CE9 RID: 3305
		private DelegateBasedCachePerfCounter _writeRequestPerSecCounter;

		// Token: 0x04000CEA RID: 3306
		private IValueStore _objectReturned;

		// Token: 0x04000CEB RID: 3307
		private DelegateBasedCachePerfCounter _objectReturnedCounter;

		// Token: 0x04000CEC RID: 3308
		private DelegateBasedCachePerfCounter _objectReturnedPerSecCounter;

		// Token: 0x04000CED RID: 3309
		private IValueStore _getAndLockCount;

		// Token: 0x04000CEE RID: 3310
		private DelegateBasedCachePerfCounter _getAndLockCounter;

		// Token: 0x04000CEF RID: 3311
		private DelegateBasedCachePerfCounter _getAndLockPerSecCounter;

		// Token: 0x04000CF0 RID: 3312
		private IValueStore _successfulgetAndLockCount;

		// Token: 0x04000CF1 RID: 3313
		private DelegateBasedCachePerfCounter _successfulgetAndLockCounter;

		// Token: 0x04000CF2 RID: 3314
		private DelegateBasedCachePerfCounter _successfulgetAndLockPerSecCounter;

		// Token: 0x04000CF3 RID: 3315
		private DelegateBasedCachePerfCounter _totalRequestCounter;

		// Token: 0x04000CF4 RID: 3316
		private DelegateBasedCachePerfCounter _totalRequestPerSecCounter;

		// Token: 0x04000CF5 RID: 3317
		private IValueStore _primaryDataSize;

		// Token: 0x04000CF6 RID: 3318
		private DelegateBasedCachePerfCounter _totalPrimaryDataSizeCounter;

		// Token: 0x04000CF7 RID: 3319
		private IValueStore _totalEvictionCount;

		// Token: 0x04000CF8 RID: 3320
		private DelegateBasedCachePerfCounter _totalEvictioRuns;

		// Token: 0x04000CF9 RID: 3321
		private OMWriteBehindCacheStats _wbStats;

		// Token: 0x04000CFA RID: 3322
		private IValueStore _secondaryDataSize;

		// Token: 0x04000CFB RID: 3323
		private DelegateBasedCachePerfCounter _totalSecondarDataSizeCounter;

		// Token: 0x04000CFC RID: 3324
		private OMReadThroughStats _readThroughStats;
	}
}
