using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001806 RID: 6150
	internal abstract class ProjectColumnsQuery : Query
	{
		// Token: 0x06009B8E RID: 39822 RVA: 0x00201CC4 File Offset: 0x001FFEC4
		public static Query New(ColumnSelection columnProjection, Query query)
		{
			ColumnSelection columnSelection;
			ColumnSelection columnSelection2;
			columnProjection.Split(query.Columns, out columnSelection, out columnSelection2);
			return RenameReorderColumnsQuery.New(columnSelection2, SelectColumnsQuery.New(columnSelection, query));
		}

		// Token: 0x06009B8F RID: 39823 RVA: 0x00201CEE File Offset: 0x001FFEEE
		protected ProjectColumnsQuery(ColumnSelection columnSelection, Query innerQuery)
		{
			this.columnSelection = columnSelection;
			this.innerQuery = innerQuery;
		}

		// Token: 0x17002811 RID: 10257
		// (get) Token: 0x06009B90 RID: 39824 RVA: 0x00002139 File Offset: 0x00000339
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.ProjectColumns;
			}
		}

		// Token: 0x17002812 RID: 10258
		// (get) Token: 0x06009B91 RID: 39825 RVA: 0x00201D04 File Offset: 0x001FFF04
		public Query InnerQuery
		{
			get
			{
				return this.innerQuery;
			}
		}

		// Token: 0x17002813 RID: 10259
		// (get) Token: 0x06009B92 RID: 39826 RVA: 0x00201D0C File Offset: 0x001FFF0C
		public override Keys Columns
		{
			get
			{
				return this.columnSelection.Keys;
			}
		}

		// Token: 0x06009B93 RID: 39827 RVA: 0x00201D19 File Offset: 0x001FFF19
		public override TypeValue GetColumnType(int column)
		{
			return this.innerQuery.GetColumnType(this.columnSelection.GetColumn(column));
		}

		// Token: 0x17002814 RID: 10260
		// (get) Token: 0x06009B94 RID: 39828 RVA: 0x00201D32 File Offset: 0x001FFF32
		public override IList<TableKey> TableKeys
		{
			get
			{
				if (this.tableKeys == null)
				{
					this.tableKeys = Microsoft.Mashup.Engine1.Runtime.TableKeys.SelectColumns(this.innerQuery.TableKeys, this.innerQuery.Columns, this.columnSelection);
				}
				return this.tableKeys;
			}
		}

		// Token: 0x17002815 RID: 10261
		// (get) Token: 0x06009B95 RID: 39829 RVA: 0x00201D69 File Offset: 0x001FFF69
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				if (this.computedColumns == null)
				{
					this.computedColumns = Microsoft.Mashup.Engine1.Runtime.ComputedColumns.SelectColumns(this.innerQuery.ComputedColumns, QueryTableValue.NewRowType(this.innerQuery), this.columnSelection);
				}
				return this.computedColumns;
			}
		}

		// Token: 0x17002816 RID: 10262
		// (get) Token: 0x06009B96 RID: 39830 RVA: 0x00201DA0 File Offset: 0x001FFFA0
		public override RowCount RowCount
		{
			get
			{
				return this.innerQuery.RowCount;
			}
		}

		// Token: 0x17002817 RID: 10263
		// (get) Token: 0x06009B97 RID: 39831 RVA: 0x00201DB0 File Offset: 0x001FFFB0
		public override TableSortOrder SortOrder
		{
			get
			{
				if (this.sortOrder == null && this.innerQuery.SortOrder != null && !SortQuery.TryAdjustSelectors(QueryTableValue.NewRowType(this.innerQuery), this.Columns, this.innerQuery.SortOrder, this.ApplyAfter, this.AdjustAfter, out this.sortOrder))
				{
					this.sortOrder = TableSortOrder.Unknown;
				}
				return this.sortOrder;
			}
		}

		// Token: 0x17002818 RID: 10264
		// (get) Token: 0x06009B98 RID: 39832 RVA: 0x00201E18 File Offset: 0x00200018
		public ColumnSelection ColumnSelection
		{
			get
			{
				return this.columnSelection;
			}
		}

		// Token: 0x17002819 RID: 10265
		// (get) Token: 0x06009B99 RID: 39833
		public abstract bool RenameReorder { get; }

		// Token: 0x1700281A RID: 10266
		// (get) Token: 0x06009B9A RID: 39834
		public abstract bool FloatingSelect { get; }

		// Token: 0x06009B9B RID: 39835 RVA: 0x00201E20 File Offset: 0x00200020
		public override TableValue GetPartitionTable(int[] columns)
		{
			int[] array = new int[columns.Length];
			for (int i = 0; i < columns.Length; i++)
			{
				array[i] = this.ColumnSelection.GetColumn(columns[i]);
			}
			TableValue partitionTable = this.InnerQuery.GetPartitionTable(array);
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			for (int j = 0; j < this.ColumnSelection.Keys.Length; j++)
			{
				int num;
				if (partitionTable.Columns.TryGetKeyIndex(this.InnerQuery.Columns[this.ColumnSelection.GetColumn(j)], out num))
				{
					columnSelectionBuilder.Add(this.ColumnSelection.Keys[j], num);
				}
			}
			return partitionTable.SelectColumns(columnSelectionBuilder.ToColumnSelection());
		}

		// Token: 0x06009B9C RID: 39836 RVA: 0x00201EE0 File Offset: 0x002000E0
		public override Query SelectRows(FunctionValue condition)
		{
			FunctionValue functionValue;
			FunctionValue functionValue2;
			if ((this.RenameReorder || this.FloatingSelect) && SelectRowsQuery.TryGetAndAdjustConditions(QueryTableValue.NewRowType(this), this.InnerQuery.Columns, condition, this.ApplyBefore, this.AdjustBefore, out functionValue, out functionValue2) && functionValue != null)
			{
				Query query = this.CreateProjectColumns(this.InnerQuery.SelectRows(functionValue), this.ColumnSelection);
				if (functionValue2 != null)
				{
					query = query.SelectRows(functionValue2);
				}
				return query;
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06009B9D RID: 39837 RVA: 0x00201F58 File Offset: 0x00200158
		public override Query AddColumns(ColumnsConstructor columnGenerator)
		{
			Query query;
			ColumnSelection columnSelection;
			if ((this.RenameReorder || this.FloatingSelect) && this.TryAddColumns(columnGenerator, out query, out columnSelection))
			{
				return this.CreateProjectColumns(query, columnSelection);
			}
			return base.AddColumns(columnGenerator);
		}

		// Token: 0x06009B9E RID: 39838 RVA: 0x00201F94 File Offset: 0x00200194
		protected bool TryAddColumns(ColumnsConstructor columnGenerator, out Query query, out ColumnSelection newColumnProjection)
		{
			IList<QueryExpression> list = AddColumnsQuery.CreateQueryExpressions(columnGenerator.Function, QueryTableValue.NewRowType(this));
			if (list != null)
			{
				ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
				columnSelectionBuilder.Add(this.ColumnSelection);
				string[] array = new string[columnGenerator.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = columnGenerator.Names[i];
					columnSelectionBuilder.Add(array[i], this.InnerQuery.Columns.Length + i);
				}
				Keys keys = JoinQuery.EnsureUniqueKeys(Keys.New(array), this.InnerQuery.Columns);
				FunctionValue[] array2 = new FunctionValue[array.Length];
				int num = 0;
				while (num < array.Length && array2 != null)
				{
					if (this.ApplyBefore(list[num]))
					{
						array2[num] = QueryExpressionAssembler.Assemble(this.InnerQuery.Columns, this.AdjustBefore(list[num]));
					}
					else
					{
						array2 = null;
					}
					num++;
				}
				if (array2 != null)
				{
					query = this.InnerQuery.AddColumns(new ColumnsConstructor(keys, new TableValue.FunctionsColumnsConstructorFunctionValue(array2), columnGenerator.Types));
					newColumnProjection = columnSelectionBuilder.ToColumnSelection();
					return true;
				}
			}
			query = null;
			newColumnProjection = null;
			return false;
		}

		// Token: 0x06009B9F RID: 39839 RVA: 0x002020CC File Offset: 0x002002CC
		public override Query Sort(TableSortOrder sortOrder)
		{
			TableSortOrder tableSortOrder;
			if ((this.RenameReorder || this.FloatingSelect) && SortQuery.TryAdjustSelectors(QueryTableValue.NewRowType(this), this.InnerQuery.Columns, sortOrder, this.ApplyBefore, this.AdjustBefore, out tableSortOrder))
			{
				return this.CreateProjectColumns(this.InnerQuery.Sort(tableSortOrder), this.ColumnSelection);
			}
			return base.Sort(sortOrder);
		}

		// Token: 0x06009BA0 RID: 39840 RVA: 0x00202130 File Offset: 0x00200330
		public override Query Distinct(TableDistinct distinctCriteria)
		{
			if (this.RenameReorder || this.FloatingSelect)
			{
				Distinct[] array = new Distinct[distinctCriteria.Distincts.Length];
				bool flag = true;
				int num = 0;
				while (flag && num < array.Length)
				{
					Distinct distinct = distinctCriteria.Distincts[num];
					if (distinct.Comparer != null || distinct.Selector == null)
					{
						goto IL_0094;
					}
					QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this, distinct.Selector);
					if (!this.ApplyBefore(queryExpression))
					{
						goto IL_0094;
					}
					FunctionValue functionValue = QueryExpressionAssembler.Assemble(this.InnerQuery.Columns, this.AdjustBefore(queryExpression));
					array[num] = new Distinct(functionValue, null);
					IL_0096:
					num++;
					continue;
					IL_0094:
					flag = false;
					goto IL_0096;
				}
				if (flag)
				{
					return this.CreateProjectColumns(this.InnerQuery.Distinct(new TableDistinct(array)), this.ColumnSelection);
				}
			}
			return base.Distinct(distinctCriteria);
		}

		// Token: 0x06009BA1 RID: 39841 RVA: 0x00202208 File Offset: 0x00200408
		public override Query Group(Grouping grouping)
		{
			if ((this.RenameReorder || this.FloatingSelect) && grouping.Comparer == null)
			{
				int[] columns = this.ColumnSelection.GetColumns(grouping.KeyColumns);
				KeysBuilder keysBuilder = default(KeysBuilder);
				ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
				for (int i = 0; i < columns.Length; i++)
				{
					keysBuilder.Add(this.InnerQuery.Columns[columns[i]]);
					columnSelectionBuilder.Add(this.Columns[grouping.KeyColumns[i]]);
				}
				Keys keys = keysBuilder.ToKeys();
				ColumnConstructor[] array = new ColumnConstructor[grouping.Constructors.Length];
				for (int j = 0; j < array.Length; j++)
				{
					ColumnConstructor columnConstructor = grouping.Constructors[j];
					string text = JoinQuery.EnsureUniqueKey(columnConstructor.Name, this.InnerQuery.Columns, ref keysBuilder);
					keysBuilder.Add(text);
					QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewTableType(this), columnConstructor.Function);
					if (columnSelectionBuilder.IndexOf(columnConstructor.Name) != -1 || !this.ApplyBefore(queryExpression))
					{
						array = null;
						break;
					}
					columnSelectionBuilder.Add(columnConstructor.Name);
					FunctionValue functionValue = QueryExpressionAssembler.Assemble(this.InnerQuery.Columns, this.AdjustBefore(queryExpression));
					array[j] = new ColumnConstructor(text, functionValue, columnConstructor.Type);
				}
				if (array != null)
				{
					Keys keys2 = keysBuilder.ToKeys();
					Grouping grouping2 = new Grouping(grouping.Adjacent, keys2, keys, columns, array, grouping.CompareRecords, grouping.Comparer, QueryTableValue.NewTableType(this.InnerQuery));
					return this.InnerQuery.Group(grouping2).RenameReorderColumns(columnSelectionBuilder.ToColumnSelection());
				}
			}
			return base.Group(grouping);
		}

		// Token: 0x06009BA2 RID: 39842 RVA: 0x002023D0 File Offset: 0x002005D0
		public override bool TryJoinAsLeft(RowCount take, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			if (this.RenameReorder || this.FloatingSelect)
			{
				Keys keys = JoinQuery.EnsureUniqueKeys(rightQuery.Columns, this.InnerQuery.Columns);
				Keys joinKeys2 = TableValue.GetJoinKeys(this.InnerQuery.Columns, keys);
				JoinColumn[] joinColumns2 = TableValue.GetJoinColumns(joinKeys2, this.InnerQuery.Columns, keys);
				Query query2 = rightQuery;
				if (!keys.Equals(query2.Columns))
				{
					query2 = query2.RenameReorderColumns(new ColumnSelection(keys));
				}
				if (this.InnerQuery.TryJoinAsLeft(take, this.ColumnSelection.GetColumns(leftKeyColumns), query2, rightKeyColumns, joinKind, joinKeys2, joinColumns2, joinAlgorithm, keyEqualityComparers, out query))
				{
					ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
					for (int i = 0; i < joinColumns.Length; i++)
					{
						JoinColumn joinColumn = joinColumns[i];
						if (joinColumn.Left)
						{
							columnSelectionBuilder.Add(this.ColumnSelection.Keys[joinColumn.LeftColumn], this.ColumnSelection.GetColumn(joinColumn.LeftColumn));
						}
						else
						{
							columnSelectionBuilder.Add(joinKeys[i], this.InnerQuery.Columns.Length + joinColumn.RightColumn);
						}
					}
					query = this.CreateProjectColumns(query, columnSelectionBuilder.ToColumnSelection());
					return true;
				}
			}
			query = null;
			return false;
		}

		// Token: 0x06009BA3 RID: 39843 RVA: 0x00202518 File Offset: 0x00200718
		public override bool TryJoinAsRight(RowCount take, Query leftQuery, int[] leftKeyColumns, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			if (this.RenameReorder || this.FloatingSelect)
			{
				Keys keys = JoinQuery.EnsureUniqueKeys(leftQuery.Columns, this.InnerQuery.Columns);
				Keys joinKeys2 = TableValue.GetJoinKeys(keys, this.InnerQuery.Columns);
				JoinColumn[] joinColumns2 = TableValue.GetJoinColumns(joinKeys2, keys, this.InnerQuery.Columns);
				Query query2 = leftQuery;
				if (!keys.Equals(query2.Columns))
				{
					query2 = query2.RenameReorderColumns(new ColumnSelection(keys));
				}
				if (this.InnerQuery.TryJoinAsRight(take, query2, leftKeyColumns, this.ColumnSelection.GetColumns(rightKeyColumns), joinKind, joinKeys2, joinColumns2, joinAlgorithm, keyEqualityComparers, out query))
				{
					ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
					for (int i = 0; i < joinColumns.Length; i++)
					{
						JoinColumn joinColumn = joinColumns[i];
						if (joinColumn.Left)
						{
							columnSelectionBuilder.Add(joinKeys[i], joinColumn.LeftColumn);
						}
						else
						{
							columnSelectionBuilder.Add(this.ColumnSelection.Keys[joinColumn.RightColumn], query2.Columns.Length + this.ColumnSelection.GetColumn(joinColumn.RightColumn));
						}
					}
					query = this.CreateProjectColumns(query, columnSelectionBuilder.ToColumnSelection());
					return true;
				}
			}
			query = null;
			return false;
		}

		// Token: 0x06009BA4 RID: 39844 RVA: 0x0020265C File Offset: 0x0020085C
		public override Query NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			if (this.RenameReorder || this.FloatingSelect)
			{
				string text = JoinQuery.EnsureUniqueKey(newColumn, this.InnerQuery.Columns);
				KeysBuilder keysBuilder = default(KeysBuilder);
				keysBuilder.Union(this.InnerQuery.Columns);
				keysBuilder.Add(text);
				Query query = this.InnerQuery.NestedJoin(this.ColumnSelection.GetColumns(leftKeyColumns), rightTable, rightKey, joinKind, text, keysBuilder.ToKeys(), keyEqualityComparers);
				return this.CreateProjectColumns(query, new ColumnSelection(query.Columns).SelectColumns(this.ColumnSelection.Add(newColumn, query.Columns.Length - 1)));
			}
			return base.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers);
		}

		// Token: 0x06009BA5 RID: 39845 RVA: 0x0020271A File Offset: 0x0020091A
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			if (this.innerQuery.TryExpandListColumn(this.columnSelection.GetColumn(columnIndex), singleOrDefault, out query))
			{
				query = this.CreateProjectColumns(query, this.columnSelection);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009BA6 RID: 39846 RVA: 0x00202750 File Offset: 0x00200950
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			int column = this.columnSelection.GetColumn(columnToExpand);
			int num = fieldsToProject.Length - 1;
			for (int i = 0; i < this.columnSelection.Keys.Length + num; i++)
			{
				if (i < columnToExpand)
				{
					int num2 = this.columnSelection.GetColumn(i);
					if (num2 > column)
					{
						num2 += num;
					}
					columnSelectionBuilder.Add(this.Columns[i], num2);
				}
				else if (i <= columnToExpand + num)
				{
					int num3 = i - columnToExpand;
					int num4 = column + num3;
					columnSelectionBuilder.Add(newColumns[num3], num4);
				}
				else
				{
					int num5 = this.columnSelection.GetColumn(i - num);
					if (num5 > column)
					{
						num5 += num;
					}
					columnSelectionBuilder.Add(this.Columns[i - num], num5);
				}
			}
			Keys keys = JoinQuery.EnsureUniqueKeys(newColumns, this.innerQuery.Columns);
			if (this.innerQuery.TryExpandRecordColumn(this.columnSelection.GetColumn(columnToExpand), fieldsToProject, keys, out query))
			{
				query = this.CreateProjectColumns(query, columnSelectionBuilder.ToColumnSelection());
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009BA7 RID: 39847 RVA: 0x0020287C File Offset: 0x00200A7C
		public override Query Unordered()
		{
			return this.CreateProjectColumns(this.innerQuery.Unordered(), this.columnSelection);
		}

		// Token: 0x1700281B RID: 10267
		// (get) Token: 0x06009BA8 RID: 39848 RVA: 0x00202895 File Offset: 0x00200A95
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.innerQuery.QueryDomain;
			}
		}

		// Token: 0x06009BA9 RID: 39849 RVA: 0x002028A2 File Offset: 0x00200AA2
		public override IEnumerable<IValueReference> GetRows()
		{
			return new ProjectColumnsQuery.SelectColumnsEnumerable(this.innerQuery.GetRows(), this.columnSelection);
		}

		// Token: 0x06009BAA RID: 39850 RVA: 0x002028BA File Offset: 0x00200ABA
		public override bool TryGetReader(out IPageReader reader)
		{
			if (this.InnerQuery.TryGetReader(out reader))
			{
				reader = new ProjectColumnsPageReader(reader, this.ColumnSelection);
				return true;
			}
			reader = null;
			return false;
		}

		// Token: 0x06009BAB RID: 39851
		protected abstract Query CreateProjectColumns(Query innerQuery, ColumnSelection columnProjection);

		// Token: 0x1700281C RID: 10268
		// (get) Token: 0x06009BAC RID: 39852 RVA: 0x002028DF File Offset: 0x00200ADF
		private ColumnSelection.SelectMap SelectMap
		{
			get
			{
				if (this.selectMap == null)
				{
					this.selectMap = this.ColumnSelection.CreateSelectMap(this.InnerQuery.Columns);
				}
				return this.selectMap;
			}
		}

		// Token: 0x1700281D RID: 10269
		// (get) Token: 0x06009BAD RID: 39853 RVA: 0x0020290B File Offset: 0x00200B0B
		protected Func<QueryExpression, bool> ApplyAfter
		{
			get
			{
				return (QueryExpression node) => node.AllAccess(ArgumentAccess.Deny, (int column) => this.SelectMap.MapColumn(column) != -1);
			}
		}

		// Token: 0x1700281E RID: 10270
		// (get) Token: 0x06009BAE RID: 39854 RVA: 0x00202919 File Offset: 0x00200B19
		protected Func<QueryExpression, QueryExpression> AdjustAfter
		{
			get
			{
				return (QueryExpression node) => node.AdjustColumnAccess((int column) => this.SelectMap.MapColumn(column));
			}
		}

		// Token: 0x1700281F RID: 10271
		// (get) Token: 0x06009BAF RID: 39855 RVA: 0x00202927 File Offset: 0x00200B27
		protected Func<QueryExpression, bool> ApplyBefore
		{
			get
			{
				return (QueryExpression node) => node.AllAccess(ArgumentAccess.Safe, (int column) => true);
			}
		}

		// Token: 0x17002820 RID: 10272
		// (get) Token: 0x06009BB0 RID: 39856 RVA: 0x00202948 File Offset: 0x00200B48
		protected Func<QueryExpression, QueryExpression> AdjustBefore
		{
			get
			{
				return (QueryExpression node) => node.AdjustColumnAccess(this.ColumnSelection);
			}
		}

		// Token: 0x06009BB1 RID: 39857 RVA: 0x00202956 File Offset: 0x00200B56
		public static IEnumerator<IValueReference> Project(IEnumerator<IValueReference> rows, ColumnSelection columnSelection)
		{
			return new ProjectColumnsQuery.SelectColumnsEnumerable.SelectColumnsEnumerator(rows, columnSelection);
		}

		// Token: 0x06009BB2 RID: 39858 RVA: 0x00202960 File Offset: 0x00200B60
		public static RecordValue Project(RecordValue row, ColumnSelection columnSelection)
		{
			IValueReference[] array = new IValueReference[columnSelection.Keys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = row.GetReference(columnSelection.GetColumn(i));
			}
			return RecordValue.New(columnSelection.Keys, array);
		}

		// Token: 0x04005224 RID: 21028
		private Query innerQuery;

		// Token: 0x04005225 RID: 21029
		private ColumnSelection columnSelection;

		// Token: 0x04005226 RID: 21030
		private IList<TableKey> tableKeys;

		// Token: 0x04005227 RID: 21031
		private IList<ComputedColumn> computedColumns;

		// Token: 0x04005228 RID: 21032
		private TableSortOrder sortOrder;

		// Token: 0x04005229 RID: 21033
		private ColumnSelection.SelectMap selectMap;

		// Token: 0x02001807 RID: 6151
		private class SelectColumnsEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06009BB8 RID: 39864 RVA: 0x00202A05 File Offset: 0x00200C05
			public SelectColumnsEnumerable(IEnumerable<IValueReference> rows, ColumnSelection columnSelection)
			{
				this.rows = rows;
				this.columnSelection = columnSelection;
			}

			// Token: 0x06009BB9 RID: 39865 RVA: 0x00202A1B File Offset: 0x00200C1B
			public IEnumerator<IValueReference> GetEnumerator()
			{
				return new ProjectColumnsQuery.SelectColumnsEnumerable.SelectColumnsEnumerator(this.rows.GetEnumerator(), this.columnSelection);
			}

			// Token: 0x06009BBA RID: 39866 RVA: 0x00202A33 File Offset: 0x00200C33
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400522A RID: 21034
			private IEnumerable<IValueReference> rows;

			// Token: 0x0400522B RID: 21035
			private ColumnSelection columnSelection;

			// Token: 0x02001808 RID: 6152
			public class SelectColumnsEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06009BBB RID: 39867 RVA: 0x00202A3B File Offset: 0x00200C3B
				public SelectColumnsEnumerator(IEnumerator<IValueReference> rowEnumerator, ColumnSelection columnSelection)
				{
					this.enumerator = rowEnumerator;
					this.columnSelection = columnSelection;
				}

				// Token: 0x17002821 RID: 10273
				// (get) Token: 0x06009BBC RID: 39868 RVA: 0x00202A51 File Offset: 0x00200C51
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06009BBD RID: 39869 RVA: 0x00202A59 File Offset: 0x00200C59
				public void Dispose()
				{
					this.enumerator.Dispose();
				}

				// Token: 0x06009BBE RID: 39870 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x17002822 RID: 10274
				// (get) Token: 0x06009BBF RID: 39871 RVA: 0x00202A66 File Offset: 0x00200C66
				public IValueReference Current
				{
					get
					{
						if (this.current == null)
						{
							this.current = ProjectColumnsQuery.Project(this.enumerator.Current.Value.AsRecord, this.columnSelection);
						}
						return this.current;
					}
				}

				// Token: 0x06009BC0 RID: 39872 RVA: 0x00202A9C File Offset: 0x00200C9C
				public bool MoveNext()
				{
					this.current = null;
					return this.enumerator.MoveNext();
				}

				// Token: 0x0400522C RID: 21036
				private IEnumerator<IValueReference> enumerator;

				// Token: 0x0400522D RID: 21037
				private ColumnSelection columnSelection;

				// Token: 0x0400522E RID: 21038
				private IValueReference current;
			}
		}
	}
}
