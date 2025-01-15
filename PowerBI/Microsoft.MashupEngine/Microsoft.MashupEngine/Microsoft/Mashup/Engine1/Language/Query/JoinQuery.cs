using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200185F RID: 6239
	internal class JoinQuery : Query
	{
		// Token: 0x06009E1C RID: 40476 RVA: 0x0020A824 File Offset: 0x00208A24
		public JoinQuery(RowCount take, Query leftQuery, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers)
		{
			this.take = take;
			this.leftQuery = leftQuery;
			this.leftKeyColumns = leftKeyColumns;
			this.rightQuery = rightQuery;
			this.rightKeyColumns = rightKeyColumns;
			this.joinKind = joinKind;
			this.joinKeys = joinKeys;
			this.joinColumns = joinColumns;
			this.joinAlgorithm = joinAlgorithm;
			this.keyEqualityComparers = keyEqualityComparers;
		}

		// Token: 0x170028D1 RID: 10449
		// (get) Token: 0x06009E1D RID: 40477 RVA: 0x00142610 File Offset: 0x00140810
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.Join;
			}
		}

		// Token: 0x170028D2 RID: 10450
		// (get) Token: 0x06009E1E RID: 40478 RVA: 0x0020A884 File Offset: 0x00208A84
		public override Keys Columns
		{
			get
			{
				return this.joinKeys;
			}
		}

		// Token: 0x06009E1F RID: 40479 RVA: 0x0020A88C File Offset: 0x00208A8C
		public override TypeValue GetColumnType(int index)
		{
			JoinColumn joinColumn = this.joinColumns[index];
			TypeValue typeValue = (joinColumn.Left ? this.leftQuery.GetColumnType(joinColumn.LeftColumn) : this.rightQuery.GetColumnType(joinColumn.RightColumn));
			if (JoinQuery.NullableColumn(joinColumn.Left, this.joinKind))
			{
				return typeValue.Nullable;
			}
			return typeValue;
		}

		// Token: 0x170028D3 RID: 10451
		// (get) Token: 0x06009E20 RID: 40480 RVA: 0x0020A8F4 File Offset: 0x00208AF4
		public override IList<TableKey> TableKeys
		{
			get
			{
				if (this.tableKeys == null)
				{
					if (this.leftQuery.TableKeys.Count > 0 || this.rightQuery.TableKeys.Count > 0)
					{
						this.tableKeys = JoinQuery.JoinTableKeys(this.leftQuery, this.leftKeyColumns, this.rightQuery, this.rightKeyColumns, this.joinKind, this.joinColumns);
					}
					else
					{
						this.tableKeys = Microsoft.Mashup.Engine1.Runtime.TableKeys.None;
					}
				}
				return this.tableKeys;
			}
		}

		// Token: 0x170028D4 RID: 10452
		// (get) Token: 0x06009E21 RID: 40481 RVA: 0x0020A974 File Offset: 0x00208B74
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				if (this.computedColumns == null)
				{
					this.computedColumns = Microsoft.Mashup.Engine1.Runtime.ComputedColumns.Join(this.leftQuery.ComputedColumns, this.rightQuery.ComputedColumns, QueryTableValue.NewRowType(this.leftQuery), QueryTableValue.NewRowType(this.rightQuery), this.joinKeys, this.joinColumns);
				}
				return this.computedColumns;
			}
		}

		// Token: 0x170028D5 RID: 10453
		// (get) Token: 0x06009E22 RID: 40482 RVA: 0x00049E54 File Offset: 0x00048054
		public override TableSortOrder SortOrder
		{
			get
			{
				return TableSortOrder.None;
			}
		}

		// Token: 0x170028D6 RID: 10454
		// (get) Token: 0x06009E23 RID: 40483 RVA: 0x0020A9D2 File Offset: 0x00208BD2
		public RowCount TakeCount
		{
			get
			{
				return this.take;
			}
		}

		// Token: 0x170028D7 RID: 10455
		// (get) Token: 0x06009E24 RID: 40484 RVA: 0x0020A9DA File Offset: 0x00208BDA
		public TableTypeAlgebra.JoinKind JoinKind
		{
			get
			{
				return this.joinKind;
			}
		}

		// Token: 0x170028D8 RID: 10456
		// (get) Token: 0x06009E25 RID: 40485 RVA: 0x0020A9E2 File Offset: 0x00208BE2
		public Query LeftQuery
		{
			get
			{
				return this.leftQuery;
			}
		}

		// Token: 0x170028D9 RID: 10457
		// (get) Token: 0x06009E26 RID: 40486 RVA: 0x0020A9EA File Offset: 0x00208BEA
		public int[] LeftKeyColumns
		{
			get
			{
				return this.leftKeyColumns;
			}
		}

		// Token: 0x170028DA RID: 10458
		// (get) Token: 0x06009E27 RID: 40487 RVA: 0x0020A9F2 File Offset: 0x00208BF2
		public Query RightQuery
		{
			get
			{
				return this.rightQuery;
			}
		}

		// Token: 0x170028DB RID: 10459
		// (get) Token: 0x06009E28 RID: 40488 RVA: 0x0020A9FA File Offset: 0x00208BFA
		public int[] RightKeyColumns
		{
			get
			{
				return this.rightKeyColumns;
			}
		}

		// Token: 0x170028DC RID: 10460
		// (get) Token: 0x06009E29 RID: 40489 RVA: 0x0020A884 File Offset: 0x00208A84
		public Keys JoinKeys
		{
			get
			{
				return this.joinKeys;
			}
		}

		// Token: 0x170028DD RID: 10461
		// (get) Token: 0x06009E2A RID: 40490 RVA: 0x0020AA02 File Offset: 0x00208C02
		public JoinColumn[] JoinColumns
		{
			get
			{
				return this.joinColumns;
			}
		}

		// Token: 0x170028DE RID: 10462
		// (get) Token: 0x06009E2B RID: 40491 RVA: 0x0020AA0A File Offset: 0x00208C0A
		public JoinAlgorithm JoinAlgorithm
		{
			get
			{
				return this.joinAlgorithm;
			}
		}

		// Token: 0x170028DF RID: 10463
		// (get) Token: 0x06009E2C RID: 40492 RVA: 0x0020AA12 File Offset: 0x00208C12
		public FunctionValue[] KeyEqualityComparers
		{
			get
			{
				return this.keyEqualityComparers;
			}
		}

		// Token: 0x06009E2D RID: 40493 RVA: 0x0020AA1C File Offset: 0x00208C1C
		public override Query Take(RowCount count)
		{
			return Query.Join(RowRange.All.Take(this.take).Take(count).TakeCount, this.leftQuery, this.leftKeyColumns, this.rightQuery, this.rightKeyColumns, this.joinKind, this.joinKeys, this.joinColumns, this.joinAlgorithm, this.keyEqualityComparers);
		}

		// Token: 0x06009E2E RID: 40494 RVA: 0x0020AA88 File Offset: 0x00208C88
		private static bool TryGetNotNullFilter(QueryExpression expression, out int column, out bool nullComparison)
		{
			bool? flag = JoinQuery.CompareAgainstNull(expression, out column, out nullComparison);
			bool flag2 = true;
			return !((flag.GetValueOrDefault() == flag2) & (flag != null)) && column != -1;
		}

		// Token: 0x06009E2F RID: 40495 RVA: 0x0020AAC0 File Offset: 0x00208CC0
		private static bool? CompareAgainstNull(QueryExpression expression, out int column, out bool nullComparison)
		{
			QueryExpressionKind kind = expression.Kind;
			if (kind == QueryExpressionKind.Binary)
			{
				return JoinQuery.CompareAgainstNull((BinaryQueryExpression)expression, out column, out nullComparison);
			}
			if (kind == QueryExpressionKind.Unary)
			{
				return JoinQuery.CompareAgainstNull((UnaryQueryExpression)expression, out column, out nullComparison);
			}
			column = -1;
			nullComparison = false;
			return null;
		}

		// Token: 0x06009E30 RID: 40496 RVA: 0x0020AB08 File Offset: 0x00208D08
		private static bool? CompareAgainstNull(UnaryQueryExpression unary, out int column, out bool nullComparison)
		{
			if (unary.Operator != UnaryOperator2.Not)
			{
				column = -1;
				nullComparison = false;
				return null;
			}
			bool? flag = JoinQuery.CompareAgainstNull(unary.Expression, out column, out nullComparison);
			if (flag == null)
			{
				return null;
			}
			return new bool?(!flag.GetValueOrDefault());
		}

		// Token: 0x06009E31 RID: 40497 RVA: 0x0020AB60 File Offset: 0x00208D60
		private static bool? CompareAgainstNull(BinaryQueryExpression binary, out int column, out bool nullComparison)
		{
			int num;
			Value value;
			if ((binary.Left.TryGetColumnAccess(out num) && binary.Right.TryGetConstant(out value)) || (binary.Right.TryGetColumnAccess(out num) && binary.Left.TryGetConstant(out value)))
			{
				switch (binary.Operator)
				{
				case BinaryOperator2.GreaterThan:
				case BinaryOperator2.LessThan:
				case BinaryOperator2.GreaterThanOrEquals:
				case BinaryOperator2.LessThanOrEquals:
					column = num;
					nullComparison = false;
					return null;
				case BinaryOperator2.Equals:
					column = num;
					nullComparison = value.IsNull;
					return new bool?(value.IsNull);
				case BinaryOperator2.NotEquals:
					column = num;
					nullComparison = value.IsNull;
					return new bool?(!value.IsNull);
				}
			}
			column = -1;
			nullComparison = false;
			return null;
		}

		// Token: 0x06009E32 RID: 40498 RVA: 0x0020AC24 File Offset: 0x00208E24
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			Query query;
			int[] array;
			Query query2;
			int[] array2;
			Keys keys;
			JoinColumn[] array3;
			if (JoinQuery.SelectColumns(this.leftQuery, this.rightQuery, this.rightKeyColumns, this.joinKeys, this.joinColumns, this.leftKeyColumns, columnSelection, out query, out array, out query2, out array2, out keys, out array3))
			{
				return Query.Join(this.take, query, array, query2, array2, this.joinKind, keys, array3, this.joinAlgorithm, this.keyEqualityComparers);
			}
			return new JoinQuery.WithSelectColumnsQuery(columnSelection, this);
		}

		// Token: 0x06009E33 RID: 40499 RVA: 0x0020AC98 File Offset: 0x00208E98
		public override Query SelectRows(FunctionValue condition)
		{
			Query query;
			if (this.TrySelectRows(new JoinQuery.CreateJoin(JoinQuery.DefaultCreateJoin), new JoinQuery.TryAdjustCondition(JoinQuery.DefaultTryAdjustCondition), condition, out query))
			{
				return query;
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06009E34 RID: 40500 RVA: 0x0020ACD1 File Offset: 0x00208ED1
		private bool TrySelectRows(JoinQuery.CreateJoin createJoin, JoinQuery.TryAdjustCondition tryAdjustCondition, FunctionValue condition, out Query query)
		{
			if (this.take.IsInfinite)
			{
				return this.TryPromoteOuterJoin(createJoin, tryAdjustCondition, condition, out query) || this.TryPushFilters(createJoin, tryAdjustCondition, condition, out query);
			}
			query = null;
			return false;
		}

		// Token: 0x06009E35 RID: 40501 RVA: 0x0020AD00 File Offset: 0x00208F00
		private bool TryPromoteOuterJoin(JoinQuery.CreateJoin createJoin, JoinQuery.TryAdjustCondition tryAdjustCondition, FunctionValue condition, out Query query)
		{
			if (this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter || this.joinKind == TableTypeAlgebra.JoinKind.RightOuter || this.joinKind == TableTypeAlgebra.JoinKind.FullOuter)
			{
				HashSet<int> hashSet = new HashSet<int>();
				HashSet<int> hashSet2 = new HashSet<int>();
				List<QueryExpression> list = new List<QueryExpression>();
				List<QueryExpression> conjunctiveNF = SelectRowsQuery.GetConjunctiveNF(QueryExpressionBuilder.ToQueryExpression(this, condition));
				for (int i = 0; i < conjunctiveNF.Count; i++)
				{
					int num;
					bool flag2;
					bool flag = JoinQuery.TryGetNotNullFilter(conjunctiveNF[i], out num, out flag2);
					if (flag)
					{
						JoinColumn joinColumn = this.joinColumns[num];
						if (JoinQuery.IsLeftKeyColumn(this.leftKeyColumns, joinColumn))
						{
							hashSet.Add(joinColumn.LeftColumn);
						}
						if (JoinQuery.IsRightKeyColumn(this.rightKeyColumns, joinColumn))
						{
							hashSet2.Add(joinColumn.RightColumn);
						}
					}
					if (!flag || !flag2)
					{
						list.Add(conjunctiveNF[i]);
					}
				}
				TableTypeAlgebra.JoinKind joinKind = this.joinKind;
				bool flag3 = hashSet.Count == this.leftKeyColumns.Length;
				bool flag4 = hashSet2.Count == this.rightKeyColumns.Length;
				if (joinKind == TableTypeAlgebra.JoinKind.FullOuter && flag3)
				{
					joinKind = TableTypeAlgebra.JoinKind.LeftOuter;
				}
				if (joinKind == TableTypeAlgebra.JoinKind.FullOuter && flag4)
				{
					joinKind = TableTypeAlgebra.JoinKind.RightOuter;
				}
				if (joinKind == TableTypeAlgebra.JoinKind.LeftOuter && flag4)
				{
					joinKind = TableTypeAlgebra.JoinKind.Inner;
				}
				if (joinKind == TableTypeAlgebra.JoinKind.RightOuter && flag3)
				{
					joinKind = TableTypeAlgebra.JoinKind.Inner;
				}
				if (joinKind != this.joinKind)
				{
					query = createJoin(this.take, this.leftQuery, this.leftKeyColumns, this.rightQuery, this.rightKeyColumns, joinKind, this.joinKeys, this.joinColumns, this.joinAlgorithm, this.keyEqualityComparers);
					if (list.Count > 0)
					{
						QueryExpression queryExpression;
						if (tryAdjustCondition(SelectRowsQuery.CreateConjunctiveNF(list), out queryExpression))
						{
							query = query.SelectRows(QueryExpressionAssembler.Assemble(query.Columns, queryExpression));
						}
						else
						{
							query = null;
						}
					}
					return query != null;
				}
			}
			query = null;
			return false;
		}

		// Token: 0x06009E36 RID: 40502 RVA: 0x0020AEC8 File Offset: 0x002090C8
		private bool TryPushFilters(JoinQuery.CreateJoin createJoin, JoinQuery.TryAdjustCondition tryAdjustCondition, FunctionValue condition, out Query query)
		{
			List<QueryExpression> conjunctiveNF = SelectRowsQuery.GetConjunctiveNF(QueryExpressionBuilder.ToQueryExpression(this, condition));
			List<QueryExpression> list = new List<QueryExpression>();
			List<QueryExpression> list2 = new List<QueryExpression>();
			List<QueryExpression> list3 = new List<QueryExpression>();
			for (int i = 0; i < conjunctiveNF.Count; i++)
			{
				bool flag = false;
				QueryExpression queryExpression = conjunctiveNF[i];
				if ((this.joinKind == TableTypeAlgebra.JoinKind.Inner || this.joinKind == TableTypeAlgebra.JoinKind.LeftOuter || this.joinKind == TableTypeAlgebra.JoinKind.LeftSemi) && list3.Count == 0 && queryExpression.AllAccess(ArgumentAccess.Safe, (int column) => this.joinColumns[column].Left))
				{
					list.Add(queryExpression.AdjustColumnAccess((int column) => this.joinColumns[column].LeftColumn));
					flag = true;
				}
				if ((this.joinKind == TableTypeAlgebra.JoinKind.Inner || this.joinKind == TableTypeAlgebra.JoinKind.RightOuter || this.joinKind == TableTypeAlgebra.JoinKind.RightSemi) && list3.Count == 0 && queryExpression.AllAccess(ArgumentAccess.Safe, (int column) => this.joinColumns[column].Right))
				{
					list2.Add(queryExpression.AdjustColumnAccess((int column) => this.joinColumns[column].RightColumn));
					flag = true;
				}
				if (!flag)
				{
					list3.Add(queryExpression);
				}
			}
			if (list.Count > 0 || list2.Count > 0)
			{
				Query query2 = this.leftQuery;
				Query query3 = this.rightQuery;
				if (list.Count > 0)
				{
					query2 = query2.SelectRows(QueryExpressionAssembler.Assemble(query2.Columns, SelectRowsQuery.CreateConjunctiveNF(list)));
				}
				if (list2.Count > 0)
				{
					query3 = query3.SelectRows(QueryExpressionAssembler.Assemble(query3.Columns, SelectRowsQuery.CreateConjunctiveNF(list2)));
				}
				query = createJoin(this.take, query2, this.leftKeyColumns, query3, this.rightKeyColumns, this.joinKind, this.joinKeys, this.joinColumns, this.joinAlgorithm, this.keyEqualityComparers);
				if (list3.Count > 0)
				{
					QueryExpression queryExpression2;
					if (tryAdjustCondition(SelectRowsQuery.CreateConjunctiveNF(list3), out queryExpression2))
					{
						query = query.SelectRows(QueryExpressionAssembler.Assemble(query.Columns, queryExpression2));
					}
					else
					{
						query = null;
					}
				}
				return query != null;
			}
			query = null;
			return false;
		}

		// Token: 0x06009E37 RID: 40503 RVA: 0x0020B0C8 File Offset: 0x002092C8
		public override Query Group(Grouping grouping)
		{
			bool flag;
			ColumnSelection columnSelection;
			int num;
			ColumnSelection columnSelection2;
			if (this.take.IsInfinite && this.TryGetAliasedSide(grouping, out flag, out columnSelection) && this.TryGetCollapsedSide(grouping, flag, out num, out columnSelection2))
			{
				Query query;
				Query query2;
				int[] array;
				int[] array2;
				if (flag)
				{
					query = this.leftQuery.ProjectColumns(columnSelection);
					query2 = this.rightQuery.ProjectColumns(columnSelection2);
					array = columnSelection.GetColumns(this.leftKeyColumns);
					array2 = columnSelection2.GetColumns(this.rightKeyColumns);
				}
				else
				{
					query = this.rightQuery.ProjectColumns(columnSelection);
					query2 = this.leftQuery.ProjectColumns(columnSelection2);
					array = columnSelection.GetColumns(this.rightKeyColumns);
					array2 = columnSelection2.GetColumns(this.leftKeyColumns);
				}
				if (array != null && array2 != null)
				{
					string name = grouping.Constructors[num].Name;
					KeysBuilder keysBuilder = default(KeysBuilder);
					keysBuilder.Union(query.Columns);
					keysBuilder.Add(name);
					Query query3 = query.NestedJoin(array, new QueryTableValue(query2), Query.GetKeys(query2.Columns, array2), this.joinKind, name, keysBuilder.ToKeys(), this.keyEqualityComparers);
					if (num != grouping.Constructors.Length - 1)
					{
						int num2 = grouping.KeyColumns.Length + num;
						query3 = query3.RenameReorderColumns(new ColumnSelection(query3.Columns).Move(query3.Columns.Length - 1, num2));
					}
					return query3;
				}
			}
			return base.Group(grouping);
		}

		// Token: 0x06009E38 RID: 40504 RVA: 0x0020B23C File Offset: 0x0020943C
		private bool TryGetAliasedSide(Grouping grouping, out bool aliasLeft, out ColumnSelection aliasProjection)
		{
			aliasLeft = false;
			aliasProjection = null;
			bool flag = false;
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			for (int i = 0; i < grouping.KeyColumns.Length; i++)
			{
				int num = grouping.KeyColumns[i];
				JoinColumn joinColumn = this.joinColumns[num];
				if (flag && aliasLeft != joinColumn.Left)
				{
					aliasLeft = false;
					return false;
				}
				flag = true;
				aliasLeft = joinColumn.Left;
				columnSelectionBuilder.Add(grouping.KeyKeys[i], aliasLeft ? joinColumn.LeftColumn : joinColumn.RightColumn);
			}
			for (int j = 0; j < grouping.Constructors.Length; j++)
			{
				RecordTypeValue recordTypeValue = QueryTableValue.NewRowType(this);
				ColumnConstructor columnConstructor = grouping.Constructors[j];
				int num2;
				int[] array;
				if (Grouping.TryGetAliasedColumn(recordTypeValue, columnConstructor.Function, out num2))
				{
					JoinColumn joinColumn2 = this.joinColumns[num2];
					if (flag && aliasLeft != joinColumn2.Left)
					{
						aliasLeft = false;
						return false;
					}
					flag = true;
					aliasLeft = joinColumn2.Left;
					columnSelectionBuilder.Add(columnConstructor.Name, aliasLeft ? joinColumn2.LeftColumn : joinColumn2.RightColumn);
				}
				else if (!Grouping.TryGetCollapsedColumns(recordTypeValue, columnConstructor.Function, out array))
				{
					aliasLeft = false;
					return false;
				}
			}
			if (flag)
			{
				aliasProjection = columnSelectionBuilder.ToColumnSelection();
				return true;
			}
			return false;
		}

		// Token: 0x06009E39 RID: 40505 RVA: 0x0020B384 File Offset: 0x00209584
		private bool TryGetCollapsedSide(Grouping grouping, bool collapseRight, out int collapseCtor, out ColumnSelection collapseProjection)
		{
			collapseCtor = -1;
			collapseProjection = null;
			for (int i = 0; i < grouping.Constructors.Length; i++)
			{
				RecordTypeValue recordTypeValue = QueryTableValue.NewRowType(this);
				ColumnConstructor columnConstructor = grouping.Constructors[i];
				int[] array;
				if (Grouping.TryGetCollapsedColumns(recordTypeValue, columnConstructor.Function, out array))
				{
					if (collapseProjection != null)
					{
						collapseCtor = -1;
						collapseProjection = null;
						return false;
					}
					ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
					foreach (int num in array)
					{
						JoinColumn joinColumn = this.joinColumns[num];
						if (collapseRight != joinColumn.Right)
						{
							return false;
						}
						columnSelectionBuilder.Add(this.Columns[num], collapseRight ? joinColumn.RightColumn : joinColumn.LeftColumn);
					}
					collapseCtor = i;
					collapseProjection = columnSelectionBuilder.ToColumnSelection();
				}
			}
			return collapseProjection != null;
		}

		// Token: 0x06009E3A RID: 40506 RVA: 0x0020B454 File Offset: 0x00209654
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			JoinColumn joinColumn = this.joinColumns[columnIndex];
			if (this.take.IsInfinite && !JoinQuery.IsKeyColumn(this.leftKeyColumns, this.rightKeyColumns, joinColumn))
			{
				Query query2 = this.leftQuery;
				Query query3 = this.rightQuery;
				if (joinColumn.Left)
				{
					query2.TryExpandListColumn(joinColumn.LeftColumn, singleOrDefault, out query2);
				}
				if (joinColumn.Right)
				{
					query3.TryExpandListColumn(joinColumn.RightColumn, singleOrDefault, out query3);
				}
				if (query2 != null && query3 != null)
				{
					query = Query.Join(this.take, query2, this.leftKeyColumns, query3, this.rightKeyColumns, this.joinKind, this.joinKeys, this.joinColumns, this.joinAlgorithm, this.keyEqualityComparers);
					return true;
				}
			}
			query = null;
			return false;
		}

		// Token: 0x06009E3B RID: 40507 RVA: 0x0020B51C File Offset: 0x0020971C
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			JoinColumn joinColumn = this.joinColumns[columnToExpand];
			if (!JoinQuery.IsKeyColumn(this.leftKeyColumns, this.rightKeyColumns, joinColumn))
			{
				int[] array = this.leftKeyColumns;
				int[] array2 = this.rightKeyColumns;
				Query query2 = this.leftQuery;
				Query query3 = this.rightQuery;
				if (joinColumn.Left)
				{
					array = JoinQuery.AdjustColumns(array, joinColumn.LeftColumn, fieldsToProject.Length - 1);
					query2.TryExpandRecordColumn(joinColumn.LeftColumn, fieldsToProject, newColumns, out query2);
				}
				else if (joinColumn.Right)
				{
					array2 = JoinQuery.AdjustColumns(array2, joinColumn.RightColumn, fieldsToProject.Length - 1);
					query3.TryExpandRecordColumn(joinColumn.RightColumn, fieldsToProject, newColumns, out query3);
				}
				if (query2 != null && query3 != null)
				{
					Keys keys = TableValue.GetJoinKeys(query2.Columns, query3.Columns);
					JoinColumn[] array3 = TableValue.GetJoinColumns(keys, query2.Columns, query3.Columns);
					query = Query.Join(this.take, query2, array, query3, array2, this.joinKind, keys, array3, this.joinAlgorithm, this.keyEqualityComparers);
					return true;
				}
			}
			query = null;
			return false;
		}

		// Token: 0x06009E3C RID: 40508 RVA: 0x0020B630 File Offset: 0x00209830
		public override Query Unordered()
		{
			return Query.Join(this.take, this.leftQuery.Unordered(), this.leftKeyColumns, this.rightQuery.Unordered(), this.rightKeyColumns, this.joinKind, this.joinKeys, this.joinColumns, this.joinAlgorithm, this.keyEqualityComparers);
		}

		// Token: 0x170028E0 RID: 10464
		// (get) Token: 0x06009E3D RID: 40509 RVA: 0x0020B688 File Offset: 0x00209888
		public override IQueryDomain QueryDomain
		{
			get
			{
				IQueryDomain queryDomain;
				if (this.leftQuery.QueryDomain.TryGetCompatibleDomain(this.rightQuery.QueryDomain, out queryDomain))
				{
					return queryDomain;
				}
				return new JoinQuery.JoinQueryDomain(this);
			}
		}

		// Token: 0x06009E3E RID: 40510 RVA: 0x0020B6BC File Offset: 0x002098BC
		public override IEnumerable<IValueReference> GetRows()
		{
			if (this.keyEqualityComparers != null)
			{
				throw ValueException.NewExpressionError<Message0>(Strings.Join_LocalEvaluationWithKeyEqualityComparersNotSupported, null, null);
			}
			IEnumerable<IValueReference> enumerable = this.joinAlgorithm.Join(new JoinAlgorithm.JoinParameters(this.take, this.leftQuery, this.leftKeyColumns, this.rightQuery, this.rightKeyColumns, this.joinKind, this.joinKeys, this.joinColumns));
			if (!this.take.IsInfinite)
			{
				enumerable = new SkipTakeEnumerable(enumerable, RowRange.All.Take(this.take));
			}
			return enumerable;
		}

		// Token: 0x06009E3F RID: 40511 RVA: 0x0020B747 File Offset: 0x00209947
		public static bool IsLeftKeyColumn(int[] leftKeyColumns, JoinColumn column)
		{
			return column.Left && Array.IndexOf<int>(leftKeyColumns, column.LeftColumn) != -1;
		}

		// Token: 0x06009E40 RID: 40512 RVA: 0x0020B767 File Offset: 0x00209967
		public static bool IsRightKeyColumn(int[] rightKeyColumns, JoinColumn column)
		{
			return column.Right && Array.IndexOf<int>(rightKeyColumns, column.RightColumn) != -1;
		}

		// Token: 0x06009E41 RID: 40513 RVA: 0x0020B787 File Offset: 0x00209987
		public static bool IsKeyColumn(int[] leftKeyColumns, int[] rightKeyColumns, JoinColumn column)
		{
			return JoinQuery.IsLeftKeyColumn(leftKeyColumns, column) || JoinQuery.IsRightKeyColumn(rightKeyColumns, column);
		}

		// Token: 0x06009E42 RID: 40514 RVA: 0x0020B79C File Offset: 0x0020999C
		public static IList<TableKey> JoinTableKeys(Query query1, int[] joinKey1, Query query2, int[] joinKey2, TableTypeAlgebra.JoinKind joinKind, JoinColumn[] joinColumns)
		{
			JoinQuery.JoinMap joinMap = new JoinQuery.JoinMap(query1.Columns, query2.Columns, joinColumns);
			IList<TableKey> list = query1.TableKeys;
			IList<TableKey> list2 = query2.TableKeys;
			List<TableKey> list3 = new List<TableKey>();
			int num = -1;
			int num2 = -1;
			if (JoinQuery.ContainsKey(list, joinKey1))
			{
				foreach (TableKey tableKey in list2)
				{
					TableKey tableKey2 = new TableKey(joinMap.MapRightColumns(tableKey.Columns), tableKey.Primary && joinKind == TableTypeAlgebra.JoinKind.Inner);
					if (tableKey2.Primary)
					{
						num = list3.Count;
					}
					list3.Add(tableKey2);
				}
			}
			if (JoinQuery.ContainsKey(list2, joinKey2))
			{
				foreach (TableKey tableKey3 in list)
				{
					TableKey tableKey4 = new TableKey(joinMap.MapLeftColumns(tableKey3.Columns), tableKey3.Primary && joinKind == TableTypeAlgebra.JoinKind.Inner);
					if (tableKey4.Primary)
					{
						num2 = list3.Count;
					}
					list3.Add(tableKey4);
				}
			}
			if (list3.Count > 0)
			{
				if (num != -1 && num2 != -1)
				{
					list3[num2] = new TableKey(list3[num2].Columns, false);
				}
				return list3;
			}
			TableKey[] array = new TableKey[list.Count * list2.Count];
			int num3 = 0;
			for (int i = 0; i < list.Count; i++)
			{
				TableKey tableKey5 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					TableKey tableKey6 = list2[j];
					int[] array2 = joinMap.MapLeftColumns(tableKey5.Columns);
					int[] array3 = joinMap.MapRightColumns(tableKey6.Columns);
					int[] array4 = new int[array2.Length + array3.Length];
					Array.Copy(array2, array4, array2.Length);
					Array.Copy(array3, 0, array4, array2.Length, array3.Length);
					array[num3++] = new TableKey(array4, tableKey5.Primary && tableKey6.Primary && joinKind == TableTypeAlgebra.JoinKind.Inner);
				}
			}
			return array;
		}

		// Token: 0x06009E43 RID: 40515 RVA: 0x0020B9E0 File Offset: 0x00209BE0
		public static bool SelectColumns(Query leftQuery, Query rightQuery, int[] rightKeyColumns, Keys joinKeys, JoinColumn[] joinColumns, int[] leftKeyColumns, ColumnSelection columnSelection, out Query newLeftQuery, out int[] newLeftKeyColumns, out Query newRightQuery, out int[] newRightKeyColumns, out Keys newJoinKeys, out JoinColumn[] newJoinColumns)
		{
			JoinQuery.JoinMap joinMap = new JoinQuery.JoinMap(leftQuery.Columns, rightQuery.Columns, joinColumns);
			ColumnSelection.SelectMap selectMap = columnSelection.CreateSelectMap(joinKeys);
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			ColumnSelectionBuilder columnSelectionBuilder2 = default(ColumnSelectionBuilder);
			for (int i = 0; i < leftQuery.Columns.Length; i++)
			{
				int num = joinMap.MapLeftColumn(i);
				if (num != -1 && selectMap.MapColumn(num) != -1)
				{
					columnSelectionBuilder.Add(leftQuery.Columns[i], i);
				}
			}
			for (int j = 0; j < rightQuery.Columns.Length; j++)
			{
				int num2 = joinMap.MapRightColumn(j);
				if (num2 != -1 && selectMap.MapColumn(num2) != -1)
				{
					columnSelectionBuilder2.Add(rightQuery.Columns[j], j);
				}
			}
			ColumnSelection columnSelection2 = columnSelectionBuilder.ToColumnSelection();
			ColumnSelection columnSelection3 = columnSelectionBuilder2.ToColumnSelection();
			ColumnSelection.SelectMap selectMap2 = columnSelection2.CreateSelectMap(leftQuery.Columns);
			ColumnSelection.SelectMap selectMap3 = columnSelection3.CreateSelectMap(rightQuery.Columns);
			newLeftKeyColumns = selectMap2.MapColumns(leftKeyColumns);
			newRightKeyColumns = selectMap3.MapColumns(rightKeyColumns);
			newLeftQuery = null;
			newRightQuery = null;
			newJoinKeys = null;
			newJoinColumns = null;
			if (newLeftKeyColumns != null && newRightKeyColumns != null)
			{
				newLeftQuery = leftQuery.SelectColumns(columnSelection2);
				newRightQuery = rightQuery.SelectColumns(columnSelection3);
				newJoinKeys = TableValue.GetJoinKeys(newLeftQuery.Columns, newRightQuery.Columns);
				newJoinColumns = TableValue.GetJoinColumns(newJoinKeys, newLeftQuery.Columns, newRightQuery.Columns);
				return true;
			}
			return false;
		}

		// Token: 0x06009E44 RID: 40516 RVA: 0x0020BB5C File Offset: 0x00209D5C
		private static bool ContainsKey(IList<TableKey> keys, int[] joinKey)
		{
			using (IEnumerator<TableKey> enumerator = keys.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (JoinQuery.KeyEquals(enumerator.Current.Columns, joinKey))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06009E45 RID: 40517 RVA: 0x0020BBB0 File Offset: 0x00209DB0
		private static bool KeyEquals(int[] left, int[] right)
		{
			if (left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06009E46 RID: 40518 RVA: 0x0020BBE0 File Offset: 0x00209DE0
		public static int[] AdjustColumns(int[] columns, int index, int offset)
		{
			int[] array = new int[columns.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ((columns[i] >= index) ? (columns[i] + offset) : columns[i]);
			}
			return array;
		}

		// Token: 0x06009E47 RID: 40519 RVA: 0x0020BC18 File Offset: 0x00209E18
		public static string EnsureUniqueKey(string originalKey, Keys keysToAvoid)
		{
			KeysBuilder keysBuilder = default(KeysBuilder);
			return JoinQuery.EnsureUniqueKey(originalKey, keysToAvoid, ref keysBuilder);
		}

		// Token: 0x06009E48 RID: 40520 RVA: 0x0020BC38 File Offset: 0x00209E38
		public static string EnsureUniqueKey(string originalKey, Keys keysToAvoid, ref KeysBuilder resultKeys)
		{
			string text = originalKey;
			int num = 2;
			int num2;
			while (keysToAvoid.TryGetKeyIndex(text, out num2) || resultKeys.IndexOf(text) != -1)
			{
				text = originalKey + num.ToString(CultureInfo.InvariantCulture);
				num++;
			}
			return text;
		}

		// Token: 0x06009E49 RID: 40521 RVA: 0x0020BC78 File Offset: 0x00209E78
		public static Keys EnsureUniqueKeys(Keys originalKeys, Keys keysToAvoid)
		{
			KeysBuilder keysBuilder = default(KeysBuilder);
			for (int i = 0; i < originalKeys.Length; i++)
			{
				string text = JoinQuery.EnsureUniqueKey(originalKeys[i], keysToAvoid, ref keysBuilder);
				keysBuilder.Add(text);
			}
			return keysBuilder.ToKeys();
		}

		// Token: 0x06009E4A RID: 40522 RVA: 0x0020BCC0 File Offset: 0x00209EC0
		public static bool NullableColumn(bool left, TableTypeAlgebra.JoinKind joinKind)
		{
			switch (joinKind)
			{
			case TableTypeAlgebra.JoinKind.Inner:
				return false;
			case TableTypeAlgebra.JoinKind.LeftOuter:
				return !left;
			case TableTypeAlgebra.JoinKind.FullOuter:
				return true;
			case TableTypeAlgebra.JoinKind.RightOuter:
				return left;
			case TableTypeAlgebra.JoinKind.LeftAnti:
				return !left;
			case TableTypeAlgebra.JoinKind.RightAnti:
				return left;
			case TableTypeAlgebra.JoinKind.LeftSemi:
				return !left;
			case TableTypeAlgebra.JoinKind.RightSemi:
				return left;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06009E4B RID: 40523 RVA: 0x0020BD14 File Offset: 0x00209F14
		private static Query DefaultCreateJoin(RowCount take, Query leftQuery, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers)
		{
			return Query.Join(take, leftQuery, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers);
		}

		// Token: 0x06009E4C RID: 40524 RVA: 0x00049610 File Offset: 0x00047810
		private static bool DefaultTryAdjustCondition(QueryExpression expression, out QueryExpression newExpression)
		{
			newExpression = expression;
			return true;
		}

		// Token: 0x04005323 RID: 21283
		private RowCount take;

		// Token: 0x04005324 RID: 21284
		private Query leftQuery;

		// Token: 0x04005325 RID: 21285
		private int[] leftKeyColumns;

		// Token: 0x04005326 RID: 21286
		private Query rightQuery;

		// Token: 0x04005327 RID: 21287
		private int[] rightKeyColumns;

		// Token: 0x04005328 RID: 21288
		private TableTypeAlgebra.JoinKind joinKind;

		// Token: 0x04005329 RID: 21289
		private Keys joinKeys;

		// Token: 0x0400532A RID: 21290
		private JoinColumn[] joinColumns;

		// Token: 0x0400532B RID: 21291
		private IList<TableKey> tableKeys;

		// Token: 0x0400532C RID: 21292
		private IList<ComputedColumn> computedColumns;

		// Token: 0x0400532D RID: 21293
		private JoinAlgorithm joinAlgorithm;

		// Token: 0x0400532E RID: 21294
		private FunctionValue[] keyEqualityComparers;

		// Token: 0x02001860 RID: 6240
		// (Invoke) Token: 0x06009E52 RID: 40530
		private delegate Query CreateJoin(RowCount take, Query leftQuery, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers);

		// Token: 0x02001861 RID: 6241
		// (Invoke) Token: 0x06009E56 RID: 40534
		private delegate bool TryAdjustCondition(QueryExpression expression, out QueryExpression newExpression);

		// Token: 0x02001862 RID: 6242
		private sealed class WithSelectColumnsQuery : SelectColumnsQuery
		{
			// Token: 0x06009E59 RID: 40537 RVA: 0x0020BD82 File Offset: 0x00209F82
			public WithSelectColumnsQuery(ColumnSelection columnSelection, JoinQuery join)
				: base(columnSelection, join)
			{
				this.join = join;
			}

			// Token: 0x06009E5A RID: 40538 RVA: 0x0020BD94 File Offset: 0x00209F94
			public override Query SelectRows(FunctionValue condition)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this, condition);
				if (queryExpression != null && base.ApplyBefore(queryExpression))
				{
					FunctionValue functionValue = QueryExpressionAssembler.Assemble(base.InnerQuery.Columns, base.AdjustBefore(queryExpression));
					Query query;
					if (this.join.TrySelectRows(new JoinQuery.CreateJoin(this.CreateJoin), new JoinQuery.TryAdjustCondition(this.TryAdjustCondition), functionValue, out query))
					{
						return query;
					}
				}
				return base.SelectRows(condition);
			}

			// Token: 0x06009E5B RID: 40539 RVA: 0x0020BE08 File Offset: 0x0020A008
			private Query CreateJoin(RowCount take, Query leftQuery, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers)
			{
				return Query.Join(take, leftQuery, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers).SelectColumns(base.ColumnSelection);
			}

			// Token: 0x06009E5C RID: 40540 RVA: 0x0020BE36 File Offset: 0x0020A036
			private bool TryAdjustCondition(QueryExpression expression, out QueryExpression newExpression)
			{
				if (expression != null && base.ApplyAfter(expression))
				{
					newExpression = base.AdjustAfter(expression);
					return true;
				}
				newExpression = null;
				return false;
			}

			// Token: 0x0400532F RID: 21295
			private readonly JoinQuery join;
		}

		// Token: 0x02001863 RID: 6243
		public class JoinMap
		{
			// Token: 0x06009E5D RID: 40541 RVA: 0x0020BE60 File Offset: 0x0020A060
			public JoinMap(Keys leftColumns, Keys rightColumns, JoinColumn[] joinColumns)
			{
				this.leftMap = new int[leftColumns.Length];
				this.rightMap = new int[rightColumns.Length];
				for (int i = 0; i < joinColumns.Length; i++)
				{
					JoinColumn joinColumn = joinColumns[i];
					if (joinColumn.Left)
					{
						this.leftMap[joinColumn.LeftColumn] = i + 1;
					}
					if (joinColumn.Right)
					{
						this.rightMap[joinColumn.RightColumn] = i + 1;
					}
				}
			}

			// Token: 0x06009E5E RID: 40542 RVA: 0x0020BEDF File Offset: 0x0020A0DF
			public int MapColumn(bool left, int column)
			{
				if (!left)
				{
					return this.MapRightColumn(column);
				}
				return this.MapLeftColumn(column);
			}

			// Token: 0x06009E5F RID: 40543 RVA: 0x0020BEF3 File Offset: 0x0020A0F3
			public int MapLeftColumn(int column)
			{
				return this.leftMap[column] - 1;
			}

			// Token: 0x06009E60 RID: 40544 RVA: 0x0020BEFF File Offset: 0x0020A0FF
			public int MapRightColumn(int column)
			{
				return this.rightMap[column] - 1;
			}

			// Token: 0x06009E61 RID: 40545 RVA: 0x0020BF0B File Offset: 0x0020A10B
			public int[] MapColumns(bool left, int[] columns)
			{
				if (!left)
				{
					return this.MapRightColumns(columns);
				}
				return this.MapLeftColumns(columns);
			}

			// Token: 0x06009E62 RID: 40546 RVA: 0x0020BF1F File Offset: 0x0020A11F
			public int[] MapLeftColumns(int[] columns)
			{
				return JoinQuery.JoinMap.MapColumns(this.leftMap, columns);
			}

			// Token: 0x06009E63 RID: 40547 RVA: 0x0020BF2D File Offset: 0x0020A12D
			public int[] MapRightColumns(int[] columns)
			{
				return JoinQuery.JoinMap.MapColumns(this.rightMap, columns);
			}

			// Token: 0x06009E64 RID: 40548 RVA: 0x0020BF3C File Offset: 0x0020A13C
			private static int[] MapColumns(int[] map, int[] columns)
			{
				List<int> list = new List<int>(columns.Length);
				for (int i = 0; i < columns.Length; i++)
				{
					int num = map[columns[i]];
					if (num != 0)
					{
						list.Add(num - 1);
					}
				}
				return list.ToArray();
			}

			// Token: 0x04005330 RID: 21296
			private int[] leftMap;

			// Token: 0x04005331 RID: 21297
			private int[] rightMap;
		}

		// Token: 0x02001864 RID: 6244
		private class JoinQueryDomain : IQueryDomain
		{
			// Token: 0x06009E65 RID: 40549 RVA: 0x0020BF78 File Offset: 0x0020A178
			public JoinQueryDomain(JoinQuery joinQuery)
			{
				this.joinQuery = joinQuery;
			}

			// Token: 0x06009E66 RID: 40550 RVA: 0x00002E92 File Offset: 0x00001092
			public bool IsCompatibleWith(IQueryDomain domain)
			{
				return this == domain;
			}

			// Token: 0x170028E1 RID: 10465
			// (get) Token: 0x06009E67 RID: 40551 RVA: 0x00002105 File Offset: 0x00000305
			public bool CanIndex
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06009E68 RID: 40552 RVA: 0x0020BF88 File Offset: 0x0020A188
			public Query Optimize(Query query)
			{
				JoinQuery joinQuery = new JoinQuery(this.joinQuery.TakeCount, this.joinQuery.LeftQuery.QueryDomain.Optimize(this.joinQuery.LeftQuery), this.joinQuery.LeftKeyColumns, this.joinQuery.RightQuery.QueryDomain.Optimize(this.joinQuery.RightQuery), this.joinQuery.RightKeyColumns, this.joinQuery.JoinKind, this.joinQuery.JoinKeys, this.joinQuery.JoinColumns, this.joinQuery.JoinAlgorithm, this.joinQuery.KeyEqualityComparers);
				return new ReplaceQueryVisitor(this.joinQuery, joinQuery).VisitQuery(query);
			}

			// Token: 0x04005332 RID: 21298
			private JoinQuery joinQuery;
		}
	}
}
