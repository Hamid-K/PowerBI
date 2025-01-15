using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000B9 RID: 185
	internal sealed class SubtotalAnalyzer
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x0001E864 File Offset: 0x0001CA64
		public static bool IsSubtotal(ExpressionNode node)
		{
			Calculation calculation;
			return SubtotalAnalyzer.IsSubtotal(node, out calculation);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0001E87C File Offset: 0x0001CA7C
		public static bool IsSubtotal(ExpressionNode node, out Calculation targetCalculation)
		{
			FunctionCallExpressionNode functionCallExpressionNode = node as FunctionCallExpressionNode;
			if (functionCallExpressionNode != null && functionCallExpressionNode.Descriptor.Name == "Subtotal" && functionCallExpressionNode.Arguments != null && functionCallExpressionNode.Arguments.Count == 1)
			{
				ResolvedCalculationReferenceExpressionNode resolvedCalculationReferenceExpressionNode = functionCallExpressionNode.Arguments[0] as ResolvedCalculationReferenceExpressionNode;
				if (resolvedCalculationReferenceExpressionNode != null)
				{
					targetCalculation = resolvedCalculationReferenceExpressionNode.Calculation;
					return true;
				}
			}
			targetCalculation = null;
			return false;
		}
	}
}
