using System;
using System.Threading.Tasks;

namespace Microsoft.BIServer.HostingEnvironment
{
	// Token: 0x0200000A RID: 10
	public sealed class ExponentialBackoff
	{
		// Token: 0x06000034 RID: 52 RVA: 0x00002852 File Offset: 0x00000A52
		public ExponentialBackoff(TimeSpan maxWaitPeriod, TimeSpan backOffExpirePeriod, TimeSpan initialWaitPeriod, double backOffMultiplier)
			: this(maxWaitPeriod, backOffExpirePeriod, initialWaitPeriod, backOffMultiplier, new SystemClock())
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002864 File Offset: 0x00000A64
		public ExponentialBackoff(TimeSpan maxWaitPeriod, TimeSpan backOffExpirePeriod, TimeSpan initialWaitPeriod, double backOffMultiplier, IClock clock)
		{
			this._clock = clock;
			this._maxWaitPeriod = maxWaitPeriod;
			this._initialWaitPeriod = initialWaitPeriod;
			this._backOffExpirePeriod = backOffExpirePeriod;
			this._backOffMultiplier = backOffMultiplier;
			this._lastCallToWait = DateTime.MinValue;
			this._period = this._initialWaitPeriod;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028B4 File Offset: 0x00000AB4
		public async Task<double> BackoffAsync()
		{
			DateTime now = this._clock.Now;
			TimeSpan timeSpan = now - this._lastCallToWait;
			double sleepInMilliseconds = this._period.TotalMilliseconds - timeSpan.TotalMilliseconds;
			if (sleepInMilliseconds > 0.0)
			{
				Logger.Debug("Throttling by {0} MS", new object[] { sleepInMilliseconds });
				await this._clock.WaitAsync(TimeSpan.FromMilliseconds(sleepInMilliseconds));
				this._period = TimeSpan.FromMilliseconds(this._period.TotalMilliseconds * this._backOffMultiplier);
				if (this._period > this._maxWaitPeriod)
				{
					this._period = this._maxWaitPeriod;
				}
			}
			else
			{
				sleepInMilliseconds = 0.0;
				if (now >= this._lastCallToWait.Add(this._backOffExpirePeriod))
				{
					this._period = this._initialWaitPeriod;
				}
			}
			this._lastCallToWait = this._clock.Now;
			return sleepInMilliseconds;
		}

		// Token: 0x0400003E RID: 62
		private readonly TimeSpan _maxWaitPeriod;

		// Token: 0x0400003F RID: 63
		private readonly TimeSpan _initialWaitPeriod;

		// Token: 0x04000040 RID: 64
		private readonly TimeSpan _backOffExpirePeriod;

		// Token: 0x04000041 RID: 65
		private readonly double _backOffMultiplier;

		// Token: 0x04000042 RID: 66
		private TimeSpan _period;

		// Token: 0x04000043 RID: 67
		private DateTime _lastCallToWait;

		// Token: 0x04000044 RID: 68
		private IClock _clock;
	}
}
