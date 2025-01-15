using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V2
{
	// Token: 0x02000094 RID: 148
	[OriginalName("ItemExecutionType")]
	public enum ItemExecutionType
	{
		// Token: 0x040002F1 RID: 753
		[OriginalName("Live")]
		Live,
		// Token: 0x040002F2 RID: 754
		[OriginalName("Cache")]
		Cache,
		// Token: 0x040002F3 RID: 755
		[OriginalName("Snapshot")]
		Snapshot
	}
}
