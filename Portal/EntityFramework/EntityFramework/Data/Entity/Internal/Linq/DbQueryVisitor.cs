using System;
using System.Collections.Concurrent;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x0200013D RID: 317
	internal class DbQueryVisitor : ExpressionVisitor
	{
		// Token: 0x060014F6 RID: 5366 RVA: 0x00037050 File Offset: 0x00035250
		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			Check.NotNull<MethodCallExpression>(node, "node");
			if (typeof(DbContext).IsAssignableFrom(node.Method.DeclaringType))
			{
				DbContext dbContext = null;
				MemberExpression memberExpression = node.Object as MemberExpression;
				if (memberExpression != null)
				{
					dbContext = DbQueryVisitor.GetContextFromConstantExpression(memberExpression.Expression, memberExpression.Member);
				}
				else
				{
					ConstantExpression constantExpression = node.Object as ConstantExpression;
					if (constantExpression != null)
					{
						dbContext = constantExpression.Value as DbContext;
					}
				}
				if (dbContext != null && !node.Method.GetCustomAttributes(false).Any<DbFunctionAttribute>() && node.Method.GetParameters().Length == 0)
				{
					Expression expression = DbQueryVisitor.CreateObjectQueryConstant(node.Method.Invoke(dbContext, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null));
					if (expression != null)
					{
						return expression;
					}
				}
			}
			return base.VisitMethodCall(node);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00037110 File Offset: 0x00035310
		protected override Expression VisitMember(MemberExpression node)
		{
			Check.NotNull<MemberExpression>(node, "node");
			PropertyInfo propertyInfo = node.Member as PropertyInfo;
			MemberExpression memberExpression = node.Expression as MemberExpression;
			if (propertyInfo != null && memberExpression != null && typeof(IQueryable).IsAssignableFrom(propertyInfo.PropertyType) && typeof(DbContext).IsAssignableFrom(node.Member.DeclaringType))
			{
				DbContext contextFromConstantExpression = DbQueryVisitor.GetContextFromConstantExpression(memberExpression.Expression, memberExpression.Member);
				if (contextFromConstantExpression != null)
				{
					Expression expression = DbQueryVisitor.CreateObjectQueryConstant(propertyInfo.GetValue(contextFromConstantExpression, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null));
					if (expression != null)
					{
						return expression;
					}
				}
			}
			return base.VisitMember(node);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x000371B4 File Offset: 0x000353B4
		private static DbContext GetContextFromConstantExpression(Expression expression, MemberInfo member)
		{
			if (expression == null)
			{
				return DbQueryVisitor.GetContextFromMember(member, null);
			}
			object expressionValue = DbQueryVisitor.GetExpressionValue(expression);
			if (expressionValue != null)
			{
				return DbQueryVisitor.GetContextFromMember(member, expressionValue);
			}
			return null;
		}

		// Token: 0x060014F9 RID: 5369 RVA: 0x000371E0 File Offset: 0x000353E0
		private static object GetExpressionValue(Expression expression)
		{
			ConstantExpression constantExpression = expression as ConstantExpression;
			if (constantExpression != null)
			{
				return constantExpression.Value;
			}
			MemberExpression memberExpression = expression as MemberExpression;
			if (memberExpression != null)
			{
				FieldInfo fieldInfo = memberExpression.Member as FieldInfo;
				if (fieldInfo != null)
				{
					object expressionValue = DbQueryVisitor.GetExpressionValue(memberExpression.Expression);
					if (expressionValue != null)
					{
						return fieldInfo.GetValue(expressionValue);
					}
				}
				PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
				if (propertyInfo != null)
				{
					object expressionValue2 = DbQueryVisitor.GetExpressionValue(memberExpression.Expression);
					if (expressionValue2 != null)
					{
						return propertyInfo.GetValue(expressionValue2, null);
					}
				}
			}
			return null;
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00037268 File Offset: 0x00035468
		private static DbContext GetContextFromMember(MemberInfo member, object value)
		{
			FieldInfo fieldInfo = member as FieldInfo;
			if (fieldInfo != null)
			{
				return fieldInfo.GetValue(value) as DbContext;
			}
			PropertyInfo propertyInfo = member as PropertyInfo;
			if (propertyInfo != null)
			{
				return propertyInfo.GetValue(value, null) as DbContext;
			}
			return null;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x000372B4 File Offset: 0x000354B4
		private static Expression CreateObjectQueryConstant(object dbQuery)
		{
			ObjectQuery objectQuery = DbQueryVisitor.ExtractObjectQuery(dbQuery);
			if (objectQuery != null)
			{
				Type type = objectQuery.GetType().GetGenericArguments().Single<Type>();
				Func<ObjectQuery, object> func;
				if (!DbQueryVisitor._wrapperFactories.TryGetValue(type, out func))
				{
					MethodInfo declaredMethod = typeof(ReplacementDbQueryWrapper<>).MakeGenericType(new Type[] { type }).GetDeclaredMethod("Create", new Type[] { typeof(ObjectQuery) });
					func = (Func<ObjectQuery, object>)Delegate.CreateDelegate(typeof(Func<ObjectQuery, object>), declaredMethod);
					DbQueryVisitor._wrapperFactories.TryAdd(type, func);
				}
				object obj = func(objectQuery);
				return Expression.Property(Expression.Constant(obj, obj.GetType()), "Query");
			}
			return null;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00037368 File Offset: 0x00035568
		private static ObjectQuery ExtractObjectQuery(object dbQuery)
		{
			IInternalQueryAdapter internalQueryAdapter = dbQuery as IInternalQueryAdapter;
			if (internalQueryAdapter != null)
			{
				return internalQueryAdapter.InternalQuery.ObjectQuery;
			}
			return null;
		}

		// Token: 0x040009CA RID: 2506
		private const BindingFlags SetAccessBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x040009CB RID: 2507
		private static readonly ConcurrentDictionary<Type, Func<ObjectQuery, object>> _wrapperFactories = new ConcurrentDictionary<Type, Func<ObjectQuery, object>>();
	}
}
