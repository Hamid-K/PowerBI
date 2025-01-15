using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000C1 RID: 193
	internal sealed class ReadOnlyMemoryByteConverter : JsonConverter<ReadOnlyMemory<byte>>
	{
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0002E034 File Offset: 0x0002C234
		public override bool HandleNull
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0002E037 File Offset: 0x0002C237
		public override ReadOnlyMemory<byte> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return (reader.TokenType == JsonTokenType.Null) ? null : reader.GetBytesFromBase64();
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0002E051 File Offset: 0x0002C251
		public override void Write(Utf8JsonWriter writer, ReadOnlyMemory<byte> value, JsonSerializerOptions options)
		{
			writer.WriteBase64StringValue(value.Span);
		}
	}
}
