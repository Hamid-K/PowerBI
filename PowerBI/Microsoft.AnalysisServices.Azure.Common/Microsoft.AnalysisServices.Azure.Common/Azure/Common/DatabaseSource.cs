using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200006D RID: 109
	[DataContract]
	public enum DatabaseSource
	{
		// Token: 0x040001B4 RID: 436
		[EnumMember(Value = "PowerBI")]
		PowerBI,
		// Token: 0x040001B5 RID: 437
		[EnumMember(Value = "O365")]
		O365,
		// Token: 0x040001B6 RID: 438
		[EnumMember(Value = "O365SOBE")]
		O365SOBE
	}
}
