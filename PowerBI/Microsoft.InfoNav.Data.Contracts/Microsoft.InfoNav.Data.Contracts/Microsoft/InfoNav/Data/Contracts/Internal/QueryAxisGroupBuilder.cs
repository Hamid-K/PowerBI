using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000284 RID: 644
	public sealed class QueryAxisGroupBuilder<TParent> : BaseBuilder<QueryAxisGroup, TParent>
	{
		// Token: 0x06001370 RID: 4976 RVA: 0x00022FB1 File Offset: 0x000211B1
		public QueryAxisGroupBuilder(QueryAxisGroup queryAxisGroup, TParent parent)
			: base(parent)
		{
			this._queryAxisGroup = queryAxisGroup;
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00022FC1 File Offset: 0x000211C1
		public QueryAxisGroupBuilder<TParent> WithKey(QueryExpressionContainer queryExpressionContainer)
		{
			this._queryAxisGroup.Keys.Add(queryExpressionContainer);
			return this;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00022FD5 File Offset: 0x000211D5
		public QueryAxisGroupBuilder<TParent> WithSubtotal()
		{
			this._queryAxisGroup.Subtotal = true;
			return this;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x00022FE4 File Offset: 0x000211E4
		public override QueryAxisGroup Build()
		{
			return this._queryAxisGroup;
		}

		// Token: 0x04000800 RID: 2048
		private readonly QueryAxisGroup _queryAxisGroup;
	}
}
