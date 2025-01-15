using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200002C RID: 44
	[DataContract]
	public class ClusterSystemDetails
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00002E9A File Offset: 0x0000109A
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00002EA2 File Offset: 0x000010A2
		[DataMember(Name = "anchorGatewayId", Order = 10)]
		public long AnchorGatewayId { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002EAB File Offset: 0x000010AB
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00002EB3 File Offset: 0x000010B3
		[DataMember(Name = "name", Order = 20)]
		public string Name { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002EBC File Offset: 0x000010BC
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00002EC4 File Offset: 0x000010C4
		[DataMember(Name = "clusterLoadBalancingSettings", Order = 30)]
		public string ClusterLoadBalancingSettings { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00002ECD File Offset: 0x000010CD
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00002ED5 File Offset: 0x000010D5
		[DataMember(Name = "gatewayClusterMembers", Order = 40)]
		public IList<ClusterMemberSystemDetails> GatewayClusterMembers { get; set; }
	}
}
