using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200003F RID: 63
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class OAuthRenewTokenOnGatewayResult : GatewayResultBase
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000029DC File Offset: 0x00000BDC
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000029E4 File Offset: 0x00000BE4
		[DataMember(Name = "tokenRenewed", IsRequired = true)]
		public bool TokenRenewed { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000029ED File Offset: 0x00000BED
		// (set) Token: 0x0600011F RID: 287 RVA: 0x000029F5 File Offset: 0x00000BF5
		[DataMember(Name = "errorMessages", IsRequired = false)]
		public IList<string> ErrorMessages { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000029FE File Offset: 0x00000BFE
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00002A06 File Offset: 0x00000C06
		[DataMember(Name = "oAuthCredentialDetails", IsRequired = true)]
		public CredentialDetails OAuthCredentialDetails { get; set; }
	}
}
