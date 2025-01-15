using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Resources;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Utilities
{
	// Token: 0x0200007E RID: 126
	internal static class ExpressionExtensions
	{
		// Token: 0x06000447 RID: 1095 RVA: 0x0000FD64 File Offset: 0x0000DF64
		public static PropertyPath GetSimplePropertyAccess(this LambdaExpression propertyAccessExpression)
		{
			PropertyPath propertyPath = propertyAccessExpression.Parameters.Single<ParameterExpression>().MatchSimplePropertyAccess(propertyAccessExpression.Body);
			if (propertyPath == null)
			{
				throw Error.InvalidPropertyExpression(propertyAccessExpression);
			}
			return propertyPath;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000FD8C File Offset: 0x0000DF8C
		public static PropertyPath GetComplexPropertyAccess(this LambdaExpression propertyAccessExpression)
		{
			PropertyPath propertyPath = propertyAccessExpression.Parameters.Single<ParameterExpression>().MatchComplexPropertyAccess(propertyAccessExpression.Body);
			if (propertyPath == null)
			{
				throw Error.InvalidComplexPropertyExpression(propertyAccessExpression);
			}
			return propertyPath;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		public static IEnumerable<PropertyPath> GetSimplePropertyAccessList(this LambdaExpression propertyAccessExpression)
		{
			IEnumerable<PropertyPath> enumerable = propertyAccessExpression.MatchPropertyAccessList((Expression p, Expression e) => e.MatchSimplePropertyAccess(p));
			if (enumerable == null)
			{
				throw Error.InvalidPropertiesExpression(propertyAccessExpression);
			}
			return enumerable;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000FDE5 File Offset: 0x0000DFE5
		public static IEnumerable<PropertyPath> GetComplexPropertyAccessList(this LambdaExpression propertyAccessExpression)
		{
			IEnumerable<PropertyPath> enumerable = propertyAccessExpression.MatchPropertyAccessList((Expression p, Expression e) => e.MatchComplexPropertyAccess(p));
			if (enumerable == null)
			{
				throw Error.InvalidComplexPropertiesExpression(propertyAccessExpression);
			}
			return enumerable;
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000FE18 File Offset: 0x0000E018
		private static IEnumerable<PropertyPath> MatchPropertyAccessList(this LambdaExpression lambdaExpression, Func<Expression, Expression, PropertyPath> propertyMatcher)
		{
			NewExpression newExpression = lambdaExpression.Body.RemoveConvert() as NewExpression;
			if (newExpression != null)
			{
				ParameterExpression parameterExpression = lambdaExpression.Parameters.Single<ParameterExpression>();
				IEnumerable<PropertyPath> enumerable = from a in newExpression.Arguments
					select propertyMatcher(a, parameterExpression) into p
					where p != null
					select p;
				if (enumerable.Count<PropertyPath>() == newExpression.Arguments.Count<Expression>())
				{
					if (!newExpression.HasDefaultMembersOnly(enumerable))
					{
						return null;
					}
					return enumerable;
				}
			}
			PropertyPath propertyPath = propertyMatcher(lambdaExpression.Body, lambdaExpression.Parameters.Single<ParameterExpression>());
			if (!(propertyPath != null))
			{
				return null;
			}
			return new PropertyPath[] { propertyPath };
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		private static bool HasDefaultMembersOnly(this NewExpression newExpression, IEnumerable<PropertyPath> propertyPaths)
		{
			return newExpression.Members == null || !newExpression.Members.Where((MemberInfo t, int i) => !string.Equals(t.Name, propertyPaths.ElementAt(i).Last<PropertyInfo>().Name, StringComparison.Ordinal)).Any<MemberInfo>();
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000FF2C File Offset: 0x0000E12C
		private static PropertyPath MatchSimplePropertyAccess(this Expression parameterExpression, Expression propertyAccessExpression)
		{
			PropertyPath propertyPath = parameterExpression.MatchPropertyAccess(propertyAccessExpression);
			if (!(propertyPath != null) || propertyPath.Count != 1)
			{
				return null;
			}
			return propertyPath;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000FF56 File Offset: 0x0000E156
		private static PropertyPath MatchComplexPropertyAccess(this Expression parameterExpression, Expression propertyAccessExpression)
		{
			return parameterExpression.MatchPropertyAccess(propertyAccessExpression);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000FF60 File Offset: 0x0000E160
		private static PropertyPath MatchPropertyAccess(this Expression parameterExpression, Expression propertyAccessExpression)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();
			for (;;)
			{
				MemberExpression memberExpression = propertyAccessExpression.RemoveConvert() as MemberExpression;
				if (memberExpression == null)
				{
					break;
				}
				PropertyInfo propertyInfo = memberExpression.Member as PropertyInfo;
				if (propertyInfo == null)
				{
					goto Block_2;
				}
				list.Insert(0, propertyInfo);
				propertyAccessExpression = memberExpression.Expression;
				if (memberExpression.Expression == parameterExpression)
				{
					goto Block_3;
				}
			}
			return null;
			Block_2:
			return null;
			Block_3:
			return new PropertyPath(list);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000FFBA File Offset: 0x0000E1BA
		public static Expression RemoveConvert(this Expression expression)
		{
			while (expression.NodeType == ExpressionType.Convert || expression.NodeType == ExpressionType.ConvertChecked)
			{
				expression = ((UnaryExpression)expression).Operand;
			}
			return expression;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000FFE0 File Offset: 0x0000E1E0
		public static bool IsNullConstant(this Expression expression)
		{
			expression = expression.RemoveConvert();
			return expression.NodeType == ExpressionType.Constant && ((ConstantExpression)expression).Value == null;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00010004 File Offset: 0x0000E204
		public static bool IsStringAddExpression(this Expression expression)
		{
			BinaryExpression binaryExpression = expression as BinaryExpression;
			return binaryExpression != null && !(binaryExpression.Method == null) && binaryExpression.NodeType == ExpressionType.Add && binaryExpression.Method.DeclaringType == typeof(string) && string.Equals(binaryExpression.Method.Name, "Concat", StringComparison.Ordinal);
		}
	}
}
