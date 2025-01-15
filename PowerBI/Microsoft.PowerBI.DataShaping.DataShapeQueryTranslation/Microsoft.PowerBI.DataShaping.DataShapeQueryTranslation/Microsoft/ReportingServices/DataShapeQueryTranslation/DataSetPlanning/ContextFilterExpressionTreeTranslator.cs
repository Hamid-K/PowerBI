using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000D7 RID: 215
	internal sealed class ContextFilterExpressionTreeTranslator : ExpressionNodeTreeTransform
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x00022830 File Offset: 0x00020A30
		private ContextFilterExpressionTreeTranslator(ExpressionNode expressionNode, Func<DataIntersection, ScopeTree, IScope> getIntersectionScope, ScopeTree scopeTree)
			: base(false)
		{
			this.m_expressionNode = expressionNode;
			this.m_getIntersectionScope = getIntersectionScope;
			this.m_scopeTree = scopeTree;
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0002284E File Offset: 0x00020A4E
		public static ExpressionNode Translate(ExpressionNode node, Func<DataIntersection, ScopeTree, IScope> getIntersectionScope, ScopeTree scopeTree)
		{
			return new ContextFilterExpressionTreeTranslator(node, getIntersectionScope, scopeTree).Visit(node);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0002285E File Offset: 0x00020A5E
		public override ExpressionNode Visit(ResolvedScopeReferenceExpressionNode node)
		{
			if (node.Scope.ObjectType == ObjectType.DataIntersection)
			{
				return new ResolvedScopeReferenceExpressionNode(this.m_getIntersectionScope((DataIntersection)node.Scope, this.m_scopeTree));
			}
			return node;
		}

		// Token: 0x0400043C RID: 1084
		private readonly ExpressionNode m_expressionNode;

		// Token: 0x0400043D RID: 1085
		private readonly Func<DataIntersection, ScopeTree, IScope> m_getIntersectionScope;

		// Token: 0x0400043E RID: 1086
		private readonly ScopeTree m_scopeTree;
	}
}
