using System;
using System.Diagnostics;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000053 RID: 83
	internal class TimeoutUtils
	{
		// Token: 0x0200018A RID: 394
		// (Invoke) Token: 0x060012C9 RID: 4809
		internal delegate bool OnTimeoutAction(bool isOnDispose);

		// Token: 0x0200018B RID: 395
		internal class TimeLeft
		{
			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x060012CC RID: 4812 RVA: 0x00041E86 File Offset: 0x00040086
			// (set) Token: 0x060012CD RID: 4813 RVA: 0x00041E8E File Offset: 0x0004008E
			internal bool Infinite { get; set; }

			// Token: 0x17000624 RID: 1572
			// (get) Token: 0x060012CE RID: 4814 RVA: 0x00041E97 File Offset: 0x00040097
			// (set) Token: 0x060012CF RID: 4815 RVA: 0x00041E9F File Offset: 0x0004009F
			internal int TimeMs
			{
				get
				{
					return this.timeMs;
				}
				set
				{
					if (!this.Infinite)
					{
						this.timeMs = value;
					}
				}
			}

			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00041EB0 File Offset: 0x000400B0
			internal int TimeSec
			{
				get
				{
					return this.timeMs / 1000;
				}
			}

			// Token: 0x060012D1 RID: 4817 RVA: 0x00041EBE File Offset: 0x000400BE
			private TimeLeft(int timeMs)
			{
				if (timeMs == 0)
				{
					this.Infinite = true;
				}
				this.TimeMs = timeMs;
			}

			// Token: 0x060012D2 RID: 4818 RVA: 0x00041ED7 File Offset: 0x000400D7
			internal static TimeoutUtils.TimeLeft FromMs(int timeMs)
			{
				return new TimeoutUtils.TimeLeft(timeMs);
			}

			// Token: 0x060012D3 RID: 4819 RVA: 0x00041EDF File Offset: 0x000400DF
			internal static TimeoutUtils.TimeLeft FromSeconds(int timeSec)
			{
				return new TimeoutUtils.TimeLeft(timeSec * 1000);
			}

			// Token: 0x04000C16 RID: 3094
			private int timeMs;
		}

		// Token: 0x0200018C RID: 396
		internal class TimeRestrictedMonitor : Disposable
		{
			// Token: 0x060012D4 RID: 4820 RVA: 0x00041EED File Offset: 0x000400ED
			internal TimeRestrictedMonitor(TimeoutUtils.TimeLeft timeLeft, TimeoutUtils.OnTimeoutAction timoutAction)
			{
				this.timeLeft = timeLeft;
				this.timoutAction = timoutAction;
				if (!timeLeft.Infinite && timeLeft.TimeMs < 0)
				{
					timoutAction(false);
				}
				this.watch = Stopwatch.StartNew();
			}

			// Token: 0x060012D5 RID: 4821 RVA: 0x00041F27 File Offset: 0x00040127
			internal void CheckNow()
			{
				this.CheckAndInvokeTimeoutAction(false);
			}

			// Token: 0x060012D6 RID: 4822 RVA: 0x00041F30 File Offset: 0x00040130
			internal void Restart()
			{
				this.watch.Reset();
				this.watch.Start();
			}

			// Token: 0x060012D7 RID: 4823 RVA: 0x00041F48 File Offset: 0x00040148
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.watch.Stop();
					this.CheckAndInvokeTimeoutAction(true);
				}
				base.Dispose(disposing);
			}

			// Token: 0x060012D8 RID: 4824 RVA: 0x00041F66 File Offset: 0x00040166
			private void CheckAndInvokeTimeoutAction(bool isOnDispose)
			{
				if (!this.timeLeft.Infinite && (long)this.timeLeft.TimeMs < this.watch.ElapsedMilliseconds)
				{
					this.timoutAction(isOnDispose);
				}
			}

			// Token: 0x04000C18 RID: 3096
			private TimeoutUtils.TimeLeft timeLeft;

			// Token: 0x04000C19 RID: 3097
			private Stopwatch watch;

			// Token: 0x04000C1A RID: 3098
			private TimeoutUtils.OnTimeoutAction timoutAction;
		}
	}
}
