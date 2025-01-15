using System;
using System.Runtime.Serialization;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000071 RID: 113
	[DataContract]
	public enum ModelPowerBIDatasourceFormatVersion
	{
		// Token: 0x040001C4 RID: 452
		[EnumMember]
		PowerBI_V1,
		// Token: 0x040001C5 RID: 453
		[EnumMember]
		PowerBI_V2,
		// Token: 0x040001C6 RID: 454
		[EnumMember]
		PowerBI_V3
	}
}
