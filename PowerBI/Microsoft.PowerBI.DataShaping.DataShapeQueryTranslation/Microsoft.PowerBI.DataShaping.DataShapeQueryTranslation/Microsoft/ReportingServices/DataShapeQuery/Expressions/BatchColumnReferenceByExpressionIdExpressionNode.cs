using System;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x02000013 RID: 19
	internal sealed class BatchColumnReferenceByExpressionIdExpressionNode : ExpressionNode
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00004347 File Offset: 0x00002547
		internal BatchColumnReferenceByExpressionIdExpressionNode(ExpressionId expressionId)
		{
			this.m_expressionId = expressionId;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004356 File Offset: 0x00002556
		public ExpressionId ExpressionId
		{
			get
			{
				return this.m_expressionId;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x0000435E File Offset: 0x0000255E
		public override ExpressionNodeKind Kind
		{
			get
			{
				return ExpressionNodeKind.BatchColumnReferenceByExpressionId;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004364 File Offset: 0x00002564
		public override bool Equals(ExpressionNode other)
		{
			bool flag;
			BatchColumnReferenceByExpressionIdExpressionNode batchColumnReferenceByExpressionIdExpressionNode;
			if (ExpressionNode.CheckReferenceAndTypeEquality<BatchColumnReferenceByExpressionIdExpressionNode>(this, other, out flag, out batchColumnReferenceByExpressionIdExpressionNode))
			{
				return flag;
			}
			return this.m_expressionId.Equals(batchColumnReferenceByExpressionIdExpressionNode.m_expressionId);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004394 File Offset: 0x00002594
		protected override int GetHashCodeImpl()
		{
			return Hashing.GetHashCode<ExpressionId>(this.m_expressionId, null);
		}

		// Token: 0x04000041 RID: 65
		private readonly ExpressionId m_expressionId;
	}
}
