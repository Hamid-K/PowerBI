using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001801 RID: 6145
	public abstract class Query
	{
		// Token: 0x170027F4 RID: 10228
		// (get) Token: 0x06009B38 RID: 39736
		public abstract QueryKind Kind { get; }

		// Token: 0x170027F5 RID: 10229
		// (get) Token: 0x06009B39 RID: 39737
		public abstract Keys Columns { get; }

		// Token: 0x06009B3A RID: 39738
		public abstract TypeValue GetColumnType(int column);

		// Token: 0x170027F6 RID: 10230
		// (get) Token: 0x06009B3B RID: 39739
		public abstract IList<TableKey> TableKeys { get; }

		// Token: 0x170027F7 RID: 10231
		// (get) Token: 0x06009B3C RID: 39740
		public abstract IList<ComputedColumn> ComputedColumns { get; }

		// Token: 0x170027F8 RID: 10232
		// (get) Token: 0x06009B3D RID: 39741
		public abstract TableSortOrder SortOrder { get; }

		// Token: 0x170027F9 RID: 10233
		// (get) Token: 0x06009B3E RID: 39742 RVA: 0x00201530 File Offset: 0x001FF730
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public virtual RowCount RowCount
		{
			get
			{
				RowCount rowCount = RowCount.Zero;
				foreach (IValueReference valueReference in this.GetRows())
				{
					if (rowCount.Value == RowCount.MaxValue)
					{
						throw ValueException.ListCountTooLarge(rowCount.Value);
					}
					rowCount = RowCount.op_Increment(rowCount);
				}
				return rowCount;
			}
		}

		// Token: 0x170027FA RID: 10234
		// (get) Token: 0x06009B3F RID: 39743
		public abstract IQueryDomain QueryDomain { get; }

		// Token: 0x06009B40 RID: 39744
		public abstract IEnumerable<IValueReference> GetRows();

		// Token: 0x06009B41 RID: 39745 RVA: 0x000E6755 File Offset: 0x000E4955
		public virtual bool TryGetReader(out IPageReader reader)
		{
			reader = null;
			return false;
		}

		// Token: 0x06009B42 RID: 39746 RVA: 0x002015A0 File Offset: 0x001FF7A0
		public virtual void TestConnection()
		{
			using (IEnumerator<IValueReference> enumerator = this.GetRows().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					enumerator.Current.Value.TestConnection();
				}
			}
		}

		// Token: 0x06009B43 RID: 39747 RVA: 0x002015F4 File Offset: 0x001FF7F4
		public virtual Query SelectRows(FunctionValue function)
		{
			return SelectRowsQuery.New(function, this);
		}

		// Token: 0x06009B44 RID: 39748 RVA: 0x00201600 File Offset: 0x001FF800
		public Query ProjectColumns(ColumnSelection columnSelection)
		{
			ColumnSelection columnSelection2;
			ColumnSelection columnSelection3;
			columnSelection.Split(this.Columns, out columnSelection2, out columnSelection3);
			return this.SelectColumns(columnSelection2).RenameReorderColumns(columnSelection3);
		}

		// Token: 0x06009B45 RID: 39749 RVA: 0x0020162A File Offset: 0x001FF82A
		public virtual Query SelectColumns(ColumnSelection columnSelection)
		{
			return SelectColumnsQuery.New(columnSelection, this);
		}

		// Token: 0x06009B46 RID: 39750 RVA: 0x00201633 File Offset: 0x001FF833
		public virtual Query RenameReorderColumns(ColumnSelection columnSelection)
		{
			return RenameReorderColumnsQuery.New(columnSelection, this);
		}

		// Token: 0x06009B47 RID: 39751 RVA: 0x0020163C File Offset: 0x001FF83C
		public virtual Query AddColumns(ColumnsConstructor columnGenerator)
		{
			if (columnGenerator.Length == 0)
			{
				return this;
			}
			return new AddColumnsQuery(columnGenerator, this);
		}

		// Token: 0x06009B48 RID: 39752 RVA: 0x00201650 File Offset: 0x001FF850
		public virtual Query NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return new NestedJoinQuery(this, leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers, null);
		}

		// Token: 0x06009B49 RID: 39753 RVA: 0x00201670 File Offset: 0x001FF870
		public virtual Query Skip(RowCount count)
		{
			return SkipTakeQuery.New(RowRange.All.Skip(count), this, false);
		}

		// Token: 0x06009B4A RID: 39754 RVA: 0x00201694 File Offset: 0x001FF894
		public virtual Query Take(RowCount count)
		{
			return SkipTakeQuery.New(RowRange.All.Take(count), this, false);
		}

		// Token: 0x06009B4B RID: 39755 RVA: 0x002016B6 File Offset: 0x001FF8B6
		public virtual Query Sort(TableSortOrder sortOrder)
		{
			return SortQuery.New(sortOrder, RowCount.Infinite, this);
		}

		// Token: 0x06009B4C RID: 39756 RVA: 0x002016C4 File Offset: 0x001FF8C4
		public Query Sort(Keys columns, Value sortOrder)
		{
			return this.Sort(TableValue.GetTableSortOrder(columns, sortOrder));
		}

		// Token: 0x06009B4D RID: 39757
		public abstract Query Unordered();

		// Token: 0x06009B4E RID: 39758 RVA: 0x002016D3 File Offset: 0x001FF8D3
		public virtual bool TryCombineAsItem(Query[] queries, int index, TypeValue[] types, TableSortOrder sortOrder, int disjointColumn, out Query query)
		{
			query = null;
			return false;
		}

		// Token: 0x06009B4F RID: 39759 RVA: 0x002016DA File Offset: 0x001FF8DA
		public static Query Combine(Query[] queries, TypeValue[] types, TableSortOrder sortOrder, int disjointColumn)
		{
			return Query.Combine(queries, types, sortOrder, disjointColumn, RowCount.Infinite);
		}

		// Token: 0x06009B50 RID: 39760 RVA: 0x002016EC File Offset: 0x001FF8EC
		public static Query Combine(Query[] queries, TypeValue[] types, TableSortOrder sortOrder, int disjointColumn, RowCount takeCount)
		{
			for (int i = 0; i < queries.Length; i++)
			{
				Query query;
				if (queries[i].TryCombineAsItem(queries, i, types, sortOrder, disjointColumn, out query))
				{
					return query;
				}
			}
			return new CombineQuery(queries, types, sortOrder, disjointColumn, takeCount);
		}

		// Token: 0x06009B51 RID: 39761 RVA: 0x000BF1DC File Offset: 0x000BD3DC
		public virtual bool TryJoinAsLeft(RowCount take, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			query = null;
			return false;
		}

		// Token: 0x06009B52 RID: 39762 RVA: 0x000BF1DC File Offset: 0x000BD3DC
		public virtual bool TryJoinAsRight(RowCount take, Query leftQuery, int[] leftKeyColumns, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			query = null;
			return false;
		}

		// Token: 0x06009B53 RID: 39763 RVA: 0x00201728 File Offset: 0x001FF928
		public static Query Join(RowCount take, Query leftQuery, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers)
		{
			Query query;
			if (leftQuery.TryJoinAsLeft(take, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query))
			{
				return query;
			}
			if (rightQuery.TryJoinAsRight(take, leftQuery, leftKeyColumns, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query))
			{
				return query;
			}
			return new JoinQuery(take, leftQuery, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers);
		}

		// Token: 0x06009B54 RID: 39764 RVA: 0x00201780 File Offset: 0x001FF980
		public virtual Query Group(Grouping grouping)
		{
			return new GroupQuery(grouping, this, false);
		}

		// Token: 0x06009B55 RID: 39765 RVA: 0x0020178A File Offset: 0x001FF98A
		public virtual Query Distinct(TableDistinct distinctCriteria)
		{
			return DistinctQuery.New(distinctCriteria, this, false);
		}

		// Token: 0x06009B56 RID: 39766 RVA: 0x00201794 File Offset: 0x001FF994
		public virtual Query Pivot(TableTypeValue inputType, TableTypeValue outputType, string[] pivotValues, string attributeColumn, string valueColumn, FunctionValue aggregateFunction)
		{
			return PivotQuery.New(this, inputType, outputType, pivotValues, attributeColumn, valueColumn, aggregateFunction);
		}

		// Token: 0x06009B57 RID: 39767 RVA: 0x002017A5 File Offset: 0x001FF9A5
		public virtual Query Unpivot(TableTypeValue inputType, TableTypeValue outputType, string[] pivotValues, string attributeColumn, string valueColumn)
		{
			return UnpivotQuery.New(this, inputType, outputType, pivotValues, attributeColumn, valueColumn);
		}

		// Token: 0x06009B58 RID: 39768 RVA: 0x000912D6 File Offset: 0x0008F4D6
		public virtual bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			query = null;
			return false;
		}

		// Token: 0x06009B59 RID: 39769 RVA: 0x002017B4 File Offset: 0x001FF9B4
		public Query ExpandListColumn(int columnIndex, bool singleOrDefault)
		{
			Query query;
			if (!this.TryExpandListColumn(columnIndex, singleOrDefault, out query))
			{
				query = new ExpandListColumnQuery(columnIndex, singleOrDefault, null, this);
			}
			return query;
		}

		// Token: 0x06009B5A RID: 39770 RVA: 0x000BF254 File Offset: 0x000BD454
		public virtual bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			query = null;
			return false;
		}

		// Token: 0x06009B5B RID: 39771 RVA: 0x002017D8 File Offset: 0x001FF9D8
		public Query ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
		{
			Query query;
			if (!this.TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out query))
			{
				query = new ExpandRecordColumnQuery(columnToExpand, fieldsToProject, newColumns, null, this);
			}
			return query;
		}

		// Token: 0x06009B5C RID: 39772 RVA: 0x000BF254 File Offset: 0x000BD454
		public virtual bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			result = null;
			return false;
		}

		// Token: 0x06009B5D RID: 39773 RVA: 0x002017FE File Offset: 0x001FF9FE
		public virtual TableValue DeltaSince(Value tag)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.Delta_SinceNotSupported, new QueryTableValue(this), null);
		}

		// Token: 0x06009B5E RID: 39774 RVA: 0x00201811 File Offset: 0x001FFA11
		public virtual Value NativeQuery(TextValue query, Value parameters, Value options)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.NativeQuery_NotSupported, new QueryTableValue(this), null);
		}

		// Token: 0x06009B5F RID: 39775 RVA: 0x001FE930 File Offset: 0x001FCB30
		public virtual ActionValue InsertRows(Query rowsToInsert)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.ValueNotUpdatable, new QueryTableValue(this), null);
		}

		// Token: 0x06009B60 RID: 39776 RVA: 0x001FE930 File Offset: 0x001FCB30
		public virtual ActionValue UpdateRows(ColumnUpdates columnUpdates)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.ValueNotUpdatable, new QueryTableValue(this), null);
		}

		// Token: 0x06009B61 RID: 39777 RVA: 0x001FE930 File Offset: 0x001FCB30
		public virtual ActionValue DeleteRows()
		{
			throw ValueException.NewExpressionError<Message0>(Strings.ValueNotUpdatable, new QueryTableValue(this), null);
		}

		// Token: 0x06009B62 RID: 39778 RVA: 0x00201824 File Offset: 0x001FFA24
		public virtual ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
		{
			throw ValueException.NewExpressionError<Message0>(Strings.Action_NativeStatementsNotSupported, new QueryTableValue(this), null);
		}

		// Token: 0x06009B63 RID: 39779 RVA: 0x00201837 File Offset: 0x001FFA37
		public virtual TableValue GetPartitionTable(int[] columns)
		{
			return ListValue.New(new Value[] { RecordValue.Empty }).ToTable();
		}

		// Token: 0x06009B64 RID: 39780 RVA: 0x00201854 File Offset: 0x001FFA54
		protected static Keys GetKeys(Keys keys, int[] columns)
		{
			string[] array = new string[columns.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = keys[columns[i]];
			}
			return Keys.New(array);
		}

		// Token: 0x06009B65 RID: 39781 RVA: 0x0020188C File Offset: 0x001FFA8C
		protected static Query TransformColumn(Query query, int columnIndex, FunctionValue transform, TypeValue newColumnType = null)
		{
			Keys keys = Keys.New(Guid.NewGuid().ToString());
			FunctionValue functionValue = new TableValue.FunctionsColumnsConstructorFunctionValue(new FunctionValue[]
			{
				new TableValue.TransformColumnFunctionValue(query.Columns[columnIndex], columnIndex, transform)
			});
			IValueReference[] array = new TypeValue[] { newColumnType ?? transform.Type.AsFunctionType.ReturnType };
			ColumnsConstructor columnsConstructor = new ColumnsConstructor(keys, functionValue, array);
			Query query2 = query.AddColumns(columnsConstructor);
			ColumnSelection columnSelection = new ColumnSelection(query2.Columns).Remove(columnIndex).Move(query2.Columns.Length - 2, columnIndex).Rename(columnIndex, query.Columns[columnIndex]);
			return query2.ProjectColumns(columnSelection);
		}

		// Token: 0x06009B66 RID: 39782 RVA: 0x00201940 File Offset: 0x001FFB40
		private static bool IsConcreteTableType(TypeValue type)
		{
			if (type.TypeKind != ValueKind.Table)
			{
				return false;
			}
			RecordTypeValue itemType = type.AsTableType.ItemType;
			if (itemType.Open)
			{
				return false;
			}
			new TypeValue[itemType.FieldKeys.Length];
			for (int i = 0; i < itemType.FieldKeys.Length; i++)
			{
				bool flag;
				itemType.GetFieldType(i, out flag);
				if (flag)
				{
					return false;
				}
			}
			return true;
		}
	}
}
