using System;
using Microsoft.DataShaping;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x0200014E RID: 334
	internal static class DaxTypeSafeFloorExpressionTranslator
	{
		// Token: 0x06001283 RID: 4739 RVA: 0x00035845 File Offset: 0x00033A45
		internal static DaxExpression Translate(QueryTypeSafeFloorExpression expression, DaxTransform daxTransform)
		{
			return DaxTypeSafeFloorExpressionTranslator.BuildAppropriateRoundExpression(expression).Accept<DaxExpression>(daxTransform);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x00035854 File Offset: 0x00033A54
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression BuildAppropriateRoundExpression(QueryTypeSafeFloorExpression typeSafeFloorExpr)
		{
			if ((typeSafeFloorExpr.TimeUnit == null && typeSafeFloorExpr.Expression.ConceptualResultType.IsDateTime()) || (typeSafeFloorExpr.TimeUnit != null && !typeSafeFloorExpr.Expression.ConceptualResultType.IsDateTime()))
			{
				Contract.RetailFail("Flooring on DateTime Field must have a TimeUnit specified!");
			}
			bool flag;
			if (typeSafeFloorExpr.TimeUnit != null)
			{
				TimeUnit? timeUnit = typeSafeFloorExpr.TimeUnit;
				TimeUnit timeUnit2 = TimeUnit.Week;
				if (!((timeUnit.GetValueOrDefault() == timeUnit2) & (timeUnit != null)))
				{
					timeUnit = typeSafeFloorExpr.TimeUnit;
					timeUnit2 = TimeUnit.Decade;
					flag = (timeUnit.GetValueOrDefault() == timeUnit2) & (timeUnit != null);
				}
				else
				{
					flag = true;
				}
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				Contract.RetailFail("Week and Decade TimeUnits are not supported for DateTime Field flooring!");
			}
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = DaxTypeSafeFloorExpressionTranslator.ConvertToUnits(typeSafeFloorExpr);
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression2 = DaxTypeSafeFloorExpressionTranslator.ConvertFromUnits(DaxTypeSafeFloorExpressionTranslator.BuildRoundExpr(typeSafeFloorExpr, queryExpression), typeSafeFloorExpr);
			return typeSafeFloorExpr.Expression.IsNull().If(queryExpression2.ConceptualResultType.Null(), queryExpression2);
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00035944 File Offset: 0x00033B44
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression ConvertToUnits(QueryTypeSafeFloorExpression floorExpr)
		{
			if (floorExpr.TimeUnit == null)
			{
				return floorExpr.Expression;
			}
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = null;
			TimeUnit? timeUnit = floorExpr.TimeUnit;
			if (timeUnit != null)
			{
				switch (timeUnit.GetValueOrDefault())
				{
				case TimeUnit.Day:
					queryExpression = floorExpr.Expression.Int().Minus(Literals.OneInt64);
					break;
				case TimeUnit.Month:
					queryExpression = floorExpr.Expression.Month().Minus(Literals.OneInt64);
					break;
				case TimeUnit.Year:
					queryExpression = floorExpr.Expression.Year();
					break;
				case TimeUnit.Second:
					queryExpression = floorExpr.Expression.Multiply(Literals.NumberOfSecondsInADay);
					break;
				case TimeUnit.Minute:
					queryExpression = floorExpr.Expression.Multiply(Literals.NumberOfMinutesInADay);
					break;
				case TimeUnit.Hour:
					queryExpression = floorExpr.Expression.Multiply(Literals.NumberOfHoursInADay);
					break;
				}
			}
			return queryExpression;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x00035A24 File Offset: 0x00033C24
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression ConvertFromUnits(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression inputExpr, QueryTypeSafeFloorExpression floorExpr)
		{
			if (floorExpr.TimeUnit == null)
			{
				return inputExpr;
			}
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = null;
			TimeUnit? timeUnit = floorExpr.TimeUnit;
			if (timeUnit != null)
			{
				switch (timeUnit.GetValueOrDefault())
				{
				case TimeUnit.Day:
					queryExpression = Literals.OneInt64.Plus(inputExpr);
					break;
				case TimeUnit.Month:
					queryExpression = floorExpr.Expression.Year().Date(Literals.OneInt64.Plus(inputExpr), Literals.OneInt64);
					break;
				case TimeUnit.Year:
					queryExpression = inputExpr.Date(Literals.OneInt64, Literals.OneInt64);
					break;
				case TimeUnit.Second:
					queryExpression = inputExpr.DivideRaw(Literals.NumberOfSecondsInADay);
					break;
				case TimeUnit.Minute:
					queryExpression = inputExpr.DivideRaw(Literals.NumberOfMinutesInADay);
					break;
				case TimeUnit.Hour:
					queryExpression = inputExpr.DivideRaw(Literals.NumberOfHoursInADay);
					break;
				}
			}
			return queryExpression;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x00035AF8 File Offset: 0x00033CF8
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression BuildRoundExpr(QueryTypeSafeFloorExpression floorExpr, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression convertedToUnitsExpr)
		{
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression2;
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = DaxTypeSafeFloorExpressionTranslator.ApplySizeDivision(convertedToUnitsExpr, floorExpr.Size, out queryExpression2);
			return DaxTypeSafeFloorExpressionTranslator.ApplyRoundingAndSizeMultiplication(floorExpr, queryExpression, queryExpression2);
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x00035B1C File Offset: 0x00033D1C
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression ApplySizeDivision(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression convertedToUnitsExpr, double size, out Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression sizeExpr)
		{
			if (size == 1.0)
			{
				sizeExpr = null;
				return convertedToUnitsExpr;
			}
			sizeExpr = QueryExpressionBuilder.Literal(size);
			return convertedToUnitsExpr.DivideRaw(sizeExpr);
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00035B44 File Offset: 0x00033D44
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression ApplyRoundingAndSizeMultiplication(QueryTypeSafeFloorExpression floorExpr, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression innerExpr, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression sizeExpr)
		{
			if (!floorExpr.Expression.ConceptualResultType.IsDouble())
			{
				if (floorExpr.TimeUnit == null || (floorExpr.TimeUnit.Value != TimeUnit.Year && floorExpr.TimeUnit.Value != TimeUnit.Month) || floorExpr.Size != 1.0)
				{
					innerExpr = innerExpr.Int();
				}
				return DaxTypeSafeFloorExpressionTranslator.ApplySizeMultiplication(floorExpr.Size, innerExpr, sizeExpr);
			}
			QueryFunctionExpression queryFunctionExpression = innerExpr.RoundUp(Literals.ZeroInt64);
			QueryFunctionExpression queryFunctionExpression2 = innerExpr.RoundDown(Literals.ZeroInt64);
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = DaxTypeSafeFloorExpressionTranslator.ApplySizeMultiplication(floorExpr.Size, queryFunctionExpression, sizeExpr);
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression2 = DaxTypeSafeFloorExpressionTranslator.ApplySizeMultiplication(floorExpr.Size, queryFunctionExpression2, sizeExpr);
			return floorExpr.Expression.GreaterThanOrEqual(Literals.ZeroInt64).If(queryExpression2, queryExpression);
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00035C09 File Offset: 0x00033E09
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression ApplySizeMultiplication(double size, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression innerExpr, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression sizeExpr)
		{
			if (size == 1.0)
			{
				return innerExpr;
			}
			return innerExpr.Multiply(sizeExpr);
		}
	}
}
