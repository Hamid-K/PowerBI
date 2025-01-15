using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000F4 RID: 244
	internal sealed class DecimalConverter : JsonPrimitiveConverter<decimal>
	{
		// Token: 0x06000C97 RID: 3223 RVA: 0x00032063 File Offset: 0x00030263
		public DecimalConverter()
		{
			base.IsInternalConverterForNumberType = true;
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x00032072 File Offset: 0x00030272
		public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetDecimal();
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0003207A File Offset: 0x0003027A
		public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue(value);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00032083 File Offset: 0x00030283
		internal override decimal ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetDecimalWithQuotes();
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0003208B File Offset: 0x0003028B
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName(value);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00032094 File Offset: 0x00030294
		internal override decimal ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String && (JsonNumberHandling.AllowReadingFromString & handling) != JsonNumberHandling.Strict)
			{
				return reader.GetDecimalWithQuotes();
			}
			return reader.GetDecimal();
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000320B1 File Offset: 0x000302B1
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, decimal value, JsonNumberHandling handling)
		{
			if ((JsonNumberHandling.WriteAsString & handling) != JsonNumberHandling.Strict)
			{
				writer.WriteNumberValueAsString(value);
				return;
			}
			writer.WriteNumberValue(value);
		}
	}
}
