using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000FA RID: 250
	internal sealed class Int16Converter : JsonPrimitiveConverter<short>
	{
		// Token: 0x06000CBD RID: 3261 RVA: 0x0003295C File Offset: 0x00030B5C
		public Int16Converter()
		{
			base.IsInternalConverterForNumberType = true;
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0003296B File Offset: 0x00030B6B
		public override short Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetInt16();
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00032973 File Offset: 0x00030B73
		public override void Write(Utf8JsonWriter writer, short value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue((long)value);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0003297D File Offset: 0x00030B7D
		internal override short ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetInt16WithQuotes();
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x00032985 File Offset: 0x00030B85
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, short value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName((int)value);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0003298E File Offset: 0x00030B8E
		internal override short ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String && (JsonNumberHandling.AllowReadingFromString & handling) != JsonNumberHandling.Strict)
			{
				return reader.GetInt16WithQuotes();
			}
			return reader.GetInt16();
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000329AB File Offset: 0x00030BAB
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, short value, JsonNumberHandling handling)
		{
			if ((JsonNumberHandling.WriteAsString & handling) != JsonNumberHandling.Strict)
			{
				writer.WriteNumberValueAsString((long)value);
				return;
			}
			writer.WriteNumberValue((long)value);
		}
	}
}
