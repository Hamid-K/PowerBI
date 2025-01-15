using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200002B RID: 43
	[DataContract]
	public sealed class ClusterStatusResponse
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002E2C File Offset: 0x0000102C
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00002E34 File Offset: 0x00001034
		[DataMember(Name = "clusterStatus", Order = 10)]
		public ClusterStatus ClusterStatus { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002E3D File Offset: 0x0000103D
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00002E45 File Offset: 0x00001045
		[DataMember(Name = "gatewayStaticCapabilities", Order = 20)]
		public GatewayStaticCapabilities GatewayStaticCapabilities { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002E4E File Offset: 0x0000104E
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00002E56 File Offset: 0x00001056
		[DataMember(Name = "gatewayVersion", Order = 30)]
		public string GatewayVersion { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002E5F File Offset: 0x0000105F
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00002E67 File Offset: 0x00001067
		[DataMember(Name = "gatewayUpgradeState", Order = 40)]
		public string GatewayUpgradeState { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00002E70 File Offset: 0x00001070
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00002E78 File Offset: 0x00001078
		[DataMember(Name = "gatewayNameToErrorDictionary", Order = 50)]
		public Dictionary<string, string> GatewayNameToErrorDictionary { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002E81 File Offset: 0x00001081
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00002E89 File Offset: 0x00001089
		[DataMember(Name = "gatewayNameToErrorResponseDictionary", Order = 60)]
		public IDictionary<string, PowerBIApiErrorResponse> GatewayNameToErrorResponseDictionary { get; set; }
	}
}
