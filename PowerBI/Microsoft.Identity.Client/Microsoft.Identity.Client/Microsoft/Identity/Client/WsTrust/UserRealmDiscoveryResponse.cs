using System;
using Microsoft.Identity.Json;

namespace Microsoft.Identity.Client.WsTrust
{
	// Token: 0x020001BB RID: 443
	[JsonObject]
	[Preserve(AllMembers = true)]
	internal sealed class UserRealmDiscoveryResponse
	{
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0004304E File Offset: 0x0004124E
		// (set) Token: 0x060013C7 RID: 5063 RVA: 0x00043056 File Offset: 0x00041256
		[JsonProperty("ver")]
		public string Version { get; set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x0004305F File Offset: 0x0004125F
		// (set) Token: 0x060013C9 RID: 5065 RVA: 0x00043067 File Offset: 0x00041267
		[JsonProperty("account_type")]
		public string AccountType { get; set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x00043070 File Offset: 0x00041270
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x00043078 File Offset: 0x00041278
		[JsonProperty("federation_protocol")]
		public string FederationProtocol { get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00043081 File Offset: 0x00041281
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x00043089 File Offset: 0x00041289
		[JsonProperty("federation_metadata_url")]
		public string FederationMetadataUrl { get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x00043092 File Offset: 0x00041292
		// (set) Token: 0x060013CF RID: 5071 RVA: 0x0004309A File Offset: 0x0004129A
		[JsonProperty("federation_active_auth_url")]
		public string FederationActiveAuthUrl { get; set; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x000430A3 File Offset: 0x000412A3
		// (set) Token: 0x060013D1 RID: 5073 RVA: 0x000430AB File Offset: 0x000412AB
		[JsonProperty("cloud_audience_urn")]
		public string CloudAudienceUrn { get; set; }

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x000430B4 File Offset: 0x000412B4
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x000430BC File Offset: 0x000412BC
		[JsonProperty("domain_name")]
		public string DomainName { get; set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x000430C5 File Offset: 0x000412C5
		public bool IsFederated
		{
			get
			{
				return string.Equals(this.AccountType, "federated", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x000430D8 File Offset: 0x000412D8
		public bool IsManaged
		{
			get
			{
				return string.Equals(this.AccountType, "managed", StringComparison.OrdinalIgnoreCase);
			}
		}
	}
}
