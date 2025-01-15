using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002CD RID: 717
	public class NonOverlappingStrictPeriodicTimerPolicy : PeriodicTimerPolicy
	{
		// Token: 0x06001331 RID: 4913 RVA: 0x00042647 File Offset: 0x00040847
		public NonOverlappingStrictPeriodicTimerPolicy(int period)
		{
			base.EnsureValidPeriod(period);
			this.m_lock = new object();
			this.m_period = period;
			this.m_concurrentCallbacks = 0;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0004266F File Offset: 0x0004086F
		internal override void SetInitialSchedule(Timer timer)
		{
			timer.Change(this.m_period, this.m_period);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00042684 File Offset: 0x00040884
		internal override bool OnPeriodicCallback(string identity, Timer timer, Action callback)
		{
			object obj = this.m_lock;
			lock (obj)
			{
				this.m_concurrentCallbacks++;
				if (this.m_concurrentCallbacks > 1)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Periodic timer '{0}' ticked, but callback will not be invoked as there are already {1} concurrent callbacks", new object[]
					{
						identity,
						this.m_concurrentCallbacks - 1
					});
					return false;
				}
			}
			callback();
			obj = this.m_lock;
			lock (obj)
			{
				int concurrentCallbacks = this.m_concurrentCallbacks;
				this.m_concurrentCallbacks = 0;
				if (concurrentCallbacks > 1)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Periodic timer '{0}' skipped {1} callbacks; adding a single additional callback to compensate", new object[]
					{
						identity,
						concurrentCallbacks - 1
					});
					int num = 1;
					timer.Change(num, this.m_period);
				}
			}
			return true;
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00042780 File Offset: 0x00040980
		internal override void UpdatePeriod(int period, Timer timer, bool tickNow)
		{
			base.EnsureValidPeriod(period);
			object @lock = this.m_lock;
			lock (@lock)
			{
				this.m_period = period;
				timer.Change(tickNow ? 1 : period, period);
			}
		}

		// Token: 0x0400072D RID: 1837
		private object m_lock;

		// Token: 0x0400072E RID: 1838
		private int m_period;

		// Token: 0x0400072F RID: 1839
		private int m_concurrentCallbacks;
	}
}
