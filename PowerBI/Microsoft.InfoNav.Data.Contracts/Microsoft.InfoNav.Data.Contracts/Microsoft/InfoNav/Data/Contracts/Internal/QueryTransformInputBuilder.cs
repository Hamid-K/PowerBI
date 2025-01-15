using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.SemanticQuery.ExpressionBuilder;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002DE RID: 734
	public sealed class QueryTransformInputBuilder<TParent> : BaseBuilder<QueryTransformInput, QueryTransformBuilder<TParent>>
	{
		// Token: 0x06001886 RID: 6278 RVA: 0x0002BF7C File Offset: 0x0002A17C
		public QueryTransformInputBuilder(QueryTransformBuilder<TParent> parent)
			: base(parent)
		{
			this._queryTransformInput = new QueryTransformInput();
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0002BF90 File Offset: 0x0002A190
		public QueryTransformInputBuilder(QueryTransformInput queryTransformInput, QueryTransformBuilder<TParent> parent)
			: base(parent)
		{
			this._queryTransformInput = queryTransformInput;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x0002BFA0 File Offset: 0x0002A1A0
		public QueryTransformTableBuilder<QueryTransformInputBuilder<TParent>> WithInputTable(string name)
		{
			QueryTransformTable queryTransformTable = new QueryTransformTable();
			queryTransformTable.Name = name;
			this._queryTransformInput.Table = queryTransformTable;
			return new QueryTransformTableBuilder<QueryTransformInputBuilder<TParent>>(queryTransformTable, this);
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x0002BFD0 File Offset: 0x0002A1D0
		public QueryTransformInputBuilder<TParent> WithInputParameter(string name, string value)
		{
			if (this._queryTransformInput.Parameters == null)
			{
				this._queryTransformInput.Parameters = new List<QueryExpressionContainer>();
			}
			this._queryTransformInput.Parameters.Add(new QueryExpressionContainer
			{
				Literal = value.Literal(),
				Name = name
			});
			return this;
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x0002C023 File Offset: 0x0002A223
		public override QueryTransformInput Build()
		{
			return this._queryTransformInput;
		}

		// Token: 0x040008A8 RID: 2216
		private readonly QueryTransformInput _queryTransformInput;
	}
}
