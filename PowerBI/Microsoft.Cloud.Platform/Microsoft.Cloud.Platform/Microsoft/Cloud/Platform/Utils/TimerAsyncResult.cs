using System;
using System.Threading;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000194 RID: 404
	internal class TimerAsyncResult : AsyncResult
	{
		// Token: 0x06000A61 RID: 2657 RVA: 0x00023D63 File Offset: 0x00021F63
		public TimerAsyncResult(int dueTime, TimerFactory timerFactory, AsyncCallback callback, object context)
			: base(callback, context)
		{
			if (dueTime == 0)
			{
				base.SignalCompletionInternal(true);
				return;
			}
			timerFactory.ScheduleOneShotTimer("AsyncTimer", dueTime, new TimerCallback(this.OnTimerCallback), null);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x00023D93 File Offset: 0x00021F93
		public void End()
		{
			base.EndInternal();
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00023D9B File Offset: 0x00021F9B
		private void OnTimerCallback(object state)
		{
			base.SignalCompletionInternal(false);
		}
	}
}
