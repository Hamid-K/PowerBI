using System;
using System.Linq.Expressions;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000056 RID: 86
	internal class NoThrowExpressionVisitor : ExpressionVisitor
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x000151CF File Offset: 0x000133CF
		protected override Expression VisitConditional(ConditionalExpression node)
		{
			if (node.IfFalse.NodeType == ExpressionType.Throw)
			{
				return Expression.Condition(node.Test, node.IfTrue, Expression.Constant(NoThrowExpressionVisitor.ErrorResult));
			}
			return base.VisitConditional(node);
		}

		// Token: 0x040001C2 RID: 450
		internal static readonly object ErrorResult = new object();
	}
}
