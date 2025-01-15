using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000F0 RID: 240
	internal sealed class ByteConverter : JsonPrimitiveConverter<byte>
	{
		// Token: 0x06000C81 RID: 3201 RVA: 0x00031EFF File Offset: 0x000300FF
		public ByteConverter()
		{
			base.IsInternalConverterForNumberType = true;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00031F0E File Offset: 0x0003010E
		public override byte Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetByte();
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00031F16 File Offset: 0x00030116
		public override void Write(Utf8JsonWriter writer, byte value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue((int)value);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00031F1F File Offset: 0x0003011F
		internal override byte ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetByteWithQuotes();
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00031F27 File Offset: 0x00030127
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, byte value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName((int)value);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00031F30 File Offset: 0x00030130
		internal override byte ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String && (JsonNumberHandling.AllowReadingFromString & handling) != JsonNumberHandling.Strict)
			{
				return reader.GetByteWithQuotes();
			}
			return reader.GetByte();
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00031F4D File Offset: 0x0003014D
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, byte value, JsonNumberHandling handling)
		{
			if ((JsonNumberHandling.WriteAsString & handling) != JsonNumberHandling.Strict)
			{
				writer.WriteNumberValueAsString((long)((ulong)value));
				return;
			}
			writer.WriteNumberValue((int)value);
		}
	}
}
