using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.PlatformsCommon.Factories;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x02000239 RID: 569
	internal static class MsalIdHelper
	{
		// Token: 0x0600171E RID: 5918 RVA: 0x0004C568 File Offset: 0x0004A768
		public static IDictionary<string, string> GetMsalIdParameters(ILoggerAdapter logger)
		{
			IPlatformProxy platformProxy = PlatformProxyFactory.CreatePlatformProxy(logger);
			if (platformProxy == null)
			{
				throw new MsalClientException("platform_not_supported", "Platform Not Supported");
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["x-client-SKU"] = platformProxy.GetProductName();
			dictionary["x-client-Ver"] = MsalIdHelper.GetMsalVersion();
			Dictionary<string, string> dictionary2 = dictionary;
			string operatingSystem = platformProxy.GetOperatingSystem();
			if (operatingSystem != null)
			{
				dictionary2["x-client-OS"] = operatingSystem;
			}
			string deviceModel = platformProxy.GetDeviceModel();
			if (deviceModel != null)
			{
				dictionary2["x-client-DM"] = deviceModel;
			}
			return dictionary2;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0004C5E3 File Offset: 0x0004A7E3
		public static string GetMsalVersion()
		{
			return MsalIdHelper.s_msalVersion.Value;
		}

		// Token: 0x04000A15 RID: 2581
		private static readonly Lazy<string> s_msalVersion = new Lazy<string>(delegate
		{
			string fullName = typeof(MsalIdHelper).Assembly.FullName;
			Match match = new Regex("Version=[\\d]+.[\\d+]+.[\\d]+.[\\d]+").Match(fullName);
			if (!match.Success)
			{
				return null;
			}
			return match.Groups[0].Value.Split(new char[] { '=' }, StringSplitOptions.None)[1];
		});
	}
}
