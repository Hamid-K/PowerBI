using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x020000E2 RID: 226
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class CustomCreationConverter<[Nullable(2)] T> : JsonConverter
	{
		// Token: 0x06000C3A RID: 3130 RVA: 0x00031369 File Offset: 0x0002F569
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			throw new NotSupportedException("CustomCreationConverter should only be used while deserializing.");
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00031378 File Offset: 0x0002F578
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

		// Token: 0x06000C3C RID: 3132
		public abstract T Create(Type objectType);

		// Token: 0x06000C3D RID: 3133 RVA: 0x000313C0 File Offset: 0x0002F5C0
		public override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x000313D2 File Offset: 0x0002F5D2
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
