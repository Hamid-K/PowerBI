using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x02000035 RID: 53
	public class AzureDeveloperCliCredentialOptions : TokenCredentialOptions, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005D16 File Offset: 0x00003F16
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00005D1E File Offset: 0x00003F1E
		public string TenantId { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005D27 File Offset: 0x00003F27
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00005D2F File Offset: 0x00003F2F
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00005D38 File Offset: 0x00003F38
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00005D40 File Offset: 0x00003F40
		public TimeSpan? ProcessTimeout { get; set; }
	}
}
