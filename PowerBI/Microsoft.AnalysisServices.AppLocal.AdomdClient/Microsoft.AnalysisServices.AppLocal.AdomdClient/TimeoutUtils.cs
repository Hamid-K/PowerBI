using System;
using System.Diagnostics;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000038 RID: 56
	internal class TimeoutUtils
	{
		// Token: 0x0200018F RID: 399
		// (Invoke) Token: 0x0600122D RID: 4653
		internal delegate bool OnTimeoutAction(bool isOnDispose);

		// Token: 0x02000190 RID: 400
		internal class TimeLeft
		{
			// Token: 0x1700065F RID: 1631
			// (get) Token: 0x06001230 RID: 4656 RVA: 0x0003F90F File Offset: 0x0003DB0F
			// (set) Token: 0x06001231 RID: 4657 RVA: 0x0003F917 File Offset: 0x0003DB17
			internal bool Infinite { get; set; }

			// Token: 0x17000660 RID: 1632
			// (get) Token: 0x06001232 RID: 4658 RVA: 0x0003F920 File Offset: 0x0003DB20
			// (set) Token: 0x06001233 RID: 4659 RVA: 0x0003F928 File Offset: 0x0003DB28
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

			// Token: 0x17000661 RID: 1633
			// (get) Token: 0x06001234 RID: 4660 RVA: 0x0003F939 File Offset: 0x0003DB39
			internal int TimeSec
			{
				get
				{
					return this.timeMs / 1000;
				}
			}

			// Token: 0x06001235 RID: 4661 RVA: 0x0003F947 File Offset: 0x0003DB47
			private TimeLeft(int timeMs)
			{
				if (timeMs == 0)
				{
					this.Infinite = true;
				}
				this.TimeMs = timeMs;
			}

			// Token: 0x06001236 RID: 4662 RVA: 0x0003F960 File Offset: 0x0003DB60
			internal static TimeoutUtils.TimeLeft FromMs(int timeMs)
			{
				return new TimeoutUtils.TimeLeft(timeMs);
			}

			// Token: 0x06001237 RID: 4663 RVA: 0x0003F968 File Offset: 0x0003DB68
			internal static TimeoutUtils.TimeLeft FromSeconds(int timeSec)
			{
				return new TimeoutUtils.TimeLeft(timeSec * 1000);
			}

			// Token: 0x04000C5E RID: 3166
			private int timeMs;
		}

		// Token: 0x02000191 RID: 401
		internal class TimeRestrictedMonitor : Disposable
		{
			// Token: 0x06001238 RID: 4664 RVA: 0x0003F976 File Offset: 0x0003DB76
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

			// Token: 0x06001239 RID: 4665 RVA: 0x0003F9B0 File Offset: 0x0003DBB0
			internal void CheckNow()
			{
				this.CheckAndInvokeTimeoutAction(false);
			}

			// Token: 0x0600123A RID: 4666 RVA: 0x0003F9B9 File Offset: 0x0003DBB9
			internal void Restart()
			{
				this.watch.Reset();
				this.watch.Start();
			}

			// Token: 0x0600123B RID: 4667 RVA: 0x0003F9D1 File Offset: 0x0003DBD1
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.watch.Stop();
					this.CheckAndInvokeTimeoutAction(true);
				}
				base.Dispose(disposing);
			}

			// Token: 0x0600123C RID: 4668 RVA: 0x0003F9EF File Offset: 0x0003DBEF
			private void CheckAndInvokeTimeoutAction(bool isOnDispose)
			{
				if (!this.timeLeft.Infinite && (long)this.timeLeft.TimeMs < this.watch.ElapsedMilliseconds)
				{
					this.timoutAction(isOnDispose);
				}
			}

			// Token: 0x04000C60 RID: 3168
			private TimeoutUtils.TimeLeft timeLeft;

			// Token: 0x04000C61 RID: 3169
			private Stopwatch watch;

			// Token: 0x04000C62 RID: 3170
			private TimeoutUtils.OnTimeoutAction timoutAction;
		}
	}
}
