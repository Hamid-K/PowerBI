using System;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x0200062C RID: 1580
	internal class QueryCacheEntry
	{
		// Token: 0x06004C27 RID: 19495 RVA: 0x0010C2E2 File Offset: 0x0010A4E2
		internal QueryCacheEntry(QueryCacheKey queryCacheKey, object target)
		{
			this._queryCacheKey = queryCacheKey;
			this._target = target;
		}

		// Token: 0x06004C28 RID: 19496 RVA: 0x0010C2F8 File Offset: 0x0010A4F8
		internal virtual object GetTarget()
		{
			return this._target;
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06004C29 RID: 19497 RVA: 0x0010C300 File Offset: 0x0010A500
		internal QueryCacheKey QueryCacheKey
		{
			get
			{
				return this._queryCacheKey;
			}
		}

		// Token: 0x04001AA4 RID: 6820
		private readonly QueryCacheKey _queryCacheKey;

		// Token: 0x04001AA5 RID: 6821
		protected readonly object _target;
	}
}
