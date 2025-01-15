using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x0200002B RID: 43
	internal sealed class SqlColumnRefExpression : SqlRefExpressionBase
	{
		// Token: 0x060001AA RID: 426 RVA: 0x00007EFA File Offset: 0x000060FA
		internal SqlColumnRefExpression(SqlTableSource tableSource, string columnAlias, SqlBatch sqlBatch, bool nullable)
			: base(tableSource, sqlBatch, nullable)
		{
			if (columnAlias == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("columnAlias"));
			}
			if (sqlBatch == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("sqlBatch"));
			}
			this.m_columnAlias = columnAlias;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00004B5D File Offset: 0x00002D5D
		internal override bool CanGroupBy
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007F34 File Offset: 0x00006134
		protected override void InitValues()
		{
			base.Values.Add(new SqlStringSnippet("{0}.{1}", new object[]
			{
				base.SqlBatch.GetDelimitedIdentifier(base.SqlTableSource.Alias),
				base.SqlBatch.GetDelimitedIdentifier(this.m_columnAlias)
			}));
		}

		// Token: 0x040000C2 RID: 194
		private readonly string m_columnAlias;
	}
}
