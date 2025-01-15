using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000F3 RID: 243
	internal sealed class DateTimeOffsetConverter : JsonPrimitiveConverter<DateTimeOffset>
	{
		// Token: 0x06000C92 RID: 3218 RVA: 0x00032039 File Offset: 0x00030239
		public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetDateTimeOffset();
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00032041 File Offset: 0x00030241
		public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003204A File Offset: 0x0003024A
		internal override DateTimeOffset ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetDateTimeOffsetNoValidation();
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x00032052 File Offset: 0x00030252
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName(value);
		}
	}
}
