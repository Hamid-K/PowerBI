using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses
{
	// Token: 0x02000435 RID: 1077
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.AutoReconfigure)]
	[Serializable]
	public sealed class GlobalEndpointConfiguration : ConfigurationClass
	{
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x0007CCD8 File Offset: 0x0007AED8
		// (set) Token: 0x06002135 RID: 8501 RVA: 0x0007CCE0 File Offset: 0x0007AEE0
		[ConfigurationProperty]
		public string AzurePrivateServiceEndpointSuffix { get; set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x0007CCE9 File Offset: 0x0007AEE9
		[NonConfigurationProperty]
		public string ManagementApiVersion
		{
			get
			{
				return "2014-04-01";
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x0007CCF0 File Offset: 0x0007AEF0
		// (set) Token: 0x06002138 RID: 8504 RVA: 0x0007CCF8 File Offset: 0x0007AEF8
		[ConfigurationProperty]
		public string AzurePublicServiceEndpointSuffix { get; set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06002139 RID: 8505 RVA: 0x0007CD01 File Offset: 0x0007AF01
		// (set) Token: 0x0600213A RID: 8506 RVA: 0x0007CD09 File Offset: 0x0007AF09
		[ConfigurationProperty]
		public string AzureVirtualMachineScaleSetEndpointSuffix { get; set; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x0007CD12 File Offset: 0x0007AF12
		// (set) Token: 0x0600213C RID: 8508 RVA: 0x0007CD1A File Offset: 0x0007AF1A
		[ConfigurationProperty]
		public string AzureAPIMServiceEndpointSuffix { get; set; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x0007CD23 File Offset: 0x0007AF23
		// (set) Token: 0x0600213E RID: 8510 RVA: 0x0007CD2B File Offset: 0x0007AF2B
		[ConfigurationProperty]
		public AzureCloudEnvironmentType AzureCloudEnvironmentName { get; set; }

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x0007CD34 File Offset: 0x0007AF34
		// (set) Token: 0x06002140 RID: 8512 RVA: 0x0007CD3C File Offset: 0x0007AF3C
		[ConfigurationProperty]
		public string Office365LoginDomain { get; set; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x0007CD45 File Offset: 0x0007AF45
		// (set) Token: 0x06002142 RID: 8514 RVA: 0x0007CD4D File Offset: 0x0007AF4D
		[ConfigurationProperty]
		public string AzureTrafficManagerEndpointSuffix { get; set; }

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06002143 RID: 8515 RVA: 0x0007CD56 File Offset: 0x0007AF56
		// (set) Token: 0x06002144 RID: 8516 RVA: 0x0007CD5E File Offset: 0x0007AF5E
		[ConfigurationProperty]
		public string AzureAuthenticationTenantID { get; set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06002145 RID: 8517 RVA: 0x0007CD67 File Offset: 0x0007AF67
		// (set) Token: 0x06002146 RID: 8518 RVA: 0x0007CD6F File Offset: 0x0007AF6F
		[ConfigurationProperty]
		public string ActiveDirectoryAuthority { get; set; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06002147 RID: 8519 RVA: 0x0007CD78 File Offset: 0x0007AF78
		// (set) Token: 0x06002148 RID: 8520 RVA: 0x0007CD80 File Offset: 0x0007AF80
		[ConfigurationProperty]
		public string AzureKeyVaultDnsSuffix { get; set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06002149 RID: 8521 RVA: 0x0007CD89 File Offset: 0x0007AF89
		// (set) Token: 0x0600214A RID: 8522 RVA: 0x0007CD91 File Offset: 0x0007AF91
		[ConfigurationProperty]
		public string ResourceManagerDnsSuffix { get; set; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x0007CD9A File Offset: 0x0007AF9A
		// (set) Token: 0x0600214C RID: 8524 RVA: 0x0007CDA2 File Offset: 0x0007AFA2
		[ConfigurationProperty]
		public string AzureCosmosDbAccountEndpoint { get; set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x0007CDAB File Offset: 0x0007AFAB
		// (set) Token: 0x0600214E RID: 8526 RVA: 0x0007CDB3 File Offset: 0x0007AFB3
		[ConfigurationProperty]
		public string PowerBIServicePlan0Id { get; set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x0007CDBC File Offset: 0x0007AFBC
		// (set) Token: 0x06002150 RID: 8528 RVA: 0x0007CDC4 File Offset: 0x0007AFC4
		[ConfigurationProperty]
		public string PowerBIServicePlan1Id { get; set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x0007CDCD File Offset: 0x0007AFCD
		// (set) Token: 0x06002152 RID: 8530 RVA: 0x0007CDD5 File Offset: 0x0007AFD5
		[ConfigurationProperty]
		public string PowerBIServicePlan2Id { get; set; }

		// Token: 0x04000B5C RID: 2908
		private const string m_ManagementApiVersion = "2014-04-01";
	}
}
