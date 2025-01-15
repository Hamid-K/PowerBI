using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.API
{
	// Token: 0x02000027 RID: 39
	[DataContract]
	public enum ClusterDatasourceStatus
	{
		// Token: 0x040000AD RID: 173
		[EnumMember]
		None,
		// Token: 0x040000AE RID: 174
		[EnumMember]
		Live
	}
}
