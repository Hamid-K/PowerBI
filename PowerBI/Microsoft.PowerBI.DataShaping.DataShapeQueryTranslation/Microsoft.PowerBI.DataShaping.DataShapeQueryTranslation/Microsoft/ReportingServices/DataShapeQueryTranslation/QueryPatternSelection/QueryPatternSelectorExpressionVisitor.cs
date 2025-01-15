using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.QueryPatternSelection
{
	// Token: 0x02000071 RID: 113
	internal sealed class QueryPatternSelectorExpressionVisitor : ExpressionNodeTreeTransform
	{
		// Token: 0x060005C4 RID: 1476 RVA: 0x00014B67 File Offset: 0x00012D67
		private QueryPatternSelectorExpressionVisitor(QueryPatternSelectionContext context, bool allowEvaluate, bool allowRollup, bool allowSubtotal)
			: base(false)
		{
			this.m_context = context;
			this.m_allowEvaluateScopeChange = allowEvaluate;
			this.m_allowRollup = allowRollup;
			this.m_allowSubtotal = allowSubtotal;
			this.m_isCompatibleWithProcessing = true;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00014B94 File Offset: 0x00012D94
		public static bool IsValidForBatchQueryPattern(QueryPatternSelectionContext context, ExpressionNode node, bool allowEvaluate, bool allowRollup, bool allowSubtotal, IScope containingScope, out bool isCompatibleWithProcessing)
		{
			QueryPatternSelectorExpressionVisitor queryPatternSelectorExpressionVisitor = new QueryPatternSelectorExpressionVisitor(context, allowEvaluate, allowRollup, allowSubtotal);
			queryPatternSelectorExpressionVisitor.VisitInScope(node, containingScope);
			isCompatibleWithProcessing = queryPatternSelectorExpressionVisitor.m_isCompatibleWithProcessing;
			return !queryPatternSelectorExpressionVisitor.m_foundInvalidFeature;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00014BC8 File Offset: 0x00012DC8
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			if (!node.Descriptor.IsBatchQueryCompatible)
			{
				this.m_foundInvalidFeature = true;
			}
			if (node.Descriptor.Name == "Evaluate")
			{
				return this.VisitEvaluate(node);
			}
			if (node.Descriptor.Name == "Subtotal")
			{
				if (!this.m_allowSubtotal)
				{
					this.m_foundInvalidFeature = true;
				}
				return node;
			}
			if (node.Descriptor.Name == "Rollup" && !this.m_allowRollup)
			{
				this.m_foundInvalidFeature = true;
			}
			return base.Visit(node);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00014C60 File Offset: 0x00012E60
		private ExpressionNode VisitEvaluate(FunctionCallExpressionNode node)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			FunctionCallExpressionNode functionCallExpressionNode = node.Arguments[1] as FunctionCallExpressionNode;
			if (functionCallExpressionNode != null)
			{
				if (functionCallExpressionNode.Descriptor.Name == "Rollup")
				{
					flag = true;
					flag2 = true;
					flag3 = true;
				}
				else if (functionCallExpressionNode.Descriptor.Name == "Scope")
				{
					flag = true;
				}
			}
			else if (node.Arguments[1] is ResolvedScopeReferenceExpressionNode)
			{
				flag = true;
				flag2 = true;
				flag3 = true;
			}
			if (!flag)
			{
				this.m_foundInvalidFeature = true;
			}
			if (flag2 && !this.m_allowEvaluateScopeChange)
			{
				this.m_foundInvalidFeature = true;
			}
			if (flag2 && !flag3)
			{
				this.m_foundInvalidFeature = true;
			}
			IScope evaluateOutputScope = ExpressionAnalysisUtils.GetEvaluateOutputScope(node);
			if (evaluateOutputScope != null)
			{
				this.VisitInScope(node.Arguments[0], evaluateOutputScope);
				this.Visit(node.Arguments[1]);
				return node;
			}
			return base.Visit(node);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00014D44 File Offset: 0x00012F44
		private ExpressionNode VisitInScope(ExpressionNode node, IScope containingScope)
		{
			IScope containingScope2 = this.m_containingScope;
			this.m_containingScope = containingScope;
			ExpressionNode expressionNode = this.Visit(node);
			this.m_containingScope = containingScope2;
			return expressionNode;
		}

		// Token: 0x040002DE RID: 734
		private readonly QueryPatternSelectionContext m_context;

		// Token: 0x040002DF RID: 735
		private readonly bool m_allowEvaluateScopeChange;

		// Token: 0x040002E0 RID: 736
		private readonly bool m_allowRollup;

		// Token: 0x040002E1 RID: 737
		private readonly bool m_allowSubtotal;

		// Token: 0x040002E2 RID: 738
		private IScope m_containingScope;

		// Token: 0x040002E3 RID: 739
		private bool m_foundInvalidFeature;

		// Token: 0x040002E4 RID: 740
		private bool m_isCompatibleWithProcessing;
	}
}
