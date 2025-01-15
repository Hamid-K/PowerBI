using System;
using System.Diagnostics;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x0200005C RID: 92
	internal abstract class SqlNestedQuery : SqlQuery
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0001306B File Offset: 0x0001126B
		protected SqlNestedQuery(QueryPlanBuilder qpBuilder, NestedQueryKey key)
			: base(qpBuilder)
		{
			this.m_key = key;
		}

		// Token: 0x06000450 RID: 1104
		internal abstract SqlTableSource Join(SqlSelectQuery parentQuery, SqlSelectQuery nestedQuery);

		// Token: 0x06000451 RID: 1105
		internal abstract void SelectJoinKeys(SqlSelectQuery selectQuery);

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0001307B File Offset: 0x0001127B
		internal NestedQueryKey Key
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_key;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00013083 File Offset: 0x00011283
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0001308B File Offset: 0x0001128B
		internal SqlTableSource TableSourceInParentQuery
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_tabelSourceInParentQuery;
			}
			[DebuggerStepThrough]
			set
			{
				if (this.m_tabelSourceInParentQuery != null || value == null)
				{
					throw SQEAssert.AssertFalseAndThrow("This property can be set only once.", Array.Empty<object>());
				}
				this.m_tabelSourceInParentQuery = value;
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000130AF File Offset: 0x000112AF
		protected override void TraceQueryExtInfo(FormattedStringWriter qpTracer)
		{
			base.TraceQueryExtInfo(qpTracer);
			qpTracer.IndentWriteLine("KEY={0}", new object[] { this.m_key });
		}

		// Token: 0x040001E4 RID: 484
		private readonly NestedQueryKey m_key;

		// Token: 0x040001E5 RID: 485
		private SqlTableSource m_tabelSourceInParentQuery;
	}
}
