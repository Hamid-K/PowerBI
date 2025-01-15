using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017FB RID: 6139
	internal class QueryTableValue : TableValue
	{
		// Token: 0x06009AFE RID: 39678 RVA: 0x00201016 File Offset: 0x001FF216
		public QueryTableValue(TableValue table)
			: this(table.Query, table.Type)
		{
		}

		// Token: 0x06009AFF RID: 39679 RVA: 0x0020102A File Offset: 0x001FF22A
		public QueryTableValue(Query query)
			: this(query, null)
		{
		}

		// Token: 0x06009B00 RID: 39680 RVA: 0x00201034 File Offset: 0x001FF234
		public QueryTableValue(Query query, TypeValue tableType)
		{
			this.query = query;
			this.tableType = tableType;
		}

		// Token: 0x170027DF RID: 10207
		// (get) Token: 0x06009B01 RID: 39681 RVA: 0x0020104A File Offset: 0x001FF24A
		public sealed override TypeValue Type
		{
			get
			{
				if (this.tableType == null)
				{
					this.tableType = QueryTableValue.NewTableType(this.query);
				}
				return this.tableType;
			}
		}

		// Token: 0x170027E0 RID: 10208
		// (get) Token: 0x06009B02 RID: 39682 RVA: 0x0020106B File Offset: 0x001FF26B
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return this.query.ComputedColumns;
			}
		}

		// Token: 0x170027E1 RID: 10209
		// (get) Token: 0x06009B03 RID: 39683 RVA: 0x00201078 File Offset: 0x001FF278
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.query.SortOrder;
			}
		}

		// Token: 0x170027E2 RID: 10210
		// (get) Token: 0x06009B04 RID: 39684 RVA: 0x00201085 File Offset: 0x001FF285
		public override IQueryDomain QueryDomain
		{
			get
			{
				return this.query.QueryDomain;
			}
		}

		// Token: 0x170027E3 RID: 10211
		// (get) Token: 0x06009B05 RID: 39685 RVA: 0x00201092 File Offset: 0x001FF292
		public override Query Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x170027E4 RID: 10212
		// (get) Token: 0x06009B06 RID: 39686 RVA: 0x0020109A File Offset: 0x001FF29A
		public override IExpression Expression
		{
			get
			{
				return QueryToExpressionVisitor.ToResourceExpression(this.OptimizedQuery);
			}
		}

		// Token: 0x170027E5 RID: 10213
		// (get) Token: 0x06009B07 RID: 39687 RVA: 0x002010A7 File Offset: 0x001FF2A7
		private Query OptimizedQuery
		{
			get
			{
				if (this.optimizedQuery == null)
				{
					this.optimizedQuery = this.query.QueryDomain.Optimize(this.query);
				}
				return this.optimizedQuery;
			}
		}

		// Token: 0x170027E6 RID: 10214
		// (get) Token: 0x06009B08 RID: 39688 RVA: 0x002010D3 File Offset: 0x001FF2D3
		public override Keys Columns
		{
			get
			{
				return this.query.Columns;
			}
		}

		// Token: 0x06009B09 RID: 39689 RVA: 0x002010E0 File Offset: 0x001FF2E0
		public override TableValue Optimize()
		{
			if (this.optimizedTable == null)
			{
				this.optimizedTable = new QueryTableValue.OptimizedQueryTableValue(this.OptimizedQuery, null);
			}
			return this.optimizedTable;
		}

		// Token: 0x06009B0A RID: 39690 RVA: 0x00201102 File Offset: 0x001FF302
		public override TypeValue GetColumnType(int index)
		{
			return this.query.GetColumnType(index);
		}

		// Token: 0x170027E7 RID: 10215
		// (get) Token: 0x06009B0B RID: 39691 RVA: 0x00201110 File Offset: 0x001FF310
		public override IList<TableKey> TableKeys
		{
			get
			{
				return this.query.TableKeys;
			}
		}

		// Token: 0x170027E8 RID: 10216
		// (get) Token: 0x06009B0C RID: 39692 RVA: 0x00201120 File Offset: 0x001FF320
		public override long LargeCount
		{
			get
			{
				return this.OptimizedQuery.RowCount.Value;
			}
		}

		// Token: 0x06009B0D RID: 39693 RVA: 0x00201140 File Offset: 0x001FF340
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			if (this.rows == null)
			{
				this.rows = this.OptimizedQuery.GetRows();
			}
			return new QueryTableValue.QueryEnumerator(this.rows.GetEnumerator());
		}

		// Token: 0x06009B0E RID: 39694 RVA: 0x0020116C File Offset: 0x001FF36C
		public override IPageReader GetReader()
		{
			IPageReader pageReader;
			if (this.OptimizedQuery.TryGetReader(out pageReader))
			{
				return pageReader;
			}
			return base.GetReader();
		}

		// Token: 0x06009B0F RID: 39695 RVA: 0x00201190 File Offset: 0x001FF390
		public override void TestConnection()
		{
			this.OptimizedQuery.TestConnection();
		}

		// Token: 0x06009B10 RID: 39696 RVA: 0x0020119D File Offset: 0x001FF39D
		public static RecordTypeValue NewRowType(Query query)
		{
			return new QueryTableValue.RowTypeValue(query);
		}

		// Token: 0x06009B11 RID: 39697 RVA: 0x002011A5 File Offset: 0x001FF3A5
		public static TableTypeValue NewTableType(Query query)
		{
			return TableTypeValue.New(QueryTableValue.NewRowType(query), query.TableKeys);
		}

		// Token: 0x06009B12 RID: 39698 RVA: 0x002011B8 File Offset: 0x001FF3B8
		public override TableValue SelectRows(FunctionValue condition)
		{
			return new QueryTableValue(this.query.SelectRows(condition), this.Type.AsTableType);
		}

		// Token: 0x06009B13 RID: 39699 RVA: 0x002011D6 File Offset: 0x001FF3D6
		public override TableValue AddColumns(ColumnsConstructor columnGenerator)
		{
			return new QueryTableValue(this.query.AddColumns(columnGenerator));
		}

		// Token: 0x06009B14 RID: 39700 RVA: 0x002011EC File Offset: 0x001FF3EC
		public override TableValue NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
		{
			return RelatedTablesTableValue.New(new QueryTableValue(this.query.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers)), Microsoft.Mashup.Engine1.Runtime.RelatedTables.NestedJoin(this.RelatedTables, this.Columns.Length, rightTable), Microsoft.Mashup.Engine1.Runtime.ColumnIdentities.AddColumns(this.ColumnIdentities, 1), Microsoft.Mashup.Engine1.Runtime.Relationships.NestedJoin(this.Relationships, leftKeyColumns, rightTable, rightKey));
		}

		// Token: 0x06009B15 RID: 39701 RVA: 0x0020124A File Offset: 0x001FF44A
		public override TableValue Skip(RowCount count)
		{
			return new QueryTableValue(this.query.Skip(count), this.Type.AsTableType);
		}

		// Token: 0x06009B16 RID: 39702 RVA: 0x00201268 File Offset: 0x001FF468
		public override TableValue Take(RowCount count)
		{
			return new QueryTableValue(this.query.Take(count), this.Type.AsTableType);
		}

		// Token: 0x06009B17 RID: 39703 RVA: 0x00201286 File Offset: 0x001FF486
		public override TableValue Sort(TableSortOrder sortOrder)
		{
			return new QueryTableValue(this.query.Sort(sortOrder), this.Type.AsTableType);
		}

		// Token: 0x06009B18 RID: 39704 RVA: 0x002012A4 File Offset: 0x001FF4A4
		public override TableValue Unordered()
		{
			return new QueryTableValue(this.query.Unordered(), this.Type.AsTableType);
		}

		// Token: 0x06009B19 RID: 39705 RVA: 0x002012C1 File Offset: 0x001FF4C1
		public override TableValue Distinct(TableDistinct distinctCriteria)
		{
			return new QueryTableValue(this.query.Distinct(distinctCriteria));
		}

		// Token: 0x06009B1A RID: 39706 RVA: 0x002012D4 File Offset: 0x001FF4D4
		public override TableValue Group(Grouping grouping)
		{
			return new QueryTableValue(this.query.Group(grouping));
		}

		// Token: 0x06009B1B RID: 39707 RVA: 0x002012E7 File Offset: 0x001FF4E7
		public override TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
		{
			return new QueryTableValue(this.query.ExpandListColumn(columnIndex, singleOrDefault));
		}

		// Token: 0x06009B1C RID: 39708 RVA: 0x002012FB File Offset: 0x001FF4FB
		public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
		{
			return new QueryTableValue(this.query.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns));
		}

		// Token: 0x06009B1D RID: 39709 RVA: 0x00201310 File Offset: 0x001FF510
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			return this.OptimizedQuery.TryInvokeAsArgument(function, arguments, index, out result);
		}

		// Token: 0x06009B1E RID: 39710 RVA: 0x00201322 File Offset: 0x001FF522
		public override TableValue DeltaSince(Value tag)
		{
			return this.OptimizedQuery.DeltaSince(tag);
		}

		// Token: 0x06009B1F RID: 39711 RVA: 0x00201330 File Offset: 0x001FF530
		public override Value NativeQuery(TextValue query, Value parameters, Value options)
		{
			return this.OptimizedQuery.NativeQuery(query, parameters, options);
		}

		// Token: 0x06009B20 RID: 39712 RVA: 0x00201340 File Offset: 0x001FF540
		public override ActionValue InsertRows(Query rowsToInsert)
		{
			return this.OptimizedQuery.InsertRows(rowsToInsert);
		}

		// Token: 0x06009B21 RID: 39713 RVA: 0x0020134E File Offset: 0x001FF54E
		public override ActionValue UpdateRows(ColumnUpdates columnUpdates)
		{
			return this.OptimizedQuery.UpdateRows(columnUpdates);
		}

		// Token: 0x06009B22 RID: 39714 RVA: 0x0020135C File Offset: 0x001FF55C
		public override ActionValue DeleteRows()
		{
			return this.OptimizedQuery.DeleteRows();
		}

		// Token: 0x06009B23 RID: 39715 RVA: 0x00201369 File Offset: 0x001FF569
		public override ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
		{
			return this.OptimizedQuery.NativeStatement(statement, parameters, options);
		}

		// Token: 0x04005200 RID: 20992
		private Query query;

		// Token: 0x04005201 RID: 20993
		private Query optimizedQuery;

		// Token: 0x04005202 RID: 20994
		private TableValue optimizedTable;

		// Token: 0x04005203 RID: 20995
		private IEnumerable<IValueReference> rows;

		// Token: 0x04005204 RID: 20996
		private TypeValue tableType;

		// Token: 0x020017FC RID: 6140
		private class RowTypeValue : RecordTypeValue
		{
			// Token: 0x06009B24 RID: 39716 RVA: 0x00201379 File Offset: 0x001FF579
			public RowTypeValue(Query query)
			{
				this.query = query;
			}

			// Token: 0x170027E9 RID: 10217
			// (get) Token: 0x06009B25 RID: 39717 RVA: 0x00201388 File Offset: 0x001FF588
			private RecordTypeValue EnsureType
			{
				get
				{
					if (this.type == null)
					{
						Value[] array = new Value[this.query.Columns.Length];
						for (int i = 0; i < array.Length; i++)
						{
							int fieldIndex = i;
							array[fieldIndex] = RecordValue.New(RecordTypeValue.RecordFieldKeys, delegate(int recordTypeIndex)
							{
								if (recordTypeIndex == 0)
								{
									return this.query.GetColumnType(fieldIndex);
								}
								return LogicalValue.False;
							});
						}
						this.type = RecordTypeValue.New(RecordValue.New(this.query.Columns, array));
					}
					return this.type;
				}
			}

			// Token: 0x170027EA RID: 10218
			// (get) Token: 0x06009B26 RID: 39718 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027EB RID: 10219
			// (get) Token: 0x06009B27 RID: 39719 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue NonNullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170027EC RID: 10220
			// (get) Token: 0x06009B28 RID: 39720 RVA: 0x00201415 File Offset: 0x001FF615
			public override TypeValue Nullable
			{
				get
				{
					return this.EnsureType.Nullable;
				}
			}

			// Token: 0x170027ED RID: 10221
			// (get) Token: 0x06009B29 RID: 39721 RVA: 0x00201422 File Offset: 0x001FF622
			public override object TypeIdentity
			{
				get
				{
					return this.EnsureType.TypeIdentity;
				}
			}

			// Token: 0x170027EE RID: 10222
			// (get) Token: 0x06009B2A RID: 39722 RVA: 0x0020142F File Offset: 0x001FF62F
			public override Keys FieldKeys
			{
				get
				{
					return this.query.Columns;
				}
			}

			// Token: 0x170027EF RID: 10223
			// (get) Token: 0x06009B2B RID: 39723 RVA: 0x00002105 File Offset: 0x00000305
			public override bool Open
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170027F0 RID: 10224
			// (get) Token: 0x06009B2C RID: 39724 RVA: 0x0020143C File Offset: 0x001FF63C
			public override RecordValue Fields
			{
				get
				{
					return this.EnsureType.Fields;
				}
			}

			// Token: 0x06009B2D RID: 39725 RVA: 0x00201449 File Offset: 0x001FF649
			public override TypeValue GetFieldType(int index, out bool optional)
			{
				optional = false;
				return this.query.GetColumnType(index);
			}

			// Token: 0x04005205 RID: 20997
			private readonly Query query;

			// Token: 0x04005206 RID: 20998
			private RecordTypeValue type;
		}

		// Token: 0x020017FE RID: 6142
		private class QueryEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x06009B30 RID: 39728 RVA: 0x0020147B File Offset: 0x001FF67B
			public QueryEnumerator(IEnumerator<IValueReference> enumerator)
			{
				this.enumerator = enumerator;
			}

			// Token: 0x06009B31 RID: 39729 RVA: 0x0020148A File Offset: 0x001FF68A
			public void Dispose()
			{
				this.enumerator.Dispose();
			}

			// Token: 0x170027F1 RID: 10225
			// (get) Token: 0x06009B32 RID: 39730 RVA: 0x00201497 File Offset: 0x001FF697
			object IEnumerator.Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06009B33 RID: 39731 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x170027F2 RID: 10226
			// (get) Token: 0x06009B34 RID: 39732 RVA: 0x002014A0 File Offset: 0x001FF6A0
			public IValueReference Current
			{
				get
				{
					if (this.current == null)
					{
						try
						{
							this.current = this.enumerator.Current;
						}
						catch (ValueException ex)
						{
							this.current = new ExceptionValueReference(ex);
						}
					}
					return this.current;
				}
			}

			// Token: 0x06009B35 RID: 39733 RVA: 0x002014F0 File Offset: 0x001FF6F0
			public bool MoveNext()
			{
				this.current = null;
				return this.enumerator.MoveNext();
			}

			// Token: 0x04005209 RID: 21001
			private readonly IEnumerator<IValueReference> enumerator;

			// Token: 0x0400520A RID: 21002
			private IValueReference current;
		}

		// Token: 0x020017FF RID: 6143
		public sealed class OptimizedQueryTableValue : QueryTableValue, IOptimizedValue
		{
			// Token: 0x06009B36 RID: 39734 RVA: 0x00201504 File Offset: 0x001FF704
			public OptimizedQueryTableValue(Query query, TypeValue tableType)
				: base(query, tableType)
			{
				this.optimizedQuery = this.query;
				this.optimizedTable = this;
			}

			// Token: 0x170027F3 RID: 10227
			// (get) Token: 0x06009B37 RID: 39735 RVA: 0x00201521 File Offset: 0x001FF721
			public override IExpression Expression
			{
				get
				{
					return QueryToExpressionVisitor.ToExpression(this.query);
				}
			}
		}
	}
}
