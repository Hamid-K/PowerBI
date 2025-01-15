using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000EF RID: 239
	internal sealed class ByteArrayConverter : JsonConverter<byte[]>
	{
		// Token: 0x06000C7E RID: 3198 RVA: 0x00031ECB File Offset: 0x000300CB
		public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Null)
			{
				return null;
			}
			return reader.GetBytesFromBase64();
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00031EDF File Offset: 0x000300DF
		public override void Write(Utf8JsonWriter writer, byte[] value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			writer.WriteBase64StringValue(value);
		}
	}
}
