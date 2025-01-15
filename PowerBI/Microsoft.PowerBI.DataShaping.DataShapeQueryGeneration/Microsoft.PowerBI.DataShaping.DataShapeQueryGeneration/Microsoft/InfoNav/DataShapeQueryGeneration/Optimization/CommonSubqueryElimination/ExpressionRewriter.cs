using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryExpressionBuilder;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.Optimization.CommonSubqueryElimination
{
	// Token: 0x020000FF RID: 255
	internal sealed class ExpressionRewriter : ResolvedQueryExpressionRewriter
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x00021CBC File Offset: 0x0001FEBC
		internal ExpressionRewriter(IReadOnlyDictionary<ResolvedQueryDefinition, ResolvedQueryLetRefExpression> queryRewriteMapping)
		{
			this._queryRewriteMapping = queryRewriteMapping;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00021CCB File Offset: 0x0001FECB
		internal void SetDefinitionRewriter(ResolvedQueryDefinitionRewriter definitionRewriter)
		{
			this._definitionRewriter = definitionRewriter;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00021CD4 File Offset: 0x0001FED4
		public override ResolvedQueryExpression Visit(ResolvedQuerySubqueryExpression expression)
		{
			ResolvedQueryDefinition subquery = expression.Subquery;
			ResolvedQueryLetRefExpression resolvedQueryLetRefExpression;
			if (this._queryRewriteMapping.TryGetValue(subquery, out resolvedQueryLetRefExpression))
			{
				return resolvedQueryLetRefExpression;
			}
			ResolvedQueryDefinition resolvedQueryDefinition = this._definitionRewriter.Rewrite(subquery);
			if (subquery != resolvedQueryDefinition)
			{
				return resolvedQueryDefinition.Subquery();
			}
			return expression;
		}

		// Token: 0x04000457 RID: 1111
		private readonly IReadOnlyDictionary<ResolvedQueryDefinition, ResolvedQueryLetRefExpression> _queryRewriteMapping;

		// Token: 0x04000458 RID: 1112
		private ResolvedQueryDefinitionRewriter _definitionRewriter;
	}
}
