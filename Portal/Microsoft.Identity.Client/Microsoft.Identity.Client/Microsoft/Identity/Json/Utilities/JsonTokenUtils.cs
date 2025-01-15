using System;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000060 RID: 96
	internal static class JsonTokenUtils
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x000171F2 File Offset: 0x000153F2
		internal static bool IsEndToken(JsonToken token)
		{
			return token - JsonToken.EndObject <= 2;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000171FE File Offset: 0x000153FE
		internal static bool IsStartToken(JsonToken token)
		{
			return token - JsonToken.StartObject <= 2;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00017209 File Offset: 0x00015409
		internal static bool IsPrimitiveToken(JsonToken token)
		{
			return token - JsonToken.Integer <= 5 || token - JsonToken.Date <= 1;
		}
	}
}
