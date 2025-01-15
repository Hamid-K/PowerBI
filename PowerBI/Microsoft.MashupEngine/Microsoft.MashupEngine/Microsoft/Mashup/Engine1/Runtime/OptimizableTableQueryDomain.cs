using System;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015A1 RID: 5537
	internal sealed class OptimizableTableQueryDomain : IQueryDomain
	{
		// Token: 0x06008AE7 RID: 35559 RVA: 0x001D3996 File Offset: 0x001D1B96
		public OptimizableTableQueryDomain(TableValue table)
		{
			this.table = table;
		}

		// Token: 0x170024A0 RID: 9376
		// (get) Token: 0x06008AE8 RID: 35560 RVA: 0x00002105 File Offset: 0x00000305
		public bool CanIndex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06008AE9 RID: 35561 RVA: 0x00002105 File Offset: 0x00000305
		public bool IsCompatibleWith(IQueryDomain domain)
		{
			return false;
		}

		// Token: 0x06008AEA RID: 35562 RVA: 0x001D39A5 File Offset: 0x001D1BA5
		public Query Optimize(Query query)
		{
			return new OptimizableTableQueryDomain.OptimizableTableQueryVisitor().VisitQuery(query);
		}

		// Token: 0x04004C23 RID: 19491
		private readonly TableValue table;

		// Token: 0x020015A2 RID: 5538
		private sealed class OptimizableTableQueryVisitor : QueryVisitor
		{
			// Token: 0x06008AEB RID: 35563 RVA: 0x001D39B4 File Offset: 0x001D1BB4
			protected override Query VisitDataSource(DataSourceQuery query)
			{
				TableQuery tableQuery = query as TableQuery;
				if (tableQuery != null)
				{
					return new TableQuery(tableQuery.Table.Optimize(), query.EngineHost);
				}
				return base.VisitDataSource(query);
			}
		}
	}
}
