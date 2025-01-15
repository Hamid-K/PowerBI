using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002BC RID: 700
	public class OperationsRateThrottlingPolicy : IThrottlingPolicy
	{
		// Token: 0x060012D8 RID: 4824 RVA: 0x000415DC File Offset: 0x0003F7DC
		public OperationsRateThrottlingPolicy(TimeSpan slotDuration, int slotMaxConcurrentOperations)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(slotMaxConcurrentOperations, "slotMaxConcurrentOperations");
			this.m_limiter = new EventRateLimiter(slotDuration, slotMaxConcurrentOperations);
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x000415FC File Offset: 0x0003F7FC
		public int CurrentlyRunningOperations
		{
			get
			{
				return this.m_limiter.TimeslotEventCount;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060012DA RID: 4826 RVA: 0x00041609 File Offset: 0x0003F809
		public int MaxConcurrentOperations
		{
			get
			{
				return this.m_limiter.MaxSlotEvents;
			}
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00041618 File Offset: 0x0003F818
		public TimeToRun GetTimeToRun(DateTime now)
		{
			DateTime eventProcessingTime = this.m_limiter.GetEventProcessingTime(now);
			if (now == eventProcessingTime)
			{
				return new TimeToRun(true, TimeToRun.Infinite);
			}
			return new TimeToRun(false, eventProcessingTime);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0004164E File Offset: 0x0003F84E
		public void OnOperationStarted(DateTime now)
		{
			this.m_limiter.UpdateTimeslotState(now);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00009B3B File Offset: 0x00007D3B
		public void OnOperationCompleted(DateTime now)
		{
		}

		// Token: 0x040006FD RID: 1789
		private EventRateLimiter m_limiter;
	}
}
