using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000C2 RID: 194
	internal sealed class MemoryByteConverter : JsonConverter<Memory<byte>>
	{
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0002E068 File Offset: 0x0002C268
		public override bool HandleNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002E06B File Offset: 0x0002C26B
		public override Memory<byte> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return (reader.TokenType == JsonTokenType.Null) ? null : reader.GetBytesFromBase64();
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0002E085 File Offset: 0x0002C285
		public override void Write(Utf8JsonWriter writer, Memory<byte> value, JsonSerializerOptions options)
		{
			writer.WriteBase64StringValue(value.Span);
		}
	}
}
