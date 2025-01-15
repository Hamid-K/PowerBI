using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200003C RID: 60
	internal sealed class UnaryOperatorExpressionNode : ExpressionNode
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000778B File Offset: 0x0000598B
		internal UnaryOperatorExpressionNode(UnaryOperatorKind operatorKind, ExpressionNode operand)
		{
			this.m_operatorKind = operatorKind;
			this.m_operand = operand;
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600028C RID: 652 RVA: 0x000077A1 File Offset: 0x000059A1
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.UnaryOperator;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600028D RID: 653 RVA: 0x000077A5 File Offset: 0x000059A5
		public UnaryOperatorKind OperatorKind
		{
			get
			{
				return this.m_operatorKind;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600028E RID: 654 RVA: 0x000077AD File Offset: 0x000059AD
		public ExpressionNode Operand
		{
			get
			{
				return this.m_operand;
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000077B8 File Offset: 0x000059B8
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			UnaryOperatorExpressionNode unaryOperatorExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<UnaryOperatorExpressionNode>(this, other, out flag, out unaryOperatorExpressionNode))
			{
				return flag;
			}
			return this.OperatorKind == unaryOperatorExpressionNode.OperatorKind && this.Operand.Equals(unaryOperatorExpressionNode.Operand);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000077F8 File Offset: 0x000059F8
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.OperatorKind.GetHashCode(), this.Operand.GetHashCode());
		}

		// Token: 0x040000AF RID: 175
		private readonly UnaryOperatorKind m_operatorKind;

		// Token: 0x040000B0 RID: 176
		private readonly ExpressionNode m_operand;
	}
}
