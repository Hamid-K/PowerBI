using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000D6 RID: 214
	internal sealed class ScopeFilterExpressionTranslator : FilterVisitor<FilterCondition>
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x00022784 File Offset: 0x00020984
		private ScopeFilterExpressionTranslator(Func<DataIntersection, ScopeTree, IScope> getIntersectionScope, ScopeTree scopeTree, WritableExpressionTable expressionTable)
			: base(null)
		{
			this.m_scopeTree = scopeTree;
			this.m_writableExpressionTable = expressionTable;
			this.m_getIntersectionScope = getIntersectionScope;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000227A2 File Offset: 0x000209A2
		public static void RewriteFilterExpression(Filter filter, Func<DataIntersection, ScopeTree, IScope> getIntersectionScope, ScopeTree scopeTree, WritableExpressionTable expressionTable)
		{
			new ScopeFilterExpressionTranslator(getIntersectionScope, scopeTree, expressionTable).Visit(filter);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x000227B3 File Offset: 0x000209B3
		internal override FilterCondition Visit(UnaryFilterCondition condition)
		{
			this.RewriteExpression(condition.Expression);
			return condition;
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x000227C2 File Offset: 0x000209C2
		internal override FilterCondition Visit(BinaryFilterCondition condition)
		{
			this.RewriteExpression(condition.LeftExpression);
			this.RewriteExpression(condition.RightExpression);
			return condition;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x000227DD File Offset: 0x000209DD
		internal override FilterCondition Visit(CompoundFilterCondition condition)
		{
			this.Visit(condition.Conditions, condition.ObjectType, "Conditions");
			return condition;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x000227F8 File Offset: 0x000209F8
		private void RewriteExpression(Expression expression)
		{
			ExpressionNode expressionNode = ContextFilterExpressionTreeTranslator.Translate(this.m_writableExpressionTable.GetNode(expression), this.m_getIntersectionScope, this.m_scopeTree);
			this.m_writableExpressionTable.SetNode(expression, expressionNode);
		}

		// Token: 0x04000439 RID: 1081
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400043A RID: 1082
		private readonly WritableExpressionTable m_writableExpressionTable;

		// Token: 0x0400043B RID: 1083
		private readonly Func<DataIntersection, ScopeTree, IScope> m_getIntersectionScope;
	}
}
