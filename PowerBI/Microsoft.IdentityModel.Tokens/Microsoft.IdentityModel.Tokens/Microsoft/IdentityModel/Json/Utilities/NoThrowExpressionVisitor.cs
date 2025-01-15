using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Utilities
{
	// Token: 0x02000057 RID: 87
	[NullableContext(1)]
	[Nullable(0)]
	internal class NoThrowExpressionVisitor : ExpressionVisitor
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x0001571B File Offset: 0x0001391B
		protected override Expression VisitConditional(ConditionalExpression node)
		{
			if (node.IfFalse.NodeType == ExpressionType.Throw)
			{
				return Expression.Condition(node.Test, node.IfTrue, Expression.Constant(NoThrowExpressionVisitor.ErrorResult));
			}
			return base.VisitConditional(node);
		}

		// Token: 0x040001DD RID: 477
		internal static readonly object ErrorResult = new object();
	}
}
