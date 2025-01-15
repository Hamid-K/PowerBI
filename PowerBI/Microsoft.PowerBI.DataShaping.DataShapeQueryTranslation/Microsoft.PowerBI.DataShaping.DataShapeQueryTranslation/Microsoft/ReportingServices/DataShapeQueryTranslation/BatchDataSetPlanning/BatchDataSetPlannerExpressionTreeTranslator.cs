using System;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;
using Microsoft.ReportingServices.DataShapeQueryTranslation.CommonDataSetPlanning;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.BatchDataSetPlanning
{
	// Token: 0x02000167 RID: 359
	internal sealed class BatchDataSetPlannerExpressionTreeTranslator : DataSetPlannerExpressionTreeTranslatorBase
	{
		// Token: 0x06000D0D RID: 3341 RVA: 0x00035EBD File Offset: 0x000340BD
		private BatchDataSetPlannerExpressionTreeTranslator(ExpressionContext context, ScopeTree scopeTree, DataShapeAnnotations annotations, IScope containingScope, ExpressionTable expressionTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
			: base(scopeTree, annotations, expressionTable, transformReferenceMap, applyTransformsInQuery, context, containingScope)
		{
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00035ED0 File Offset: 0x000340D0
		public static ExpressionNode Translate(ExpressionContext context, ExpressionNode node, ScopeTree scopeTree, DataShapeAnnotations annotations, IScope containingScope, ExpressionTable expressionTable, DataTransformReferenceMap transformReferenceMap, bool applyTransformsInQuery)
		{
			return new BatchDataSetPlannerExpressionTreeTranslator(context, scopeTree, annotations, containingScope, expressionTable, transformReferenceMap, applyTransformsInQuery).Visit(node);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00035EE8 File Offset: 0x000340E8
		protected override ExpressionNode TranslateEvaluateExpression(FunctionCallExpressionNode node)
		{
			ExpressionNode expressionNode = node.Arguments[1];
			if (expressionNode.Kind != ExpressionNodeKind.FunctionCall)
			{
				return node;
			}
			FunctionCallExpressionNode functionCallExpressionNode = (FunctionCallExpressionNode)expressionNode;
			if (functionCallExpressionNode.Descriptor.Name == "Scope")
			{
				return base.TranslateEvaluateWithScopeFunction(node, functionCallExpressionNode);
			}
			Contract.RetailFail("Batch query pattern only supports Scope as a function argument to Evaluate");
			throw new InvalidOperationException();
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00035F44 File Offset: 0x00034144
		protected override ExpressionNode TranslateSubtotalExpression(FunctionCallExpressionNode node)
		{
			return (ResolvedCalculationReferenceExpressionNode)node.Arguments[0];
		}
	}
}
