using System;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.ManagedIdentity
{
	// Token: 0x02000224 RID: 548
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal class ManagedIdentityResponse
	{
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001691 RID: 5777 RVA: 0x0004B057 File Offset: 0x00049257
		// (set) Token: 0x06001692 RID: 5778 RVA: 0x0004B05F File Offset: 0x0004925F
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x0004B068 File Offset: 0x00049268
		// (set) Token: 0x06001694 RID: 5780 RVA: 0x0004B070 File Offset: 0x00049270
		[JsonProperty("expires_on")]
		public string ExpiresOn { get; set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x0004B079 File Offset: 0x00049279
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x0004B081 File Offset: 0x00049281
		[JsonProperty("resource")]
		public string Resource { get; set; }

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x0004B08A File Offset: 0x0004928A
		// (set) Token: 0x06001698 RID: 5784 RVA: 0x0004B092 File Offset: 0x00049292
		[JsonProperty("token_type")]
		public string TokenType { get; set; }

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0004B09B File Offset: 0x0004929B
		// (set) Token: 0x0600169A RID: 5786 RVA: 0x0004B0A3 File Offset: 0x000492A3
		[JsonProperty("client_id")]
		public string ClientId { get; set; }
	}
}
