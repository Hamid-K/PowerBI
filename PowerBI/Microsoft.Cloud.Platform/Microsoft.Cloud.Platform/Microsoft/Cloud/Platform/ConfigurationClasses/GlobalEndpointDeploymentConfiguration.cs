using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.Cloud.Platform.ConfigurationClasses
{
	// Token: 0x02000437 RID: 1079
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.AutoReconfigure)]
	[Serializable]
	public sealed class GlobalEndpointDeploymentConfiguration : ConfigurationClass
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x0007CDDE File Offset: 0x0007AFDE
		// (set) Token: 0x06002155 RID: 8533 RVA: 0x0007CDE6 File Offset: 0x0007AFE6
		[ConfigurationProperty]
		public string AzurePrivateServiceEndpointSuffix { get; set; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x0007CDEF File Offset: 0x0007AFEF
		// (set) Token: 0x06002157 RID: 8535 RVA: 0x0007CDF7 File Offset: 0x0007AFF7
		[ConfigurationProperty]
		public string AzureManagementEndpoint { get; set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x0007CE00 File Offset: 0x0007B000
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x0007CE08 File Offset: 0x0007B008
		[ConfigurationProperty]
		public string AzureManagementEndpointWithPort { get; set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x0007CE11 File Offset: 0x0007B011
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x0007CE19 File Offset: 0x0007B019
		[ConfigurationProperty]
		public string AzureSqlRouterWithPort { get; set; }

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600215C RID: 8540 RVA: 0x0007CE22 File Offset: 0x0007B022
		// (set) Token: 0x0600215D RID: 8541 RVA: 0x0007CE2A File Offset: 0x0007B02A
		[ConfigurationProperty]
		public string AzureManagementApiVersion { get; set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600215E RID: 8542 RVA: 0x0007CE33 File Offset: 0x0007B033
		// (set) Token: 0x0600215F RID: 8543 RVA: 0x0007CE3B File Offset: 0x0007B03B
		[ConfigurationProperty]
		public string AzureCachingServiceEndpointUri { get; set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x0007CE44 File Offset: 0x0007B044
		// (set) Token: 0x06002161 RID: 8545 RVA: 0x0007CE4C File Offset: 0x0007B04C
		[ConfigurationProperty]
		public string AzureServiceBusEndpointUri { get; set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x0007CE55 File Offset: 0x0007B055
		// (set) Token: 0x06002163 RID: 8547 RVA: 0x0007CE5D File Offset: 0x0007B05D
		[ConfigurationProperty]
		public string AzureBlobEndpointUri { get; set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x0007CE66 File Offset: 0x0007B066
		// (set) Token: 0x06002165 RID: 8549 RVA: 0x0007CE6E File Offset: 0x0007B06E
		[ConfigurationProperty]
		public string AzureTableEndpointUri { get; set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x0007CE77 File Offset: 0x0007B077
		// (set) Token: 0x06002167 RID: 8551 RVA: 0x0007CE7F File Offset: 0x0007B07F
		[ConfigurationProperty]
		public string AzureQueueEndpointUri { get; set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x0007CE88 File Offset: 0x0007B088
		// (set) Token: 0x06002169 RID: 8553 RVA: 0x0007CE90 File Offset: 0x0007B090
		[ConfigurationProperty]
		public string AzureFileEndpointUri { get; set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x0007CE99 File Offset: 0x0007B099
		// (set) Token: 0x0600216B RID: 8555 RVA: 0x0007CEA1 File Offset: 0x0007B0A1
		[ConfigurationProperty]
		public string AzureDatabaseEndpoint { get; set; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600216C RID: 8556 RVA: 0x0007CEAA File Offset: 0x0007B0AA
		// (set) Token: 0x0600216D RID: 8557 RVA: 0x0007CEB2 File Offset: 0x0007B0B2
		[ConfigurationProperty]
		public string AzurePublicServiceEndpointSuffix { get; set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600216E RID: 8558 RVA: 0x0007CEBB File Offset: 0x0007B0BB
		// (set) Token: 0x0600216F RID: 8559 RVA: 0x0007CEC3 File Offset: 0x0007B0C3
		[ConfigurationProperty]
		public string AzureVirtualMachineScaleSetEndpointSuffix { get; set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x0007CECC File Offset: 0x0007B0CC
		// (set) Token: 0x06002171 RID: 8561 RVA: 0x0007CED4 File Offset: 0x0007B0D4
		[ConfigurationProperty]
		public string AzureAPIMServiceEndpointSuffix { get; set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06002172 RID: 8562 RVA: 0x0007CEDD File Offset: 0x0007B0DD
		// (set) Token: 0x06002173 RID: 8563 RVA: 0x0007CEE5 File Offset: 0x0007B0E5
		[ConfigurationProperty]
		public AzureCloudEnvironmentType AzureCloudEnvironmentName { get; set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06002174 RID: 8564 RVA: 0x0007CEEE File Offset: 0x0007B0EE
		// (set) Token: 0x06002175 RID: 8565 RVA: 0x0007CEF6 File Offset: 0x0007B0F6
		[ConfigurationProperty]
		public string Office365LoginDomainWithPort { get; set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x0007CEFF File Offset: 0x0007B0FF
		// (set) Token: 0x06002177 RID: 8567 RVA: 0x0007CF07 File Offset: 0x0007B107
		[ConfigurationProperty]
		public string AzureTrafficManagerEndpointSuffix { get; set; }

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x0007CF10 File Offset: 0x0007B110
		// (set) Token: 0x06002179 RID: 8569 RVA: 0x0007CF18 File Offset: 0x0007B118
		[ConfigurationProperty]
		public string AzureAuthenticationTenantID { get; set; }

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x0007CF21 File Offset: 0x0007B121
		// (set) Token: 0x0600217B RID: 8571 RVA: 0x0007CF29 File Offset: 0x0007B129
		[ConfigurationProperty]
		public string ActiveDirectoryAuthority { get; set; }

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x0007CF32 File Offset: 0x0007B132
		// (set) Token: 0x0600217D RID: 8573 RVA: 0x0007CF3A File Offset: 0x0007B13A
		[ConfigurationProperty]
		public string ActiveDirectoryAuthorityUrl { get; set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x0007CF43 File Offset: 0x0007B143
		// (set) Token: 0x0600217F RID: 8575 RVA: 0x0007CF4B File Offset: 0x0007B14B
		[ConfigurationProperty]
		public string GalleryUrl { get; set; }

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x0007CF54 File Offset: 0x0007B154
		// (set) Token: 0x06002181 RID: 8577 RVA: 0x0007CF5C File Offset: 0x0007B15C
		[ConfigurationProperty]
		public string GraphUrl { get; set; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06002182 RID: 8578 RVA: 0x0007CF65 File Offset: 0x0007B165
		// (set) Token: 0x06002183 RID: 8579 RVA: 0x0007CF6D File Offset: 0x0007B16D
		[ConfigurationProperty]
		public string ResourceManagerDnsSuffix { get; set; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x0007CF76 File Offset: 0x0007B176
		// (set) Token: 0x06002185 RID: 8581 RVA: 0x0007CF7E File Offset: 0x0007B17E
		[ConfigurationProperty]
		public string ResourceManagerUrl { get; set; }

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x0007CF87 File Offset: 0x0007B187
		// (set) Token: 0x06002187 RID: 8583 RVA: 0x0007CF8F File Offset: 0x0007B18F
		[ConfigurationProperty]
		public string AzureKeyVaultDnsSuffix { get; set; }

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06002188 RID: 8584 RVA: 0x0007CF98 File Offset: 0x0007B198
		// (set) Token: 0x06002189 RID: 8585 RVA: 0x0007CFA0 File Offset: 0x0007B1A0
		[ConfigurationProperty]
		public string AzureKeyVaultServiceEndpointResourceId { get; set; }

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x0007CFA9 File Offset: 0x0007B1A9
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x0007CFB1 File Offset: 0x0007B1B1
		[ConfigurationProperty]
		public string PowerBIServicePlan0Id { get; set; }

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0007CFBA File Offset: 0x0007B1BA
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x0007CFC2 File Offset: 0x0007B1C2
		[ConfigurationProperty]
		public string PowerBIServicePlan1Id { get; set; }

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x0007CFCB File Offset: 0x0007B1CB
		// (set) Token: 0x0600218F RID: 8591 RVA: 0x0007CFD3 File Offset: 0x0007B1D3
		[ConfigurationProperty]
		public string PowerBIServicePlan2Id { get; set; }
	}
}
