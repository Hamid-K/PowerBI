using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQuery.Expressions
{
	// Token: 0x0200002F RID: 47
	internal abstract class ResolvedGroupElementReferenceExpressionNode : ResolvedStructureReferenceExpressionNode
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000244 RID: 580
		protected abstract Expression ReferencedExpression { get; }

		// Token: 0x06000245 RID: 581 RVA: 0x0000725C File Offset: 0x0000545C
		public override bool Equals(ExpressionNode other)
		{
			ResolvedGroupElementReferenceExpressionNode resolvedGroupElementReferenceExpressionNode = other as ResolvedGroupElementReferenceExpressionNode;
			return resolvedGroupElementReferenceExpressionNode != null && this.Equals(this.ReferencedExpression, resolvedGroupElementReferenceExpressionNode.ReferencedExpression);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007287 File Offset: 0x00005487
		protected override int GetHashCodeImpl()
		{
			return ExpressionComparerByOriginalNode.Instance.GetHashCode(this.ReferencedExpression);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00007299 File Offset: 0x00005499
		private bool Equals(Expression left, Expression right)
		{
			return ExpressionComparerByOriginalNode.Instance.Equals(left, right);
		}
	}
}
