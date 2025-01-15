using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x0200010B RID: 267
	internal sealed class VersionConverter : JsonPrimitiveConverter<Version>
	{
		// Token: 0x06000D24 RID: 3364 RVA: 0x000332E9 File Offset: 0x000314E9
		public override Version Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Null)
			{
				return null;
			}
			if (reader.TokenType != JsonTokenType.String)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedString(reader.TokenType);
			}
			return VersionConverter.ReadCore(ref reader);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00033314 File Offset: 0x00031514
		private static Version ReadCore(ref Utf8JsonReader reader)
		{
			string @string = reader.GetString();
			if (!string.IsNullOrEmpty(@string) && (!char.IsDigit(@string[0]) || !char.IsDigit(@string[@string.Length - 1])))
			{
				ThrowHelper.ThrowFormatException(DataType.Version);
			}
			Version version;
			if (Version.TryParse(@string, out version))
			{
				return version;
			}
			ThrowHelper.ThrowJsonException(null);
			return null;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0003336C File Offset: 0x0003156C
		public override void Write(Utf8JsonWriter writer, Version value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			writer.WriteStringValue(value.ToString());
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00033384 File Offset: 0x00031584
		internal override Version ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return VersionConverter.ReadCore(ref reader);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0003338C File Offset: 0x0003158C
		internal override void WriteAsPropertyNameCore(Utf8JsonWriter writer, Version value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException("value");
			}
			writer.WritePropertyName(value.ToString());
		}
	}
}
