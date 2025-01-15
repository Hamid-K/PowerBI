using System;
using System.Threading.Tasks;

namespace Microsoft.InfoNav.Common
{
	// Token: 0x02000066 RID: 102
	internal sealed class RateLimitController : IRateLimitController
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0000A15C File Offset: 0x0000835C
		internal RateLimitController(TimeSpan minTimeBetweenRequests, bool exponentialBackOff = false, Func<DateTime> now = null)
		{
			this._exponentialBackOff = exponentialBackOff;
			Func<DateTime> func = now;
			if (now == null && (func = RateLimitController.<>c.<>9__5_0) == null)
			{
				func = (RateLimitController.<>c.<>9__5_0 = () => DateTime.UtcNow);
			}
			this._now = func;
			this._minTimeBetweenRequests = (this._exponentialBackOff ? TimeSpan.FromTicks(minTimeBetweenRequests.Ticks / 2L) : minTimeBetweenRequests);
			this._nextTimeToGrantAccess = this._now();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000A1DC File Offset: 0x000083DC
		public async Task<bool> TryRequestAccessAsync()
		{
			return await this.TryRequestAccessAsync(TimeSpan.Zero);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000A220 File Offset: 0x00008420
		public async Task<bool> TryRequestAccessAsync(TimeSpan timeout)
		{
			DateTime dateTime = this._now();
			object @lock = this._lock;
			TimeSpan timeSpan;
			lock (@lock)
			{
				if (this._exponentialBackOff)
				{
					this._minTimeBetweenRequests = TimeSpan.FromTicks(this._minTimeBetweenRequests.Ticks * 2L);
				}
				if (timeout > TimeSpan.Zero && dateTime + timeout < this._nextTimeToGrantAccess)
				{
					return false;
				}
				DateTime dateTime2 = this._now();
				if (dateTime2 >= this._nextTimeToGrantAccess)
				{
					this._nextTimeToGrantAccess = dateTime2 + this._minTimeBetweenRequests;
					return true;
				}
				timeSpan = this._nextTimeToGrantAccess - dateTime2;
				this._nextTimeToGrantAccess += this._minTimeBetweenRequests;
			}
			await Task.Delay(timeSpan);
			return true;
		}

		// Token: 0x040000D0 RID: 208
		private readonly object _lock = new object();

		// Token: 0x040000D1 RID: 209
		private readonly bool _exponentialBackOff;

		// Token: 0x040000D2 RID: 210
		private readonly Func<DateTime> _now;

		// Token: 0x040000D3 RID: 211
		private TimeSpan _minTimeBetweenRequests;

		// Token: 0x040000D4 RID: 212
		private DateTime _nextTimeToGrantAccess;
	}
}
