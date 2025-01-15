using System;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000E8 RID: 232
	internal sealed class DefaultObjectConverter : ObjectConverter
	{
		// Token: 0x06000C55 RID: 3157 RVA: 0x000305C7 File Offset: 0x0002E7C7
		public DefaultObjectConverter()
		{
			base.RequiresReadAhead = true;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x000305D6 File Offset: 0x0002E7D6
		public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (options.UnknownTypeHandling == JsonUnknownTypeHandling.JsonElement)
			{
				return JsonElement.ParseValue(ref reader);
			}
			return JsonNodeConverter.Instance.Read(ref reader, typeToConvert, options);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000305FC File Offset: 0x0002E7FC
		internal override bool OnTryRead(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options, [ScopedRef] ref ReadStack state, out object value)
		{
			object obj;
			if (options.UnknownTypeHandling == JsonUnknownTypeHandling.JsonElement)
			{
				JsonElement jsonElement = JsonElement.ParseValue(ref reader);
				if (options.ReferenceHandlingStrategy == ReferenceHandlingStrategy.Preserve && JsonSerializer.TryHandleReferenceFromJsonElement(ref reader, ref state, jsonElement, out obj))
				{
					value = obj;
				}
				else
				{
					value = jsonElement;
				}
				return true;
			}
			JsonNode jsonNode = JsonNodeConverter.Instance.Read(ref reader, typeToConvert, options);
			if (options.ReferenceHandlingStrategy == ReferenceHandlingStrategy.Preserve && JsonSerializer.TryHandleReferenceFromJsonNode(ref reader, ref state, jsonNode, out obj))
			{
				value = obj;
			}
			else
			{
				value = jsonNode;
			}
			return true;
		}
	}
}
