using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002E0 RID: 736
	public sealed class QueryTransformOutputBuilder<TParent> : BaseBuilder<QueryTransformOutput, QueryTransformBuilder<TParent>>
	{
		// Token: 0x0600188E RID: 6286 RVA: 0x0002C044 File Offset: 0x0002A244
		public QueryTransformOutputBuilder(QueryTransformBuilder<TParent> parent)
			: base(parent)
		{
			this._queryTransformOutput = new QueryTransformOutput();
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0002C058 File Offset: 0x0002A258
		public QueryTransformOutputBuilder(QueryTransformOutput queryTransformOutput, QueryTransformBuilder<TParent> parent)
			: base(parent)
		{
			this._queryTransformOutput = queryTransformOutput;
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0002C068 File Offset: 0x0002A268
		public QueryTransformTableBuilder<QueryTransformOutputBuilder<TParent>> WithOutputTable(string name)
		{
			QueryTransformTable queryTransformTable = new QueryTransformTable();
			queryTransformTable.Name = name;
			this._queryTransformOutput.Table = queryTransformTable;
			return new QueryTransformTableBuilder<QueryTransformOutputBuilder<TParent>>(queryTransformTable, this);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x0002C095 File Offset: 0x0002A295
		public override QueryTransformOutput Build()
		{
			return this._queryTransformOutput;
		}

		// Token: 0x040008AA RID: 2218
		private readonly QueryTransformOutput _queryTransformOutput;
	}
}
