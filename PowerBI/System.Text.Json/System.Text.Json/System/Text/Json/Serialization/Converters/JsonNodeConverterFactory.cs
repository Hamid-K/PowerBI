using System;
using System.Text.Json.Nodes;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000E3 RID: 227
	internal sealed class JsonNodeConverterFactory : JsonConverterFactory
	{
		// Token: 0x06000C40 RID: 3136 RVA: 0x00030360 File Offset: 0x0002E560
		public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
		{
			if (typeof(JsonValue).IsAssignableFrom(typeToConvert))
			{
				return JsonNodeConverter.ValueConverter;
			}
			if (typeof(JsonObject) == typeToConvert)
			{
				return JsonNodeConverter.ObjectConverter;
			}
			if (typeof(JsonArray) == typeToConvert)
			{
				return JsonNodeConverter.ArrayConverter;
			}
			return JsonNodeConverter.Instance;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000303BA File Offset: 0x0002E5BA
		public override bool CanConvert(Type typeToConvert)
		{
			return typeof(JsonNode).IsAssignableFrom(typeToConvert);
		}
	}
}
