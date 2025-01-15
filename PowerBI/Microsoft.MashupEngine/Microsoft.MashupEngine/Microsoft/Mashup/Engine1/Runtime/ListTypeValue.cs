using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200156A RID: 5482
	public abstract class ListTypeValue : TypeValue, IListTypeValue, ITypeValue, IValue
	{
		// Token: 0x06008848 RID: 34888 RVA: 0x001CEA39 File Offset: 0x001CCC39
		public static ListTypeValue New(TypeValue itemType)
		{
			if (itemType == TypeValue.Any)
			{
				return ListTypeValue.Any;
			}
			return new ListTypeValue.CustomListTypeValue(itemType);
		}

		// Token: 0x06008849 RID: 34889 RVA: 0x001CEA4F File Offset: 0x001CCC4F
		public static ListTypeValue New(ListValue list)
		{
			return ListTypeValue.New(list.Item0.AsType);
		}

		// Token: 0x170023D3 RID: 9171
		// (get) Token: 0x0600884A RID: 34890 RVA: 0x0014025A File Offset: 0x0013E45A
		public override ValueKind TypeKind
		{
			get
			{
				return ValueKind.List;
			}
		}

		// Token: 0x170023D4 RID: 9172
		// (get) Token: 0x0600884B RID: 34891 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsListType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170023D5 RID: 9173
		// (get) Token: 0x0600884C RID: 34892 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override ListTypeValue AsListType
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170023D6 RID: 9174
		// (get) Token: 0x0600884D RID: 34893
		public abstract TypeValue ItemType { get; }

		// Token: 0x0600884E RID: 34894 RVA: 0x001CEA61 File Offset: 0x001CCC61
		public override Value NewMeta(RecordValue metaValue)
		{
			if (metaValue.IsEmpty)
			{
				return this;
			}
			return new ListTypeValue.MetaFacetsListTypeValue(this, metaValue, this.Facets);
		}

		// Token: 0x0600884F RID: 34895 RVA: 0x001CEA7A File Offset: 0x001CCC7A
		public override TypeValue NewFacets(TypeFacets facets)
		{
			if (facets.IsEmpty)
			{
				return this;
			}
			return new ListTypeValue.MetaFacetsListTypeValue(this, this.MetaValue, facets);
		}

		// Token: 0x06008850 RID: 34896 RVA: 0x001CEA93 File Offset: 0x001CCC93
		public override bool IsCompatibleWith(TypeValue other)
		{
			return other.TypeKind == ValueKind.Any || other.NonNullable.Equals(TypeValue.List) || other.Equals(this);
		}

		// Token: 0x170023D7 RID: 9175
		// (get) Token: 0x06008851 RID: 34897 RVA: 0x001CEABC File Offset: 0x001CCCBC
		ITypeValue IListTypeValue.ItemType
		{
			get
			{
				return this.ItemType;
			}
		}

		// Token: 0x04004B89 RID: 19337
		public new static readonly ListTypeValue Any = new ListTypeValue.CustomListTypeValue(TypeValue.Any);

		// Token: 0x04004B8A RID: 19338
		public new static readonly ListTypeValue Number = new ListTypeValue.CustomListTypeValue(TypeValue.Number);

		// Token: 0x04004B8B RID: 19339
		public new static readonly ListTypeValue Record = new ListTypeValue.CustomListTypeValue(TypeValue.Record);

		// Token: 0x04004B8C RID: 19340
		public new static readonly ListTypeValue Text = new ListTypeValue.CustomListTypeValue(TypeValue.Text);

		// Token: 0x0200156B RID: 5483
		private class CustomListTypeValue : ListTypeValue
		{
			// Token: 0x06008854 RID: 34900 RVA: 0x001CEB02 File Offset: 0x001CCD02
			public CustomListTypeValue(TypeValue itemType)
			{
				this.itemType = itemType;
			}

			// Token: 0x170023D8 RID: 9176
			// (get) Token: 0x06008855 RID: 34901 RVA: 0x001CEB11 File Offset: 0x001CCD11
			public override TypeValue ItemType
			{
				get
				{
					return this.itemType;
				}
			}

			// Token: 0x170023D9 RID: 9177
			// (get) Token: 0x06008856 RID: 34902 RVA: 0x00002105 File Offset: 0x00000305
			public override bool IsNullable
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170023DA RID: 9178
			// (get) Token: 0x06008857 RID: 34903 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue NonNullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170023DB RID: 9179
			// (get) Token: 0x06008858 RID: 34904 RVA: 0x001CEB19 File Offset: 0x001CCD19
			public override TypeValue Nullable
			{
				get
				{
					if (this.nullableType == null)
					{
						this.nullableType = new ListTypeValue.NullableListTypeValue(this);
					}
					return this.nullableType;
				}
			}

			// Token: 0x04004B8D RID: 19341
			private readonly TypeValue itemType;

			// Token: 0x04004B8E RID: 19342
			private TypeValue nullableType;
		}

		// Token: 0x0200156C RID: 5484
		private class NullableListTypeValue : ListTypeValue
		{
			// Token: 0x06008859 RID: 34905 RVA: 0x001CEB35 File Offset: 0x001CCD35
			public NullableListTypeValue(ListTypeValue type)
			{
				this.type = type;
			}

			// Token: 0x170023DC RID: 9180
			// (get) Token: 0x0600885A RID: 34906 RVA: 0x001CEB44 File Offset: 0x001CCD44
			public override TypeValue ItemType
			{
				get
				{
					return this.type.ItemType;
				}
			}

			// Token: 0x170023DD RID: 9181
			// (get) Token: 0x0600885B RID: 34907 RVA: 0x00002139 File Offset: 0x00000339
			public override bool IsNullable
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170023DE RID: 9182
			// (get) Token: 0x0600885C RID: 34908 RVA: 0x001CEB51 File Offset: 0x001CCD51
			public override TypeValue NonNullable
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x170023DF RID: 9183
			// (get) Token: 0x0600885D RID: 34909 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override TypeValue Nullable
			{
				get
				{
					return this;
				}
			}

			// Token: 0x04004B8F RID: 19343
			private readonly ListTypeValue type;
		}

		// Token: 0x0200156D RID: 5485
		private class MetaFacetsListTypeValue : ListTypeValue
		{
			// Token: 0x0600885E RID: 34910 RVA: 0x001CEB59 File Offset: 0x001CCD59
			public MetaFacetsListTypeValue(ListTypeValue type, RecordValue meta, TypeFacets facets)
			{
				this.type = type;
				this.meta = meta;
				this.facets = facets;
			}

			// Token: 0x0600885F RID: 34911 RVA: 0x001CEB76 File Offset: 0x001CCD76
			private static TypeValue New(ListTypeValue type, RecordValue meta, TypeFacets facets)
			{
				if (!meta.IsEmpty || !facets.IsEmpty)
				{
					return new ListTypeValue.MetaFacetsListTypeValue(type, meta, facets);
				}
				return type;
			}

			// Token: 0x170023E0 RID: 9184
			// (get) Token: 0x06008860 RID: 34912 RVA: 0x001CEB92 File Offset: 0x001CCD92
			public override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x170023E1 RID: 9185
			// (get) Token: 0x06008861 RID: 34913 RVA: 0x001CEB9A File Offset: 0x001CCD9A
			public override TypeFacets Facets
			{
				get
				{
					return this.facets;
				}
			}

			// Token: 0x170023E2 RID: 9186
			// (get) Token: 0x06008862 RID: 34914 RVA: 0x001CEBA2 File Offset: 0x001CCDA2
			public override bool IsNullable
			{
				get
				{
					return this.type.IsNullable;
				}
			}

			// Token: 0x170023E3 RID: 9187
			// (get) Token: 0x06008863 RID: 34915 RVA: 0x001CEBAF File Offset: 0x001CCDAF
			public override TypeValue NonNullable
			{
				get
				{
					return this.type.NonNullable;
				}
			}

			// Token: 0x170023E4 RID: 9188
			// (get) Token: 0x06008864 RID: 34916 RVA: 0x001CEBBC File Offset: 0x001CCDBC
			public override TypeValue Nullable
			{
				get
				{
					return this.type.Nullable;
				}
			}

			// Token: 0x170023E5 RID: 9189
			// (get) Token: 0x06008865 RID: 34917 RVA: 0x001CEBC9 File Offset: 0x001CCDC9
			public override object TypeIdentity
			{
				get
				{
					return this.type.TypeIdentity;
				}
			}

			// Token: 0x170023E6 RID: 9190
			// (get) Token: 0x06008866 RID: 34918 RVA: 0x001CEBD6 File Offset: 0x001CCDD6
			public override TypeValue ItemType
			{
				get
				{
					return this.type.ItemType;
				}
			}

			// Token: 0x06008867 RID: 34919 RVA: 0x001CEBE3 File Offset: 0x001CCDE3
			public override Value NewMeta(RecordValue metaValue)
			{
				return ListTypeValue.MetaFacetsListTypeValue.New(this.type, metaValue, this.facets);
			}

			// Token: 0x06008868 RID: 34920 RVA: 0x001CEBF7 File Offset: 0x001CCDF7
			public override TypeValue NewFacets(TypeFacets facets)
			{
				return ListTypeValue.MetaFacetsListTypeValue.New(this.type, this.meta, facets);
			}

			// Token: 0x06008869 RID: 34921 RVA: 0x001CEC0B File Offset: 0x001CCE0B
			public override bool IsCompatibleWith(TypeValue other)
			{
				return this.type.IsCompatibleWith(other);
			}

			// Token: 0x04004B90 RID: 19344
			private readonly ListTypeValue type;

			// Token: 0x04004B91 RID: 19345
			private readonly RecordValue meta;

			// Token: 0x04004B92 RID: 19346
			private readonly TypeFacets facets;
		}
	}
}
