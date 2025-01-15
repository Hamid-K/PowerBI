using System;
using Microsoft.Data.Common;

namespace Microsoft.Data.ProviderBase
{
	// Token: 0x02000167 RID: 359
	internal class TimeoutTimer
	{
		// Token: 0x06001AA5 RID: 6821 RVA: 0x0006D2BC File Offset: 0x0006B4BC
		internal static TimeoutTimer StartSecondsTimeout(int seconds)
		{
			TimeoutTimer timeoutTimer = new TimeoutTimer();
			timeoutTimer.SetTimeoutSeconds(seconds);
			return timeoutTimer;
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0006D2D8 File Offset: 0x0006B4D8
		internal static TimeoutTimer StartMillisecondsTimeout(long milliseconds)
		{
			TimeoutTimer timeoutTimer = new TimeoutTimer();
			timeoutTimer._originalTimerTicks = milliseconds * 10000L;
			timeoutTimer._timerExpire = checked(ADP.TimerCurrent() + timeoutTimer._originalTimerTicks);
			timeoutTimer._isInfiniteTimeout = false;
			return timeoutTimer;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x0006D313 File Offset: 0x0006B513
		internal void SetTimeoutSeconds(int seconds)
		{
			if (TimeoutTimer.InfiniteTimeout == (long)seconds)
			{
				this._isInfiniteTimeout = true;
				return;
			}
			this._originalTimerTicks = ADP.TimerFromSeconds(seconds);
			this._timerExpire = checked(ADP.TimerCurrent() + this._originalTimerTicks);
			this._isInfiniteTimeout = false;
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x0006D34B File Offset: 0x0006B54B
		internal void Reset()
		{
			if (TimeoutTimer.InfiniteTimeout == this._originalTimerTicks)
			{
				this._isInfiniteTimeout = true;
				return;
			}
			this._timerExpire = checked(ADP.TimerCurrent() + this._originalTimerTicks);
			this._isInfiniteTimeout = false;
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x0006D37B File Offset: 0x0006B57B
		internal bool IsExpired
		{
			get
			{
				return !this.IsInfinite && ADP.TimerHasExpired(this._timerExpire);
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001AAA RID: 6826 RVA: 0x0006D392 File Offset: 0x0006B592
		internal bool IsInfinite
		{
			get
			{
				return this._isInfiniteTimeout;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0006D39A File Offset: 0x0006B59A
		internal long LegacyTimerExpire
		{
			get
			{
				if (!this._isInfiniteTimeout)
				{
					return this._timerExpire;
				}
				return long.MaxValue;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x0006D3B4 File Offset: 0x0006B5B4
		internal long MillisecondsRemaining
		{
			get
			{
				long num;
				if (this._isInfiniteTimeout)
				{
					num = long.MaxValue;
				}
				else
				{
					num = ADP.TimerRemainingMilliseconds(this._timerExpire);
					if (0L > num)
					{
						num = 0L;
					}
				}
				return num;
			}
		}

		// Token: 0x04000AE5 RID: 2789
		private long _timerExpire;

		// Token: 0x04000AE6 RID: 2790
		private bool _isInfiniteTimeout;

		// Token: 0x04000AE7 RID: 2791
		private long _originalTimerTicks;

		// Token: 0x04000AE8 RID: 2792
		internal static readonly long InfiniteTimeout;
	}
}
