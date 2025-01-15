using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000039 RID: 57
	internal sealed class SingleValueExpressionNode : ExpressionNode
	{
		// Token: 0x0600027A RID: 634 RVA: 0x000075F2 File Offset: 0x000057F2
		internal SingleValueExpressionNode(ExpressionNode input)
		{
			this.m_input = input;
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00007601 File Offset: 0x00005801
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.SingleValue;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600027C RID: 636 RVA: 0x00007605 File Offset: 0x00005805
		public ExpressionNode Input
		{
			get
			{
				return this.m_input;
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007610 File Offset: 0x00005810
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			SingleValueExpressionNode singleValueExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<SingleValueExpressionNode>(this, other, out flag, out singleValueExpressionNode))
			{
				return flag;
			}
			return this.Input.Equals(singleValueExpressionNode.Input);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00007640 File Offset: 0x00005840
		protected override int GetHashCodeImpl()
		{
			return Hashing.CombineHash(this.Kind.GetHashCode(), this.Input.GetHashCode());
		}

		// Token: 0x040000AA RID: 170
		private readonly ExpressionNode m_input;
	}
}
