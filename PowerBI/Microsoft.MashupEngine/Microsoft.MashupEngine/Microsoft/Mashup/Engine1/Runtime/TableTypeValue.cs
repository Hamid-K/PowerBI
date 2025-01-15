using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001621 RID: 5665
	public abstract class TableTypeValue : TypeValue, ITableTypeValue, ITypeValue, IValue
	{
		// Token: 0x06008EC4 RID: 36548 RVA: 0x001DC078 File Offset: 0x001DA278
		public static TableTypeValue New(Keys columns, TypeValue defaultColumnType = null)
		{
			RecordValue columnType = ((defaultColumnType != null && !defaultColumnType.Equals(TypeValue.Any)) ? RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				defaultColumnType,
				LogicalValue.False
			}) : TableTypeValue.fieldType);
			return TableTypeValue.New(RecordTypeValue.New(RecordValue.New(columns, (int index) => columnType)));
		}

		// Token: 0x06008EC5 RID: 36549 RVA: 0x001DC0E0 File Offset: 0x001DA2E0
		public static TableTypeValue New(RecordTypeValue rowType)
		{
			return new TableTypeValue.CustomTableTypeValue(rowType, new List<TableKey>());
		}

		// Token: 0x06008EC6 RID: 36550 RVA: 0x001DC0ED File Offset: 0x001DA2ED
		public static TableTypeValue New(RecordTypeValue itemType, IList<TableKey> tableKeys)
		{
			return new TableTypeValue.CustomTableTypeValue(itemType, tableKeys ?? new List<TableKey>());
		}

		// Token: 0x06008EC7 RID: 36551 RVA: 0x001DC100 File Offset: 0x001DA300
		public static TableTypeValue FromValue(Value columns, TypeValue defaultColumnType = null)
		{
			if (columns.IsType && columns.AsType.TypeKind == ValueKind.Table)
			{
				return columns.AsType.AsTableType;
			}
			if (columns.IsNumber)
			{
				return TableTypeValue.FromValue(columns.AsNumber, defaultColumnType);
			}
			if (columns.IsList)
			{
				return TableTypeValue.FromValue(columns.AsList, defaultColumnType);
			}
			throw ValueException.NewExpressionError<Message0>(Strings.TableType_FromValue, columns, null);
		}

		// Token: 0x06008EC8 RID: 36552 RVA: 0x001DC166 File Offset: 0x001DA366
		public static TableTypeValue FromValue(NumberValue columns, TypeValue defaultColumnType = null)
		{
			return TableTypeValue.New(ColumnLabelGenerator.GenerateKeys(columns.AsInteger32), defaultColumnType);
		}

		// Token: 0x06008EC9 RID: 36553 RVA: 0x001DC17C File Offset: 0x001DA37C
		public static TableTypeValue FromValue(ListValue columns, TypeValue defaultColumnType = null)
		{
			KeysBuilder keysBuilder = new KeysBuilder(columns.AsList.Count);
			foreach (IValueReference valueReference in columns.AsList)
			{
				if (!valueReference.Value.IsText)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.TableType_FromValue, columns, null);
				}
				keysBuilder.Add(valueReference.Value.AsString);
			}
			return TableTypeValue.New(keysBuilder.ToKeys(), defaultColumnType);
		}

		// Token: 0x17002566 RID: 9574
		// (get) Token: 0x06008ECA RID: 36554 RVA: 0x001422C0 File Offset: 0x001404C0
		public override ValueKind TypeKind
		{
			get
			{
				return ValueKind.Table;
			}
		}

		// Token: 0x17002567 RID: 9575
		// (get) Token: 0x06008ECB RID: 36555 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsTableType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002568 RID: 9576
		// (get) Token: 0x06008ECC RID: 36556 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override TableTypeValue AsTableType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17002569 RID: 9577
		// (get) Token: 0x06008ECD RID: 36557
		public abstract RecordTypeValue ItemType { get; }

		// Token: 0x1700256A RID: 9578
		// (get) Token: 0x06008ECE RID: 36558
		public abstract IList<TableKey> TableKeys { get; }

		// Token: 0x06008ECF RID: 36559 RVA: 0x001DC210 File Offset: 0x001DA410
		public TableTypeValue ReplaceTableKeys(IList<TableKey> tableKeys)
		{
			return TableTypeValue.New(this.ItemType, tableKeys);
		}

		// Token: 0x06008ED0 RID: 36560 RVA: 0x001DC220 File Offset: 0x001DA420
		public TableKey GetPrimaryKey()
		{
			foreach (TableKey tableKey in this.TableKeys)
			{
				if (tableKey.Primary)
				{
					return tableKey;
				}
			}
			return null;
		}

		// Token: 0x06008ED1 RID: 36561 RVA: 0x001DC278 File Offset: 0x001DA478
		public override Value NewMeta(RecordValue metaValue)
		{
			if (metaValue.IsEmpty)
			{
				return this;
			}
			return new TableTypeValue.MetaFacetsTableTypeValue(this, metaValue, this.Facets);
		}

		// Token: 0x06008ED2 RID: 36562 RVA: 0x001DC291 File Offset: 0x001DA491
		public override TypeValue NewFacets(TypeFacets facets)
		{
			if (facets.IsEmpty)
			{
				return this;
			}
			return new TableTypeValue.MetaFacetsTableTypeValue(this, this.MetaValue, facets);
		}

		// Token: 0x06008ED3 RID: 36563 RVA: 0x001DC2AA File Offset: 0x001DA4AA
		public override bool IsCompatibleWith(TypeValue other)
		{
			return other.TypeKind == ValueKind.Any || other.NonNullable.Equals(TypeValue.Table) || other.Equals(this);
		}

		// Token: 0x1700256B RID: 9579
		// (get) Token: 0x06008ED4 RID: 36564 RVA: 0x001DC2D3 File Offset: 0x001DA4D3
		IRecordTypeValue ITableTypeValue.ItemType
		{
			get
			{
				return this.ItemType;
			}
		}

		// Token: 0x04004D5F RID: 19807
		public new static readonly TableTypeValue Any = new TableTypeValue.CustomTableTypeValue(RecordTypeValue.Any, new List<TableKey>());

		// Token: 0x04004D60 RID: 19808
		private static readonly RecordValue fieldType = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
		{
			TypeValue.Any,
			LogicalValue.False
		});

		// Token: 0x02001622 RID: 5666
		private class CustomTableTypeValue : TableTypeValue
		{
			// Token: 0x06008ED7 RID: 36567 RVA: 0x001DC316 File Offset: 0x001DA516
			public CustomTableTypeValue(RecordTypeValue itemType, IList<TableKey> keys)
			{
				this.itemType = itemType;
				this.keys = keys;
			}

			// Token: 0x1700256C RID: 9580
			// (get) Token: 0x06008ED8 RID: 36568 RVA: 0x001DC32C File Offset: 0x001DA52C
			public override RecordTypeValue ItemType
			{
				get
				{
					return this.itemType;
				}
			}

			// Token: 0x1700256D RID: 9581
			// (get) Token: 0x06008ED9 RID: 36569 RVA: 0x001DC334 File Offset: 0x001DA534
			public override IList<TableKey> TableKeys
			{
				get
				{
					return this.keys;
				}
			}

			// Token: 0x1700256E RID: 9582
			// (get) Token: 0x06008EDA RID: 36570 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700256F RID: 9583
			// (get) Token: 0x06008EDB RID: 36571 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue NonNullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17002570 RID: 9584
			// (get) Token: 0x06008EDC RID: 36572 RVA: 0x001DC33C File Offset: 0x001DA53C
			public override TypeValue Nullable
			{
				get
				{
					if (this.nullableType == null)
					{
						this.nullableType = new TableTypeValue.NullableTableTypeValue(this);
					}
					return this.nullableType;
				}
			}

			// Token: 0x04004D61 RID: 19809
			private readonly IList<TableKey> keys;

			// Token: 0x04004D62 RID: 19810
			private readonly RecordTypeValue itemType;

			// Token: 0x04004D63 RID: 19811
			private TypeValue nullableType;
		}

		// Token: 0x02001623 RID: 5667
		private class NullableTableTypeValue : TableTypeValue
		{
			// Token: 0x06008EDD RID: 36573 RVA: 0x001DC358 File Offset: 0x001DA558
			public NullableTableTypeValue(TableTypeValue type)
			{
				this.type = type;
			}

			// Token: 0x17002571 RID: 9585
			// (get) Token: 0x06008EDE RID: 36574 RVA: 0x001DC367 File Offset: 0x001DA567
			public override RecordTypeValue ItemType
			{
				get
				{
					return this.type.ItemType;
				}
			}

			// Token: 0x17002572 RID: 9586
			// (get) Token: 0x06008EDF RID: 36575 RVA: 0x001DC374 File Offset: 0x001DA574
			public override IList<TableKey> TableKeys
			{
				get
				{
					return this.type.TableKeys;
				}
			}

			// Token: 0x17002573 RID: 9587
			// (get) Token: 0x06008EE0 RID: 36576 RVA: 0x00002139 File Offset: 0x00000339
			public override bool IsNullable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17002574 RID: 9588
			// (get) Token: 0x06008EE1 RID: 36577 RVA: 0x001DC381 File Offset: 0x001DA581
			public override TypeValue NonNullable
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x17002575 RID: 9589
			// (get) Token: 0x06008EE2 RID: 36578 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue Nullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x04004D64 RID: 19812
			private readonly TableTypeValue type;
		}

		// Token: 0x02001624 RID: 5668
		private class MetaFacetsTableTypeValue : TableTypeValue
		{
			// Token: 0x06008EE3 RID: 36579 RVA: 0x001DC389 File Offset: 0x001DA589
			public MetaFacetsTableTypeValue(TableTypeValue type, RecordValue meta, TypeFacets facets)
			{
				this.type = type;
				this.meta = meta;
				this.facets = facets;
			}

			// Token: 0x06008EE4 RID: 36580 RVA: 0x001DC3A6 File Offset: 0x001DA5A6
			private static TypeValue New(TableTypeValue type, RecordValue meta, TypeFacets facets)
			{
				if (!meta.IsEmpty || !facets.IsEmpty)
				{
					return new TableTypeValue.MetaFacetsTableTypeValue(type, meta, facets);
				}
				return type;
			}

			// Token: 0x17002576 RID: 9590
			// (get) Token: 0x06008EE5 RID: 36581 RVA: 0x001DC3C2 File Offset: 0x001DA5C2
			public override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x17002577 RID: 9591
			// (get) Token: 0x06008EE6 RID: 36582 RVA: 0x001DC3CA File Offset: 0x001DA5CA
			public override TypeFacets Facets
			{
				get
				{
					return this.facets;
				}
			}

			// Token: 0x17002578 RID: 9592
			// (get) Token: 0x06008EE7 RID: 36583 RVA: 0x001DC3D2 File Offset: 0x001DA5D2
			public override bool IsNullable
			{
				get
				{
					return this.type.IsNullable;
				}
			}

			// Token: 0x17002579 RID: 9593
			// (get) Token: 0x06008EE8 RID: 36584 RVA: 0x001DC3DF File Offset: 0x001DA5DF
			public override TypeValue Nullable
			{
				get
				{
					return this.type.Nullable;
				}
			}

			// Token: 0x1700257A RID: 9594
			// (get) Token: 0x06008EE9 RID: 36585 RVA: 0x001DC3EC File Offset: 0x001DA5EC
			public override TypeValue NonNullable
			{
				get
				{
					return this.type.NonNullable;
				}
			}

			// Token: 0x1700257B RID: 9595
			// (get) Token: 0x06008EEA RID: 36586 RVA: 0x001DC3F9 File Offset: 0x001DA5F9
			public override object TypeIdentity
			{
				get
				{
					return this.type.TypeIdentity;
				}
			}

			// Token: 0x1700257C RID: 9596
			// (get) Token: 0x06008EEB RID: 36587 RVA: 0x001DC406 File Offset: 0x001DA606
			public override RecordTypeValue ItemType
			{
				get
				{
					return this.type.ItemType;
				}
			}

			// Token: 0x1700257D RID: 9597
			// (get) Token: 0x06008EEC RID: 36588 RVA: 0x001DC413 File Offset: 0x001DA613
			public override IList<TableKey> TableKeys
			{
				get
				{
					return this.type.TableKeys;
				}
			}

			// Token: 0x06008EED RID: 36589 RVA: 0x001DC420 File Offset: 0x001DA620
			public override Value NewMeta(RecordValue metaValue)
			{
				return TableTypeValue.MetaFacetsTableTypeValue.New(this.type, metaValue, this.facets);
			}

			// Token: 0x06008EEE RID: 36590 RVA: 0x001DC434 File Offset: 0x001DA634
			public override TypeValue NewFacets(TypeFacets facets)
			{
				return TableTypeValue.MetaFacetsTableTypeValue.New(this.type, this.meta, facets);
			}

			// Token: 0x06008EEF RID: 36591 RVA: 0x001DC448 File Offset: 0x001DA648
			public override bool IsCompatibleWith(TypeValue other)
			{
				return this.type.IsCompatibleWith(other);
			}

			// Token: 0x04004D65 RID: 19813
			private readonly TableTypeValue type;

			// Token: 0x04004D66 RID: 19814
			private readonly RecordValue meta;

			// Token: 0x04004D67 RID: 19815
			private readonly TypeFacets facets;
		}
	}
}
