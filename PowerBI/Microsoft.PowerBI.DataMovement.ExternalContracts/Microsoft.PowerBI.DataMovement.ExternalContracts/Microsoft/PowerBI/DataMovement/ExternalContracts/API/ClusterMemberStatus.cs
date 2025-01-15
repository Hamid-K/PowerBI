using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000028 RID: 40
	[DataContract]
	public enum ClusterMemberStatus
	{
		// Token: 0x040000B0 RID: 176
		[EnumMember]
		None,
		// Token: 0x040000B1 RID: 177
		[EnumMember]
		Enabled
	}
}
