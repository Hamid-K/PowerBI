using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x020000E1 RID: 225
	internal abstract class CustomCreationConverter<T> : JsonConverter
	{
		// Token: 0x06000C2D RID: 3117 RVA: 0x00030BE9 File Offset: 0x0002EDE9
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			throw new NotSupportedException("CustomCreationConverter should only be used while deserializing.");
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00030BF8 File Offset: 0x0002EDF8
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			T t = this.Create(objectType);
			if (t == null)
			{
				throw new JsonSerializationException("No object created.");
			}
			serializer.Populate(reader, t);
			return t;
		}

		// Token: 0x06000C2F RID: 3119
		public abstract T Create(Type objectType);

		// Token: 0x06000C30 RID: 3120 RVA: 0x00030C40 File Offset: 0x0002EE40
		public override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x00030C52 File Offset: 0x0002EE52
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
