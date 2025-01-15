using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000058 RID: 88
	[DataContract]
	public enum GatewayType
	{
		// Token: 0x04000210 RID: 528
		[EnumMember]
		Resource,
		// Token: 0x04000211 RID: 529
		[EnumMember]
		Personal,
		// Token: 0x04000212 RID: 530
		[EnumMember]
		VirtualNetwork,
		// Token: 0x04000213 RID: 531
		[EnumMember]
		TenantCloud
	}
}
