using System;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000FE RID: 254
	internal sealed class JsonElementConverter : JsonConverter<JsonElement>
	{
		// Token: 0x06000CD5 RID: 3285 RVA: 0x00032AB1 File Offset: 0x00030CB1
		public override JsonElement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return JsonElement.ParseValue(ref reader);
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x00032AB9 File Offset: 0x00030CB9
		public override void Write(Utf8JsonWriter writer, JsonElement value, JsonSerializerOptions options)
		{
			value.WriteTo(writer);
		}
	}
}
