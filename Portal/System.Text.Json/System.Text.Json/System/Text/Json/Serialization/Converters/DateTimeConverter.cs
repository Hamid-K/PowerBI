using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000F2 RID: 242
	internal sealed class DateTimeConverter : JsonPrimitiveConverter<DateTime>
	{
		// Token: 0x06000C8D RID: 3213 RVA: 0x0003200F File Offset: 0x0003020F
		public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetDateTime();
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00032017 File Offset: 0x00030217
		public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00032020 File Offset: 0x00030220
		internal override DateTime ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetDateTimeNoValidation();
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x00032028 File Offset: 0x00030228
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName(value);
		}
	}
}
