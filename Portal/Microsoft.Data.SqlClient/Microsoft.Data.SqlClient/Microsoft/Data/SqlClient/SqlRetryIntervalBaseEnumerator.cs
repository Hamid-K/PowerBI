using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000035 RID: 53
	public abstract class SqlRetryIntervalBaseEnumerator : IEnumerator<TimeSpan>, IDisposable, IEnumerator, ICloneable
	{
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0000E749 File Offset: 0x0000C949
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x0000E751 File Offset: 0x0000C951
		public TimeSpan GapTimeInterval { get; protected set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0000E75A File Offset: 0x0000C95A
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x0000E762 File Offset: 0x0000C962
		public TimeSpan MaxTimeInterval { get; protected set; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0000E76B File Offset: 0x0000C96B
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x0000E773 File Offset: 0x0000C973
		public TimeSpan MinTimeInterval { get; protected set; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0000E77C File Offset: 0x0000C97C
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x0000E784 File Offset: 0x0000C984
		public TimeSpan Current { get; protected set; } = TimeSpan.Zero;

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x0000E78D File Offset: 0x0000C98D
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0000E79C File Offset: 0x0000C99C
		public SqlRetryIntervalBaseEnumerator()
		{
			this.GapTimeInterval = TimeSpan.Zero;
			this.MaxTimeInterval = TimeSpan.Zero;
			this.MinTimeInterval = TimeSpan.Zero;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0000E7FC File Offset: 0x0000C9FC
		public SqlRetryIntervalBaseEnumerator(TimeSpan timeInterval, TimeSpan maxTime, TimeSpan minTime)
		{
			this.Validate(timeInterval, maxTime, minTime);
			this.GapTimeInterval = timeInterval;
			this.MaxTimeInterval = maxTime;
			this.MinTimeInterval = minTime;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0000E857 File Offset: 0x0000CA57
		public virtual void Reset()
		{
			this.Current = TimeSpan.Zero;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0000E864 File Offset: 0x0000CA64
		protected virtual void Validate(TimeSpan timeInterval, TimeSpan maxTimeInterval, TimeSpan minTimeInterval)
		{
			if (minTimeInterval < this._minValue || minTimeInterval > this._maxValue)
			{
				throw SqlReliabilityUtil.ArgumentOutOfRange("minTimeInterval", minTimeInterval, this._minValue, this._maxValue);
			}
			if (maxTimeInterval < this._minValue || maxTimeInterval > this._maxValue)
			{
				throw SqlReliabilityUtil.ArgumentOutOfRange("maxTimeInterval", maxTimeInterval, this._minValue, this._maxValue);
			}
			if (timeInterval < this._minValue || timeInterval > this._maxValue)
			{
				throw SqlReliabilityUtil.ArgumentOutOfRange("timeInterval", timeInterval, this._minValue, this._maxValue);
			}
			if (maxTimeInterval < minTimeInterval)
			{
				throw SqlReliabilityUtil.InvalidMinAndMaxPair("minTimeInterval", minTimeInterval, "maxTimeInterval", maxTimeInterval);
			}
		}

		// Token: 0x06000710 RID: 1808
		protected abstract TimeSpan GetNextInterval();

		// Token: 0x06000711 RID: 1809 RVA: 0x0000E928 File Offset: 0x0000CB28
		public virtual bool MoveNext()
		{
			TimeSpan timeSpan = this.Current;
			if (this.Current < this.MaxTimeInterval)
			{
				timeSpan = this.GetNextInterval();
			}
			bool flag = timeSpan <= this.MaxTimeInterval;
			if (flag)
			{
				this.Current = timeSpan;
			}
			return flag;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000BB08 File Offset: 0x00009D08
		public virtual void Dispose()
		{
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0000E96E File Offset: 0x0000CB6E
		public virtual object Clone()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000B7 RID: 183
		private readonly TimeSpan _minValue = TimeSpan.Zero;

		// Token: 0x040000B8 RID: 184
		private readonly TimeSpan _maxValue = TimeSpan.FromSeconds(120.0);
	}
}
