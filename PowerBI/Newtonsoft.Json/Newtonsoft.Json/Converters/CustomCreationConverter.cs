using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020000E2 RID: 226
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CustomCreationConverter<[Nullable(2)] T> : JsonConverter
	{
		// Token: 0x06000C44 RID: 3140 RVA: 0x000314B9 File Offset: 0x0002F6B9
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			throw new NotSupportedException("CustomCreationConverter should only be used while deserializing.");
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000314C8 File Offset: 0x0002F6C8
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

		// Token: 0x06000C46 RID: 3142
		public abstract T Create(Type objectType);

		// Token: 0x06000C47 RID: 3143 RVA: 0x00031510 File Offset: 0x0002F710
		public override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000C48 RID: 3144 RVA: 0x00031522 File Offset: 0x0002F722
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
