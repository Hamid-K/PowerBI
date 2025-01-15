using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200002E RID: 46
	[DataContract]
	public sealed class CreateGatewayAndAddAppPrinciaplsRequest
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00002EE6 File Offset: 0x000010E6
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00002EEE File Offset: 0x000010EE
		[DataMember(Name = "createGatewayRequest", Order = 10)]
		[Required]
		public CreateGatewayRequest CreateGatewayRequest { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00002EF7 File Offset: 0x000010F7
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00002EFF File Offset: 0x000010FF
		[DataMember(Name = "appsToAdd", Order = 20)]
		public IList<string> AppsToAdd { get; set; }
	}
}
