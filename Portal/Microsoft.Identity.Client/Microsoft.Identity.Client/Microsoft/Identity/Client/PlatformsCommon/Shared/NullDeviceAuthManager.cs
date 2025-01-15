using System;
using System.Net.Http.Headers;
using Microsoft.Identity.Client.PlatformsCommon.Interfaces;

namespace Microsoft.Identity.Client.PlatformsCommon.Shared
{
	// Token: 0x020001F6 RID: 502
	internal class NullDeviceAuthManager : IDeviceAuthManager
	{
		// Token: 0x06001581 RID: 5505 RVA: 0x00047C59 File Offset: 0x00045E59
		public bool TryCreateDeviceAuthChallengeResponse(HttpResponseHeaders headers, Uri endpointUri, out string responseHeader)
		{
			if (!DeviceAuthHelper.IsDeviceAuthChallenge(headers))
			{
				responseHeader = string.Empty;
				return false;
			}
			responseHeader = DeviceAuthHelper.GetBypassChallengeResponse(headers);
			return true;
		}
	}
}
