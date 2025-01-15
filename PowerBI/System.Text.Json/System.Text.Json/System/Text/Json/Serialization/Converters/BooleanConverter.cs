using System;
using System.Buffers.Text;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000EE RID: 238
	internal sealed class BooleanConverter : JsonPrimitiveConverter<bool>
	{
		// Token: 0x06000C79 RID: 3193 RVA: 0x00031E75 File Offset: 0x00030075
		public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return reader.GetBoolean();
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00031E7D File Offset: 0x0003007D
		public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
		{
			writer.WriteBooleanValue(value);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00031E88 File Offset: 0x00030088
		internal override bool ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			ReadOnlySpan<byte> span = (ref reader).GetSpan();
			bool flag;
			int num;
			if (!Utf8Parser.TryParse(span, out flag, out num, '\0') || span.Length != num)
			{
				ThrowHelper.ThrowFormatException(DataType.Boolean);
			}
			return flag;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00031EBA File Offset: 0x000300BA
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, bool value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName(value);
		}
	}
}
