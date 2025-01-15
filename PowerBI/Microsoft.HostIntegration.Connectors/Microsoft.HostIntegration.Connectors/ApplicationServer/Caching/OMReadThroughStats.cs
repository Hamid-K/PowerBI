using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000287 RID: 647
	internal sealed class OMReadThroughStats
	{
		// Token: 0x0600172C RID: 5932 RVA: 0x00046780 File Offset: 0x00044980
		internal OMReadThroughStats(string cacheName, OMCacheNodeStats nodeStats)
		{
			this._cacheName = cacheName;
			this._nodeStats = nodeStats;
			this._pendingCount = new SafeValueStore();
			this._successCount = new SafeValueStore();
			this._missedCount = new SafeValueStore();
			this._errorCount = new SafeValueStore();
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000467D0 File Offset: 0x000449D0
		internal void InitializePerfCounters()
		{
			this._pendingCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.RT_PENDING_READS, this._cacheName, new PerfCounterValue(this.GetPendingCount));
			this._successCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.RT_SUCCESSFUL_READS, this._cacheName, new PerfCounterValue(this.GetSuccessCount));
			this._missedCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.RT_MISSED_READS, this._cacheName, new PerfCounterValue(this.GetMissedCount));
			this._errorCounter = new DelegateBasedCachePerfCounter(CachePerfCounter.Name.RT_FAILED_READS, this._cacheName, new PerfCounterValue(this.GetErrorCount));
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x00046859 File Offset: 0x00044A59
		internal long PendingCount
		{
			get
			{
				return this._pendingCount.GetValue();
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00046866 File Offset: 0x00044A66
		internal long SuccessCount
		{
			get
			{
				return this._successCount.GetValue();
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x00046873 File Offset: 0x00044A73
		internal long MissedCount
		{
			get
			{
				return this._missedCount.GetValue();
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x00046880 File Offset: 0x00044A80
		internal long ErrorCount
		{
			get
			{
				return this._errorCount.GetValue();
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00046880 File Offset: 0x00044A80
		private long GetErrorCount()
		{
			return this._errorCount.GetValue();
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00046873 File Offset: 0x00044A73
		private long GetMissedCount()
		{
			return this._missedCount.GetValue();
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00046866 File Offset: 0x00044A66
		private long GetSuccessCount()
		{
			return this._successCount.GetValue();
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00046859 File Offset: 0x00044A59
		private long GetPendingCount()
		{
			return this._pendingCount.GetValue();
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0004688D File Offset: 0x00044A8D
		internal void AddErrorCount(long value)
		{
			this._errorCount.Add(value);
			this._nodeStats.AddRTErrorCount(value);
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x000468A7 File Offset: 0x00044AA7
		internal void AddMissedCount(long value)
		{
			this._missedCount.Add(value);
			this._nodeStats.AddRTMissedCount(value);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x000468C1 File Offset: 0x00044AC1
		internal void AddSuccessCount(long value)
		{
			this._successCount.Add(value);
			this._nodeStats.AddRTSuccessCount(value);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000468DB File Offset: 0x00044ADB
		internal void AddPendingCount(long value)
		{
			this._pendingCount.Add(value);
			this._nodeStats.AddRTPendingCount(value);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000468F5 File Offset: 0x00044AF5
		internal void Delete()
		{
			this._pendingCounter.Delete();
			this._successCounter.Delete();
			this._missedCounter.Delete();
			this._errorCounter.Delete();
		}

		// Token: 0x04000CFD RID: 3325
		private string _cacheName;

		// Token: 0x04000CFE RID: 3326
		private OMCacheNodeStats _nodeStats;

		// Token: 0x04000CFF RID: 3327
		private IValueStore _pendingCount;

		// Token: 0x04000D00 RID: 3328
		private IValueStore _successCount;

		// Token: 0x04000D01 RID: 3329
		private IValueStore _missedCount;

		// Token: 0x04000D02 RID: 3330
		private IValueStore _errorCount;

		// Token: 0x04000D03 RID: 3331
		private DelegateBasedCachePerfCounter _pendingCounter;

		// Token: 0x04000D04 RID: 3332
		private DelegateBasedCachePerfCounter _successCounter;

		// Token: 0x04000D05 RID: 3333
		private DelegateBasedCachePerfCounter _missedCounter;

		// Token: 0x04000D06 RID: 3334
		private DelegateBasedCachePerfCounter _errorCounter;
	}
}
