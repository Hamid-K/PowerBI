using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x0200002C RID: 44
	internal sealed class SqlPassThruExpression : SqlRefExpressionBase
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00007F89 File Offset: 0x00006189
		internal SqlPassThruExpression(SqlTableSource tableSource, Expression expressionKey, SqlBatch sqlBatch, bool nullable)
			: this(tableSource, sqlBatch, nullable)
		{
			this.m_sourceSqlSelectExpression = base.SqlTableSource.TuplesSource.GetSelectExpression(expressionKey);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007FAC File Offset: 0x000061AC
		internal SqlPassThruExpression(SqlTableSource tableSource, DsvColumn expressionKey, SqlBatch sqlBatch, bool nullable)
			: this(tableSource, sqlBatch, nullable)
		{
			this.m_sourceSqlSelectExpression = base.SqlTableSource.TuplesSource.GetSelectExpression(expressionKey);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00007FCF File Offset: 0x000061CF
		private SqlPassThruExpression(SqlTableSource tableSource, SqlBatch sqlBatch, bool nullable)
			: base(tableSource, sqlBatch, nullable)
		{
			if (tableSource == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("tableSource"));
			}
			if (tableSource.TuplesSource == null)
			{
				throw SQEAssert.AssertFalseAndThrow("tableSource.TuplesSource == null", Array.Empty<object>());
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00004B5D File Offset: 0x00002D5D
		internal override bool CanGroupBy
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008008 File Offset: 0x00006208
		protected override void InitValues()
		{
			if (this.m_sourceSqlSelectExpression == null)
			{
				throw SQEAssert.AssertFalseAndThrow("m_sourceSqlSelectExpression == null", Array.Empty<object>());
			}
			for (int i = 0; i < this.m_sourceSqlSelectExpression.Aliases.Length; i++)
			{
				string text = this.m_sourceSqlSelectExpression.Aliases[i];
				SqlExpression sqlExpression = this.m_sourceSqlSelectExpression.SqlExpression.Values[i] as SqlExpression;
				base.Values.Add(new SqlColumnRefExpression(base.SqlTableSource, this.m_sourceSqlSelectExpression.Aliases[i], base.SqlBatch, (sqlExpression != null) ? sqlExpression.IsNullable : this.IsNullable));
			}
		}

		// Token: 0x040000C3 RID: 195
		private readonly SqlSelectExpression m_sourceSqlSelectExpression;
	}
}
