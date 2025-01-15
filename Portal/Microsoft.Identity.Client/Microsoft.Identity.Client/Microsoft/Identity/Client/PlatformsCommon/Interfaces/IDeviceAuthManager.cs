using System;
using System.Net.Http.Headers;

namespace Microsoft.Identity.Client.PlatformsCommon.Interfaces
{
	// Token: 0x020001FC RID: 508
	internal interface IDeviceAuthManager
	{
		// Token: 0x06001592 RID: 5522
		bool TryCreateDeviceAuthChallengeResponse(HttpResponseHeaders headers, Uri endpointUri, out string responseHeader);
	}
}
