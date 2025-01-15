using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000081 RID: 129
	[DataContract]
	public sealed class UnifiedGatewayStatusResponse
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00004DF2 File Offset: 0x00002FF2
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00004DFA File Offset: 0x00002FFA
		[DataMember(Name = "gatewayStatus")]
		public string GatewayStatus { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00004E03 File Offset: 0x00003003
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00004E0B File Offset: 0x0000300B
		[DataMember(Name = "gatewayVersion")]
		public string GatewayVersion { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00004E14 File Offset: 0x00003014
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00004E1C File Offset: 0x0000301C
		[DataMember(Name = "gatewayUpgradeState")]
		public string GatewayUpgradeState { get; set; }
	}
}
