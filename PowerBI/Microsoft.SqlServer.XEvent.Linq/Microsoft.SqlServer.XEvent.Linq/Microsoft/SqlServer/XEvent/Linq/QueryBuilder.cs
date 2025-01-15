using System;
using System.Linq;
using System.Linq.Expressions;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000C2 RID: 194
	internal static class QueryBuilder
	{
		// Token: 0x06000249 RID: 585 RVA: 0x0001B018 File Offset: 0x0001B018
		internal static void CheckExpressionTypeSupported(Expression expression)
		{
			if (expression != null)
			{
				bool flag = false;
				ExpressionType nodeType = expression.NodeType;
				if (nodeType != ExpressionType.Call)
				{
					if (nodeType == ExpressionType.Constant)
					{
						flag = true;
					}
				}
				else
				{
					MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
					string name = methodCallExpression.Method.Name;
					if (name == "Select" && methodCallExpression.Type == typeof(IQueryable<PublishedEvent>))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					throw new NotSupportedException();
				}
			}
		}
	}
}
