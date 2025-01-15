using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000FD RID: 253
	internal sealed class JsonDocumentConverter : JsonConverter<JsonDocument>
	{
		// Token: 0x06000CD2 RID: 3282 RVA: 0x00032A8E File Offset: 0x00030C8E
		public override JsonDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return JsonDocument.ParseValue(ref reader);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00032A96 File Offset: 0x00030C96
		public override void Write(Utf8JsonWriter writer, JsonDocument value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			value.WriteTo(writer);
		}
	}
}
