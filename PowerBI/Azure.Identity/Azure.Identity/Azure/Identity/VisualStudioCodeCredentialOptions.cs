using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x02000054 RID: 84
	public class VisualStudioCodeCredentialOptions : TokenCredentialOptions, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000097B6 File Offset: 0x000079B6
		// (set) Token: 0x060002FD RID: 765 RVA: 0x000097BE File Offset: 0x000079BE
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

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000097CE File Offset: 0x000079CE
		// (set) Token: 0x060002FF RID: 767 RVA: 0x000097D6 File Offset: 0x000079D6
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = new List<string>();

		// Token: 0x040001DC RID: 476
		private string _tenantId;
	}
}
