using System;
using System.Diagnostics;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000038 RID: 56
	internal class TimeoutUtils
	{
		// Token: 0x0200018F RID: 399
		// (Invoke) Token: 0x06001220 RID: 4640
		internal delegate bool OnTimeoutAction(bool isOnDispose);

		// Token: 0x02000190 RID: 400
		internal class TimeLeft
		{
			// Token: 0x17000659 RID: 1625
			// (get) Token: 0x06001223 RID: 4643 RVA: 0x0003F3DF File Offset: 0x0003D5DF
			// (set) Token: 0x06001224 RID: 4644 RVA: 0x0003F3E7 File Offset: 0x0003D5E7
			internal bool Infinite { get; set; }

			// Token: 0x1700065A RID: 1626
			// (get) Token: 0x06001225 RID: 4645 RVA: 0x0003F3F0 File Offset: 0x0003D5F0
			// (set) Token: 0x06001226 RID: 4646 RVA: 0x0003F3F8 File Offset: 0x0003D5F8
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

			// Token: 0x1700065B RID: 1627
			// (get) Token: 0x06001227 RID: 4647 RVA: 0x0003F409 File Offset: 0x0003D609
			internal int TimeSec
			{
				get
				{
					return this.timeMs / 1000;
				}
			}

			// Token: 0x06001228 RID: 4648 RVA: 0x0003F417 File Offset: 0x0003D617
			private TimeLeft(int timeMs)
			{
				if (timeMs == 0)
				{
					this.Infinite = true;
				}
				this.TimeMs = timeMs;
			}

			// Token: 0x06001229 RID: 4649 RVA: 0x0003F430 File Offset: 0x0003D630
			internal static TimeoutUtils.TimeLeft FromMs(int timeMs)
			{
				return new TimeoutUtils.TimeLeft(timeMs);
			}

			// Token: 0x0600122A RID: 4650 RVA: 0x0003F438 File Offset: 0x0003D638
			internal static TimeoutUtils.TimeLeft FromSeconds(int timeSec)
			{
				return new TimeoutUtils.TimeLeft(timeSec * 1000);
			}

			// Token: 0x04000C4D RID: 3149
			private int timeMs;
		}

		// Token: 0x02000191 RID: 401
		internal class TimeRestrictedMonitor : Disposable
		{
			// Token: 0x0600122B RID: 4651 RVA: 0x0003F446 File Offset: 0x0003D646
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

			// Token: 0x0600122C RID: 4652 RVA: 0x0003F480 File Offset: 0x0003D680
			internal void CheckNow()
			{
				this.CheckAndInvokeTimeoutAction(false);
			}

			// Token: 0x0600122D RID: 4653 RVA: 0x0003F489 File Offset: 0x0003D689
			internal void Restart()
			{
				this.watch.Reset();
				this.watch.Start();
			}

			// Token: 0x0600122E RID: 4654 RVA: 0x0003F4A1 File Offset: 0x0003D6A1
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.watch.Stop();
					this.CheckAndInvokeTimeoutAction(true);
				}
				base.Dispose(disposing);
			}

			// Token: 0x0600122F RID: 4655 RVA: 0x0003F4BF File Offset: 0x0003D6BF
			private void CheckAndInvokeTimeoutAction(bool isOnDispose)
			{
				if (!this.timeLeft.Infinite && (long)this.timeLeft.TimeMs < this.watch.ElapsedMilliseconds)
				{
					this.timoutAction(isOnDispose);
				}
			}

			// Token: 0x04000C4F RID: 3151
			private TimeoutUtils.TimeLeft timeLeft;

			// Token: 0x04000C50 RID: 3152
			private Stopwatch watch;

			// Token: 0x04000C51 RID: 3153
			private TimeoutUtils.OnTimeoutAction timoutAction;
		}
	}
}
