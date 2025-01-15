using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200183D RID: 6205
	internal class SortQuery : Query
	{
		// Token: 0x06009D29 RID: 40233 RVA: 0x00207858 File Offset: 0x00205A58
		public static Query New(TableSortOrder sortOrder, RowCount take, Query query)
		{
			ColumnSelection columnSelection = null;
			QueryExpression[] array;
			bool[] array2;
			if (!sortOrder.IsEmpty && SortQuery.TryGetSelectors(sortOrder, QueryTableValue.NewRowType(query), out array, out array2))
			{
				KeysBuilder keysBuilder = default(KeysBuilder);
				ArrayBuilder<FunctionValue> arrayBuilder = default(ArrayBuilder<FunctionValue>);
				ArrayBuilder<IValueReference> arrayBuilder2 = default(ArrayBuilder<IValueReference>);
				SortOrder[] array3 = new SortOrder[array.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					SortOrder sortOrder2 = sortOrder.SortOrders[i];
					if (sortOrder2.Comparer == null && array[i].Kind != QueryExpressionKind.Constant && array[i].Kind != QueryExpressionKind.ColumnAccess)
					{
						string uniqueName = TableValue.GetUniqueName(query.Columns, i);
						keysBuilder.Add(uniqueName);
						arrayBuilder.Add(sortOrder2.Selector);
						arrayBuilder2.Add(sortOrder2.Selector.Type.AsFunctionType.ReturnType);
						sortOrder2 = new SortOrder(new TableValue.ColumnSelectorFunctionValue(uniqueName, query.Columns.Length + keysBuilder.Count - 1), sortOrder2.Comparer, sortOrder2.Ascending);
					}
					array3[i] = sortOrder2;
				}
				if (keysBuilder.Count > 0)
				{
					ColumnsConstructor columnsConstructor = new ColumnsConstructor(keysBuilder.ToKeys(), new TableValue.FunctionsColumnsConstructorFunctionValue(arrayBuilder.ToArray()), arrayBuilder2.ToArray());
					columnSelection = new ColumnSelection(query.Columns);
					query = query.AddColumns(columnsConstructor);
					sortOrder = new TableSortOrder(array3);
				}
			}
			if (sortOrder.IsEmpty)
			{
				query = query.Unordered();
			}
			if (!sortOrder.IsEmpty && !SortQuery.IsSortOrderPrefix(QueryTableValue.NewRowType(query), query.SortOrder, sortOrder))
			{
				if (columnSelection != null)
				{
					query = query.Sort(sortOrder).Take(take);
				}
				else if (!take.IsInfinite && take.Value > 2147483647L)
				{
					query = new SortQuery(sortOrder, RowCount.Infinite, query).Take(take);
				}
				else
				{
					query = new SortQuery(sortOrder, take, query);
				}
			}
			else
			{
				query = query.Take(take);
			}
			if (columnSelection != null)
			{
				query = query.SelectColumns(columnSelection);
			}
			return query;
		}

		// Token: 0x06009D2A RID: 40234 RVA: 0x00207A51 File Offset: 0x00205C51
		protected SortQuery(TableSortOrder sortOrder, RowCount take, Query innerQuery)
		{
			this.sortOrder = sortOrder;
			this.take = take;
			this.innerQuery = innerQuery;
		}

		// Token: 0x17002883 RID: 10371
		// (get) Token: 0x06009D2B RID: 40235 RVA: 0x00075E2C File Offset: 0x0007402C
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.Sort;
			}
		}

		// Token: 0x17002884 RID: 10372
		// (get) Token: 0x06009D2C RID: 40236 RVA: 0x00207A6E File Offset: 0x00205C6E
		public Query InnerQuery
		{
			get
			{
				return this.innerQuery;
			}
		}

		// Token: 0x17002885 RID: 10373
		// (get) Token: 0x06009D2D RID: 40237 RVA: 0x00207A76 File Offset: 0x00205C76
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x17002886 RID: 10374
		// (get) Token: 0x06009D2E RID: 40238 RVA: 0x00207A7E File Offset: 0x00205C7E
		public RowCount TakeCount
		{
			get
			{
				return this.take;
			}
		}

		// Token: 0x17002887 RID: 10375
		// (get) Token: 0x06009D2F RID: 40239 RVA: 0x00207A86 File Offset: 0x00205C86
		public override Keys Columns
		{
			get
			{
				return this.innerQuery.Columns;
			}
		}

		// Token: 0x06009D30 RID: 40240 RVA: 0x00207A93 File Offset: 0x00205C93
		public override TypeValue GetColumnType(int column)
		{
			return this.innerQuery.GetColumnType(column);
		}

		// Token: 0x17002888 RID: 10376
		// (get) Token: 0x06009D31 RID: 40241 RVA: 0x00207AA1 File Offset: 0x00205CA1
		public override IList<TableKey> TableKeys
		{
			get
			{
				return this.innerQuery.TableKeys;
			}
		}

		// Token: 0x17002889 RID: 10377
		// (get) Token: 0x06009D32 RID: 40242 RVA: 0x00207AAE File Offset: 0x00205CAE
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return this.innerQuery.ComputedColumns;
			}
		}

		// Token: 0x1700288A RID: 10378
		// (get) Token: 0x06009D33 RID: 40243 RVA: 0x00207ABB File Offset: 0x00205CBB
		public override RowCount RowCount
		{
			get
			{
				return this.innerQuery.RowCount;
			}
		}

		// Token: 0x06009D34 RID: 40244 RVA: 0x00207AC8 File Offset: 0x00205CC8
		public override Query Distinct(TableDistinct distinctCriteria)
		{
			if (!this.take.IsInfinite)
			{
				return base.Distinct(distinctCriteria);
			}
			return this.innerQuery.Distinct(distinctCriteria).Sort(this.sortOrder);
		}

		// Token: 0x06009D35 RID: 40245 RVA: 0x00207AF8 File Offset: 0x00205CF8
		public override Query Take(RowCount count)
		{
			return SortQuery.New(this.sortOrder, RowRange.All.Take(this.take).Take(count).TakeCount, this.innerQuery);
		}

		// Token: 0x06009D36 RID: 40246 RVA: 0x00207B3C File Offset: 0x00205D3C
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			int[] selected = new int[this.Columns.Length];
			for (int i = 0; i < columnSelection.Keys.Length; i++)
			{
				selected[columnSelection.GetColumn(i)] = i + 1;
			}
			Func<int, bool> <>9__2;
			Func<int, int> <>9__3;
			TableSortOrder tableSortOrder;
			if (SortQuery.TryAdjustSelectors(QueryTableValue.NewRowType(this), columnSelection.Keys, this.sortOrder, delegate(QueryExpression selector)
			{
				Func<InvocationQueryExpression, bool> safe = ArgumentAccess.Safe;
				Func<int, bool> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (int column) => selected[column] != 0);
				}
				return selector.AllAccess(safe, func);
			}, delegate(QueryExpression selector)
			{
				Func<int, int> func2;
				if ((func2 = <>9__3) == null)
				{
					func2 = (<>9__3 = (int column) => selected[column] - 1);
				}
				return selector.AdjustColumnAccess(func2);
			}, out tableSortOrder))
			{
				return this.innerQuery.SelectColumns(columnSelection).Sort(tableSortOrder).Take(this.take);
			}
			return base.SelectColumns(columnSelection);
		}

		// Token: 0x06009D37 RID: 40247 RVA: 0x00207BE4 File Offset: 0x00205DE4
		public override Query SelectRows(FunctionValue function)
		{
			if (!this.take.IsInfinite)
			{
				return base.SelectRows(function);
			}
			return this.innerQuery.SelectRows(function).Sort(this.sortOrder);
		}

		// Token: 0x06009D38 RID: 40248 RVA: 0x00207C14 File Offset: 0x00205E14
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			if (!this.take.IsInfinite)
			{
				return base.TryExpandListColumn(columnIndex, singleOrDefault, out query);
			}
			QueryExpression[] array;
			bool[] array2;
			if (SortQuery.TryGetSelectors(this.sortOrder, QueryTableValue.NewRowType(this), out array, out array2))
			{
				bool flag = true;
				int num = 0;
				Func<int, bool> <>9__0;
				while (flag && num < array.Length)
				{
					QueryExpression queryExpression = array[num];
					Func<InvocationQueryExpression, bool> deny = ArgumentAccess.Deny;
					Func<int, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (int column) => column != columnIndex);
					}
					flag = queryExpression.AllAccess(deny, func);
					num++;
				}
				if (flag && this.innerQuery.TryExpandListColumn(columnIndex, singleOrDefault, out query))
				{
					query = query.Sort(this.sortOrder);
					return true;
				}
			}
			query = null;
			return false;
		}

		// Token: 0x06009D39 RID: 40249 RVA: 0x00207CD8 File Offset: 0x00205ED8
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			if (!this.take.IsInfinite)
			{
				return base.TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out query);
			}
			Query query2;
			TableSortOrder tableSortOrder;
			if (this.innerQuery.TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out query2) && SortQuery.TryAdjustSelectors(QueryTableValue.NewRowType(this.innerQuery), query2.Columns, this.sortOrder, ExpandRecordColumnQuery.CreateApplyAfter(columnToExpand), ExpandRecordColumnQuery.CreateAdjustAfter(columnToExpand, fieldsToProject.Length), out tableSortOrder))
			{
				query = query2.Sort(tableSortOrder);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009D3A RID: 40250 RVA: 0x00207D53 File Offset: 0x00205F53
		public override Query Group(Grouping grouping)
		{
			if (!grouping.Adjacent && this.take.IsInfinite)
			{
				return this.innerQuery.Group(grouping).Take(this.take);
			}
			return base.Group(grouping);
		}

		// Token: 0x06009D3B RID: 40251 RVA: 0x00207D89 File Offset: 0x00205F89
		public override TableValue GetPartitionTable(int[] columns)
		{
			return this.innerQuery.GetPartitionTable(columns);
		}

		// Token: 0x06009D3C RID: 40252 RVA: 0x00207D97 File Offset: 0x00205F97
		public override Query Unordered()
		{
			if (!this.take.IsInfinite)
			{
				return this;
			}
			return this.innerQuery.Unordered();
		}

		// Token: 0x1700288B RID: 10379
		// (get) Token: 0x06009D3D RID: 40253 RVA: 0x00207DB3 File Offset: 0x00205FB3
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.innerQuery.QueryDomain;
			}
		}

		// Token: 0x06009D3E RID: 40254 RVA: 0x00207DC0 File Offset: 0x00205FC0
		public override IEnumerable<IValueReference> GetRows()
		{
			return new SortQuery.SortEnumerable(this.innerQuery.GetRows(), this.take, this.sortOrder.ToComparer());
		}

		// Token: 0x06009D3F RID: 40255 RVA: 0x00207DE4 File Offset: 0x00205FE4
		public static bool TryGetSelectors(TableSortOrder sortOrder, RecordTypeValue rowType, out QueryExpression[] expressions, out bool[] ascendings)
		{
			expressions = new QueryExpression[sortOrder.SortOrders.Length];
			ascendings = new bool[sortOrder.SortOrders.Length];
			int num = 0;
			while (expressions != null && num < expressions.Length)
			{
				SortOrder sortOrder2 = sortOrder.SortOrders[num];
				if (sortOrder2.Selector != null && sortOrder2.Comparer == null && QueryExpressionBuilder.TryToQueryExpression(rowType, sortOrder2.Selector, out expressions[num]))
				{
					ascendings[num] = sortOrder2.Ascending;
				}
				else
				{
					expressions = null;
					ascendings = null;
				}
				num++;
			}
			return expressions != null;
		}

		// Token: 0x06009D40 RID: 40256 RVA: 0x00207E70 File Offset: 0x00206070
		public static bool TryAdjustSelectors(RecordTypeValue rowType, Keys newColumns, TableSortOrder sortOrder, Func<QueryExpression, bool> canApply)
		{
			TableSortOrder tableSortOrder;
			return SortQuery.TryAdjustSelectors(rowType, newColumns, sortOrder, canApply, null, out tableSortOrder);
		}

		// Token: 0x06009D41 RID: 40257 RVA: 0x00207E8C File Offset: 0x0020608C
		public static bool TryAdjustSelectors(RecordTypeValue rowType, Keys newColumns, TableSortOrder sortOrder, Func<QueryExpression, bool> canApply, Func<QueryExpression, QueryExpression> adjust, out TableSortOrder newSortOrder)
		{
			bool flag;
			if (SortQuery.TryAdjustSelectors(rowType, newColumns, sortOrder, canApply, adjust, out newSortOrder, out flag) && flag)
			{
				return true;
			}
			newSortOrder = null;
			return false;
		}

		// Token: 0x06009D42 RID: 40258 RVA: 0x00207EB4 File Offset: 0x002060B4
		public static bool TryAdjustSelectors(RecordTypeValue rowType, Keys newColumns, TableSortOrder sortOrder, Func<QueryExpression, bool> canApply, Func<QueryExpression, QueryExpression> adjust, out TableSortOrder newSortOrder, out bool newSortOrderIsComplete)
		{
			QueryExpression[] array;
			bool[] array2;
			if (SortQuery.TryGetSelectors(sortOrder, rowType, out array, out array2))
			{
				ArrayBuilder<SortOrder> arrayBuilder = default(ArrayBuilder<SortOrder>);
				newSortOrderIsComplete = true;
				for (int i = 0; i < array.Length; i++)
				{
					if (!canApply(array[i]))
					{
						newSortOrderIsComplete = false;
						break;
					}
					if (adjust != null)
					{
						FunctionValue functionValue = QueryExpressionAssembler.Assemble(newColumns, adjust(array[i]));
						arrayBuilder.Add(new SortOrder(functionValue, null, array2[i]));
					}
				}
				newSortOrder = ((adjust != null) ? new TableSortOrder(arrayBuilder.ToArray()) : null);
				return true;
			}
			newSortOrder = null;
			newSortOrderIsComplete = false;
			return false;
		}

		// Token: 0x06009D43 RID: 40259 RVA: 0x00207F44 File Offset: 0x00206144
		protected static bool IsSortOrderPrefix(RecordTypeValue rowType, TableSortOrder sortOrder, TableSortOrder sortOrderPrefix)
		{
			QueryExpression[] array;
			bool[] array2;
			QueryExpression[] array3;
			bool[] array4;
			if (TableSortOrder.IsKnown(sortOrder) && sortOrder != TableSortOrder.None && TableSortOrder.IsKnown(sortOrderPrefix) && sortOrderPrefix != TableSortOrder.None && SortQuery.TryGetSelectors(sortOrder, rowType, out array, out array2) && SortQuery.TryGetSelectors(sortOrderPrefix, rowType, out array3, out array4) && array3.Length <= array.Length)
			{
				bool flag = true;
				int num = 0;
				while (flag && num < array3.Length)
				{
					flag = SortQuery.IsSameColumnReference(array[num], array3[num]) && array2[num] == array4[num];
					num++;
				}
				return flag;
			}
			return false;
		}

		// Token: 0x06009D44 RID: 40260 RVA: 0x00207FD0 File Offset: 0x002061D0
		private static bool IsSameColumnReference(QueryExpression x, QueryExpression y)
		{
			int num;
			int num2;
			return x.TryGetColumnAccess(out num) && y.TryGetColumnAccess(out num2) && num == num2;
		}

		// Token: 0x040052A2 RID: 21154
		private Query innerQuery;

		// Token: 0x040052A3 RID: 21155
		private TableSortOrder sortOrder;

		// Token: 0x040052A4 RID: 21156
		private RowCount take;

		// Token: 0x0200183E RID: 6206
		private class SortEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06009D45 RID: 40261 RVA: 0x00207FF7 File Offset: 0x002061F7
			public SortEnumerable(IEnumerable<IValueReference> rows, RowCount take, IComparer<Value> comparer)
			{
				this.rows = rows;
				this.take = take;
				this.comparer = comparer;
			}

			// Token: 0x06009D46 RID: 40262 RVA: 0x00208014 File Offset: 0x00206214
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06009D47 RID: 40263 RVA: 0x0020801C File Offset: 0x0020621C
			public IEnumerator<IValueReference> GetEnumerator()
			{
				long num = ((!this.take.IsInfinite) ? this.take.Value : long.MaxValue);
				return SortQuery.SortEnumerable.Sort(this.rows, this.comparer, num).Cast<IValueReference>().GetEnumerator();
			}

			// Token: 0x06009D48 RID: 40264 RVA: 0x00208070 File Offset: 0x00206270
			public static IEnumerable<Value> Sort(IEnumerable<IValueReference> values, IComparer<Value> comparer, long takeCount)
			{
				if (takeCount > 2147483647L)
				{
					List<Value> list = new List<Value>();
					foreach (IValueReference valueReference in values)
					{
						list.Add(valueReference.Value);
					}
					try
					{
						list.Sort(comparer);
					}
					catch (ArgumentException)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.CustomComparersResultedInInvalidSort, null, null);
					}
					catch (InvalidOperationException ex)
					{
						if (ex.InnerException != null)
						{
							throw ex.InnerException;
						}
						throw;
					}
					return list;
				}
				return values.Select((IValueReference i) => i.Value).MinK((int)takeCount, comparer);
			}

			// Token: 0x040052A5 RID: 21157
			private readonly IEnumerable<IValueReference> rows;

			// Token: 0x040052A6 RID: 21158
			private readonly RowCount take;

			// Token: 0x040052A7 RID: 21159
			private readonly IComparer<Value> comparer;
		}
	}
}
