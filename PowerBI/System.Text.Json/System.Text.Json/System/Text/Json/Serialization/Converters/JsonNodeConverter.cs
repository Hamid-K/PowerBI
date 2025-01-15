using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000E2 RID: 226
	internal sealed class JsonNodeConverter : JsonConverter<JsonNode>
	{
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00030227 File Offset: 0x0002E427
		public static JsonNodeConverter Instance
		{
			get
			{
				JsonNodeConverter jsonNodeConverter;
				if ((jsonNodeConverter = JsonNodeConverter.s_nodeConverter) == null)
				{
					jsonNodeConverter = (JsonNodeConverter.s_nodeConverter = new JsonNodeConverter());
				}
				return jsonNodeConverter;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0003023D File Offset: 0x0002E43D
		public static JsonArrayConverter ArrayConverter
		{
			get
			{
				JsonArrayConverter jsonArrayConverter;
				if ((jsonArrayConverter = JsonNodeConverter.s_arrayConverter) == null)
				{
					jsonArrayConverter = (JsonNodeConverter.s_arrayConverter = new JsonArrayConverter());
				}
				return jsonArrayConverter;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00030253 File Offset: 0x0002E453
		public static JsonObjectConverter ObjectConverter
		{
			get
			{
				JsonObjectConverter jsonObjectConverter;
				if ((jsonObjectConverter = JsonNodeConverter.s_objectConverter) == null)
				{
					jsonObjectConverter = (JsonNodeConverter.s_objectConverter = new JsonObjectConverter());
				}
				return jsonObjectConverter;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00030269 File Offset: 0x0002E469
		public static JsonValueConverter ValueConverter
		{
			get
			{
				JsonValueConverter jsonValueConverter;
				if ((jsonValueConverter = JsonNodeConverter.s_valueConverter) == null)
				{
					jsonValueConverter = (JsonNodeConverter.s_valueConverter = new JsonValueConverter());
				}
				return jsonValueConverter;
			}
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0003027F File Offset: 0x0002E47F
		public override void Write(Utf8JsonWriter writer, JsonNode value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			value.WriteTo(writer, options);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00030294 File Offset: 0x0002E494
		public override JsonNode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			switch (reader.TokenType)
			{
			case JsonTokenType.StartObject:
				return JsonNodeConverter.ObjectConverter.Read(ref reader, typeToConvert, options);
			case JsonTokenType.StartArray:
				return JsonNodeConverter.ArrayConverter.Read(ref reader, typeToConvert, options);
			case JsonTokenType.String:
			case JsonTokenType.Number:
			case JsonTokenType.True:
			case JsonTokenType.False:
				return JsonNodeConverter.ValueConverter.Read(ref reader, typeToConvert, options);
			case JsonTokenType.Null:
				return null;
			}
			throw new JsonException();
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00030310 File Offset: 0x0002E510
		public static JsonNode Create(JsonElement element, JsonNodeOptions? options)
		{
			JsonValueKind valueKind = element.ValueKind;
			JsonNode jsonNode;
			if (valueKind != JsonValueKind.Object)
			{
				if (valueKind != JsonValueKind.Array)
				{
					if (valueKind == JsonValueKind.Null)
					{
						jsonNode = null;
					}
					else
					{
						jsonNode = new JsonValuePrimitive<JsonElement>(element, JsonMetadataServices.JsonElementConverter, options);
					}
				}
				else
				{
					jsonNode = new JsonArray(element, options);
				}
			}
			else
			{
				jsonNode = new JsonObject(element, options);
			}
			return jsonNode;
		}

		// Token: 0x04000404 RID: 1028
		private static JsonNodeConverter s_nodeConverter;

		// Token: 0x04000405 RID: 1029
		private static JsonArrayConverter s_arrayConverter;

		// Token: 0x04000406 RID: 1030
		private static JsonObjectConverter s_objectConverter;

		// Token: 0x04000407 RID: 1031
		private static JsonValueConverter s_valueConverter;
	}
}
