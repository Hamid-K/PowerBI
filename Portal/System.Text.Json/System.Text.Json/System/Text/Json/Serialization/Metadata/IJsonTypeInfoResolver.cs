using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x0200009C RID: 156
	[NullableContext(1)]
	public interface IJsonTypeInfoResolver
	{
		// Token: 0x06000938 RID: 2360
		[return: Nullable(2)]
		JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options);
	}
}
