using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001825 RID: 6181
	internal class NestedJoinQuery : Query
	{
		// Token: 0x06009C7E RID: 40062 RVA: 0x00205024 File Offset: 0x00203224
		public NestedJoinQuery(Query leftQuery, int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumnName, Keys joinKeys, FunctionValue[] keyEqualityComparers, TypeValue columnType)
		{
			this.leftQuery = leftQuery;
			this.leftKeyColumns = leftKeyColumns;
			this.rightTable = rightTable;
			this.rightKey = rightKey;
			this.joinKind = joinKind;
			this.newColumnName = newColumnName;
			this.joinKeys = joinKeys;
			this.keyEqualityComparers = keyEqualityComparers;
			this.columnType = columnType;
		}

		// Token: 0x1700284C RID: 10316
		// (get) Token: 0x06009C7F RID: 40063 RVA: 0x0014025A File Offset: 0x0013E45A
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.NestedJoin;
			}
		}

		// Token: 0x1700284D RID: 10317
		// (get) Token: 0x06009C80 RID: 40064 RVA: 0x0020507C File Offset: 0x0020327C
		public override Keys Columns
		{
			get
			{
				return this.joinKeys;
			}
		}

		// Token: 0x1700284E RID: 10318
		// (get) Token: 0x06009C81 RID: 40065 RVA: 0x00205084 File Offset: 0x00203284
		public TypeValue ColumnType
		{
			get
			{
				return this.columnType;
			}
		}

		// Token: 0x06009C82 RID: 40066 RVA: 0x0020508C File Offset: 0x0020328C
		public override TypeValue GetColumnType(int index)
		{
			if (index < this.leftQuery.Columns.Length)
			{
				return this.leftQuery.GetColumnType(index);
			}
			if (this.columnType == null)
			{
				this.columnType = TypeServices.ConvertToLimitedPreview(new NestedJoinQuery.NestedJoinTableType(this)).AsType;
			}
			return this.columnType;
		}

		// Token: 0x1700284F RID: 10319
		// (get) Token: 0x06009C83 RID: 40067 RVA: 0x002050E0 File Offset: 0x002032E0
		public override IList<TableKey> TableKeys
		{
			get
			{
				TableTypeAlgebra.JoinKind joinKind = this.joinKind;
				if (joinKind <= TableTypeAlgebra.JoinKind.LeftOuter || joinKind == TableTypeAlgebra.JoinKind.LeftSemi)
				{
					return this.leftQuery.TableKeys;
				}
				return Microsoft.Mashup.Engine1.Runtime.TableKeys.None;
			}
		}

		// Token: 0x17002850 RID: 10320
		// (get) Token: 0x06009C84 RID: 40068 RVA: 0x0020510D File Offset: 0x0020330D
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return this.leftQuery.ComputedColumns;
			}
		}

		// Token: 0x17002851 RID: 10321
		// (get) Token: 0x06009C85 RID: 40069 RVA: 0x0020511A File Offset: 0x0020331A
		public override RowCount RowCount
		{
			get
			{
				if (this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter)
				{
					return this.leftQuery.RowCount;
				}
				return base.RowCount;
			}
		}

		// Token: 0x17002852 RID: 10322
		// (get) Token: 0x06009C86 RID: 40070 RVA: 0x00205137 File Offset: 0x00203337
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.leftQuery.SortOrder;
			}
		}

		// Token: 0x17002853 RID: 10323
		// (get) Token: 0x06009C87 RID: 40071 RVA: 0x00205144 File Offset: 0x00203344
		public Query LeftQuery
		{
			get
			{
				return this.leftQuery;
			}
		}

		// Token: 0x17002854 RID: 10324
		// (get) Token: 0x06009C88 RID: 40072 RVA: 0x0020514C File Offset: 0x0020334C
		public int[] LeftKeyColumns
		{
			get
			{
				return this.leftKeyColumns;
			}
		}

		// Token: 0x17002855 RID: 10325
		// (get) Token: 0x06009C89 RID: 40073 RVA: 0x00205154 File Offset: 0x00203354
		public Value DelayedRightTable
		{
			get
			{
				return this.rightTable;
			}
		}

		// Token: 0x17002856 RID: 10326
		// (get) Token: 0x06009C8A RID: 40074 RVA: 0x0020515C File Offset: 0x0020335C
		public Keys RightKey
		{
			get
			{
				return this.rightKey;
			}
		}

		// Token: 0x17002857 RID: 10327
		// (get) Token: 0x06009C8B RID: 40075 RVA: 0x00205164 File Offset: 0x00203364
		public TableTypeAlgebra.JoinKind JoinKind
		{
			get
			{
				return this.joinKind;
			}
		}

		// Token: 0x17002858 RID: 10328
		// (get) Token: 0x06009C8C RID: 40076 RVA: 0x0020516C File Offset: 0x0020336C
		public string NewColumnName
		{
			get
			{
				return this.newColumnName;
			}
		}

		// Token: 0x17002859 RID: 10329
		// (get) Token: 0x06009C8D RID: 40077 RVA: 0x0020507C File Offset: 0x0020327C
		public Keys JoinKeys
		{
			get
			{
				return this.joinKeys;
			}
		}

		// Token: 0x1700285A RID: 10330
		// (get) Token: 0x06009C8E RID: 40078 RVA: 0x00205174 File Offset: 0x00203374
		public FunctionValue[] KeyEqualityComparers
		{
			get
			{
				return this.keyEqualityComparers;
			}
		}

		// Token: 0x1700285B RID: 10331
		// (get) Token: 0x06009C8F RID: 40079 RVA: 0x0020517C File Offset: 0x0020337C
		public TableValue RightTable
		{
			get
			{
				if (this.rightTable.IsFunction)
				{
					this.rightTable = this.rightTable.AsFunction.Invoke();
				}
				return this.rightTable.AsTable;
			}
		}

		// Token: 0x1700285C RID: 10332
		// (get) Token: 0x06009C90 RID: 40080 RVA: 0x002051AC File Offset: 0x002033AC
		public bool IsRelationshipSingleOrDefault
		{
			get
			{
				Query query = this.RightTable.Query;
				int[] columns = TableValue.GetColumns(query.Columns, this.rightKey);
				return Microsoft.Mashup.Engine1.Runtime.TableKeys.IsTableKey(query.TableKeys, columns);
			}
		}

		// Token: 0x06009C91 RID: 40081 RVA: 0x002051E1 File Offset: 0x002033E1
		private int SelectJoinColumn(ColumnSelection columnSelection)
		{
			return columnSelection.CreateSelectMap(this.Columns).MapColumn(this.leftQuery.Columns.Length);
		}

		// Token: 0x06009C92 RID: 40082 RVA: 0x00205204 File Offset: 0x00203404
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			Query query;
			if (this.TrySelectColumns(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), columnSelection, out query))
			{
				return query;
			}
			return base.SelectColumns(columnSelection);
		}

		// Token: 0x06009C93 RID: 40083 RVA: 0x00205234 File Offset: 0x00203434
		private Query CreateNonNestedJoin(TableTypeAlgebra.JoinKind joinKind)
		{
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			ColumnSelection columnSelection = new ColumnSelection(this.RightKey);
			KeysBuilder keysBuilder = new KeysBuilder(this.RightKey.Length);
			int[] array = new int[this.RightKey.Length];
			bool flag = false;
			for (int i = 0; i < this.RightKey.Length; i++)
			{
				string text = this.RightKey[i];
				string text2 = JoinQuery.EnsureUniqueKey(text, this.LeftQuery.Columns, ref keysBuilder);
				if (text2 != text)
				{
					flag = true;
					columnSelection = columnSelection.Rename(i, text2);
				}
				array[i] = i;
				columnSelectionBuilder.Add(text, this.RightTable.Columns.IndexOfKey(text));
			}
			Query query = this.RightTable.Query.SelectColumns(columnSelectionBuilder.ToColumnSelection());
			if (flag)
			{
				query = query.RenameReorderColumns(columnSelection);
			}
			return TableValue.Join(new QueryTableValue(this.LeftQuery), this.LeftKeyColumns, new QueryTableValue(query), array, joinKind, JoinAlgorithm.Dynamic, this.KeyEqualityComparers).Query.SelectColumns(new ColumnSelection(this.LeftQuery.Columns));
		}

		// Token: 0x06009C94 RID: 40084 RVA: 0x0020535C File Offset: 0x0020355C
		private static TableDistinct AllDistinct(Keys keys)
		{
			Distinct[] array = new Distinct[keys.Length];
			for (int i = 0; i < keys.Length; i++)
			{
				array[i] = new Distinct(new TableValue.ColumnSelectorFunctionValue(keys[i], i), null);
			}
			return new TableDistinct(array);
		}

		// Token: 0x06009C95 RID: 40085 RVA: 0x002053A8 File Offset: 0x002035A8
		private bool TrySelectColumns(NestedJoinQuery.CreateNestedJoin createNestedJoin, ColumnSelection columnSelection, out Query query)
		{
			int num = this.SelectJoinColumn(columnSelection);
			if (num == -1)
			{
				Query query2 = null;
				switch (this.joinKind)
				{
				case TableTypeAlgebra.JoinKind.Inner:
					query2 = this.CreateNonNestedJoin(TableTypeAlgebra.JoinKind.LeftSemi);
					break;
				case TableTypeAlgebra.JoinKind.LeftOuter:
					query2 = this.leftQuery;
					break;
				case TableTypeAlgebra.JoinKind.FullOuter:
					query2 = Query.Combine(new Query[]
					{
						this.LeftQuery,
						this.CreateNonNestedJoin(TableTypeAlgebra.JoinKind.RightAnti).Distinct(NestedJoinQuery.AllDistinct(this.LeftQuery.Columns))
					}, null, null, -1);
					break;
				case TableTypeAlgebra.JoinKind.RightOuter:
					query2 = Query.Combine(new Query[]
					{
						this.CreateNonNestedJoin(TableTypeAlgebra.JoinKind.LeftSemi),
						this.CreateNonNestedJoin(TableTypeAlgebra.JoinKind.RightAnti).Distinct(NestedJoinQuery.AllDistinct(this.LeftQuery.Columns))
					}, null, null, -1);
					break;
				case TableTypeAlgebra.JoinKind.LeftAnti:
				case TableTypeAlgebra.JoinKind.LeftSemi:
					query2 = this.CreateNonNestedJoin(this.joinKind);
					break;
				case TableTypeAlgebra.JoinKind.RightAnti:
				case TableTypeAlgebra.JoinKind.RightSemi:
					query2 = this.CreateNonNestedJoin(this.joinKind).Distinct(NestedJoinQuery.AllDistinct(this.LeftQuery.Columns));
					break;
				}
				if (query2 != null)
				{
					query = query2.SelectColumns(columnSelection);
					return true;
				}
			}
			ColumnSelection columnSelection2;
			ColumnSelectionBuilder columnSelectionBuilder;
			int[] array;
			string text;
			KeysBuilder keysBuilder;
			if (NestedJoinQuery.TrySelectColumns(num, this.leftQuery, this.leftKeyColumns, this.joinKind, this.joinKeys, this.newColumnName, columnSelection, out columnSelection2, out columnSelectionBuilder, out array, out text, out keysBuilder))
			{
				query = createNestedJoin(this.leftQuery.SelectColumns(columnSelection2), array, this.rightTable, this.rightKey, this.joinKind, text, keysBuilder.ToKeys(), this.keyEqualityComparers);
				query = FloatingSelectColumnsQuery.New(columnSelectionBuilder.ToColumnSelection(), query);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009C96 RID: 40086 RVA: 0x0020554C File Offset: 0x0020374C
		public override Query SelectRows(FunctionValue condition)
		{
			Query query;
			if (this.TrySelectRows(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), condition, out query))
			{
				return query;
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06009C97 RID: 40087 RVA: 0x0020557C File Offset: 0x0020377C
		private bool TrySelectRows(NestedJoinQuery.CreateNestedJoin createNestedJoin, FunctionValue condition, out Query query)
		{
			FunctionValue functionValue;
			FunctionValue functionValue2;
			if ((this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter || this.joinKind == TableTypeAlgebra.JoinKind.LeftAnti || this.joinKind == TableTypeAlgebra.JoinKind.Inner || this.joinKind == TableTypeAlgebra.JoinKind.LeftSemi) && SelectRowsQuery.TryGetConditions(QueryTableValue.NewRowType(this), this.leftQuery.Columns, condition, this.ApplyBefore, out functionValue, out functionValue2) && functionValue != null)
			{
				query = this.leftQuery.SelectRows(functionValue);
				query = createNestedJoin(query, this.leftKeyColumns, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, this.joinKeys, this.keyEqualityComparers);
				if (functionValue2 != null)
				{
					query = query.SelectRows(functionValue2);
				}
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009C98 RID: 40088 RVA: 0x00205628 File Offset: 0x00203828
		public override Query AddColumns(ColumnsConstructor columnGenerator)
		{
			Query query;
			if (this.TryAddColumns(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), columnGenerator, out query))
			{
				return query;
			}
			return base.AddColumns(columnGenerator);
		}

		// Token: 0x06009C99 RID: 40089 RVA: 0x00205658 File Offset: 0x00203858
		private bool TryAddColumns(NestedJoinQuery.CreateNestedJoin createNestedJoin, ColumnsConstructor columnGenerator, out Query query)
		{
			IList<QueryExpression> list = AddColumnsQuery.CreateQueryExpressions(columnGenerator.Function, QueryTableValue.NewRowType(this));
			if (list != null)
			{
				List<int> list2 = null;
				List<int> list3 = null;
				List<int> list4 = null;
				for (int i = 0; i < list.Count; i++)
				{
					QueryExpression queryExpression = list[i];
					int num;
					if (this.ApplyBefore(queryExpression))
					{
						if (list2 == null)
						{
							list2 = new List<int>();
						}
						list2.Add(i);
					}
					else if (queryExpression.TryGetColumnAccess(out num) && num == this.leftQuery.Columns.Length)
					{
						if (list3 == null)
						{
							list3 = new List<int>();
						}
						list3.Add(i);
					}
					else
					{
						if (list4 == null)
						{
							list4 = new List<int>();
						}
						list4.Add(i);
					}
				}
				if (list2 != null || list3 != null)
				{
					int num2 = ((list2 != null) ? list2.Count : 0);
					int num3 = ((list3 != null) ? list3.Count : 0);
					ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
					columnSelectionBuilder.Add(new ColumnSelection(this.leftQuery.Columns));
					columnSelectionBuilder.Add(this.newColumnName, this.leftQuery.Columns.Length + num2);
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					for (int j = 0; j < list.Count; j++)
					{
						string text = columnGenerator.Names[j];
						if (list2 != null && list2.Contains(j))
						{
							columnSelectionBuilder.Add(text, this.leftQuery.Columns.Length + num4);
							num4++;
						}
						else if (list3 != null && list3.Contains(j))
						{
							columnSelectionBuilder.Add(text, this.leftQuery.Columns.Length + num2 + 1 + num5);
							num5++;
						}
						else
						{
							columnSelectionBuilder.Add(text, this.leftQuery.Columns.Length + num2 + 1 + num3 + num6);
							num6++;
						}
					}
					query = this.leftQuery;
					if (list2 != null)
					{
						query = this.ApplyAddColumns(query, columnGenerator, list, list2, null);
					}
					KeysBuilder keysBuilder = default(KeysBuilder);
					keysBuilder.Union(query.Columns);
					keysBuilder.Add(this.newColumnName);
					query = createNestedJoin(query, this.leftKeyColumns, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, keysBuilder.ToKeys(), this.keyEqualityComparers);
					if (list3 != null)
					{
						for (int k = 0; k < list3.Count; k++)
						{
							string text2 = columnGenerator.Names[list3[k]];
							keysBuilder = default(KeysBuilder);
							keysBuilder.Union(query.Columns);
							keysBuilder.Add(text2);
							query = createNestedJoin(query, this.leftKeyColumns, this.rightTable, this.rightKey, this.joinKind, text2, keysBuilder.ToKeys(), this.keyEqualityComparers);
						}
					}
					if (list4 != null)
					{
						int oldIndex = this.leftQuery.Columns.Length;
						int newIndex = query.Columns.Length - num3 - 1;
						Func<int, int> <>9__1;
						query = this.ApplyAddColumns(query, columnGenerator, list, list4, delegate(QueryExpression node)
						{
							Func<int, int> func;
							if ((func = <>9__1) == null)
							{
								func = (<>9__1 = delegate(int column)
								{
									if (column != oldIndex)
									{
										return column;
									}
									return newIndex;
								});
							}
							return node.AdjustColumnAccess(func);
						});
					}
					query = query.RenameReorderColumns(columnSelectionBuilder.ToColumnSelection());
					return true;
				}
			}
			query = null;
			return false;
		}

		// Token: 0x06009C9A RID: 40090 RVA: 0x00205988 File Offset: 0x00203B88
		private Query ApplyAddColumns(Query query, ColumnsConstructor columnGenerator, IList<QueryExpression> expressions, List<int> indices, Func<QueryExpression, QueryExpression> adjustment)
		{
			string[] array = new string[indices.Count];
			FunctionValue[] array2 = new FunctionValue[indices.Count];
			IValueReference[] array3 = new IValueReference[indices.Count];
			for (int i = 0; i < indices.Count; i++)
			{
				int num = indices[i];
				QueryExpression queryExpression = ((adjustment != null) ? adjustment(expressions[num]) : expressions[num]);
				array2[i] = QueryExpressionAssembler.Assemble(query.Columns, queryExpression);
				array[i] = columnGenerator.Names[num];
				array3[i] = columnGenerator.Types[num];
			}
			return query.AddColumns(new ColumnsConstructor(Keys.New(array), new TableValue.FunctionsColumnsConstructorFunctionValue(array2), array3));
		}

		// Token: 0x06009C9B RID: 40091 RVA: 0x00205A3C File Offset: 0x00203C3C
		public override Query Take(RowCount count)
		{
			Query query;
			if (this.TryTake(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), count, out query))
			{
				return query;
			}
			return base.Take(count);
		}

		// Token: 0x06009C9C RID: 40092 RVA: 0x00205A6C File Offset: 0x00203C6C
		private bool TryTake(NestedJoinQuery.CreateNestedJoin createNestedJoin, RowCount count, out Query query)
		{
			if (this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter)
			{
				query = this.leftQuery.Take(count);
				query = createNestedJoin(query, this.leftKeyColumns, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, this.joinKeys, this.keyEqualityComparers);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009C9D RID: 40093 RVA: 0x00205ACC File Offset: 0x00203CCC
		public override Query Skip(RowCount count)
		{
			Query query;
			if (this.TrySkip(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), count, out query))
			{
				return query;
			}
			return base.Skip(count);
		}

		// Token: 0x06009C9E RID: 40094 RVA: 0x00205AFC File Offset: 0x00203CFC
		private bool TrySkip(NestedJoinQuery.CreateNestedJoin createNestedJoin, RowCount count, out Query query)
		{
			if (this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter)
			{
				query = this.leftQuery.Skip(count);
				query = createNestedJoin(query, this.leftKeyColumns, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, this.joinKeys, this.keyEqualityComparers);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009C9F RID: 40095 RVA: 0x00205B5C File Offset: 0x00203D5C
		public override Query Sort(TableSortOrder sortOrder)
		{
			Query query;
			if (this.TrySort(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), sortOrder, out query))
			{
				return query;
			}
			return base.Sort(sortOrder);
		}

		// Token: 0x06009CA0 RID: 40096 RVA: 0x00205B8C File Offset: 0x00203D8C
		private bool TrySort(NestedJoinQuery.CreateNestedJoin createNestedJoin, TableSortOrder sortOrder, out Query query)
		{
			if (this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter && SortQuery.TryAdjustSelectors(QueryTableValue.NewRowType(this), this.leftQuery.Columns, sortOrder, this.ApplyBefore))
			{
				query = this.leftQuery.Sort(sortOrder);
				query = createNestedJoin(query, this.leftKeyColumns, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, this.joinKeys, this.keyEqualityComparers);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009CA1 RID: 40097 RVA: 0x00205C0C File Offset: 0x00203E0C
		public override bool TryJoinAsLeft(RowCount take, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			return this.TryJoinAsLeft(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), take, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query);
		}

		// Token: 0x06009CA2 RID: 40098 RVA: 0x00205C3C File Offset: 0x00203E3C
		private bool TryJoinAsLeft(NestedJoinQuery.CreateNestedJoin createNestedJoin, RowCount take, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			if (this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter && Array.IndexOf<int>(leftKeyColumns, this.leftQuery.Columns.Length) == -1)
			{
				Keys keys = TableValue.GetJoinKeys(this.leftQuery.Columns, rightQuery.Columns);
				JoinColumn[] joinColumns2 = TableValue.GetJoinColumns(keys, this.leftQuery.Columns, rightQuery.Columns);
				query = Query.Join(take, this.leftQuery, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, keys, joinColumns2, joinAlgorithm, keyEqualityComparers);
				int[] array = new JoinQuery.JoinMap(this.leftQuery.Columns, rightQuery.Columns, joinColumns2).MapLeftColumns(this.leftKeyColumns);
				KeysBuilder keysBuilder = default(KeysBuilder);
				keysBuilder.Union(query.Columns);
				keysBuilder.Add(this.newColumnName);
				query = createNestedJoin(query, array, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, keysBuilder.ToKeys(), this.keyEqualityComparers);
				query = query.RenameReorderColumns(new ColumnSelection(query.Columns).Move(query.Columns.Length - 1, this.leftQuery.Columns.Length));
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009CA3 RID: 40099 RVA: 0x00205D7C File Offset: 0x00203F7C
		public override bool TryJoinAsRight(RowCount take, Query leftQuery, int[] leftKeyColumns, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			return this.TryJoinAsRight(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), take, leftQuery, leftKeyColumns, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query);
		}

		// Token: 0x06009CA4 RID: 40100 RVA: 0x00205DAC File Offset: 0x00203FAC
		private bool TryJoinAsRight(NestedJoinQuery.CreateNestedJoin createNestedJoin, RowCount take, Query leftQuery, int[] leftKeyColumns, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			if (this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter && Array.IndexOf<int>(rightKeyColumns, this.leftQuery.Columns.Length) == -1)
			{
				Keys keys = TableValue.GetJoinKeys(leftQuery.Columns, this.leftQuery.Columns);
				JoinColumn[] joinColumns2 = TableValue.GetJoinColumns(keys, leftQuery.Columns, this.leftQuery.Columns);
				query = Query.Join(take, leftQuery, leftKeyColumns, this.leftQuery, rightKeyColumns, joinKind, keys, joinColumns2, joinAlgorithm, keyEqualityComparers);
				int[] array = new JoinQuery.JoinMap(leftQuery.Columns, this.leftQuery.Columns, joinColumns2).MapRightColumns(this.leftKeyColumns);
				KeysBuilder keysBuilder = default(KeysBuilder);
				keysBuilder.Union(query.Columns);
				keysBuilder.Add(this.newColumnName);
				query = createNestedJoin(query, array, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, keysBuilder.ToKeys(), this.keyEqualityComparers);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009CA5 RID: 40101 RVA: 0x00205EB0 File Offset: 0x002040B0
		public override Query Group(Grouping grouping)
		{
			Query query;
			if (this.TryGroup(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), grouping, out query))
			{
				return query;
			}
			return base.Group(grouping);
		}

		// Token: 0x06009CA6 RID: 40102 RVA: 0x00205EE0 File Offset: 0x002040E0
		private bool TryGroup(NestedJoinQuery.CreateNestedJoin createNestedJoin, Grouping grouping, out Query query)
		{
			if (this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter && grouping.Comparer == null && Array.IndexOf<int>(grouping.KeyColumns, this.leftQuery.Columns.Length) == -1)
			{
				int[] array = new int[this.leftKeyColumns.Length];
				for (int i = 0; i < array.Length; i++)
				{
					int num = Array.IndexOf<int>(grouping.KeyColumns, this.leftKeyColumns[i]);
					if (num == -1)
					{
						array = null;
						break;
					}
					array[i] = num;
				}
				bool flag = true;
				int num2 = -1;
				string text = null;
				List<ColumnConstructor> list = new List<ColumnConstructor>();
				KeysBuilder keysBuilder = default(KeysBuilder);
				keysBuilder.Union(grouping.KeyKeys);
				int num3 = 0;
				while (flag && num3 < grouping.Constructors.Length)
				{
					ColumnConstructor columnConstructor = grouping.Constructors[num3];
					QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(QueryTableValue.NewTableType(this), columnConstructor.Function);
					flag = this.ApplyBefore(queryExpression);
					if (flag)
					{
						keysBuilder.Add(columnConstructor.Name);
						list.Add(columnConstructor);
					}
					else if (array != null && NestedJoinQuery.IsFirstInvocation(queryExpression, this.leftQuery.Columns.Length))
					{
						num2 = grouping.KeyKeys.Length + num3;
						text = columnConstructor.Name;
						flag = true;
					}
					num3++;
				}
				if (flag)
				{
					Grouping grouping2 = new Grouping(grouping.Adjacent, keysBuilder.ToKeys(), grouping.KeyKeys, grouping.KeyColumns, list.ToArray(), grouping.CompareRecords, grouping.Comparer, QueryTableValue.NewTableType(this.leftQuery));
					query = this.leftQuery.Group(grouping2);
					if (num2 != -1)
					{
						keysBuilder.Add(text);
						query = createNestedJoin(query, array, this.rightTable, this.rightKey, this.joinKind, text, keysBuilder.ToKeys(), this.keyEqualityComparers);
						if (num2 != query.Columns.Length - 1)
						{
							query = query.RenameReorderColumns(new ColumnSelection(query.Columns).Move(query.Columns.Length - 1, num2));
						}
					}
					return true;
				}
			}
			query = null;
			return false;
		}

		// Token: 0x06009CA7 RID: 40103 RVA: 0x002060F4 File Offset: 0x002042F4
		public override Query Distinct(TableDistinct distinctCriteria)
		{
			Query query;
			if (this.TryDistinct(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), distinctCriteria, out query))
			{
				return query;
			}
			return base.Distinct(distinctCriteria);
		}

		// Token: 0x06009CA8 RID: 40104 RVA: 0x00206124 File Offset: 0x00204324
		private bool TryDistinct(NestedJoinQuery.CreateNestedJoin createNestedJoin, TableDistinct distinctCriteria, out Query query)
		{
			List<Distinct> list = new List<Distinct>();
			bool[] array = new bool[this.leftKeyColumns.Length];
			bool flag = false;
			bool flag2 = false;
			bool flag3 = true;
			int num = 0;
			while (flag3 && num < distinctCriteria.Distincts.Length)
			{
				Distinct distinct = distinctCriteria.Distincts[num];
				int num2;
				if (distinct.Comparer != null || distinct.Selector == null || !QueryExpressionBuilder.ToQueryExpression(this, distinct.Selector).TryGetColumnAccess(out num2))
				{
					goto IL_00A9;
				}
				if (num2 < this.leftQuery.Columns.Length)
				{
					list.Add(distinct);
					int num3 = Array.IndexOf<int>(this.leftKeyColumns, num2);
					if (num3 != -1)
					{
						array[num3] = true;
					}
				}
				else
				{
					if (num2 != this.leftQuery.Columns.Length)
					{
						goto IL_00A9;
					}
					flag = true;
				}
				IL_00AC:
				num++;
				continue;
				IL_00A9:
				flag3 = false;
				goto IL_00AC;
			}
			if (flag3 && (flag || this.joinKind != TableTypeAlgebra.JoinKind.LeftOuter))
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (!array[i])
					{
						FunctionValue functionValue = QueryExpressionAssembler.Assemble(this.Columns, new ColumnAccessQueryExpression(this.leftKeyColumns[i]));
						list.Add(new Distinct(functionValue, null));
						flag2 = !flag;
					}
				}
			}
			if (flag3)
			{
				query = this.leftQuery.Distinct(new TableDistinct(list.ToArray()));
				query = createNestedJoin(query, this.leftKeyColumns, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, this.joinKeys, this.keyEqualityComparers);
				if (flag2)
				{
					query = DistinctQuery.New(distinctCriteria, query, false);
				}
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009CA9 RID: 40105 RVA: 0x002062B0 File Offset: 0x002044B0
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			if (this.TryExpandListColumn(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), columnIndex, singleOrDefault, out query))
			{
				return true;
			}
			if (columnIndex == this.leftQuery.Columns.Length)
			{
				query = new NestedJoinQuery.WithExpandListColumnQuery(columnIndex, singleOrDefault, this);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009CAA RID: 40106 RVA: 0x002062F0 File Offset: 0x002044F0
		private bool TryExpandListColumn(NestedJoinQuery.CreateNestedJoin createNestedJoin, int columnIndex, bool singleOrDefault, out Query query)
		{
			if (columnIndex < this.leftQuery.Columns.Length && this.leftQuery.TryExpandListColumn(columnIndex, singleOrDefault, out query))
			{
				query = createNestedJoin(query, this.leftKeyColumns, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, this.joinKeys, this.keyEqualityComparers);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009CAB RID: 40107 RVA: 0x0020635E File Offset: 0x0020455E
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			if (this.TryExpandRecordColumn(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), columnToExpand, fieldsToProject, newColumns, out query))
			{
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009CAC RID: 40108 RVA: 0x00206380 File Offset: 0x00204580
		private bool TryExpandRecordColumn(NestedJoinQuery.CreateNestedJoin createNestedJoin, int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			Query query2;
			if (columnToExpand < this.leftQuery.Columns.Length && this.leftQuery.TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out query2))
			{
				int[] array = JoinQuery.AdjustColumns(this.leftKeyColumns, columnToExpand, fieldsToProject.Length - 1);
				KeysBuilder keysBuilder = default(KeysBuilder);
				keysBuilder.Union(query2.Columns);
				keysBuilder.Add(this.newColumnName);
				query = createNestedJoin(query2, array, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, keysBuilder.ToKeys(), this.keyEqualityComparers);
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009CAD RID: 40109 RVA: 0x00206424 File Offset: 0x00204624
		public override Query Unordered()
		{
			Query query;
			if (this.TryUnordered(new NestedJoinQuery.CreateNestedJoin(NestedJoinQuery.DefaultCreateNestedJoin), out query))
			{
				return query;
			}
			return this;
		}

		// Token: 0x06009CAE RID: 40110 RVA: 0x0020644C File Offset: 0x0020464C
		private bool TryUnordered(NestedJoinQuery.CreateNestedJoin createNestedJoin, out Query query)
		{
			query = this.leftQuery.Unordered();
			query = createNestedJoin(query, this.leftKeyColumns, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, this.joinKeys, this.keyEqualityComparers);
			return true;
		}

		// Token: 0x1700285D RID: 10333
		// (get) Token: 0x06009CAF RID: 40111 RVA: 0x0020649B File Offset: 0x0020469B
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.leftQuery.QueryDomain;
			}
		}

		// Token: 0x06009CB0 RID: 40112 RVA: 0x002064A8 File Offset: 0x002046A8
		public override IEnumerable<IValueReference> GetRows()
		{
			if (this.keyEqualityComparers != null)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Join_LocalEvaluationWithKeyEqualityComparersNotSupported, null, null);
			}
			NestedJoinParameters nestedJoinParameters = new NestedJoinParameters(this.leftQuery, this.leftKeyColumns, this.rightTable, this.rightKey, this.joinKind, this.newColumnName, this.joinKeys);
			return ((this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter) ? NestedJoinAlgorithm.RightIndex : NestedJoinAlgorithm.GroupJoin).NestedJoin(nestedJoinParameters);
		}

		// Token: 0x1700285E RID: 10334
		// (get) Token: 0x06009CB1 RID: 40113 RVA: 0x00206515 File Offset: 0x00204715
		private Func<QueryExpression, bool> ApplyBefore
		{
			get
			{
				return (QueryExpression node) => node.AllAccess(ArgumentAccess.Safe, (int column) => column < this.leftQuery.Columns.Length);
			}
		}

		// Token: 0x06009CB2 RID: 40114 RVA: 0x00206524 File Offset: 0x00204724
		public static bool TrySelectColumns(int newColumn, Query leftQuery, int[] leftKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, string queryNewColumnName, ColumnSelection columnSelection, out ColumnSelection innerSelection, out ColumnSelectionBuilder outerBuilder, out int[] newLeftKeyColumns, out string newColumnName, out KeysBuilder newJoinKeys)
		{
			outerBuilder = default(ColumnSelectionBuilder);
			newJoinKeys = default(KeysBuilder);
			ColumnSelection.SelectMap map = columnSelection.CreateSelectMap(joinKeys);
			if (AddColumnsQuery.TryGetInnerSelection(leftQuery, (int c) => map.MapColumn(c) != -1 || Array.IndexOf<int>(leftKeyColumns, c) != -1, out innerSelection))
			{
				ColumnSelection.SelectMap selectMap = innerSelection.CreateSelectMap(leftQuery.Columns);
				for (int i = 0; i < columnSelection.Keys.Length; i++)
				{
					int column = columnSelection.GetColumn(i);
					if (column < leftQuery.Columns.Length)
					{
						outerBuilder.Add(columnSelection.Keys[i], selectMap.MapColumn(column));
					}
				}
				if (newColumn != -1)
				{
					newColumnName = columnSelection.Keys[newColumn];
					outerBuilder.Add(columnSelection.Keys[newColumn], innerSelection.Keys.Length);
				}
				else
				{
					newColumnName = JoinQuery.EnsureUniqueKey(queryNewColumnName, innerSelection.Keys);
				}
				newLeftKeyColumns = selectMap.MapColumns(leftKeyColumns);
				newJoinKeys.Union(innerSelection.Keys);
				newJoinKeys.Add(newColumnName);
				return true;
			}
			newLeftKeyColumns = null;
			newColumnName = null;
			return false;
		}

		// Token: 0x06009CB3 RID: 40115 RVA: 0x0020664A File Offset: 0x0020484A
		private static Query DefaultCreateNestedJoin(Query leftQuery, int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumnName, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return leftQuery.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumnName, joinKeys, keyEqualityComparers);
		}

		// Token: 0x06009CB4 RID: 40116 RVA: 0x00206660 File Offset: 0x00204860
		private static bool IsFirstInvocation(QueryExpression expression, int column)
		{
			int num;
			return GroupQuery.TryGetFirstInvocation(expression, out num) && column == num;
		}

		// Token: 0x0400526D RID: 21101
		private Query leftQuery;

		// Token: 0x0400526E RID: 21102
		private int[] leftKeyColumns;

		// Token: 0x0400526F RID: 21103
		private Value rightTable;

		// Token: 0x04005270 RID: 21104
		private Keys rightKey;

		// Token: 0x04005271 RID: 21105
		private TableTypeAlgebra.JoinKind joinKind;

		// Token: 0x04005272 RID: 21106
		private string newColumnName;

		// Token: 0x04005273 RID: 21107
		private Keys joinKeys;

		// Token: 0x04005274 RID: 21108
		private FunctionValue[] keyEqualityComparers;

		// Token: 0x04005275 RID: 21109
		private TypeValue columnType;

		// Token: 0x02001826 RID: 6182
		// (Invoke) Token: 0x06009CB8 RID: 40120
		private delegate Query CreateNestedJoin(Query leftQuery, int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumnName, Keys joinKeys, FunctionValue[] keyEqualityComparers);

		// Token: 0x02001827 RID: 6183
		private sealed class WithExpandListColumnQuery : ExpandListColumnQuery
		{
			// Token: 0x06009CBB RID: 40123 RVA: 0x002066AB File Offset: 0x002048AB
			public WithExpandListColumnQuery(int columnIndex, bool singleOrDefault, NestedJoinQuery nestedJoin)
				: base(columnIndex, singleOrDefault, null, nestedJoin)
			{
				this.nestedJoin = nestedJoin;
			}

			// Token: 0x06009CBC RID: 40124 RVA: 0x002066C0 File Offset: 0x002048C0
			public override Query SelectColumns(ColumnSelection columnSelection)
			{
				Query query;
				if ((base.SingleOrDefault || this.nestedJoin.IsRelationshipSingleOrDefault || this.nestedJoin.SelectJoinColumn(columnSelection) != -1) && this.nestedJoin.TrySelectColumns(new NestedJoinQuery.CreateNestedJoin(this.CreateNestedJoin), columnSelection, out query))
				{
					return query;
				}
				return base.SelectColumns(columnSelection);
			}

			// Token: 0x06009CBD RID: 40125 RVA: 0x00206718 File Offset: 0x00204918
			public override Query SelectRows(FunctionValue condition)
			{
				Query query;
				if (this.nestedJoin.TrySelectRows(new NestedJoinQuery.CreateNestedJoin(this.CreateNestedJoin), condition, out query))
				{
					return query;
				}
				return base.SelectRows(condition);
			}

			// Token: 0x06009CBE RID: 40126 RVA: 0x0020674C File Offset: 0x0020494C
			public override Query AddColumns(ColumnsConstructor columnGenerator)
			{
				Query query;
				if (this.nestedJoin.TryAddColumns(new NestedJoinQuery.CreateNestedJoin(this.CreateNestedJoin), columnGenerator, out query))
				{
					return query;
				}
				return base.AddColumns(columnGenerator);
			}

			// Token: 0x06009CBF RID: 40127 RVA: 0x00206780 File Offset: 0x00204980
			public override Query Take(RowCount count)
			{
				Query query;
				if (base.SingleOrDefault && this.nestedJoin.TryTake(new NestedJoinQuery.CreateNestedJoin(this.CreateNestedJoin), count, out query))
				{
					return query;
				}
				return base.Take(count);
			}

			// Token: 0x06009CC0 RID: 40128 RVA: 0x002067BC File Offset: 0x002049BC
			public override Query Skip(RowCount count)
			{
				Query query;
				if (base.SingleOrDefault && this.nestedJoin.TrySkip(new NestedJoinQuery.CreateNestedJoin(this.CreateNestedJoin), count, out query))
				{
					return query;
				}
				return base.Take(count);
			}

			// Token: 0x06009CC1 RID: 40129 RVA: 0x002067F8 File Offset: 0x002049F8
			public override Query Sort(TableSortOrder sortOrder)
			{
				Query query;
				if (this.nestedJoin.TrySort(new NestedJoinQuery.CreateNestedJoin(this.CreateNestedJoin), sortOrder, out query))
				{
					return query;
				}
				return base.Sort(sortOrder);
			}

			// Token: 0x06009CC2 RID: 40130 RVA: 0x0020682C File Offset: 0x00204A2C
			public override bool TryJoinAsLeft(RowCount take, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
			{
				return this.nestedJoin.TryJoinAsLeft(new NestedJoinQuery.CreateNestedJoin(this.CreateNestedJoin), take, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query);
			}

			// Token: 0x06009CC3 RID: 40131 RVA: 0x00206864 File Offset: 0x00204A64
			public override bool TryJoinAsRight(RowCount take, Query leftQuery, int[] leftKeyColumns, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
			{
				return this.nestedJoin.TryJoinAsRight(new NestedJoinQuery.CreateNestedJoin(this.CreateNestedJoin), take, leftQuery, leftKeyColumns, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out query);
			}

			// Token: 0x06009CC4 RID: 40132 RVA: 0x0020689C File Offset: 0x00204A9C
			public override Query Group(Grouping grouping)
			{
				Query query;
				if (this.nestedJoin.TryGroup(new NestedJoinQuery.CreateNestedJoin(this.CreateNestedJoin), grouping, out query))
				{
					return query;
				}
				return base.Group(grouping);
			}

			// Token: 0x06009CC5 RID: 40133 RVA: 0x002068D0 File Offset: 0x00204AD0
			public override Query Distinct(TableDistinct distinctCriteria)
			{
				Query query;
				if (this.nestedJoin.TryDistinct(new NestedJoinQuery.CreateNestedJoin(this.CreateNestedJoin), distinctCriteria, out query))
				{
					return query;
				}
				return base.Distinct(distinctCriteria);
			}

			// Token: 0x06009CC6 RID: 40134 RVA: 0x00206902 File Offset: 0x00204B02
			public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
			{
				if (this.nestedJoin.TryExpandListColumn(new NestedJoinQuery.CreateNestedJoin(this.CreateNestedJoin), columnIndex, singleOrDefault, out query))
				{
					return true;
				}
				query = null;
				return false;
			}

			// Token: 0x06009CC7 RID: 40135 RVA: 0x00206928 File Offset: 0x00204B28
			public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
			{
				if (columnToExpand == base.ColumnIndex && (!base.SingleOrDefault || this.nestedJoin.IsRelationshipSingleOrDefault))
				{
					Query query2 = this.nestedJoin.RightTable.Query;
					Keys columns = this.nestedJoin.LeftQuery.Columns;
					Keys keys = JoinQuery.EnsureUniqueKeys(query2.Columns, columns);
					Keys joinKeys = TableValue.GetJoinKeys(columns, keys);
					JoinColumn[] joinColumns = TableValue.GetJoinColumns(joinKeys, columns, keys);
					query = Query.Join(RowCount.Infinite, this.nestedJoin.LeftQuery.Unordered(), this.nestedJoin.LeftKeyColumns, query2.Unordered().RenameReorderColumns(new ColumnSelection(keys)), TableValue.GetColumns(query2.Columns, this.nestedJoin.RightKey), this.nestedJoin.JoinKind, joinKeys, joinColumns, JoinAlgorithm.Dynamic, this.nestedJoin.KeyEqualityComparers);
					if (this.nestedJoin.LeftQuery.SortOrder != TableSortOrder.None && !this.nestedJoin.LeftQuery.SortOrder.IsEmpty)
					{
						query = query.Sort(this.nestedJoin.LeftQuery.SortOrder);
					}
					ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
					for (int i = 0; i < columns.Length; i++)
					{
						columnSelectionBuilder.Add(columns[i], i);
					}
					KeysBuilder keysBuilder = default(KeysBuilder);
					for (int j = 0; j < fieldsToProject.Length; j++)
					{
						int num;
						if (query2.Columns.TryGetKeyIndex(fieldsToProject[j], out num))
						{
							columnSelectionBuilder.Add(newColumns[j], this.nestedJoin.LeftQuery.Columns.Length + num);
						}
						else
						{
							columnSelectionBuilder.Add(newColumns[j], query.Columns.Length + keysBuilder.Count);
							keysBuilder.Add(newColumns[j]);
						}
					}
					if (keysBuilder.Count > 0)
					{
						FunctionValue[] array = new FunctionValue[keysBuilder.Count];
						IValueReference[] array2 = new IValueReference[array.Length];
						for (int k = 0; k < array.Length; k++)
						{
							array[k] = ConstantFunctionValue.EachNull;
							array2[k] = TypeValue.Null;
						}
						query = query.AddColumns(new ColumnsConstructor(keysBuilder.ToKeys(), new TableValue.FunctionsColumnsConstructorFunctionValue(array), array2));
					}
					query = query.ProjectColumns(columnSelectionBuilder.ToColumnSelection());
					return true;
				}
				if (this.nestedJoin.TryExpandRecordColumn(new NestedJoinQuery.CreateNestedJoin(this.CreateNestedJoin), columnToExpand, fieldsToProject, newColumns, out query))
				{
					return true;
				}
				query = null;
				return false;
			}

			// Token: 0x06009CC8 RID: 40136 RVA: 0x00206BB4 File Offset: 0x00204DB4
			private Query CreateNestedJoin(Query leftQuery, int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumnName, Keys joinKeys, FunctionValue[] keyEqualityComparers)
			{
				return leftQuery.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumnName, joinKeys, keyEqualityComparers).ExpandListColumn(leftQuery.Columns.Length, base.SingleOrDefault);
			}

			// Token: 0x04005276 RID: 21110
			private readonly NestedJoinQuery nestedJoin;
		}

		// Token: 0x02001828 RID: 6184
		private sealed class NestedJoinTableType : TableTypeValue
		{
			// Token: 0x06009CC9 RID: 40137 RVA: 0x00206BDE File Offset: 0x00204DDE
			public NestedJoinTableType(NestedJoinQuery query)
			{
				this.query = query;
				this.tableType = null;
			}

			// Token: 0x1700285F RID: 10335
			// (get) Token: 0x06009CCA RID: 40138 RVA: 0x00206BF4 File Offset: 0x00204DF4
			public override RecordTypeValue ItemType
			{
				get
				{
					return this.TableType.ItemType;
				}
			}

			// Token: 0x17002860 RID: 10336
			// (get) Token: 0x06009CCB RID: 40139 RVA: 0x00206C01 File Offset: 0x00204E01
			public override IList<TableKey> TableKeys
			{
				get
				{
					return this.TableType.TableKeys;
				}
			}

			// Token: 0x17002861 RID: 10337
			// (get) Token: 0x06009CCC RID: 40140 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002862 RID: 10338
			// (get) Token: 0x06009CCD RID: 40141 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue Nullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17002863 RID: 10339
			// (get) Token: 0x06009CCE RID: 40142 RVA: 0x00206C0E File Offset: 0x00204E0E
			public override TypeValue NonNullable
			{
				get
				{
					return this.TableType.NonNullable;
				}
			}

			// Token: 0x17002864 RID: 10340
			// (get) Token: 0x06009CCF RID: 40143 RVA: 0x00206C1C File Offset: 0x00204E1C
			private TableTypeValue TableType
			{
				get
				{
					if (this.tableType == null)
					{
						RecordTypeValue asRecordType = NavigationTableServices.ConvertToLink(new NestedJoinQuery.NestedJoinRecordType(this.query)).AsRecordType;
						this.tableType = TableTypeValue.New(asRecordType).AsTableType;
					}
					return this.tableType;
				}
			}

			// Token: 0x04005277 RID: 21111
			private readonly NestedJoinQuery query;

			// Token: 0x04005278 RID: 21112
			private TableTypeValue tableType;
		}

		// Token: 0x02001829 RID: 6185
		private sealed class NestedJoinRecordType : RecordTypeValue
		{
			// Token: 0x06009CD0 RID: 40144 RVA: 0x00206C5E File Offset: 0x00204E5E
			public NestedJoinRecordType(NestedJoinQuery query)
			{
				this.query = query;
			}

			// Token: 0x17002865 RID: 10341
			// (get) Token: 0x06009CD1 RID: 40145 RVA: 0x00206C6D File Offset: 0x00204E6D
			public override RecordValue Fields
			{
				get
				{
					return this.RecordType.Fields;
				}
			}

			// Token: 0x17002866 RID: 10342
			// (get) Token: 0x06009CD2 RID: 40146 RVA: 0x00002105 File Offset: 0x00000305
			public override bool Open
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002867 RID: 10343
			// (get) Token: 0x06009CD3 RID: 40147 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17002868 RID: 10344
			// (get) Token: 0x06009CD4 RID: 40148 RVA: 0x00206C7A File Offset: 0x00204E7A
			public override TypeValue Nullable
			{
				get
				{
					return this.NullableRecordType;
				}
			}

			// Token: 0x17002869 RID: 10345
			// (get) Token: 0x06009CD5 RID: 40149 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue NonNullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x1700286A RID: 10346
			// (get) Token: 0x06009CD6 RID: 40150 RVA: 0x00206C82 File Offset: 0x00204E82
			public override object TypeIdentity
			{
				get
				{
					return this.RecordType.TypeIdentity;
				}
			}

			// Token: 0x1700286B RID: 10347
			// (get) Token: 0x06009CD7 RID: 40151 RVA: 0x00206C90 File Offset: 0x00204E90
			private RecordTypeValue RecordType
			{
				get
				{
					if (this.recordType == null)
					{
						TypeValue type = this.query.RightTable.Type;
						this.recordType = type.AsTableType.ItemType;
					}
					return this.recordType;
				}
			}

			// Token: 0x1700286C RID: 10348
			// (get) Token: 0x06009CD8 RID: 40152 RVA: 0x00206CCD File Offset: 0x00204ECD
			private RecordTypeValue NullableRecordType
			{
				get
				{
					if (this.nullableRecordType == null)
					{
						this.nullableRecordType = new NestedJoinQuery.NestedJoinRecordType.NullableNestedJoinRecordType(this);
					}
					return this.nullableRecordType;
				}
			}

			// Token: 0x04005279 RID: 21113
			private readonly NestedJoinQuery query;

			// Token: 0x0400527A RID: 21114
			private RecordTypeValue recordType;

			// Token: 0x0400527B RID: 21115
			private RecordTypeValue nullableRecordType;

			// Token: 0x0200182A RID: 6186
			private sealed class NullableNestedJoinRecordType : RecordTypeValue
			{
				// Token: 0x06009CD9 RID: 40153 RVA: 0x00206CE9 File Offset: 0x00204EE9
				public NullableNestedJoinRecordType(NestedJoinQuery.NestedJoinRecordType type)
				{
					this.type = type;
				}

				// Token: 0x1700286D RID: 10349
				// (get) Token: 0x06009CDA RID: 40154 RVA: 0x00206CF8 File Offset: 0x00204EF8
				public override RecordValue Fields
				{
					get
					{
						return this.type.Fields;
					}
				}

				// Token: 0x1700286E RID: 10350
				// (get) Token: 0x06009CDB RID: 40155 RVA: 0x00206D05 File Offset: 0x00204F05
				public override bool Open
				{
					get
					{
						return this.type.Open;
					}
				}

				// Token: 0x1700286F RID: 10351
				// (get) Token: 0x06009CDC RID: 40156 RVA: 0x00002139 File Offset: 0x00000339
				public override bool IsNullable
				{
					get
					{
						return true;
					}
				}

				// Token: 0x17002870 RID: 10352
				// (get) Token: 0x06009CDD RID: 40157 RVA: 0x00206D12 File Offset: 0x00204F12
				public override TypeValue NonNullable
				{
					get
					{
						return this.type;
					}
				}

				// Token: 0x17002871 RID: 10353
				// (get) Token: 0x06009CDE RID: 40158 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
				public override TypeValue Nullable
				{
					get
					{
						return this;
					}
				}

				// Token: 0x17002872 RID: 10354
				// (get) Token: 0x06009CDF RID: 40159 RVA: 0x00206D1A File Offset: 0x00204F1A
				public override object TypeIdentity
				{
					get
					{
						return this.type.RecordType.Nullable.TypeIdentity;
					}
				}

				// Token: 0x0400527C RID: 21116
				private readonly NestedJoinQuery.NestedJoinRecordType type;
			}
		}
	}
}
