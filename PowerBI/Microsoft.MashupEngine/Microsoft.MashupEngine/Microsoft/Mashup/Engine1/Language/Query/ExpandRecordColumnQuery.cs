using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001876 RID: 6262
	internal class ExpandRecordColumnQuery : Query
	{
		// Token: 0x06009EDE RID: 40670 RVA: 0x0020D668 File Offset: 0x0020B868
		public ExpandRecordColumnQuery(int columnToExpand, Keys fieldsToProject, Keys newColumns, TypeValue[] projectedTypes, Query innerQuery)
		{
			this.columnToExpand = columnToExpand;
			this.fieldsToProject = fieldsToProject;
			this.newColumns = newColumns;
			this.innerQuery = innerQuery;
			this.projectedTypes = projectedTypes;
			this.columns = ExpandRecordColumnQuery.CreateColumns(innerQuery.Columns, columnToExpand, newColumns);
		}

		// Token: 0x170028FE RID: 10494
		// (get) Token: 0x06009EDF RID: 40671 RVA: 0x001422C0 File Offset: 0x001404C0
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.ExpandRecordColumn;
			}
		}

		// Token: 0x170028FF RID: 10495
		// (get) Token: 0x06009EE0 RID: 40672 RVA: 0x0020D6B4 File Offset: 0x0020B8B4
		public Query InnerQuery
		{
			get
			{
				return this.innerQuery;
			}
		}

		// Token: 0x17002900 RID: 10496
		// (get) Token: 0x06009EE1 RID: 40673 RVA: 0x0020D6BC File Offset: 0x0020B8BC
		public int ColumnToExpand
		{
			get
			{
				return this.columnToExpand;
			}
		}

		// Token: 0x17002901 RID: 10497
		// (get) Token: 0x06009EE2 RID: 40674 RVA: 0x0020D6C4 File Offset: 0x0020B8C4
		public Keys FieldsToProject
		{
			get
			{
				return this.fieldsToProject;
			}
		}

		// Token: 0x17002902 RID: 10498
		// (get) Token: 0x06009EE3 RID: 40675 RVA: 0x0020D6CC File Offset: 0x0020B8CC
		public Keys NewColumns
		{
			get
			{
				return this.newColumns;
			}
		}

		// Token: 0x17002903 RID: 10499
		// (get) Token: 0x06009EE4 RID: 40676 RVA: 0x0020D6D4 File Offset: 0x0020B8D4
		public override Keys Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17002904 RID: 10500
		// (get) Token: 0x06009EE5 RID: 40677 RVA: 0x0020D6DC File Offset: 0x0020B8DC
		public TypeValue[] ColumnTypes
		{
			get
			{
				return this.projectedTypes;
			}
		}

		// Token: 0x06009EE6 RID: 40678 RVA: 0x0020D6E4 File Offset: 0x0020B8E4
		public override TypeValue GetColumnType(int column)
		{
			if (column < this.columnToExpand)
			{
				return this.innerQuery.GetColumnType(column);
			}
			if (column < this.columnToExpand + this.fieldsToProject.Length)
			{
				return this.GetProjectedColumnType(column);
			}
			return this.innerQuery.GetColumnType(column - this.fieldsToProject.Length + 1);
		}

		// Token: 0x06009EE7 RID: 40679 RVA: 0x0020D740 File Offset: 0x0020B940
		private TypeValue GetProjectedColumnType(int column)
		{
			if (this.projectedTypes == null)
			{
				this.projectedTypes = new TypeValue[this.fieldsToProject.Length];
			}
			int num = column - this.columnToExpand;
			if (this.projectedTypes[num] == null)
			{
				TypeValue columnType = this.innerQuery.GetColumnType(this.columnToExpand);
				TypeValue typeValue = TypeValue.Any;
				if (columnType.TypeKind == ValueKind.Record)
				{
					RecordTypeValue asRecordType = columnType.AsRecordType;
					TypeValue typeValue2;
					bool flag;
					if (asRecordType.TryGetFieldType(this.fieldsToProject[num], out typeValue2, out flag))
					{
						typeValue = typeValue2;
						if (flag || asRecordType.IsNullable)
						{
							typeValue = typeValue.Nullable.NewMeta(typeValue.MetaValue).AsType;
						}
					}
				}
				this.projectedTypes[num] = typeValue;
			}
			return this.projectedTypes[num];
		}

		// Token: 0x17002905 RID: 10501
		// (get) Token: 0x06009EE8 RID: 40680 RVA: 0x0020D7F6 File Offset: 0x0020B9F6
		public override IList<TableKey> TableKeys
		{
			get
			{
				if (this.tableKeys == null)
				{
					this.tableKeys = Microsoft.Mashup.Engine1.Runtime.TableKeys.ExpandRecordColumn(this.innerQuery.TableKeys, this.columnToExpand, this.fieldsToProject.Length);
				}
				return this.tableKeys;
			}
		}

		// Token: 0x17002906 RID: 10502
		// (get) Token: 0x06009EE9 RID: 40681 RVA: 0x0020D830 File Offset: 0x0020BA30
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				if (this.computedColumns == null)
				{
					this.computedColumns = Microsoft.Mashup.Engine1.Runtime.ComputedColumns.ExpandRecordColumn(this.innerQuery.ComputedColumns, QueryTableValue.NewRowType(this.innerQuery), this.columnToExpand, this.fieldsToProject.Length, this.Columns);
				}
				return this.computedColumns;
			}
		}

		// Token: 0x17002907 RID: 10503
		// (get) Token: 0x06009EEA RID: 40682 RVA: 0x0020D883 File Offset: 0x0020BA83
		public override RowCount RowCount
		{
			get
			{
				return this.innerQuery.RowCount;
			}
		}

		// Token: 0x17002908 RID: 10504
		// (get) Token: 0x06009EEB RID: 40683 RVA: 0x0020D890 File Offset: 0x0020BA90
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

		// Token: 0x06009EEC RID: 40684 RVA: 0x0020D8F8 File Offset: 0x0020BAF8
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			if (columnIndex < this.columnToExpand && this.innerQuery.TryExpandListColumn(columnIndex, singleOrDefault, out query) && query.TryExpandRecordColumn(this.columnToExpand, this.fieldsToProject, this.newColumns, out query))
			{
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009EED RID: 40685 RVA: 0x0020D938 File Offset: 0x0020BB38
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			if (columnToExpand < this.columnToExpand && this.innerQuery.TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out query) && query.TryExpandRecordColumn(this.columnToExpand + fieldsToProject.Length - 1, this.fieldsToProject, this.newColumns, out query))
			{
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009EEE RID: 40686 RVA: 0x0020D990 File Offset: 0x0020BB90
		public override Query SelectRows(FunctionValue condition)
		{
			TypeValue columnType = this.innerQuery.GetColumnType(this.columnToExpand);
			RecordTypeValue recordTypeValue = (columnType.IsRecordType ? columnType.AsRecordType : TypeValue.Record);
			List<QueryExpression> conjunctiveNF = SelectRowsQuery.GetConjunctiveNF(QueryExpressionBuilder.ToQueryExpression(this, condition));
			List<QueryExpression> list = null;
			List<QueryExpression> list2 = null;
			for (int i = 0; i < conjunctiveNF.Count; i++)
			{
				QueryExpression queryExpression;
				if (list2 == null && this.TryGetInnerQueryExpression(recordTypeValue, conjunctiveNF[i], out queryExpression))
				{
					list = list ?? new List<QueryExpression>();
					list.Add(queryExpression);
				}
				else
				{
					list2 = list2 ?? new List<QueryExpression>();
					list2.Add(conjunctiveNF[i]);
				}
			}
			if (list != null)
			{
				FunctionValue functionValue = QueryExpressionAssembler.Assemble(this.innerQuery.Columns, SelectRowsQuery.CreateConjunctiveNF(list));
				Query query = this.innerQuery.SelectRows(functionValue);
				query = new ExpandRecordColumnQuery(this.columnToExpand, this.fieldsToProject, this.newColumns, this.projectedTypes, query);
				if (list2 != null)
				{
					FunctionValue functionValue2 = QueryExpressionAssembler.Assemble(query.Columns, SelectRowsQuery.CreateConjunctiveNF(list2));
					query = query.SelectRows(functionValue2);
				}
				return query;
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06009EEF RID: 40687 RVA: 0x0020DAB0 File Offset: 0x0020BCB0
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			ColumnSelection columnSelection2;
			ColumnSelection columnSelection3;
			int innerAndOuterSelection = ExpandRecordColumnQuery.GetInnerAndOuterSelection(this.innerQuery.Columns, this.Columns, columnSelection, this.columnToExpand, this.fieldsToProject.Length, out columnSelection2, out columnSelection3);
			Query query = this.innerQuery.SelectColumns(columnSelection2);
			query = new ExpandRecordColumnQuery(innerAndOuterSelection, this.fieldsToProject, this.newColumns, this.projectedTypes, query);
			return FloatingSelectColumnsQuery.New(columnSelection3, query);
		}

		// Token: 0x06009EF0 RID: 40688 RVA: 0x0020DB16 File Offset: 0x0020BD16
		public override Query Skip(RowCount count)
		{
			return new ExpandRecordColumnQuery(this.columnToExpand, this.fieldsToProject, this.newColumns, this.projectedTypes, this.innerQuery.Skip(count));
		}

		// Token: 0x06009EF1 RID: 40689 RVA: 0x0020DB41 File Offset: 0x0020BD41
		public override Query Take(RowCount count)
		{
			return new ExpandRecordColumnQuery(this.columnToExpand, this.fieldsToProject, this.newColumns, this.projectedTypes, this.innerQuery.Take(count));
		}

		// Token: 0x06009EF2 RID: 40690 RVA: 0x0020DB6C File Offset: 0x0020BD6C
		public override Query Sort(TableSortOrder sortOrder)
		{
			QueryExpression[] array;
			bool[] array2;
			if (SortQuery.TryGetSelectors(sortOrder, QueryTableValue.NewRowType(this), out array, out array2))
			{
				SortOrder[] array3 = new SortOrder[array.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					int num;
					if (!array[i].TryGetColumnAccess(out num))
					{
						array3 = null;
						break;
					}
					if (num >= this.columnToExpand && num < this.columnToExpand + this.fieldsToProject.Length)
					{
						string text = this.fieldsToProject[num - this.columnToExpand];
						array3[i] = new SortOrder(new ExpandRecordColumnQuery.ColumnNullOrOptionalFieldSelectorFunctionValue(this.columnToExpand, this.innerQuery.Columns[this.columnToExpand], text), null, array2[i]);
					}
					else
					{
						array3[i] = sortOrder.SortOrders[i];
					}
				}
				if (array3 != null)
				{
					Query query = this.innerQuery.Sort(new TableSortOrder(array3));
					return new ExpandRecordColumnQuery(this.columnToExpand, this.fieldsToProject, this.newColumns, this.projectedTypes, query);
				}
			}
			return base.Sort(sortOrder);
		}

		// Token: 0x06009EF3 RID: 40691 RVA: 0x0020DC7C File Offset: 0x0020BE7C
		public override TableValue GetPartitionTable(int[] columns)
		{
			int[] array = new int[columns.Length];
			for (int i = 0; i < columns.Length; i++)
			{
				int num = columns[i];
				if (num < this.columnToExpand)
				{
					array[i] = num;
				}
				else
				{
					if (num <= this.columnToExpand + this.fieldsToProject.Length - 1)
					{
						throw new InvalidOperationException();
					}
					array[i] = num - this.fieldsToProject.Length;
				}
			}
			return this.innerQuery.GetPartitionTable(array);
		}

		// Token: 0x06009EF4 RID: 40692 RVA: 0x0020DCEE File Offset: 0x0020BEEE
		public override Query Unordered()
		{
			return this.innerQuery.Unordered().ExpandRecordColumn(this.columnToExpand, this.fieldsToProject, this.newColumns);
		}

		// Token: 0x17002909 RID: 10505
		// (get) Token: 0x06009EF5 RID: 40693 RVA: 0x0020DD12 File Offset: 0x0020BF12
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.innerQuery.QueryDomain;
			}
		}

		// Token: 0x06009EF6 RID: 40694 RVA: 0x0020DD1F File Offset: 0x0020BF1F
		public override IEnumerable<IValueReference> GetRows()
		{
			return new ExpandRecordColumnQuery.ExpandRecordColumnEnumerable(this.innerQuery.GetRows(), this.columns, this.columnToExpand, this.fieldsToProject);
		}

		// Token: 0x06009EF7 RID: 40695 RVA: 0x0020DD43 File Offset: 0x0020BF43
		protected virtual bool TryGetInnerQueryExpression(RecordTypeValue expandedRecordType, QueryExpression expr, out QueryExpression newExpr)
		{
			return this.TryGetInnerQueryExpression(expandedRecordType, expr, this.ApplyBefore, out newExpr);
		}

		// Token: 0x06009EF8 RID: 40696 RVA: 0x0020DD54 File Offset: 0x0020BF54
		protected bool TryGetInnerQueryExpression(RecordTypeValue expandedRecordType, QueryExpression expr, Func<QueryExpression, bool> applyBefore, out QueryExpression newExpr)
		{
			bool supported = applyBefore(expr);
			if (supported)
			{
				QueryExpression newNode;
				newExpr = expr.Rewrite(delegate(QueryExpression node)
				{
					QueryExpressionKind kind = node.Kind;
					if (kind != QueryExpressionKind.ColumnAccess)
					{
						string text;
						if (kind == QueryExpressionKind.Invocation && node.TryGetOptionalColumnAccess(out text))
						{
							int num;
							supported &= this.Columns.TryGetKeyIndex(text, out num);
							if (supported && this.TryCreateExpandedColumnFieldAccess(expandedRecordType, num, true, out newNode))
							{
								return newNode;
							}
						}
						return node;
					}
					ColumnAccessQueryExpression columnAccessQueryExpression = (ColumnAccessQueryExpression)node;
					if (this.TryCreateExpandedColumnFieldAccess(expandedRecordType, columnAccessQueryExpression.Column, false, out newNode))
					{
						return newNode;
					}
					return this.AdjustBefore(columnAccessQueryExpression);
				});
				if (supported)
				{
					return true;
				}
			}
			newExpr = null;
			return false;
		}

		// Token: 0x06009EF9 RID: 40697 RVA: 0x0020DDB0 File Offset: 0x0020BFB0
		private bool TryCreateExpandedColumnFieldAccess(RecordTypeValue expandedRecordType, int columnIndex, bool optional, out QueryExpression newExpr)
		{
			int num = columnIndex - this.columnToExpand;
			if (num >= 0 && num < this.fieldsToProject.Length)
			{
				string text = this.fieldsToProject[num];
				optional = optional || !RecordTypeAlgebra.IsRequiredField(expandedRecordType, text);
				newExpr = new InvocationQueryExpression(new ConstantQueryExpression(optional ? Library.Record.FieldOrDefault : Library.Record.Field), new QueryExpression[]
				{
					new ColumnAccessQueryExpression(this.columnToExpand),
					new ConstantQueryExpression(TextValue.New(text))
				});
				return true;
			}
			newExpr = null;
			return false;
		}

		// Token: 0x1700290A RID: 10506
		// (get) Token: 0x06009EFA RID: 40698 RVA: 0x0020DE3C File Offset: 0x0020C03C
		private Func<QueryExpression, bool> ApplyAfter
		{
			get
			{
				return ExpandRecordColumnQuery.CreateApplyAfter(this.columnToExpand);
			}
		}

		// Token: 0x1700290B RID: 10507
		// (get) Token: 0x06009EFB RID: 40699 RVA: 0x0020DE49 File Offset: 0x0020C049
		private Func<QueryExpression, QueryExpression> AdjustAfter
		{
			get
			{
				return ExpandRecordColumnQuery.CreateAdjustAfter(this.columnToExpand, this.fieldsToProject.Length);
			}
		}

		// Token: 0x1700290C RID: 10508
		// (get) Token: 0x06009EFC RID: 40700 RVA: 0x0020DE61 File Offset: 0x0020C061
		private Func<QueryExpression, bool> ApplyBefore
		{
			get
			{
				return (QueryExpression node) => node.AllAccess(ArgumentAccess.Deny, (int column) => column < this.columnToExpand || column > this.columnToExpand + this.fieldsToProject.Length - 1);
			}
		}

		// Token: 0x1700290D RID: 10509
		// (get) Token: 0x06009EFD RID: 40701 RVA: 0x0020DE6F File Offset: 0x0020C06F
		private Func<QueryExpression, QueryExpression> AdjustBefore
		{
			get
			{
				return (QueryExpression node) => node.AdjustColumnAccess(delegate(int column)
				{
					if (column >= this.columnToExpand)
					{
						return column - this.fieldsToProject.Length + 1;
					}
					return column;
				});
			}
		}

		// Token: 0x06009EFE RID: 40702 RVA: 0x0020DE7D File Offset: 0x0020C07D
		public static Func<QueryExpression, bool> CreateApplyAfter(int columnToExpand)
		{
			Func<int, bool> <>9__1;
			return delegate(QueryExpression node)
			{
				Func<InvocationQueryExpression, bool> deny = ArgumentAccess.Deny;
				Func<int, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (int column) => column != columnToExpand);
				}
				return node.AllAccess(deny, func);
			};
		}

		// Token: 0x06009EFF RID: 40703 RVA: 0x0020DE96 File Offset: 0x0020C096
		public static Func<QueryExpression, QueryExpression> CreateAdjustAfter(int columnToExpand, int fieldsToProject)
		{
			Func<int, int> <>9__1;
			return delegate(QueryExpression node)
			{
				Func<int, int> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = delegate(int column)
					{
						if (column >= columnToExpand)
						{
							return column + fieldsToProject - 1;
						}
						return column;
					});
				}
				return node.AdjustColumnAccess(func);
			};
		}

		// Token: 0x06009F00 RID: 40704 RVA: 0x0020DEB8 File Offset: 0x0020C0B8
		public static int GetInnerAndOuterSelection(Keys innerColumns, Keys outerColumns, ColumnSelection columnSelection, int columnToExpand, int fieldsToProject, out ColumnSelection innerSelection, out ColumnSelection outerSelection)
		{
			ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
			ColumnSelectionBuilder columnSelectionBuilder2 = default(ColumnSelectionBuilder);
			ColumnSelection.SelectMap selectMap = columnSelection.CreateSelectMap(outerColumns);
			for (int i = 0; i < columnToExpand; i++)
			{
				if (selectMap.MapColumn(i) != -1)
				{
					columnSelectionBuilder2.Add(outerColumns[i], columnSelectionBuilder.Count);
					columnSelectionBuilder.Add(innerColumns[i], i);
				}
			}
			int count = columnSelectionBuilder.Count;
			for (int j = 0; j < fieldsToProject; j++)
			{
				int num = columnToExpand + j;
				if (selectMap.MapColumn(num) != -1)
				{
					columnSelectionBuilder2.Add(outerColumns[num], columnSelectionBuilder.Count + j);
				}
			}
			columnSelectionBuilder.Add(innerColumns[columnToExpand], columnToExpand);
			for (int k = columnToExpand + 1; k < innerColumns.Length; k++)
			{
				int num2 = k + fieldsToProject - 1;
				if (selectMap.MapColumn(num2) != -1)
				{
					columnSelectionBuilder2.Add(outerColumns[num2], columnSelectionBuilder.Count + fieldsToProject - 1);
					columnSelectionBuilder.Add(innerColumns[k], k);
				}
			}
			innerSelection = columnSelectionBuilder.ToColumnSelection();
			outerSelection = columnSelectionBuilder2.ToColumnSelection();
			return count;
		}

		// Token: 0x06009F01 RID: 40705 RVA: 0x0020DFE0 File Offset: 0x0020C1E0
		private static Keys CreateColumns(Keys innerQueryColumns, int columnToExpand, Keys newColumns)
		{
			KeysBuilder keysBuilder = default(KeysBuilder);
			for (int i = 0; i < columnToExpand; i++)
			{
				keysBuilder.Add(innerQueryColumns[i]);
			}
			for (int j = 0; j < newColumns.Length; j++)
			{
				keysBuilder.Add(newColumns[j]);
			}
			for (int k = columnToExpand + 1; k < innerQueryColumns.Length; k++)
			{
				keysBuilder.Add(innerQueryColumns[k]);
			}
			return keysBuilder.ToKeys();
		}

		// Token: 0x04005364 RID: 21348
		private int columnToExpand;

		// Token: 0x04005365 RID: 21349
		private Keys fieldsToProject;

		// Token: 0x04005366 RID: 21350
		private Keys newColumns;

		// Token: 0x04005367 RID: 21351
		private Query innerQuery;

		// Token: 0x04005368 RID: 21352
		private Keys columns;

		// Token: 0x04005369 RID: 21353
		private TypeValue[] projectedTypes;

		// Token: 0x0400536A RID: 21354
		private IList<TableKey> tableKeys;

		// Token: 0x0400536B RID: 21355
		private IList<ComputedColumn> computedColumns;

		// Token: 0x0400536C RID: 21356
		private TableSortOrder sortOrder;

		// Token: 0x02001877 RID: 6263
		private class ExpandRecordColumnEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06009F06 RID: 40710 RVA: 0x0020E0C3 File Offset: 0x0020C2C3
			public ExpandRecordColumnEnumerable(IEnumerable<IValueReference> rows, Keys columns, int columnToExpand, Keys fieldsToProject)
			{
				this.rows = rows;
				this.columns = columns;
				this.columnToExpand = columnToExpand;
				this.fieldsToProject = fieldsToProject;
			}

			// Token: 0x06009F07 RID: 40711 RVA: 0x0020E0E8 File Offset: 0x0020C2E8
			public IEnumerator<IValueReference> GetEnumerator()
			{
				return new ExpandRecordColumnQuery.ExpandRecordColumnEnumerable.ExpandRecordColumnEnumerator(this.rows, this.columns, this.columnToExpand, this.fieldsToProject);
			}

			// Token: 0x06009F08 RID: 40712 RVA: 0x0020E107 File Offset: 0x0020C307
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0400536D RID: 21357
			private IEnumerable<IValueReference> rows;

			// Token: 0x0400536E RID: 21358
			private Keys columns;

			// Token: 0x0400536F RID: 21359
			private int columnToExpand;

			// Token: 0x04005370 RID: 21360
			private Keys fieldsToProject;

			// Token: 0x02001878 RID: 6264
			private class ExpandRecordColumnEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
			{
				// Token: 0x06009F09 RID: 40713 RVA: 0x0020E10F File Offset: 0x0020C30F
				public ExpandRecordColumnEnumerator(IEnumerable<IValueReference> rows, Keys columns, int columnToExpand, Keys fieldsToProject)
				{
					this.enumerator = rows.GetEnumerator();
					this.columns = columns;
					this.columnToExpand = columnToExpand;
					this.fieldsToProject = fieldsToProject;
				}

				// Token: 0x1700290E RID: 10510
				// (get) Token: 0x06009F0A RID: 40714 RVA: 0x0020E139 File Offset: 0x0020C339
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06009F0B RID: 40715 RVA: 0x0020E141 File Offset: 0x0020C341
				public void Dispose()
				{
					this.enumerator.Dispose();
				}

				// Token: 0x06009F0C RID: 40716 RVA: 0x0000EE09 File Offset: 0x0000D009
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x1700290F RID: 10511
				// (get) Token: 0x06009F0D RID: 40717 RVA: 0x0020E150 File Offset: 0x0020C350
				public IValueReference Current
				{
					get
					{
						if (this.current == null)
						{
							this.current = ExpandRecordColumnQuery.ExpandRecordColumnEnumerable.ExpandRecordColumnEnumerator.Project(this.enumerator.Current.Value.AsRecord, this.columns, this.columnToExpand, this.fieldsToProject);
						}
						return this.current;
					}
				}

				// Token: 0x06009F0E RID: 40718 RVA: 0x0020E19D File Offset: 0x0020C39D
				public bool MoveNext()
				{
					this.current = null;
					return this.enumerator.MoveNext();
				}

				// Token: 0x06009F0F RID: 40719 RVA: 0x0020E1B4 File Offset: 0x0020C3B4
				private static RecordValue Project(RecordValue row, Keys columns, int columnToExpand, Keys fieldsToProject)
				{
					IValueReference[] array = new IValueReference[columns.Length];
					for (int i = 0; i < columnToExpand; i++)
					{
						array[i] = row.GetReference(i);
					}
					IValueReference reference = row.GetReference(columnToExpand);
					for (int j = 0; j < fieldsToProject.Length; j++)
					{
						array[columnToExpand + j] = new OptionalFieldAccessValueReference(reference, fieldsToProject[j]);
					}
					for (int k = columnToExpand + 1; k < row.Keys.Length; k++)
					{
						array[k + fieldsToProject.Length - 1] = row.GetReference(k);
					}
					return RecordValue.New(columns, array);
				}

				// Token: 0x04005371 RID: 21361
				private IEnumerator<IValueReference> enumerator;

				// Token: 0x04005372 RID: 21362
				private Keys columns;

				// Token: 0x04005373 RID: 21363
				private int columnToExpand;

				// Token: 0x04005374 RID: 21364
				private Keys fieldsToProject;

				// Token: 0x04005375 RID: 21365
				private IValueReference current;
			}
		}

		// Token: 0x02001879 RID: 6265
		private sealed class ColumnNullOrOptionalFieldSelectorFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06009F10 RID: 40720 RVA: 0x0020E246 File Offset: 0x0020C446
			public ColumnNullOrOptionalFieldSelectorFunctionValue(int columnIndex, string columnName, string fieldName)
				: base(Identifier.Underscore.Name)
			{
				this.columnIndex = columnIndex;
				this.columnName = columnName;
				this.fieldName = fieldName;
			}

			// Token: 0x06009F11 RID: 40721 RVA: 0x0020E270 File Offset: 0x0020C470
			public override Value Invoke(Value row)
			{
				Value value = row[this.columnIndex];
				Value value2;
				if (!value.IsNull && value.AsRecord.TryGetValue(this.fieldName, out value2))
				{
					return value2;
				}
				return Value.Null;
			}

			// Token: 0x17002910 RID: 10512
			// (get) Token: 0x06009F12 RID: 40722 RVA: 0x0020E2B0 File Offset: 0x0020C4B0
			public override IExpression Expression
			{
				get
				{
					if (this.expression == null)
					{
						this.expression = new FunctionExpressionSyntaxNode(QueryHelpers.EachFunctionType, new OptionalFieldAccessExpressionSyntaxNode(new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), Identifier.New(this.columnName)), Identifier.New(this.fieldName)));
					}
					return this.expression;
				}
			}

			// Token: 0x17002911 RID: 10513
			// (get) Token: 0x06009F13 RID: 40723 RVA: 0x0020E305 File Offset: 0x0020C505
			public int ColumnIndex
			{
				get
				{
					return this.columnIndex;
				}
			}

			// Token: 0x17002912 RID: 10514
			// (get) Token: 0x06009F14 RID: 40724 RVA: 0x0020E30D File Offset: 0x0020C50D
			public string ColumnName
			{
				get
				{
					return this.columnName;
				}
			}

			// Token: 0x17002913 RID: 10515
			// (get) Token: 0x06009F15 RID: 40725 RVA: 0x0020E315 File Offset: 0x0020C515
			public string FieldName
			{
				get
				{
					return this.fieldName;
				}
			}

			// Token: 0x04005376 RID: 21366
			private int columnIndex;

			// Token: 0x04005377 RID: 21367
			private string columnName;

			// Token: 0x04005378 RID: 21368
			private string fieldName;

			// Token: 0x04005379 RID: 21369
			private IExpression expression;
		}
	}
}
