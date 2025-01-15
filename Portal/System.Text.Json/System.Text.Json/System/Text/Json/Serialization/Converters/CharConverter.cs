using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000F1 RID: 241
	internal sealed class CharConverter : JsonPrimitiveConverter<char>
	{
		// Token: 0x06000C88 RID: 3208 RVA: 0x00031F64 File Offset: 0x00030164
		public unsafe override char Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			JsonTokenType tokenType = reader.TokenType;
			if (tokenType != JsonTokenType.PropertyName && tokenType != JsonTokenType.String)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedString(reader.TokenType);
			}
			if (!JsonHelpers.IsInRangeInclusive(reader.ValueLength, 1, 6))
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedChar(reader.TokenType);
			}
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)12], 6);
			Span<char> span2 = span;
			int num = reader.CopyString(span2);
			if (num != 1)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedChar(reader.TokenType);
			}
			return *span2[0];
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x00031FDE File Offset: 0x000301DE
		public override void Write(Utf8JsonWriter writer, char value, JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString());
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00031FED File Offset: 0x000301ED
		internal override char ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return this.Read(ref reader, typeToConvert, options);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00031FF8 File Offset: 0x000301F8
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, char value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			writer.WritePropertyName(value.ToString());
		}

		// Token: 0x0400040A RID: 1034
		private const int MaxEscapedCharacterLength = 6;
	}
}
