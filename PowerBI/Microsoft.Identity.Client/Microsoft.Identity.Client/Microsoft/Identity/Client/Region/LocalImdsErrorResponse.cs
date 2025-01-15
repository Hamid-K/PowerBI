using System;
using System.Collections.Generic;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Region
{
	// Token: 0x02000266 RID: 614
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal sealed class LocalImdsErrorResponse
	{
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x00050C6A File Offset: 0x0004EE6A
		// (set) Token: 0x0600184E RID: 6222 RVA: 0x00050C72 File Offset: 0x0004EE72
		[JsonProperty("error")]
		public string Error { get; set; }

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x00050C7B File Offset: 0x0004EE7B
		// (set) Token: 0x06001850 RID: 6224 RVA: 0x00050C83 File Offset: 0x0004EE83
		[JsonProperty("newest-versions")]
		public List<string> NewestVersions { get; set; }
	}
}
