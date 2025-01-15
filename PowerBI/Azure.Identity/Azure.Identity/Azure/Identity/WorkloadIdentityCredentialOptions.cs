using System;
using System.Collections.Generic;

namespace Azure.Identity
{
	// Token: 0x02000058 RID: 88
	public class WorkloadIdentityCredentialOptions : TokenCredentialOptions, ISupportsDisableInstanceDiscovery, ISupportsAdditionallyAllowedTenants
	{
		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000A00B File Offset: 0x0000820B
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000A013 File Offset: 0x00008213
		public string TenantId { get; set; } = EnvironmentVariables.TenantId;

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000A01C File Offset: 0x0000821C
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000A024 File Offset: 0x00008224
		public string ClientId { get; set; } = EnvironmentVariables.ClientId;

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000A02D File Offset: 0x0000822D
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000A035 File Offset: 0x00008235
		public string TokenFilePath { get; set; } = EnvironmentVariables.AzureFederatedTokenFile;

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000A03E File Offset: 0x0000823E
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000A046 File Offset: 0x00008246
		public bool DisableInstanceDiscovery { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000A04F File Offset: 0x0000824F
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000A057 File Offset: 0x00008257
		public IList<string> AdditionallyAllowedTenants { get; internal set; } = EnvironmentVariables.AdditionallyAllowedTenants;

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000A060 File Offset: 0x00008260
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000A068 File Offset: 0x00008268
		internal CredentialPipeline Pipeline { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000A071 File Offset: 0x00008271
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000A079 File Offset: 0x00008279
		internal MsalConfidentialClient MsalClient { get; set; }
	}
}
