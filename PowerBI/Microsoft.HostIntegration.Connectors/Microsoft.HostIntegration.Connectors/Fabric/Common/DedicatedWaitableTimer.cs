using System;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000437 RID: 1079
	internal class DedicatedWaitableTimer : WaitableTimer
	{
		// Token: 0x0600259A RID: 9626 RVA: 0x000733E3 File Offset: 0x000715E3
		public DedicatedWaitableTimer()
			: this(Priority.NormalThreadPriority)
		{
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x000733F0 File Offset: 0x000715F0
		public DedicatedWaitableTimer(ThreadPriority priority)
		{
			this.m_priority = priority;
			this.m_timerThreadStart = new ThreadStart(this.TimerThreadStart);
			this.m_timerThreadStatus = 0;
			this.m_terminationTime = FileTime.Zero;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x00073424 File Offset: 0x00071624
		private void TimerThreadStart()
		{
			for (;;)
			{
				this.WaitOne();
				int num = this.m_timerThreadStatus;
				if (num == 1)
				{
					base.WaitCallback(null, false);
				}
				else if (num == 2)
				{
					num = Interlocked.CompareExchange(ref this.m_timerThreadStatus, 0, 2);
					if (num == 2)
					{
						break;
					}
					ReleaseAssert.IsTrue(num == 1);
				}
			}
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x00073474 File Offset: 0x00071674
		public override void SetTimer(ulong dueTime)
		{
			base.SetTimer(dueTime);
			int num = this.m_timerThreadStatus;
			if (num == 2)
			{
				num = Interlocked.CompareExchange(ref this.m_timerThreadStatus, 1, 2);
				ReleaseAssert.IsTrue(num == 2 || num == 0);
			}
			if (num == 0)
			{
				this.m_timerThreadStatus = 1;
				new Thread(this.m_timerThreadStart)
				{
					IsBackground = true,
					Priority = this.m_priority
				}.Start();
			}
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000734E0 File Offset: 0x000716E0
		public override void CancelTimer()
		{
			this.m_terminationTime = FileTime.FromTimeSpan(DedicatedWaitableTimer.TimerThreadIdleTime);
			this.m_timerThreadStatus = 2;
			base.SetTimer(this.m_terminationTime.Ticks);
		}

		// Token: 0x040016B6 RID: 5814
		private static TimeSpan TimerThreadIdleTime = new TimeSpan(0, 2, 0);

		// Token: 0x040016B7 RID: 5815
		private ThreadPriority m_priority;

		// Token: 0x040016B8 RID: 5816
		private ThreadStart m_timerThreadStart;

		// Token: 0x040016B9 RID: 5817
		private int m_timerThreadStatus;

		// Token: 0x040016BA RID: 5818
		private FileTime m_terminationTime;

		// Token: 0x02000438 RID: 1080
		private enum TimerThreadStatus
		{
			// Token: 0x040016BC RID: 5820
			NotPresent,
			// Token: 0x040016BD RID: 5821
			Present,
			// Token: 0x040016BE RID: 5822
			Terminating
		}
	}
}
