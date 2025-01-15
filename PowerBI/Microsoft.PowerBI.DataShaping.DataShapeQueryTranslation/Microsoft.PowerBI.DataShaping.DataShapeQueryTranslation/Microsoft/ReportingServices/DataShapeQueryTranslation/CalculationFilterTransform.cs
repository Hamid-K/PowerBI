using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation
{
	// Token: 0x02000042 RID: 66
	internal static class CalculationFilterTransform
	{
		// Token: 0x060002AC RID: 684 RVA: 0x00007C70 File Offset: 0x00005E70
		internal static ExpressionNode TranslateToInlineFilter(ExpressionNode expressionNode, ExpressionTable inputExpressionTable, Calculation calculation, DataShape dataShape)
		{
			Filter filterForTargetStructure = dataShape.Filters.GetFilterForTargetStructure(inputExpressionTable, calculation);
			if (filterForTargetStructure == null)
			{
				return expressionNode;
			}
			return CalculationFilterTransform.TranslateToInlineFilter(expressionNode, inputExpressionTable, calculation, filterForTargetStructure.Condition);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00007CA0 File Offset: 0x00005EA0
		internal static ExpressionNode TranslateToInlineFilter(ExpressionNode expressionNode, ExpressionTable inputExpressionTable, Calculation calculation, FilterCondition filterCondition)
		{
			FunctionCallExpressionNode functionCallExpressionNode = expressionNode as FunctionCallExpressionNode;
			if (functionCallExpressionNode != null && functionCallExpressionNode.UsageKind == FunctionUsageKind.Processing)
			{
				ExpressionNode[] array = new ExpressionNode[functionCallExpressionNode.Arguments.Count];
				for (int i = 0; i < functionCallExpressionNode.Arguments.Count; i++)
				{
					array[i] = CalculationFilterTransform.TranslateToInlineFilter(functionCallExpressionNode.Arguments[i], inputExpressionTable, calculation, filterCondition);
				}
				return new FunctionCallExpressionNode(functionCallExpressionNode.Descriptor, functionCallExpressionNode.UsageKind, array);
			}
			if (expressionNode.Kind == ExpressionNodeKind.FilterInlinedCalculation)
			{
				FilterInlinedCalculationExpressionNode filterInlinedCalculationExpressionNode = (FilterInlinedCalculationExpressionNode)expressionNode;
				FilterCondition filterCondition2 = filterInlinedCalculationExpressionNode.FilterCondition;
				CompoundFilterCondition compoundFilterCondition = filterCondition2 as CompoundFilterCondition;
				if (compoundFilterCondition != null && compoundFilterCondition.ConditionsHaveIndependentContext())
				{
					compoundFilterCondition.Conditions.Add(filterCondition);
				}
				else
				{
					filterCondition = new CompoundFilterCondition
					{
						Operator = CompoundFilterOperator.All,
						Conditions = new List<FilterCondition> { filterCondition2, filterCondition }
					};
				}
				expressionNode = filterInlinedCalculationExpressionNode.ExpressionNode;
			}
			return new FilterInlinedCalculationExpressionNode(expressionNode, calculation, filterCondition, null);
		}
	}
}
