using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200001A RID: 26
	[DataContract]
	public enum AsyncOperationStatus
	{
		// Token: 0x04000042 RID: 66
		[DataMember]
		Unknown,
		// Token: 0x04000043 RID: 67
		[DataMember]
		InProgress,
		// Token: 0x04000044 RID: 68
		[DataMember]
		Completed,
		// Token: 0x04000045 RID: 69
		[DataMember]
		Faulted,
		// Token: 0x04000046 RID: 70
		[DataMember]
		Cancelled,
		// Token: 0x04000047 RID: 71
		[DataMember]
		Closed,
		// Token: 0x04000048 RID: 72
		[DataMember]
		Expired
	}
}
