using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x0200003F RID: 63
	public class ClientSecretCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00006DE8 File Offset: 0x00004FE8
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00006DF0 File Offset: 0x00004FF0
		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00006DF9 File Offset: 0x00004FF9
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00006E01 File Offset: 0x00005001
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006E0A File Offset: 0x0000500A
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00006E12 File Offset: 0x00005012
		public bool DisableInstanceDiscovery { get; set; }
	}
}
