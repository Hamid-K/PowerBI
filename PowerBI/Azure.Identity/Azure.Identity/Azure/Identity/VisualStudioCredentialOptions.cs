using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x02000056 RID: 86
	public class VisualStudioCredentialOptions : TokenCredentialOptions, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00009E04 File Offset: 0x00008004
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00009E0C File Offset: 0x0000800C
		public string TenantId
		{
			get
			{
				return this._tenantId;
			}
			set
			{
				this._tenantId = Validations.ValidateTenantId(value, null, true);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00009E1C File Offset: 0x0000801C
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00009E24 File Offset: 0x00008024
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00009E2D File Offset: 0x0000802D
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00009E35 File Offset: 0x00008035
		public TimeSpan? ProcessTimeout { get; set; }

		// Token: 0x040001EB RID: 491
		private string _tenantId;
	}
}
