using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x0200003B RID: 59
	public class ClientAssertionCredentialOptions : TokenCredentialOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006853 File Offset: 0x00004A53
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000685B File Offset: 0x00004A5B
		internal CredentialPipeline Pipeline { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00006864 File Offset: 0x00004A64
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000686C File Offset: 0x00004A6C
		internal MsalConfidentialClient MsalClient { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00006875 File Offset: 0x00004A75
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000687D File Offset: 0x00004A7D
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00006886 File Offset: 0x00004A86
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000688E File Offset: 0x00004A8E
		public bool DisableInstanceDiscovery { get; set; }
	}
}
