using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000082 RID: 130
	[DataContract]
	public sealed class UpdateClusterGatewayRequest
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00004E2D File Offset: 0x0000302D
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00004E35 File Offset: 0x00003035
		[MaxLength(200)]
		[DataMember(Name = "gatewayName", Order = 10)]
		public string GatewayName { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00004E3E File Offset: 0x0000303E
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x00004E46 File Offset: 0x00003046
		[MaxLength(4000)]
		[DataMember(Name = "gatewayAnnotation", Order = 20)]
		public string GatewayAnnotation { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00004E4F File Offset: 0x0000304F
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00004E57 File Offset: 0x00003057
		[DataMember(Name = "clusterMemberStatus", Order = 30)]
		public ClusterMemberStatus? ClusterMemberStatus { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00004E60 File Offset: 0x00003060
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00004E68 File Offset: 0x00003068
		[MaxLength(4000)]
		[DataMember(Name = "LoadBalancingSettings", Order = 40)]
		public string LoadBalancingSettings { get; set; }
	}
}
