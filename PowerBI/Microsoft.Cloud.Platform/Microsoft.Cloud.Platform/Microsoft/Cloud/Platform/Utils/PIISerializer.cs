using System;
using Newtonsoft.Json;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001AD RID: 429
	public static class PIISerializer
	{
		// Token: 0x06000B1A RID: 2842 RVA: 0x00026C98 File Offset: 0x00024E98
		public static string SerializeObject(object value)
		{
			return JsonConvert.SerializeObject(value, PIISerializer.s_piiSerializerSettings);
		}

		// Token: 0x04000461 RID: 1121
		private static readonly JsonSerializerSettings s_piiSerializerSettings = new JsonSerializerSettings
		{
			ContractResolver = new PIIPropertyJsonResolver<PIIAttribute>()
		};
	}
}
