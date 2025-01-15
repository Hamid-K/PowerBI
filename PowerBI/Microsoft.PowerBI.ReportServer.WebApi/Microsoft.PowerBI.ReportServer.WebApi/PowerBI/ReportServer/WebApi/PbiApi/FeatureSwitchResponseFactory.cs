using System;
using Microsoft.PowerBI.ReportServer.WebApi.FeatureSwitches;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x02000024 RID: 36
	public sealed class FeatureSwitchResponseFactory
	{
		// Token: 0x06000095 RID: 149 RVA: 0x000034F4 File Offset: 0x000016F4
		internal static JObject CreateFeatureSwitchResponse()
		{
			return JObject.FromObject(new
			{
				featureSwitches = PbiFeatureSwitches.ClientSwitches,
				serverSwitches = PbiFeatureSwitches.ServerSwitches
			});
		}
	}
}
