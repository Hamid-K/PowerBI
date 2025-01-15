using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005F3 RID: 1523
	internal class OdbcFilteredTableQuery : FilteredTableQuery
	{
		// Token: 0x0600300D RID: 12301 RVA: 0x00091670 File Offset: 0x0008F870
		public OdbcFilteredTableQuery(TableValue table, IEngineHost engineHost)
			: base(table, engineHost)
		{
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x0600300E RID: 12302 RVA: 0x0009167A File Offset: 0x0008F87A
		private OdbcStatementBuilder StatementBuilder
		{
			get
			{
				if (this.statementBuilder == null)
				{
					this.statementBuilder = new OdbcStatementBuilder(base.Table, ((OdbcQuery)base.Table.Query).ColumnInfos);
				}
				return this.statementBuilder;
			}
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000916B0 File Offset: 0x0008F8B0
		public override ActionValue InsertRows(Query rowsToInsert)
		{
			return this.StatementBuilder.InsertRows(rowsToInsert);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000916BE File Offset: 0x0008F8BE
		public override ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector)
		{
			return this.StatementBuilder.UpdateRows(columnUpdates, selector);
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000916CD File Offset: 0x0008F8CD
		public override ActionValue DeleteRows(FunctionValue selector)
		{
			return this.StatementBuilder.DeleteRows(selector);
		}

		// Token: 0x0400152A RID: 5418
		private OdbcStatementBuilder statementBuilder;
	}
}
