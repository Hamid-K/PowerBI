using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x02000033 RID: 51
	public class AzureCliCredentialOptions : TokenCredentialOptions, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000058B4 File Offset: 0x00003AB4
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000058BC File Offset: 0x00003ABC
		public string TenantId { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000058C5 File Offset: 0x00003AC5
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000058CD File Offset: 0x00003ACD
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000058D6 File Offset: 0x00003AD6
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000058DE File Offset: 0x00003ADE
		public TimeSpan? ProcessTimeout { get; set; }
	}
}
