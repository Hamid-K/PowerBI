using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x02000106 RID: 262
	internal sealed class UInt32Converter : JsonPrimitiveConverter<uint>
	{
		// Token: 0x06000D08 RID: 3336 RVA: 0x0003308C File Offset: 0x0003128C
		public UInt32Converter()
		{
			base.IsInternalConverterForNumberType = true;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0003309B File Offset: 0x0003129B
		public override uint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetUInt32();
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000330A3 File Offset: 0x000312A3
		public override void Write(Utf8JsonWriter writer, uint value, JsonSerializerOptions options)
		{
			writer.WriteNumberValue((ulong)value);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000330AD File Offset: 0x000312AD
		internal override uint ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetUInt32WithQuotes();
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x000330B5 File Offset: 0x000312B5
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, uint value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName(value);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x000330BE File Offset: 0x000312BE
		internal override uint ReadNumberWithCustomHandling(ref Utf8JsonReader reader, JsonNumberHandling handling, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String && (JsonNumberHandling.AllowReadingFromString & handling) != JsonNumberHandling.Strict)
			{
				return reader.GetUInt32WithQuotes();
			}
			return reader.GetUInt32();
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x000330DB File Offset: 0x000312DB
		internal override void WriteNumberWithCustomHandling(Utf8JsonWriter writer, uint value, JsonNumberHandling handling)
		{
			if ((JsonNumberHandling.WriteAsString & handling) != JsonNumberHandling.Strict)
			{
				writer.WriteNumberValueAsString((long)((ulong)value));
				return;
			}
			writer.WriteNumberValue((ulong)value);
		}
	}
}
