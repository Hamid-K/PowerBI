using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x02000028 RID: 40
	public sealed class CustomVisualsResponseFactory
	{
		// Token: 0x0600009A RID: 154 RVA: 0x000035EE File Offset: 0x000017EE
		internal static JObject Create(IDictionary<string, string> customVisuals)
		{
			return JObject.FromObject(customVisuals);
		}
	}
}
