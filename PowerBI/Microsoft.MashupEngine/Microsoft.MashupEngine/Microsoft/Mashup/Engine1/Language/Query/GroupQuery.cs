using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200185A RID: 6234
	internal class GroupQuery : Query
	{
		// Token: 0x06009DF0 RID: 40432 RVA: 0x00209C69 File Offset: 0x00207E69
		public GroupQuery(Grouping grouping, Query innerQuery, bool floating = false)
		{
			this.grouping = grouping;
			this.floating = floating;
			this.innerQuery = innerQuery;
		}

		// Token: 0x170028C4 RID: 10436
		// (get) Token: 0x06009DF1 RID: 40433 RVA: 0x000024ED File Offset: 0x000006ED
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.Group;
			}
		}

		// Token: 0x170028C5 RID: 10437
		// (get) Token: 0x06009DF2 RID: 40434 RVA: 0x00209C86 File Offset: 0x00207E86
		public Query InnerQuery
		{
			get
			{
				return this.innerQuery;
			}
		}

		// Token: 0x170028C6 RID: 10438
		// (get) Token: 0x06009DF3 RID: 40435 RVA: 0x00209C8E File Offset: 0x00207E8E
		public Grouping Grouping
		{
			get
			{
				return this.grouping;
			}
		}

		// Token: 0x170028C7 RID: 10439
		// (get) Token: 0x06009DF4 RID: 40436 RVA: 0x00209C96 File Offset: 0x00207E96
		public bool Floating
		{
			get
			{
				return this.floating;
			}
		}

		// Token: 0x170028C8 RID: 10440
		// (get) Token: 0x06009DF5 RID: 40437 RVA: 0x00209C9E File Offset: 0x00207E9E
		public override Keys Columns
		{
			get
			{
				return this.grouping.ResultKeys;
			}
		}

		// Token: 0x06009DF6 RID: 40438 RVA: 0x00209CAC File Offset: 0x00207EAC
		public override TypeValue GetColumnType(int column)
		{
			if (column < this.grouping.KeyColumns.Length)
			{
				return this.innerQuery.GetColumnType(this.grouping.KeyColumns[column]);
			}
			return this.grouping.Constructors[column - this.grouping.KeyColumns.Length].Type.Value.AsType;
		}

		// Token: 0x170028C9 RID: 10441
		// (get) Token: 0x06009DF7 RID: 40439 RVA: 0x00209D0C File Offset: 0x00207F0C
		public override IList<TableKey> TableKeys
		{
			get
			{
				if (this.tableKeys == null)
				{
					if (this.grouping.Adjacent)
					{
						this.tableKeys = new TableKey[0];
					}
					else
					{
						int[] array = new int[this.grouping.KeyKeys.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = i;
						}
						this.tableKeys = new TableKey[]
						{
							new TableKey(array, true)
						};
					}
				}
				return this.tableKeys;
			}
		}

		// Token: 0x170028CA RID: 10442
		// (get) Token: 0x06009DF8 RID: 40440 RVA: 0x00209D80 File Offset: 0x00207F80
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				if (this.computedColumns == null)
				{
					this.computedColumns = Microsoft.Mashup.Engine1.Runtime.ComputedColumns.Group(this.innerQuery.ComputedColumns, QueryTableValue.NewRowType(this.innerQuery), this.grouping);
				}
				return this.computedColumns;
			}
		}

		// Token: 0x170028CB RID: 10443
		// (get) Token: 0x06009DF9 RID: 40441 RVA: 0x00209DB8 File Offset: 0x00207FB8
		public override TableSortOrder SortOrder
		{
			get
			{
				if (this.sortOrder == null && this.innerQuery.SortOrder != null && this.grouping.Adjacent && !SortQuery.TryAdjustSelectors(QueryTableValue.NewRowType(this.innerQuery), this.Columns, this.innerQuery.SortOrder, this.ApplyAfter, this.AdjustAfter, out this.sortOrder))
				{
					this.sortOrder = TableSortOrder.Unknown;
				}
				return this.sortOrder;
			}
		}

		// Token: 0x06009DFA RID: 40442 RVA: 0x00209E30 File Offset: 0x00208030
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			IList<QueryExpression> list = GroupQuery.CreateQueryExpressions(this.grouping.Constructors, QueryTableValue.NewRowType(this.innerQuery));
			if (list != null)
			{
				ColumnSelection.SelectMap selectMap = columnSelection.CreateSelectMap(this.Columns);
				ColumnSelection columnSelection2;
				int[] array;
				FunctionValue[] array2;
				if (AddColumnsQuery.TryGetInnerSelection(this.innerQuery, selectMap, (int c) => Array.IndexOf<int>(this.grouping.KeyColumns, c) != -1, this.grouping.KeyColumns.Length, list, out columnSelection2, out array, out array2))
				{
					int[] array3 = new int[this.innerQuery.Columns.Length];
					for (int i = 0; i < this.grouping.KeyColumns.Length; i++)
					{
						array3[this.grouping.KeyColumns[i]] = i + 1;
					}
					ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
					for (int j = 0; j < columnSelection.Keys.Length; j++)
					{
						int column = columnSelection.GetColumn(j);
						if (column < this.grouping.KeyColumns.Length)
						{
							columnSelectionBuilder.Add(columnSelection.Keys[j], column);
						}
					}
					KeysBuilder keysBuilder = default(KeysBuilder);
					keysBuilder.Union(this.grouping.KeyKeys);
					ArrayBuilder<ColumnConstructor> arrayBuilder = default(ArrayBuilder<ColumnConstructor>);
					int k = 0;
					int num = 0;
					while (k < this.grouping.Constructors.Length)
					{
						int num2 = this.grouping.KeyColumns.Length + k;
						if (selectMap.MapColumn(num2) != -1)
						{
							ColumnConstructor columnConstructor = this.grouping.Constructors[k];
							int num3;
							if (GroupQuery.TryGetFirstInvocation(list[k], out num3) && array3[num3] != 0 && selectMap.MapColumn(array3[num3] - 1) == -1 && array[num3] > 1)
							{
								columnSelectionBuilder.Add(columnConstructor.Name, array3[num3] - 1);
								array[num3] = 1;
							}
							else
							{
								keysBuilder.Add(columnConstructor.Name);
								columnSelectionBuilder.Add(columnConstructor.Name, this.grouping.KeyColumns.Length + arrayBuilder.Count);
								arrayBuilder.Add(new ColumnConstructor(columnConstructor.Name, array2[num], columnConstructor.Type));
							}
							num++;
						}
						k++;
					}
					ColumnSelection.SelectMap selectMap2 = columnSelection2.CreateSelectMap(this.innerQuery.Columns);
					Query query = this.innerQuery.SelectColumns(columnSelection2);
					Grouping grouping = new Grouping(this.grouping.Adjacent, keysBuilder.ToKeys(), this.grouping.KeyKeys, selectMap2.MapColumns(this.grouping.KeyColumns), arrayBuilder.ToArray(), this.grouping.CompareRecords, this.grouping.Comparer, QueryTableValue.NewTableType(query));
					if (this.floating)
					{
						query = new GroupQuery(grouping, query, this.floating);
					}
					else
					{
						query = query.Group(grouping);
					}
					return ProjectColumnsQuery.New(columnSelectionBuilder.ToColumnSelection(), query);
				}
			}
			return base.SelectColumns(columnSelection);
		}

		// Token: 0x06009DFB RID: 40443 RVA: 0x0020A107 File Offset: 0x00208307
		public override Query Unordered()
		{
			return this.innerQuery.Unordered().Group(this.grouping);
		}

		// Token: 0x170028CC RID: 10444
		// (get) Token: 0x06009DFC RID: 40444 RVA: 0x0020A11F File Offset: 0x0020831F
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.innerQuery.QueryDomain;
			}
		}

		// Token: 0x06009DFD RID: 40445 RVA: 0x0020A12C File Offset: 0x0020832C
		public override IEnumerable<IValueReference> GetRows()
		{
			bool flag = this.grouping.Adjacent;
			Query query = this.innerQuery;
			IEqualityComparer<Value> equalityComparer;
			if (this.grouping.Comparer == null || !this.grouping.Comparer.TryGetEqualityComparer(out equalityComparer))
			{
				equalityComparer = null;
			}
			if (!flag && this.grouping.Comparer != null && equalityComparer == null)
			{
				flag = true;
				query = query.Sort(GroupQuery.GetSortOrder(this.grouping));
			}
			Grouping grouping = this.grouping;
			IAccumulable accumulable = AccumulableHelper.CreateAccumulable(QueryTableValue.NewRowType(this.InnerQuery), ref grouping);
			if (flag)
			{
				return GroupQuery.GroupAdjacent(query.GetRows(), grouping, accumulable);
			}
			return new GroupQuery.GroupEnumerable(query.GetRows(), grouping, accumulable, equalityComparer);
		}

		// Token: 0x170028CD RID: 10445
		// (get) Token: 0x06009DFE RID: 40446 RVA: 0x0020A1D2 File Offset: 0x002083D2
		private Func<QueryExpression, bool> ApplyAfter
		{
			get
			{
				return (QueryExpression node) => node.AllAccess(ArgumentAccess.Deny, (int column) => Array.IndexOf<int>(this.grouping.KeyColumns, column) != -1);
			}
		}

		// Token: 0x170028CE RID: 10446
		// (get) Token: 0x06009DFF RID: 40447 RVA: 0x0020A1E0 File Offset: 0x002083E0
		private Func<QueryExpression, QueryExpression> AdjustAfter
		{
			get
			{
				return (QueryExpression node) => node.AdjustColumnAccess((int column) => Array.IndexOf<int>(this.grouping.KeyColumns, column));
			}
		}

		// Token: 0x06009E00 RID: 40448 RVA: 0x0020A1F0 File Offset: 0x002083F0
		public static bool TryGetFirstInvocation(QueryExpression expression, out int column)
		{
			InvocationQueryExpression invocationQueryExpression = expression as InvocationQueryExpression;
			if (invocationQueryExpression != null && invocationQueryExpression.Arguments.Count == 1)
			{
				ConstantQueryExpression constantQueryExpression = invocationQueryExpression.Function as ConstantQueryExpression;
				ColumnAccessQueryExpression columnAccessQueryExpression = invocationQueryExpression.Arguments[0] as ColumnAccessQueryExpression;
				if (constantQueryExpression != null && constantQueryExpression.Value.AsFunction.FunctionIdentity.Equals(Library.List.First.FunctionIdentity) && columnAccessQueryExpression != null)
				{
					column = columnAccessQueryExpression.Column;
					return true;
				}
			}
			column = -1;
			return false;
		}

		// Token: 0x06009E01 RID: 40449 RVA: 0x0020A268 File Offset: 0x00208468
		private static IList<QueryExpression> CreateQueryExpressions(ColumnConstructor[] columnCtors, RecordTypeValue rowType)
		{
			QueryExpression[] array = new QueryExpression[columnCtors.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = QueryExpressionBuilder.ToQueryExpression(rowType, columnCtors[i].Function);
			}
			return array;
		}

		// Token: 0x06009E02 RID: 40450 RVA: 0x0020A29E File Offset: 0x0020849E
		private static IEnumerable<IValueReference> GroupAdjacent(IEnumerable<IValueReference> table, Grouping grouping, IAccumulable accumulable)
		{
			int[] keyColumns = grouping.KeyColumns;
			ColumnConstructor[] constructors = grouping.Constructors;
			IEqualityComparer<Value> equalityComparer = GroupQuery.GetComparer(grouping.Comparer);
			Value value = null;
			IAccumulator accumulator = null;
			foreach (IValueReference valueReference in table)
			{
				RecordValue row = valueReference.Value.AsRecord;
				Value key = GroupQuery.CreateGroupKey(grouping, row);
				if (value == null || !equalityComparer.Equals(value, key))
				{
					if (value != null)
					{
						yield return AccumulableHelper.CreateGroupResult(grouping, value, accumulator.Current);
					}
					value = key;
					accumulator = accumulable.CreateAccumulator();
				}
				accumulator.AccumulateNext(row);
				row = null;
				key = null;
			}
			IEnumerator<IValueReference> enumerator = null;
			if (value != null)
			{
				yield return AccumulableHelper.CreateGroupResult(grouping, value, accumulator.Current);
			}
			yield break;
			yield break;
		}

		// Token: 0x06009E03 RID: 40451 RVA: 0x0020A2BC File Offset: 0x002084BC
		private static Value CreateGroupKey(Grouping grouping, RecordValue row)
		{
			int[] keyColumns = grouping.KeyColumns;
			if (grouping.CompareRecords)
			{
				IValueReference[] array = new IValueReference[keyColumns.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = row.GetReference(keyColumns[i]);
				}
				return RecordValue.New(grouping.KeyKeys, array);
			}
			return row[keyColumns[0]];
		}

		// Token: 0x06009E04 RID: 40452 RVA: 0x0020A314 File Offset: 0x00208514
		private static TableSortOrder GetSortOrder(Grouping grouping)
		{
			FunctionValue functionValue = grouping.Comparer;
			if (!grouping.CompareRecords)
			{
				functionValue = new GroupQuery.ComparerFunctionValue(functionValue, grouping.KeyColumns[0]);
			}
			return new TableSortOrder(new SortOrder[]
			{
				new SortOrder(null, functionValue, true)
			});
		}

		// Token: 0x06009E05 RID: 40453 RVA: 0x0020A35C File Offset: 0x0020855C
		private static IEqualityComparer<Value> GetComparer(FunctionValue comparer)
		{
			if (comparer == null)
			{
				return _ValueComparer.StrictDefault;
			}
			IEqualityComparer<Value> equalityComparer;
			if (!comparer.TryGetEqualityComparer(out equalityComparer))
			{
				return new GroupQuery.FunctionEqualityComparer(comparer);
			}
			return equalityComparer;
		}

		// Token: 0x04005309 RID: 21257
		private Query innerQuery;

		// Token: 0x0400530A RID: 21258
		private Grouping grouping;

		// Token: 0x0400530B RID: 21259
		private TableKey[] tableKeys;

		// Token: 0x0400530C RID: 21260
		private IList<ComputedColumn> computedColumns;

		// Token: 0x0400530D RID: 21261
		private TableSortOrder sortOrder;

		// Token: 0x0400530E RID: 21262
		private bool floating;

		// Token: 0x0200185B RID: 6235
		private class GroupEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06009E0B RID: 40459 RVA: 0x0020A3DD File Offset: 0x002085DD
			public GroupEnumerable(IEnumerable<IValueReference> rows, Grouping grouping, IAccumulable accumulable, IEqualityComparer<Value> equalityComparer)
			{
				this.rows = rows;
				this.grouping = grouping;
				this.accumulable = accumulable;
				this.equalityComparer = equalityComparer;
			}

			// Token: 0x06009E0C RID: 40460 RVA: 0x0020A402 File Offset: 0x00208602
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06009E0D RID: 40461 RVA: 0x0020A40C File Offset: 0x0020860C
			public IEnumerator<IValueReference> GetEnumerator()
			{
				int[] keyColumns = this.grouping.KeyColumns;
				ColumnConstructor[] constructors = this.grouping.Constructors;
				Dictionary<Value, IAccumulator> dictionary = new Dictionary<Value, IAccumulator>(this.equalityComparer);
				foreach (IValueReference valueReference in this.rows)
				{
					RecordValue asRecord = valueReference.Value.AsRecord;
					Value value = GroupQuery.CreateGroupKey(this.grouping, asRecord);
					IAccumulator accumulator;
					if (!dictionary.TryGetValue(value, out accumulator))
					{
						accumulator = this.accumulable.CreateAccumulator();
						dictionary.Add(value, accumulator);
					}
					accumulator.AccumulateNext(asRecord);
				}
				RecordValue[] array = new RecordValue[dictionary.Count];
				int num = 0;
				foreach (KeyValuePair<Value, IAccumulator> keyValuePair in dictionary)
				{
					array[num++] = AccumulableHelper.CreateGroupResult(this.grouping, keyValuePair.Key, keyValuePair.Value.Current);
				}
				Value[] array2 = array;
				return ListValue.New(array2).GetEnumerator();
			}

			// Token: 0x0400530F RID: 21263
			private readonly IEnumerable<IValueReference> rows;

			// Token: 0x04005310 RID: 21264
			private readonly Grouping grouping;

			// Token: 0x04005311 RID: 21265
			private readonly IAccumulable accumulable;

			// Token: 0x04005312 RID: 21266
			private readonly IEqualityComparer<Value> equalityComparer;
		}

		// Token: 0x0200185C RID: 6236
		private class ComparerFunctionValue : NativeFunctionValue2
		{
			// Token: 0x06009E0E RID: 40462 RVA: 0x0020A538 File Offset: 0x00208738
			public ComparerFunctionValue(FunctionValue comparer, int column)
				: base("x", "y")
			{
				this.comparer = comparer;
				this.column = column;
			}

			// Token: 0x06009E0F RID: 40463 RVA: 0x0020A558 File Offset: 0x00208758
			public override Value Invoke(Value x, Value y)
			{
				return this.comparer.Invoke(x[this.column], y[this.column]);
			}

			// Token: 0x04005313 RID: 21267
			private int column;

			// Token: 0x04005314 RID: 21268
			private FunctionValue comparer;
		}

		// Token: 0x0200185D RID: 6237
		private sealed class FunctionEqualityComparer : IEqualityComparer<Value>
		{
			// Token: 0x06009E10 RID: 40464 RVA: 0x0020A57D File Offset: 0x0020877D
			public FunctionEqualityComparer(FunctionValue comparer)
			{
				this.comparer = comparer;
			}

			// Token: 0x06009E11 RID: 40465 RVA: 0x0020A58C File Offset: 0x0020878C
			public bool Equals(Value x, Value y)
			{
				return this.comparer.Invoke(x, y).AsInteger32 == 0;
			}

			// Token: 0x06009E12 RID: 40466 RVA: 0x0000EE09 File Offset: 0x0000D009
			public int GetHashCode(Value x)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x04005315 RID: 21269
			private readonly FunctionValue comparer;
		}
	}
}
