using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200003A RID: 58
	internal sealed class SubExpressionNode : ExpressionNode
	{
		// Token: 0x0600027F RID: 639 RVA: 0x00007671 File Offset: 0x00005871
		internal SubExpressionNode(ExpressionId expressionId)
		{
			this.m_expressionId = expressionId;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000280 RID: 640 RVA: 0x00007680 File Offset: 0x00005880
		public ExpressionId ExpressionId
		{
			get
			{
				return this.m_expressionId;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000281 RID: 641 RVA: 0x00007688 File Offset: 0x00005888
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.SubExpression;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000768C File Offset: 0x0000588C
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			SubExpressionNode subExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<SubExpressionNode>(this, other, out flag, out subExpressionNode))
			{
				return flag;
			}
			return this.ExpressionId == subExpressionNode.ExpressionId;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000076BC File Offset: 0x000058BC
		protected override int GetHashCodeImpl()
		{
			return this.ExpressionId.GetHashCode();
		}

		// Token: 0x040000AB RID: 171
		private readonly ExpressionId m_expressionId;
	}
}
