using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200166E RID: 5742
	public class TextValue : PrimitiveValue, ITextValue, IValue
	{
		// Token: 0x06009129 RID: 37161 RVA: 0x001E2B93 File Offset: 0x001E0D93
		public static TextValue New(char value)
		{
			return new TextValue(new string(value, 1));
		}

		// Token: 0x0600912A RID: 37162 RVA: 0x001E2BA1 File Offset: 0x001E0DA1
		public static TextValue New(string value)
		{
			if (value.Length == 0)
			{
				return TextValue.Empty;
			}
			return new TextValue(value);
		}

		// Token: 0x0600912B RID: 37163 RVA: 0x001E2BB7 File Offset: 0x001E0DB7
		private static TextValue New(TextValue value, RecordValue meta, TypeValue type)
		{
			if (value.MetaValue.IsEmpty && meta.IsEmpty && type == TypeValue.Text)
			{
				return value;
			}
			return new TextValue.MetaTypeTextValue(value.AsString, meta, type);
		}

		// Token: 0x0600912C RID: 37164 RVA: 0x001E2BE5 File Offset: 0x001E0DE5
		public static Value NewOrNull(string value)
		{
			if (value == null)
			{
				return Value.Null;
			}
			return TextValue.New(value);
		}

		// Token: 0x0600912D RID: 37165 RVA: 0x001E2BF6 File Offset: 0x001E0DF6
		protected TextValue(string value)
		{
			this.value = value;
		}

		// Token: 0x170025F4 RID: 9716
		// (get) Token: 0x0600912E RID: 37166 RVA: 0x001C2E64 File Offset: 0x001C1064
		public override TypeValue Type
		{
			get
			{
				return TypeValue.Text;
			}
		}

		// Token: 0x0600912F RID: 37167 RVA: 0x001E2C05 File Offset: 0x001E0E05
		public override Value NewType(TypeValue type)
		{
			return TextValue.New(this, this.MetaValue, type);
		}

		// Token: 0x170025F5 RID: 9717
		// (get) Token: 0x06009130 RID: 37168 RVA: 0x000024ED File Offset: 0x000006ED
		public sealed override ValueKind Kind
		{
			get
			{
				return ValueKind.Text;
			}
		}

		// Token: 0x170025F6 RID: 9718
		// (get) Token: 0x06009131 RID: 37169 RVA: 0x00002139 File Offset: 0x00000339
		public override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170025F7 RID: 9719
		// (get) Token: 0x06009132 RID: 37170 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override TextValue AsText
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170025F8 RID: 9720
		// (get) Token: 0x06009133 RID: 37171 RVA: 0x001E2C14 File Offset: 0x001E0E14
		public string String
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170025F9 RID: 9721
		// (get) Token: 0x06009134 RID: 37172 RVA: 0x001E2C1C File Offset: 0x001E0E1C
		public int Length
		{
			get
			{
				return this.String.Length;
			}
		}

		// Token: 0x170025FA RID: 9722
		public override Value this[int index]
		{
			get
			{
				if (index >= this.String.Length)
				{
					throw ValueException.TextIndexOutOfRange(index, this);
				}
				return TextValue.New(this.String[index]);
			}
		}

		// Token: 0x06009136 RID: 37174 RVA: 0x001E2C52 File Offset: 0x001E0E52
		public sealed override string ToSource()
		{
			return Escape.AsQuotedString(this.value);
		}

		// Token: 0x06009137 RID: 37175 RVA: 0x001E2C5F File Offset: 0x001E0E5F
		public override object ToOleDb(Type type)
		{
			return ValueMarshaller.ToOleDbString(this.value, this, type);
		}

		// Token: 0x06009138 RID: 37176 RVA: 0x001E2C6E File Offset: 0x001E0E6E
		public override Value Concatenate(Value value)
		{
			if (value.IsNull)
			{
				return Value.Null;
			}
			if (value.IsText)
			{
				return TextValue.New(this.String + value.AsText.String);
			}
			return base.Concatenate(value);
		}

		// Token: 0x06009139 RID: 37177 RVA: 0x001E2CA9 File Offset: 0x001E0EA9
		public override bool Equals(Value value, _ValueComparer comparer)
		{
			return value.IsText && comparer.Equals(this.String, value.AsText.String);
		}

		// Token: 0x0600913A RID: 37178 RVA: 0x001E2CCC File Offset: 0x001E0ECC
		public override int GetHashCode(_ValueComparer comparer)
		{
			return comparer.GetHashCode(this.String);
		}

		// Token: 0x0600913B RID: 37179 RVA: 0x001E2CDA File Offset: 0x001E0EDA
		public override int CompareTo(Value value, _ValueComparer comparer)
		{
			if (value.IsText)
			{
				return comparer.Compare(this.String, value.AsText.String);
			}
			return base.CompareTo(value, comparer);
		}

		// Token: 0x170025FB RID: 9723
		// (get) Token: 0x0600913C RID: 37180 RVA: 0x00019E61 File Offset: 0x00018061
		public override RecordValue MetaValue
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x0600913D RID: 37181 RVA: 0x001E2D04 File Offset: 0x001E0F04
		public override Value NewMeta(RecordValue metaValue)
		{
			return TextValue.New(this, metaValue, this.Type);
		}

		// Token: 0x0600913E RID: 37182 RVA: 0x001E2C14 File Offset: 0x001E0E14
		public override string ToString()
		{
			return this.value;
		}

		// Token: 0x04004DFF RID: 19967
		public static readonly TextValue Empty = new TextValue(string.Empty);

		// Token: 0x04004E00 RID: 19968
		private string value;

		// Token: 0x0200166F RID: 5743
		private class MetaTypeTextValue : TextValue
		{
			// Token: 0x06009140 RID: 37184 RVA: 0x001E2D24 File Offset: 0x001E0F24
			public MetaTypeTextValue(string value, RecordValue meta, TypeValue type)
				: base(value)
			{
				this.type = type;
				this.meta = meta;
			}

			// Token: 0x170025FC RID: 9724
			// (get) Token: 0x06009141 RID: 37185 RVA: 0x001E2D3B File Offset: 0x001E0F3B
			public override TypeValue Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x170025FD RID: 9725
			// (get) Token: 0x06009142 RID: 37186 RVA: 0x001E2D43 File Offset: 0x001E0F43
			public override RecordValue MetaValue
			{
				get
				{
					return this.meta;
				}
			}

			// Token: 0x04004E01 RID: 19969
			private TypeValue type;

			// Token: 0x04004E02 RID: 19970
			private RecordValue meta;
		}
	}
}
