using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000204 RID: 516
	public static class ExtendedExpression
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0003010C File Offset: 0x0002E30C
		[Pure]
		public static Expression<Func<T, bool>> Or<T>([InstantHandle] IEnumerable<Expression<Func<T, bool>>> expressions)
		{
			Expression<Func<T, bool>> expression = ExtendedExpression.False<T>();
			foreach (Expression<Func<T, bool>> expression2 in expressions)
			{
				expression = expression.Or(expression2);
			}
			return expression;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0003015C File Offset: 0x0002E35C
		[Pure]
		public static Expression<Func<T, bool>> And<T>([InstantHandle] IEnumerable<Expression<Func<T, bool>>> expressions)
		{
			Expression<Func<T, bool>> expression = ExtendedExpression.True<T>();
			foreach (Expression<Func<T, bool>> expression2 in expressions)
			{
				expression = expression.And(expression2);
			}
			return expression;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000301AC File Offset: 0x0002E3AC
		[Pure]
		private static Expression<Func<T, bool>> True<T>()
		{
			return (T f) => true;
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000301F4 File Offset: 0x0002E3F4
		[Pure]
		private static Expression<Func<T, bool>> False<T>()
		{
			return (T f) => false;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0003023C File Offset: 0x0002E43C
		[Pure]
		private static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
		{
			InvocationExpression invocationExpression = Expression.Invoke(exp2, exp1.Parameters.Cast<Expression>());
			return Expression.Lambda<Func<T, bool>>(Expression.OrElse(exp1.Body, invocationExpression), exp1.Parameters);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00030274 File Offset: 0x0002E474
		[Pure]
		private static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp1, Expression<Func<T, bool>> exp2)
		{
			InvocationExpression invocationExpression = Expression.Invoke(exp2, exp1.Parameters.Cast<Expression>());
			return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(exp1.Body, invocationExpression), exp1.Parameters);
		}
	}
}
