using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x02000101 RID: 257
	public sealed class SlidingWindowCounter
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x000196E8 File Offset: 0x000178E8
		public SlidingWindowCounter(TimeSpan windowCounterTimeSpan, TimeSpan slidingWindowTimeSpan)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(windowCounterTimeSpan, "windowCounterTimeSpan");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(slidingWindowTimeSpan, "slidingWindowTimeSpan");
			ExtendedDiagnostics.EnsureOperation(windowCounterTimeSpan <= slidingWindowTimeSpan, "windowCounterTimeSpan should not be greater than slidingWindowTimeSpan");
			this.m_windowCounterTimeSpan = windowCounterTimeSpan;
			this.SlidingWindowTimeSpan = slidingWindowTimeSpan;
			this.m_windowCounters = new LinkedList<WindowCounter>();
			this.LatestWindowCounterTime = DateTime.UtcNow;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x00019746 File Offset: 0x00017946
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0001974E File Offset: 0x0001794E
		public int LatestWindowCounterCount { get; private set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x00019757 File Offset: 0x00017957
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x0001975F File Offset: 0x0001795F
		public DateTime LatestWindowCounterTime { get; private set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00019768 File Offset: 0x00017968
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x00019770 File Offset: 0x00017970
		public TimeSpan SlidingWindowTimeSpan { get; private set; }

		// Token: 0x0600072F RID: 1839 RVA: 0x0001977C File Offset: 0x0001797C
		public async Task<int> GetSlidingWindowTotalCount(DateTime? currentTime = null)
		{
			this.InvalidateExpiredWindowCounters(currentTime ?? DateTime.UtcNow);
			return this.m_slidingWindowTotalCount;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x000197CC File Offset: 0x000179CC
		public void AddCount(DateTime currentTime, int count = 1)
		{
			if (!this.m_windowCounters.Any<WindowCounter>())
			{
				this.m_oldestWindowCounterTime = currentTime;
				this.AddNewWindowCounter(currentTime, count);
			}
			else if (currentTime.Ticks > this.LatestWindowCounterTime.Ticks + this.m_windowCounterTimeSpan.Ticks)
			{
				this.AddNewWindowCounter(currentTime, count);
			}
			else
			{
				LinkedListNode<WindowCounter> last = this.m_windowCounters.Last;
				last.Value.Count += count;
				this.LatestWindowCounterCount = last.Value.Count;
			}
			this.m_slidingWindowTotalCount += count;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00019868 File Offset: 0x00017A68
		private void AddNewWindowCounter(DateTime dateTime, int count)
		{
			WindowCounter windowCounter = new WindowCounter(count, dateTime);
			this.m_windowCounters.AddLast(windowCounter);
			this.LatestWindowCounterCount = count;
			this.LatestWindowCounterTime = dateTime;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00019898 File Offset: 0x00017A98
		private void InvalidateExpiredWindowCounters(DateTime currentTime)
		{
			long num = currentTime.Ticks - this.SlidingWindowTimeSpan.Ticks;
			while (this.m_windowCounters.Any<WindowCounter>() && this.m_oldestWindowCounterTime.Ticks < num)
			{
				LinkedListNode<WindowCounter> first = this.m_windowCounters.First;
				this.m_slidingWindowTotalCount -= first.Value.Count;
				this.m_windowCounters.RemoveFirst();
				LinkedListNode<WindowCounter> first2 = this.m_windowCounters.First;
				if (first2 != null)
				{
					this.m_oldestWindowCounterTime = first2.Value.WindowStartTimeStamp;
				}
			}
		}

		// Token: 0x0400027B RID: 635
		private readonly LinkedList<WindowCounter> m_windowCounters;

		// Token: 0x0400027C RID: 636
		private readonly TimeSpan m_windowCounterTimeSpan;

		// Token: 0x0400027D RID: 637
		private DateTime m_oldestWindowCounterTime;

		// Token: 0x0400027E RID: 638
		private int m_slidingWindowTotalCount;
	}
}
