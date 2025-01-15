using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common.Serialization
{
	// Token: 0x0200000C RID: 12
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TypedValueConverter : JsonConverter
	{
		// Token: 0x06000026 RID: 38 RVA: 0x000026CD File Offset: 0x000008CD
		public override bool CanConvert(Type objectType)
		{
			return typeof(object).IsAssignableFrom(objectType);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000026E0 File Offset: 0x000008E0
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JProperty[] array = JObject.Load(reader).Properties().ToArray<JProperty>();
			Type type = Type.GetType((string)array[1].Value);
			return Convert.ChangeType(array[0].Value, type);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000271D File Offset: 0x0000091D
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteStartObject();
			writer.WritePropertyName("Value");
			writer.WriteValue(value);
			writer.WritePropertyName("Type");
			writer.WriteValue(value.GetType().FullName);
			writer.WriteEndObject();
		}
	}
}
