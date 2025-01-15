using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000FB RID: 251
	internal sealed class Int32Converter : JsonPrimitiveConverter<int>
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x000329C3 File Offset: 0x00030BC3
		public Int32Converter()
		{
			base.IsInternalConverterForNumberType = true;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000329D2 File Offset: 0x00030BD2
		public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetInt32();
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000329DA File Offset: 0x00030BDA
		public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue((long)value);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000329E4 File Offset: 0x00030BE4
		internal override int ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetInt32WithQuotes();
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000329EC File Offset: 0x00030BEC
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, int value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName(value);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000329F5 File Offset: 0x00030BF5
		internal override int ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String && (JsonNumberHandling.AllowReadingFromString & handling) != JsonNumberHandling.Strict)
			{
				return reader.GetInt32WithQuotes();
			}
			return reader.GetInt32();
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x00032A12 File Offset: 0x00030C12
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, int value, JsonNumberHandling handling)
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
