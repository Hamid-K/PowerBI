using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000041 RID: 65
	[DataContract]
	public sealed class DataSourceNoCredentials
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000390D File Offset: 0x00001B0D
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00003915 File Offset: 0x00001B15
		[DataMember(Name = "gatewayObjectId", Order = 10)]
		public Guid GatewayObjectId { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000391E File Offset: 0x00001B1E
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x00003926 File Offset: 0x00001B26
		[DataMember(Name = "dataSourceObjectId", Order = 20)]
		public Guid DatasourceObjectId { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000392F File Offset: 0x00001B2F
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00003937 File Offset: 0x00001B37
		[DataMember(Name = "dataSourceName", Order = 30, EmitDefaultValue = false)]
		public string Name { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00003940 File Offset: 0x00001B40
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x00003948 File Offset: 0x00001B48
		[DataMember(Name = "dataSourceType", Order = 40)]
		public string DatasourceType { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00003951 File Offset: 0x00001B51
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00003959 File Offset: 0x00001B59
		[DataMember(Name = "dataSourceReference", Order = 50)]
		public string DataSourceReference { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00003962 File Offset: 0x00001B62
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000396A File Offset: 0x00001B6A
		[DataMember(Name = "credentialType", Order = 60)]
		public string CredentialType { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00003973 File Offset: 0x00001B73
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000397B File Offset: 0x00001B7B
		[DataMember(Name = "privacyLevel", Order = 70)]
		public string PrivacyLevel { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00003984 File Offset: 0x00001B84
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000398C File Offset: 0x00001B8C
		[DataMember(Name = "onPremGatewayRequired", Order = 80)]
		public bool OnPremGatewayRequired { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00003995 File Offset: 0x00001B95
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000399D File Offset: 0x00001B9D
		[DataMember(Name = "singleSignOnType", Order = 90)]
		public string SingleSignOnType { get; set; }
	}
}
