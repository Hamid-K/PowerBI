using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000029 RID: 41
	[DataContract]
	public class ClusterMemberSystemDetails : GatewaySystemDetailsBase
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002E02 File Offset: 0x00001002
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00002E0A File Offset: 0x0000100A
		[DataMember(Name = "memberStatus", Order = 50)]
		public ClusterMemberStatus MemberStatus { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002E13 File Offset: 0x00001013
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00002E1B File Offset: 0x0000101B
		[DataMember(Name = "gatewayLoadBalancingSettings", Order = 60)]
		public string GatewayLoadBalancingSettings { get; set; }
	}
}
