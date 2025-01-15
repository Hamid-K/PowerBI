using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x02000105 RID: 261
	public class RateLimitingSlidingWindowThrottler<TKey>
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x00019BFC File Offset: 0x00017DFC
		public RateLimitingSlidingWindowThrottler(int maxNumKeys, int eventsPerSlidingWindowPerKey, TimeSpan slidingWindowCounterTimeSpan, TimeSpan slidingWindowTotalTimeSpan, TimeSpan? throttlerWaitTimeOutTimeSpan = null)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(maxNumKeys, "maxNumKeys");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(eventsPerSlidingWindowPerKey, "eventsPerSlidingWindowPerKey");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(slidingWindowCounterTimeSpan, "slidingWindowCounterTimeSpan");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(slidingWindowTotalTimeSpan, "slidingWindowTotalTimeSpan");
			ExtendedDiagnostics.EnsureOperation(slidingWindowCounterTimeSpan <= slidingWindowTotalTimeSpan, "slidingWindowCounterTimeSpan should not be greater than slidingWindowTimeSpan");
			this.m_maxNumKeys = maxNumKeys;
			this.m_eventsPerSlidingWindowPerKey = eventsPerSlidingWindowPerKey;
			this.m_windowCounterTimeSpan = slidingWindowCounterTimeSpan;
			this.m_slidingWindowTimeSpan = slidingWindowTotalTimeSpan;
			if (throttlerWaitTimeOutTimeSpan != null)
			{
				ExtendedDiagnostics.EnsureArgumentIsNotNegative(throttlerWaitTimeOutTimeSpan.Value, "throttlerWaitTimeOutTimeSpan");
				if (throttlerWaitTimeOutTimeSpan.Value.TotalMilliseconds < 30000.0)
				{
					this.m_throttlerWaitTimeOutMilliSeconds = new int?((int)throttlerWaitTimeOutTimeSpan.Value.TotalMilliseconds);
				}
			}
			this.m_keysToSlidingWindowCounters = new LRUCache<TKey, SlidingWindowCounter>(this.m_maxNumKeys);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x00019CD8 File Offset: 0x00017ED8
		public Task<ThrottlerResult> RecordEventAndCheckIfThrottled(TKey key)
		{
			return this.RecordEventAndCheckIfThrottled(key, DateTime.UtcNow);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00019CE8 File Offset: 0x00017EE8
		public async Task<ThrottlerResult> RecordEventAndCheckIfThrottled(TKey key, DateTime eventTimestamp)
		{
			ThrottlerResult throttlerResult;
			try
			{
				TaskAwaiter<bool> taskAwaiter = this.m_rateLimitingSlidingWindowThrottlerSemaphore.WaitAsync(this.m_throttlerWaitTimeOutMilliSeconds ?? 30000).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<bool> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<bool>);
				}
				if (taskAwaiter.GetResult())
				{
					SlidingWindowCounter slidingWindowCounter;
					if (this.m_keysToSlidingWindowCounters.TryGet(key, out slidingWindowCounter))
					{
						TaskAwaiter<int> taskAwaiter3 = slidingWindowCounter.GetSlidingWindowTotalCount(new DateTime?(eventTimestamp)).GetAwaiter();
						if (!taskAwaiter3.IsCompleted)
						{
							await taskAwaiter3;
							TaskAwaiter<int> taskAwaiter4;
							taskAwaiter3 = taskAwaiter4;
							taskAwaiter4 = default(TaskAwaiter<int>);
						}
						if (taskAwaiter3.GetResult() + 1 > this.m_eventsPerSlidingWindowPerKey)
						{
							TimeSpan slidingWindowTimeSpan = this.m_slidingWindowTimeSpan;
							TraceSourceBase<ThrottlerTrace>.Tracer.TraceWarning(string.Format("Received more than {0} events for the key: {1}. Throttling the current event.", this.m_eventsPerSlidingWindowPerKey, key));
							return new ThrottlerResult(true, slidingWindowTimeSpan);
						}
					}
					else
					{
						slidingWindowCounter = new SlidingWindowCounter(this.m_windowCounterTimeSpan, this.m_slidingWindowTimeSpan);
					}
					slidingWindowCounter.AddCount(eventTimestamp, 1);
					this.m_keysToSlidingWindowCounters.Set(key, slidingWindowCounter);
					throttlerResult = ThrottlerResult.NotThrottled;
				}
				else
				{
					TraceSourceBase<ThrottlerTrace>.Tracer.TraceWarning("RecordEventAndCheckIfThrottled operation timed out while waiting for a lock. Failed to record the current event or check the event for throttling.");
					throttlerResult = ThrottlerResult.TimedOut;
				}
			}
			finally
			{
				this.m_rateLimitingSlidingWindowThrottlerSemaphore.Release();
			}
			return throttlerResult;
		}

		// Token: 0x0400028D RID: 653
		private const int c_throttlerWaitDefaultTimeOutMilliSeconds = 30000;

		// Token: 0x0400028E RID: 654
		private readonly LRUCache<TKey, SlidingWindowCounter> m_keysToSlidingWindowCounters;

		// Token: 0x0400028F RID: 655
		private readonly int m_maxNumKeys;

		// Token: 0x04000290 RID: 656
		private readonly int m_eventsPerSlidingWindowPerKey;

		// Token: 0x04000291 RID: 657
		private readonly TimeSpan m_slidingWindowTimeSpan;

		// Token: 0x04000292 RID: 658
		private readonly TimeSpan m_windowCounterTimeSpan;

		// Token: 0x04000293 RID: 659
		private readonly int? m_throttlerWaitTimeOutMilliSeconds;

		// Token: 0x04000294 RID: 660
		private readonly SemaphoreSlim m_rateLimitingSlidingWindowThrottlerSemaphore = new SemaphoreSlim(1, 1);
	}
}
