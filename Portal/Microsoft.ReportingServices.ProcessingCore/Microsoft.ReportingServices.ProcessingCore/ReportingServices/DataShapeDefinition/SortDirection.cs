using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.DataShapeDefinition
{
	// Token: 0x020005A8 RID: 1448
	[DataContract]
	internal enum SortDirection
	{
		// Token: 0x0400297A RID: 10618
		[EnumMember]
		Unspecified,
		// Token: 0x0400297B RID: 10619
		[EnumMember]
		Ascending,
		// Token: 0x0400297C RID: 10620
		[EnumMember]
		Descending
	}
}
