using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x0200002F RID: 47
	public class AuthorizationCodeCredentialOptions : TokenCredentialOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005279 File Offset: 0x00003479
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00005281 File Offset: 0x00003481
		public Uri RedirectUri { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000528A File Offset: 0x0000348A
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00005292 File Offset: 0x00003492
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000529B File Offset: 0x0000349B
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000052A3 File Offset: 0x000034A3
		public bool DisableInstanceDiscovery { get; set; }
	}
}
