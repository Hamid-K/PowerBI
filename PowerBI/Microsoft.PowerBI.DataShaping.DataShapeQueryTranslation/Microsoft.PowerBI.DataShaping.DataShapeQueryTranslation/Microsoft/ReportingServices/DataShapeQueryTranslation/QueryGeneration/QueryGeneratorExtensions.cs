using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x0200008F RID: 143
	internal static class QueryGeneratorExtensions
	{
		// Token: 0x060006D4 RID: 1748 RVA: 0x00019E6D File Offset: 0x0001806D
		internal static IConceptualColumn GetColumn(this GroupKey groupKey, ExpressionTable expressionTable)
		{
			ExpressionNode node = expressionTable.GetNode(groupKey.Value);
			Microsoft.DataShaping.Contract.RetailAssert(node.Kind == ExpressionNodeKind.ResolvedProperty, "Expression node is expected to be of kind ResolvedProperty");
			return ((ResolvedPropertyExpressionNode)node).Property.AsColumn();
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00019E9E File Offset: 0x0001809E
		internal static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection ToQdmSortDirection(this Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection sortDirection)
		{
			if (sortDirection == Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection.Ascending)
			{
				return Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection.Ascending;
			}
			if (sortDirection != Microsoft.DataShaping.InternalContracts.DataShapeQuery.SortDirection.Descending)
			{
				Microsoft.DataShaping.Contract.RetailFail("Invalid SortDirection: {0}", sortDirection);
				throw new InvalidOperationException();
			}
			return Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.SortDirection.Descending;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00019EC4 File Offset: 0x000180C4
		internal static QueryExpression ToQueryExpression(this Expression expression, IQueryExpressionGenerator expressionGenerator, ExpressionContext context)
		{
			ExpressionId value = expression.ExpressionId.Value;
			return expressionGenerator.TranslateExpression(value, context).QueryExpression;
		}
	}
}
