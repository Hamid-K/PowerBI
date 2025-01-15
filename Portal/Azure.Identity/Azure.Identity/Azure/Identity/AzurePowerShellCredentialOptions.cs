using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x02000037 RID: 55
	public class AzurePowerShellCredentialOptions : TokenCredentialOptions, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00006349 File Offset: 0x00004549
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00006351 File Offset: 0x00004551
		public string TenantId { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000635A File Offset: 0x0000455A
		// (set) Token: 0x06000177 RID: 375 RVA: 0x00006362 File Offset: 0x00004562
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000636B File Offset: 0x0000456B
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00006373 File Offset: 0x00004573
		public TimeSpan? ProcessTimeout { get; set; } = new TimeSpan?(TimeSpan.FromSeconds(10.0));
	}
}
