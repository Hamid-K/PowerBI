using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015A3 RID: 5539
	internal class OptimizingQueryVisitor : QueryVisitor
	{
		// Token: 0x06008AED RID: 35565 RVA: 0x001D39EC File Offset: 0x001D1BEC
		public Query Optimize(Query query)
		{
			Query query2 = this.VisitQuery(query);
			if (query2 != query)
			{
				return new RetryQuery(query2, query);
			}
			return query2;
		}

		// Token: 0x06008AEE RID: 35566 RVA: 0x001D3A10 File Offset: 0x001D1C10
		public override Query VisitQuery(Query query)
		{
			Query query2;
			try
			{
				query2 = base.VisitQuery(query);
			}
			catch (NotSupportedException ex)
			{
				this.ReportOptimizationFailure(ex);
				query2 = query;
			}
			return query2;
		}

		// Token: 0x06008AEF RID: 35567 RVA: 0x001D3A44 File Offset: 0x001D1C44
		protected override Query VisitDataSource(DataSourceQuery query)
		{
			OptimizableQuery optimizableQuery = query as OptimizableQuery;
			if (optimizableQuery != null)
			{
				return optimizableQuery.Query;
			}
			RetryQuery retryQuery = query as RetryQuery;
			if (retryQuery != null)
			{
				return this.VisitQuery(retryQuery.Query);
			}
			return query;
		}

		// Token: 0x06008AF0 RID: 35568 RVA: 0x001D3A7C File Offset: 0x001D1C7C
		protected override Query VisitSelectColumns(ProjectColumnsQuery query)
		{
			if (query.RenameReorder)
			{
				return this.VisitQuery(query.InnerQuery, (Query q) => q.RenameReorderColumns(query.ColumnSelection));
			}
			return this.VisitQuery(query.InnerQuery, (Query q) => q.SelectColumns(query.ColumnSelection));
		}

		// Token: 0x06008AF1 RID: 35569 RVA: 0x001D3AE0 File Offset: 0x001D1CE0
		protected override Query VisitSelectRows(SelectRowsQuery query)
		{
			return this.VisitQuery(query.InnerQuery, (Query q) => q.SelectRows(query.Condition));
		}

		// Token: 0x06008AF2 RID: 35570 RVA: 0x001D3B18 File Offset: 0x001D1D18
		protected override Query VisitSkipTake(SkipTakeQuery query)
		{
			return this.VisitQuery(query.InnerQuery, (Query q) => q.Skip(query.RowRange.SkipCount).Take(query.RowRange.TakeCount));
		}

		// Token: 0x06008AF3 RID: 35571 RVA: 0x001D3B50 File Offset: 0x001D1D50
		protected override Query VisitAddColumns(AddColumnsQuery query)
		{
			return this.VisitQuery(query.InnerQuery, (Query q) => q.AddColumns(query.ColumnsConstructor));
		}

		// Token: 0x06008AF4 RID: 35572 RVA: 0x001D3B88 File Offset: 0x001D1D88
		protected override Query VisitDistinct(DistinctQuery query)
		{
			return this.VisitQuery(query.InnerQuery, (Query q) => q.Distinct(query.DistinctCriteria));
		}

		// Token: 0x06008AF5 RID: 35573 RVA: 0x001D3BC0 File Offset: 0x001D1DC0
		protected override Query VisitSort(SortQuery query)
		{
			return this.VisitQuery(query.InnerQuery, (Query q) => q.Sort(query.SortOrder).Take(query.TakeCount));
		}

		// Token: 0x06008AF6 RID: 35574 RVA: 0x001D3BF8 File Offset: 0x001D1DF8
		protected override Query VisitGroup(GroupQuery query)
		{
			return this.VisitQuery(query.InnerQuery, (Query q) => q.Group(query.Grouping));
		}

		// Token: 0x06008AF7 RID: 35575 RVA: 0x001D3C30 File Offset: 0x001D1E30
		protected override Query VisitExpandListColumn(ExpandListColumnQuery query)
		{
			return this.VisitQuery(query.InnerQuery, (Query q) => q.ExpandListColumn(query.ColumnIndex, query.SingleOrDefault));
		}

		// Token: 0x06008AF8 RID: 35576 RVA: 0x001D3C68 File Offset: 0x001D1E68
		protected override Query VisitExpandRecordColumn(ExpandRecordColumnQuery query)
		{
			return this.VisitQuery(query.InnerQuery, (Query q) => q.ExpandRecordColumn(query.ColumnToExpand, query.FieldsToProject, query.NewColumns));
		}

		// Token: 0x06008AF9 RID: 35577 RVA: 0x001D3CA0 File Offset: 0x001D1EA0
		protected override Query VisitNestedJoin(NestedJoinQuery query)
		{
			return this.VisitQuery(query.LeftQuery, (Query q) => q.NestedJoin(query.LeftKeyColumns, query.RightTable, query.RightKey, query.JoinKind, query.NewColumnName, query.JoinKeys, query.KeyEqualityComparers));
		}

		// Token: 0x06008AFA RID: 35578 RVA: 0x001D3CD8 File Offset: 0x001D1ED8
		protected override Query VisitPivot(PivotQuery query)
		{
			return this.VisitQuery(query.InnerQuery, (Query q) => q.Pivot(query.InputType, query.OutputType, query.PivotValues, query.AttributeColumn, query.ValueColumn, query.AggregateFunction));
		}

		// Token: 0x06008AFB RID: 35579 RVA: 0x001D3D10 File Offset: 0x001D1F10
		protected override Query VisitUnpivot(UnpivotQuery query)
		{
			return this.VisitQuery(query.InnerQuery, (Query q) => q.Unpivot(query.InputType, query.OutputType, query.PivotValues, query.AttributeColumn, query.ValueColumn));
		}

		// Token: 0x06008AFC RID: 35580 RVA: 0x001D3D48 File Offset: 0x001D1F48
		protected Query VisitQuery(Query query, Func<Query, Query> operation)
		{
			query = this.VisitQuery(query);
			Query query2;
			try
			{
				query2 = operation(query);
			}
			catch (NotSupportedException ex)
			{
				this.ReportOptimizationFailure(ex);
				query2 = operation(new OptimizingQueryVisitor.OptimizedQuery(query));
			}
			return query2;
		}

		// Token: 0x06008AFD RID: 35581 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void ReportOptimizationFailure(NotSupportedException ex)
		{
		}

		// Token: 0x020015A4 RID: 5540
		private class OptimizedQuery : DataSourceQuery
		{
			// Token: 0x06008AFF RID: 35583 RVA: 0x001D3D90 File Offset: 0x001D1F90
			public OptimizedQuery(Query innerQuery)
			{
				this.innerQuery = innerQuery;
			}

			// Token: 0x170024A1 RID: 9377
			// (get) Token: 0x06008B00 RID: 35584 RVA: 0x001D3D9F File Offset: 0x001D1F9F
			public override Keys Columns
			{
				get
				{
					return this.innerQuery.Columns;
				}
			}

			// Token: 0x06008B01 RID: 35585 RVA: 0x001D3DAC File Offset: 0x001D1FAC
			public override TypeValue GetColumnType(int column)
			{
				return this.innerQuery.GetColumnType(column);
			}

			// Token: 0x170024A2 RID: 9378
			// (get) Token: 0x06008B02 RID: 35586 RVA: 0x001D3DBA File Offset: 0x001D1FBA
			public override IList<TableKey> TableKeys
			{
				get
				{
					return this.innerQuery.TableKeys;
				}
			}

			// Token: 0x170024A3 RID: 9379
			// (get) Token: 0x06008B03 RID: 35587 RVA: 0x001D3DC7 File Offset: 0x001D1FC7
			public override IList<ComputedColumn> ComputedColumns
			{
				get
				{
					return this.innerQuery.ComputedColumns;
				}
			}

			// Token: 0x170024A4 RID: 9380
			// (get) Token: 0x06008B04 RID: 35588 RVA: 0x001D3DD4 File Offset: 0x001D1FD4
			public override RowCount RowCount
			{
				get
				{
					return this.innerQuery.RowCount;
				}
			}

			// Token: 0x170024A5 RID: 9381
			// (get) Token: 0x06008B05 RID: 35589 RVA: 0x001D3DE1 File Offset: 0x001D1FE1
			public override TableSortOrder SortOrder
			{
				get
				{
					return this.innerQuery.SortOrder;
				}
			}

			// Token: 0x170024A6 RID: 9382
			// (get) Token: 0x06008B06 RID: 35590 RVA: 0x001D3DEE File Offset: 0x001D1FEE
			public override IEngineHost EngineHost
			{
				get
				{
					return this.innerQuery.GetEngineHost();
				}
			}

			// Token: 0x06008B07 RID: 35591 RVA: 0x001D3DFB File Offset: 0x001D1FFB
			public override Query Unordered()
			{
				if (this.innerQuery.SortOrder == TableSortOrder.None)
				{
					return this;
				}
				return new OptimizingQueryVisitor.OptimizedQuery(this.innerQuery.Unordered());
			}

			// Token: 0x06008B08 RID: 35592 RVA: 0x001D3E21 File Offset: 0x001D2021
			public override bool TryGetReader(out IPageReader reader)
			{
				return this.innerQuery.TryGetReader(out reader);
			}

			// Token: 0x06008B09 RID: 35593 RVA: 0x001D3E2F File Offset: 0x001D202F
			public override IEnumerable<IValueReference> GetRows()
			{
				return this.innerQuery.GetRows();
			}

			// Token: 0x06008B0A RID: 35594 RVA: 0x001D3E3C File Offset: 0x001D203C
			public override bool TryGetExpression(out IExpression expression)
			{
				expression = QueryToExpressionVisitor.ToExpression(this.innerQuery);
				return true;
			}

			// Token: 0x04004C24 RID: 19492
			private readonly Query innerQuery;
		}
	}
}
