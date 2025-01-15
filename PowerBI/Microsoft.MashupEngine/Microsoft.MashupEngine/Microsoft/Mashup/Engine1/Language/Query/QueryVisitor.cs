using System;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200189D RID: 6301
	internal abstract class QueryVisitor
	{
		// Token: 0x06009FD6 RID: 40918 RVA: 0x00210428 File Offset: 0x0020E628
		public virtual Query VisitQuery(Query query)
		{
			switch (query.Kind)
			{
			case QueryKind.DataSource:
				return this.VisitDataSource((DataSourceQuery)query);
			case QueryKind.ProjectColumns:
				return this.VisitSelectColumns((ProjectColumnsQuery)query);
			case QueryKind.SelectRows:
				return this.VisitSelectRows((SelectRowsQuery)query);
			case QueryKind.AddColumns:
				return this.VisitAddColumns((AddColumnsQuery)query);
			case QueryKind.SkipTake:
				return this.VisitSkipTake((SkipTakeQuery)query);
			case QueryKind.Sort:
				return this.VisitSort((SortQuery)query);
			case QueryKind.Distinct:
				return this.VisitDistinct((DistinctQuery)query);
			case QueryKind.Combine:
				return this.VisitCombine((CombineQuery)query);
			case QueryKind.Group:
				return this.VisitGroup((GroupQuery)query);
			case QueryKind.Join:
				return this.VisitJoin((JoinQuery)query);
			case QueryKind.NestedJoin:
				return this.VisitNestedJoin((NestedJoinQuery)query);
			case QueryKind.ExpandListColumn:
				return this.VisitExpandListColumn((ExpandListColumnQuery)query);
			case QueryKind.ExpandRecordColumn:
				return this.VisitExpandRecordColumn((ExpandRecordColumnQuery)query);
			case QueryKind.Unpivot:
				return this.VisitUnpivot((UnpivotQuery)query);
			case QueryKind.Pivot:
				return this.VisitPivot((PivotQuery)query);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06009FD7 RID: 40919 RVA: 0x0021054B File Offset: 0x0020E74B
		protected virtual Query NewAddColumns(ColumnsConstructor columnsConstructor, Query innerQuery)
		{
			return new AddColumnsQuery(columnsConstructor, innerQuery);
		}

		// Token: 0x06009FD8 RID: 40920 RVA: 0x00210554 File Offset: 0x0020E754
		protected virtual Query NewSelectColumns(ColumnSelection columnSelection, Query innerQuery)
		{
			return SelectColumnsQuery.New(columnSelection, innerQuery);
		}

		// Token: 0x06009FD9 RID: 40921 RVA: 0x0021055D File Offset: 0x0020E75D
		protected virtual Query NewRenameReorderColumns(ColumnSelection columnSelection, Query innerQuery)
		{
			return RenameReorderColumnsQuery.New(columnSelection, innerQuery);
		}

		// Token: 0x06009FDA RID: 40922 RVA: 0x00210566 File Offset: 0x0020E766
		protected virtual Query NewSkipTake(RowRange rowRange, Query innerQuery)
		{
			return this.NewSkipTake(rowRange, false, innerQuery);
		}

		// Token: 0x06009FDB RID: 40923 RVA: 0x00210571 File Offset: 0x0020E771
		protected virtual Query NewSkipTake(RowRange rowRange, bool floating, Query innerQuery)
		{
			return new SkipTakeQuery(rowRange, innerQuery, floating);
		}

		// Token: 0x06009FDC RID: 40924 RVA: 0x0021057B File Offset: 0x0020E77B
		protected virtual Query NewSelectRows(FunctionValue condition, Query innerQuery)
		{
			return SelectRowsQuery.New(condition, innerQuery);
		}

		// Token: 0x06009FDD RID: 40925 RVA: 0x00210584 File Offset: 0x0020E784
		protected virtual Query NewSort(TableSortOrder sortOrder, RowCount takeCount, Query innerQuery)
		{
			return SortQuery.New(sortOrder, takeCount, innerQuery);
		}

		// Token: 0x06009FDE RID: 40926 RVA: 0x0021058E File Offset: 0x0020E78E
		protected virtual Query NewDistinct(TableDistinct distinctCriteria, Query innerQuery)
		{
			return this.NewDistinct(distinctCriteria, false, innerQuery);
		}

		// Token: 0x06009FDF RID: 40927 RVA: 0x00210599 File Offset: 0x0020E799
		protected virtual Query NewDistinct(TableDistinct distinctCriteria, bool floating, Query innerQuery)
		{
			return DistinctQuery.New(distinctCriteria, innerQuery, floating);
		}

		// Token: 0x06009FE0 RID: 40928 RVA: 0x002105A3 File Offset: 0x0020E7A3
		protected virtual Query NewCombine(Query[] queries, TypeValue[] columnTypes, TableSortOrder sortOrder, int disjointColumn, RowCount takeCount)
		{
			return new CombineQuery(queries, columnTypes, sortOrder, disjointColumn, takeCount);
		}

		// Token: 0x06009FE1 RID: 40929 RVA: 0x002105B1 File Offset: 0x0020E7B1
		protected virtual Query NewGroup(Grouping grouping, Query innerQuery)
		{
			return this.NewGroup(grouping, false, innerQuery);
		}

		// Token: 0x06009FE2 RID: 40930 RVA: 0x002105BC File Offset: 0x0020E7BC
		protected virtual Query NewGroup(Grouping grouping, bool floating, Query innerQuery)
		{
			return new GroupQuery(grouping, innerQuery, floating);
		}

		// Token: 0x06009FE3 RID: 40931 RVA: 0x002105C8 File Offset: 0x0020E7C8
		protected virtual Query NewJoin(RowCount take, Query leftQuery, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers)
		{
			return new JoinQuery(take, leftQuery, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers);
		}

		// Token: 0x06009FE4 RID: 40932 RVA: 0x002105EC File Offset: 0x0020E7EC
		protected virtual Query NewNestedJoin(Query leftQuery, int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumnName, Keys joinKeys, FunctionValue[] keyEqualityComparers, TypeValue columnType)
		{
			return new NestedJoinQuery(leftQuery, leftKeyColumns, rightTable, rightKey, joinKind, newColumnName, joinKeys, keyEqualityComparers, columnType);
		}

		// Token: 0x06009FE5 RID: 40933 RVA: 0x0021060D File Offset: 0x0020E80D
		protected virtual Query NewExpandListColumn(int columnIndex, bool singleOrDefault, TypeValue columnType, Query innerQuery)
		{
			return new ExpandListColumnQuery(columnIndex, singleOrDefault, columnType, innerQuery);
		}

		// Token: 0x06009FE6 RID: 40934 RVA: 0x00210619 File Offset: 0x0020E819
		protected virtual Query NewExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, TypeValue[] columnTypes, Query innerQuery)
		{
			return new ExpandRecordColumnQuery(columnToExpand, fieldsToProject, newColumns, columnTypes, innerQuery);
		}

		// Token: 0x06009FE7 RID: 40935 RVA: 0x00210627 File Offset: 0x0020E827
		protected virtual Query NewPivot(Query innerQuery, TableTypeValue inputTableType, TableTypeValue outputTableType, string[] pivotValues, string attributeColumn, string valueColumn, FunctionValue aggregateFunction)
		{
			return PivotQuery.New(innerQuery, inputTableType, outputTableType, pivotValues, attributeColumn, valueColumn, aggregateFunction);
		}

		// Token: 0x06009FE8 RID: 40936 RVA: 0x00210639 File Offset: 0x0020E839
		protected virtual Query NewUnpivot(Query innerQuery, TableTypeValue inputTableType, TableTypeValue outputTableType, string[] toPivot, string attributeColumn, string valueColumn)
		{
			return UnpivotQuery.New(innerQuery, inputTableType, outputTableType, toPivot, attributeColumn, valueColumn);
		}

		// Token: 0x06009FE9 RID: 40937 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual Query VisitDataSource(DataSourceQuery query)
		{
			return query;
		}

		// Token: 0x06009FEA RID: 40938 RVA: 0x0021064C File Offset: 0x0020E84C
		protected virtual Query VisitAddColumns(AddColumnsQuery query)
		{
			Query query2 = this.VisitQuery(query.InnerQuery);
			if (query2 == query.InnerQuery)
			{
				return query;
			}
			return this.NewAddColumns(query.ColumnsConstructor, query2);
		}

		// Token: 0x06009FEB RID: 40939 RVA: 0x00210680 File Offset: 0x0020E880
		protected virtual Query VisitSelectColumns(ProjectColumnsQuery query)
		{
			Query query2 = this.VisitQuery(query.InnerQuery);
			if (query2 == query.InnerQuery)
			{
				return query;
			}
			if (query.RenameReorder)
			{
				return this.NewRenameReorderColumns(query.ColumnSelection, query2);
			}
			return this.NewSelectColumns(query.ColumnSelection, query2);
		}

		// Token: 0x06009FEC RID: 40940 RVA: 0x002106C8 File Offset: 0x0020E8C8
		protected virtual Query VisitSkipTake(SkipTakeQuery query)
		{
			Query query2 = this.VisitQuery(query.InnerQuery);
			if (query2 == query.InnerQuery)
			{
				return query;
			}
			return this.NewSkipTake(query.RowRange, query.Floating, query2);
		}

		// Token: 0x06009FED RID: 40941 RVA: 0x00210700 File Offset: 0x0020E900
		protected virtual Query VisitSelectRows(SelectRowsQuery query)
		{
			Query query2 = this.VisitQuery(query.InnerQuery);
			if (query2 == query.InnerQuery)
			{
				return query;
			}
			return this.NewSelectRows(query.Condition, query2);
		}

		// Token: 0x06009FEE RID: 40942 RVA: 0x00210734 File Offset: 0x0020E934
		protected virtual Query VisitSort(SortQuery query)
		{
			Query query2 = this.VisitQuery(query.InnerQuery);
			if (query2 == query.InnerQuery)
			{
				return query;
			}
			return this.NewSort(query.SortOrder, query.TakeCount, query2);
		}

		// Token: 0x06009FEF RID: 40943 RVA: 0x0021076C File Offset: 0x0020E96C
		protected virtual Query VisitDistinct(DistinctQuery query)
		{
			Query query2 = this.VisitQuery(query.InnerQuery);
			if (query2 == query.InnerQuery)
			{
				return query;
			}
			return this.NewDistinct(query.DistinctCriteria, query.Floating, query2);
		}

		// Token: 0x06009FF0 RID: 40944 RVA: 0x002107A4 File Offset: 0x0020E9A4
		protected virtual Query VisitCombine(CombineQuery query)
		{
			bool flag = true;
			Query[] array = new Query[query.Queries.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Query query2 = this.VisitQuery(query.Queries[i]);
				flag = flag && query2 == query.Queries[i];
				array[i] = query2;
			}
			if (!flag)
			{
				return this.NewCombine(array, query.ColumnTypes, query.SortOrder, query.DisjointColumn, query.TakeCount);
			}
			return query;
		}

		// Token: 0x06009FF1 RID: 40945 RVA: 0x00210818 File Offset: 0x0020EA18
		protected virtual Query VisitGroup(GroupQuery query)
		{
			Query query2 = this.VisitQuery(query.InnerQuery);
			if (query2 == query.InnerQuery)
			{
				return query;
			}
			return this.NewGroup(query.Grouping, query.Floating, query2);
		}

		// Token: 0x06009FF2 RID: 40946 RVA: 0x00210850 File Offset: 0x0020EA50
		protected virtual Query VisitJoin(JoinQuery query)
		{
			Query query2 = this.VisitQuery(query.LeftQuery);
			Query query3 = this.VisitQuery(query.RightQuery);
			if (query2 == query.LeftQuery && query3 == query.RightQuery)
			{
				return query;
			}
			return this.NewJoin(query.TakeCount, query2, query.LeftKeyColumns, query3, query.RightKeyColumns, query.JoinKind, query.JoinKeys, query.JoinColumns, query.JoinAlgorithm, query.KeyEqualityComparers);
		}

		// Token: 0x06009FF3 RID: 40947 RVA: 0x002108C4 File Offset: 0x0020EAC4
		protected virtual Query VisitNestedJoin(NestedJoinQuery query)
		{
			Query query2 = this.VisitQuery(query.LeftQuery);
			if (query2 == query.LeftQuery)
			{
				return query;
			}
			return this.NewNestedJoin(query2, query.LeftKeyColumns, query.RightTable, query.RightKey, query.JoinKind, query.NewColumnName, query.JoinKeys, query.KeyEqualityComparers, query.ColumnType);
		}

		// Token: 0x06009FF4 RID: 40948 RVA: 0x00210920 File Offset: 0x0020EB20
		protected virtual Query VisitExpandListColumn(ExpandListColumnQuery query)
		{
			Query query2 = this.VisitQuery(query.InnerQuery);
			if (query2 == query.InnerQuery)
			{
				return query;
			}
			return this.NewExpandListColumn(query.ColumnIndex, query.SingleOrDefault, query.ColumnType, query2);
		}

		// Token: 0x06009FF5 RID: 40949 RVA: 0x00210960 File Offset: 0x0020EB60
		protected virtual Query VisitExpandRecordColumn(ExpandRecordColumnQuery query)
		{
			Query query2 = this.VisitQuery(query.InnerQuery);
			if (query2 == query.InnerQuery)
			{
				return query;
			}
			return this.NewExpandRecordColumn(query.ColumnToExpand, query.FieldsToProject, query.NewColumns, query.ColumnTypes, query2);
		}

		// Token: 0x06009FF6 RID: 40950 RVA: 0x002109A4 File Offset: 0x0020EBA4
		protected virtual Query VisitPivot(PivotQuery query)
		{
			Query query2 = this.VisitQuery(query.InnerQuery);
			if (query2 == query.InnerQuery)
			{
				return query;
			}
			return this.NewPivot(query2, query.InputType, query.OutputType, query.PivotValues, query.AttributeColumn, query.ValueColumn, query.AggregateFunction);
		}

		// Token: 0x06009FF7 RID: 40951 RVA: 0x002109F4 File Offset: 0x0020EBF4
		protected virtual Query VisitUnpivot(UnpivotQuery query)
		{
			Query query2 = this.VisitQuery(query.InnerQuery);
			if (query2 == query.InnerQuery)
			{
				return query;
			}
			return this.NewUnpivot(query2, query.InputType, query.OutputType, query.PivotValues, query.AttributeColumn, query.ValueColumn);
		}
	}
}
