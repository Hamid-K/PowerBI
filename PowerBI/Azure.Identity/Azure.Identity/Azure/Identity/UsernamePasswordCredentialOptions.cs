using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x02000052 RID: 82
	public class UsernamePasswordCredentialOptions : TokenCredentialOptions, ISupportsTokenCachePersistenceOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000093AB File Offset: 0x000075AB
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x000093B3 File Offset: 0x000075B3
		public TokenCachePersistenceOptions TokenCachePersistenceOptions { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x000093BC File Offset: 0x000075BC
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x000093C4 File Offset: 0x000075C4
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002EA RID: 746 RVA: 0x000093CD File Offset: 0x000075CD
		// (set) Token: 0x060002EB RID: 747 RVA: 0x000093D5 File Offset: 0x000075D5
		public bool DisableInstanceDiscovery { get; set; }
	}
}
