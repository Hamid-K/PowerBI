using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000056 RID: 86
	[DataContract]
	public class GatewaySystemDetails : GatewaySystemDetailsBase
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000042B8 File Offset: 0x000024B8
		// (set) Token: 0x06000296 RID: 662 RVA: 0x000042C0 File Offset: 0x000024C0
		[DataMember(Name = "clusterSystemDetails", Order = 50)]
		public ClusterSystemDetails ClusterSystemDetails { get; set; }
	}
}
