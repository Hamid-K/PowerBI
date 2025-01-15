using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000DD RID: 221
	internal sealed class VisualCalculationExpressionNode : ExpressionNode
	{
		// Token: 0x06000622 RID: 1570 RVA: 0x0000D1F2 File Offset: 0x0000B3F2
		internal VisualCalculationExpressionNode(ExpressionNode expression)
		{
			this.Expression = expression;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0000D201 File Offset: 0x0000B401
		public ExpressionNode Expression { get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0000D209 File Offset: 0x0000B409
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.VisualCalculation;
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000D210 File Offset: 0x0000B410
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			VisualCalculationExpressionNode visualCalculationExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<VisualCalculationExpressionNode>(this, other, out flag, out visualCalculationExpressionNode))
			{
				return flag;
			}
			return this.Expression.Equals(visualCalculationExpressionNode.Expression);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000D23D File Offset: 0x0000B43D
		protected override int GetHashCodeImpl()
		{
			return this.Expression.GetHashCode();
		}

		// Token: 0x040002BD RID: 701
		internal const string ExpressionNodeIdentifier = "VisualCalculation";
	}
}
