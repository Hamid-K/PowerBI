using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000F5 RID: 245
	internal sealed class DataSetPlannerFilterExpressionTranslator : DataSetPlannerFilterExpressionTranslatorBase
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x00025A39 File Offset: 0x00023C39
		private DataSetPlannerFilterExpressionTranslator(DataSetPlannerFilterExpressionTreeTranslatorBase filterExpressionTreeTranslator, Filter filter, TranslationErrorContext errorContext, ExpressionTable inputExpressionTable, WritableExpressionTable outputExpressionTable, ScopeTree scopeTree)
			: base(filterExpressionTreeTranslator, filter, errorContext, inputExpressionTable, outputExpressionTable, scopeTree)
		{
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00025A4A File Offset: 0x00023C4A
		public static void Translate(DataSetPlannerFilterExpressionTreeTranslatorBase filterExpressionTreeTranslator, Filter filter, TranslationErrorContext errorContext, ExpressionTable inputExpressionTable, WritableExpressionTable outputExpressionTable, ScopeTree scopeTree)
		{
			new DataSetPlannerFilterExpressionTranslator(filterExpressionTreeTranslator, filter, errorContext, inputExpressionTable, outputExpressionTable, scopeTree).Visit(filter);
		}
	}
}
