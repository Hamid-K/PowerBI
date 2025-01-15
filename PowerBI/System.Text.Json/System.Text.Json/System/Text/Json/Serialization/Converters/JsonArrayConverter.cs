using System;
using System.Text.Json.Nodes;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000E1 RID: 225
	internal sealed class JsonArrayConverter : JsonConverter<JsonArray>
	{
		// Token: 0x06000C34 RID: 3124 RVA: 0x000301B0 File Offset: 0x0002E3B0
		public override void Write(Utf8JsonWriter writer, JsonArray value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			value.WriteTo(writer, options);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x000301C4 File Offset: 0x0002E3C4
		public override JsonArray Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			JsonTokenType tokenType = reader.TokenType;
			if (tokenType == JsonTokenType.StartArray)
			{
				return JsonArrayConverter.ReadList(ref reader, new JsonNodeOptions?(options.GetNodeOptions()));
			}
			if (tokenType != JsonTokenType.Null)
			{
				throw ThrowHelper.GetInvalidOperationException_ExpectedArray(reader.TokenType);
			}
			return null;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00030204 File Offset: 0x0002E404
		public static JsonArray ReadList(ref Utf8JsonReader reader, JsonNodeOptions? options = null)
		{
			JsonElement jsonElement = JsonElement.ParseValue(ref reader);
			return new JsonArray(jsonElement, options);
		}
	}
}
