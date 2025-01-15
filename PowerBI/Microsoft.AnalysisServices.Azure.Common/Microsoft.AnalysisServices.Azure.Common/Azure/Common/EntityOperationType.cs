using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000078 RID: 120
	[DataContract]
	public enum EntityOperationType
	{
		// Token: 0x040001E7 RID: 487
		[EnumMember(Value = "Add")]
		Add,
		// Token: 0x040001E8 RID: 488
		[EnumMember(Value = "Update")]
		Update,
		// Token: 0x040001E9 RID: 489
		[EnumMember(Value = "Delete")]
		Delete,
		// Token: 0x040001EA RID: 490
		[EnumMember(Value = "Unknown")]
		Unknown,
		// Token: 0x040001EB RID: 491
		[EnumMember(Value = "UpdateWritethrough")]
		UpdateWritethrough
	}
}
