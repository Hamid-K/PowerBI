using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000C3 RID: 195
	internal sealed class BinaryOperatorExpressionNode : ExpressionNode
	{
		// Token: 0x06000511 RID: 1297 RVA: 0x0000A9FD File Offset: 0x00008BFD
		internal BinaryOperatorExpressionNode(BinaryOperatorKind operatorKind, ExpressionNode left, ExpressionNode right)
		{
			this.OperatorKind = operatorKind;
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000AA1A File Offset: 0x00008C1A
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.BinaryOperator;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000AA1D File Offset: 0x00008C1D
		public BinaryOperatorKind OperatorKind { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000AA25 File Offset: 0x00008C25
		public ExpressionNode Left { get; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000AA2D File Offset: 0x00008C2D
		public ExpressionNode Right { get; }

		// Token: 0x06000516 RID: 1302 RVA: 0x0000AA38 File Offset: 0x00008C38
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			BinaryOperatorExpressionNode binaryOperatorExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<BinaryOperatorExpressionNode>(this, other, out flag, out binaryOperatorExpressionNode))
			{
				return flag;
			}
			return this.OperatorKind == binaryOperatorExpressionNode.OperatorKind && this.Left.Equals(binaryOperatorExpressionNode.Left) && this.Right.Equals(binaryOperatorExpressionNode.Right);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000AA88 File Offset: 0x00008C88
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.Left.GetHashCode(), this.OperatorKind.GetHashCode(), this.Right.GetHashCode());
		}
	}
}
