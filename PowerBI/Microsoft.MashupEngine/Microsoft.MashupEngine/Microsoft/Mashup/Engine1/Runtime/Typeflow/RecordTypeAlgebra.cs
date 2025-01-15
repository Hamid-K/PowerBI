using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Runtime.Typeflow
{
	// Token: 0x020016E2 RID: 5858
	internal static class RecordTypeAlgebra
	{
		// Token: 0x060094DD RID: 38109 RVA: 0x001EB7C0 File Offset: 0x001E99C0
		public static RecordValue OptionalToNullableField(RecordValue field)
		{
			if (field["Optional"].AsBoolean)
			{
				field = RecordTypeAlgebra.NewField(field["Type"].AsType.Nullable, false);
			}
			return field;
		}

		// Token: 0x060094DE RID: 38110 RVA: 0x001EB7F2 File Offset: 0x001E99F2
		public static RecordValue EnsureNullableField(RecordValue field)
		{
			return RecordTypeAlgebra.NewField(field["Type"].AsType.Nullable, field["Optional"].AsBoolean);
		}

		// Token: 0x060094DF RID: 38111 RVA: 0x001EB820 File Offset: 0x001E9A20
		public static TypeValue EnsureNullableFields(TypeValue type)
		{
			if (type.TypeKind == ValueKind.Record)
			{
				RecordTypeValue asRecordType = type.AsRecordType;
				Value[] array = new Value[asRecordType.Fields.Keys.Length];
				for (int i = 0; i < asRecordType.Fields.Keys.Length; i++)
				{
					array[i] = RecordTypeAlgebra.EnsureNullableField(asRecordType.Fields[i].AsRecord);
				}
				return RecordTypeValue.New(RecordValue.New(asRecordType.Fields.Keys, array), asRecordType.Open);
			}
			if (type.TypeKind == ValueKind.Any)
			{
				return type;
			}
			return TypeValue.None;
		}

		// Token: 0x060094E0 RID: 38112 RVA: 0x001EB8B8 File Offset: 0x001E9AB8
		public static TypeValue EnsureClosedWithRequiredFieldsOnly(TypeValue type)
		{
			if (type.TypeKind == ValueKind.Record)
			{
				RecordTypeValue asRecordType = type.AsRecordType;
				Value[] array = new Value[asRecordType.Fields.Keys.Length];
				for (int i = 0; i < asRecordType.Fields.Keys.Length; i++)
				{
					array[i] = RecordTypeAlgebra.OptionalToNullableField(asRecordType.Fields[i].AsRecord);
				}
				return RecordTypeValue.New(RecordValue.New(asRecordType.Fields.Keys, array), false);
			}
			if (type.TypeKind == ValueKind.Any)
			{
				return type;
			}
			return TypeValue.None;
		}

		// Token: 0x060094E1 RID: 38113 RVA: 0x001EB948 File Offset: 0x001E9B48
		public static TypeValue Field(TypeValue type, string fieldName)
		{
			return RecordTypeAlgebra.FieldOrDefault(type, fieldName, TypeValue.None);
		}

		// Token: 0x060094E2 RID: 38114 RVA: 0x001EB958 File Offset: 0x001E9B58
		public static TypeValue FieldOrDefault(TypeValue type, string fieldName, TypeValue defaultType)
		{
			if (type.TypeKind != ValueKind.Record && type.TypeKind != ValueKind.Any)
			{
				return TypeValue.None;
			}
			RecordTypeValue recordTypeValue = ((type.TypeKind == ValueKind.Record) ? type.AsRecordType : TypeValue.Record);
			Value value;
			if (recordTypeValue.Fields.TryGetValue(fieldName, out value))
			{
				return RecordTypeAlgebra.FieldReferenceType(value);
			}
			if (recordTypeValue.Open)
			{
				return TypeValue.Any;
			}
			return defaultType;
		}

		// Token: 0x060094E3 RID: 38115 RVA: 0x001EB9BC File Offset: 0x001E9BBC
		public static MissingFieldMode GetMissingFieldMode(Value missingField)
		{
			if (!missingField.IsNull)
			{
				return Library.MissingField.Type.GetValue(missingField.AsNumber);
			}
			return MissingFieldMode.Error;
		}

		// Token: 0x060094E4 RID: 38116 RVA: 0x001EB9D8 File Offset: 0x001E9BD8
		public static RecordValue NewField(TypeValue type, bool optional)
		{
			return RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				type,
				LogicalValue.New(optional)
			});
		}

		// Token: 0x060094E5 RID: 38117 RVA: 0x001EB9F8 File Offset: 0x001E9BF8
		public static bool IsClosedRecordWithRequiredFieldsOnly(TypeValue type)
		{
			if (type.TypeKind == ValueKind.Record)
			{
				RecordTypeValue asRecordType = type.AsRecordType;
				bool flag = !asRecordType.Open;
				int num = 0;
				while (flag && num < asRecordType.Fields.Count)
				{
					flag = !RecordTypeAlgebra.IsOptional(asRecordType.Fields[num]);
					num++;
				}
				return flag;
			}
			return false;
		}

		// Token: 0x060094E6 RID: 38118 RVA: 0x001EBA51 File Offset: 0x001E9C51
		public static bool IsOptional(Value field)
		{
			return field.AsRecord["Optional"].AsBoolean;
		}

		// Token: 0x060094E7 RID: 38119 RVA: 0x001EBA68 File Offset: 0x001E9C68
		public static bool IsRequiredField(RecordTypeValue recordType, string fieldName)
		{
			Value value;
			return !recordType.IsNullable && recordType.Fields.TryGetValue(fieldName, out value) && !RecordTypeAlgebra.IsOptional(value);
		}

		// Token: 0x060094E8 RID: 38120 RVA: 0x001EBA98 File Offset: 0x001E9C98
		public static TypeValue FieldReferenceType(Value field)
		{
			TypeValue typeValue = RecordTypeAlgebra.FieldType(field);
			if (RecordTypeAlgebra.IsOptional(field) && !typeValue.IsNullable)
			{
				typeValue = typeValue.Nullable;
			}
			return typeValue;
		}

		// Token: 0x060094E9 RID: 38121 RVA: 0x001EBAC4 File Offset: 0x001E9CC4
		public static TypeValue FieldType(Value field)
		{
			return field.AsRecord["Type"].AsType;
		}

		// Token: 0x060094EA RID: 38122 RVA: 0x001EBADC File Offset: 0x001E9CDC
		public static ListValue ComputeFieldOrder(Keys fields, ListValue orderSpec)
		{
			ListValue asList = Library.List.Buffer.Invoke(orderSpec).AsList;
			List<string> list = new List<string>();
			int i = 0;
			for (int j = 0; j < fields.Length; j++)
			{
				string text = fields[j];
				if (RecordTypeAlgebra.IndexOfString(asList, text) != -1)
				{
					list.Add(asList[i].AsString);
					i++;
				}
				else
				{
					list.Add(text);
				}
			}
			while (i < asList.Count)
			{
				list.Add(asList[i].AsString);
				i++;
			}
			return ListValue.New(list.ToArray());
		}

		// Token: 0x060094EB RID: 38123 RVA: 0x001EBB74 File Offset: 0x001E9D74
		private static int IndexOfString(ListValue list, string text)
		{
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].AsString == text)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04004F2A RID: 20266
		public static readonly RecordValue DefaultFieldRecord = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
		{
			TypeValue.Any,
			LogicalValue.False
		});

		// Token: 0x04004F2B RID: 20267
		public static readonly RecordValue NoFieldRecord = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
		{
			TypeValue.None,
			LogicalValue.False
		});

		// Token: 0x04004F2C RID: 20268
		public static readonly RecordValue NullFieldRecord = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
		{
			TypeValue.Null,
			LogicalValue.False
		});

		// Token: 0x04004F2D RID: 20269
		public static readonly RecordValue OptionalNoFieldRecord = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
		{
			TypeValue.None,
			LogicalValue.True
		});

		// Token: 0x04004F2E RID: 20270
		public static readonly RecordValue OptionalFieldRecord = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
		{
			TypeValue.Any,
			LogicalValue.True
		});

		// Token: 0x020016E3 RID: 5859
		public static class RenamePart
		{
			// Token: 0x04004F2F RID: 20271
			public const int OldName = 0;

			// Token: 0x04004F30 RID: 20272
			public const int NewName = 1;
		}

		// Token: 0x020016E4 RID: 5860
		public static class TransformPart
		{
			// Token: 0x04004F31 RID: 20273
			public const int Name = 0;

			// Token: 0x04004F32 RID: 20274
			public const int Transform = 1;
		}
	}
}
