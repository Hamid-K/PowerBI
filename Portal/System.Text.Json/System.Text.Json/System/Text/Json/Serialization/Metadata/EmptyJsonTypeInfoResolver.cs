using System;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000A1 RID: 161
	internal sealed class EmptyJsonTypeInfoResolver : IJsonTypeInfoResolver, IBuiltInJsonTypeInfoResolver
	{
		// Token: 0x06000969 RID: 2409 RVA: 0x000289B0 File Offset: 0x00026BB0
		public JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
		{
			return null;
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x000289B3 File Offset: 0x00026BB3
		public bool IsCompatibleWithOptions(JsonSerializerOptions _)
		{
			return true;
		}
	}
}
