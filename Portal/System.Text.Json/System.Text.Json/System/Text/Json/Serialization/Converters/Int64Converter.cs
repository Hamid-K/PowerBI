using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000FC RID: 252
	internal sealed class Int64Converter : JsonPrimitiveConverter<long>
	{
		// Token: 0x06000CCB RID: 3275 RVA: 0x00032A2A File Offset: 0x00030C2A
		public Int64Converter()
		{
			base.IsInternalConverterForNumberType = true;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00032A39 File Offset: 0x00030C39
		public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetInt64();
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00032A41 File Offset: 0x00030C41
		public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue(value);
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x00032A4A File Offset: 0x00030C4A
		internal override long ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetInt64WithQuotes();
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00032A52 File Offset: 0x00030C52
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, long value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName(value);
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00032A5B File Offset: 0x00030C5B
		internal override long ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String && (JsonNumberHandling.AllowReadingFromString & handling) != JsonNumberHandling.Strict)
			{
				return reader.GetInt64WithQuotes();
			}
			return reader.GetInt64();
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00032A78 File Offset: 0x00030C78
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, long value, JsonNumberHandling handling)
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
