using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000F9 RID: 249
	internal sealed class GuidConverter : JsonPrimitiveConverter<Guid>
	{
		// Token: 0x06000CB8 RID: 3256 RVA: 0x00032932 File Offset: 0x00030B32
		public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetGuid();
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0003293A File Offset: 0x00030B3A
		public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x00032943 File Offset: 0x00030B43
		internal override Guid ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetGuidNoValidation();
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0003294B File Offset: 0x00030B4B
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName(value);
		}
	}
}
