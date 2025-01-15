using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x0200008F RID: 143
	[OriginalName("KpiSharedDataItemAggregation")]
	public enum KpiSharedDataItemAggregation
	{
		// Token: 0x040002CE RID: 718
		[OriginalName("None")]
		None,
		// Token: 0x040002CF RID: 719
		[OriginalName("First")]
		First,
		// Token: 0x040002D0 RID: 720
		[OriginalName("Last")]
		Last,
		// Token: 0x040002D1 RID: 721
		[OriginalName("Min")]
		Min,
		// Token: 0x040002D2 RID: 722
		[OriginalName("Max")]
		Max,
		// Token: 0x040002D3 RID: 723
		[OriginalName("Average")]
		Average,
		// Token: 0x040002D4 RID: 724
		[OriginalName("Sum")]
		Sum
	}
}
