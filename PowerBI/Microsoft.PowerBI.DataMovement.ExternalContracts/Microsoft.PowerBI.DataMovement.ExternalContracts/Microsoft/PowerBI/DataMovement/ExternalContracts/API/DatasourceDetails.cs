using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200003B RID: 59
	[DataContract]
	public class DatasourceDetails
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00003479 File Offset: 0x00001679
		// (set) Token: 0x06000156 RID: 342 RVA: 0x00003481 File Offset: 0x00001681
		[DataMember(Name = "dataSourceId", Order = 0)]
		public long DatasourceId { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000348A File Offset: 0x0000168A
		// (set) Token: 0x06000158 RID: 344 RVA: 0x00003492 File Offset: 0x00001692
		[DataMember(Name = "dataSourceObjectId", Order = 1)]
		public Guid DatasourceObjectId { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000349B File Offset: 0x0000169B
		// (set) Token: 0x0600015A RID: 346 RVA: 0x000034A3 File Offset: 0x000016A3
		[DataMember(Name = "gatewayId", Order = 5)]
		public long GatewayId { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000034AC File Offset: 0x000016AC
		// (set) Token: 0x0600015C RID: 348 RVA: 0x000034B4 File Offset: 0x000016B4
		[DataMember(Name = "dataSourceType", Order = 10)]
		public DatasourceType DatasourceType { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000034BD File Offset: 0x000016BD
		// (set) Token: 0x0600015E RID: 350 RVA: 0x000034C5 File Offset: 0x000016C5
		[DataMember(Name = "onPremGatewayRequired", Order = 20)]
		public bool OnPremGatewayRequired { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600015F RID: 351 RVA: 0x000034CE File Offset: 0x000016CE
		// (set) Token: 0x06000160 RID: 352 RVA: 0x000034D6 File Offset: 0x000016D6
		[DataMember(Name = "connectionDetails", Order = 30)]
		public string ConnectionDetails { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000161 RID: 353 RVA: 0x000034DF File Offset: 0x000016DF
		// (set) Token: 0x06000162 RID: 354 RVA: 0x000034E7 File Offset: 0x000016E7
		[DataMember(Name = "credentialDetails", Order = 40)]
		public CredentialDetails CredentialDetails { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000034F0 File Offset: 0x000016F0
		// (set) Token: 0x06000164 RID: 356 RVA: 0x000034F8 File Offset: 0x000016F8
		[DataMember(Name = "dataSourceName", Order = 50, EmitDefaultValue = false)]
		public string Name { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00003501 File Offset: 0x00001701
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00003509 File Offset: 0x00001709
		[DataMember(Name = "dataSourceAnnotation", Order = 60, EmitDefaultValue = false)]
		public string Annotation { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00003512 File Offset: 0x00001712
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000351A File Offset: 0x0000171A
		[DataMember(Name = "dataSourceServerAnnotation", Order = 65, EmitDefaultValue = false)]
		public string ServerAnnotation { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00003523 File Offset: 0x00001723
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000352B File Offset: 0x0000172B
		[DataMember(Name = "oAuth2Endpoint", Order = 70)]
		public string OAuth2Endpoint { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00003534 File Offset: 0x00001734
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000353C File Offset: 0x0000173C
		[DataMember(Name = "oAuth2Nonce", Order = 80)]
		public string OAuth2Nonce { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00003545 File Offset: 0x00001745
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000354D File Offset: 0x0000174D
		[DataMember(Name = "supportedAuthenticationTypes", Order = 90)]
		public IList<CredentialType> SupportedAuthenticationTypes { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00003556 File Offset: 0x00001756
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000355E File Offset: 0x0000175E
		[DataMember(Name = "singleSignOnType", Order = 100)]
		public SingleSignOnType SingleSignOnType { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00003567 File Offset: 0x00001767
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00003572 File Offset: 0x00001772
		[DataMember(Name = "singleSignOnEnabled", Order = 110)]
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

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00003574 File Offset: 0x00001774
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000357C File Offset: 0x0000177C
		[DataMember(Name = "gatewayObjectId", Order = 120)]
		public Guid? GatewayObjectId { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00003585 File Offset: 0x00001785
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000358D File Offset: 0x0000178D
		[DataMember(Name = "gatewayName", Order = 130)]
		public string GatewayName { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00003596 File Offset: 0x00001796
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000359E File Offset: 0x0000179E
		[DataMember(Name = "gatewayType", Order = 140)]
		public string GatewayType { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000035A7 File Offset: 0x000017A7
		// (set) Token: 0x0600017A RID: 378 RVA: 0x000035AF File Offset: 0x000017AF
		[DataMember(Name = "dataSourceReference", Order = 150)]
		public string DataSourceReference { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000035B8 File Offset: 0x000017B8
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000035C0 File Offset: 0x000017C0
		[DataMember(Name = "key", Order = 160)]
		public string Key { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000035C9 File Offset: 0x000017C9
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000035D1 File Offset: 0x000017D1
		[DataMember(Name = "normalizedPath", Order = 170)]
		public string NormalizedPath { get; set; }

		// Token: 0x0600017F RID: 383 RVA: 0x000035DA File Offset: 0x000017DA
		public DatasourceDetails()
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000035E4 File Offset: 0x000017E4
		public DatasourceDetails(DatasourceDetails datasourceDetails)
		{
			this.DatasourceId = datasourceDetails.DatasourceId;
			this.DatasourceObjectId = datasourceDetails.DatasourceObjectId;
			this.GatewayId = datasourceDetails.GatewayId;
			this.DatasourceType = datasourceDetails.DatasourceType;
			this.OnPremGatewayRequired = datasourceDetails.OnPremGatewayRequired;
			this.ConnectionDetails = datasourceDetails.ConnectionDetails;
			this.CredentialDetails = datasourceDetails.CredentialDetails;
			this.Name = datasourceDetails.Name;
			this.Annotation = datasourceDetails.Annotation;
			this.ServerAnnotation = datasourceDetails.ServerAnnotation;
			this.OAuth2Endpoint = datasourceDetails.OAuth2Endpoint;
			this.OAuth2Nonce = datasourceDetails.OAuth2Nonce;
			this.SupportedAuthenticationTypes = datasourceDetails.SupportedAuthenticationTypes;
			this.SingleSignOnType = datasourceDetails.SingleSignOnType;
			this.GatewayObjectId = datasourceDetails.GatewayObjectId;
			this.GatewayName = datasourceDetails.GatewayName;
			this.GatewayType = datasourceDetails.GatewayType;
			this.DataSourceReference = datasourceDetails.DataSourceReference;
			this.Key = datasourceDetails.Key;
			this.NormalizedPath = datasourceDetails.NormalizedPath;
		}
	}
}
