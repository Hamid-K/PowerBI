using System;
using System.Runtime.Serialization.Json;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000010 RID: 16
	internal static class JsonUtils
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000036DC File Offset: 0x000018DC
		internal static string ToJsonString(object o)
		{
			return new DataContractJsonSerializer(o.GetType()).ToJsonString(o);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000036EF File Offset: 0x000018EF
		internal static string ToJsonStringOrNull(object o)
		{
			if (o == null)
			{
				return null;
			}
			return JsonUtils.ToJsonString(o);
		}
	}
}
