using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000057 RID: 87
	[DataContract]
	public abstract class OperationDataContract
	{
		// Token: 0x02000072 RID: 114
		[NullableContext(1)]
		[Nullable(0)]
		protected sealed class TypedValueConverter : JsonConverter
		{
			// Token: 0x06000204 RID: 516 RVA: 0x00003364 File Offset: 0x00001564
			public override bool CanConvert(Type objectType)
			{
				return typeof(object).IsAssignableFrom(objectType);
			}

			// Token: 0x06000205 RID: 517 RVA: 0x00003378 File Offset: 0x00001578
			public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
			{
				JProperty[] array = JObject.Load(reader).Properties().ToArray<JProperty>();
				Type type = Type.GetType((string)array[1].Value);
				return Convert.ChangeType(array[0].Value, type);
			}

			// Token: 0x06000206 RID: 518 RVA: 0x000033B5 File Offset: 0x000015B5
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
}
