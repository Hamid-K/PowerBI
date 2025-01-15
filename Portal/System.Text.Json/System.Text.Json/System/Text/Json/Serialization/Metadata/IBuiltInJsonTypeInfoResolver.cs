using System;

namespace System.Text.Json.Serialization.Metadata
{
	// Token: 0x020000A2 RID: 162
	internal interface IBuiltInJsonTypeInfoResolver
	{
		// Token: 0x0600096C RID: 2412
		bool IsCompatibleWithOptions(JsonSerializerOptions options);
	}
}
