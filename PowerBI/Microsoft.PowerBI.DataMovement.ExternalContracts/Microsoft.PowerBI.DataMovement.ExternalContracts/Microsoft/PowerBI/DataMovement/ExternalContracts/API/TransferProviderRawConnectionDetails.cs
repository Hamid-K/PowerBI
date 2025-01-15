using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200008B RID: 139
	[DataContract]
	public class TransferProviderRawConnectionDetails
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x000050DA File Offset: 0x000032DA
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x000050E2 File Offset: 0x000032E2
		[DataMember(Name = "gatewayId")]
		public long GatewayId { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x000050EB File Offset: 0x000032EB
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x000050F3 File Offset: 0x000032F3
		[DataMember(Name = "provider")]
		public string Provider { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x000050FC File Offset: 0x000032FC
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x00005104 File Offset: 0x00003304
		[DataMember(Name = "connectionString")]
		public string ConnectionString { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000510D File Offset: 0x0000330D
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x00005115 File Offset: 0x00003315
		[DataMember(Name = "impersonationCredentials")]
		public string ImpersonationCredentials { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000511E File Offset: 0x0000331E
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x00005126 File Offset: 0x00003326
		[DataMember(Name = "gatewaySBDetails")]
		public GatewayServiceBusDetails GatewayServiceBusDetails { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000512F File Offset: 0x0000332F
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x00005137 File Offset: 0x00003337
		[DataMember(Name = "clusterSystemDetails")]
		public ClusterSystemDetails ClusterSystemDetails { get; set; }
	}
}
