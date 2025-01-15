using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x02000102 RID: 258
	internal sealed class SingleConverter : JsonPrimitiveConverter<float>
	{
		// Token: 0x06000CEF RID: 3311 RVA: 0x00032DD8 File Offset: 0x00030FD8
		public SingleConverter()
		{
			base.IsInternalConverterForNumberType = true;
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x00032DE7 File Offset: 0x00030FE7
		public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetSingle();
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x00032DEF File Offset: 0x00030FEF
		public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue(value);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x00032DF8 File Offset: 0x00030FF8
		internal override float ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetSingleWithQuotes();
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x00032E00 File Offset: 0x00031000
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, float value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName(value);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x00032E09 File Offset: 0x00031009
		internal override float ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				if ((JsonNumberHandling.AllowReadingFromString & handling) != JsonNumberHandling.Strict)
				{
					return reader.GetSingleWithQuotes();
				}
				if ((JsonNumberHandling.AllowNamedFloatingPointLiterals & handling) != JsonNumberHandling.Strict)
				{
					return reader.GetSingleFloatingPointConstant();
				}
			}
			return reader.GetSingle();
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00032E32 File Offset: 0x00031032
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, float value, JsonNumberHandling handling)
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
