using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000077 RID: 119
	internal interface IQueryExpressionGenerator
	{
		// Token: 0x060005E1 RID: 1505
		List<KeyValuePair<ExpressionId, QueryExpressionContext>> TranslateCalculation(Calculation calculation);

		// Token: 0x060005E2 RID: 1506
		QueryExpressionContext TranslateCalculationReference(Calculation calculation);

		// Token: 0x060005E3 RID: 1507
		QueryExpressionContext TranslateGroupKeyReference(GroupKey groupKey);

		// Token: 0x060005E4 RID: 1508
		QueryExpressionContext TranslateSortKeyReference(SortKey sortKey);

		// Token: 0x060005E5 RID: 1509
		QueryExpressionContext TranslateNullExpression(Expression expression, ConceptualResultType resultType);

		// Token: 0x060005E6 RID: 1510
		bool IsNullLiteralExpression(Expression expression);

		// Token: 0x060005E7 RID: 1511
		QueryExpressionContext TranslateExpression(ExpressionId expressionId, ExpressionContext expressionContext);

		// Token: 0x060005E8 RID: 1512
		QueryExpressionContext TranslateExpression(ExpressionNode expressionNode, ExpressionContext expressionContext);

		// Token: 0x060005E9 RID: 1513
		QueryExpressionContext TranslateFilterExpression(ExpressionId expressionId, ExpressionContext expressionContext);

		// Token: 0x060005EA RID: 1514
		bool ValidateLiteralType(QueryExpression queryExpr, ScalarValue value, ExpressionContext expressionContext, EngineMessageSeverity severity);

		// Token: 0x060005EB RID: 1515
		ExpressionNode GetExpressionNode(ExpressionId expressionId);
	}
}
