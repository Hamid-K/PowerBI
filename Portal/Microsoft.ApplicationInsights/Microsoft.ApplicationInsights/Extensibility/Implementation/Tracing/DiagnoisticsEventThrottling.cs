using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x02000094 RID: 148
	internal class DiagnoisticsEventThrottling : IDiagnoisticsEventThrottling
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x0001432B File Offset: 0x0001252B
		internal DiagnoisticsEventThrottling(int throttleAfterCount)
		{
			if (!throttleAfterCount.IsInRangeThrottleAfterCount())
			{
				throw new ArgumentOutOfRangeException("throttleAfterCount");
			}
			this.throttleAfterCount = throttleAfterCount;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00014363 File Offset: 0x00012563
		internal int ThrottleAfterCount
		{
			get
			{
				return this.throttleAfterCount;
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001436C File Offset: 0x0001256C
		public bool ThrottleEvent(int eventId, long keywords, out bool justExceededThreshold)
		{
			if (!DiagnoisticsEventThrottling.IsExcludedFromThrottling(keywords))
			{
				DiagnoisticsEventCounters diagnoisticsEventCounters = this.InternalGetEventCounter(eventId);
				justExceededThreshold = this.ThrottleAfterCount == diagnoisticsEventCounters.Increment() - 1;
				return this.ThrottleAfterCount < diagnoisticsEventCounters.ExecCount;
			}
			justExceededThreshold = false;
			return false;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000143AE File Offset: 0x000125AE
		public IDictionary<int, DiagnoisticsEventCounters> CollectSnapshot()
		{
			IDictionary<int, DiagnoisticsEventCounters> dictionary = this.counters;
			this.syncRoot.ExecuteSpinWaitLock(delegate
			{
				this.counters = new Dictionary<int, DiagnoisticsEventCounters>();
			});
			return dictionary;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000143CD File Offset: 0x000125CD
		private static bool IsExcludedFromThrottling(long keywords)
		{
			return (keywords & 2L) != 0L;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000143D8 File Offset: 0x000125D8
		private DiagnoisticsEventCounters InternalGetEventCounter(int eventId)
		{
			DiagnoisticsEventCounters result = null;
			this.syncRoot.ExecuteSpinWaitLock(delegate
			{
				if (!this.counters.TryGetValue(eventId, out result))
				{
					result = new DiagnoisticsEventCounters(0);
					this.counters.Add(eventId, result);
				}
			});
			return result;
		}

		// Token: 0x040001D3 RID: 467
		private readonly int throttleAfterCount;

		// Token: 0x040001D4 RID: 468
		private readonly object syncRoot = new object();

		// Token: 0x040001D5 RID: 469
		private Dictionary<int, DiagnoisticsEventCounters> counters = new Dictionary<int, DiagnoisticsEventCounters>();
	}
}
