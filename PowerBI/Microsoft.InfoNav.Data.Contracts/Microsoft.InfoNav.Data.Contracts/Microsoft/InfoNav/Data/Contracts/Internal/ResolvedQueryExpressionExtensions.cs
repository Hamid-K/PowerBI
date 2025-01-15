using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200022F RID: 559
	public static class ResolvedQueryExpressionExtensions
	{
		// Token: 0x06001021 RID: 4129 RVA: 0x0001E640 File Offset: 0x0001C840
		public static bool TryGetDateTime(this ResolvedQueryExpression expression, out DateTime dateTime)
		{
			ResolvedQueryLiteralExpression resolvedQueryLiteralExpression = expression as ResolvedQueryLiteralExpression;
			if (resolvedQueryLiteralExpression == null)
			{
				dateTime = default(DateTime);
				return false;
			}
			DateTimePrimitiveValue dateTimePrimitiveValue = resolvedQueryLiteralExpression.Value as DateTimePrimitiveValue;
			if (dateTimePrimitiveValue == null)
			{
				dateTime = default(DateTime);
				return false;
			}
			dateTime = dateTimePrimitiveValue.Value;
			return true;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0001E68C File Offset: 0x0001C88C
		public static bool TryGetAsProperty<TProperty>(this ResolvedQueryExpression expression, out TProperty propertyInstance) where TProperty : class, IConceptualProperty
		{
			ResolvedQueryPropertyExpression resolvedQueryPropertyExpression = expression as ResolvedQueryPropertyExpression;
			if (resolvedQueryPropertyExpression == null)
			{
				propertyInstance = default(TProperty);
				return false;
			}
			propertyInstance = resolvedQueryPropertyExpression.Property as TProperty;
			return propertyInstance != null;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0001E6D8 File Offset: 0x0001C8D8
		public static bool TryGetAsPropertyOrFilteredEvalMeasureProperty<TProperty>(this ResolvedQueryExpression expression, out TProperty propertyInstance) where TProperty : class, IConceptualProperty
		{
			ResolvedQueryExpression resolvedQueryExpression;
			if (ResolvedQueryExpressionExtensions.TryGetFilteredEvalExpression(expression, out resolvedQueryExpression))
			{
				expression = resolvedQueryExpression;
			}
			return expression.TryGetAsProperty(out propertyInstance);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0001E6FC File Offset: 0x0001C8FC
		private static bool TryGetFilteredEvalExpression(ResolvedQueryExpression expression, out ResolvedQueryExpression outExpression)
		{
			ResolvedQueryFilteredEvalExpression resolvedQueryFilteredEvalExpression = expression as ResolvedQueryFilteredEvalExpression;
			if (resolvedQueryFilteredEvalExpression != null)
			{
				outExpression = resolvedQueryFilteredEvalExpression.Expression;
				return true;
			}
			outExpression = null;
			return false;
		}
	}
}
