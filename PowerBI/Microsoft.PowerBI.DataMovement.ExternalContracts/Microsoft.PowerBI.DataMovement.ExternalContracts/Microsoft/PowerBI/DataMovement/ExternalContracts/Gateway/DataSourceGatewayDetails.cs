using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway
{
	// Token: 0x02000015 RID: 21
	[DataContract]
	public sealed class DataSourceGatewayDetails
	{
		// Token: 0x0600005B RID: 91 RVA: 0x0000283A File Offset: 0x00000A3A
		public DataSourceGatewayDetails()
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002844 File Offset: 0x00000A44
		public DataSourceGatewayDetails(DataSourceGatewayDetails original)
		{
			if (original == null)
			{
				throw new ArgumentNullException("original");
			}
			this.DataSourceId = original.DataSourceId;
			this.DataSourceObjectId = original.DataSourceObjectId;
			this.OnPremGatewayRequired = original.OnPremGatewayRequired;
			this.GatewayId = original.GatewayId;
			this.GatewayObjectId = original.GatewayObjectId;
			this.DataSourceType = original.DataSourceType;
			this.ServerAnnotation = original.ServerAnnotation;
			this.DataSourceReference = original.DataSourceReference;
			this.SingleSignOnType = original.SingleSignOnType;
			GatewayCredentialDetails gatewayCredentialDetails = original.CredentialDetails as GatewayCredentialDetails;
			if (gatewayCredentialDetails != null)
			{
				this.CredentialDetails = new GatewayCredentialDetails(gatewayCredentialDetails);
				return;
			}
			this.CredentialDetails = new CredentialDetails(original.CredentialDetails);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000028FE File Offset: 0x00000AFE
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002906 File Offset: 0x00000B06
		[DataMember(Name = "dataSourceId", Order = 0)]
		public long DataSourceId { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005F RID: 95 RVA: 0x0000290F File Offset: 0x00000B0F
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002917 File Offset: 0x00000B17
		[DataMember(Name = "dataSourceObjectId", Order = 5)]
		public Guid DataSourceObjectId { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002920 File Offset: 0x00000B20
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002928 File Offset: 0x00000B28
		[DataMember(Name = "onPremGatewayRequired", Order = 10)]
		public bool OnPremGatewayRequired { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002931 File Offset: 0x00000B31
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002939 File Offset: 0x00000B39
		[DataMember(Name = "gatewayId", Order = 20)]
		public long GatewayId { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002942 File Offset: 0x00000B42
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000294A File Offset: 0x00000B4A
		[DataMember(Name = "gatewayObjectId", Order = 25)]
		public Guid GatewayObjectId { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002953 File Offset: 0x00000B53
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000295B File Offset: 0x00000B5B
		[DataMember(Name = "credentialDetails", Order = 30)]
		public CredentialDetails CredentialDetails { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002964 File Offset: 0x00000B64
		// (set) Token: 0x0600006A RID: 106 RVA: 0x0000296C File Offset: 0x00000B6C
		[DataMember(Name = "dataSourceType", Order = 40)]
		public DatasourceType? DataSourceType { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002975 File Offset: 0x00000B75
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000297D File Offset: 0x00000B7D
		[DataMember(Name = "serverAnnotation", Order = 50)]
		public string ServerAnnotation { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002986 File Offset: 0x00000B86
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000298E File Offset: 0x00000B8E
		[DataMember(Name = "dataSourceReference", Order = 60)]
		public string DataSourceReference { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002997 File Offset: 0x00000B97
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000299F File Offset: 0x00000B9F
		[DataMember(Name = "singleSignOnType", Order = 70)]
		public SingleSignOnType SingleSignOnType { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000029A8 File Offset: 0x00000BA8
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000029B3 File Offset: 0x00000BB3
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

		// Token: 0x06000073 RID: 115 RVA: 0x000029B8 File Offset: 0x00000BB8
		public static DataSourceGatewayDetails CreateFromMonikerAndDataSourceSystemDetails(MonikerDataSourcesSystemDetails monikerDataSource, IList<DataSourceSystemDetails> dataSources)
		{
			DataSourceGatewayDetails dataSourceGatewayDetails = DataSourceGatewayDetails.CreateFromDataSourceSystemDetails(dataSources.First((DataSourceSystemDetails ds) => ds.DataSourceObjectId == monikerDataSource.DataSourceObjectId));
			dataSourceGatewayDetails.DataSourceReference = monikerDataSource.DataSourceReference;
			return dataSourceGatewayDetails;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000029FC File Offset: 0x00000BFC
		public static DataSourceGatewayDetails CreateFromDataSourceSystemDetails(DataSourceSystemDetails dataSource)
		{
			return new DataSourceGatewayDetails
			{
				DataSourceId = dataSource.DataSourceId,
				DataSourceObjectId = dataSource.DataSourceObjectId,
				OnPremGatewayRequired = dataSource.OnPremGatewayRequired,
				GatewayId = dataSource.GatewayId,
				GatewayObjectId = dataSource.GatewayObjectId,
				CredentialDetails = new CredentialDetails(dataSource.CredentialDetails),
				DataSourceType = dataSource.DataSourceType,
				ServerAnnotation = dataSource.ServerAnnotation,
				DataSourceReference = dataSource.DataSourceReference,
				SingleSignOnType = dataSource.SingleSignOnType
			};
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002A8C File Offset: 0x00000C8C
		public static DataSourceGatewayDetails CreateFromDatasourceDetails(DatasourceDetails datasourceDetails)
		{
			return new DataSourceGatewayDetails
			{
				DataSourceId = datasourceDetails.DatasourceId,
				DataSourceObjectId = datasourceDetails.DatasourceObjectId,
				OnPremGatewayRequired = datasourceDetails.OnPremGatewayRequired,
				GatewayId = datasourceDetails.GatewayId,
				GatewayObjectId = datasourceDetails.GatewayObjectId.GetValueOrDefault(),
				CredentialDetails = new CredentialDetails(datasourceDetails.CredentialDetails),
				DataSourceType = new DatasourceType?(datasourceDetails.DatasourceType),
				ServerAnnotation = datasourceDetails.ServerAnnotation,
				DataSourceReference = datasourceDetails.DataSourceReference,
				SingleSignOnType = datasourceDetails.SingleSignOnType
			};
		}
	}
}
