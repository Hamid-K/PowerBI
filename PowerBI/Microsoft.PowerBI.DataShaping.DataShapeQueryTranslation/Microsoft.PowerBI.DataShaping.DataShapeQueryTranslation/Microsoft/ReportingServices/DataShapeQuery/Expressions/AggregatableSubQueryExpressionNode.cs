using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000011 RID: 17
	internal sealed class AggregatableSubQueryExpressionNode : ExpressionNode
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x000041F6 File Offset: 0x000023F6
		internal AggregatableSubQueryExpressionNode(ExpressionId targetExpressionId, DataSetPlan dataSetPlan, PlanOperation table = null)
		{
			this.m_targetExpressionId = targetExpressionId;
			this.m_dataSetPlan = dataSetPlan;
			this.m_table = table;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004213 File Offset: 0x00002413
		public ExpressionId TargetExpressionId
		{
			get
			{
				return this.m_targetExpressionId;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000421B File Offset: 0x0000241B
		public DataSetPlan DataSetPlan
		{
			get
			{
				return this.m_dataSetPlan;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004223 File Offset: 0x00002423
		public PlanOperation Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000422B File Offset: 0x0000242B
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.AggregatableSubQueryExpression;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004230 File Offset: 0x00002430
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			AggregatableSubQueryExpressionNode aggregatableSubQueryExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<AggregatableSubQueryExpressionNode>(this, other, out flag, out aggregatableSubQueryExpressionNode))
			{
				return flag;
			}
			return this.TargetExpressionId == aggregatableSubQueryExpressionNode.TargetExpressionId && this.DataSetPlan == aggregatableSubQueryExpressionNode.DataSetPlan && this.Table == aggregatableSubQueryExpressionNode.Table;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004280 File Offset: 0x00002480
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.TargetExpressionId.GetHashCode(), Hashing.GetHashCode<DataSetPlan>(this.DataSetPlan, null), Hashing.GetHashCode<PlanOperation>(this.Table, null));
		}

		// Token: 0x0400003C RID: 60
		private readonly ExpressionId m_targetExpressionId;

		// Token: 0x0400003D RID: 61
		private readonly DataSetPlan m_dataSetPlan;

		// Token: 0x0400003E RID: 62
		private readonly PlanOperation m_table;
	}
}
