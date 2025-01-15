using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x02000101 RID: 257
	internal sealed class SByteConverter : JsonPrimitiveConverter<sbyte>
	{
		// Token: 0x06000CE8 RID: 3304 RVA: 0x00032D73 File Offset: 0x00030F73
		public SByteConverter()
		{
			base.IsInternalConverterForNumberType = true;
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x00032D82 File Offset: 0x00030F82
		public override sbyte Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetSByte();
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x00032D8A File Offset: 0x00030F8A
		public override void Write(Utf8JsonWriter writer, sbyte value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue((int)value);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00032D93 File Offset: 0x00030F93
		internal override sbyte ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetSByteWithQuotes();
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00032D9B File Offset: 0x00030F9B
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, sbyte value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName((int)value);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00032DA4 File Offset: 0x00030FA4
		internal override sbyte ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String && (JsonNumberHandling.AllowReadingFromString & handling) != JsonNumberHandling.Strict)
			{
				return reader.GetSByteWithQuotes();
			}
			return reader.GetSByte();
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x00032DC1 File Offset: 0x00030FC1
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, sbyte value, JsonNumberHandling handling)
		{
			if ((JsonNumberHandling.WriteAsString & handling) != JsonNumberHandling.Strict)
			{
				writer.WriteNumberValueAsString((long)value);
				return;
			}
			writer.WriteNumberValue((int)value);
		}
	}
}
