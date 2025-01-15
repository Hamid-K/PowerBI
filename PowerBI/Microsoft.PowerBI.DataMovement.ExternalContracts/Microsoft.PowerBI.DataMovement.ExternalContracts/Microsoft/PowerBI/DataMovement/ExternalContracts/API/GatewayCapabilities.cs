using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200004D RID: 77
	[DataContract]
	[Flags]
	public enum GatewayCapabilities
	{
		// Token: 0x040001AF RID: 431
		[EnumMember]
		None = 0,
		// Token: 0x040001B0 RID: 432
		[EnumMember]
		DirectQuery = 1,
		// Token: 0x040001B1 RID: 433
		[EnumMember]
		ETL = 2,
		// Token: 0x040001B2 RID: 434
		[EnumMember]
		All = 3
	}
}
