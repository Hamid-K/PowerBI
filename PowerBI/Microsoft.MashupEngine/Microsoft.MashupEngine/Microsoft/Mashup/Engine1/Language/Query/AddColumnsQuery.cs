using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001815 RID: 6165
	internal class AddColumnsQuery : Query
	{
		// Token: 0x06009C1B RID: 39963 RVA: 0x00203586 File Offset: 0x00201786
		public AddColumnsQuery(ColumnsConstructor columnGenerator, Query innerQuery)
		{
			this.columnGenerator = columnGenerator;
			this.innerQuery = innerQuery;
		}

		// Token: 0x1700283C RID: 10300
		// (get) Token: 0x06009C1C RID: 39964 RVA: 0x0000240C File Offset: 0x0000060C
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.AddColumns;
			}
		}

		// Token: 0x1700283D RID: 10301
		// (get) Token: 0x06009C1D RID: 39965 RVA: 0x0020359C File Offset: 0x0020179C
		public override Keys Columns
		{
			get
			{
				if (this.columns == null)
				{
					KeysBuilder keysBuilder = new KeysBuilder(this.innerQuery.Columns.Length + this.columnGenerator.Length);
					keysBuilder.Union(this.innerQuery.Columns);
					for (int i = 0; i < this.columnGenerator.Length; i++)
					{
						keysBuilder.Add(this.columnGenerator.Names[i]);
					}
					this.columns = keysBuilder.ToKeys();
				}
				return this.columns;
			}
		}

		// Token: 0x06009C1E RID: 39966 RVA: 0x00203628 File Offset: 0x00201828
		public override TypeValue GetColumnType(int column)
		{
			int num = column - this.innerQuery.Columns.Length;
			if (num >= 0 && num < this.columnGenerator.Length)
			{
				return this.columnGenerator.Types[num].Value.AsType;
			}
			return this.innerQuery.GetColumnType(column);
		}

		// Token: 0x1700283E RID: 10302
		// (get) Token: 0x06009C1F RID: 39967 RVA: 0x0020367E File Offset: 0x0020187E
		public override RowCount RowCount
		{
			get
			{
				return this.innerQuery.RowCount;
			}
		}

		// Token: 0x1700283F RID: 10303
		// (get) Token: 0x06009C20 RID: 39968 RVA: 0x0020368B File Offset: 0x0020188B
		public override IList<TableKey> TableKeys
		{
			get
			{
				return this.innerQuery.TableKeys;
			}
		}

		// Token: 0x17002840 RID: 10304
		// (get) Token: 0x06009C21 RID: 39969 RVA: 0x00203698 File Offset: 0x00201898
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				if (this.computedColumns == null)
				{
					if (this.QueryExpressions != null)
					{
						IList<ComputedColumn> list = this.innerQuery.ComputedColumns;
						ComputedColumn[] array = new ComputedColumn[list.Count + this.columnGenerator.Length];
						for (int i = 0; i < list.Count; i++)
						{
							array[i] = list[i];
						}
						for (int j = 0; j < this.columnGenerator.Length; j++)
						{
							FunctionValue functionValue = QueryExpressionAssembler.Assemble(this.Columns, this.QueryExpressions[j]);
							array[list.Count + j] = new ComputedColumn(this.innerQuery.Columns.Length + j, functionValue, this.columnGenerator.Types[j]);
						}
						this.computedColumns = array;
					}
					else
					{
						this.computedColumns = Microsoft.Mashup.Engine1.Runtime.ComputedColumns.None;
					}
				}
				return this.computedColumns;
			}
		}

		// Token: 0x17002841 RID: 10305
		// (get) Token: 0x06009C22 RID: 39970 RVA: 0x00203773 File Offset: 0x00201973
		public Query InnerQuery
		{
			get
			{
				return this.innerQuery;
			}
		}

		// Token: 0x17002842 RID: 10306
		// (get) Token: 0x06009C23 RID: 39971 RVA: 0x0020377B File Offset: 0x0020197B
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.innerQuery.SortOrder;
			}
		}

		// Token: 0x17002843 RID: 10307
		// (get) Token: 0x06009C24 RID: 39972 RVA: 0x00203788 File Offset: 0x00201988
		public ColumnsConstructor ColumnsConstructor
		{
			get
			{
				return this.columnGenerator;
			}
		}

		// Token: 0x06009C25 RID: 39973 RVA: 0x00203790 File Offset: 0x00201990
		public override TableValue GetPartitionTable(int[] columns)
		{
			List<int> list = new List<int>();
			foreach (int num in columns)
			{
				if (num < this.innerQuery.Columns.Length)
				{
					list.Add(num);
				}
			}
			TableValue tableValue = this.innerQuery.GetPartitionTable(list.ToArray());
			IList<QueryExpression> list2 = this.QueryExpressions;
			if (list2 != null)
			{
				bool[] array = new bool[list2.Count];
				foreach (int num2 in columns)
				{
					if (num2 >= this.innerQuery.Columns.Length)
					{
						array[num2 - this.innerQuery.Columns.Length] = true;
					}
				}
				List<Value> list3 = new List<Value>();
				List<FunctionValue> list4 = new List<FunctionValue>();
				List<IValueReference> list5 = new List<IValueReference>();
				for (int k = 0; k < list2.Count; k++)
				{
					if (array[k])
					{
						list3.Add(TextValue.New(this.columnGenerator.Names[k]));
						list4.Add(QueryExpressionAssembler.Assemble(this.Columns, list2[k]));
						list5.Add(this.columnGenerator.Types[k]);
					}
				}
				tableValue = tableValue.AddColumns(ListValue.New(list3.ToArray()), new TableValue.FunctionsColumnsConstructorFunctionValue(list4.ToArray()), ListValue.New(list5.ToArray()));
			}
			return tableValue;
		}

		// Token: 0x06009C26 RID: 39974 RVA: 0x002038EB File Offset: 0x00201AEB
		public override Query Take(RowCount count)
		{
			return this.innerQuery.Take(count).AddColumns(this.columnGenerator);
		}

		// Token: 0x06009C27 RID: 39975 RVA: 0x00203904 File Offset: 0x00201B04
		public override Query Skip(RowCount count)
		{
			return this.innerQuery.Skip(count).AddColumns(this.columnGenerator);
		}

		// Token: 0x06009C28 RID: 39976 RVA: 0x00203920 File Offset: 0x00201B20
		public override Query AddColumns(ColumnsConstructor columnGenerator)
		{
			IList<QueryExpression> list = AddColumnsQuery.CreateQueryExpressions(columnGenerator.Function, QueryTableValue.NewRowType(this));
			if (list != null && this.QueryExpressions != null)
			{
				bool[] array = new bool[columnGenerator.Names.Length];
				KeysBuilder keysBuilder = default(KeysBuilder);
				KeysBuilder keysBuilder2 = default(KeysBuilder);
				ArrayBuilder<FunctionValue> arrayBuilder = default(ArrayBuilder<FunctionValue>);
				ArrayBuilder<FunctionValue> arrayBuilder2 = default(ArrayBuilder<FunctionValue>);
				ArrayBuilder<IValueReference> arrayBuilder3 = default(ArrayBuilder<IValueReference>);
				ArrayBuilder<IValueReference> arrayBuilder4 = default(ArrayBuilder<IValueReference>);
				for (int i = 0; i < this.QueryExpressions.Count; i++)
				{
					keysBuilder.Add(this.columnGenerator.Names[i]);
					arrayBuilder.Add(QueryExpressionAssembler.Assemble(this.innerQuery.Columns, this.QueryExpressions[i]));
					arrayBuilder3.Add(this.columnGenerator.Types[i]);
				}
				for (int j = 0; j < list.Count; j++)
				{
					QueryExpression queryExpression = list[j];
					if (queryExpression.AllAccess(ArgumentAccess.Safe, (int c) => c < this.innerQuery.Columns.Length))
					{
						array[j] = true;
						keysBuilder.Add(columnGenerator.Names[j]);
						arrayBuilder.Add(QueryExpressionAssembler.Assemble(this.innerQuery.Columns, queryExpression));
						arrayBuilder3.Add(columnGenerator.Types[j]);
					}
					else
					{
						keysBuilder2.Add(columnGenerator.Names[j]);
						arrayBuilder2.Add(QueryExpressionAssembler.Assemble(this.Columns, queryExpression));
						arrayBuilder4.Add(columnGenerator.Types[j]);
					}
				}
				if (arrayBuilder.Count > this.QueryExpressions.Count)
				{
					Query query = this.innerQuery.AddColumns(new ColumnsConstructor(keysBuilder.ToKeys(), new TableValue.FunctionsColumnsConstructorFunctionValue(arrayBuilder.ToArray()), arrayBuilder3.ToArray()));
					if (arrayBuilder2.Count > 0)
					{
						query = query.AddColumns(new ColumnsConstructor(keysBuilder2.ToKeys(), new TableValue.FunctionsColumnsConstructorFunctionValue(arrayBuilder2.ToArray()), arrayBuilder4.ToArray()));
						ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
						columnSelectionBuilder.Add(new ColumnSelection(this.Columns));
						int num = 0;
						int num2 = 0;
						int num3 = arrayBuilder.Count - this.columnGenerator.Names.Length;
						for (int k = 0; k < columnGenerator.Names.Length; k++)
						{
							int num4;
							if (array[k])
							{
								num4 = this.Columns.Length + num;
								num++;
							}
							else
							{
								num4 = this.Columns.Length + num3 + num2;
								num2++;
							}
							columnSelectionBuilder.Add(columnGenerator.Names[k], num4);
						}
						query = query.RenameReorderColumns(columnSelectionBuilder.ToColumnSelection());
					}
					return query;
				}
			}
			return base.AddColumns(columnGenerator);
		}

		// Token: 0x06009C29 RID: 39977 RVA: 0x00203BEC File Offset: 0x00201DEC
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			IList<QueryExpression> list = this.QueryExpressions;
			Func<int, bool> <>9__1;
			if (columnIndex < this.innerQuery.Columns.Length && list != null && this.innerQuery.TryExpandListColumn(columnIndex, singleOrDefault, out query) && list.All(delegate(QueryExpression expression)
			{
				Func<InvocationQueryExpression, bool> deny = ArgumentAccess.Deny;
				Func<int, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (int column) => column != columnIndex);
				}
				return expression.AllAccess(deny, func);
			}))
			{
				query = query.AddColumns(this.columnGenerator);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009C2A RID: 39978 RVA: 0x00203C68 File Offset: 0x00201E68
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			IList<QueryExpression> list = this.QueryExpressions;
			Query query2;
			if (columnToExpand < this.innerQuery.Columns.Length && list != null && this.innerQuery.TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out query2))
			{
				FunctionValue[] array = new FunctionValue[list.Count];
				int num = 0;
				Func<int, bool> <>9__0;
				Func<int, int> <>9__1;
				while (array != null && num < list.Count)
				{
					QueryExpression queryExpression = list[num];
					Func<InvocationQueryExpression, bool> deny = ArgumentAccess.Deny;
					Func<int, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (int column) => column != columnToExpand);
					}
					if (queryExpression.AllAccess(deny, func))
					{
						QueryExpression queryExpression2 = list[num];
						Func<int, int> func2;
						if ((func2 = <>9__1) == null)
						{
							func2 = (<>9__1 = delegate(int column)
							{
								if (column >= columnToExpand)
								{
									return column + fieldsToProject.Length - 1;
								}
								return column;
							});
						}
						QueryExpression queryExpression3 = queryExpression2.AdjustColumnAccess(func2);
						array[num] = QueryExpressionAssembler.Assemble(query2.Columns, queryExpression3);
					}
					else
					{
						array = null;
					}
					num++;
				}
				if (array != null)
				{
					query = query2.AddColumns(new ColumnsConstructor(this.columnGenerator.Names, new TableValue.FunctionsColumnsConstructorFunctionValue(array), this.columnGenerator.Types));
					return true;
				}
			}
			query = null;
			return false;
		}

		// Token: 0x06009C2B RID: 39979 RVA: 0x00203DA8 File Offset: 0x00201FA8
		public override Query Unordered()
		{
			return this.innerQuery.Unordered().AddColumns(this.columnGenerator);
		}

		// Token: 0x06009C2C RID: 39980 RVA: 0x00203DC0 File Offset: 0x00201FC0
		public override Query NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return base.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers);
		}

		// Token: 0x06009C2D RID: 39981 RVA: 0x00203DD4 File Offset: 0x00201FD4
		public override Query Distinct(TableDistinct distinctCriteria)
		{
			TableDistinct tableDistinct;
			if (this.TryGetInnerDistinct(distinctCriteria, out tableDistinct))
			{
				return this.innerQuery.Distinct(tableDistinct).AddColumns(this.columnGenerator);
			}
			return base.Distinct(distinctCriteria);
		}

		// Token: 0x06009C2E RID: 39982 RVA: 0x00203E0C File Offset: 0x0020200C
		private bool TryGetInnerDistinct(TableDistinct distinctCriteria, out TableDistinct innerDistinctCriteria)
		{
			innerDistinctCriteria = null;
			IList<QueryExpression> columnExpressions = this.QueryExpressions;
			HashSet<int> columnAccesses = new HashSet<int>();
			Func<int, bool> <>9__0;
			for (int i = 0; i < distinctCriteria.Distincts.Length; i++)
			{
				Distinct distinct = distinctCriteria.Distincts[i];
				if (distinct.Comparer != null || distinct.Selector == null)
				{
					return false;
				}
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this, distinct.Selector);
				Func<InvocationQueryExpression, bool> safe = ArgumentAccess.Safe;
				Func<int, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = delegate(int column)
					{
						if (column < this.innerQuery.Columns.Length)
						{
							columnAccesses.Add(column);
							return true;
						}
						if (columnExpressions != null)
						{
							QueryExpression queryExpression2 = columnExpressions[column - this.innerQuery.Columns.Length];
							HashSet<int> expressionColumnAccesses = new HashSet<int>();
							if (queryExpression2.AllAccess(ArgumentAccess.Safe, delegate(int innerColumn)
							{
								expressionColumnAccesses.Add(innerColumn);
								return innerColumn < this.innerQuery.Columns.Length;
							}))
							{
								columnAccesses.UnionWith(expressionColumnAccesses);
								return expressionColumnAccesses.Count == 0 || queryExpression2.Kind == QueryExpressionKind.ColumnAccess;
							}
						}
						return false;
					});
				}
				if (!queryExpression.AllAccess(safe, func))
				{
					return false;
				}
			}
			List<Distinct> list = new List<Distinct>();
			foreach (int num in columnAccesses)
			{
				list.Add(new Distinct(new TableValue.ColumnSelectorFunctionValue(this.innerQuery.Columns[num], num), null));
			}
			innerDistinctCriteria = new TableDistinct(list.ToArray());
			return true;
		}

		// Token: 0x06009C2F RID: 39983 RVA: 0x00203F2C File Offset: 0x0020212C
		public override Query Group(Grouping grouping)
		{
			if (this.CanInnerGroup(grouping))
			{
				return this.innerQuery.Group(grouping);
			}
			return base.Group(grouping);
		}

		// Token: 0x06009C30 RID: 39984 RVA: 0x00203F4C File Offset: 0x0020214C
		private bool CanInnerGroup(Grouping grouping)
		{
			int[] keyColumns = grouping.KeyColumns;
			for (int i = 0; i < keyColumns.Length; i++)
			{
				if (keyColumns[i] >= this.innerQuery.Columns.Length)
				{
					return false;
				}
			}
			if (grouping.Comparer != null)
			{
				return false;
			}
			foreach (ColumnConstructor columnConstructor in grouping.Constructors)
			{
				if (!QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewTableType(this), columnConstructor.Function).AllAccess(ArgumentAccess.Safe, (int column) => column < this.innerQuery.Columns.Length))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06009C31 RID: 39985 RVA: 0x00203FD4 File Offset: 0x002021D4
		public override Query SelectRows(FunctionValue condition)
		{
			Query query;
			if (this.TrySelectRows(new AddColumnsQuery.CreateAddColumns(AddColumnsQuery.DefaultCreateAddColumns), new AddColumnsQuery.TryCreateSelectRows(AddColumnsQuery.DefaultTryCreateSelectRows), condition, out query))
			{
				return query;
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06009C32 RID: 39986 RVA: 0x00204010 File Offset: 0x00202210
		private bool TrySelectRows(AddColumnsQuery.CreateAddColumns createAddColumns, AddColumnsQuery.TryCreateSelectRows tryCreateSelectRows, FunctionValue condition, out Query query)
		{
			QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this, condition);
			IList<QueryExpression> list = this.QueryExpressions;
			if (list != null)
			{
				queryExpression = new AddColumnsQuery.QueryExpressionReplacer(this.innerQuery.Columns.Length, list).Visit(queryExpression);
			}
			FunctionValue functionValue;
			FunctionValue functionValue2;
			if (SelectRowsQuery.TryGetAndAdjustConditions(this.Columns, this.innerQuery.Columns, queryExpression, this.ApplyBefore, this.AdjustBefore, out functionValue, out functionValue2) && functionValue != null)
			{
				Query query2 = createAddColumns(this.columnGenerator, this.innerQuery.SelectRows(functionValue));
				if (functionValue2 != null && !tryCreateSelectRows(query2, functionValue2, out query2))
				{
					query2 = null;
				}
				if (query2 != null)
				{
					query = query2;
					return true;
				}
			}
			query = null;
			return false;
		}

		// Token: 0x06009C33 RID: 39987 RVA: 0x002040B8 File Offset: 0x002022B8
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			IList<QueryExpression> list = this.QueryExpressions;
			if (list != null)
			{
				ColumnSelection.SelectMap map = columnSelection.CreateSelectMap(this.Columns);
				ColumnSelection columnSelection2;
				int[] array;
				FunctionValue[] array2;
				if (AddColumnsQuery.TryGetInnerSelection(this.innerQuery, map, (int c) => map.MapColumn(c) != -1, this.innerQuery.Columns.Length, list, out columnSelection2, out array, out array2))
				{
					ColumnSelection.SelectMap selectMap = columnSelection2.CreateSelectMap(this.innerQuery.Columns);
					ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
					for (int i = 0; i < columnSelection.Keys.Length; i++)
					{
						int column = columnSelection.GetColumn(i);
						if (column < this.innerQuery.Columns.Length)
						{
							columnSelectionBuilder.Add(columnSelection.Keys[i], selectMap.MapColumn(column));
						}
					}
					KeysBuilder keysBuilder = default(KeysBuilder);
					ArrayBuilder<FunctionValue> arrayBuilder = default(ArrayBuilder<FunctionValue>);
					ArrayBuilder<IValueReference> arrayBuilder2 = default(ArrayBuilder<IValueReference>);
					int j = 0;
					int num = 0;
					while (j < list.Count)
					{
						int num2 = this.innerQuery.Columns.Length + j;
						if (map.MapColumn(num2) != -1)
						{
							int num3;
							if (!list[j].TryGetColumnAccess(out num3))
							{
								goto IL_0163;
							}
							int[] array3 = array;
							int num4 = num3;
							int num5 = array3[num4] - 1;
							array3[num4] = num5;
							if (num5 != 0)
							{
								goto IL_0163;
							}
							columnSelectionBuilder.Add(this.columnGenerator.Names[j], selectMap.MapColumn(num3));
							IL_01C9:
							num++;
							goto IL_01CF;
							IL_0163:
							columnSelectionBuilder.Add(this.columnGenerator.Names[j], columnSelection2.Keys.Length + keysBuilder.Count);
							keysBuilder.Add(this.columnGenerator.Names[j]);
							arrayBuilder.Add(array2[num]);
							arrayBuilder2.Add(this.columnGenerator.Types[j]);
							goto IL_01C9;
						}
						IL_01CF:
						j++;
					}
					Query query = this.innerQuery.SelectColumns(columnSelection2);
					if (keysBuilder.Count > 0)
					{
						query = query.AddColumns(new ColumnsConstructor(keysBuilder.ToKeys(), new TableValue.FunctionsColumnsConstructorFunctionValue(arrayBuilder.ToArray()), arrayBuilder2.ToArray()));
					}
					ColumnSelection columnSelection3;
					ColumnSelection columnSelection4;
					columnSelectionBuilder.ToColumnSelection().Split(query.Columns, out columnSelection3, out columnSelection4);
					if (query.Kind == QueryKind.ProjectColumns)
					{
						ProjectColumnsQuery projectColumnsQuery = (ProjectColumnsQuery)query;
						if (!projectColumnsQuery.RenameReorder)
						{
							columnSelection3 = projectColumnsQuery.ColumnSelection.SelectColumns(columnSelection3);
							query = projectColumnsQuery.InnerQuery;
						}
					}
					if (query.Kind == QueryKind.AddColumns)
					{
						query = AddColumnsQuery.WithSelectColumnsQuery.New(columnSelection3, (AddColumnsQuery)query);
					}
					else
					{
						query = SelectColumnsQuery.New(columnSelection3, query);
					}
					return RenameReorderColumnsQuery.New(columnSelection4, query);
				}
			}
			return AddColumnsQuery.WithSelectColumnsQuery.New(columnSelection, this);
		}

		// Token: 0x06009C34 RID: 39988 RVA: 0x0020436C File Offset: 0x0020256C
		public static bool TryGetInnerSelection(Query innerQuery, Func<int, bool> innerRequired, out ColumnSelection innerSelection)
		{
			int[] array;
			FunctionValue[] array2;
			return AddColumnsQuery.TryGetInnerSelection(innerQuery, null, innerRequired, -1, EmptyArray<QueryExpression>.Instance, out innerSelection, out array, out array2);
		}

		// Token: 0x06009C35 RID: 39989 RVA: 0x0020438C File Offset: 0x0020258C
		public static bool TryGetInnerSelection(Query innerQuery, ColumnSelection.SelectMap map, Func<int, bool> innerRequired, int columnExpressionOffset, IList<QueryExpression> columnExpressions, out ColumnSelection innerSelection, out int[] innerSelectedCount, out FunctionValue[] newColumnFunctions)
		{
			int[] innerSelected = new int[innerQuery.Columns.Length];
			for (int i = 0; i < innerQuery.Columns.Length; i++)
			{
				if (innerRequired(i))
				{
					innerSelected[i]++;
				}
			}
			Func<int, bool> <>9__0;
			for (int j = 0; j < columnExpressions.Count; j++)
			{
				if (map.MapColumn(columnExpressionOffset + j) != -1)
				{
					QueryExpression queryExpression = columnExpressions[j];
					Func<InvocationQueryExpression, bool> safe = ArgumentAccess.Safe;
					Func<int, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = delegate(int c)
						{
							innerSelected[c]++;
							return true;
						});
					}
					if (!queryExpression.AllAccess(safe, func))
					{
						innerSelection = null;
						innerSelectedCount = null;
						newColumnFunctions = null;
						return false;
					}
				}
			}
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			for (int k = 0; k < innerQuery.Columns.Length; k++)
			{
				if (innerSelected[k] != 0)
				{
					columnSelectionBuilder.Add(innerQuery.Columns[k], k);
				}
			}
			innerSelection = columnSelectionBuilder.ToColumnSelection();
			ColumnSelection.SelectMap innerMap = innerSelection.CreateSelectMap(innerQuery.Columns);
			ArrayBuilder<FunctionValue> arrayBuilder = default(ArrayBuilder<FunctionValue>);
			Func<int, int> <>9__1;
			for (int l = 0; l < columnExpressions.Count; l++)
			{
				int num = columnExpressionOffset + l;
				if (map.MapColumn(num) != -1)
				{
					Keys keys = innerSelection.Keys;
					QueryExpression queryExpression2 = columnExpressions[l];
					Func<int, int> func2;
					if ((func2 = <>9__1) == null)
					{
						func2 = (<>9__1 = (int c) => innerMap.MapColumn(c));
					}
					arrayBuilder.Add(QueryExpressionAssembler.Assemble(keys, queryExpression2.AdjustColumnAccess(func2)));
				}
			}
			innerSelectedCount = innerSelected;
			newColumnFunctions = arrayBuilder.ToArray();
			return true;
		}

		// Token: 0x06009C36 RID: 39990 RVA: 0x00204534 File Offset: 0x00202734
		public override Query Sort(TableSortOrder sortOrder)
		{
			TableSortOrder tableSortOrder;
			if (this.TryGetInnerSort(sortOrder, out tableSortOrder))
			{
				return this.innerQuery.Sort(tableSortOrder).AddColumns(this.columnGenerator);
			}
			return base.Sort(sortOrder);
		}

		// Token: 0x06009C37 RID: 39991 RVA: 0x0020456C File Offset: 0x0020276C
		private bool TryGetInnerSort(TableSortOrder sortOrder, out TableSortOrder innerSortOrder)
		{
			QueryExpression[] array;
			bool[] array2;
			if (!SortQuery.TryGetSelectors(sortOrder, QueryTableValue.NewRowType(this), out array, out array2))
			{
				innerSortOrder = null;
				return false;
			}
			SortOrder[] array3 = new SortOrder[sortOrder.SortOrders.Length];
			IList<QueryExpression> list = this.QueryExpressions;
			bool flag = true;
			int num = 0;
			while (flag && num < array.Length)
			{
				flag = false;
				if (sortOrder.SortOrders[num].Comparer == null)
				{
					if (list != null)
					{
						array[num] = new AddColumnsQuery.QueryExpressionReplacer(this.innerQuery.Columns.Length, list).Visit(array[num]);
					}
					if (array[num].AllAccess(ArgumentAccess.Safe, (int column) => column < this.innerQuery.Columns.Length))
					{
						array3[num] = new SortOrder(QueryExpressionAssembler.Assemble(this.innerQuery.Columns, array[num]), null, array2[num]);
						flag = true;
					}
				}
				num++;
			}
			if (flag)
			{
				innerSortOrder = new TableSortOrder(array3);
				return true;
			}
			innerSortOrder = null;
			return false;
		}

		// Token: 0x17002844 RID: 10308
		// (get) Token: 0x06009C38 RID: 39992 RVA: 0x00204658 File Offset: 0x00202858
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.innerQuery.QueryDomain;
			}
		}

		// Token: 0x06009C39 RID: 39993 RVA: 0x00204668 File Offset: 0x00202868
		public override IEnumerable<IValueReference> GetRows()
		{
			IEnumerable<IValueReference> enumerable;
			if (Vectorization.TryVectorize(this, out enumerable))
			{
				return enumerable;
			}
			TableValue.FunctionsColumnsConstructorFunctionValue functionsColumnsConstructorFunctionValue = this.columnGenerator.Function as TableValue.FunctionsColumnsConstructorFunctionValue;
			if (functionsColumnsConstructorFunctionValue != null)
			{
				return new AddColumnsQuery.AddColumnsDirectCtorsEnumerable(this.innerQuery.GetRows(), this.Columns, functionsColumnsConstructorFunctionValue.ColumnGenerators);
			}
			return new AddColumnsQuery.AddColumnsEnumerable(this.innerQuery.GetRows(), this.Columns, this.columnGenerator);
		}

		// Token: 0x17002845 RID: 10309
		// (get) Token: 0x06009C3A RID: 39994 RVA: 0x002046D0 File Offset: 0x002028D0
		public IList<QueryExpression> QueryExpressions
		{
			get
			{
				if (this.queryExpressions == null && this.columnGenerator.Function.Expression != null)
				{
					this.queryExpressions = AddColumnsQuery.CreateQueryExpressions(this.columnGenerator.Function, QueryTableValue.NewRowType(this.innerQuery));
				}
				return this.queryExpressions;
			}
		}

		// Token: 0x17002846 RID: 10310
		// (get) Token: 0x06009C3B RID: 39995 RVA: 0x0020471E File Offset: 0x0020291E
		private Func<QueryExpression, bool> ApplyBefore
		{
			get
			{
				return (QueryExpression node) => node.AllAccess(ArgumentAccess.Safe, (int c) => c < this.innerQuery.Columns.Length);
			}
		}

		// Token: 0x17002847 RID: 10311
		// (get) Token: 0x06009C3C RID: 39996 RVA: 0x0020472C File Offset: 0x0020292C
		private Func<QueryExpression, QueryExpression> AdjustBefore
		{
			get
			{
				return (QueryExpression node) => node;
			}
		}

		// Token: 0x06009C3D RID: 39997 RVA: 0x00204750 File Offset: 0x00202950
		public static IList<QueryExpression> CreateQueryExpressions(FunctionValue columnGeneratorFunction, RecordTypeValue rowType)
		{
			IFunctionExpression functionExpression = columnGeneratorFunction.Expression as IFunctionExpression;
			if (functionExpression != null && functionExpression.FunctionType.Parameters.Count == 1)
			{
				IConstantExpression constantExpression = functionExpression.Expression as IConstantExpression;
				if (constantExpression != null && constantExpression.Value.IsList)
				{
					ListValue asList = constantExpression.Value.AsList;
					QueryExpression[] array = new QueryExpression[asList.Count];
					int num = 0;
					foreach (IValueReference valueReference in asList)
					{
						array[num++] = new ConstantQueryExpression(valueReference.Value);
					}
					return array;
				}
				IListExpression listExpression = functionExpression.Expression as IListExpression;
				if (listExpression != null)
				{
					QueryExpression[] array2 = new QueryExpression[listExpression.Members.Count];
					for (int i = 0; i < array2.Length; i++)
					{
						IFunctionExpression functionExpression2;
						if (!AddColumnsQuery.TryGetInnerFunction(listExpression.Members[i], functionExpression.FunctionType.Parameters[0].Identifier, out functionExpression2))
						{
							functionExpression2 = new FunctionExpressionSyntaxNode(functionExpression.FunctionType, listExpression.Members[i]);
						}
						if (!QueryExpressionBuilder.TryToQueryExpression(rowType, functionExpression2, out array2[i]))
						{
							return null;
						}
					}
					return array2;
				}
			}
			return null;
		}

		// Token: 0x06009C3E RID: 39998 RVA: 0x002048A4 File Offset: 0x00202AA4
		public static bool TryGetInnerFunction(IExpression expression, Identifier outerArgument, out IFunctionExpression functionExpr)
		{
			FunctionValue functionValue;
			if (AddColumnsQuery.TryGetInnerFunctionOrExpression(expression, outerArgument, out functionExpr, out functionValue) && functionExpr != null)
			{
				return true;
			}
			functionExpr = null;
			return false;
		}

		// Token: 0x06009C3F RID: 39999 RVA: 0x002048C8 File Offset: 0x00202AC8
		public static bool TryGetInnerFunction(IExpression expression, Identifier outerArgument, out FunctionValue function)
		{
			IFunctionExpression functionExpression;
			if (AddColumnsQuery.TryGetInnerFunctionOrExpression(expression, outerArgument, out functionExpression, out function) && function != null)
			{
				return true;
			}
			function = null;
			return false;
		}

		// Token: 0x06009C40 RID: 40000 RVA: 0x002048EC File Offset: 0x00202AEC
		private static bool TryGetInnerFunctionOrExpression(IExpression expression, Identifier outerArgument, out IFunctionExpression functionExpr, out FunctionValue function)
		{
			functionExpr = null;
			function = null;
			IInvocationExpression invocationExpression = expression as IInvocationExpression;
			Identifier identifier;
			if (invocationExpression != null && invocationExpression.Arguments.Count == 1 && invocationExpression.Arguments[0].TryGetIdentifier(out identifier) && identifier == outerArgument)
			{
				ExpressionKind kind = invocationExpression.Function.Kind;
				if (kind != ExpressionKind.Constant)
				{
					if (kind == ExpressionKind.Function)
					{
						functionExpr = (IFunctionExpression)invocationExpression.Function;
					}
				}
				else
				{
					IConstantExpression constantExpression = (IConstantExpression)invocationExpression.Function;
					function = constantExpression.Value as FunctionValue;
					functionExpr = constantExpression.Value.Expression as IFunctionExpression;
				}
			}
			return functionExpr != null || function != null;
		}

		// Token: 0x06009C41 RID: 40001 RVA: 0x0020498E File Offset: 0x00202B8E
		private static Query DefaultCreateAddColumns(ColumnsConstructor columnGenerator, Query innerQuery)
		{
			return innerQuery.AddColumns(columnGenerator);
		}

		// Token: 0x06009C42 RID: 40002 RVA: 0x00204997 File Offset: 0x00202B97
		private static bool DefaultTryCreateSelectRows(Query query, FunctionValue condition, out Query newQuery)
		{
			newQuery = query.SelectRows(condition);
			return true;
		}

		// Token: 0x04005244 RID: 21060
		private Query innerQuery;

		// Token: 0x04005245 RID: 21061
		private Keys columns;

		// Token: 0x04005246 RID: 21062
		private ColumnsConstructor columnGenerator;

		// Token: 0x04005247 RID: 21063
		private IList<QueryExpression> queryExpressions;

		// Token: 0x04005248 RID: 21064
		private IList<ComputedColumn> computedColumns;

		// Token: 0x02001816 RID: 6166
		// (Invoke) Token: 0x06009C49 RID: 40009
		private delegate Query CreateAddColumns(ColumnsConstructor columnGenerator, Query innerQuery);

		// Token: 0x02001817 RID: 6167
		// (Invoke) Token: 0x06009C4D RID: 40013
		private delegate bool TryCreateSelectRows(Query query, FunctionValue condition, out Query newQuery);

		// Token: 0x02001818 RID: 6168
		private sealed class WithSelectColumnsQuery : SelectColumnsQuery
		{
			// Token: 0x06009C50 RID: 40016 RVA: 0x002049D1 File Offset: 0x00202BD1
			public static Query New(ColumnSelection columnSelection, AddColumnsQuery addColumns)
			{
				if (SelectColumnsQuery.NewQueryRequired(columnSelection, addColumns))
				{
					return new AddColumnsQuery.WithSelectColumnsQuery(columnSelection, addColumns);
				}
				return addColumns;
			}

			// Token: 0x06009C51 RID: 40017 RVA: 0x002049E5 File Offset: 0x00202BE5
			private WithSelectColumnsQuery(ColumnSelection columnSelection, AddColumnsQuery addColumns)
				: base(columnSelection, addColumns)
			{
				this.addColumns = addColumns;
			}

			// Token: 0x06009C52 RID: 40018 RVA: 0x002049F8 File Offset: 0x00202BF8
			public override Query AddColumns(ColumnsConstructor columnGenerator)
			{
				Query query;
				ColumnSelection columnSelection;
				if (!base.TryAddColumns(columnGenerator, out query, out columnSelection))
				{
					return base.AddColumns(columnGenerator);
				}
				if (query.Kind == QueryKind.AddColumns)
				{
					ColumnSelection columnSelection2;
					ColumnSelection columnSelection3;
					columnSelection.Split(query.Columns, out columnSelection2, out columnSelection3);
					return AddColumnsQuery.WithSelectColumnsQuery.New(columnSelection2, (AddColumnsQuery)query).RenameReorderColumns(columnSelection3);
				}
				return ProjectColumnsQuery.New(columnSelection, query);
			}

			// Token: 0x06009C53 RID: 40019 RVA: 0x00204A50 File Offset: 0x00202C50
			public override Query SelectRows(FunctionValue condition)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this, condition);
				if (queryExpression != null && base.ApplyBefore(queryExpression))
				{
					FunctionValue functionValue = QueryExpressionAssembler.Assemble(base.InnerQuery.Columns, base.AdjustBefore(queryExpression));
					Query query;
					if (this.addColumns.TrySelectRows(new AddColumnsQuery.CreateAddColumns(this.CreateAddColumns), new AddColumnsQuery.TryCreateSelectRows(this.TryCreateSelectRows), functionValue, out query))
					{
						return query;
					}
				}
				return base.SelectRows(condition);
			}

			// Token: 0x06009C54 RID: 40020 RVA: 0x00204AC4 File Offset: 0x00202CC4
			private Query CreateAddColumns(ColumnsConstructor columnGenerator, Query innerQuery)
			{
				return innerQuery.AddColumns(columnGenerator).SelectColumns(base.ColumnSelection);
			}

			// Token: 0x06009C55 RID: 40021 RVA: 0x00204AD8 File Offset: 0x00202CD8
			private bool TryCreateSelectRows(Query query, FunctionValue condition, out Query newQuery)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(base.InnerQuery, condition);
				if (queryExpression != null && base.ApplyAfter(queryExpression))
				{
					FunctionValue functionValue = QueryExpressionAssembler.Assemble(base.ColumnSelection.Keys, base.AdjustAfter(queryExpression));
					newQuery = query.SelectRows(functionValue);
					return true;
				}
				newQuery = null;
				return false;
			}

			// Token: 0x06009C56 RID: 40022 RVA: 0x00204B30 File Offset: 0x00202D30
			public override Query Take(RowCount count)
			{
				Query query = this.addColumns.Take(count);
				if (query.Kind == QueryKind.AddColumns)
				{
					return AddColumnsQuery.WithSelectColumnsQuery.New(base.ColumnSelection, (AddColumnsQuery)query);
				}
				return SelectColumnsQuery.New(base.ColumnSelection, query);
			}

			// Token: 0x06009C57 RID: 40023 RVA: 0x00204B74 File Offset: 0x00202D74
			public override Query Skip(RowCount count)
			{
				Query query = this.addColumns.Skip(count);
				if (query.Kind == QueryKind.AddColumns)
				{
					return AddColumnsQuery.WithSelectColumnsQuery.New(base.ColumnSelection, (AddColumnsQuery)query);
				}
				return SelectColumnsQuery.New(base.ColumnSelection, query);
			}

			// Token: 0x04005249 RID: 21065
			private readonly AddColumnsQuery addColumns;
		}

		// Token: 0x02001819 RID: 6169
		private class QueryExpressionReplacer : QueryExpressionVisitor
		{
			// Token: 0x06009C58 RID: 40024 RVA: 0x00204BB5 File Offset: 0x00202DB5
			public QueryExpressionReplacer(int column, IList<QueryExpression> queryExpressions)
			{
				this.column = column;
				this.queryExpressions = queryExpressions;
			}

			// Token: 0x06009C59 RID: 40025 RVA: 0x00204BCC File Offset: 0x00202DCC
			protected override QueryExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
			{
				if (columnAccess.Column >= this.column)
				{
					QueryExpression queryExpression = this.queryExpressions[columnAccess.Column - this.column];
					QueryExpressionKind kind = queryExpression.Kind;
					if (kind - QueryExpressionKind.Constant <= 1)
					{
						return queryExpression;
					}
				}
				return columnAccess;
			}

			// Token: 0x0400524A RID: 21066
			private int column;

			// Token: 0x0400524B RID: 21067
			private IList<QueryExpression> queryExpressions;
		}

		// Token: 0x0200181A RID: 6170
		private class AddColumnsEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06009C5A RID: 40026 RVA: 0x00204C10 File Offset: 0x00202E10
			public AddColumnsEnumerable(IEnumerable<IValueReference> rows, Keys columns, ColumnsConstructor columnGenerator)
			{
				this.rows = rows;
				this.columns = columns;
				this.columnGenerator = columnGenerator;
			}

			// Token: 0x06009C5B RID: 40027 RVA: 0x00204C2D File Offset: 0x00202E2D
			public IEnumerator<IValueReference> GetEnumerator()
			{
				return new AddColumnsQuery.AddColumnsEnumerable.Enumerator(this.rows, this.columns, this.columnGenerator);
			}

			// Token: 0x06009C5C RID: 40028 RVA: 0x00204C46 File Offset: 0x00202E46
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400524C RID: 21068
			private IEnumerable<IValueReference> rows;

			// Token: 0x0400524D RID: 21069
			private Keys columns;

			// Token: 0x0400524E RID: 21070
			private ColumnsConstructor columnGenerator;

			// Token: 0x0200181B RID: 6171
			private class Enumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06009C5D RID: 40029 RVA: 0x00204C4E File Offset: 0x00202E4E
				public Enumerator(IEnumerable<IValueReference> rows, Keys columns, ColumnsConstructor columnGenerator)
				{
					this.enumerator = rows.GetEnumerator();
					this.columns = columns;
					this.columnGenerator = columnGenerator;
				}

				// Token: 0x17002848 RID: 10312
				// (get) Token: 0x06009C5E RID: 40030 RVA: 0x00204C70 File Offset: 0x00202E70
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06009C5F RID: 40031 RVA: 0x00204C78 File Offset: 0x00202E78
				public void Dispose()
				{
					this.enumerator.Dispose();
				}

				// Token: 0x06009C60 RID: 40032 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x17002849 RID: 10313
				// (get) Token: 0x06009C61 RID: 40033 RVA: 0x00204C88 File Offset: 0x00202E88
				public IValueReference Current
				{
					get
					{
						if (this.current == null)
						{
							RecordValue asRecord = this.enumerator.Current.Value.AsRecord;
							IValueReference[] array = new IValueReference[this.columns.Length];
							for (int i = 0; i < asRecord.Keys.Length; i++)
							{
								array[i] = asRecord.GetReference(i);
							}
							IValueReference valueReference = new TransformValueReference(asRecord, this.columnGenerator.Function);
							for (int j = 0; j < this.columnGenerator.Length; j++)
							{
								array[asRecord.Keys.Length + j] = new RequiredElementAccessValueReference(valueReference, j);
							}
							this.current = RecordValue.New(this.columns, array);
						}
						return this.current;
					}
				}

				// Token: 0x06009C62 RID: 40034 RVA: 0x00204D43 File Offset: 0x00202F43
				public bool MoveNext()
				{
					this.current = null;
					return this.enumerator.MoveNext();
				}

				// Token: 0x0400524F RID: 21071
				private IEnumerator<IValueReference> enumerator;

				// Token: 0x04005250 RID: 21072
				private Keys columns;

				// Token: 0x04005251 RID: 21073
				private ColumnsConstructor columnGenerator;

				// Token: 0x04005252 RID: 21074
				private IValueReference current;
			}
		}

		// Token: 0x0200181C RID: 6172
		private class AddColumnsDirectCtorsEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06009C63 RID: 40035 RVA: 0x00204D57 File Offset: 0x00202F57
			public AddColumnsDirectCtorsEnumerable(IEnumerable<IValueReference> rows, Keys columns, FunctionValue[] columnGenerators)
			{
				this.rows = rows;
				this.columns = columns;
				this.columnGenerators = columnGenerators;
			}

			// Token: 0x06009C64 RID: 40036 RVA: 0x00204D74 File Offset: 0x00202F74
			public IEnumerator<IValueReference> GetEnumerator()
			{
				return new AddColumnsQuery.AddColumnsDirectCtorsEnumerable.Enumerator(this.rows, this.columns, this.columnGenerators);
			}

			// Token: 0x06009C65 RID: 40037 RVA: 0x00204D8D File Offset: 0x00202F8D
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x04005253 RID: 21075
			private IEnumerable<IValueReference> rows;

			// Token: 0x04005254 RID: 21076
			private Keys columns;

			// Token: 0x04005255 RID: 21077
			private FunctionValue[] columnGenerators;

			// Token: 0x0200181D RID: 6173
			private class Enumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06009C66 RID: 40038 RVA: 0x00204D95 File Offset: 0x00202F95
				public Enumerator(IEnumerable<IValueReference> rows, Keys columns, FunctionValue[] columnGenerators)
				{
					this.enumerator = rows.GetEnumerator();
					this.columns = columns;
					this.columnGenerators = columnGenerators;
				}

				// Token: 0x1700284A RID: 10314
				// (get) Token: 0x06009C67 RID: 40039 RVA: 0x00204DB7 File Offset: 0x00202FB7
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06009C68 RID: 40040 RVA: 0x00204DBF File Offset: 0x00202FBF
				public void Dispose()
				{
					this.enumerator.Dispose();
				}

				// Token: 0x06009C69 RID: 40041 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x1700284B RID: 10315
				// (get) Token: 0x06009C6A RID: 40042 RVA: 0x00204DCC File Offset: 0x00202FCC
				public IValueReference Current
				{
					get
					{
						if (this.current == null)
						{
							RecordValue asRecord = this.enumerator.Current.Value.AsRecord;
							IValueReference[] array = new IValueReference[this.columns.Length];
							for (int i = 0; i < asRecord.Keys.Length; i++)
							{
								array[i] = asRecord.GetReference(i);
							}
							for (int j = 0; j < this.columnGenerators.Length; j++)
							{
								array[asRecord.Keys.Length + j] = new TransformValueReference(asRecord, this.columnGenerators[j]);
							}
							this.current = RecordValue.New(this.columns, array);
						}
						return this.current;
					}
				}

				// Token: 0x06009C6B RID: 40043 RVA: 0x00204E73 File Offset: 0x00203073
				public bool MoveNext()
				{
					this.current = null;
					return this.enumerator.MoveNext();
				}

				// Token: 0x04005256 RID: 21078
				private IEnumerator<IValueReference> enumerator;

				// Token: 0x04005257 RID: 21079
				private Keys columns;

				// Token: 0x04005258 RID: 21080
				private FunctionValue[] columnGenerators;

				// Token: 0x04005259 RID: 21081
				private IValueReference current;
			}
		}
	}
}
