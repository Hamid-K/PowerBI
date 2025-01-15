using System;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.Instance.Oidc
{
	// Token: 0x02000279 RID: 633
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal class OidcMetadata
	{
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x00051893 File Offset: 0x0004FA93
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x0005189B File Offset: 0x0004FA9B
		[JsonProperty("token_endpoint")]
		public string TokenEndpoint { get; set; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x000518A4 File Offset: 0x0004FAA4
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x000518AC File Offset: 0x0004FAAC
		[JsonProperty("authorization_endpoint")]
		public string AuthorizationEndpoint { get; set; }
	}
}
