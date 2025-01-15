using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNet.OData.Query.Expressions;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200004F RID: 79
	internal static class ExpressionHelpers
	{
		// Token: 0x06000208 RID: 520 RVA: 0x00009D78 File Offset: 0x00007F78
		public static Func<long> Count(IQueryable query, Type type)
		{
			MethodInfo countMethod = ExpressionHelperMethods.QueryableCountGeneric.MakeGenericMethod(new Type[] { type });
			return () => (long)countMethod.Invoke(null, new object[] { query });
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00009DB8 File Offset: 0x00007FB8
		public static IQueryable Skip(IQueryable query, int count, Type type, bool parameterize)
		{
			MethodInfo methodInfo = ExpressionHelperMethods.QueryableSkipGeneric.MakeGenericMethod(new Type[] { type });
			Expression expression = (parameterize ? LinqParameterContainer.Parameterize(typeof(int), count) : Expression.Constant(count));
			Expression expression2 = Expression.Call(null, methodInfo, new Expression[] { query.Expression, expression });
			MethodBase methodBase = ExpressionHelperMethods.CreateQueryGeneric.MakeGenericMethod(new Type[] { type });
			object provider = query.Provider;
			object[] array = new Expression[] { expression2 };
			return methodBase.Invoke(provider, array) as IQueryable;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00009E4C File Offset: 0x0000804C
		public static IQueryable Take(IQueryable query, int count, Type type, bool parameterize)
		{
			Expression expression = ExpressionHelpers.Take(query.Expression, count, type, parameterize);
			MethodBase methodBase = ExpressionHelperMethods.CreateQueryGeneric.MakeGenericMethod(new Type[] { type });
			object provider = query.Provider;
			object[] array = new Expression[] { expression };
			return methodBase.Invoke(provider, array) as IQueryable;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00009E98 File Offset: 0x00008098
		public static Expression Skip(Expression source, int count, Type type, bool parameterize)
		{
			MethodInfo methodInfo;
			if (typeof(IQueryable).IsAssignableFrom(source.Type))
			{
				methodInfo = ExpressionHelperMethods.QueryableSkipGeneric.MakeGenericMethod(new Type[] { type });
			}
			else
			{
				methodInfo = ExpressionHelperMethods.EnumerableSkipGeneric.MakeGenericMethod(new Type[] { type });
			}
			Expression expression = (parameterize ? LinqParameterContainer.Parameterize(typeof(int), count) : Expression.Constant(count));
			return Expression.Call(null, methodInfo, new Expression[] { source, expression });
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00009F24 File Offset: 0x00008124
		public static Expression Take(Expression source, int count, Type elementType, bool parameterize)
		{
			MethodInfo methodInfo;
			if (typeof(IQueryable).IsAssignableFrom(source.Type))
			{
				methodInfo = ExpressionHelperMethods.QueryableTakeGeneric.MakeGenericMethod(new Type[] { elementType });
			}
			else
			{
				methodInfo = ExpressionHelperMethods.EnumerableTakeGeneric.MakeGenericMethod(new Type[] { elementType });
			}
			Expression expression = (parameterize ? LinqParameterContainer.Parameterize(typeof(int), count) : Expression.Constant(count));
			return Expression.Call(null, methodInfo, new Expression[] { source, expression });
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00009FB0 File Offset: 0x000081B0
		public static Expression OrderByPropertyExpression(Expression source, string propertyName, Type elementType, bool alreadyOrdered = false)
		{
			LambdaExpression propertyAccessLambda = ExpressionHelpers.GetPropertyAccessLambda(elementType, propertyName);
			return ExpressionHelpers.OrderBy(source, propertyAccessLambda, elementType, OrderByDirection.Ascending, alreadyOrdered);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00009FD0 File Offset: 0x000081D0
		public static Expression OrderBy(Expression source, LambdaExpression orderByLambda, Type elementType, OrderByDirection direction, bool alreadyOrdered = false)
		{
			Type type = orderByLambda.Body.Type;
			MethodInfo methodInfo;
			if (!alreadyOrdered)
			{
				if (typeof(IQueryable).IsAssignableFrom(source.Type))
				{
					if (direction == OrderByDirection.Ascending)
					{
						methodInfo = ExpressionHelperMethods.QueryableOrderByGeneric.MakeGenericMethod(new Type[] { elementType, type });
					}
					else
					{
						methodInfo = ExpressionHelperMethods.QueryableOrderByDescendingGeneric.MakeGenericMethod(new Type[] { elementType, type });
					}
				}
				else if (direction == OrderByDirection.Ascending)
				{
					methodInfo = ExpressionHelperMethods.EnumerableOrderByGeneric.MakeGenericMethod(new Type[] { elementType, type });
				}
				else
				{
					methodInfo = ExpressionHelperMethods.EnumerableOrderByDescendingGeneric.MakeGenericMethod(new Type[] { elementType, type });
				}
			}
			else if (typeof(IQueryable).IsAssignableFrom(source.Type))
			{
				if (direction == OrderByDirection.Ascending)
				{
					methodInfo = ExpressionHelperMethods.QueryableThenByGeneric.MakeGenericMethod(new Type[] { elementType, type });
				}
				else
				{
					methodInfo = ExpressionHelperMethods.QueryableThenByDescendingGeneric.MakeGenericMethod(new Type[] { elementType, type });
				}
			}
			else if (direction == OrderByDirection.Ascending)
			{
				methodInfo = ExpressionHelperMethods.EnumerableThenByGeneric.MakeGenericMethod(new Type[] { elementType, type });
			}
			else
			{
				methodInfo = ExpressionHelperMethods.EnumerableThenByDescendingGeneric.MakeGenericMethod(new Type[] { elementType, type });
			}
			return Expression.Call(null, methodInfo, new Expression[] { source, orderByLambda });
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000A124 File Offset: 0x00008324
		public static IQueryable OrderByIt(IQueryable query, OrderByDirection direction, Type type, bool alreadyOrdered = false)
		{
			ParameterExpression parameterExpression = Expression.Parameter(type, "$it");
			LambdaExpression lambdaExpression = Expression.Lambda(parameterExpression, new ParameterExpression[] { parameterExpression });
			return ExpressionHelpers.OrderBy(query, lambdaExpression, direction, type, alreadyOrdered);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000A158 File Offset: 0x00008358
		public static IQueryable OrderByProperty(IQueryable query, IEdmModel model, IEdmProperty property, OrderByDirection direction, Type type, bool alreadyOrdered = false)
		{
			string clrPropertyName = EdmLibHelpers.GetClrPropertyName(property, model);
			LambdaExpression propertyAccessLambda = ExpressionHelpers.GetPropertyAccessLambda(type, clrPropertyName);
			return ExpressionHelpers.OrderBy(query, propertyAccessLambda, direction, type, alreadyOrdered);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000A184 File Offset: 0x00008384
		public static IQueryable OrderBy(IQueryable query, LambdaExpression orderByLambda, OrderByDirection direction, Type type, bool alreadyOrdered = false)
		{
			Type type2 = orderByLambda.Body.Type;
			IOrderedQueryable orderedQueryable;
			if (alreadyOrdered)
			{
				MethodInfo methodInfo;
				if (direction == OrderByDirection.Ascending)
				{
					methodInfo = ExpressionHelperMethods.QueryableThenByGeneric.MakeGenericMethod(new Type[] { type, type2 });
				}
				else
				{
					methodInfo = ExpressionHelperMethods.QueryableThenByDescendingGeneric.MakeGenericMethod(new Type[] { type, type2 });
				}
				orderedQueryable = query as IOrderedQueryable;
				orderedQueryable = methodInfo.Invoke(null, new object[] { orderedQueryable, orderByLambda }) as IOrderedQueryable;
			}
			else
			{
				MethodInfo methodInfo;
				if (direction == OrderByDirection.Ascending)
				{
					methodInfo = ExpressionHelperMethods.QueryableOrderByGeneric.MakeGenericMethod(new Type[] { type, type2 });
				}
				else
				{
					methodInfo = ExpressionHelperMethods.QueryableOrderByDescendingGeneric.MakeGenericMethod(new Type[] { type, type2 });
				}
				orderedQueryable = methodInfo.Invoke(null, new object[] { query, orderByLambda }) as IOrderedQueryable;
			}
			return orderedQueryable;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000A253 File Offset: 0x00008453
		public static IQueryable GroupBy(IQueryable query, Expression expression, Type type, Type wrapperType)
		{
			return ExpressionHelperMethods.QueryableGroupByGeneric.MakeGenericMethod(new Type[] { type, wrapperType }).Invoke(null, new object[] { query, expression }) as IQueryable;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000A286 File Offset: 0x00008486
		public static IQueryable Select(IQueryable query, LambdaExpression expression, Type type)
		{
			return ExpressionHelperMethods.QueryableSelectGeneric.MakeGenericMethod(new Type[]
			{
				type,
				expression.Body.Type
			}).Invoke(null, new object[] { query, expression }) as IQueryable;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000A2C3 File Offset: 0x000084C3
		public static IQueryable SelectMany(IQueryable query, LambdaExpression expression, Type type)
		{
			return ExpressionHelperMethods.QueryableSelectManyGeneric.MakeGenericMethod(new Type[]
			{
				type,
				expression.Body.Type
			}).Invoke(null, new object[] { query, expression }) as IQueryable;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000A300 File Offset: 0x00008500
		public static IQueryable Aggregate(IQueryable query, object init, LambdaExpression sumLambda, Type type, Type wrapperType)
		{
			Type type2 = sumLambda.Body.Type;
			object obj = ExpressionHelperMethods.QueryableAggregateGeneric.MakeGenericMethod(new Type[] { type, type2 }).Invoke(null, new object[] { query, init, sumLambda });
			return ExpressionHelperMethods.EntityAsQueryable.MakeGenericMethod(new Type[] { wrapperType }).Invoke(null, new object[] { obj }) as IQueryable;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000A374 File Offset: 0x00008574
		public static IQueryable Where(IQueryable query, Expression where, Type type)
		{
			return ExpressionHelperMethods.QueryableWhereGeneric.MakeGenericMethod(new Type[] { type }).Invoke(null, new object[] { query, where }) as IQueryable;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000A3A3 File Offset: 0x000085A3
		public static Expression ToNullable(Expression expression)
		{
			if (!TypeHelper.IsNullable(expression.Type))
			{
				return Expression.Convert(expression, TypeHelper.ToNullable(expression.Type));
			}
			return expression;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000A3C5 File Offset: 0x000085C5
		public static Expression Default(Type type)
		{
			if (TypeHelper.IsValueType(type))
			{
				return Expression.Constant(Activator.CreateInstance(type), type);
			}
			return Expression.Constant(null, type);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000A3E4 File Offset: 0x000085E4
		public static LambdaExpression GetPropertyAccessLambda(Type type, string propertyName)
		{
			ParameterExpression parameterExpression = Expression.Parameter(type, "$it");
			return Expression.Lambda(Expression.Property(parameterExpression, propertyName), new ParameterExpression[] { parameterExpression });
		}
	}
}
