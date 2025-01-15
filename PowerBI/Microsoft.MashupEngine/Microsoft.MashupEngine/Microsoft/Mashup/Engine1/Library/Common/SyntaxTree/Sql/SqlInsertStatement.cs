using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x0200120F RID: 4623
	internal class SqlInsertStatement : SqlStatement
	{
		// Token: 0x06007A0F RID: 31247 RVA: 0x001A570E File Offset: 0x001A390E
		public SqlInsertStatement(TableReference table, List<IList<ScalarExpression>> values, OutputClause outputClause = null, List<ColumnReference> columnList = null)
			: this(table, values.Select((IList<ScalarExpression> l) => l.Cast<SqlExpression>()), outputClause, columnList)
		{
		}

		// Token: 0x06007A10 RID: 31248 RVA: 0x001A573F File Offset: 0x001A393F
		protected SqlInsertStatement(TableReference table, IEnumerable<IEnumerable<SqlExpression>> values, OutputClause outputClause = null, List<ColumnReference> columnList = null)
		{
			this.table = table;
			this.columnList = columnList;
			this.source = new ValuesInsertSource(values);
			this.outputClause = outputClause;
		}

		// Token: 0x06007A11 RID: 31249 RVA: 0x001A5769 File Offset: 0x001A3969
		public SqlInsertStatement(TableReference table, SqlQueryExpression sourceQuery, OutputClause outputClause = null, List<ColumnReference> columnList = null)
		{
			this.table = table;
			this.columnList = columnList;
			this.source = new QueryInsertSource(sourceQuery);
			this.outputClause = outputClause;
		}

		// Token: 0x17002155 RID: 8533
		// (get) Token: 0x06007A12 RID: 31250 RVA: 0x001A5793 File Offset: 0x001A3993
		public TableReference Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x17002156 RID: 8534
		// (get) Token: 0x06007A13 RID: 31251 RVA: 0x001A579B File Offset: 0x001A399B
		public IList<ColumnReference> ColumnList
		{
			get
			{
				return this.columnList;
			}
		}

		// Token: 0x17002157 RID: 8535
		// (get) Token: 0x06007A14 RID: 31252 RVA: 0x001A57A3 File Offset: 0x001A39A3
		public SqlInsertSource Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17002158 RID: 8536
		// (get) Token: 0x06007A15 RID: 31253 RVA: 0x001A57AB File Offset: 0x001A39AB
		public OutputClause OutputClause
		{
			get
			{
				return this.outputClause;
			}
		}

		// Token: 0x06007A16 RID: 31254 RVA: 0x001A57B4 File Offset: 0x001A39B4
		public override void WriteCreateScript(ScriptWriter writer)
		{
			this.outputClause.WritePrefixScript(writer);
			writer.WriteSpaceAfter(SqlLanguageStrings.InsertIntoSqlString);
			this.table.WriteCreateScript(writer);
			if (this.columnList != null)
			{
				if (this.columnList.Any<ColumnReference>())
				{
					writer.WriteSpace();
					writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
					bool flag = false;
					foreach (SqlExpression sqlExpression in this.columnList)
					{
						flag = writer.WriteCommaIfNeeded(flag);
						sqlExpression.WriteCreateScript(writer);
					}
					writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
				}
				else
				{
					writer.Settings.EmptyRowInsertStrategy.WriteEmptyColumnList(writer);
				}
			}
			this.outputClause.WriteCreateScript(writer);
			writer.WriteLine();
			this.source.WriteCreateScript(writer);
			this.outputClause.WriteSuffixScript(writer);
		}

		// Token: 0x04004279 RID: 17017
		private readonly TableReference table;

		// Token: 0x0400427A RID: 17018
		private readonly List<ColumnReference> columnList;

		// Token: 0x0400427B RID: 17019
		private readonly SqlInsertSource source;

		// Token: 0x0400427C RID: 17020
		private readonly OutputClause outputClause;
	}
}
