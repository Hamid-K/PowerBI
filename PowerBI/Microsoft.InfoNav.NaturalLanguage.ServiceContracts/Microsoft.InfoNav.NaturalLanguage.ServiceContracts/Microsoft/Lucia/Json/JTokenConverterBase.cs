using System;
using Microsoft.InfoNav;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Json
{
	// Token: 0x02000029 RID: 41
	public abstract class JTokenConverterBase<TTarget, TToken> : JsonConverter where TToken : JToken
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003421 File Offset: 0x00001621
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003424 File Offset: 0x00001624
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw Contract.ExceptNotSupported();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000342B File Offset: 0x0000162B
		public override bool CanConvert(Type objectType)
		{
			return typeof(TTarget).IsAssignableFrom(objectType);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003440 File Offset: 0x00001640
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			TToken ttoken = (TToken)((object)JToken.Load(reader));
			return this.Create(objectType, ttoken, serializer);
		}

		// Token: 0x060000A7 RID: 167
		protected abstract TTarget Create(Type objectType, TToken token, JsonSerializer serializer);
	}
}
