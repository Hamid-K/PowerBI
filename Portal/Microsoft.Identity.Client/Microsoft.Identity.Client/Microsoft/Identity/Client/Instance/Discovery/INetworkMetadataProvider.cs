using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.Instance.Discovery
{
	// Token: 0x0200027E RID: 638
	internal interface INetworkMetadataProvider
	{
		// Token: 0x060018BB RID: 6331
		Task<InstanceDiscoveryMetadataEntry> GetMetadataAsync(Uri authority, RequestContext requestContext);
	}
}
