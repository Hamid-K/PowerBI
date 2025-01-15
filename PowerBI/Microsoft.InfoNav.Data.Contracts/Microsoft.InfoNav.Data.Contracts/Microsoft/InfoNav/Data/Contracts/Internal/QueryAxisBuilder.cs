using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000282 RID: 642
	public sealed class QueryAxisBuilder<TParent> : BaseBuilder<QueryAxis, TParent>
	{
		// Token: 0x06001362 RID: 4962 RVA: 0x00022E1A File Offset: 0x0002101A
		public QueryAxisBuilder(QueryAxis queryAxis, TParent parent)
			: base(parent)
		{
			this._queryAxis = queryAxis;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00022E2C File Offset: 0x0002102C
		public QueryAxisGroupBuilder<QueryAxisBuilder<TParent>> WithAxisGroup(bool withSubtotal = false)
		{
			QueryAxisGroup queryAxisGroup = new QueryAxisGroup
			{
				Keys = new List<QueryExpressionContainer>(),
				Subtotal = withSubtotal
			};
			this._queryAxis.Groups.Add(queryAxisGroup);
			return new QueryAxisGroupBuilder<QueryAxisBuilder<TParent>>(queryAxisGroup, this);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00022E69 File Offset: 0x00021069
		public override QueryAxis Build()
		{
			return this._queryAxis;
		}

		// Token: 0x040007FD RID: 2045
		private readonly QueryAxis _queryAxis;
	}
}
