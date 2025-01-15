using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200003D RID: 61
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class OAuthInitiateLoginOnGatewayResult : GatewayResultBase
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00002966 File Offset: 0x00000B66
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000296E File Offset: 0x00000B6E
		[DataMember(Name = "errorMessages", IsRequired = false)]
		public IList<string> ErrorMessages { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00002977 File Offset: 0x00000B77
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000297F File Offset: 0x00000B7F
		[DataMember(Name = "oAuthLogin", IsRequired = true)]
		public OAuthLoginInfo OAuthLogin { get; set; }
	}
}
