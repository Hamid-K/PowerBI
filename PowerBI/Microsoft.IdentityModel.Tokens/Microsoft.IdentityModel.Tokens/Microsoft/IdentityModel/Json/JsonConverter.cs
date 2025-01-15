using System;
using System.Runtime.CompilerServices;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x0200001B RID: 27
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class JsonConverter
	{
		// Token: 0x06000082 RID: 130
		public abstract void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer);

		// Token: 0x06000083 RID: 131
		[return: Nullable(2)]
		public abstract object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer);

		// Token: 0x06000084 RID: 132
		public abstract bool CanConvert(Type objectType);

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002E7F File Offset: 0x0000107F
		public virtual bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002E82 File Offset: 0x00001082
		public virtual bool CanWrite
		{
			get
			{
				return true;
			}
		}
	}
}
