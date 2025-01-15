using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000E4 RID: 228
	internal sealed class JsonObjectConverter : JsonConverter<JsonObject>
	{
		// Token: 0x06000C43 RID: 3139 RVA: 0x000303D4 File Offset: 0x0002E5D4
		internal override void ConfigureJsonTypeInfo(JsonTypeInfo jsonTypeInfo, JsonSerializerOptions options)
		{
			jsonTypeInfo.CreateObjectForExtensionDataProperty = () => new JsonObject(new JsonNodeOptions?(options.GetNodeOptions()));
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00030400 File Offset: 0x0002E600
		internal override void ReadElementAndSetProperty(object obj, string propertyName, ref Utf8JsonReader reader, JsonSerializerOptions options, [ScopedRef] ref ReadStack state)
		{
			JsonNode jsonNode;
			bool flag2;
			bool flag = JsonNodeConverter.Instance.TryRead(ref reader, typeof(JsonNode), options, ref state, out jsonNode, out flag2);
			JsonObject jsonObject = (JsonObject)obj;
			JsonNode jsonNode2 = jsonNode;
			jsonObject[propertyName] = jsonNode2;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0003043C File Offset: 0x0002E63C
		public override void Write(Utf8JsonWriter writer, JsonObject value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}
			value.WriteTo(writer, options);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00030450 File Offset: 0x0002E650
		public override JsonObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			JsonTokenType tokenType = reader.TokenType;
			if (tokenType == JsonTokenType.StartObject)
			{
				return JsonObjectConverter.ReadObject(ref reader, new JsonNodeOptions?(options.GetNodeOptions()));
			}
			if (tokenType != JsonTokenType.Null)
			{
				throw ThrowHelper.GetInvalidOperationException_ExpectedObject(reader.TokenType);
			}
			return null;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x00030490 File Offset: 0x0002E690
		public static JsonObject ReadObject(ref Utf8JsonReader reader, JsonNodeOptions? options)
		{
			JsonElement jsonElement = JsonElement.ParseValue(ref reader);
			return new JsonObject(jsonElement, options);
		}
	}
}
