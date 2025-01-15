using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Instance.Discovery;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.Region
{
	// Token: 0x02000263 RID: 611
	internal interface IRegionDiscoveryProvider
	{
		// Token: 0x06001847 RID: 6215
		Task<InstanceDiscoveryMetadataEntry> GetMetadataAsync(Uri authority, RequestContext requestContext);
	}
}
