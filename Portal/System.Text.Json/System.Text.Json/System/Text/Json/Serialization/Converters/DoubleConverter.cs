using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000F5 RID: 245
	internal sealed class DoubleConverter : JsonPrimitiveConverter<double>
	{
		// Token: 0x06000C9E RID: 3230 RVA: 0x000320C7 File Offset: 0x000302C7
		public DoubleConverter()
		{
			base.IsInternalConverterForNumberType = true;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x000320D6 File Offset: 0x000302D6
		public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetDouble();
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000320DE File Offset: 0x000302DE
		public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue(value);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000320E7 File Offset: 0x000302E7
		internal override double ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetDoubleWithQuotes();
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000320EF File Offset: 0x000302EF
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, double value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName(value);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000320F8 File Offset: 0x000302F8
		internal override double ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				if ((JsonNumberHandling.AllowReadingFromString & handling) != JsonNumberHandling.Strict)
				{
					return reader.GetDoubleWithQuotes();
				}
				if ((JsonNumberHandling.AllowNamedFloatingPointLiterals & handling) != JsonNumberHandling.Strict)
				{
					return reader.GetDoubleFloatingPointConstant();
				}
			}
			return reader.GetDouble();
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00032121 File Offset: 0x00030321
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, double value, JsonNumberHandling handling)
		{
			if ((JsonNumberHandling.WriteAsString & handling) != JsonNumberHandling.Strict)
			{
				writer.WriteNumberValueAsString(value);
				return;
			}
			if ((JsonNumberHandling.AllowNamedFloatingPointLiterals & handling) != JsonNumberHandling.Strict)
			{
				writer.WriteFloatingPointConstant(value);
				return;
			}
			writer.WriteNumberValue(value);
		}
	}
}
