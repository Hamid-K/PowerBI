using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000AE RID: 174
	internal static class ExpressionAnalysisUtils
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x0001D9CC File Offset: 0x0001BBCC
		public static bool IsFilterByRollupScopeMeasureExpression(ExpressionNode node, out ExpressionNode measure)
		{
			FunctionCallExpressionNode functionCallExpressionNode = node as FunctionCallExpressionNode;
			if (functionCallExpressionNode != null && functionCallExpressionNode.Descriptor.Name == "Evaluate")
			{
				FunctionCallExpressionNode functionCallExpressionNode2 = functionCallExpressionNode.Arguments[1] as FunctionCallExpressionNode;
				if (functionCallExpressionNode2 != null && functionCallExpressionNode2.Descriptor.Name == "Rollup")
				{
					measure = functionCallExpressionNode.Arguments[0];
					return true;
				}
			}
			measure = null;
			return false;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001DA3C File Offset: 0x0001BC3C
		public static bool CanReferenceCalculationInInnermostScopeSubQuery(Calculation calculation, IScope containingScope, ScopeTree scopeTree, DataShapeAnnotations annotations, IEnumerable<ExpressionNode> parentNodes)
		{
			IScope containingScope2 = scopeTree.GetContainingScope(calculation);
			if (!(containingScope is DataShape))
			{
				return false;
			}
			if (!scopeTree.AreSameScope(containingScope2, scopeTree.GetInnermostScopeInDataShape((DataShape)containingScope)))
			{
				if (annotations.IsMeasure(calculation))
				{
					return false;
				}
				foreach (ExpressionNode expressionNode in parentNodes)
				{
					FunctionCallExpressionNode functionCallExpressionNode = expressionNode as FunctionCallExpressionNode;
					if (functionCallExpressionNode != null && functionCallExpressionNode.Descriptor.FunctionCategory == FunctionCategory.Aggregate && !ExpressionAnalysisUtils.IsAggregateThatIgnoresInputCardinality(functionCallExpressionNode))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001DAD4 File Offset: 0x0001BCD4
		public static bool IsAggregateThatIgnoresInputCardinality(FunctionCallExpressionNode function)
		{
			if (function.Descriptor.FunctionCategory == FunctionCategory.Aggregate)
			{
				string name = function.Descriptor.Name;
				if (name == "Any" || name == "DistinctCount" || name == "Max" || name == "Min" || name == "SingleValue")
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001DB3E File Offset: 0x0001BD3E
		public static bool IsDetail(Calculation calc, DataShapeAnnotations annotations)
		{
			return !annotations.IsMeasure(calc) && !annotations.IsSubtotal(calc) && !annotations.IsVisualCalculation(calc);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0001DB60 File Offset: 0x0001BD60
		public static IScope GetEvaluateOutputScope(FunctionCallExpressionNode evaluateNode)
		{
			ExpressionNode expressionNode = evaluateNode.Arguments[1];
			ResolvedScopeReferenceExpressionNode resolvedScopeReferenceExpressionNode = expressionNode as ResolvedScopeReferenceExpressionNode;
			if (resolvedScopeReferenceExpressionNode != null)
			{
				return resolvedScopeReferenceExpressionNode.Scope;
			}
			FunctionCallExpressionNode functionCallExpressionNode = expressionNode as FunctionCallExpressionNode;
			if (functionCallExpressionNode.Descriptor.Name == "Rollup")
			{
				return ((ResolvedScopeReferenceExpressionNode)functionCallExpressionNode.Arguments[1]).Scope;
			}
			return null;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0001DBC4 File Offset: 0x0001BDC4
		public static bool TryExtractDataTransformColumnReference(Expression expression, ExpressionTable expressionTable, out DataTransformTableColumn referencedColumn)
		{
			return ExpressionAnalysisUtils.TryExtractDataTransformColumnReference(expressionTable.GetNode(expression.ExpressionId.Value), out referencedColumn);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001DBEC File Offset: 0x0001BDEC
		public static bool TryExtractDataTransformColumnReference(ExpressionNode node, out DataTransformTableColumn referencedColumn)
		{
			ResolvedDataTransformTableColumnReferenceExpressionNode resolvedDataTransformTableColumnReferenceExpressionNode = node as ResolvedDataTransformTableColumnReferenceExpressionNode;
			if (resolvedDataTransformTableColumnReferenceExpressionNode == null)
			{
				referencedColumn = null;
				return false;
			}
			referencedColumn = resolvedDataTransformTableColumnReferenceExpressionNode.Column;
			return true;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0001DC14 File Offset: 0x0001BE14
		public static bool IsSynchronizationIndex(ExpressionNode node)
		{
			FunctionCallExpressionNode functionCallExpressionNode = node as FunctionCallExpressionNode;
			return functionCallExpressionNode != null && functionCallExpressionNode.Descriptor == ExpressionAnalysisUtils.SyncIndexFunctionDescriptor;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001DC3A File Offset: 0x0001BE3A
		internal static bool IsVisualCalculation(ExpressionNode node)
		{
			return node.Kind == ExpressionNodeKind.VisualCalculation;
		}

		// Token: 0x040003D1 RID: 977
		private static readonly FunctionDescriptor SyncIndexFunctionDescriptor = FunctionDescriptorFactory.GetDescriptor("SynchronizationIndex");
	}
}
