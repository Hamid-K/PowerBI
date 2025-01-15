using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.ExternalContracts.API;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200003B RID: 59
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public sealed class OAuthFinishLoginOnGatewayResult : GatewayResultBase
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00002912 File Offset: 0x00000B12
		// (set) Token: 0x06000105 RID: 261 RVA: 0x0000291A File Offset: 0x00000B1A
		[DataMember(Name = "errorMessages", IsRequired = false)]
		public IList<string> ErrorMessages { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00002923 File Offset: 0x00000B23
		// (set) Token: 0x06000107 RID: 263 RVA: 0x0000292B File Offset: 0x00000B2B
		[DataMember(Name = "oAuthCredentialDetails", IsRequired = true)]
		public CredentialDetails OAuthCredentialDetails { get; set; }
	}
}
