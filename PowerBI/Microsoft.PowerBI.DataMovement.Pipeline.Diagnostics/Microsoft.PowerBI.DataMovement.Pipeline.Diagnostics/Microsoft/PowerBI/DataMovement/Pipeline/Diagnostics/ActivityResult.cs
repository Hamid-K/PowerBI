using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics
{
	// Token: 0x02000009 RID: 9
	[DataContract]
	public enum ActivityResult : byte
	{
		// Token: 0x04000019 RID: 25
		[EnumMember(Value = "U")]
		Unknown,
		// Token: 0x0400001A RID: 26
		[EnumMember(Value = "NS")]
		NotStarted,
		// Token: 0x0400001B RID: 27
		[EnumMember(Value = "R")]
		Running,
		// Token: 0x0400001C RID: 28
		[EnumMember(Value = "CS")]
		CompletedSuccessfully,
		// Token: 0x0400001D RID: 29
		[EnumMember(Value = "CSDF")]
		CompletedSuccessfullyDespiteFailure,
		// Token: 0x0400001E RID: 30
		[EnumMember(Value = "CWF")]
		CompletedWithFailure,
		// Token: 0x0400001F RID: 31
		[EnumMember(Value = "CWRF")]
		CompletedWithRemoteFailure
	}
}
