using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x0200002A RID: 42
	[DataContract]
	public enum ClusterStatus
	{
		// Token: 0x040000B5 RID: 181
		[EnumMember]
		None,
		// Token: 0x040000B6 RID: 182
		[EnumMember]
		Live
	}
}
