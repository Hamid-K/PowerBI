using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client.Utils;
using Microsoft.Identity.Json;
using Microsoft.Identity.Json.Linq;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x0200022D RID: 557
	internal static class ClaimsHelper
	{
		// Token: 0x060016D1 RID: 5841 RVA: 0x0004B6E4 File Offset: 0x000498E4
		internal static string GetMergedClaimsAndClientCapabilities(string claims, IEnumerable<string> clientCapabilities)
		{
			if (clientCapabilities != null && clientCapabilities.Any<string>())
			{
				JObject jobject = ClaimsHelper.CreateClientCapabilitiesRequestJson(clientCapabilities);
				return JsonHelper.JsonObjectToString(ClaimsHelper.MergeClaimsIntoCapabilityJson(claims, jobject));
			}
			return claims;
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x0004B714 File Offset: 0x00049914
		internal static JObject MergeClaimsIntoCapabilityJson(string claims, JObject capabilitiesJson)
		{
			if (!string.IsNullOrEmpty(claims))
			{
				JObject jobject;
				try
				{
					jobject = JsonHelper.ParseIntoJsonObject(claims);
				}
				catch (JsonException ex)
				{
					throw new MsalClientException("invalid_json_claims_format", MsalErrorMessage.InvalidJsonClaimsFormat(claims), ex);
				}
				capabilitiesJson.Merge(jobject, new JsonMergeSettings
				{
					MergeArrayHandling = MergeArrayHandling.Union
				});
			}
			return capabilitiesJson;
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x0004B76C File Offset: 0x0004996C
		private static JObject CreateClientCapabilitiesRequestJson(IEnumerable<string> clientCapabilities)
		{
			JObject jobject = new JObject();
			string text = "access_token";
			JObject jobject2 = new JObject();
			string text2 = "xms_cc";
			JObject jobject3 = new JObject();
			jobject3["values"] = new JArray(clientCapabilities);
			jobject2[text2] = jobject3;
			jobject[text] = jobject2;
			return jobject;
		}

		// Token: 0x040009BD RID: 2493
		private const string AccessTokenClaim = "access_token";

		// Token: 0x040009BE RID: 2494
		private const string XmsClientCapability = "xms_cc";
	}
}
