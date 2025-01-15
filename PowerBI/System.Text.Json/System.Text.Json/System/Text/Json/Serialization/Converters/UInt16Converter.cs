using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x02000105 RID: 261
	internal sealed class UInt16Converter : JsonPrimitiveConverter<ushort>
	{
		// Token: 0x06000D01 RID: 3329 RVA: 0x00033025 File Offset: 0x00031225
		public UInt16Converter()
		{
			base.IsInternalConverterForNumberType = true;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x00033034 File Offset: 0x00031234
		public override ushort Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetUInt16();
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0003303C File Offset: 0x0003123C
		public override void Write(Utf8JsonWriter writer, ushort value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue((long)((ulong)value));
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00033046 File Offset: 0x00031246
		internal override ushort ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetUInt16WithQuotes();
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0003304E File Offset: 0x0003124E
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, ushort value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName((int)value);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00033057 File Offset: 0x00031257
		internal override ushort ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String && (JsonNumberHandling.AllowReadingFromString & handling) != JsonNumberHandling.Strict)
			{
				return reader.GetUInt16WithQuotes();
			}
			return reader.GetUInt16();
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00033074 File Offset: 0x00031274
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, ushort value, JsonNumberHandling handling)
		{
			if ((JsonNumberHandling.WriteAsString & handling) != JsonNumberHandling.Strict)
			{
				writer.WriteNumberValueAsString((long)((ulong)value));
				return;
			}
			writer.WriteNumberValue((long)((ulong)value));
		}
	}
}
