using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x02000120 RID: 288
	[OriginalName("KpiSharedDataItemAggregation")]
	public enum KpiSharedDataItemAggregation
	{
		// Token: 0x040005A2 RID: 1442
		[OriginalName("None")]
		None,
		// Token: 0x040005A3 RID: 1443
		[OriginalName("First")]
		First,
		// Token: 0x040005A4 RID: 1444
		[OriginalName("Last")]
		Last,
		// Token: 0x040005A5 RID: 1445
		[OriginalName("Min")]
		Min,
		// Token: 0x040005A6 RID: 1446
		[OriginalName("Max")]
		Max,
		// Token: 0x040005A7 RID: 1447
		[OriginalName("Average")]
		Average,
		// Token: 0x040005A8 RID: 1448
		[OriginalName("Sum")]
		Sum
	}
}
