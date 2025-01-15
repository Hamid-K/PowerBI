using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004A5 RID: 1189
	public interface IEndpointProvider
	{
		// Token: 0x06002492 RID: 9362
		Uri GetPublishedEndpoint(EndpointInfo endpointInfo, string serviceName);
	}
}
