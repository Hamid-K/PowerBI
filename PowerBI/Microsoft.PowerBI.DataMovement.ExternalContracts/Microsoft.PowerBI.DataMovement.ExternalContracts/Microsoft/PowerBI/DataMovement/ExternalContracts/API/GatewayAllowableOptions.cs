using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200004C RID: 76
	[DataContract]
	[Flags]
	public enum GatewayAllowableOptions : long
	{
		// Token: 0x040001AB RID: 427
		[EnumMember]
		None = 0L,
		// Token: 0x040001AC RID: 428
		[EnumMember]
		CloudDatasourceRefresh = 1L,
		// Token: 0x040001AD RID: 429
		[EnumMember]
		CustomConnectors = 2L
	}
}
