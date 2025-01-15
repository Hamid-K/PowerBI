using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000168 RID: 360
	internal sealed class BatchDataSetPlannerFilterExpressionTranslator : DataSetPlannerFilterExpressionTranslatorBase
	{
		// Token: 0x06000D11 RID: 3345 RVA: 0x00035F57 File Offset: 0x00034157
		private BatchDataSetPlannerFilterExpressionTranslator(DataSetPlannerFilterExpressionTreeTranslatorBase filterExpressionTreeTranslator, Filter filter, TranslationErrorContext errorContext, ExpressionTable inputExpressionTable, WritableExpressionTable outputExpressionTable, ScopeTree scopeTree)
			: base(filterExpressionTreeTranslator, filter, errorContext, inputExpressionTable, outputExpressionTable, scopeTree)
		{
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00035F68 File Offset: 0x00034168
		public static void Translate(DataSetPlannerFilterExpressionTreeTranslatorBase filterExpressionTreeTranslator, Filter filter, TranslationErrorContext errorContext, ExpressionTable inputExpressionTable, WritableExpressionTable outputExpressionTable, ScopeTree scopeTree)
		{
			new BatchDataSetPlannerFilterExpressionTranslator(filterExpressionTreeTranslator, filter, errorContext, inputExpressionTable, outputExpressionTable, scopeTree).Visit(filter);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00035F7E File Offset: 0x0003417E
		internal override void VisitExpression(Expression expression, FilterCondition owner, string propertyName)
		{
			BatchDataSetPlanningUtils.ExtractExpressionFromEvaluateRollup(expression, this.m_inputExpressionTable, this.m_outputExpressionTable);
			base.VisitExpression(expression, owner, propertyName);
		}
	}
}
