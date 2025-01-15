using System;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000115 RID: 277
	public interface IAuditingPrincipal
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000777 RID: 1911
		string UserPrincipalName { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000778 RID: 1912
		string PUID { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000779 RID: 1913
		string AadAppId { get; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600077A RID: 1914
		string TenantObjectId { get; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600077B RID: 1915
		string TenantName { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600077C RID: 1916
		bool IsTenantAdmin { get; }
	}
}
