using System;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200025B RID: 603
	public sealed class ReplacementDbQueryWrapper<TElement>
	{
		// Token: 0x06001ED9 RID: 7897 RVA: 0x00055B64 File Offset: 0x00053D64
		private ReplacementDbQueryWrapper(ObjectQuery<TElement> query)
		{
			this._query = query;
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00055B73 File Offset: 0x00053D73
		internal static ReplacementDbQueryWrapper<TElement> Create(ObjectQuery query)
		{
			return new ReplacementDbQueryWrapper<TElement>((ObjectQuery<TElement>)query);
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x00055B80 File Offset: 0x00053D80
		public ObjectQuery<TElement> Query
		{
			get
			{
				return this._query;
			}
		}

		// Token: 0x04000B3B RID: 2875
		private readonly ObjectQuery<TElement> _query;
	}
}
