using System;
using System.Threading;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200022B RID: 555
	internal class OncePerPeriodHelper
	{
		// Token: 0x060013F1 RID: 5105 RVA: 0x0004B85D File Offset: 0x00049A5D
		public OncePerPeriodHelper()
			: this(OncePerPeriodHelper.DefaultPeriod)
		{
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0004B86A File Offset: 0x00049A6A
		public OncePerPeriodHelper(TimeSpan period)
		{
			this._period = period;
			this._lastCallToWait = DateTime.MinValue;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0004B884 File Offset: 0x00049A84
		public void Wait()
		{
			DateTime dateTime = this._lastCallToWait.Add(this._period);
			DateTime utcNow = DateTime.UtcNow;
			if (dateTime > utcNow)
			{
				Thread.Sleep(dateTime.Subtract(utcNow));
			}
			this._lastCallToWait = DateTime.UtcNow;
		}

		// Token: 0x0400071A RID: 1818
		public static TimeSpan DefaultPeriod = TimeSpan.FromSeconds(60.0);

		// Token: 0x0400071B RID: 1819
		private readonly TimeSpan _period;

		// Token: 0x0400071C RID: 1820
		private DateTime _lastCallToWait;
	}
}
