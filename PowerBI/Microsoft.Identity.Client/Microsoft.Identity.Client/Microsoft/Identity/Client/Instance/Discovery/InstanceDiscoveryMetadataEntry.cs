using System;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Instance.Discovery
{
	// Token: 0x02000280 RID: 640
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal sealed class InstanceDiscoveryMetadataEntry
	{
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00051B4D File Offset: 0x0004FD4D
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x00051B55 File Offset: 0x0004FD55
		[JsonProperty("preferred_network")]
		public string PreferredNetwork { get; set; }

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x00051B5E File Offset: 0x0004FD5E
		// (set) Token: 0x060018C6 RID: 6342 RVA: 0x00051B66 File Offset: 0x0004FD66
		[JsonProperty("preferred_cache")]
		public string PreferredCache { get; set; }

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x00051B6F File Offset: 0x0004FD6F
		// (set) Token: 0x060018C8 RID: 6344 RVA: 0x00051B77 File Offset: 0x0004FD77
		[JsonProperty("aliases")]
		public string[] Aliases { get; set; }
	}
}
