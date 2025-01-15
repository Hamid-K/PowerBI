using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001647 RID: 5703
	internal class CastTableValue : OptimizableTableValue
	{
		// Token: 0x06008FBC RID: 36796 RVA: 0x001DDFC5 File Offset: 0x001DC1C5
		public static TableValue New(TableValue table, TableTypeValue type)
		{
			if (table.Type.Equals(type))
			{
				return new CastTableValue.CastMetaOnlyTableValue(table, type);
			}
			return new CastTableValue(table, type);
		}

		// Token: 0x06008FBD RID: 36797 RVA: 0x001DDFE4 File Offset: 0x001DC1E4
		protected CastTableValue(TableValue table, TableTypeValue type)
		{
			this.table = table;
			this.type = type;
		}

		// Token: 0x170025A2 RID: 9634
		// (get) Token: 0x06008FBE RID: 36798 RVA: 0x001DDFFC File Offset: 0x001DC1FC
		public override IExpression Expression
		{
			get
			{
				if (this.expression == null)
				{
					this.expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(Library._Value.ReplaceType), new IExpression[]
					{
						this.table.Expression,
						new ConstantExpressionSyntaxNode(this.type)
					});
				}
				return this.expression;
			}
		}

		// Token: 0x06008FBF RID: 36799 RVA: 0x001DE04E File Offset: 0x001DC24E
		public override TableValue Optimize()
		{
			return new OptimizedTableValue(new CastTableValue(this.table.Optimize(), this.type));
		}

		// Token: 0x06008FC0 RID: 36800 RVA: 0x001DE06B File Offset: 0x001DC26B
		private TableValue Cast(TableValue table)
		{
			return CastTableValue.New(table, this.type);
		}

		// Token: 0x170025A3 RID: 9635
		// (get) Token: 0x06008FC1 RID: 36801 RVA: 0x001DE079 File Offset: 0x001DC279
		public override TypeValue Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06008FC2 RID: 36802 RVA: 0x001DE081 File Offset: 0x001DC281
		public override Value NewType(TypeValue type)
		{
			return this.table.NewType(type);
		}

		// Token: 0x170025A4 RID: 9636
		// (get) Token: 0x06008FC3 RID: 36803 RVA: 0x001DE08F File Offset: 0x001DC28F
		public override RecordValue MetaValue
		{
			get
			{
				return this.table.MetaValue;
			}
		}

		// Token: 0x06008FC4 RID: 36804 RVA: 0x001DE09C File Offset: 0x001DC29C
		public override Value NewMeta(RecordValue metaValue)
		{
			return this.Cast(this.table.NewMeta(metaValue).AsTable);
		}

		// Token: 0x170025A5 RID: 9637
		// (get) Token: 0x06008FC5 RID: 36805 RVA: 0x001DE0B5 File Offset: 0x001DC2B5
		public override Keys Columns
		{
			get
			{
				return this.type.ItemType.Fields.Keys;
			}
		}

		// Token: 0x170025A6 RID: 9638
		// (get) Token: 0x06008FC6 RID: 36806 RVA: 0x001DE0CC File Offset: 0x001DC2CC
		public override TableSortOrder SortOrder
		{
			get
			{
				return this.table.SortOrder;
			}
		}

		// Token: 0x170025A7 RID: 9639
		// (get) Token: 0x06008FC7 RID: 36807 RVA: 0x001DE0D9 File Offset: 0x001DC2D9
		public override IList<ComputedColumn> ComputedColumns
		{
			get
			{
				return this.table.ComputedColumns;
			}
		}

		// Token: 0x170025A8 RID: 9640
		// (get) Token: 0x06008FC8 RID: 36808 RVA: 0x001DE0E6 File Offset: 0x001DC2E6
		public override long LargeCount
		{
			get
			{
				return this.table.LargeCount;
			}
		}

		// Token: 0x170025A9 RID: 9641
		// (get) Token: 0x06008FC9 RID: 36809 RVA: 0x001DE0F3 File Offset: 0x001DC2F3
		public override IList<RelatedTable> RelatedTables
		{
			get
			{
				return this.table.RelatedTables;
			}
		}

		// Token: 0x170025AA RID: 9642
		// (get) Token: 0x06008FCA RID: 36810 RVA: 0x001DE100 File Offset: 0x001DC300
		public override ColumnIdentity[] ColumnIdentities
		{
			get
			{
				return this.table.ColumnIdentities;
			}
		}

		// Token: 0x170025AB RID: 9643
		// (get) Token: 0x06008FCB RID: 36811 RVA: 0x001DE10D File Offset: 0x001DC30D
		public override IList<Relationship> Relationships
		{
			get
			{
				return this.table.Relationships;
			}
		}

		// Token: 0x06008FCC RID: 36812 RVA: 0x001DE11A File Offset: 0x001DC31A
		public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables)
		{
			return this.Cast(this.table.ReplaceRelatedTables(relatedTables));
		}

		// Token: 0x06008FCD RID: 36813 RVA: 0x001DE12E File Offset: 0x001DC32E
		public override TableValue ReplaceRelatedTables(IList<RelatedTable> relatedTables, ColumnIdentity[] columnIdentities, IList<Relationship> relationships)
		{
			return this.Cast(this.table.ReplaceRelatedTables(relatedTables, columnIdentities, relationships));
		}

		// Token: 0x06008FCE RID: 36814 RVA: 0x001DE144 File Offset: 0x001DC344
		public override TableValue ReplaceRelationshipIdentity(string identity)
		{
			return this.Cast(this.table.ReplaceRelationshipIdentity(identity));
		}

		// Token: 0x06008FCF RID: 36815 RVA: 0x001DE158 File Offset: 0x001DC358
		public override TableValue ReplaceColumnIdentities(ColumnIdentity[] columnIdentities)
		{
			return this.Cast(this.table.ReplaceColumnIdentities(columnIdentities));
		}

		// Token: 0x06008FD0 RID: 36816 RVA: 0x001DE16C File Offset: 0x001DC36C
		public override TableValue ReplaceRelationships(IList<Relationship> relationships)
		{
			return this.Cast(this.table.ReplaceRelationships(relationships));
		}

		// Token: 0x06008FD1 RID: 36817 RVA: 0x001DE180 File Offset: 0x001DC380
		public override TableValue SelectRows(FunctionValue condition)
		{
			return this.Cast(this.table.SelectRows(condition));
		}

		// Token: 0x06008FD2 RID: 36818 RVA: 0x001DE194 File Offset: 0x001DC394
		public override TableValue Skip(RowCount count)
		{
			return this.Cast(this.table.Skip(count));
		}

		// Token: 0x06008FD3 RID: 36819 RVA: 0x001DE1A8 File Offset: 0x001DC3A8
		public override TableValue Take(RowCount count)
		{
			return this.Cast(this.table.Take(count));
		}

		// Token: 0x06008FD4 RID: 36820 RVA: 0x001DE1BC File Offset: 0x001DC3BC
		public override TableValue Sort(TableSortOrder sortOrder)
		{
			return this.Cast(this.table.Sort(sortOrder));
		}

		// Token: 0x06008FD5 RID: 36821 RVA: 0x001DE1D0 File Offset: 0x001DC3D0
		public override TableValue Unordered()
		{
			return this.Cast(this.table.Unordered());
		}

		// Token: 0x06008FD6 RID: 36822 RVA: 0x001DE1E3 File Offset: 0x001DC3E3
		public override TableValue Distinct(TableDistinct distinctCriteria)
		{
			return this.Cast(this.table.Distinct(distinctCriteria));
		}

		// Token: 0x06008FD7 RID: 36823 RVA: 0x001DE1F7 File Offset: 0x001DC3F7
		public override void TestConnection()
		{
			this.table.TestConnection();
		}

		// Token: 0x06008FD8 RID: 36824 RVA: 0x001DE204 File Offset: 0x001DC404
		public override bool TryGetValue(Value index, out Value value)
		{
			if (index.IsText)
			{
				string asString = index.AsString;
				Keys keys = this.type.AsTableType.ItemType.Fields.Keys;
				Keys keys2 = this.table.Type.AsTableType.ItemType.Fields.Keys;
				int num;
				if (keys.TryGetKeyIndex(asString, out num))
				{
					value = this.table[keys2[num]];
					return true;
				}
				value = Value.Null;
				return false;
			}
			else
			{
				if (index.IsRecord)
				{
					RecordValue asRecord = index.AsRecord;
					Keys keys3 = this.type.AsTableType.ItemType.Fields.Keys;
					Keys keys4 = this.table.Type.AsTableType.ItemType.Fields.Keys;
					Value[] array = new Value[asRecord.Keys.Length];
					KeysBuilder keysBuilder = new KeysBuilder(array.Length);
					for (int i = 0; i < array.Length; i++)
					{
						int num2;
						if (!keys3.TryGetKeyIndex(asRecord.Keys[i], out num2))
						{
							value = Value.Null;
							return false;
						}
						keysBuilder.Add(keys4[num2]);
						array[i] = asRecord[i];
					}
					RecordValue recordValue = RecordValue.New(keysBuilder.ToKeys(), array);
					return this.table.TryGetValue(recordValue, out value);
				}
				return this.table.TryGetValue(index, out value);
			}
		}

		// Token: 0x06008FD9 RID: 36825 RVA: 0x001DE36C File Offset: 0x001DC56C
		public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue table)
		{
			TableValue tableValue = this.table.SelectColumns(columnSelection);
			RecordTypeValue itemType = this.Type.AsTableType.ItemType;
			Value[] array = new Value[columnSelection.Keys.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.type.ItemType.Fields[columnSelection.GetColumn(i)];
			}
			IList<TableKey> list = Microsoft.Mashup.Engine1.Runtime.TableKeys.SelectColumns(this.TableKeys, this.Columns, columnSelection);
			TableTypeValue tableTypeValue = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(columnSelection.Keys, array)), list);
			table = tableValue.NewType(tableTypeValue).AsTable;
			return true;
		}

		// Token: 0x06008FDA RID: 36826 RVA: 0x001DE415 File Offset: 0x001DC615
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return this.table.Select(delegate(IValueReference x)
			{
				RecordTypeValue itemType = this.Type.AsTableType.ItemType;
				RecordValue asRecord = x.Value.AsRecord;
				IValueReference[] array = new IValueReference[asRecord.Keys.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = asRecord.GetReference(i);
				}
				return RecordValue.New(itemType, array);
			}).GetEnumerator();
		}

		// Token: 0x04004D96 RID: 19862
		protected readonly TableValue table;

		// Token: 0x04004D97 RID: 19863
		protected readonly TableTypeValue type;

		// Token: 0x04004D98 RID: 19864
		private IExpression expression;

		// Token: 0x02001648 RID: 5704
		private class CastMetaOnlyTableValue : CastTableValue
		{
			// Token: 0x06008FDC RID: 36828 RVA: 0x001DE48E File Offset: 0x001DC68E
			public CastMetaOnlyTableValue(TableValue table, TableTypeValue type)
				: base(table, type)
			{
			}

			// Token: 0x170025AC RID: 9644
			// (get) Token: 0x06008FDD RID: 36829 RVA: 0x001DE498 File Offset: 0x001DC698
			public override IQueryDomain QueryDomain
			{
				get
				{
					return this.table.QueryDomain;
				}
			}

			// Token: 0x170025AD RID: 9645
			// (get) Token: 0x06008FDE RID: 36830 RVA: 0x001DE4A5 File Offset: 0x001DC6A5
			public override Query Query
			{
				get
				{
					return this.table.Query;
				}
			}

			// Token: 0x170025AE RID: 9646
			// (get) Token: 0x06008FDF RID: 36831 RVA: 0x001DE4B2 File Offset: 0x001DC6B2
			public override IExpression Expression
			{
				get
				{
					return this.table.Expression;
				}
			}

			// Token: 0x06008FE0 RID: 36832 RVA: 0x001DE4BF File Offset: 0x001DC6BF
			public override TableValue Optimize()
			{
				return this.table.Optimize();
			}

			// Token: 0x06008FE1 RID: 36833 RVA: 0x001DE4CC File Offset: 0x001DC6CC
			public override IPageReader GetReader()
			{
				IPageReader reader = this.table.GetReader();
				IPageReader pageReader;
				try
				{
					pageReader = new CastTableValue.CastMetaOnlyTableValue.MetaPageReader(reader, this.type.MetaValue);
				}
				catch
				{
					reader.Dispose();
					throw;
				}
				return pageReader;
			}

			// Token: 0x06008FE2 RID: 36834 RVA: 0x001DE514 File Offset: 0x001DC714
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				RecordTypeValue rowType = this.type.ItemType;
				return this.table.Select((IValueReference x) => x.Value.ReplaceType(rowType)).GetEnumerator();
			}

			// Token: 0x06008FE3 RID: 36835 RVA: 0x001DE554 File Offset: 0x001DC754
			public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue table)
			{
				return this.table.TrySelectColumns(columnSelection, out table);
			}

			// Token: 0x06008FE4 RID: 36836 RVA: 0x001DE563 File Offset: 0x001DC763
			public override TableValue SelectColumns(ColumnSelection columnSelection)
			{
				return this.table.SelectColumns(columnSelection);
			}

			// Token: 0x06008FE5 RID: 36837 RVA: 0x001DE571 File Offset: 0x001DC771
			public override TableValue AddColumns(ColumnsConstructor columnGenerator)
			{
				return this.table.AddColumns(columnGenerator);
			}

			// Token: 0x06008FE6 RID: 36838 RVA: 0x001DE57F File Offset: 0x001DC77F
			public override TableValue NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
			{
				return this.table.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers);
			}

			// Token: 0x06008FE7 RID: 36839 RVA: 0x001DE597 File Offset: 0x001DC797
			public override TableValue Group(Grouping grouping)
			{
				return this.table.Group(grouping);
			}

			// Token: 0x06008FE8 RID: 36840 RVA: 0x001DE5A5 File Offset: 0x001DC7A5
			public override TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
			{
				return this.table.ExpandListColumn(columnIndex, singleOrDefault);
			}

			// Token: 0x06008FE9 RID: 36841 RVA: 0x001DE5B4 File Offset: 0x001DC7B4
			public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
			{
				return this.table.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns);
			}

			// Token: 0x06008FEA RID: 36842 RVA: 0x001DE5C4 File Offset: 0x001DC7C4
			public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
			{
				return this.table.TryInvokeAsArgument(function, arguments, index, out result);
			}

			// Token: 0x06008FEB RID: 36843 RVA: 0x001DE5D6 File Offset: 0x001DC7D6
			public override TableValue DeltaSince(Value tag)
			{
				return this.table.DeltaSince(tag);
			}

			// Token: 0x06008FEC RID: 36844 RVA: 0x001DE5E4 File Offset: 0x001DC7E4
			public override Value NativeQuery(TextValue query, Value parameters, Value options)
			{
				return this.table.NativeQuery(query, parameters, options);
			}

			// Token: 0x06008FED RID: 36845 RVA: 0x001DE5F4 File Offset: 0x001DC7F4
			public override ActionValue InsertRows(Query rowsToInsert)
			{
				return this.table.InsertRows(rowsToInsert);
			}

			// Token: 0x06008FEE RID: 36846 RVA: 0x001DE602 File Offset: 0x001DC802
			public override ActionValue UpdateRows(ColumnUpdates columnUpdates)
			{
				return this.table.UpdateRows(columnUpdates);
			}

			// Token: 0x06008FEF RID: 36847 RVA: 0x001DE610 File Offset: 0x001DC810
			public override ActionValue DeleteRows()
			{
				return this.table.DeleteRows();
			}

			// Token: 0x06008FF0 RID: 36848 RVA: 0x001DE61D File Offset: 0x001DC81D
			public override ActionValue Replace(Value value)
			{
				return this.table.Replace(value);
			}

			// Token: 0x06008FF1 RID: 36849 RVA: 0x001DE62B File Offset: 0x001DC82B
			public override ActionValue NativeStatement(TextValue statement, Value parameters, Value options)
			{
				return this.table.NativeStatement(statement, parameters, options);
			}

			// Token: 0x02001649 RID: 5705
			private sealed class MetaPageReader : DelegatingPageReader
			{
				// Token: 0x06008FF2 RID: 36850 RVA: 0x001DE63B File Offset: 0x001DC83B
				public MetaPageReader(IPageReader pageReader, RecordValue metaValue)
					: base(pageReader)
				{
					this.schema = base.Schema.Copy();
					this.schema.ExtendedProperties.Clear();
					TableDataMapper.SetMetadata(this.schema.ExtendedProperties, metaValue);
				}

				// Token: 0x170025AF RID: 9647
				// (get) Token: 0x06008FF3 RID: 36851 RVA: 0x001DE676 File Offset: 0x001DC876
				public override TableSchema Schema
				{
					get
					{
						return this.schema;
					}
				}

				// Token: 0x04004D99 RID: 19865
				private readonly TableSchema schema;
			}
		}
	}
}
