using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Normalization
{
	// Token: 0x0200009A RID: 154
	internal static class NormalizationUtils
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x0001B3B8 File Offset: 0x000195B8
		internal static void RewriteSubtotalToEvalRollup(IScope expressionScope, Expression expression, WritableExpressionTable expressionTable, ScopeTree scopeTree)
		{
			Calculation calculation;
			if (!SubtotalAnalyzer.IsSubtotal(expressionTable.GetNode(expression), out calculation))
			{
				return;
			}
			IScope containingScope = scopeTree.GetContainingScope(calculation);
			ExpressionNode node = expressionTable.GetNode(calculation.Value);
			ExpressionNode[] array = new ExpressionNode[]
			{
				new ResolvedScopeReferenceExpressionNode(containingScope),
				new ResolvedScopeReferenceExpressionNode(expressionScope)
			};
			FunctionCallExpressionNode functionCallExpressionNode = new FunctionCallExpressionNode(FunctionDescriptorFactory.GetDescriptor("Rollup"), FunctionUsageKind.Unassigned, array);
			ExpressionNode[] array2 = new ExpressionNode[] { node, functionCallExpressionNode };
			FunctionCallExpressionNode functionCallExpressionNode2 = new FunctionCallExpressionNode(FunctionDescriptorFactory.GetDescriptor("Evaluate"), FunctionUsageKind.Unassigned, array2);
			expressionTable.SetNode(expression, functionCallExpressionNode2);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001B448 File Offset: 0x00019648
		internal static List<RestartTokenGroupingBinding> BuildRestartGroupingBindings(IReadOnlyList<DataMember> dynamicMembers, BatchSubtotalAnnotations subtotalAnnotations)
		{
			List<RestartTokenGroupingBinding> list = new List<RestartTokenGroupingBinding>(dynamicMembers.Count);
			foreach (DataMember dataMember in dynamicMembers)
			{
				BatchSubtotalAnnotation batchSubtotalAnnotation;
				if (subtotalAnnotations.TryGetSubtotalAnnotation(dataMember, out batchSubtotalAnnotation))
				{
					SubtotalType subtotalType = ((batchSubtotalAnnotation.SortDirection == SortDirection.Ascending) ? SubtotalType.After : SubtotalType.Before);
					IList<IIdentifiable> list2;
					if (!subtotalAnnotations.TryGetSubtotalAnnotationSources(batchSubtotalAnnotation, out list2))
					{
						Contract.RetailFail("A total annotation without a source is unexpected");
					}
					IIdentifiable identifiable = list2.Single("Expected only one subtotal annotation source", Array.Empty<string>());
					list.Add(new RestartTokenGroupingBinding(dataMember.Id.Value, identifiable.Id.Value, subtotalType));
				}
				else
				{
					list.Add(new RestartTokenGroupingBinding(dataMember.Id.Value, null, SubtotalType.None));
				}
			}
			return list;
		}
	}
}
