using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000E5 RID: 229
	internal sealed class JsonValueConverter : JsonConverter<JsonValue>
	{
		// Token: 0x06000C49 RID: 3145 RVA: 0x000304B5 File Offset: 0x0002E6B5
		public override void Write(Utf8JsonWriter writer, JsonValue value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			value.WriteTo(writer, options);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000304CC File Offset: 0x0002E6CC
		public override JsonValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Null)
			{
				return null;
			}
			JsonElement jsonElement = JsonElement.ParseValue(ref reader);
			return new JsonValuePrimitive<JsonElement>(jsonElement, JsonMetadataServices.JsonElementConverter, new JsonNodeOptions?(options.GetNodeOptions()));
		}
	}
}
