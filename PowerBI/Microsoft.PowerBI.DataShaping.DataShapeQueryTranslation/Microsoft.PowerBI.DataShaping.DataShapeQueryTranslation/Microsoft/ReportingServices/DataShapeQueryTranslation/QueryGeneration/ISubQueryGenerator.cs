using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryGeneration
{
	// Token: 0x02000078 RID: 120
	internal interface ISubQueryGenerator
	{
		// Token: 0x060005EC RID: 1516
		SubQuery GenerateAggregatableSubQuery(ExpressionId targetExpressionId, ExpressionContext expressionContext, DataSetPlan dataSetPlan);

		// Token: 0x060005ED RID: 1517
		SubQuery GenerateTableSubQuery(string subqueryName, DataSetPlan dataSetPlan);
	}
}
