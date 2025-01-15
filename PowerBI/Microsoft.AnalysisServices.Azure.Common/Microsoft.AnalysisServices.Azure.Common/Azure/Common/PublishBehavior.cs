using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000077 RID: 119
	[DataContract]
	public enum PublishBehavior
	{
		// Token: 0x040001E4 RID: 484
		[EnumMember(Value = "CreateNew")]
		CreateNew,
		// Token: 0x040001E5 RID: 485
		[EnumMember(Value = "Refresh")]
		Refresh
	}
}
