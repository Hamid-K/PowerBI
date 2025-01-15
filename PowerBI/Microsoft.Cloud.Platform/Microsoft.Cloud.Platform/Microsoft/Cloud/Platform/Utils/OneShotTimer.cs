using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002CB RID: 715
	public sealed class OneShotTimer : TimerBase
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x000425ED File Offset: 0x000407ED
		internal OneShotTimer(string identity, IWorkTicketFactory workTicketFactory, TimerCallback timerCallback, object state, TimerCreationFlags timerCreationFlags)
			: base(identity, workTicketFactory, timerCallback, state, timerCreationFlags)
		{
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x000425FC File Offset: 0x000407FC
		public new bool ScheduleTimer(int dueTime)
		{
			return base.ScheduleTimer(dueTime);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00042608 File Offset: 0x00040808
		public bool ScheduleTimer(TimeSpan dueTime)
		{
			int num = TimerBase.ConvertTimeSpanToInt(dueTime);
			return base.ScheduleTimer(num);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00042623 File Offset: 0x00040823
		protected override void OnCallback(PendingCallbackReason reason)
		{
			if (reason == PendingCallbackReason.TimeExpired)
			{
				base.InvokeTimerCallback();
			}
		}
	}
}
