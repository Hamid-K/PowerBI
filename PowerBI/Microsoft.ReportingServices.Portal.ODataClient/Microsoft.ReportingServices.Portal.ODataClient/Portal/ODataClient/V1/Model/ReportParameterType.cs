using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000127 RID: 295
	[OriginalName("ReportParameterType")]
	public enum ReportParameterType
	{
		// Token: 0x040005EE RID: 1518
		[OriginalName("Boolean")]
		Boolean,
		// Token: 0x040005EF RID: 1519
		[OriginalName("DateTime")]
		DateTime,
		// Token: 0x040005F0 RID: 1520
		[OriginalName("Integer")]
		Integer,
		// Token: 0x040005F1 RID: 1521
		[OriginalName("Float")]
		Float,
		// Token: 0x040005F2 RID: 1522
		[OriginalName("String")]
		String
	}
}
