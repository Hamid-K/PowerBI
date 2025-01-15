using System;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000C7 RID: 199
	internal static class DsqExpressionUtils
	{
		// Token: 0x06000523 RID: 1315 RVA: 0x0000ABAC File Offset: 0x00008DAC
		internal static string GetDsqFunctionName(ResolvedQueryAggregationExpression expression, bool suppressModelGrouping = false)
		{
			bool flag = false;
			IConceptualProperty conceptualProperty;
			if (expression.Expression.TryGetAsProperty(out conceptualProperty))
			{
				flag = conceptualProperty.SupportsDistinctCount(suppressModelGrouping);
			}
			bool flag2 = expression.Expression is ResolvedQuerySourceRefExpression;
			QueryAggregateFunction function = expression.Function;
			switch (function)
			{
			case QueryAggregateFunction.Sum:
				return "Sum";
			case QueryAggregateFunction.Avg:
				return "Average";
			case QueryAggregateFunction.Count:
				if (flag2)
				{
					return "CountRows";
				}
				if (flag)
				{
					return "DistinctCount";
				}
				return "Count";
			case QueryAggregateFunction.Min:
				return "Min";
			case QueryAggregateFunction.Max:
				return "Max";
			case QueryAggregateFunction.CountNonNull:
				return "Count";
			case QueryAggregateFunction.Median:
				return "Median";
			case QueryAggregateFunction.StandardDeviation:
				return "StandardDeviation";
			case QueryAggregateFunction.Variance:
				return "Variance";
			case QueryAggregateFunction.SingleValue:
				return "SingleValue";
			default:
				Contract.RetailFail("Unhandled aggregate function: " + function.ToString());
				return null;
			}
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0000AC84 File Offset: 0x00008E84
		internal static bool SupportsDistinctCount(this IConceptualProperty property, bool suppressModelGrouping = false)
		{
			if (property is IConceptualMeasure)
			{
				return false;
			}
			IConceptualColumn conceptualColumn = property as IConceptualColumn;
			if (conceptualColumn != null && !suppressModelGrouping && conceptualColumn.Grouping.IsIdentityOnEntityKey)
			{
				return false;
			}
			ConceptualPrimitiveType conceptualDataType = property.ConceptualDataType;
			return conceptualDataType == ConceptualPrimitiveType.Boolean || conceptualDataType == ConceptualPrimitiveType.Date || conceptualDataType == ConceptualPrimitiveType.DateTime || conceptualDataType.IsNumeric() || conceptualDataType == ConceptualPrimitiveType.Text;
		}
	}
}
