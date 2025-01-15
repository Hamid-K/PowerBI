using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x0200010A RID: 266
	internal sealed class UriConverter : JsonPrimitiveConverter<Uri>
	{
		// Token: 0x06000D1E RID: 3358 RVA: 0x0003326C File Offset: 0x0003146C
		public override Uri Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.Null)
			{
				return UriConverter.ReadCore(ref reader);
			}
			return null;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00033280 File Offset: 0x00031480
		public override void Write(Utf8JsonWriter writer, Uri value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			writer.WriteStringValue(value.OriginalString);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00033298 File Offset: 0x00031498
		internal override Uri ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return UriConverter.ReadCore(ref reader);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000332A0 File Offset: 0x000314A0
		private static Uri ReadCore(ref Utf8JsonReader reader)
		{
			string @string = reader.GetString();
			Uri uri;
			if (!Uri.TryCreate(@string, UriKind.RelativeOrAbsolute, out uri))
			{
				ThrowHelper.ThrowJsonException(null);
			}
			return uri;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000332C6 File Offset: 0x000314C6
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, Uri value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException("value");
			}
			writer.WritePropertyName(value.OriginalString);
		}
	}
}
