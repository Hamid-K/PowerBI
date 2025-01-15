using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002CE RID: 718
	public class NonOverlappingLoosePeriodicTimerPolicy : PeriodicTimerPolicy
	{
		// Token: 0x06001335 RID: 4917 RVA: 0x000427D8 File Offset: 0x000409D8
		public NonOverlappingLoosePeriodicTimerPolicy(int period)
		{
			base.EnsureValidPeriod(period);
			this.m_lock = new object();
			this.m_period = period;
			this.m_concurrentCallbacks = 0;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00042800 File Offset: 0x00040A00
		internal override void SetInitialSchedule(Timer timer)
		{
			timer.Change(this.m_period, this.m_period);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00042818 File Offset: 0x00040A18
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
				this.m_concurrentCallbacks = 0;
			}
			return true;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000428D0 File Offset: 0x00040AD0
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

		// Token: 0x04000730 RID: 1840
		private object m_lock;

		// Token: 0x04000731 RID: 1841
		private int m_period;

		// Token: 0x04000732 RID: 1842
		private int m_concurrentCallbacks;
	}
}
