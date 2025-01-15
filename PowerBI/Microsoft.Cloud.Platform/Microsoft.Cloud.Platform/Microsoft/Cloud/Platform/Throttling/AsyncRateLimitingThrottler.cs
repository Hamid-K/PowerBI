using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x02000100 RID: 256
	public sealed class AsyncRateLimitingThrottler<TKey>
	{
		// Token: 0x0600071E RID: 1822 RVA: 0x0001906C File Offset: 0x0001726C
		public AsyncRateLimitingThrottler(int maxNumKeys, int eventsPerWindowPerKey, TimeSpan throttlingWindowLength, ThrottlingBehavior behavior)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(maxNumKeys, "maxNumKeys");
			ExtendedDiagnostics.EnsureArgumentIsPositive(eventsPerWindowPerKey, "eventsPerWindowPerKey");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(throttlingWindowLength, "throttlingWindowLength");
			ExtendedDiagnostics.EnsureOperation(throttlingWindowLength >= TimeSpan.FromSeconds(1.0), "Throttling windows must be at least 1 second in length.");
			this.m_maxNumKeys = maxNumKeys;
			this.m_eventsPerWindowPerKey = eventsPerWindowPerKey;
			this.m_throttlingWindowLength = throttlingWindowLength;
			this.m_behavior = behavior;
			this.m_keyThrottlingContexts = new LRUCache<TKey, AsyncRateLimitingThrottler<TKey>.ThrottlingContext>(this.m_maxNumKeys);
			this.AdvanceWindowsLoop().DoNotWait();
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00019102 File Offset: 0x00017302
		public AsyncRateLimitingThrottler(int maxNumKeys, int eventsPerWindowPerKey, TimeSpan throttlingWindowLength, ThrottlingBehavior behavior, MaxKeysExceededBehvaior maxKeysBehavior)
			: this(maxNumKeys, eventsPerWindowPerKey, throttlingWindowLength, behavior)
		{
			this.m_maxKeysBehavior = maxKeysBehavior;
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00019117 File Offset: 0x00017317
		public Task<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput> TryRecordEventAndThrottleAsync(TKey key)
		{
			return this.TryRecordEventAndThrottleAsync(key, DateTime.UtcNow);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00019125 File Offset: 0x00017325
		public Task RecordEventAndThrottleAsync(TKey key)
		{
			return this.RecordEventAndThrottleAsync(key, DateTime.UtcNow);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00019134 File Offset: 0x00017334
		public async Task RecordEventAndThrottleAsync(TKey key, DateTime eventTimestamp)
		{
			AsyncRateLimitingThrottler<TKey>.ThrottlerOutput throttlerOutput = await this.TryRecordEventAndThrottleAsync(key, eventTimestamp);
			AsyncRateLimitingThrottler<TKey>.ThrottlerResult throttlerResult = throttlerOutput.ThrottlerResult;
			if (throttlerResult != AsyncRateLimitingThrottler<TKey>.ThrottlerResult.MayExecute)
			{
				int retryAfterSeconds = throttlerOutput.RetryAfterSeconds;
				if (throttlerResult == AsyncRateLimitingThrottler<TKey>.ThrottlerResult.Rejected)
				{
					throw new OperationThrottledException(key.ToString(), retryAfterSeconds, null);
				}
				if (throttlerResult == AsyncRateLimitingThrottler<TKey>.ThrottlerResult.ThrottlerOverflow)
				{
					throw new ThrottlingOverflowException(key.ToString(), retryAfterSeconds, null);
				}
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001918C File Offset: 0x0001738C
		public Task<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput> TryRecordEventAndThrottleAsync(TKey key, DateTime eventTimestamp)
		{
			AsyncRateLimitingThrottler<TKey>.ThrottlingContext throttlingContext = null;
			Task<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput> task;
			try
			{
				object syncRoot = this.m_syncRoot;
				lock (syncRoot)
				{
					AsyncRateLimitingThrottler<TKey>.ThrottlingContext throttlingContext2;
					if (!this.m_keyThrottlingContexts.TryGet(key, out throttlingContext2))
					{
						throttlingContext2 = new AsyncRateLimitingThrottler<TKey>.ThrottlingContext
						{
							Window = new ThrottlingWindow
							{
								NumberOfEvents = 1L,
								EndTimeInTicks = eventTimestamp.Ticks + this.m_throttlingWindowLength.Ticks
							},
							ThrottledRequests = ((this.m_behavior == ThrottlingBehavior.QueueRequests) ? new Queue<TaskCompletionSource<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput>>(1000) : null)
						};
						throttlingContext = this.m_keyThrottlingContexts.Set(key, throttlingContext2);
						task = AsyncRateLimitingThrottler<TKey>.s_completedOutput;
					}
					else if (throttlingContext2.Window.NumberOfEvents < (long)this.m_eventsPerWindowPerKey)
					{
						ThrottlingWindow window = throttlingContext2.Window;
						long numberOfEvents = window.NumberOfEvents;
						window.NumberOfEvents = numberOfEvents + 1L;
						task = AsyncRateLimitingThrottler<TKey>.s_completedOutput;
					}
					else
					{
						DateTime dateTime = new DateTime(throttlingContext2.Window.EndTimeInTicks, DateTimeKind.Utc);
						int num = Math.Max(1, (int)(dateTime - eventTimestamp).TotalSeconds);
						if (this.m_behavior == ThrottlingBehavior.FailRequests)
						{
							TraceSourceBase<ThrottlerTrace>.Tracer.TraceWarning("There have been {0} requests for key {1}. This exceeds the throttling limit and further requests will fail until the window expires at {2}.", new object[]
							{
								throttlingContext2.Window.NumberOfEvents,
								key,
								dateTime
							});
							task = Task.FromResult<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput>(new AsyncRateLimitingThrottler<TKey>.ThrottlerOutput(AsyncRateLimitingThrottler<TKey>.ThrottlerResult.Rejected, num));
						}
						else if (throttlingContext2.ThrottledRequests.Count >= 10000)
						{
							TraceSourceBase<ThrottlerTrace>.Tracer.TraceWarning("There have been {0} requests for key {1}. This exceeds the throttling limit and the queue for further requests has overflowed its limit of {2}.", new object[]
							{
								throttlingContext2.Window.NumberOfEvents,
								key,
								10000
							});
							task = Task.FromResult<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput>(new AsyncRateLimitingThrottler<TKey>.ThrottlerOutput(AsyncRateLimitingThrottler<TKey>.ThrottlerResult.ThrottlerOverflow, num));
						}
						else
						{
							TraceSourceBase<ThrottlerTrace>.Tracer.TraceWarning("There have been {0} requests for key {1}. This exceeds the throttling limit and further requests will be queued until the window expires at {2}.", new object[]
							{
								throttlingContext2.Window.NumberOfEvents,
								key,
								dateTime
							});
							TaskCompletionSource<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput> taskCompletionSource = new TaskCompletionSource<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput>();
							throttlingContext2.ThrottledRequests.Enqueue(taskCompletionSource);
							throttlingContext = this.m_keyThrottlingContexts.Set(key, throttlingContext2);
							task = taskCompletionSource.Task;
						}
					}
				}
			}
			finally
			{
				if (throttlingContext != null && this.m_behavior == ThrottlingBehavior.QueueRequests)
				{
					throttlingContext.ReleaseQueuedRequests(this.m_maxKeysBehavior).DoNotWait();
				}
			}
			return task;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00019410 File Offset: 0x00017610
		private async Task AdvanceWindowsLoop()
		{
			for (;;)
			{
				await Task.Delay(this.AdvanceWindows());
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00019458 File Offset: 0x00017658
		private TimeSpan AdvanceWindows()
		{
			List<TaskCompletionSource<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput>> list = new List<TaskCompletionSource<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput>>();
			object syncRoot = this.m_syncRoot;
			TimeSpan timeSpan;
			lock (syncRoot)
			{
				long ticks = DateTime.UtcNow.Ticks;
				long num = ticks + this.m_throttlingWindowLength.Ticks;
				List<TKey> list2 = new List<TKey>();
				foreach (LRUCache<TKey, AsyncRateLimitingThrottler<TKey>.ThrottlingContext>.KeyValue keyValue in this.m_keyThrottlingContexts)
				{
					AsyncRateLimitingThrottler<TKey>.ThrottlingContext value = keyValue.Value;
					if (value.Window.EndTimeInTicks < ticks)
					{
						value.Window.NumberOfEvents = 0L;
						value.Window.EndTimeInTicks = ticks + this.m_throttlingWindowLength.Ticks;
						if (this.m_behavior == ThrottlingBehavior.QueueRequests)
						{
							int count = value.ThrottledRequests.Count;
							int num2 = 0;
							while (num2 < count && num2 < this.m_eventsPerWindowPerKey)
							{
								TaskCompletionSource<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput> taskCompletionSource = value.ThrottledRequests.Dequeue();
								ThrottlingWindow window = value.Window;
								long numberOfEvents = window.NumberOfEvents;
								window.NumberOfEvents = numberOfEvents + 1L;
								list.Add(taskCompletionSource);
								num2++;
							}
							if (count == 0)
							{
								list2.Add(keyValue.Key);
							}
						}
						else
						{
							list2.Add(keyValue.Key);
						}
					}
					num = Math.Min(num, value.Window.EndTimeInTicks);
				}
				foreach (TKey tkey in list2)
				{
					AsyncRateLimitingThrottler<TKey>.ThrottlingContext throttlingContext;
					this.m_keyThrottlingContexts.TryRemove(tkey, out throttlingContext);
				}
				timeSpan = TimeSpan.FromTicks(Math.Max(0L, num - DateTime.UtcNow.Ticks));
			}
			this.ReleaseTasks(list).DoNotWait();
			return timeSpan;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001967C File Offset: 0x0001787C
		private async Task ReleaseTasks(List<TaskCompletionSource<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput>> tasksToRelease)
		{
			await Task.Yield();
			foreach (TaskCompletionSource<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput> taskCompletionSource in tasksToRelease)
			{
				taskCompletionSource.SetResult(AsyncRateLimitingThrottler<TKey>.s_mayExecuteOutput);
			}
		}

		// Token: 0x04000270 RID: 624
		private readonly int m_maxNumKeys;

		// Token: 0x04000271 RID: 625
		private readonly int m_eventsPerWindowPerKey;

		// Token: 0x04000272 RID: 626
		private readonly TimeSpan m_throttlingWindowLength;

		// Token: 0x04000273 RID: 627
		private readonly ThrottlingBehavior m_behavior;

		// Token: 0x04000274 RID: 628
		private readonly LRUCache<TKey, AsyncRateLimitingThrottler<TKey>.ThrottlingContext> m_keyThrottlingContexts;

		// Token: 0x04000275 RID: 629
		private readonly object m_syncRoot = new object();

		// Token: 0x04000276 RID: 630
		private readonly MaxKeysExceededBehvaior m_maxKeysBehavior;

		// Token: 0x04000277 RID: 631
		private const int c_maxNumberOfQueuedRequestsPerKey = 10000;

		// Token: 0x04000278 RID: 632
		private static readonly AsyncRateLimitingThrottler<TKey>.ThrottlerOutput s_mayExecuteOutput = new AsyncRateLimitingThrottler<TKey>.ThrottlerOutput(AsyncRateLimitingThrottler<TKey>.ThrottlerResult.MayExecute);

		// Token: 0x04000279 RID: 633
		private static readonly AsyncRateLimitingThrottler<TKey>.ThrottlerOutput s_throttlerOverflowOutput = new AsyncRateLimitingThrottler<TKey>.ThrottlerOutput(AsyncRateLimitingThrottler<TKey>.ThrottlerResult.ThrottlerOverflow);

		// Token: 0x0400027A RID: 634
		private static readonly Task<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput> s_completedOutput = Task.FromResult<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput>(AsyncRateLimitingThrottler<TKey>.s_mayExecuteOutput);

		// Token: 0x020005EB RID: 1515
		private sealed class ThrottlingContext
		{
			// Token: 0x17000706 RID: 1798
			// (get) Token: 0x06002BEC RID: 11244 RVA: 0x0009B5BD File Offset: 0x000997BD
			// (set) Token: 0x06002BED RID: 11245 RVA: 0x0009B5C5 File Offset: 0x000997C5
			public ThrottlingWindow Window { get; set; }

			// Token: 0x17000707 RID: 1799
			// (get) Token: 0x06002BEE RID: 11246 RVA: 0x0009B5CE File Offset: 0x000997CE
			// (set) Token: 0x06002BEF RID: 11247 RVA: 0x0009B5D6 File Offset: 0x000997D6
			public Queue<TaskCompletionSource<AsyncRateLimitingThrottler<TKey>.ThrottlerOutput>> ThrottledRequests { get; set; }

			// Token: 0x06002BF0 RID: 11248 RVA: 0x0009B5E0 File Offset: 0x000997E0
			internal async Task ReleaseQueuedRequests(MaxKeysExceededBehvaior behavior)
			{
				await Task.Yield();
				if (behavior == MaxKeysExceededBehvaior.AllowRequestsThrough)
				{
					while (this.ThrottledRequests.Count > 0)
					{
						this.ThrottledRequests.Dequeue().SetResult(AsyncRateLimitingThrottler<TKey>.s_mayExecuteOutput);
					}
				}
				else
				{
					while (this.ThrottledRequests.Count > 0)
					{
						this.ThrottledRequests.Dequeue().SetResult(AsyncRateLimitingThrottler<TKey>.s_throttlerOverflowOutput);
					}
				}
			}
		}

		// Token: 0x020005EC RID: 1516
		public struct ThrottlerOutput
		{
			// Token: 0x06002BF2 RID: 11250 RVA: 0x0009B62D File Offset: 0x0009982D
			public ThrottlerOutput(AsyncRateLimitingThrottler<TKey>.ThrottlerResult throttlerResult)
			{
				this.ThrottlerResult = throttlerResult;
				this.RetryAfterSeconds = 0;
			}

			// Token: 0x06002BF3 RID: 11251 RVA: 0x0009B63D File Offset: 0x0009983D
			public ThrottlerOutput(AsyncRateLimitingThrottler<TKey>.ThrottlerResult throttlerResult, int retryAfterSeconds)
			{
				ExtendedDiagnostics.EnsureOperation(retryAfterSeconds >= 1, "Retry after seconds must be at least 1 second in length.");
				this.ThrottlerResult = throttlerResult;
				this.RetryAfterSeconds = retryAfterSeconds;
			}

			// Token: 0x17000708 RID: 1800
			// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x0009B65E File Offset: 0x0009985E
			// (set) Token: 0x06002BF5 RID: 11253 RVA: 0x0009B666 File Offset: 0x00099866
			public AsyncRateLimitingThrottler<TKey>.ThrottlerResult ThrottlerResult { get; set; }

			// Token: 0x17000709 RID: 1801
			// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x0009B66F File Offset: 0x0009986F
			// (set) Token: 0x06002BF7 RID: 11255 RVA: 0x0009B677 File Offset: 0x00099877
			public int RetryAfterSeconds { get; set; }
		}

		// Token: 0x020005ED RID: 1517
		public enum ThrottlerResult
		{
			// Token: 0x04001019 RID: 4121
			MayExecute,
			// Token: 0x0400101A RID: 4122
			Rejected,
			// Token: 0x0400101B RID: 4123
			ThrottlerOverflow
		}
	}
}
