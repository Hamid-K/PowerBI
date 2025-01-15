using System;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.ManagedIdentity
{
	// Token: 0x02000222 RID: 546
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal class ManagedIdentityErrorResponse
	{
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x0004AF96 File Offset: 0x00049196
		// (set) Token: 0x06001683 RID: 5763 RVA: 0x0004AF9E File Offset: 0x0004919E
		[JsonProperty("message")]
		public string Message { get; set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x0004AFA7 File Offset: 0x000491A7
		// (set) Token: 0x06001685 RID: 5765 RVA: 0x0004AFAF File Offset: 0x000491AF
		[JsonProperty("correlationId")]
		public string CorrelationId { get; set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0004AFB8 File Offset: 0x000491B8
		// (set) Token: 0x06001687 RID: 5767 RVA: 0x0004AFC0 File Offset: 0x000491C0
		[JsonProperty("error")]
		public string Error { get; set; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x0004AFC9 File Offset: 0x000491C9
		// (set) Token: 0x06001689 RID: 5769 RVA: 0x0004AFD1 File Offset: 0x000491D1
		[JsonProperty("error_description")]
		public string ErrorDescription { get; set; }
	}
}
