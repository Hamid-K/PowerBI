using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001118 RID: 4376
	internal class QueryResultQuery : FilteredTableQuery
	{
		// Token: 0x0600727D RID: 29309 RVA: 0x00189B17 File Offset: 0x00187D17
		public QueryResultQuery(QueryResultTableValue table)
			: base(table, table.Host)
		{
		}

		// Token: 0x1700200E RID: 8206
		// (get) Token: 0x0600727E RID: 29310 RVA: 0x00189B26 File Offset: 0x00187D26
		public QueryResultTableValue QueryResultTable
		{
			get
			{
				return (QueryResultTableValue)base.Table;
			}
		}

		// Token: 0x0600727F RID: 29311 RVA: 0x00189B33 File Offset: 0x00187D33
		public override ActionValue InsertRows(Query rowsToInsert)
		{
			return this.QueryResultTable.StatementBuilder.InsertRows(rowsToInsert);
		}

		// Token: 0x06007280 RID: 29312 RVA: 0x00189B46 File Offset: 0x00187D46
		public override ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector)
		{
			return this.QueryResultTable.StatementBuilder.UpdateRows(columnUpdates, selector);
		}

		// Token: 0x06007281 RID: 29313 RVA: 0x00189B5A File Offset: 0x00187D5A
		public override ActionValue DeleteRows(FunctionValue selector)
		{
			return this.QueryResultTable.StatementBuilder.DeleteRows(selector);
		}
	}
}
