using System;
using System.Diagnostics;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x0200002E RID: 46
	internal sealed class SqlTupleExpression : SqlExpression
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x000080E4 File Offset: 0x000062E4
		internal SqlTupleExpression()
			: base(false)
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000080F8 File Offset: 0x000062F8
		internal void AddTupleValue(SqlExpression sqlExpression)
		{
			this.AddTupleValue(sqlExpression, sqlExpression.IsNullable);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00008107 File Offset: 0x00006307
		internal void AddTupleValue(ISqlSnippet value, bool nullable)
		{
			this.m_tupleValues.Add(value);
			this.m_isNullable = this.m_isNullable || nullable;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00004555 File Offset: 0x00002755
		internal override bool CanGroupBy
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00008123 File Offset: 0x00006323
		internal override bool IsNullable
		{
			[DebuggerStepThrough]
			get
			{
				return base.IsNullable && this.m_isNullable;
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00008135 File Offset: 0x00006335
		protected override void InitValues()
		{
			base.Values.AddRange(this.m_tupleValues);
		}

		// Token: 0x040000C5 RID: 197
		private readonly SqlExpression.ValuesCollection m_tupleValues = new SqlExpression.ValuesCollection();

		// Token: 0x040000C6 RID: 198
		private bool m_isNullable;
	}
}
