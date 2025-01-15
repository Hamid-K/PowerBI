using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001868 RID: 6248
	internal class ExpandListColumnQuery : Query
	{
		// Token: 0x06009E83 RID: 40579 RVA: 0x0020C5BF File Offset: 0x0020A7BF
		public ExpandListColumnQuery(int columnIndex, bool singleOrDefault, TypeValue columnType, Query innerQuery)
		{
			this.columnIndex = columnIndex;
			this.singleOrDefault = singleOrDefault;
			this.columnType = columnType;
			this.innerQuery = innerQuery;
		}

		// Token: 0x170028E5 RID: 10469
		// (get) Token: 0x06009E84 RID: 40580 RVA: 0x0014213C File Offset: 0x0014033C
		public override QueryKind Kind
		{
			get
			{
				return QueryKind.ExpandListColumn;
			}
		}

		// Token: 0x170028E6 RID: 10470
		// (get) Token: 0x06009E85 RID: 40581 RVA: 0x0020C5E4 File Offset: 0x0020A7E4
		public Query InnerQuery
		{
			get
			{
				return this.innerQuery;
			}
		}

		// Token: 0x170028E7 RID: 10471
		// (get) Token: 0x06009E86 RID: 40582 RVA: 0x0020C5EC File Offset: 0x0020A7EC
		public int ColumnIndex
		{
			get
			{
				return this.columnIndex;
			}
		}

		// Token: 0x170028E8 RID: 10472
		// (get) Token: 0x06009E87 RID: 40583 RVA: 0x0020C5F4 File Offset: 0x0020A7F4
		public bool SingleOrDefault
		{
			get
			{
				return this.singleOrDefault;
			}
		}

		// Token: 0x170028E9 RID: 10473
		// (get) Token: 0x06009E88 RID: 40584 RVA: 0x0020C5FC File Offset: 0x0020A7FC
		public override Keys Columns
		{
			get
			{
				return this.innerQuery.Columns;
			}
		}

		// Token: 0x170028EA RID: 10474
		// (get) Token: 0x06009E89 RID: 40585 RVA: 0x0020C609 File Offset: 0x0020A809
		public TypeValue ColumnType
		{
			get
			{
				return this.columnType;
			}
		}

		// Token: 0x06009E8A RID: 40586 RVA: 0x0020C614 File Offset: 0x0020A814
		public override TypeValue GetColumnType(int column)
		{
			TypeValue typeValue = this.innerQuery.GetColumnType(column);
			if (column == this.columnIndex)
			{
				if (this.columnType == null)
				{
					ValueKind typeKind = typeValue.TypeKind;
					TypeValue typeValue2;
					if (typeKind != ValueKind.List)
					{
						if (typeKind != ValueKind.Table)
						{
							typeValue2 = TypeValue.Any;
						}
						else
						{
							typeValue2 = typeValue.AsTableType.ItemType;
						}
					}
					else
					{
						typeValue2 = typeValue.AsListType.ItemType;
					}
					this.columnType = typeValue2.Nullable.NewMeta(typeValue2.MetaValue).AsType;
				}
				return this.columnType;
			}
			return typeValue;
		}

		// Token: 0x170028EB RID: 10475
		// (get) Token: 0x06009E8B RID: 40587 RVA: 0x0020C698 File Offset: 0x0020A898
		public override IList<TableKey> TableKeys
		{
			get
			{
				if (!this.singleOrDefault)
				{
					return Microsoft.Mashup.Engine1.Runtime.TableKeys.None;
				}
				return this.innerQuery.TableKeys;
			}
		}

		// Token: 0x170028EC RID: 10476
		// (get) Token: 0x06009E8C RID: 40588 RVA: 0x0020C6B3 File Offset: 0x0020A8B3
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				if (this.computedColumns == null)
				{
					this.computedColumns = Microsoft.Mashup.Engine1.Runtime.ComputedColumns.ExpandListColumn(this.innerQuery.ComputedColumns, QueryTableValue.NewRowType(this), this.columnIndex);
				}
				return this.computedColumns;
			}
		}

		// Token: 0x170028ED RID: 10477
		// (get) Token: 0x06009E8D RID: 40589 RVA: 0x0020C6E5 File Offset: 0x0020A8E5
		public override RowCount RowCount
		{
			get
			{
				if (this.singleOrDefault)
				{
					return this.innerQuery.RowCount;
				}
				return base.RowCount;
			}
		}

		// Token: 0x170028EE RID: 10478
		// (get) Token: 0x06009E8E RID: 40590 RVA: 0x0020C704 File Offset: 0x0020A904
		public override TableSortOrder SortOrder
		{
			get
			{
				if (this.sortOrder == null && this.innerQuery.SortOrder != null)
				{
					if (SortQuery.TryAdjustSelectors(QueryTableValue.NewRowType(this.innerQuery), this.Columns, this.innerQuery.SortOrder, this.ApplyAfter))
					{
						this.sortOrder = this.innerQuery.SortOrder;
					}
					else
					{
						this.sortOrder = TableSortOrder.Unknown;
					}
				}
				return this.sortOrder;
			}
		}

		// Token: 0x06009E8F RID: 40591 RVA: 0x0020C773 File Offset: 0x0020A973
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			if (columnIndex < this.columnIndex && this.innerQuery.TryExpandListColumn(columnIndex, singleOrDefault, out query) && query.TryExpandListColumn(this.columnIndex, this.singleOrDefault, out query))
			{
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009E90 RID: 40592 RVA: 0x0020C7AC File Offset: 0x0020A9AC
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			if (columnToExpand < this.columnIndex && this.innerQuery.TryExpandRecordColumn(columnToExpand, fieldsToProject, newColumns, out query) && query.TryExpandListColumn(this.columnIndex + fieldsToProject.Length - 1, this.singleOrDefault, out query))
			{
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009E91 RID: 40593 RVA: 0x0020C7FC File Offset: 0x0020A9FC
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			ColumnSelection columnSelection2;
			ColumnSelection columnSelection3;
			int innerAndOuterSelection = ExpandRecordColumnQuery.GetInnerAndOuterSelection(this.innerQuery.Columns, this.Columns, columnSelection, this.columnIndex, 1, out columnSelection2, out columnSelection3);
			Query query = this.innerQuery.SelectColumns(columnSelection2);
			query = new ExpandListColumnQuery(innerAndOuterSelection, this.singleOrDefault, this.columnType, query);
			return FloatingSelectColumnsQuery.New(columnSelection3, query);
		}

		// Token: 0x06009E92 RID: 40594 RVA: 0x0020C854 File Offset: 0x0020AA54
		public override Query SelectRows(FunctionValue condition)
		{
			FunctionValue functionValue;
			FunctionValue functionValue2;
			if (SelectRowsQuery.TryGetAndAdjustConditions(QueryTableValue.NewRowType(this), this.innerQuery.Columns, condition, this.ApplyBefore, this.AdjustBefore, out functionValue, out functionValue2) && functionValue != null)
			{
				Query query = this.innerQuery.SelectRows(functionValue);
				query = new ExpandListColumnQuery(this.columnIndex, this.singleOrDefault, this.columnType, query);
				if (functionValue2 != null)
				{
					query = SelectRowsQuery.New(functionValue2, query);
				}
				return query;
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06009E93 RID: 40595 RVA: 0x0020C8C8 File Offset: 0x0020AAC8
		public override Query Sort(TableSortOrder sortOrder)
		{
			QueryExpression[] array;
			bool[] array2;
			if (SortQuery.TryGetSelectors(sortOrder, QueryTableValue.NewRowType(this), out array, out array2))
			{
				ArrayBuilder<SortOrder> arrayBuilder = default(ArrayBuilder<SortOrder>);
				ArrayBuilder<Value> arrayBuilder2 = default(ArrayBuilder<Value>);
				bool flag = true;
				for (int i = 0; i < array.Length; i++)
				{
					int num;
					if (arrayBuilder2.Count == 0 && array[i].TryGetColumnAccess(out num) && num != this.columnIndex)
					{
						arrayBuilder.Add(sortOrder.SortOrders[i]);
					}
					else
					{
						string text;
						if (!ExpandListColumnQuery.TryGetColumnNullOrOptionalFieldAccess(array[i], out num, out text) || num != this.columnIndex)
						{
							flag = false;
							break;
						}
						Value value = (array2[i] ? Library.Order.Ascending : Library.Order.Descending);
						arrayBuilder2.Add(ListValue.New(new Value[]
						{
							TextValue.New(text),
							value
						}));
					}
				}
				if (flag)
				{
					Query query = this.innerQuery;
					if (arrayBuilder.Count > 0)
					{
						query = this.innerQuery.Sort(new TableSortOrder(arrayBuilder.ToArray()));
					}
					if (arrayBuilder2.Count > 0)
					{
						query = Query.TransformColumn(query, this.columnIndex, new ExpandListColumnQuery.SortFunctionValue(ListValue.New(arrayBuilder2.ToArray())), null);
					}
					query = new ExpandListColumnQuery(this.columnIndex, this.singleOrDefault, this.columnType, query);
					if (arrayBuilder.Count == 0 && arrayBuilder2.Count > 0)
					{
						query = new ExpandListColumnQuery.WithMergeSortQuery(sortOrder, RowCount.Infinite, (ExpandListColumnQuery)query);
					}
					return query;
				}
			}
			return base.Sort(sortOrder);
		}

		// Token: 0x06009E94 RID: 40596 RVA: 0x0020CA4C File Offset: 0x0020AC4C
		public override Query Take(RowCount count)
		{
			ExpandListColumnQuery expandListColumnQuery;
			if (this.TryNestedTake(count, out expandListColumnQuery))
			{
				return SkipTakeQuery.New(RowRange.All.Take(count), expandListColumnQuery, false);
			}
			return base.Take(count);
		}

		// Token: 0x06009E95 RID: 40597 RVA: 0x0020CA84 File Offset: 0x0020AC84
		private bool TryNestedTake(RowCount count, out ExpandListColumnQuery query)
		{
			if (!count.IsInfinite && count.Value <= 2147483647L)
			{
				query = new ExpandListColumnQuery(this.columnIndex, this.singleOrDefault, this.columnType, Query.TransformColumn(this.innerQuery, this.columnIndex, new ExpandListColumnQuery.TakeFunctionValue(count), this.innerQuery.GetColumnType(this.columnIndex)));
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06009E96 RID: 40598 RVA: 0x0020CAF0 File Offset: 0x0020ACF0
		public override TableValue GetPartitionTable(int[] columns)
		{
			return this.innerQuery.GetPartitionTable(columns);
		}

		// Token: 0x06009E97 RID: 40599 RVA: 0x0020CAFE File Offset: 0x0020ACFE
		public override Query Unordered()
		{
			return this.innerQuery.Unordered().ExpandListColumn(this.columnIndex, this.singleOrDefault);
		}

		// Token: 0x170028EF RID: 10479
		// (get) Token: 0x06009E98 RID: 40600 RVA: 0x0020CB1C File Offset: 0x0020AD1C
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.innerQuery.QueryDomain;
			}
		}

		// Token: 0x06009E99 RID: 40601 RVA: 0x0020CB29 File Offset: 0x0020AD29
		public override IEnumerable<IValueReference> GetRows()
		{
			foreach (IEnumerable<IValueReference> enumerable in this.GetRowsets())
			{
				foreach (IValueReference valueReference in enumerable)
				{
					yield return valueReference;
				}
				IEnumerator<IValueReference> enumerator2 = null;
			}
			IEnumerator<IEnumerable<IValueReference>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x170028F0 RID: 10480
		// (get) Token: 0x06009E9A RID: 40602 RVA: 0x0020CB39 File Offset: 0x0020AD39
		private Func<QueryExpression, bool> ApplyAfter
		{
			get
			{
				return (QueryExpression node) => node.AllAccess(ArgumentAccess.Deny, (int column) => column != this.columnIndex);
			}
		}

		// Token: 0x170028F1 RID: 10481
		// (get) Token: 0x06009E9B RID: 40603 RVA: 0x0020CB47 File Offset: 0x0020AD47
		private Func<QueryExpression, bool> ApplyBefore
		{
			get
			{
				return (QueryExpression node) => node.AllAccess(ArgumentAccess.Deny, (int column) => column != this.columnIndex);
			}
		}

		// Token: 0x170028F2 RID: 10482
		// (get) Token: 0x06009E9C RID: 40604 RVA: 0x0020CB55 File Offset: 0x0020AD55
		private Func<QueryExpression, QueryExpression> AdjustBefore
		{
			get
			{
				return (QueryExpression node) => node;
			}
		}

		// Token: 0x06009E9D RID: 40605 RVA: 0x0020CB76 File Offset: 0x0020AD76
		private IEnumerable<IEnumerable<IValueReference>> GetRowsets()
		{
			foreach (IValueReference valueReference in this.innerQuery.GetRows())
			{
				Value value = valueReference.Value[this.columnIndex];
				ValueKind kind = value.Kind;
				IEnumerable<IValueReference> enumerable;
				if (kind != ValueKind.Null)
				{
					if (kind != ValueKind.List)
					{
						enumerable = ExpandListColumnQuery.AtLeastOne(value.AsTable);
					}
					else
					{
						enumerable = ExpandListColumnQuery.AtLeastOne(value.AsList);
					}
				}
				else
				{
					enumerable = ExpandListColumnQuery.singleNull;
				}
				if (this.singleOrDefault)
				{
					enumerable = ExpandListColumnQuery.Single(enumerable, value);
				}
				yield return ExpandListColumnQuery.Expand(valueReference, this.columnIndex, enumerable);
			}
			IEnumerator<IValueReference> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06009E9E RID: 40606 RVA: 0x0020CB88 File Offset: 0x0020AD88
		private static IEnumerable<IValueReference> Expand(IValueReference row, int columnIndex, IEnumerable<IValueReference> enumerable)
		{
			return enumerable.Select((IValueReference current) => new ExpandListColumnQuery.ExpandListColumnValueReference(row, current, columnIndex));
		}

		// Token: 0x06009E9F RID: 40607 RVA: 0x0020CBBB File Offset: 0x0020ADBB
		private static IEnumerable<IValueReference> AtLeastOne(IEnumerable<IValueReference> enumerable)
		{
			bool atLeastOne = false;
			foreach (IValueReference valueReference in enumerable)
			{
				atLeastOne = true;
				yield return valueReference;
			}
			IEnumerator<IValueReference> enumerator = null;
			if (!atLeastOne)
			{
				yield return Value.Null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06009EA0 RID: 40608 RVA: 0x0020CBCB File Offset: 0x0020ADCB
		private static IEnumerable<IValueReference> Single(IEnumerable<IValueReference> enumerable, Value valueForError)
		{
			return new IValueReference[]
			{
				new ExpandListColumnQuery.SingleOrDefaultValueReference(enumerable, valueForError)
			};
		}

		// Token: 0x06009EA1 RID: 40609 RVA: 0x0020CBE0 File Offset: 0x0020ADE0
		private static bool TryGetColumnNullOrOptionalFieldAccess(QueryExpression selector, out int columnIndex, out string fieldName)
		{
			IList<QueryExpression> list;
			Value value;
			if ((selector.TryGetInvocation(Library.Record.FieldOrDefault, 2, out list) || selector.TryGetInvocation(Library.Collection.FieldOrNull, 2, out list)) && list[0].TryGetColumnAccess(out columnIndex) && list[1].TryGetConstant(out value))
			{
				fieldName = value.AsString;
				return true;
			}
			columnIndex = -1;
			fieldName = null;
			return false;
		}

		// Token: 0x04005339 RID: 21305
		private static readonly IValueReference[] singleNull = new IValueReference[] { Value.Null };

		// Token: 0x0400533A RID: 21306
		private Query innerQuery;

		// Token: 0x0400533B RID: 21307
		private int columnIndex;

		// Token: 0x0400533C RID: 21308
		private TypeValue columnType;

		// Token: 0x0400533D RID: 21309
		private bool singleOrDefault;

		// Token: 0x0400533E RID: 21310
		private TableSortOrder sortOrder;

		// Token: 0x0400533F RID: 21311
		private IList<ComputedColumn> computedColumns;

		// Token: 0x02001869 RID: 6249
		private sealed class WithMergeSortQuery : SortQuery
		{
			// Token: 0x06009EA7 RID: 40615 RVA: 0x0020CC92 File Offset: 0x0020AE92
			public WithMergeSortQuery(TableSortOrder sortOrder, RowCount take, ExpandListColumnQuery expandListColumn)
				: base(sortOrder, take, expandListColumn)
			{
				this.expandListColumn = expandListColumn;
			}

			// Token: 0x06009EA8 RID: 40616 RVA: 0x0020CCA4 File Offset: 0x0020AEA4
			public override Query Take(RowCount count)
			{
				ExpandListColumnQuery expandListColumnQuery;
				if (this.expandListColumn.TryNestedTake(count, out expandListColumnQuery))
				{
					RowCount takeCount = RowRange.All.Take(base.TakeCount).Take(count).TakeCount;
					return new ExpandListColumnQuery.WithMergeSortQuery(this.SortOrder, takeCount, expandListColumnQuery);
				}
				return base.Take(count);
			}

			// Token: 0x06009EA9 RID: 40617 RVA: 0x0020CCFC File Offset: 0x0020AEFC
			public override IEnumerable<IValueReference> GetRows()
			{
				IEnumerable<IValueReference> enumerable = this.expandListColumn.GetRowsets().ToList<IEnumerable<IValueReference>>().MergeSort(new ValueReferenceComparer(this.SortOrder.ToComparer()));
				if (!base.TakeCount.IsInfinite)
				{
					enumerable = enumerable.Take((int)base.TakeCount.Value);
				}
				return enumerable;
			}

			// Token: 0x04005340 RID: 21312
			private readonly ExpandListColumnQuery expandListColumn;
		}

		// Token: 0x0200186A RID: 6250
		private sealed class TakeFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06009EAA RID: 40618 RVA: 0x0020CD56 File Offset: 0x0020AF56
			public TakeFunctionValue(RowCount take)
				: base(Identifier.Underscore.Name)
			{
				this.take = take;
			}

			// Token: 0x06009EAB RID: 40619 RVA: 0x0020CD70 File Offset: 0x0020AF70
			public override Value Invoke(Value value)
			{
				ValueKind kind = value.Kind;
				if (kind != ValueKind.Null)
				{
					if (kind == ValueKind.List)
					{
						return LanguageLibrary.List.Take.Invoke(value, NumberValue.New(this.take.Value));
					}
					if (kind != ValueKind.Record)
					{
						return value.AsTable.Take(this.take);
					}
				}
				return value;
			}

			// Token: 0x04005341 RID: 21313
			private readonly RowCount take;
		}

		// Token: 0x0200186B RID: 6251
		private sealed class SortFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06009EAC RID: 40620 RVA: 0x0020CDC3 File Offset: 0x0020AFC3
			public SortFunctionValue(ListValue tableCriteria)
				: base(Identifier.Underscore.Name)
			{
				this.tableCriteria = tableCriteria;
			}

			// Token: 0x06009EAD RID: 40621 RVA: 0x0020CDDC File Offset: 0x0020AFDC
			public override Value Invoke(Value value)
			{
				ValueKind kind = value.Kind;
				if (kind != ValueKind.Null)
				{
					if (kind == ValueKind.List)
					{
						return LanguageLibrary.List.Sort.Invoke(value, this.ListCriteria);
					}
					if (kind != ValueKind.Record)
					{
						return value.AsTable.Sort(ExpandListColumnQuery.SortFunctionValue.SelectColumns(this.tableCriteria, value.AsTable.Columns));
					}
				}
				return value;
			}

			// Token: 0x170028F3 RID: 10483
			// (get) Token: 0x06009EAE RID: 40622 RVA: 0x0020CE34 File Offset: 0x0020B034
			private ListValue ListCriteria
			{
				get
				{
					if (this.listCriteria == null)
					{
						Value[] array = this.tableCriteria.Select((IValueReference c) => ListValue.New(new Value[]
						{
							new ExpandListColumnQuery.NullOrOptionalFieldSelectorFunctionValue(c.Value.AsList.Item0.AsString),
							c.Value.AsList.Item1
						})).ToArray<ListValue>();
						this.listCriteria = ListValue.New(array);
					}
					return this.listCriteria;
				}
			}

			// Token: 0x06009EAF RID: 40623 RVA: 0x0020CE8C File Offset: 0x0020B08C
			private static ListValue SelectColumns(ListValue criteria, Keys columns)
			{
				return ListValue.New(criteria.Where((IValueReference c) => columns.Contains(c.Value.AsList.Item0.AsString)));
			}

			// Token: 0x04005342 RID: 21314
			private readonly ListValue tableCriteria;

			// Token: 0x04005343 RID: 21315
			private ListValue listCriteria;
		}

		// Token: 0x0200186E RID: 6254
		private sealed class NullOrOptionalFieldSelectorFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06009EB5 RID: 40629 RVA: 0x0020CF28 File Offset: 0x0020B128
			public NullOrOptionalFieldSelectorFunctionValue(string key)
				: base(Identifier.Underscore.Name)
			{
				this.key = key;
			}

			// Token: 0x06009EB6 RID: 40630 RVA: 0x0020CF44 File Offset: 0x0020B144
			public override Value Invoke(Value row)
			{
				Value value;
				if (!row.IsNull && row.AsRecord.TryGetValue(this.key, out value))
				{
					return value;
				}
				return Value.Null;
			}

			// Token: 0x04005347 RID: 21319
			private string key;
		}

		// Token: 0x0200186F RID: 6255
		private class ExpandListColumnValueReference : IValueReference
		{
			// Token: 0x06009EB7 RID: 40631 RVA: 0x0020CF75 File Offset: 0x0020B175
			public ExpandListColumnValueReference(IValueReference row, IValueReference nestedRow, int columnIndex)
			{
				this.row = row;
				this.nestedRow = nestedRow;
				this.columnIndex = columnIndex;
			}

			// Token: 0x170028F4 RID: 10484
			// (get) Token: 0x06009EB8 RID: 40632 RVA: 0x0020CF92 File Offset: 0x0020B192
			public bool Evaluated
			{
				get
				{
					return this.nestedRow == null;
				}
			}

			// Token: 0x170028F5 RID: 10485
			// (get) Token: 0x06009EB9 RID: 40633 RVA: 0x0020CFA0 File Offset: 0x0020B1A0
			public Value Value
			{
				get
				{
					if (this.nestedRow != null)
					{
						RecordValue asRecord = this.row.Value.AsRecord;
						IValueReference[] array = new IValueReference[asRecord.Keys.Length];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = ((i == this.columnIndex) ? this.nestedRow : asRecord.GetReference(i));
						}
						this.row = RecordValue.New(asRecord.Keys, array);
						this.nestedRow = null;
					}
					return this.row.Value;
				}
			}

			// Token: 0x04005348 RID: 21320
			private IValueReference row;

			// Token: 0x04005349 RID: 21321
			private IValueReference nestedRow;

			// Token: 0x0400534A RID: 21322
			private int columnIndex;
		}

		// Token: 0x02001870 RID: 6256
		private sealed class SingleOrDefaultValueReference : IValueReference
		{
			// Token: 0x06009EBA RID: 40634 RVA: 0x0020D024 File Offset: 0x0020B224
			public SingleOrDefaultValueReference(IEnumerable<IValueReference> enumerable, Value valueForError)
			{
				this.enumerableOrReference = enumerable;
				this.valueForError = valueForError;
			}

			// Token: 0x170028F6 RID: 10486
			// (get) Token: 0x06009EBB RID: 40635 RVA: 0x0020D03A File Offset: 0x0020B23A
			public bool Evaluated
			{
				get
				{
					return this.evaluated;
				}
			}

			// Token: 0x170028F7 RID: 10487
			// (get) Token: 0x06009EBC RID: 40636 RVA: 0x0020D044 File Offset: 0x0020B244
			public Value Value
			{
				get
				{
					if (!this.evaluated)
					{
						IEnumerable<IValueReference> enumerable = (IEnumerable<IValueReference>)this.enumerableOrReference;
						IValueReference valueReference = null;
						foreach (IValueReference valueReference2 in enumerable)
						{
							if (valueReference != null)
							{
								throw ValueException.TooManyElements(this.valueForError);
							}
							valueReference = valueReference2;
						}
						this.enumerableOrReference = valueReference ?? Value.Null;
						this.evaluated = true;
					}
					return ((IValueReference)this.enumerableOrReference).Value;
				}
			}

			// Token: 0x0400534B RID: 21323
			private object enumerableOrReference;

			// Token: 0x0400534C RID: 21324
			private Value valueForError;

			// Token: 0x0400534D RID: 21325
			private bool evaluated;
		}
	}
}
