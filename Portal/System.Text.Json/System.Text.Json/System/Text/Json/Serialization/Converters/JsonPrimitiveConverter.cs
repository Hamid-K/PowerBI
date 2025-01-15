using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000C3 RID: 195
	internal abstract class JsonPrimitiveConverter<T> : JsonConverter<T>
	{
		// Token: 0x06000BA3 RID: 2979 RVA: 0x0002E0A1 File Offset: 0x0002C2A1
		public sealed override void WriteAsPropertyName(Utf8JsonWriter writer, [DisallowNull] T value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException("value");
			}
			this.WriteAsPropertyNameCore(writer, value, options, false);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002E0BF File Offset: 0x0002C2BF
		public sealed override T ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType != JsonTokenType.PropertyName)
			{
				ThrowHelper.ThrowInvalidOperationException_ExpectedPropertyName(reader.TokenType);
			}
			return this.ReadAsPropertyNameCore(ref reader, typeToConvert, options);
		}
	}
}
