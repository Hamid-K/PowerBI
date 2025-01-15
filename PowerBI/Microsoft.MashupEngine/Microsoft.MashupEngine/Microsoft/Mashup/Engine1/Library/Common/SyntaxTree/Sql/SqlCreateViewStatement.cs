using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001203 RID: 4611
	internal sealed class SqlCreateViewStatement : SqlStatement
	{
		// Token: 0x0600798E RID: 31118 RVA: 0x001A4324 File Offset: 0x001A2524
		public SqlCreateViewStatement(TableReference view, List<SqlColumnDefinition> columnList, SqlQueryExpression selectStatement)
		{
			this.view = view;
			this.columnList = columnList;
			this.selectStatement = selectStatement;
		}

		// Token: 0x17002131 RID: 8497
		// (get) Token: 0x0600798F RID: 31119 RVA: 0x001A4341 File Offset: 0x001A2541
		public TableReference View
		{
			get
			{
				return this.view;
			}
		}

		// Token: 0x17002132 RID: 8498
		// (get) Token: 0x06007990 RID: 31120 RVA: 0x001A4349 File Offset: 0x001A2549
		public IList<SqlColumnDefinition> ColumnList
		{
			get
			{
				return this.columnList;
			}
		}

		// Token: 0x17002133 RID: 8499
		// (get) Token: 0x06007991 RID: 31121 RVA: 0x001A4351 File Offset: 0x001A2551
		public SqlQueryExpression SelectStatement
		{
			get
			{
				return this.selectStatement;
			}
		}

		// Token: 0x06007992 RID: 31122 RVA: 0x001A435C File Offset: 0x001A255C
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSpaceAfter(writer.Settings.CreateView);
			this.view.WriteCreateScript(writer);
			if (this.columnList != null && this.columnList.Count > 0)
			{
				writer.WriteSpace();
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				bool flag = false;
				foreach (SqlColumnDefinition sqlColumnDefinition in this.columnList)
				{
					flag = writer.WriteCommaIfNeeded(flag);
					sqlColumnDefinition.Column.WriteCreateScript(writer);
				}
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
			}
			writer.WriteSpace();
			writer.Write(SqlLanguageStrings.AsSqlString);
			writer.WriteSpace();
			this.selectStatement.WriteCreateScript(writer);
		}

		// Token: 0x04004252 RID: 16978
		private readonly TableReference view;

		// Token: 0x04004253 RID: 16979
		private readonly List<SqlColumnDefinition> columnList;

		// Token: 0x04004254 RID: 16980
		private readonly SqlQueryExpression selectStatement;
	}
}
