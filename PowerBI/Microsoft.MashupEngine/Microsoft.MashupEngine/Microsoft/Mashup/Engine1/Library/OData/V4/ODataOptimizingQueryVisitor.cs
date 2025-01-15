using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x0200086E RID: 2158
	internal class ODataOptimizingQueryVisitor : OptimizingQueryVisitor
	{
		// Token: 0x06003E31 RID: 15921 RVA: 0x000CB44F File Offset: 0x000C964F
		public ODataOptimizingQueryVisitor()
		{
			this.expandedColumns = new List<ODataExpandedColumn>();
			this.selectRowsOnExpandedColumn = new List<SelectRowsQuery>();
			this.allSelectRowsExpressions = new Dictionary<SelectRowsQuery, List<QueryExpression>>();
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x000CB478 File Offset: 0x000C9678
		protected override Query VisitDataSource(DataSourceQuery query)
		{
			Query query2 = base.VisitDataSource(query);
			ODataQuery odataQuery = query2 as ODataQuery;
			if (odataQuery != null)
			{
				return new RetryQuery(new ODataQuery(odataQuery, odataQuery.QueryColumn.ExpandColumn(this.expandedColumns)), query);
			}
			return query2;
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x000CB4B8 File Offset: 0x000C96B8
		protected override Query VisitSelectRows(SelectRowsQuery query)
		{
			this.selectRowsOnExpandedColumn.Add(query);
			List<QueryExpression> list;
			if (!this.allSelectRowsExpressions.TryGetValue(query, out list))
			{
				List<QueryExpression> list2 = new List<QueryExpression>();
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewRowType(query), query.Condition);
				list2.AddRange(SelectRowsQuery.GetConjunctiveNF(queryExpression));
				this.allSelectRowsExpressions[query] = list2;
			}
			return base.VisitSelectRows(query);
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x000CB51C File Offset: 0x000C971C
		protected override Query VisitExpandRecordColumn(ExpandRecordColumnQuery query)
		{
			IList<ODataExpandedColumn> list = new List<ODataExpandedColumn>();
			IList<ODataExpandedColumn> list2 = new List<ODataExpandedColumn>();
			foreach (ODataExpandedColumn odataExpandedColumn in this.expandedColumns)
			{
				int num = query.NewColumns.IndexOfKey(odataExpandedColumn.ColumnToExpandName);
				if (num != -1)
				{
					list2.Add(new ODataExpandedColumn(num, odataExpandedColumn.ColumnToExpandName, odataExpandedColumn.NewColumns, odataExpandedColumn.FieldsToProject, odataExpandedColumn.InnerExpandedColumns, odataExpandedColumn.SelectQueryExpressions));
					list.Add(odataExpandedColumn);
				}
			}
			foreach (ODataExpandedColumn odataExpandedColumn2 in list)
			{
				this.expandedColumns.Remove(odataExpandedColumn2);
			}
			ODataExpandedColumn odataExpandedColumn3 = new ODataExpandedColumn(query.ColumnToExpand, query.InnerQuery.Columns[query.ColumnToExpand], query.NewColumns, query.FieldsToProject, list2, this.allSelectRowsExpressions);
			this.expandedColumns.Add(odataExpandedColumn3);
			return base.VisitExpandRecordColumn(query);
		}

		// Token: 0x040020B7 RID: 8375
		private readonly IList<ODataExpandedColumn> expandedColumns;

		// Token: 0x040020B8 RID: 8376
		private readonly IList<SelectRowsQuery> selectRowsOnExpandedColumn;

		// Token: 0x040020B9 RID: 8377
		private Dictionary<SelectRowsQuery, List<QueryExpression>> allSelectRowsExpressions;
	}
}
