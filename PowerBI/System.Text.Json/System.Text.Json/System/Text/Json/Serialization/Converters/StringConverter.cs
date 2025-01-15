using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x02000103 RID: 259
	internal sealed class StringConverter : JsonPrimitiveConverter<string>
	{
		// Token: 0x06000CF6 RID: 3318 RVA: 0x00032E55 File Offset: 0x00031055
		public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetString();
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00032E5D File Offset: 0x0003105D
		public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			writer.WriteStringValue(value.AsSpan());
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x00032E75 File Offset: 0x00031075
		internal override string ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetString();
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x00032E7D File Offset: 0x0003107D
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, string value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException("value");
			}
			if (options.DictionaryKeyPolicy != null && !isWritingExtensionDataProperty)
			{
				value = options.DictionaryKeyPolicy.ConvertName(value);
				if (value == null)
				{
					ThrowHelper.ThrowInvalidOperationException_NamingPolicyReturnNull(options.DictionaryKeyPolicy);
				}
			}
			writer.WritePropertyName(value);
		}
	}
}
