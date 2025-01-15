using System;
using Microsoft.AnalysisServices.Tabular.Json.Linq;

namespace Microsoft.AnalysisServices.Tabular.Extensions
{
	// Token: 0x020001C1 RID: 449
	internal static class JObjectExtensions
	{
		// Token: 0x06001BC9 RID: 7113 RVA: 0x000C2F04 File Offset: 0x000C1104
		internal static bool ContainsProperty(this JObject jsonObject, string propName)
		{
			JToken jtoken = null;
			return jsonObject.TryGetValue(propName, ref jtoken);
		}
	}
}
