using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.ExpressionAnalysis
{
	// Token: 0x020000B8 RID: 184
	internal sealed class SubqueryCalculationReferenceAnalyzer : ExpressionNodeTreeTransform
	{
		// Token: 0x060007EA RID: 2026 RVA: 0x0001E7B2 File Offset: 0x0001C9B2
		private SubqueryCalculationReferenceAnalyzer(DataShape containingDataShape, ScopeTree scopeTree)
			: base(false)
		{
			this.m_originalContainingDataShape = containingDataShape;
			this.m_scopeTree = scopeTree;
			this.m_referencedCalculations = null;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0001E7D0 File Offset: 0x0001C9D0
		public static bool TryGetReferencedSubqueryDataShape(ExpressionNode node, DataShape containingDataShape, ScopeTree scopeTree, out DataShape subqueryDatashape, out IReadOnlyList<Calculation> referencedCalculations)
		{
			SubqueryCalculationReferenceAnalyzer subqueryCalculationReferenceAnalyzer = new SubqueryCalculationReferenceAnalyzer(containingDataShape, scopeTree);
			subqueryCalculationReferenceAnalyzer.Visit(node);
			subqueryDatashape = subqueryCalculationReferenceAnalyzer.m_subqueryDataShape;
			referencedCalculations = subqueryCalculationReferenceAnalyzer.m_referencedCalculations;
			return subqueryCalculationReferenceAnalyzer.m_subqueryDataShape != null;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0001E808 File Offset: 0x0001CA08
		public override ExpressionNode Visit(ResolvedCalculationReferenceExpressionNode node)
		{
			IScope containingScope = this.m_scopeTree.GetContainingScope(node.Calculation);
			DataShape containingDataShapeOrSelf = this.m_scopeTree.GetContainingDataShapeOrSelf(containingScope);
			if (this.m_subqueryDataShape == null && this.m_originalContainingDataShape != containingDataShapeOrSelf)
			{
				this.m_subqueryDataShape = containingDataShapeOrSelf;
				Util.AddToLazyList<Calculation>(ref this.m_referencedCalculations, node.Calculation);
			}
			return base.Visit(node);
		}

		// Token: 0x040003EC RID: 1004
		private readonly DataShape m_originalContainingDataShape;

		// Token: 0x040003ED RID: 1005
		private readonly ScopeTree m_scopeTree;

		// Token: 0x040003EE RID: 1006
		private DataShape m_subqueryDataShape;

		// Token: 0x040003EF RID: 1007
		private List<Calculation> m_referencedCalculations;
	}
}
