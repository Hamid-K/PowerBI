using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000030 RID: 48
	internal sealed class SqlNullExpression : SqlExpression
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00008954 File Offset: 0x00006B54
		internal SqlNullExpression(IQPExpressionInfo qpInfo)
			: this()
		{
			if (qpInfo.Expression.NodeAsNull == null)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			this.m_entityKeyTarget = qpInfo.Expression.GetResultType().EntityKeyTarget;
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00004555 File Offset: 0x00002755
		internal override bool CanGroupBy
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008994 File Offset: 0x00006B94
		protected override void InitValues()
		{
			if (this.m_entityKeyTarget != null)
			{
				int num = QueryPlanBuilder.GetEntityKeyPartTypes(this.m_entityKeyTarget).Length;
				for (int i = 0; i < num; i++)
				{
					base.Values.Add(SqlNullExpression.SqlNull);
				}
				return;
			}
			base.Values.Add(SqlExpression.NullSnippet);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000089E4 File Offset: 0x00006BE4
		private SqlNullExpression()
			: base(true)
		{
		}

		// Token: 0x040000CA RID: 202
		internal static readonly SqlNullExpression SqlNull = new SqlNullExpression();

		// Token: 0x040000CB RID: 203
		private readonly IQueryEntity m_entityKeyTarget;
	}
}
