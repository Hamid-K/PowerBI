using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200002C RID: 44
	internal sealed class ResolvedCalculationReferenceExpressionNode : ResolvedStructureReferenceExpressionNode
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000711B File Offset: 0x0000531B
		internal ResolvedCalculationReferenceExpressionNode(Calculation calculation)
		{
			this.m_calculation = calculation;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000712A File Offset: 0x0000532A
		public override IIdentifiable Target
		{
			get
			{
				return this.m_calculation;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00007132 File Offset: 0x00005332
		public Calculation Calculation
		{
			get
			{
				return this.m_calculation;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000235 RID: 565 RVA: 0x0000713A File Offset: 0x0000533A
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.ResolvedCalculationReference;
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007140 File Offset: 0x00005340
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<ResolvedCalculationReferenceExpressionNode>(this, other, out flag, out resolvedCalculationReferenceExpressionNode))
			{
				return flag;
			}
			return this.Calculation == resolvedCalculationReferenceExpressionNode.Calculation;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000716A File Offset: 0x0000536A
		protected override int GetHashCodeImpl()
		{
			return this.Calculation.GetHashCode();
		}

		// Token: 0x0400009E RID: 158
		private readonly Calculation m_calculation;
	}
}
