using System;
using Microsoft.Identity.Client.OAuth2;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Instance.Discovery
{
	// Token: 0x02000281 RID: 641
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal sealed class InstanceDiscoveryResponse : OAuth2ResponseBase
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x00051B88 File Offset: 0x0004FD88
		// (set) Token: 0x060018CB RID: 6347 RVA: 0x00051B90 File Offset: 0x0004FD90
		[JsonProperty("tenant_discovery_endpoint")]
		public string TenantDiscoveryEndpoint { get; set; }

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x00051B99 File Offset: 0x0004FD99
		// (set) Token: 0x060018CD RID: 6349 RVA: 0x00051BA1 File Offset: 0x0004FDA1
		[JsonProperty("metadata")]
		public InstanceDiscoveryMetadataEntry[] Metadata { get; set; }
	}
}
