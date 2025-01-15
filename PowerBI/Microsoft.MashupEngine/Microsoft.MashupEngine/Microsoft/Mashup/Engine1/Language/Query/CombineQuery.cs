using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200184B RID: 6219
	internal class CombineQuery : Query
	{
		// Token: 0x06009DA2 RID: 40354 RVA: 0x00208D24 File Offset: 0x00206F24
		public CombineQuery(Query[] queries, TypeValue[] types, TableSortOrder sortOrder, int disjointColumn)
			: this(queries, types, sortOrder, disjointColumn, RowCount.Infinite)
		{
		}

		// Token: 0x06009DA3 RID: 40355 RVA: 0x00208D36 File Offset: 0x00206F36
		public CombineQuery(Query[] queries, TypeValue[] types, TableSortOrder sortOrder, int disjointColumn, RowCount takeCount)
		{
			this.queries = queries;
			this.columnTypes = types;
			this.sortOrder = sortOrder;
			this.disjointColumn = disjointColumn;
			this.takeCount = takeCount;
		}

		// Token: 0x170028B4 RID: 10420
		// (get) Token: 0x06009DA4 RID: 40356 RVA: 0x00002475 File Offset: 0x00000675
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.Combine;
			}
		}

		// Token: 0x170028B5 RID: 10421
		// (get) Token: 0x06009DA5 RID: 40357 RVA: 0x00208D63 File Offset: 0x00206F63
		public Query[] Queries
		{
			get
			{
				return this.queries;
			}
		}

		// Token: 0x170028B6 RID: 10422
		// (get) Token: 0x06009DA6 RID: 40358 RVA: 0x00208D6B File Offset: 0x00206F6B
		public override Keys Columns
		{
			get
			{
				return this.queries[0].Columns;
			}
		}

		// Token: 0x170028B7 RID: 10423
		// (get) Token: 0x06009DA7 RID: 40359 RVA: 0x00208D7C File Offset: 0x00206F7C
		public TypeValue[] ColumnTypes
		{
			get
			{
				if (this.columnTypes == null)
				{
					this.columnTypes = new TypeValue[this.Columns.Length];
					for (int i = 0; i < this.columnTypes.Length; i++)
					{
						List<IValueReference> list = new List<IValueReference>();
						TypeValue typeValue = TypeValue.None;
						for (int j = 0; j < this.queries.Length; j++)
						{
							typeValue = TypeAlgebra.Union(this.queries[j].GetColumnType(i), typeValue);
							Value domain = TypeServices.GetDomain(this.queries[j].GetColumnType(i));
							if (!domain.IsNull)
							{
								list.AddRange(domain.AsList);
							}
						}
						this.columnTypes[i] = TypeServices.SetDomain(typeValue, ListValue.New(list.ToArray()));
					}
				}
				return this.columnTypes;
			}
		}

		// Token: 0x170028B8 RID: 10424
		// (get) Token: 0x06009DA8 RID: 40360 RVA: 0x00208E3E File Offset: 0x0020703E
		public int DisjointColumn
		{
			get
			{
				return this.disjointColumn;
			}
		}

		// Token: 0x06009DA9 RID: 40361 RVA: 0x00208E46 File Offset: 0x00207046
		public override TypeValue GetColumnType(int column)
		{
			return this.ColumnTypes[column];
		}

		// Token: 0x170028B9 RID: 10425
		// (get) Token: 0x06009DAA RID: 40362 RVA: 0x0010B9AA File Offset: 0x00109BAA
		public override IList<TableKey> TableKeys
		{
			get
			{
				return Microsoft.Mashup.Engine1.Runtime.TableKeys.None;
			}
		}

		// Token: 0x170028BA RID: 10426
		// (get) Token: 0x06009DAB RID: 40363 RVA: 0x001DEDD7 File Offset: 0x001DCFD7
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return Microsoft.Mashup.Engine1.Runtime.ComputedColumns.None;
			}
		}

		// Token: 0x170028BB RID: 10427
		// (get) Token: 0x06009DAC RID: 40364 RVA: 0x00208E50 File Offset: 0x00207050
		public override RowCount RowCount
		{
			get
			{
				long num = 0L;
				for (int i = 0; i < this.queries.Length; i++)
				{
					long value = this.queries[i].RowCount.Value;
					if (num > ListValue.MaxCount - value)
					{
						throw ValueException.ListCountTooLarge(value);
					}
					num += value;
				}
				return new RowCount(num);
			}
		}

		// Token: 0x170028BC RID: 10428
		// (get) Token: 0x06009DAD RID: 40365 RVA: 0x00208EA4 File Offset: 0x002070A4
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.sortOrder;
			}
		}

		// Token: 0x170028BD RID: 10429
		// (get) Token: 0x06009DAE RID: 40366 RVA: 0x00208EAC File Offset: 0x002070AC
		public RowCount TakeCount
		{
			get
			{
				return this.takeCount;
			}
		}

		// Token: 0x06009DAF RID: 40367 RVA: 0x00208EB4 File Offset: 0x002070B4
		public override Query AddColumns(ColumnsConstructor columnGenerator)
		{
			return Query.Combine(this.ApplyToAll((Query q) => q.AddColumns(columnGenerator)), null, this.sortOrder, this.disjointColumn);
		}

		// Token: 0x06009DB0 RID: 40368 RVA: 0x00208EF4 File Offset: 0x002070F4
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			TableSortOrder tableSortOrder = this.sortOrder;
			if (tableSortOrder != null && !tableSortOrder.IsEmpty)
			{
				ColumnSelection.SelectMap selected = columnSelection.CreateSelectMap(this.Columns.Length);
				Func<int, bool> <>9__3;
				if (!SortQuery.TryAdjustSelectors(QueryTableValue.NewRowType(this), columnSelection.Keys, tableSortOrder, delegate(QueryExpression selector)
				{
					Func<InvocationQueryExpression, bool> safe = ArgumentAccess.Safe;
					Func<int, bool> func;
					if ((func = <>9__3) == null)
					{
						func = (<>9__3 = (int column) => selected.MapColumn(column) != -1);
					}
					return selector.AllAccess(safe, func);
				}, (QueryExpression selector) => selector.AdjustColumnAccess(new Func<int, int>(selected.MapColumn)), out tableSortOrder))
				{
					return base.SelectColumns(columnSelection);
				}
			}
			int num = -1;
			for (int i = 0; i < columnSelection.Keys.Length; i++)
			{
				if (columnSelection.GetColumn(i) == this.disjointColumn)
				{
					num = i;
					break;
				}
			}
			return Query.Combine(this.ApplyToAll((Query q) => q.SelectColumns(columnSelection)), null, tableSortOrder, num);
		}

		// Token: 0x06009DB1 RID: 40369 RVA: 0x00208FD8 File Offset: 0x002071D8
		public override Query SelectRows(FunctionValue condition)
		{
			return Query.Combine(this.ApplyToAll((Query q) => q.SelectRows(condition)), this.columnTypes, this.sortOrder, this.disjointColumn);
		}

		// Token: 0x06009DB2 RID: 40370 RVA: 0x0020901C File Offset: 0x0020721C
		public override Query Sort(TableSortOrder tableSortOrder)
		{
			return Query.Combine(this.ApplyToAll((Query q) => q.Sort(tableSortOrder)), this.columnTypes, tableSortOrder, this.disjointColumn);
		}

		// Token: 0x06009DB3 RID: 40371 RVA: 0x00209060 File Offset: 0x00207260
		public override Query Distinct(TableDistinct distinctCriteria)
		{
			Query query = Query.Combine(this.ApplyToAll((Query table) => table.Distinct(distinctCriteria)), this.columnTypes, this.sortOrder, this.disjointColumn);
			if (this.disjointColumn == -1)
			{
				query = DistinctQuery.New(distinctCriteria, query, true);
			}
			return query;
		}

		// Token: 0x06009DB4 RID: 40372 RVA: 0x002090BC File Offset: 0x002072BC
		public override Query Skip(RowCount count)
		{
			return base.Skip(count);
		}

		// Token: 0x06009DB5 RID: 40373 RVA: 0x002090C8 File Offset: 0x002072C8
		public override Query Take(RowCount count)
		{
			Query query = this;
			if (count < this.takeCount)
			{
				query = Query.Combine(this.ApplyToAll((Query q) => q.Take(count)), this.columnTypes, this.sortOrder, this.disjointColumn, count);
			}
			if (!count.IsInfinite)
			{
				query = SkipTakeQuery.New(RowRange.All.Take(count), query, true);
			}
			return query;
		}

		// Token: 0x06009DB6 RID: 40374 RVA: 0x00209150 File Offset: 0x00207350
		public override Query Unordered()
		{
			return Query.Combine(this.ApplyToAll((Query q) => q.Unordered()), this.columnTypes, null, this.disjointColumn);
		}

		// Token: 0x06009DB7 RID: 40375 RVA: 0x0020918C File Offset: 0x0020738C
		public override Query Group(Grouping grouping)
		{
			if (this.disjointColumn != -1 && grouping.KeyKeys.Contains(this.Columns[this.disjointColumn]))
			{
				return Query.Combine(this.ApplyToAll((Query q) => q.Group(grouping)), null, null, this.disjointColumn);
			}
			Grouping innerGrouping;
			Grouping grouping2;
			if (this.TryGetGrouping(grouping, out innerGrouping, out grouping2))
			{
				Query query = Query.Combine(this.ApplyToAll((Query q) => q.Group(innerGrouping)), null, null, -1);
				return new GroupQuery(grouping2, query, true);
			}
			return base.Group(grouping);
		}

		// Token: 0x06009DB8 RID: 40376 RVA: 0x00209238 File Offset: 0x00207438
		private bool TryGetGrouping(Grouping grouping, out Grouping innerGrouping, out Grouping outerGrouping)
		{
			innerGrouping = null;
			outerGrouping = null;
			if (grouping.Comparer != null)
			{
				return false;
			}
			CombineQuery.GroupConstructorVisitor groupConstructorVisitor = new CombineQuery.GroupConstructorVisitor(this.Columns, grouping.KeyKeys);
			QueryExpression[] array = new QueryExpression[grouping.Constructors.Length];
			for (int i = 0; i < grouping.Constructors.Length; i++)
			{
				ColumnConstructor columnConstructor = grouping.Constructors[i];
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewTableType(this), columnConstructor.Function);
				try
				{
					array[i] = groupConstructorVisitor.Visit(queryExpression);
				}
				catch (NotSupportedException)
				{
					return false;
				}
			}
			Keys innerResultKeys = groupConstructorVisitor.InnerResultKeys;
			ColumnConstructor[] innerConstructors = groupConstructorVisitor.InnerConstructors;
			TableTypeValue tableTypeValue = TableTypeValue.New(RecordTypeValue.New(innerResultKeys));
			innerGrouping = new Grouping(grouping.Adjacent, innerResultKeys, grouping.KeyKeys, grouping.KeyColumns, innerConstructors, grouping.CompareRecords, null, grouping.GroupTableType);
			ColumnConstructor[] array2 = new ColumnConstructor[array.Length];
			for (int j = 0; j < array2.Length; j++)
			{
				FunctionValue functionValue = QueryExpressionAssembler.Assemble(innerResultKeys, array[j]);
				array2[j] = new ColumnConstructor(grouping.Constructors[j].Name, functionValue, grouping.Constructors[j].Type);
			}
			int[] array3 = new int[grouping.KeyKeys.Length];
			for (int k = 0; k < array3.Length; k++)
			{
				array3[k] = k;
			}
			outerGrouping = new Grouping(grouping.Adjacent, grouping.ResultKeys, grouping.KeyKeys, array3, array2, grouping.CompareRecords, null, tableTypeValue);
			return true;
		}

		// Token: 0x06009DB9 RID: 40377 RVA: 0x002093BC File Offset: 0x002075BC
		public override Query NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			if (joinKind == TableTypeAlgebra.JoinKind.LeftOuter)
			{
				return Query.Combine(this.ApplyToAll((Query q) => q.NestedJoin(leftKeyColumns, rightTable, rightKey, TableTypeAlgebra.JoinKind.LeftOuter, newColumn, joinKeys, keyEqualityComparers)), null, this.sortOrder, this.disjointColumn);
			}
			return base.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers);
		}

		// Token: 0x06009DBA RID: 40378 RVA: 0x00209454 File Offset: 0x00207654
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			Query[] array = new Query[this.queries.Length];
			for (int i = 0; i < this.queries.Length; i++)
			{
				if (!this.queries[i].TryExpandListColumn(columnIndex, singleOrDefault, out array[i]))
				{
					query = null;
					return false;
				}
			}
			query = Query.Combine(array, null, this.sortOrder, this.disjointColumn);
			return true;
		}

		// Token: 0x06009DBB RID: 40379 RVA: 0x002094B8 File Offset: 0x002076B8
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			Query[] array = new Query[this.queries.Length];
			for (int i = 0; i < this.queries.Length; i++)
			{
				if (!this.queries[i].TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out array[i]))
				{
					query = null;
					return false;
				}
			}
			query = Query.Combine(array, null, this.sortOrder, this.disjointColumn);
			return true;
		}

		// Token: 0x06009DBC RID: 40380 RVA: 0x0020951C File Offset: 0x0020771C
		public override bool TryJoinAsLeft(RowCount take, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			return base.TryJoinAsLeft(take, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query);
		}

		// Token: 0x06009DBD RID: 40381 RVA: 0x00209540 File Offset: 0x00207740
		public override bool TryJoinAsRight(RowCount take, Query leftQuery, int[] leftKeyColumns, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			return base.TryJoinAsRight(take, leftQuery, leftKeyColumns, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query);
		}

		// Token: 0x06009DBE RID: 40382 RVA: 0x00209564 File Offset: 0x00207764
		public override TableValue GetPartitionTable(int[] columns)
		{
			int[] array = columns;
			if (this.disjointColumn != -1)
			{
				array = new int[columns.Length + 1];
				Array.Copy(columns, array, columns.Length);
				array[array.Length - 1] = this.disjointColumn;
			}
			Value[] array2 = new TableValue[this.queries.Length];
			Value[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				array3[i] = this.queries[i].GetPartitionTable(array);
			}
			if (array3[0].AsTable.Columns.Length != 0)
			{
				return TableValue.Combine(ListValue.New(array3), null);
			}
			return base.GetPartitionTable(columns);
		}

		// Token: 0x170028BE RID: 10430
		// (get) Token: 0x06009DBF RID: 40383 RVA: 0x002095F8 File Offset: 0x002077F8
		public override IQueryDomain QueryDomain
		{
			get
			{
				IQueryDomain queryDomain = this.queries[0].QueryDomain;
				for (int i = 1; i < this.queries.Length; i++)
				{
					if (!queryDomain.TryGetCompatibleDomain(this.queries[i].QueryDomain, out queryDomain))
					{
						return new CombineQuery.CombineQueryDomain(this);
					}
				}
				return queryDomain;
			}
		}

		// Token: 0x06009DC0 RID: 40384 RVA: 0x00209648 File Offset: 0x00207848
		public override IEnumerable<IValueReference> GetRows()
		{
			TableSortOrder tableSortOrder = this.queries[0].SortOrder;
			IEnumerable<IValueReference>[] array = new IEnumerable<IValueReference>[this.queries.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.queries[i].GetRows();
			}
			if (this.sortOrder != null)
			{
				IComparer<IValueReference> comparer = new ValueReferenceComparer(this.sortOrder.ToComparer());
				return from x in array.MergeSort(comparer)
					select (x);
			}
			return new CombineQuery.CombineEnumerable(array);
		}

		// Token: 0x06009DC1 RID: 40385 RVA: 0x002096DC File Offset: 0x002078DC
		private Query[] ApplyToAll(Func<Query, Query> operation)
		{
			Query[] array = new Query[this.queries.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = operation(this.queries[i]);
			}
			return array;
		}

		// Token: 0x040052E7 RID: 21223
		private Query[] queries;

		// Token: 0x040052E8 RID: 21224
		private TypeValue[] columnTypes;

		// Token: 0x040052E9 RID: 21225
		private TableSortOrder sortOrder;

		// Token: 0x040052EA RID: 21226
		private int disjointColumn;

		// Token: 0x040052EB RID: 21227
		private RowCount takeCount;

		// Token: 0x0200184C RID: 6220
		private class GroupConstructorVisitor : QueryExpressionVisitor
		{
			// Token: 0x06009DC2 RID: 40386 RVA: 0x00209717 File Offset: 0x00207917
			public GroupConstructorVisitor(Keys columns, Keys keyKeys)
			{
				this.columns = columns;
				this.builder = default(KeysBuilder);
				this.innerConstructors = new List<ColumnConstructor>();
				this.columnIndex = keyKeys.Length;
				this.builder.Union(keyKeys);
			}

			// Token: 0x170028BF RID: 10431
			// (get) Token: 0x06009DC3 RID: 40387 RVA: 0x00209755 File Offset: 0x00207955
			public Keys InnerResultKeys
			{
				get
				{
					return this.builder.ToKeys();
				}
			}

			// Token: 0x170028C0 RID: 10432
			// (get) Token: 0x06009DC4 RID: 40388 RVA: 0x00209762 File Offset: 0x00207962
			public ColumnConstructor[] InnerConstructors
			{
				get
				{
					return this.innerConstructors.ToArray();
				}
			}

			// Token: 0x06009DC5 RID: 40389 RVA: 0x00209770 File Offset: 0x00207970
			private int AddInnerColumn(InvocationQueryExpression invocation)
			{
				FunctionValue functionValue = QueryExpressionAssembler.Assemble(this.columns, invocation);
				string text = this.CreateName();
				this.builder.Add(text);
				int num = this.columnIndex;
				this.columnIndex = num + 1;
				int num2 = num;
				this.innerConstructors.Add(new ColumnConstructor(text, functionValue));
				return num2;
			}

			// Token: 0x06009DC6 RID: 40390 RVA: 0x002097C0 File Offset: 0x002079C0
			private string CreateName()
			{
				string text;
				do
				{
					int num = this.id;
					this.id = num + 1;
					int num2 = num;
					text = "_a" + num2.ToString(CultureInfo.InvariantCulture);
				}
				while (this.columns.Contains(text));
				return text;
			}

			// Token: 0x06009DC7 RID: 40391 RVA: 0x00209805 File Offset: 0x00207A05
			protected override QueryExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Expression not supported: {0}.", columnAccess.Kind));
			}

			// Token: 0x06009DC8 RID: 40392 RVA: 0x00209805 File Offset: 0x00207A05
			protected override QueryExpression VisitArgumentAccess(ArgumentAccessQueryExpression argument)
			{
				throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Expression not supported: {0}.", argument.Kind));
			}

			// Token: 0x06009DC9 RID: 40393 RVA: 0x00209828 File Offset: 0x00207A28
			protected override QueryExpression VisitInvocation(InvocationQueryExpression invocation)
			{
				if (invocation.Function.Kind == QueryExpressionKind.Constant && invocation.Arguments.Count == 1 && invocation.Arguments[0].Kind == QueryExpressionKind.ColumnAccess)
				{
					Value value = ((ConstantQueryExpression)invocation.Function).Value;
					if (value.Equals(Library.List.Sum) || value.Equals(Library.List.Min) || value.Equals(Library.List.Max) || value.Equals(Library.List.First))
					{
						int num = this.AddInnerColumn(invocation);
						return new InvocationQueryExpression(invocation.Function, new QueryExpression[]
						{
							new ColumnAccessQueryExpression(num)
						});
					}
					if (value.Equals(Library.List.Average))
					{
						int num2 = this.AddInnerColumn(new InvocationQueryExpression(new ConstantQueryExpression(Library.List.Sum), invocation.Arguments));
						int num3 = this.AddInnerColumn(new InvocationQueryExpression(new ConstantQueryExpression(TableModule.Table.RowCount), new QueryExpression[] { ArgumentAccessQueryExpression.Instance }));
						QueryExpression queryExpression = new InvocationQueryExpression(new ConstantQueryExpression(Library.List.Sum), new QueryExpression[]
						{
							new ColumnAccessQueryExpression(num2)
						});
						QueryExpression queryExpression2 = new InvocationQueryExpression(new ConstantQueryExpression(Library.List.Sum), new QueryExpression[]
						{
							new ColumnAccessQueryExpression(num3)
						});
						return new BinaryQueryExpression(BinaryOperator2.Divide, queryExpression, queryExpression2);
					}
				}
				if (invocation.Function.Kind == QueryExpressionKind.Constant && invocation.Arguments.Count == 1 && invocation.Arguments[0].Kind == QueryExpressionKind.ArgumentAccess && ((ConstantQueryExpression)invocation.Function).Value.Equals(TableModule.Table.RowCount))
				{
					int num4 = this.AddInnerColumn(invocation);
					return new InvocationQueryExpression(new ConstantQueryExpression(Library.List.Sum), new QueryExpression[]
					{
						new ColumnAccessQueryExpression(num4)
					});
				}
				return base.VisitInvocation(invocation);
			}

			// Token: 0x040052EC RID: 21228
			private Keys columns;

			// Token: 0x040052ED RID: 21229
			private KeysBuilder builder;

			// Token: 0x040052EE RID: 21230
			private List<ColumnConstructor> innerConstructors;

			// Token: 0x040052EF RID: 21231
			private int columnIndex;

			// Token: 0x040052F0 RID: 21232
			private int id;
		}

		// Token: 0x0200184D RID: 6221
		private class CombineQueryDomain : IQueryDomain
		{
			// Token: 0x06009DCA RID: 40394 RVA: 0x002099ED File Offset: 0x00207BED
			public CombineQueryDomain(CombineQuery combineQuery)
			{
				this.combineQuery = combineQuery;
			}

			// Token: 0x06009DCB RID: 40395 RVA: 0x000952C1 File Offset: 0x000934C1
			public bool IsCompatibleWith(IQueryDomain domain)
			{
				return domain == this;
			}

			// Token: 0x170028C1 RID: 10433
			// (get) Token: 0x06009DCC RID: 40396 RVA: 0x00002105 File Offset: 0x00000305
			public bool CanIndex
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06009DCD RID: 40397 RVA: 0x002099FC File Offset: 0x00207BFC
			public Query Optimize(Query query)
			{
				Query[] array = new Query[this.combineQuery.Queries.Length];
				for (int i = 0; i < array.Length; i++)
				{
					Query query2 = this.combineQuery.Queries[i];
					array[i] = query2.QueryDomain.Optimize(query2);
				}
				Query query3 = Query.Combine(array, this.combineQuery.ColumnTypes, this.combineQuery.SortOrder, this.combineQuery.DisjointColumn);
				return new ReplaceQueryVisitor(this.combineQuery, query3).VisitQuery(query);
			}

			// Token: 0x040052F1 RID: 21233
			private CombineQuery combineQuery;
		}

		// Token: 0x0200184E RID: 6222
		private class CombineEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06009DCE RID: 40398 RVA: 0x00209A81 File Offset: 0x00207C81
			public CombineEnumerable(IEnumerable<IValueReference>[] rowSets)
			{
				this.rowSets = rowSets;
			}

			// Token: 0x06009DCF RID: 40399 RVA: 0x00209A90 File Offset: 0x00207C90
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06009DD0 RID: 40400 RVA: 0x00209A98 File Offset: 0x00207C98
			public IEnumerator<IValueReference> GetEnumerator()
			{
				return new CombineQuery.CombineEnumerable.CombineEnumerator(this.rowSets);
			}

			// Token: 0x040052F2 RID: 21234
			private readonly IEnumerable<IValueReference>[] rowSets;

			// Token: 0x0200184F RID: 6223
			private class CombineEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06009DD1 RID: 40401 RVA: 0x00209AA5 File Offset: 0x00207CA5
				public CombineEnumerator(IEnumerable<IValueReference>[] rowSets)
				{
					this.rowSets = rowSets;
				}

				// Token: 0x170028C2 RID: 10434
				// (get) Token: 0x06009DD2 RID: 40402 RVA: 0x00209AB4 File Offset: 0x00207CB4
				public IValueReference Current
				{
					get
					{
						return this.enumerator.Current;
					}
				}

				// Token: 0x170028C3 RID: 10435
				// (get) Token: 0x06009DD3 RID: 40403 RVA: 0x00209AC1 File Offset: 0x00207CC1
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06009DD4 RID: 40404 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x06009DD5 RID: 40405 RVA: 0x00209AC9 File Offset: 0x00207CC9
				public void Dispose()
				{
					if (this.enumerator != null)
					{
						this.enumerator.Dispose();
						this.enumerator = null;
					}
				}

				// Token: 0x06009DD6 RID: 40406 RVA: 0x00209AE8 File Offset: 0x00207CE8
				public bool MoveNext()
				{
					for (;;)
					{
						if (this.enumerator == null)
						{
							if (this.index >= this.rowSets.Length)
							{
								break;
							}
							this.enumerator = this.rowSets[this.index].GetEnumerator();
							this.index++;
						}
						if (this.enumerator.MoveNext())
						{
							return true;
						}
						this.enumerator.Dispose();
						this.enumerator = null;
					}
					return false;
				}

				// Token: 0x040052F3 RID: 21235
				private readonly IEnumerable<IValueReference>[] rowSets;

				// Token: 0x040052F4 RID: 21236
				private int index;

				// Token: 0x040052F5 RID: 21237
				private IEnumerator<IValueReference> enumerator;
			}
		}
	}
}
