using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000053 RID: 83
	[DataContract]
	public sealed class GatewayServiceBusDetails
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000427D File Offset: 0x0000247D
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00004285 File Offset: 0x00002485
		[DataMember(Name = "gatewaySBKey", Order = 10)]
		public string GatewayServiceBusKey { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000428E File Offset: 0x0000248E
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00004296 File Offset: 0x00002496
		[DataMember(Name = "gatewaySBKeyName", Order = 20)]
		public string GatewayServiceBusKeyName { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000429F File Offset: 0x0000249F
		// (set) Token: 0x06000293 RID: 659 RVA: 0x000042A7 File Offset: 0x000024A7
		[DataMember(Name = "gatewaySBEndpoint", Order = 30)]
		public string GatewayServiceBusEndpoint { get; set; }
	}
}
