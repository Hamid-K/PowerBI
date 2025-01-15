using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000B7 RID: 183
	internal sealed class StructureAggregateAnalyzer : ExpressionNodeTreeTransform
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x0001E66F File Offset: 0x0001C86F
		private StructureAggregateAnalyzer(IScope containingScope, ScopeTree scopeTree)
			: base(false)
		{
			this.m_originalContainingScope = containingScope;
			this.m_scopeTree = scopeTree;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001E686 File Offset: 0x0001C886
		public static bool IsAggregate(ExpressionNode node, IScope containingScope, ScopeTree scopeTree)
		{
			StructureAggregateAnalyzer structureAggregateAnalyzer = new StructureAggregateAnalyzer(containingScope, scopeTree);
			structureAggregateAnalyzer.Visit(node);
			return structureAggregateAnalyzer.m_isAggregate;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001E69C File Offset: 0x0001C89C
		public override ExpressionNode Visit(FunctionCallExpressionNode node)
		{
			bool inAggregateFunction = this.m_inAggregateFunction;
			if (node.Descriptor.FunctionCategory == FunctionCategory.Aggregate)
			{
				this.m_inAggregateFunction = true;
				foreach (ExpressionNode expressionNode in node.Arguments)
				{
					if (!this.m_isAggregate)
					{
						this.Visit(expressionNode);
					}
				}
			}
			if (node.Descriptor.Name == "Evaluate")
			{
				IScope evaluateOutputScope = ExpressionAnalysisUtils.GetEvaluateOutputScope(node);
				if (evaluateOutputScope == null || !this.m_scopeTree.AreSameScope(this.m_originalContainingScope, evaluateOutputScope))
				{
					this.m_isAggregate |= this.m_inAggregateFunction;
				}
				if (!this.m_isAggregate)
				{
					this.Visit(node.Arguments[0]);
				}
			}
			ExpressionNode expressionNode2 = (this.m_isAggregate ? node : base.Visit(node));
			this.m_inAggregateFunction = inAggregateFunction;
			return expressionNode2;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001E78C File Offset: 0x0001C98C
		public override ExpressionNode Visit(ResolvedCalculationReferenceExpressionNode node)
		{
			this.m_isAggregate |= this.m_inAggregateFunction;
			if (!this.m_isAggregate)
			{
				return base.Visit(node);
			}
			return node;
		}

		// Token: 0x040003E8 RID: 1000
		private readonly IScope m_originalContainingScope;

		// Token: 0x040003E9 RID: 1001
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040003EA RID: 1002
		private bool m_inAggregateFunction;

		// Token: 0x040003EB RID: 1003
		private bool m_isAggregate;
	}
}
