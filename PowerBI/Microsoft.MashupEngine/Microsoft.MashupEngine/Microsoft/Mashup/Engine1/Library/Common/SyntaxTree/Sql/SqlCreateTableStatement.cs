using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001202 RID: 4610
	internal sealed class SqlCreateTableStatement : SqlStatement
	{
		// Token: 0x0600798A RID: 31114 RVA: 0x001A4267 File Offset: 0x001A2467
		public SqlCreateTableStatement(TableReference table, List<SqlColumnDefinition> columnList = null)
		{
			this.table = table;
			this.columnList = columnList;
		}

		// Token: 0x1700212F RID: 8495
		// (get) Token: 0x0600798B RID: 31115 RVA: 0x001A427D File Offset: 0x001A247D
		public TableReference Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17002130 RID: 8496
		// (get) Token: 0x0600798C RID: 31116 RVA: 0x001A4285 File Offset: 0x001A2485
		public IList<SqlColumnDefinition> ColumnList
		{
			get
			{
				return this.columnList;
			}
		}

		// Token: 0x0600798D RID: 31117 RVA: 0x001A4290 File Offset: 0x001A2490
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSpaceAfter(writer.Settings.CreateTable);
			this.table.WriteCreateScript(writer);
			writer.WriteSpace();
			writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
			bool flag = false;
			foreach (SqlColumnDefinition sqlColumnDefinition in this.columnList)
			{
				flag = writer.WriteCommaIfNeeded(flag);
				sqlColumnDefinition.WriteCreateScript(writer);
			}
			writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
		}

		// Token: 0x04004250 RID: 16976
		private readonly TableReference table;

		// Token: 0x04004251 RID: 16977
		private readonly List<SqlColumnDefinition> columnList;
	}
}
