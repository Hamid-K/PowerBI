using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000282 RID: 642
	internal sealed class OMCacheNodeStats : OMBaseStats
	{
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001664 RID: 5732 RVA: 0x00044B1E File Offset: 0x00042D1E
		public long Size
		{
			get
			{
				return this._size.GetValue();
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00044B2B File Offset: 0x00042D2B
		public long PrimarySize
		{
			get
			{
				return this._primaryDataSize.GetValue();
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001666 RID: 5734 RVA: 0x00044B38 File Offset: 0x00042D38
		public long SecondarySize
		{
			get
			{
				return this._secondaryDataSize.GetValue();
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x00044B45 File Offset: 0x00042D45
		public long Count
		{
			get
			{
				return this._count.GetValue();
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001668 RID: 5736 RVA: 0x00044B52 File Offset: 0x00042D52
		public long WBCount
		{
			get
			{
				return this._wbCount.GetValue();
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x00044B5F File Offset: 0x00042D5F
		// (set) Token: 0x0600166A RID: 5738 RVA: 0x00044B67 File Offset: 0x00042D67
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

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x00044B70 File Offset: 0x00042D70
		// (set) Token: 0x0600166C RID: 5740 RVA: 0x00044B78 File Offset: 0x00042D78
		public int NamedCacheCount
		{
			get
			{
				return this._ncCount;
			}
			internal set
			{
				this._ncCount = value;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x00044B81 File Offset: 0x00042D81
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x00044B8E File Offset: 0x00042D8E
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

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x00044B9C File Offset: 0x00042D9C
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x00044BA9 File Offset: 0x00042DA9
		public long TotalRestReqs
		{
			get
			{
				return this._totalRestReqs.GetValue();
			}
			internal set
			{
				this._totalRestReqs.SetValue(value);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x00044BB7 File Offset: 0x00042DB7
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x00044BC4 File Offset: 0x00042DC4
		public long Miss
		{
			get
			{
				return this._miss.GetValue();
			}
			internal set
			{
				this._miss.SetValue(value);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x00044BD2 File Offset: 0x00042DD2
		// (set) Token: 0x06001674 RID: 5748 RVA: 0x00044BDF File Offset: 0x00042DDF
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

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x00044BED File Offset: 0x00042DED
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x00044BFA File Offset: 0x00042DFA
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

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x00044C08 File Offset: 0x00042E08
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x00044C15 File Offset: 0x00042E15
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

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x00044C23 File Offset: 0x00042E23
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x00044C30 File Offset: 0x00042E30
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

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x00044C3E File Offset: 0x00042E3E
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x00044C4B File Offset: 0x00042E4B
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

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x00044C59 File Offset: 0x00042E59
		public bool PerfMonCounterRequired
		{
			get
			{
				return this._perfMonCounterRequired;
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00044C64 File Offset: 0x00042E64
		public OMCacheNodeStats(bool perfMonCounterRequired, ICacheStatsContainer statsContainer)
			: base(statsContainer)
		{
			if (statsContainer != null)
			{
				this._size = new ExternalStatsBasedValueStore(null, new StatsValue(base.GetTotalMemoryFromCacheStats));
				this._primaryDataSize = new ExternalStatsBasedValueStore(null, new StatsValue(base.GetPrimaryMemoryFromCacheStats));
				this._secondaryDataSize = new ExternalStatsBasedValueStore(null, new StatsValue(base.GetSecodaryMemoryFromCacheStats));
			}
			else
			{
				this._size = new SafeValueStore();
				this._primaryDataSize = new NormalValueStore();
				this._secondaryDataSize = new NormalValueStore();
			}
			this._count = new SafeValueStore();
			this._addReqs = new SafeValueStore();
			this._delReqs = new SafeValueStore();
			this._evictReqs = new SafeValueStore();
			this._getReqs = new SafeValueStore();
			this._miss = new SafeValueStore();
			this._rtErrorCount = new SafeValueStore();
			this._rtMissedCount = new SafeValueStore();
			this._rtPendingCount = new SafeValueStore();
			this._rtSuccessCount = new SafeValueStore();
			this._totalReqs = new SafeValueStore();
			this._totalRestReqs = new SafeValueStore();
			this._upsertReqs = new SafeValueStore();
			this._wbCount = new SafeValueStore();
			this._perfMonCounterRequired = perfMonCounterRequired;
			if (this._perfMonCounterRequired)
			{
				this._totalDataSizeCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_DATA_SIZE, new PerfCounterValue(this._size.GetValue));
				this._primaryDataSizeCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_PRIMARY_DATA_SIZE, new PerfCounterValue(this._primaryDataSize.GetValue));
				this._secondaryDataSizeCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_SECONDARY_DATA_SIZE, new PerfCounterValue(this._secondaryDataSize.GetValue));
				this._totalObjectCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_OBJECT_COUNT, new PerfCounterValue(this._count.GetValue));
				this._totalMissCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_MISS_COUNT, new PerfCounterValue(this.GetTotalMissCount));
				this._totalMissPerSecCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_MISS_COUNT_PER_SEC, new PerfCounterValue(this.GetTotalMissCount));
				this._totalGetRequest = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_GET_REQUEST, new PerfCounterValue(this.GetTotalGetRequestCount));
				this._totalGetRequestPerSecond = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.TOTAL_GET_REQUEST_PER_SEC, new PerfCounterValue(this.GetTotalGetRequestCount));
				this._percentageMissCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.PERCENTAGE_MISS, new PerfCounterValue(this.GetTotalMissCount));
				this._percentageMissBaseCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.PERCENTAGE_MISS_BASE, new PerfCounterValue(this.GetTotalRequestCount));
				this._rtPendingCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.RT_PENDING_READS, new PerfCounterValue(this.GetRTPendingCount));
				this._rtSuccessCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.RT_SUCCESSFUL_READS, new PerfCounterValue(this.GetRTSuccessCount));
				this._rtMissedCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.RT_MISSED_READS, new PerfCounterValue(this.GetRTMissedCount));
				this._rtErrorCounter = new DelegateBasedHostPerfCounter(HostPerfCounter.Name.RT_FAILED_READS, new PerfCounterValue(this.GetRTErrorCount));
			}
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00044F18 File Offset: 0x00043118
		private long GetRTErrorCount()
		{
			return this._rtErrorCount.GetValue();
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00044F25 File Offset: 0x00043125
		private long GetRTMissedCount()
		{
			return this._rtMissedCount.GetValue();
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00044F32 File Offset: 0x00043132
		private long GetRTSuccessCount()
		{
			return this._rtSuccessCount.GetValue();
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00044F3F File Offset: 0x0004313F
		private long GetRTPendingCount()
		{
			return this._rtPendingCount.GetValue();
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00044F4C File Offset: 0x0004314C
		internal void AddRTErrorCount(long value)
		{
			this._rtErrorCount.Add(value);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00044F5A File Offset: 0x0004315A
		internal void AddRTMissedCount(long value)
		{
			this._rtMissedCount.Add(value);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00044F68 File Offset: 0x00043168
		internal void AddRTSuccessCount(long value)
		{
			this._rtSuccessCount.Add(value);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00044F76 File Offset: 0x00043176
		internal void AddRTPendingCount(long value)
		{
			this._rtPendingCount.Add(value);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00044F84 File Offset: 0x00043184
		internal long GetTotalMissCount()
		{
			return this.Miss;
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00044F8C File Offset: 0x0004318C
		internal long GetTotalGetRequestCount()
		{
			return this.GetReqs;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00044B81 File Offset: 0x00042D81
		internal long GetTotalRequestCount()
		{
			return this._totalReqs.GetValue();
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00044B9C File Offset: 0x00042D9C
		internal long GetTotalRestRequestCount()
		{
			return this._totalRestReqs.GetValue();
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00044F94 File Offset: 0x00043194
		internal void IncrCount()
		{
			this.IncrCount(1);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00044F9D File Offset: 0x0004319D
		internal void IncrCount(int count)
		{
			this._count.Add((long)count);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00044FAC File Offset: 0x000431AC
		internal void AddWBCount(int count)
		{
			this._wbCount.Add((long)count);
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00044FBB File Offset: 0x000431BB
		internal void DecrCount()
		{
			this.DecrCount(1L);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00044FC8 File Offset: 0x000431C8
		internal void DecrCount(long count)
		{
			this._count.Add(-count);
			if (this._count.GetValue() < 0L && Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning(this._logSource, "Count is negative. Current: {0}, Last increment: {1}", new object[]
				{
					this._count.GetValue(),
					-count
				});
			}
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x0004502E File Offset: 0x0004322E
		internal void DecrWBCount(long count)
		{
			this._wbCount.Add(-count);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0004503D File Offset: 0x0004323D
		internal void IncrSize(long size)
		{
			this._size.Add(size);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0004504B File Offset: 0x0004324B
		internal void DecrSize(long size)
		{
			this._size.Add(-size);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0004505A File Offset: 0x0004325A
		internal void IncrMiss()
		{
			this._miss.Increment();
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00045067 File Offset: 0x00043267
		internal void DecrMiss()
		{
			this._miss.Decrement();
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00045074 File Offset: 0x00043274
		internal void DecrRestTotal()
		{
			this._totalRestReqs.Decrement();
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00045081 File Offset: 0x00043281
		internal void IncrRestTotalReqs()
		{
			this._totalRestReqs.Increment();
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x0004508E File Offset: 0x0004328E
		internal void DecrTotal()
		{
			this._totalReqs.Decrement();
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x0004509B File Offset: 0x0004329B
		internal void IncrTotalReqs()
		{
			this._totalReqs.Increment();
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x000450A8 File Offset: 0x000432A8
		internal void IncrGetReqs()
		{
			this._getReqs.Increment();
			this._totalReqs.Increment();
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000450C0 File Offset: 0x000432C0
		internal void IncrAddReqs()
		{
			this._addReqs.Increment();
			this._totalReqs.Increment();
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x000450D8 File Offset: 0x000432D8
		internal void IncrDelReqs()
		{
			this._delReqs.Increment();
			this._totalReqs.Increment();
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x000450F0 File Offset: 0x000432F0
		internal void IncrUpsertReqs()
		{
			this._upsertReqs.Increment();
			this._totalReqs.Increment();
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00045108 File Offset: 0x00043308
		internal void IncrEvictReqs()
		{
			this._evictReqs.Increment();
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00045115 File Offset: 0x00043315
		internal void IncrRegionCount()
		{
			Interlocked.Increment(ref this._rCount);
			if (Provider.IsEnabled(TraceLevel.Verbose))
			{
				EventLogWriter.WriteVerbose<int>(this._logSource, "Region Count = {0}", this._rCount);
			}
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00045144 File Offset: 0x00043344
		internal void DecrRegionCount()
		{
			Interlocked.Decrement(ref this._rCount);
			if (this._rCount < 0 && Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning(this._logSource, "Region count is negative. Current: {0}, Last increment: -1", new object[] { this._rCount });
			}
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00045194 File Offset: 0x00043394
		internal void IncrNamedCacheCount()
		{
			Interlocked.Increment(ref this._ncCount);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x000451A4 File Offset: 0x000433A4
		internal void DecrNamedCacheCount()
		{
			Interlocked.Decrement(ref this._ncCount);
			if (this._ncCount < 0 && Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning(this._logSource, "Named cache count is negative. Current: {0}, Last increment: -1", new object[] { this._ncCount });
			}
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x000451F4 File Offset: 0x000433F4
		internal void IncrRegionCount(int rCount)
		{
			Interlocked.Add(ref this._rCount, rCount);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00045204 File Offset: 0x00043404
		internal void DecrRegionCount(int rCount)
		{
			Interlocked.Add(ref this._rCount, -rCount);
			if (this._rCount < 0 && Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning(this._logSource, "Region count is negative. Current: {0}, Last increment: {1}", new object[]
				{
					this._rCount,
					-rCount
				});
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00045260 File Offset: 0x00043460
		internal void IncrNamedCacheCount(int ncCount)
		{
			Interlocked.Add(ref this._ncCount, ncCount);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00045270 File Offset: 0x00043470
		internal void DecrNamedCacheCount(int ncCount)
		{
			Interlocked.Add(ref this._ncCount, -ncCount);
			if (this._ncCount < 0 && Provider.IsEnabled(TraceLevel.Warning))
			{
				EventLogWriter.WriteWarning(this._logSource, "Cache count is negative. Current: {0}, Last increment: {1}", new object[]
				{
					this._ncCount,
					-ncCount
				});
			}
		}

		// Token: 0x04000C99 RID: 3225
		private string _logSource = ObjectManager.LogSource + ".NodeStats";

		// Token: 0x04000C9A RID: 3226
		private DelegateBasedHostPerfCounter _totalDataSizeCounter;

		// Token: 0x04000C9B RID: 3227
		private IValueStore _size;

		// Token: 0x04000C9C RID: 3228
		private IValueStore _primaryDataSize;

		// Token: 0x04000C9D RID: 3229
		private DelegateBasedHostPerfCounter _primaryDataSizeCounter;

		// Token: 0x04000C9E RID: 3230
		private IValueStore _secondaryDataSize;

		// Token: 0x04000C9F RID: 3231
		private DelegateBasedHostPerfCounter _secondaryDataSizeCounter;

		// Token: 0x04000CA0 RID: 3232
		private DelegateBasedHostPerfCounter _totalObjectCounter;

		// Token: 0x04000CA1 RID: 3233
		private IValueStore _count;

		// Token: 0x04000CA2 RID: 3234
		private IValueStore _wbCount;

		// Token: 0x04000CA3 RID: 3235
		private int _rCount;

		// Token: 0x04000CA4 RID: 3236
		private int _ncCount;

		// Token: 0x04000CA5 RID: 3237
		private IValueStore _totalReqs;

		// Token: 0x04000CA6 RID: 3238
		private IValueStore _totalRestReqs;

		// Token: 0x04000CA7 RID: 3239
		private DelegateBasedHostPerfCounter _percentageMissCounter;

		// Token: 0x04000CA8 RID: 3240
		private DelegateBasedHostPerfCounter _percentageMissBaseCounter;

		// Token: 0x04000CA9 RID: 3241
		private DelegateBasedHostPerfCounter _totalMissCounter;

		// Token: 0x04000CAA RID: 3242
		private DelegateBasedHostPerfCounter _totalMissPerSecCounter;

		// Token: 0x04000CAB RID: 3243
		private IValueStore _miss;

		// Token: 0x04000CAC RID: 3244
		private DelegateBasedHostPerfCounter _totalGetRequest;

		// Token: 0x04000CAD RID: 3245
		private DelegateBasedHostPerfCounter _totalGetRequestPerSecond;

		// Token: 0x04000CAE RID: 3246
		private IValueStore _getReqs;

		// Token: 0x04000CAF RID: 3247
		private IValueStore _addReqs;

		// Token: 0x04000CB0 RID: 3248
		private IValueStore _upsertReqs;

		// Token: 0x04000CB1 RID: 3249
		private IValueStore _delReqs;

		// Token: 0x04000CB2 RID: 3250
		private IValueStore _evictReqs;

		// Token: 0x04000CB3 RID: 3251
		private IValueStore _rtPendingCount;

		// Token: 0x04000CB4 RID: 3252
		private IValueStore _rtSuccessCount;

		// Token: 0x04000CB5 RID: 3253
		private IValueStore _rtMissedCount;

		// Token: 0x04000CB6 RID: 3254
		private IValueStore _rtErrorCount;

		// Token: 0x04000CB7 RID: 3255
		private bool _perfMonCounterRequired;

		// Token: 0x04000CB8 RID: 3256
		private DelegateBasedHostPerfCounter _rtPendingCounter;

		// Token: 0x04000CB9 RID: 3257
		private DelegateBasedHostPerfCounter _rtSuccessCounter;

		// Token: 0x04000CBA RID: 3258
		private DelegateBasedHostPerfCounter _rtMissedCounter;

		// Token: 0x04000CBB RID: 3259
		private DelegateBasedHostPerfCounter _rtErrorCounter;
	}
}
