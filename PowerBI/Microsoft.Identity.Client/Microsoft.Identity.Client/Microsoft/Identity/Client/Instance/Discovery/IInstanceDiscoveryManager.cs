using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.Instance.Discovery
{
	// Token: 0x0200027B RID: 635
	internal interface IInstanceDiscoveryManager
	{
		// Token: 0x060018B5 RID: 6325
		Task<InstanceDiscoveryMetadataEntry> GetMetadataEntryTryAvoidNetworkAsync(AuthorityInfo authorityinfo, IEnumerable<string> existingEnvironmentsInCache, RequestContext requestContext);

		// Token: 0x060018B6 RID: 6326
		Task<InstanceDiscoveryMetadataEntry> GetMetadataEntryAsync(AuthorityInfo authorityinfo, RequestContext requestContext, bool forceValidation = false);
	}
}
