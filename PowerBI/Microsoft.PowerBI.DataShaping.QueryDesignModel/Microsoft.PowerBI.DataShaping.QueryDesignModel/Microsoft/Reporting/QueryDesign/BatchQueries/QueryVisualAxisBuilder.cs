using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.BatchQueries
{
	// Token: 0x02000275 RID: 629
	internal sealed class QueryVisualAxisBuilder
	{
		// Token: 0x06001B2E RID: 6958 RVA: 0x0004C5D7 File Offset: 0x0004A7D7
		public QueryVisualAxisBuilder(QueryVisualAxisName name)
		{
			this._name = name;
			this._groupBuilders = new List<QueryVisualAxisGroupBuilder>();
			this._orderBy = new List<QuerySortClause>();
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x0004C5FC File Offset: 0x0004A7FC
		public QueryVisualAxisGroupBuilder AddGroup()
		{
			QueryVisualAxisGroupBuilder queryVisualAxisGroupBuilder = new QueryVisualAxisGroupBuilder();
			this._groupBuilders.Add(queryVisualAxisGroupBuilder);
			return queryVisualAxisGroupBuilder;
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x0004C61C File Offset: 0x0004A81C
		public void AddOrderBy(QuerySortClause sortClause)
		{
			this._orderBy.Add(sortClause);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x0004C62C File Offset: 0x0004A82C
		public QueryVisualAxis ToVisualAxis(Func<QueryExpression, QueryExpression> rewriteExpression)
		{
			this._groupBuilders.RemoveDuplicatedColumnsAndEmptyGroups(null);
			IReadOnlyList<QueryVisualAxisGroup> readOnlyList = this._groupBuilders.CreateList((QueryVisualAxisGroupBuilder b) => b.ToVisualAxisGroup(rewriteExpression));
			HashSet<QuerySortClause> hashSet = new HashSet<QuerySortClause>();
			List<QuerySortClause> list = new List<QuerySortClause>(this._orderBy.Count);
			foreach (QuerySortClause querySortClause in this._orderBy)
			{
				if (hashSet.Add(querySortClause))
				{
					list.Add(DefaultExpressionVisitor.VisitSortClause(querySortClause, rewriteExpression));
				}
			}
			return new QueryVisualAxis(this._name, readOnlyList, list);
		}

		// Token: 0x04000EE9 RID: 3817
		private readonly QueryVisualAxisName _name;

		// Token: 0x04000EEA RID: 3818
		private readonly List<QueryVisualAxisGroupBuilder> _groupBuilders;

		// Token: 0x04000EEB RID: 3819
		private readonly List<QuerySortClause> _orderBy;
	}
}
