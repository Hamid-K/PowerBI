using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning.PlanOperations;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000015 RID: 21
	internal sealed class BatchFilterInlinedDeclarationCalculationExpressionNode : ExpressionNode
	{
		// Token: 0x060000DA RID: 218 RVA: 0x000043F6 File Offset: 0x000025F6
		internal BatchFilterInlinedDeclarationCalculationExpressionNode(ExpressionNode expression, PlanOperationDeclarationReference filterDeclaration)
		{
			this.m_expression = expression;
			this.m_filterDeclaration = filterDeclaration;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000440C File Offset: 0x0000260C
		public ExpressionNode ExpressionNode
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004414 File Offset: 0x00002614
		public PlanOperationDeclarationReference FilterDeclaration
		{
			get
			{
				return this.m_filterDeclaration;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000441C File Offset: 0x0000261C
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.BatchFilterInlinedDeclarationCalculation;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004420 File Offset: 0x00002620
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			BatchFilterInlinedDeclarationCalculationExpressionNode batchFilterInlinedDeclarationCalculationExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<BatchFilterInlinedDeclarationCalculationExpressionNode>(this, other, out flag, out batchFilterInlinedDeclarationCalculationExpressionNode))
			{
				return flag;
			}
			return this.ExpressionNode.Equals(batchFilterInlinedDeclarationCalculationExpressionNode.ExpressionNode) && this.FilterDeclaration.Equals(batchFilterInlinedDeclarationCalculationExpressionNode.FilterDeclaration);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004462 File Offset: 0x00002662
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.ExpressionNode.GetHashCode(), this.FilterDeclaration.GetHashCode());
		}

		// Token: 0x04000043 RID: 67
		private readonly ExpressionNode m_expression;

		// Token: 0x04000044 RID: 68
		private readonly PlanOperationDeclarationReference m_filterDeclaration;
	}
}
