using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x02000107 RID: 263
	public sealed class RateLimitingThrottler<TKey>
	{
		// Token: 0x0600074A RID: 1866 RVA: 0x00019DB0 File Offset: 0x00017FB0
		public RateLimitingThrottler(int maxNumKeys, int eventsPerTimeslotPerKey, TimeSpan throttlingWindowLength)
		{
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(maxNumKeys, "maxNumKeys");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(eventsPerTimeslotPerKey, "eventsPerTimeslotPerKey");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(throttlingWindowLength, "throttlingWindowLength");
			this.m_maxNumKeys = maxNumKeys;
			this.m_eventsPerTimeslotPerKey = eventsPerTimeslotPerKey;
			this.m_throttlingWindowLength = throttlingWindowLength;
			this.m_keysToThrottlingWindows = new LRUCache<TKey, ThrottlingWindow>(this.m_maxNumKeys);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00019E15 File Offset: 0x00018015
		public bool RecordEventAndCheckIfThrottled(TKey key, uint numOfEvents = 1U)
		{
			return this.RecordEventAndCheckIfThrottled(key, DateTime.UtcNow, numOfEvents);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00019E24 File Offset: 0x00018024
		public bool RecordEventAndCheckIfThrottled(TKey key, DateTime eventTimestamp, uint numOfEvents = 1U)
		{
			if ((ulong)numOfEvents > (ulong)((long)this.m_eventsPerTimeslotPerKey))
			{
				return true;
			}
			object syncRoot = this.m_syncRoot;
			bool flag2;
			lock (syncRoot)
			{
				ThrottlingWindow throttlingWindow;
				if (this.m_keysToThrottlingWindows.TryUpdateValue(key, RateLimitingThrottler<TKey>.GetThrottlingWindowHitCountIncrementDelegate(numOfEvents), out throttlingWindow))
				{
					if (eventTimestamp.Ticks >= throttlingWindow.EndTimeInTicks)
					{
						ThrottlingWindow throttlingWindow2 = new ThrottlingWindow
						{
							NumberOfEvents = (long)((ulong)numOfEvents),
							EndTimeInTicks = eventTimestamp.Ticks + this.m_throttlingWindowLength.Ticks
						};
						this.m_keysToThrottlingWindows.Set(key, throttlingWindow2);
						flag2 = false;
					}
					else
					{
						bool flag3 = throttlingWindow.NumberOfEvents > (long)this.m_eventsPerTimeslotPerKey;
						if (flag3)
						{
							throttlingWindow.NumberOfEvents -= (long)((ulong)numOfEvents);
						}
						flag2 = flag3;
					}
				}
				else
				{
					ThrottlingWindow throttlingWindow3 = new ThrottlingWindow
					{
						NumberOfEvents = (long)((ulong)numOfEvents),
						EndTimeInTicks = eventTimestamp.Ticks + this.m_throttlingWindowLength.Ticks
					};
					this.m_keysToThrottlingWindows.Set(key, throttlingWindow3);
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00019F34 File Offset: 0x00018134
		private static Func<ThrottlingWindow, ThrottlingWindow> GetThrottlingWindowHitCountIncrementDelegate(uint numOfEvents)
		{
			return delegate(ThrottlingWindow window)
			{
				window.NumberOfEvents += (long)((ulong)numOfEvents);
				return window;
			};
		}

		// Token: 0x04000299 RID: 665
		private readonly int m_maxNumKeys;

		// Token: 0x0400029A RID: 666
		private readonly int m_eventsPerTimeslotPerKey;

		// Token: 0x0400029B RID: 667
		private readonly TimeSpan m_throttlingWindowLength;

		// Token: 0x0400029C RID: 668
		private readonly LRUCache<TKey, ThrottlingWindow> m_keysToThrottlingWindows;

		// Token: 0x0400029D RID: 669
		private readonly object m_syncRoot = new object();
	}
}
