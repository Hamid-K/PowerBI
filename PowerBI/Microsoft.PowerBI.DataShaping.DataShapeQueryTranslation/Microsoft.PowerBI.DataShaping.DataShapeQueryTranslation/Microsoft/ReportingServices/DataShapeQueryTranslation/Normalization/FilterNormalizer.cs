using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery.Expressions;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Normalization
{
	// Token: 0x02000099 RID: 153
	internal sealed class FilterNormalizer : FilterExpressionVisitor
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x0001B338 File Offset: 0x00019538
		private FilterNormalizer(IScope filterTarget, ScopeTree scopeTree, WritableExpressionTable expressionTable)
			: base(null)
		{
			this.m_filterTarget = filterTarget;
			this.m_scopeTree = scopeTree;
			this.m_expressionTable = expressionTable;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001B358 File Offset: 0x00019558
		public static void Normalize(Filter filter, ScopeTree scopeTree, WritableExpressionTable expressionTable)
		{
			IScope targetScope = FilterNormalizer.GetTargetScope(filter, scopeTree, expressionTable);
			if (targetScope == null)
			{
				return;
			}
			new FilterNormalizer(targetScope, scopeTree, expressionTable).Visit(filter);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001B381 File Offset: 0x00019581
		private static IScope GetTargetScope(Filter filter, ScopeTree scopeTree, ExpressionTable expressionTable)
		{
			return ((ResolvedStructureReferenceExpressionNode)expressionTable.GetNode(filter.Target)).Target as IScope;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001B39E File Offset: 0x0001959E
		internal override void VisitExpression(Expression expression, FilterCondition owner, string propertyName)
		{
			NormalizationUtils.RewriteSubtotalToEvalRollup(this.m_filterTarget, expression, this.m_expressionTable, this.m_scopeTree);
		}

		// Token: 0x04000376 RID: 886
		private readonly IScope m_filterTarget;

		// Token: 0x04000377 RID: 887
		private readonly ScopeTree m_scopeTree;

		// Token: 0x04000378 RID: 888
		private readonly WritableExpressionTable m_expressionTable;
	}
}
