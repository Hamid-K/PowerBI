using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Json
{
	// Token: 0x0200002A RID: 42
	public abstract class TaggedUnionConverter<T, TKind> : JTokenConverterBase<T, JObject> where T : class where TKind : struct
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A9 RID: 169
		protected abstract string KindProperty { get; }

		// Token: 0x060000AA RID: 170
		protected abstract Type GetTypeForKind(TKind? kind);

		// Token: 0x060000AB RID: 171 RVA: 0x0000347C File Offset: 0x0000167C
		public sealed override bool CanConvert(Type objectType)
		{
			return objectType == typeof(T);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003490 File Offset: 0x00001690
		protected sealed override T Create(Type objectType, JObject obj, JsonSerializer serializer)
		{
			if (!this.CanConvert(objectType))
			{
				throw new InvalidOperationException("TaggedUnionConverter used on derived class (must use DefaultJsonConverter on derived classes).");
			}
			TKind? tkind = null;
			JToken jtoken;
			if (obj.TryGetValue(this.KindProperty, out jtoken))
			{
				JValue jvalue = jtoken as JValue;
				if (jvalue != null)
				{
					tkind = new TKind?((TKind)((object)Enum.ToObject(typeof(TKind), jvalue.Value)));
				}
			}
			Type typeForKind = this.GetTypeForKind(tkind);
			return (T)((object)serializer.Deserialize(obj.CreateReader(), typeForKind));
		}
	}
}
