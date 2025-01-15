using System;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal
{
	// Token: 0x0200019F RID: 415
	internal static class Literals
	{
		// Token: 0x0600159A RID: 5530 RVA: 0x0003C4EC File Offset: 0x0003A6EC
		internal static QueryLiteralExpression GetTypedZero(ConceptualResultType type)
		{
			ConceptualPrimitiveResultType conceptualPrimitiveResultType = (ConceptualPrimitiveResultType)type;
			switch (conceptualPrimitiveResultType.ConceptualDataType)
			{
			case ConceptualPrimitiveType.Text:
				return Literals.EmptyString;
			case ConceptualPrimitiveType.Decimal:
				return Literals.ZeroDecimal;
			case ConceptualPrimitiveType.Double:
				return Literals.ZeroDouble;
			case ConceptualPrimitiveType.Integer:
				return Literals.ZeroInt64;
			case ConceptualPrimitiveType.Boolean:
				return Literals.False;
			case ConceptualPrimitiveType.DateTime:
				return Literals.ZeroDateTime;
			}
			throw new InvalidOperationException(StringUtil.FormatInvariant("Unsupported data type: {0}", new object[] { conceptualPrimitiveResultType.ConceptualDataType }));
		}

		// Token: 0x04000B7C RID: 2940
		internal static readonly DateTime ZeroDateTimeValue = DateTime.FromOADate(0.0);

		// Token: 0x04000B7D RID: 2941
		internal static readonly QueryLiteralExpression One = QueryExpressionBuilder.Literal(1);

		// Token: 0x04000B7E RID: 2942
		internal static readonly QueryLiteralExpression OneInt64 = QueryExpressionBuilder.Literal(1L);

		// Token: 0x04000B7F RID: 2943
		internal static readonly QueryLiteralExpression Zero = QueryExpressionBuilder.Literal(0);

		// Token: 0x04000B80 RID: 2944
		internal static readonly QueryLiteralExpression ZeroInt64 = QueryExpressionBuilder.Literal(0L);

		// Token: 0x04000B81 RID: 2945
		internal static readonly QueryLiteralExpression ZeroDouble = QueryExpressionBuilder.Literal(0.0);

		// Token: 0x04000B82 RID: 2946
		internal static readonly QueryLiteralExpression ZeroDecimal = QueryExpressionBuilder.Literal(0m);

		// Token: 0x04000B83 RID: 2947
		internal static readonly QueryLiteralExpression ZeroDateTime = QueryExpressionBuilder.Literal(Literals.ZeroDateTimeValue);

		// Token: 0x04000B84 RID: 2948
		internal static readonly QueryLiteralExpression PositiveInfinity = QueryExpressionBuilder.Literal(double.PositiveInfinity);

		// Token: 0x04000B85 RID: 2949
		internal static readonly QueryLiteralExpression NegativeInfinity = QueryExpressionBuilder.Literal(double.NegativeInfinity);

		// Token: 0x04000B86 RID: 2950
		internal static readonly QueryLiteralExpression True = QueryExpressionBuilder.Literal(true);

		// Token: 0x04000B87 RID: 2951
		internal static readonly QueryLiteralExpression False = QueryExpressionBuilder.Literal(false);

		// Token: 0x04000B88 RID: 2952
		internal static readonly QueryLiteralExpression EmptyString = QueryExpressionBuilder.Literal(string.Empty);

		// Token: 0x04000B89 RID: 2953
		internal static readonly QueryLiteralExpression NumberOfHoursInADay = QueryExpressionBuilder.Literal(24L);

		// Token: 0x04000B8A RID: 2954
		internal static readonly QueryLiteralExpression NumberOfMinutesInADay = QueryExpressionBuilder.Literal(1440L);

		// Token: 0x04000B8B RID: 2955
		internal static readonly QueryLiteralExpression NumberOfSecondsInADay = QueryExpressionBuilder.Literal(86400L);

		// Token: 0x04000B8C RID: 2956
		internal static readonly QueryNullExpression Null = Literals.One.ConceptualResultType.Null();

		// Token: 0x04000B8D RID: 2957
		internal static readonly QueryNullExpression NullInt64 = Literals.OneInt64.ConceptualResultType.Null();

		// Token: 0x04000B8E RID: 2958
		internal static readonly QueryNullExpression NullBoolean = Literals.False.ConceptualResultType.Null();

		// Token: 0x04000B8F RID: 2959
		internal static readonly QueryNullExpression NullString = Literals.EmptyString.ConceptualResultType.Null();

		// Token: 0x04000B90 RID: 2960
		internal static readonly QueryNullExpression NullVariant = ConceptualPrimitiveResultType.Variant.Null();
	}
}
