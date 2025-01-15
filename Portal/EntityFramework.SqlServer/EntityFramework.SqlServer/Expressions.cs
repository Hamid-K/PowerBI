using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.SqlServer
{
	// Token: 0x02000006 RID: 6
	internal static class Expressions
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000024A5 File Offset: 0x000006A5
		internal static Expression Null<TNullType>()
		{
			return Expression.Constant(null, typeof(TNullType));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024B7 File Offset: 0x000006B7
		internal static Expression Null(Type nullType)
		{
			return Expression.Constant(null, nullType);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024C0 File Offset: 0x000006C0
		internal static Expression<Func<TArg, TResult>> Lambda<TArg, TResult>(string argumentName, Func<ParameterExpression, Expression> createLambdaBodyGivenParameter)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(TArg), argumentName);
			return Expression.Lambda<Func<TArg, TResult>>(createLambdaBodyGivenParameter(parameterExpression), new ParameterExpression[] { parameterExpression });
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024F4 File Offset: 0x000006F4
		internal static Expression Call(this Expression exp, string methodName)
		{
			return Expression.Call(exp, methodName, Type.EmptyTypes, new Expression[0]);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002508 File Offset: 0x00000708
		internal static Expression ConvertTo(this Expression exp, Type convertToType)
		{
			return Expression.Convert(exp, convertToType);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002511 File Offset: 0x00000711
		internal static Expression ConvertTo<TConvertToType>(this Expression exp)
		{
			return Expression.Convert(exp, typeof(TConvertToType));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002523 File Offset: 0x00000723
		internal static Expressions.ConditionalExpressionBuilder IfTrueThen(this Expression conditionExp, Expression resultIfTrue)
		{
			return new Expressions.ConditionalExpressionBuilder(conditionExp, resultIfTrue);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000252C File Offset: 0x0000072C
		internal static Expression Property<TPropertyType>(this Expression exp, string propertyName)
		{
			PropertyInfo runtimeProperty = exp.Type.GetRuntimeProperty(propertyName);
			return Expression.Property(exp, runtimeProperty);
		}

		// Token: 0x02000048 RID: 72
		internal sealed class ConditionalExpressionBuilder
		{
			// Token: 0x06000614 RID: 1556 RVA: 0x0001A876 File Offset: 0x00018A76
			internal ConditionalExpressionBuilder(Expression conditionExpression, Expression ifTrueExpression)
			{
				this.condition = conditionExpression;
				this.ifTrueThen = ifTrueExpression;
			}

			// Token: 0x06000615 RID: 1557 RVA: 0x0001A88C File Offset: 0x00018A8C
			internal Expression Else(Expression resultIfFalse)
			{
				return Expression.Condition(this.condition, this.ifTrueThen, resultIfFalse);
			}

			// Token: 0x04000164 RID: 356
			private readonly Expression condition;

			// Token: 0x04000165 RID: 357
			private readonly Expression ifTrueThen;
		}
	}
}
