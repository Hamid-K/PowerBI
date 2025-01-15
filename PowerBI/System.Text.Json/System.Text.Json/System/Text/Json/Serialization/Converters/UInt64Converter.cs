using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x02000107 RID: 263
	internal sealed class UInt64Converter : JsonPrimitiveConverter<ulong>
	{
		// Token: 0x06000D0F RID: 3343 RVA: 0x000330F3 File Offset: 0x000312F3
		public UInt64Converter()
		{
			base.IsInternalConverterForNumberType = true;
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x00033102 File Offset: 0x00031302
		public override ulong Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetUInt64();
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0003310A File Offset: 0x0003130A
		public override void Write(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue(value);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00033113 File Offset: 0x00031313
		internal override ulong ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetUInt64WithQuotes();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0003311B File Offset: 0x0003131B
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, ulong value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName(value);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00033124 File Offset: 0x00031324
		internal override ulong ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String && (JsonNumberHandling.AllowReadingFromString & handling) != JsonNumberHandling.Strict)
			{
				return reader.GetUInt64WithQuotes();
			}
			return reader.GetUInt64();
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00033141 File Offset: 0x00031341
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, ulong value, JsonNumberHandling handling)
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
