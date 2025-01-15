using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015E6 RID: 5606
	public abstract class RecordTypeValue : TypeValue, IRecordTypeValue, ITypeValue, IValue
	{
		// Token: 0x06008CE0 RID: 36064 RVA: 0x001D86D8 File Offset: 0x001D68D8
		public static RecordValue NewField(TypeValue type, LogicalValue optional = null)
		{
			return RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				type,
				optional ?? LogicalValue.False
			});
		}

		// Token: 0x06008CE1 RID: 36065 RVA: 0x001D86FB File Offset: 0x001D68FB
		public static RecordTypeValue New(Keys keys)
		{
			return RecordTypeValue.New(RecordValue.New(keys, (int i) => RecordTypeValue.defaultType));
		}

		// Token: 0x06008CE2 RID: 36066 RVA: 0x001D8727 File Offset: 0x001D6927
		public static RecordTypeValue New(RecordValue types)
		{
			return RecordTypeValue.New(types, false);
		}

		// Token: 0x06008CE3 RID: 36067 RVA: 0x001D8730 File Offset: 0x001D6930
		public static RecordTypeValue New(RecordValue types, bool open)
		{
			if (types.Keys.Length == 0 && open)
			{
				return RecordTypeValue.Any;
			}
			return new RecordTypeValue.CustomRecordTypeValue(types, open);
		}

		// Token: 0x170024ED RID: 9453
		// (get) Token: 0x06008CE4 RID: 36068 RVA: 0x0014213C File Offset: 0x0014033C
		public override ValueKind TypeKind
		{
			get
			{
				return ValueKind.Record;
			}
		}

		// Token: 0x170024EE RID: 9454
		// (get) Token: 0x06008CE5 RID: 36069 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsRecordType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170024EF RID: 9455
		// (get) Token: 0x06008CE6 RID: 36070 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override RecordTypeValue AsRecordType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170024F0 RID: 9456
		// (get) Token: 0x06008CE7 RID: 36071 RVA: 0x001D8751 File Offset: 0x001D6951
		public virtual Keys FieldKeys
		{
			get
			{
				return this.Fields.Keys;
			}
		}

		// Token: 0x170024F1 RID: 9457
		// (get) Token: 0x06008CE8 RID: 36072
		public abstract RecordValue Fields { get; }

		// Token: 0x170024F2 RID: 9458
		// (get) Token: 0x06008CE9 RID: 36073
		public abstract bool Open { get; }

		// Token: 0x06008CEA RID: 36074 RVA: 0x001D8760 File Offset: 0x001D6960
		public virtual TypeValue GetFieldType(int index, out bool optional)
		{
			RecordValue asRecord = this.Fields[index].AsRecord;
			optional = asRecord["Optional"].AsBoolean;
			return asRecord["Type"].AsType;
		}

		// Token: 0x06008CEB RID: 36075 RVA: 0x001D87A4 File Offset: 0x001D69A4
		public bool TryGetFieldType(string field, out TypeValue type, out bool optional)
		{
			int num;
			if (this.FieldKeys.TryGetKeyIndex(field, out num))
			{
				type = this.GetFieldType(num, out optional);
				return true;
			}
			type = null;
			optional = false;
			return false;
		}

		// Token: 0x06008CEC RID: 36076 RVA: 0x001D87D4 File Offset: 0x001D69D4
		public override Value NewMeta(RecordValue metaValue)
		{
			if (metaValue.IsEmpty)
			{
				return this;
			}
			return new RecordTypeValue.MetaFacetsRecordTypeValue(this, metaValue, this.Facets);
		}

		// Token: 0x06008CED RID: 36077 RVA: 0x001D87ED File Offset: 0x001D69ED
		public override TypeValue NewFacets(TypeFacets facets)
		{
			if (facets.IsEmpty)
			{
				return this;
			}
			return new RecordTypeValue.MetaFacetsRecordTypeValue(this, this.MetaValue, facets);
		}

		// Token: 0x06008CEE RID: 36078 RVA: 0x001D8806 File Offset: 0x001D6A06
		public override bool IsCompatibleWith(TypeValue other)
		{
			return other.TypeKind == ValueKind.Any || other.NonNullable.Equals(TypeValue.Record) || other.Equals(this);
		}

		// Token: 0x170024F3 RID: 9459
		// (get) Token: 0x06008CEF RID: 36079 RVA: 0x001D882F File Offset: 0x001D6A2F
		bool IRecordTypeValue.Open
		{
			get
			{
				return this.Open;
			}
		}

		// Token: 0x170024F4 RID: 9460
		// (get) Token: 0x06008CF0 RID: 36080 RVA: 0x001D8837 File Offset: 0x001D6A37
		IRecordValue IRecordTypeValue.Fields
		{
			get
			{
				return this.Fields;
			}
		}

		// Token: 0x04004CD6 RID: 19670
		public new static readonly RecordTypeValue Any = new RecordTypeValue.CustomRecordTypeValue(RecordValue.Empty, true);

		// Token: 0x04004CD7 RID: 19671
		public const string TypeKey = "Type";

		// Token: 0x04004CD8 RID: 19672
		public const string OptionalKey = "Optional";

		// Token: 0x04004CD9 RID: 19673
		public static readonly Keys RecordFieldKeys = Keys.New("Type", "Optional");

		// Token: 0x04004CDA RID: 19674
		private static RecordValue defaultType = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
		{
			TypeValue.Any,
			LogicalValue.False
		});

		// Token: 0x020015E7 RID: 5607
		private class CustomRecordTypeValue : RecordTypeValue
		{
			// Token: 0x06008CF3 RID: 36083 RVA: 0x001D8896 File Offset: 0x001D6A96
			public CustomRecordTypeValue(RecordValue types, bool open)
			{
				this.fields = types;
				this.open = open;
			}

			// Token: 0x170024F5 RID: 9461
			// (get) Token: 0x06008CF4 RID: 36084 RVA: 0x001D88AC File Offset: 0x001D6AAC
			public override RecordValue Fields
			{
				get
				{
					return this.fields;
				}
			}

			// Token: 0x170024F6 RID: 9462
			// (get) Token: 0x06008CF5 RID: 36085 RVA: 0x001D88B4 File Offset: 0x001D6AB4
			public override bool Open
			{
				get
				{
					return this.open;
				}
			}

			// Token: 0x170024F7 RID: 9463
			// (get) Token: 0x06008CF6 RID: 36086 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170024F8 RID: 9464
			// (get) Token: 0x06008CF7 RID: 36087 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue NonNullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170024F9 RID: 9465
			// (get) Token: 0x06008CF8 RID: 36088 RVA: 0x001D88BC File Offset: 0x001D6ABC
			public override TypeValue Nullable
			{
				get
				{
					if (this.nullableType == null)
					{
						this.nullableType = new RecordTypeValue.NullableRecordTypeValue(this);
					}
					return this.nullableType;
				}
			}

			// Token: 0x04004CDB RID: 19675
			private readonly RecordValue fields;

			// Token: 0x04004CDC RID: 19676
			private readonly bool open;

			// Token: 0x04004CDD RID: 19677
			private TypeValue nullableType;
		}

		// Token: 0x020015E8 RID: 5608
		private class NullableRecordTypeValue : RecordTypeValue
		{
			// Token: 0x06008CF9 RID: 36089 RVA: 0x001D88D8 File Offset: 0x001D6AD8
			public NullableRecordTypeValue(RecordTypeValue type)
			{
				this.type = type;
			}

			// Token: 0x170024FA RID: 9466
			// (get) Token: 0x06008CFA RID: 36090 RVA: 0x001D88E7 File Offset: 0x001D6AE7
			public override RecordValue Fields
			{
				get
				{
					return this.type.Fields;
				}
			}

			// Token: 0x170024FB RID: 9467
			// (get) Token: 0x06008CFB RID: 36091 RVA: 0x001D88F4 File Offset: 0x001D6AF4
			public override bool Open
			{
				get
				{
					return this.type.Open;
				}
			}

			// Token: 0x170024FC RID: 9468
			// (get) Token: 0x06008CFC RID: 36092 RVA: 0x00002139 File Offset: 0x00000339
			public override bool IsNullable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170024FD RID: 9469
			// (get) Token: 0x06008CFD RID: 36093 RVA: 0x001D8901 File Offset: 0x001D6B01
			public override TypeValue NonNullable
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x170024FE RID: 9470
			// (get) Token: 0x06008CFE RID: 36094 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue Nullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x04004CDE RID: 19678
			private readonly RecordTypeValue type;
		}

		// Token: 0x020015E9 RID: 5609
		private class MetaFacetsRecordTypeValue : RecordTypeValue
		{
			// Token: 0x06008CFF RID: 36095 RVA: 0x001D8909 File Offset: 0x001D6B09
			public MetaFacetsRecordTypeValue(RecordTypeValue type, RecordValue meta, TypeFacets facets)
			{
				this.type = type;
				this.meta = meta;
				this.facets = facets;
			}

			// Token: 0x06008D00 RID: 36096 RVA: 0x001D8926 File Offset: 0x001D6B26
			private static TypeValue New(RecordTypeValue type, RecordValue meta, TypeFacets facets)
			{
				if (!meta.IsEmpty || !facets.IsEmpty)
				{
					return new RecordTypeValue.MetaFacetsRecordTypeValue(type, meta, facets);
				}
				return type;
			}

			// Token: 0x170024FF RID: 9471
			// (get) Token: 0x06008D01 RID: 36097 RVA: 0x001D8942 File Offset: 0x001D6B42
			public override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x17002500 RID: 9472
			// (get) Token: 0x06008D02 RID: 36098 RVA: 0x001D894A File Offset: 0x001D6B4A
			public override TypeFacets Facets
			{
				get
				{
					return this.facets;
				}
			}

			// Token: 0x17002501 RID: 9473
			// (get) Token: 0x06008D03 RID: 36099 RVA: 0x001D8952 File Offset: 0x001D6B52
			public override bool IsNullable
			{
				get
				{
					return this.type.IsNullable;
				}
			}

			// Token: 0x17002502 RID: 9474
			// (get) Token: 0x06008D04 RID: 36100 RVA: 0x001D895F File Offset: 0x001D6B5F
			public override TypeValue NonNullable
			{
				get
				{
					return this.type.NonNullable;
				}
			}

			// Token: 0x17002503 RID: 9475
			// (get) Token: 0x06008D05 RID: 36101 RVA: 0x001D896C File Offset: 0x001D6B6C
			public override TypeValue Nullable
			{
				get
				{
					return this.type.Nullable;
				}
			}

			// Token: 0x17002504 RID: 9476
			// (get) Token: 0x06008D06 RID: 36102 RVA: 0x001D8979 File Offset: 0x001D6B79
			public override object TypeIdentity
			{
				get
				{
					return this.type.TypeIdentity;
				}
			}

			// Token: 0x17002505 RID: 9477
			// (get) Token: 0x06008D07 RID: 36103 RVA: 0x001D8986 File Offset: 0x001D6B86
			public override RecordValue Fields
			{
				get
				{
					return this.type.Fields;
				}
			}

			// Token: 0x17002506 RID: 9478
			// (get) Token: 0x06008D08 RID: 36104 RVA: 0x001D8993 File Offset: 0x001D6B93
			public override bool Open
			{
				get
				{
					return this.type.Open;
				}
			}

			// Token: 0x06008D09 RID: 36105 RVA: 0x001D89A0 File Offset: 0x001D6BA0
			public override Value NewMeta(RecordValue metaValue)
			{
				return RecordTypeValue.MetaFacetsRecordTypeValue.New(this.type, metaValue, this.facets);
			}

			// Token: 0x06008D0A RID: 36106 RVA: 0x001D89B4 File Offset: 0x001D6BB4
			public override TypeValue NewFacets(TypeFacets facets)
			{
				return RecordTypeValue.MetaFacetsRecordTypeValue.New(this.type, this.meta, facets);
			}

			// Token: 0x06008D0B RID: 36107 RVA: 0x001D89C8 File Offset: 0x001D6BC8
			public override bool IsCompatibleWith(TypeValue other)
			{
				return this.type.IsCompatibleWith(other);
			}

			// Token: 0x04004CDF RID: 19679
			private readonly RecordTypeValue type;

			// Token: 0x04004CE0 RID: 19680
			private readonly RecordValue meta;

			// Token: 0x04004CE1 RID: 19681
			private readonly TypeFacets facets;
		}
	}
}
