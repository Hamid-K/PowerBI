using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000169 RID: 361
	internal sealed class BatchDataSetPlannerFilterExpressionTreeTranslator : DataSetPlannerFilterExpressionTreeTranslatorBase
	{
		// Token: 0x06000D14 RID: 3348 RVA: 0x00035F9B File Offset: 0x0003419B
		internal BatchDataSetPlannerFilterExpressionTreeTranslator(ScopeTree scopeTree, DataShapeAnnotations annotations, ExpressionTable expressionTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
			: base(scopeTree, annotations, expressionTable, transformReferenceMap, applyTransformsInQuery)
		{
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00035FAA File Offset: 0x000341AA
		public static ExpressionNode Translate(ExpressionContext context, ExpressionNode node, ScopeTree scopeTree, DataShapeAnnotations annotations, IScope containingScope, ExpressionTable expressionTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
		{
			return new BatchDataSetPlannerFilterExpressionTreeTranslator(scopeTree, annotations, expressionTable, transformReferenceMap, applyTransformsInQuery).Translate(node, context, containingScope);
		}
	}
}
