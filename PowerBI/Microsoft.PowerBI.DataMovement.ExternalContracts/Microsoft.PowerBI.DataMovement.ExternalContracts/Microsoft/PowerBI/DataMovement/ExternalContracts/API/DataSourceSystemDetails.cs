using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000042 RID: 66
	[DataContract]
	public class DataSourceSystemDetails
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x000039AE File Offset: 0x00001BAE
		public DataSourceSystemDetails()
		{
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000039B8 File Offset: 0x00001BB8
		public DataSourceSystemDetails(DataSourceSystemDetails original)
		{
			if (original == null)
			{
				throw new ArgumentNullException("original");
			}
			this.DataSourceId = original.DataSourceId;
			this.OnPremGatewayRequired = original.OnPremGatewayRequired;
			this.GatewayId = original.GatewayId;
			this.CredentialDetails = new CredentialDetails(original.CredentialDetails);
			this.DataSourceType = original.DataSourceType;
			this.ServerAnnotation = original.ServerAnnotation;
			this.SingleSignOnType = original.SingleSignOnType;
			this.DataSourceObjectId = original.DataSourceObjectId;
			this.GatewayObjectId = original.GatewayObjectId;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x00003A4A File Offset: 0x00001C4A
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x00003A52 File Offset: 0x00001C52
		[DataMember(Name = "dataSourceId", Order = 0)]
		public long DataSourceId { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x00003A5B File Offset: 0x00001C5B
		// (set) Token: 0x060001DA RID: 474 RVA: 0x00003A63 File Offset: 0x00001C63
		[DataMember(Name = "onPremGatewayRequired", Order = 10)]
		public bool OnPremGatewayRequired { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00003A6C File Offset: 0x00001C6C
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00003A74 File Offset: 0x00001C74
		[DataMember(Name = "gatewayId", Order = 20)]
		public long GatewayId { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00003A7D File Offset: 0x00001C7D
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00003A85 File Offset: 0x00001C85
		[DataMember(Name = "credentialDetails", Order = 30)]
		public CredentialDetails CredentialDetails { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00003A8E File Offset: 0x00001C8E
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00003A96 File Offset: 0x00001C96
		[DataMember(Name = "dataSourceType", Order = 40)]
		public DatasourceType? DataSourceType { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00003A9F File Offset: 0x00001C9F
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00003AA7 File Offset: 0x00001CA7
		[DataMember(Name = "serverAnnotation", Order = 50)]
		public string ServerAnnotation { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00003AB0 File Offset: 0x00001CB0
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00003AB8 File Offset: 0x00001CB8
		[DataMember(Name = "annotation", Order = 60)]
		public string Annotation { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00003AC1 File Offset: 0x00001CC1
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00003AC9 File Offset: 0x00001CC9
		[DataMember(Name = "singleSignOnType", Order = 70)]
		public SingleSignOnType SingleSignOnType { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00003AD2 File Offset: 0x00001CD2
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00003ADD File Offset: 0x00001CDD
		[DataMember(Name = "singleSignOnEnabled", Order = 80)]
		public bool SingleSignOnEnabled
		{
			get
			{
				return this.SingleSignOnType > SingleSignOnType.None;
			}
			set
			{
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00003ADF File Offset: 0x00001CDF
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00003AE7 File Offset: 0x00001CE7
		[DataMember(Name = "oauthResourceId", Order = 90)]
		public string OAuthResourceId { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00003AF0 File Offset: 0x00001CF0
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00003AF8 File Offset: 0x00001CF8
		[DataMember(Name = "dataSourceReference", Order = 100)]
		public string DataSourceReference { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00003B01 File Offset: 0x00001D01
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00003B09 File Offset: 0x00001D09
		[DataMember(Name = "gatewayObjectId", Order = 110)]
		public Guid GatewayObjectId { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00003B12 File Offset: 0x00001D12
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00003B1A File Offset: 0x00001D1A
		[DataMember(Name = "dataSourceObjectId", Order = 120)]
		public Guid DataSourceObjectId { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00003B23 File Offset: 0x00001D23
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00003B2B File Offset: 0x00001D2B
		[IgnoreDataMember]
		public DataSourceAnnotation TypedAnnotation { get; set; }
	}
}
