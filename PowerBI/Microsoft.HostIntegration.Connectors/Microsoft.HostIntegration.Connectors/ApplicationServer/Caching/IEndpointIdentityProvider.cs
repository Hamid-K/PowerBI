using System;
using System.ServiceModel;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002A3 RID: 675
	internal interface IEndpointIdentityProvider
	{
		// Token: 0x060018DD RID: 6365
		EndpointIdentity GetEndpointIdentity(string targetHost, int targetPort);
	}
}
