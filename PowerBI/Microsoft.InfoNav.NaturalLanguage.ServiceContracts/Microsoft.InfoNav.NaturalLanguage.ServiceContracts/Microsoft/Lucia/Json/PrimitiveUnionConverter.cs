using System;
using Microsoft.InfoNav;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Json
{
	// Token: 0x0200002C RID: 44
	public sealed class PrimitiveUnionConverter<T1, T2> : JsonConverter
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x0000353F File Offset: 0x0000173F
		public override bool CanConvert(Type objectType)
		{
			return typeof(Union<T1, T2>).IsAssignableFrom(objectType);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003554 File Offset: 0x00001754
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			object obj = serializer.Deserialize(reader);
			if (obj == null)
			{
				return null;
			}
			if (obj is T1)
			{
				T1 t = (T1)((object)obj);
				return t;
			}
			if (obj is T2)
			{
				T2 t2 = (T2)((object)obj);
				return t2;
			}
			throw Contract.ExceptNotSupported("PrimitiveUnionConverter encountered unexpected value.");
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000035AC File Offset: 0x000017AC
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			Union<T1, T2> union = (Union<T1, T2>)value;
			writer.WriteValue(union.AsObject());
		}
	}
}
