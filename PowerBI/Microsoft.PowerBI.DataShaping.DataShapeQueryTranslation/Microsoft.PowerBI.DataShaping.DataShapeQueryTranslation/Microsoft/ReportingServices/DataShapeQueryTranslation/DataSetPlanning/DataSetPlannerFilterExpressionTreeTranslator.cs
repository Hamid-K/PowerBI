using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000F6 RID: 246
	internal sealed class DataSetPlannerFilterExpressionTreeTranslator : DataSetPlannerFilterExpressionTreeTranslatorBase
	{
		// Token: 0x060009D0 RID: 2512 RVA: 0x00025A60 File Offset: 0x00023C60
		internal DataSetPlannerFilterExpressionTreeTranslator(ScopeTree scopeTree, DataShapeAnnotations annotations, ExpressionTable expressionTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
			: base(scopeTree, annotations, expressionTable, transformReferenceMap, applyTransformsInQuery)
		{
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00025A6F File Offset: 0x00023C6F
		public static ExpressionNode Translate(ExpressionContext context, ExpressionNode node, ScopeTree scopeTree, DataShapeAnnotations annotations, IScope containingScope, ExpressionTable expressionTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
		{
			return new DataSetPlannerFilterExpressionTreeTranslator(scopeTree, annotations, expressionTable, transformReferenceMap, applyTransformsInQuery).Translate(node, context, containingScope);
		}
	}
}
