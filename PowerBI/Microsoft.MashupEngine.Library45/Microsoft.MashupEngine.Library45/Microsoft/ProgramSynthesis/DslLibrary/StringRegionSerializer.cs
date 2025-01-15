using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x02000806 RID: 2054
	public class StringRegionSerializer : JsonConverter
	{
		// Token: 0x06002BFA RID: 11258 RVA: 0x0007B620 File Offset: 0x00079820
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			StringRegion stringRegion = value as StringRegion;
			writer.WriteStartObject();
			writer.WritePropertyName("S");
			writer.WriteValue(stringRegion.Source);
			writer.WritePropertyName("Start");
			writer.WriteValue(stringRegion.Start);
			writer.WritePropertyName("End");
			writer.WriteValue(stringRegion.End);
			writer.WritePropertyName("Value");
			writer.WriteValue(stringRegion.Value);
			writer.WriteEndObject();
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x0007B69C File Offset: 0x0007989C
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JObject jobject = JObject.Load(reader);
			string text = jobject.Property("S").Value.Value<string>();
			uint num = jobject.Property("Start").Value.Value<uint>();
			uint num2 = jobject.Property("End").Value.Value<uint>();
			return new StringRegion(text, Token.Tokens).Slice(num, num2);
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x0007B702 File Offset: 0x00079902
		public override bool CanConvert(Type objectType)
		{
			return typeof(StringRegion).GetTypeInfo().IsAssignableFrom(objectType);
		}
	}
}
