using System;
using System.Collections.Generic;
using Microsoft.Identity.Client.Kerberos;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200012F RID: 303
	public abstract class ApplicationOptions : BaseApplicationOptions
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00039209 File Offset: 0x00037409
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x00039211 File Offset: 0x00037411
		public string ClientId { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x0003921A File Offset: 0x0003741A
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x00039222 File Offset: 0x00037422
		public string TenantId { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0003922B File Offset: 0x0003742B
		// (set) Token: 0x06000F5C RID: 3932 RVA: 0x00039233 File Offset: 0x00037433
		public AadAuthorityAudience AadAuthorityAudience { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0003923C File Offset: 0x0003743C
		// (set) Token: 0x06000F5E RID: 3934 RVA: 0x00039244 File Offset: 0x00037444
		public string Instance { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x0003924D File Offset: 0x0003744D
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x00039255 File Offset: 0x00037455
		public AzureCloudInstance AzureCloudInstance { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0003925E File Offset: 0x0003745E
		// (set) Token: 0x06000F62 RID: 3938 RVA: 0x00039266 File Offset: 0x00037466
		public string RedirectUri { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0003926F File Offset: 0x0003746F
		// (set) Token: 0x06000F64 RID: 3940 RVA: 0x00039277 File Offset: 0x00037477
		public string ClientName { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x00039280 File Offset: 0x00037480
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x00039288 File Offset: 0x00037488
		public string ClientVersion { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00039291 File Offset: 0x00037491
		// (set) Token: 0x06000F68 RID: 3944 RVA: 0x00039299 File Offset: 0x00037499
		public IEnumerable<string> ClientCapabilities { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x000392A2 File Offset: 0x000374A2
		// (set) Token: 0x06000F6A RID: 3946 RVA: 0x000392AA File Offset: 0x000374AA
		public bool LegacyCacheCompatibilityEnabled { get; set; } = true;

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x000392B3 File Offset: 0x000374B3
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x000392BB File Offset: 0x000374BB
		public string KerberosServicePrincipalName { get; set; } = string.Empty;

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x000392C4 File Offset: 0x000374C4
		// (set) Token: 0x06000F6E RID: 3950 RVA: 0x000392CC File Offset: 0x000374CC
		public KerberosTicketContainer TicketContainer { get; set; }
	}
}
