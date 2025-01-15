using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Language;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200166C RID: 5740
	public sealed class TextEnumTypeValue : TypeValue
	{
		// Token: 0x0600911B RID: 37147 RVA: 0x001E2928 File Offset: 0x001E0B28
		public TextEnumTypeValue(string name)
		{
			this.name = name;
			this.type = TypeValue.Text;
			this.nameToValue = new Dictionary<string, TextValue>();
		}

		// Token: 0x0600911C RID: 37148 RVA: 0x001E2950 File Offset: 0x001E0B50
		private TextEnumTypeValue(TextEnumTypeValue type, RecordValue newMeta)
		{
			this.name = type.name;
			this.type = TypeValue.Text;
			this.nameToValue = type.nameToValue;
			this.meta = this.MetaValue.Concatenate(newMeta).AsRecord;
		}

		// Token: 0x0600911D RID: 37149 RVA: 0x001E29A0 File Offset: 0x001E0BA0
		public TextValue NewEnumValue(string name, string value, string caption = null)
		{
			if (this.meta != null)
			{
				throw new InvalidOperationException("Enum type " + this.name + " is in use and cannot be modified.");
			}
			RecordBuilder recordBuilder = new RecordBuilder(2);
			recordBuilder.Add("Documentation.Name", TextValue.New(name), TypeValue.Text);
			if (caption != null)
			{
				recordBuilder.Add("Documentation.Caption", TextValue.New(caption), TypeValue.Text);
			}
			string @string = FunctionDescriptionStrings.ResourceManager.GetString(name.Replace(".", "_"));
			if (@string != null)
			{
				recordBuilder.Add("Documentation.Description", TextValue.New(@string), TypeValue.Text);
			}
			return TextValue.New(value).NewMeta(recordBuilder.ToRecord()).AsText;
		}

		// Token: 0x170025EE RID: 9710
		// (get) Token: 0x0600911E RID: 37150 RVA: 0x001E2A58 File Offset: 0x001E0C58
		public override RecordValue MetaValue
		{
			get
			{
				if (this.meta == null)
				{
					TextValue[] array = new TextValue[this.nameToValue.Count];
					int num = 0;
					foreach (TextValue textValue in this.nameToValue.Values.OrderBy((TextValue value) => value.AsString))
					{
						array[num] = textValue;
						num++;
					}
					this.meta = ValueHelper.AdjustEnumTypeMetavalues<TextValue>(LibraryDescriptions.NewEnumType(this.name), array).MetaValue;
				}
				return this.meta;
			}
		}

		// Token: 0x170025EF RID: 9711
		// (get) Token: 0x0600911F RID: 37151 RVA: 0x001E2B10 File Offset: 0x001E0D10
		public override ValueKind TypeKind
		{
			get
			{
				return this.type.TypeKind;
			}
		}

		// Token: 0x170025F0 RID: 9712
		// (get) Token: 0x06009120 RID: 37152 RVA: 0x001E2B1D File Offset: 0x001E0D1D
		public override bool IsNullable
		{
			get
			{
				return this.type.IsNullable;
			}
		}

		// Token: 0x170025F1 RID: 9713
		// (get) Token: 0x06009121 RID: 37153 RVA: 0x001E2B2A File Offset: 0x001E0D2A
		public override TypeValue NonNullable
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170025F2 RID: 9714
		// (get) Token: 0x06009122 RID: 37154 RVA: 0x001E2B32 File Offset: 0x001E0D32
		public override TypeValue Nullable
		{
			get
			{
				if (this.nullableType == null)
				{
					this.nullableType = this.type.Nullable.NewMeta(this.MetaValue).AsType;
				}
				return this.nullableType;
			}
		}

		// Token: 0x170025F3 RID: 9715
		// (get) Token: 0x06009123 RID: 37155 RVA: 0x001E2B63 File Offset: 0x001E0D63
		public override object TypeIdentity
		{
			get
			{
				return this.type.TypeIdentity;
			}
		}

		// Token: 0x06009124 RID: 37156 RVA: 0x001E2B70 File Offset: 0x001E0D70
		public override Value NewMeta(RecordValue metaValue)
		{
			return new TextEnumTypeValue(this, metaValue);
		}

		// Token: 0x06009125 RID: 37157 RVA: 0x001E2B79 File Offset: 0x001E0D79
		public override bool IsCompatibleWith(TypeValue other)
		{
			return this.type.IsCompatibleWith(other);
		}

		// Token: 0x04004DF8 RID: 19960
		private readonly string name;

		// Token: 0x04004DF9 RID: 19961
		private readonly TypeValue type;

		// Token: 0x04004DFA RID: 19962
		private readonly Dictionary<string, TextValue> nameToValue;

		// Token: 0x04004DFB RID: 19963
		private RecordValue meta;

		// Token: 0x04004DFC RID: 19964
		private TypeValue nullableType;
	}
}
