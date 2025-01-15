using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200005C RID: 92
	[DataContract]
	[Flags]
	public enum GetPrincipalOptions : long
	{
		// Token: 0x04000220 RID: 544
		[EnumMember]
		None = 0L,
		// Token: 0x04000221 RID: 545
		[EnumMember]
		IncludeServicePrincipal = 1L,
		// Token: 0x04000222 RID: 546
		[EnumMember]
		IncludeAppPrincipal = 2L
	}
}
