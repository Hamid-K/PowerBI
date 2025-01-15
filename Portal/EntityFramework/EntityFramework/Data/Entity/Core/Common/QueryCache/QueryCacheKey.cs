using System;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x0200062D RID: 1581
	internal abstract class QueryCacheKey
	{
		// Token: 0x06004C2A RID: 19498 RVA: 0x0010C308 File Offset: 0x0010A508
		protected QueryCacheKey()
		{
			this._hitCount = 1U;
		}

		// Token: 0x06004C2B RID: 19499
		public abstract override bool Equals(object obj);

		// Token: 0x06004C2C RID: 19500
		public abstract override int GetHashCode();

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06004C2D RID: 19501 RVA: 0x0010C317 File Offset: 0x0010A517
		// (set) Token: 0x06004C2E RID: 19502 RVA: 0x0010C31F File Offset: 0x0010A51F
		internal uint HitCount
		{
			get
			{
				return this._hitCount;
			}
			set
			{
				this._hitCount = value;
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06004C2F RID: 19503 RVA: 0x0010C328 File Offset: 0x0010A528
		// (set) Token: 0x06004C30 RID: 19504 RVA: 0x0010C330 File Offset: 0x0010A530
		internal int AgingIndex { get; set; }

		// Token: 0x06004C31 RID: 19505 RVA: 0x0010C339 File Offset: 0x0010A539
		internal void UpdateHit()
		{
			if (4294967295U != this._hitCount)
			{
				this._hitCount += 1U;
			}
		}

		// Token: 0x06004C32 RID: 19506 RVA: 0x0010C352 File Offset: 0x0010A552
		protected virtual bool Equals(string s, string t)
		{
			return string.Equals(s, t, QueryCacheKey._stringComparison);
		}

		// Token: 0x04001AA6 RID: 6822
		protected const int EstimatedParameterStringSize = 20;

		// Token: 0x04001AA7 RID: 6823
		private uint _hitCount;

		// Token: 0x04001AA8 RID: 6824
		protected static StringComparison _stringComparison = StringComparison.Ordinal;
	}
}
