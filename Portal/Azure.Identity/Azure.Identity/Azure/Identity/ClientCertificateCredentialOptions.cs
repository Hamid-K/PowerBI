using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x0200003D RID: 61
	public class ClientCertificateCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00006B5B File Offset: 0x00004D5B
		// (set) Token: 0x060001AE RID: 430 RVA: 0x00006B63 File Offset: 0x00004D63
		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00006B6C File Offset: 0x00004D6C
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00006B74 File Offset: 0x00004D74
		public bool SendCertificateChain { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00006B7D File Offset: 0x00004D7D
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x00006B85 File Offset: 0x00004D85
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00006B8E File Offset: 0x00004D8E
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00006B96 File Offset: 0x00004D96
		public bool DisableInstanceDiscovery { get; set; }
	}
}
