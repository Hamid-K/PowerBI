using System;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000423 RID: 1059
	internal class RecurringTimer : Timer
	{
		// Token: 0x060024E9 RID: 9449 RVA: 0x00070F78 File Offset: 0x0006F178
		public RecurringTimer(WaitCallback callback, object state, TimeSpan expirationInterval)
			: this(callback, state, expirationInterval, NormalPriorityTimerQueue.Singleton)
		{
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x00070F88 File Offset: 0x0006F188
		public RecurringTimer(WaitCallback callback, object state, TimeSpan expirationInterval, TimerQueue timerQueue)
			: base(callback, state, timerQueue)
		{
			this.m_expirationInterval = expirationInterval;
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x00070F9B File Offset: 0x0006F19B
		// (set) Token: 0x060024EC RID: 9452 RVA: 0x00070FA3 File Offset: 0x0006F1A3
		public TimeSpan ExpirationInterval
		{
			get
			{
				return this.m_expirationInterval;
			}
			set
			{
				if (base.IsEnqueued)
				{
					throw new InvalidOperationException("Expiration time cannot be changed when the timer is enqueued");
				}
				this.m_expirationInterval = value;
			}
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x00070FBF File Offset: 0x0006F1BF
		public bool Enqueue()
		{
			return base.Enqueue(FileTime.FromTimeSpan(this.m_expirationInterval));
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x00070FD2 File Offset: 0x0006F1D2
		protected override void OnTimerElapsed()
		{
			this.Enqueue();
			base.OnTimerElapsed();
		}

		// Token: 0x04001685 RID: 5765
		private TimeSpan m_expirationInterval;
	}
}
