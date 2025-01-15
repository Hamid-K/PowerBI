using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x0200004D RID: 77
	public class OnBehalfOfCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000889B File Offset: 0x00006A9B
		// (set) Token: 0x0600029F RID: 671 RVA: 0x000088A3 File Offset: 0x00006AA3
		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x000088AC File Offset: 0x00006AAC
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x000088B4 File Offset: 0x00006AB4
		public bool SendCertificateChain { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x000088BD File Offset: 0x00006ABD
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x000088C5 File Offset: 0x00006AC5
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x000088CE File Offset: 0x00006ACE
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x000088D6 File Offset: 0x00006AD6
		public bool DisableInstanceDiscovery { get; set; }
	}
}
