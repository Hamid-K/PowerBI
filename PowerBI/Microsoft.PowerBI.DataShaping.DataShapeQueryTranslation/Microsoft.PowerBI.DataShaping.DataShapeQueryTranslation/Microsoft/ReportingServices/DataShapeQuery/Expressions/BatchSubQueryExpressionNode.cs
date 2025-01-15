using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000017 RID: 23
	internal sealed class BatchSubQueryExpressionNode : ExpressionNode
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x000044D6 File Offset: 0x000026D6
		internal BatchSubQueryExpressionNode(PlanOperation table)
		{
			this.m_table = table;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000044E5 File Offset: 0x000026E5
		public PlanOperation Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000044ED File Offset: 0x000026ED
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.BatchSubQuery;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000044F0 File Offset: 0x000026F0
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			BatchSubQueryExpressionNode batchSubQueryExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<BatchSubQueryExpressionNode>(this, other, out flag, out batchSubQueryExpressionNode))
			{
				return flag;
			}
			return this.Table == batchSubQueryExpressionNode.Table;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000451A File Offset: 0x0000271A
		protected override int GetHashCodeImpl()
		{
			return this.m_table.GetHashCode();
		}

		// Token: 0x04000046 RID: 70
		private readonly PlanOperation m_table;
	}
}
