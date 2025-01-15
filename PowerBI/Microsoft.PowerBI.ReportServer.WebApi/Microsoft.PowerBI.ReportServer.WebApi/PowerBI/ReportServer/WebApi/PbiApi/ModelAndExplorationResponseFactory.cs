using System;
using Microsoft.PowerBI.ReportServer.WebApi.FeatureSwitches;
using Newtonsoft.Json.Linq;

namespace Microsoft.PowerBI.ReportServer.WebApi.PbiApi
{
	// Token: 0x02000029 RID: 41
	public sealed class ModelAndExplorationResponseFactory
	{
		// Token: 0x0600009C RID: 156 RVA: 0x000035F8 File Offset: 0x000017F8
		internal static JObject CreateExploration(IPowerBIConfiguration powerBIConfiguration, string modelId, string reportDocument, string reportMobileState, bool containsCustomVisuals)
		{
			Permissions permissions = (powerBIConfiguration.ExportUnderlyingDataEnabled ? Permissions.ReadWriteReshareExplore : Permissions.Read);
			JObject jobject = JObject.Parse(reportDocument);
			JObject jobject2 = ((reportMobileState == null) ? null : JObject.Parse(reportMobileState));
			if (jobject["id"] == null || jobject["id"].Value<int>() == 0)
			{
				jobject["id"] = modelId;
			}
			return JObject.FromObject(new
			{
				models = new global::<>f__AnonymousType4<string, string, string>[]
				{
					new
					{
						id = modelId,
						name = "PowerBICompanionModelResource",
						permissions = permissions.ToString("d")
					}
				},
				exploration = jobject,
				mobileState = jobject2,
				featureSwitches = PbiFeatureSwitches.ClientSwitches,
				hasCustomVisuals = containsCustomVisuals
			});
		}
	}
}
