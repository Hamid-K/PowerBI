using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000089 RID: 137
	internal sealed class ResolvedQueryDsqRewriter : ResolvedQueryDefinitionRewriter
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x00013D1B File Offset: 0x00011F1B
		private ResolvedQueryDsqRewriter(ResolvedQueryExpressionDsqRewriter expressionRewriter)
			: base(expressionRewriter.AsEnumerable<ResolvedQueryExpressionDsqRewriter>())
		{
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00013D2C File Offset: 0x00011F2C
		internal static ResolvedQueryDefinition RewriteQuery(ResolvedQueryDefinition query)
		{
			ResolvedQueryExpressionDsqRewriter resolvedQueryExpressionDsqRewriter = new ResolvedQueryExpressionDsqRewriter();
			ResolvedQueryDsqRewriter resolvedQueryDsqRewriter = new ResolvedQueryDsqRewriter(resolvedQueryExpressionDsqRewriter);
			resolvedQueryExpressionDsqRewriter.SetDefinitionRewriter(resolvedQueryDsqRewriter);
			return resolvedQueryDsqRewriter.Rewrite(query);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00013D54 File Offset: 0x00011F54
		internal override ResolvedQueryDefinition Rewrite(ResolvedQueryDefinition query)
		{
			this._queryParentCount++;
			ResolvedQueryDefinition resolvedQueryDefinition = base.Rewrite(query);
			this._queryParentCount--;
			if (this._queryParentCount > 0)
			{
				return ResolvedQueryDsqRewriter.ProjectOrderBy(resolvedQueryDefinition);
			}
			return resolvedQueryDefinition;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00013D98 File Offset: 0x00011F98
		internal static ResolvedQueryDefinition ProjectOrderBy(ResolvedQueryDefinition query)
		{
			if (query.OrderBy.IsNullOrEmpty<ResolvedQuerySortClause>())
			{
				return query;
			}
			List<ResolvedQuerySelect> list = new List<ResolvedQuerySelect>(query.Select);
			foreach (ResolvedQuerySortClause resolvedQuerySortClause in query.OrderBy)
			{
				ResolvedQueryExpression orderByExpression = resolvedQuerySortClause.Expression;
				if (!list.Any((ResolvedQuerySelect select) => select.Expression.Equals(orderByExpression)))
				{
					list.Add(new ResolvedQuerySelect(resolvedQuerySortClause.Expression, null, null));
				}
			}
			return new ResolvedQueryDefinition(query.Parameters, query.Let, query.From, query.Where, query.Transform, query.OrderBy, list, query.VisualShape, query.GroupBy, query.Top, query.Skip, query.Name);
		}

		// Token: 0x040002F4 RID: 756
		private int _queryParentCount;
	}
}
