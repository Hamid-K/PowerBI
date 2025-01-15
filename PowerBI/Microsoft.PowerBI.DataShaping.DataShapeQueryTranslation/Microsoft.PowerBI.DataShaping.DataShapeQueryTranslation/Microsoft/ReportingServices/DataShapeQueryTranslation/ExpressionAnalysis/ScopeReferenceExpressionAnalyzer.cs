using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000B6 RID: 182
	internal sealed class ScopeReferenceExpressionAnalyzer : ExpressionNodeTreeTransform
	{
		// Token: 0x060007E2 RID: 2018 RVA: 0x0001E5D2 File Offset: 0x0001C7D2
		private ScopeReferenceExpressionAnalyzer(ScopeTree scopeTree)
			: base(false)
		{
			this.m_scopeTree = scopeTree;
			this.m_scopes = new List<IScope>();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0001E5ED File Offset: 0x0001C7ED
		internal static IReadOnlyList<IScope> CollectReferencedScopes(ExpressionNode node, ScopeTree scopeTree)
		{
			ScopeReferenceExpressionAnalyzer scopeReferenceExpressionAnalyzer = new ScopeReferenceExpressionAnalyzer(scopeTree);
			scopeReferenceExpressionAnalyzer.Visit(node);
			return scopeReferenceExpressionAnalyzer.m_scopes;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0001E604 File Offset: 0x0001C804
		public override ExpressionNode Visit(ResolvedCalculationReferenceExpressionNode node)
		{
			IScope containingScope = this.m_scopeTree.GetContainingScope(node.Calculation);
			this.m_scopes.Add(containingScope);
			return base.Visit(node);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0001E638 File Offset: 0x0001C838
		public override ExpressionNode Visit(ResolvedScopeReferenceExpressionNode node)
		{
			IScope canonicalScope = this.m_scopeTree.GetCanonicalScope(node.Scope.Id);
			this.m_scopes.Add(canonicalScope);
			return base.Visit(node);
		}

		// Token: 0x040003E6 RID: 998
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040003E7 RID: 999
		private readonly List<IScope> m_scopes;
	}
}
