using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000055 RID: 85
	[DataContract]
	public enum GatewayStatus
	{
		// Token: 0x040001FD RID: 509
		[EnumMember]
		Live,
		// Token: 0x040001FE RID: 510
		[EnumMember]
		NotInstalled,
		// Token: 0x040001FF RID: 511
		[EnumMember]
		NotReachable,
		// Token: 0x04000200 RID: 512
		[EnumMember]
		Installed
	}
}
