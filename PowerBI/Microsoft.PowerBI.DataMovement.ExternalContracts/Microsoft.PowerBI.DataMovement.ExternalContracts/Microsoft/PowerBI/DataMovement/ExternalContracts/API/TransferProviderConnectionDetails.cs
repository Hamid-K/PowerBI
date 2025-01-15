using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200008A RID: 138
	[DataContract]
	public class TransferProviderConnectionDetails
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x00005028 File Offset: 0x00003228
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x00005030 File Offset: 0x00003230
		[DataMember(Name = "userId")]
		public string UserId { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00005039 File Offset: 0x00003239
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x00005041 File Offset: 0x00003241
		[DataMember(Name = "tenantId")]
		public string TenantId { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000504A File Offset: 0x0000324A
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x00005052 File Offset: 0x00003252
		[DataMember(Name = "discoverMonikerSystemResponse")]
		public DiscoverMonikerSystemResponse DiscoverMonikerSystemResponse { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000505B File Offset: 0x0000325B
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x00005063 File Offset: 0x00003263
		[DataMember(Name = "needBindingRetrySemantics")]
		public bool NeedBindingRetrySemantics { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000506C File Offset: 0x0000326C
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x00005074 File Offset: 0x00003274
		[DataMember(Name = "fetchDataSourceDetailsOnInitialization")]
		public bool FetchDataSourceDetailsOnInitialization { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000507D File Offset: 0x0000327D
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x00005085 File Offset: 0x00003285
		[DataMember(Name = "rawTransferConnectionString")]
		public TransferProviderRawConnectionDetails RawTransferConnectionString { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000508E File Offset: 0x0000328E
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x00005096 File Offset: 0x00003296
		[DataMember(Name = "needStatefulSessions")]
		public bool NeedStatefulSessions { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000509F File Offset: 0x0000329F
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x000050A7 File Offset: 0x000032A7
		[DataMember(Name = "gatewaySpoolControl")]
		public GatewaySpoolControlSetting GatewaySpoolControl { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x000050B0 File Offset: 0x000032B0
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x000050B8 File Offset: 0x000032B8
		[DataMember(Name = "rowsetPacketLimit")]
		public int RowsetPacketLimit { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x000050C1 File Offset: 0x000032C1
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x000050C9 File Offset: 0x000032C9
		[DataMember(Name = "enableCancellationForQueries")]
		public bool EnableCancellationForQueries { get; set; }

		// Token: 0x040002FC RID: 764
		public const int RowsetPacketLimitDefault = 0;

		// Token: 0x040002FD RID: 765
		public const int RowsetPacketLimitNoLimit = -1;
	}
}
