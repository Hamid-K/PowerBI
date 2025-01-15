using System;
using Microsoft.OData.Client;

namespace Microsoft.ReportingServices.Portal.ODataClient.V1.Model
{
	// Token: 0x0200012F RID: 303
	[OriginalName("ItemExecutionType")]
	public enum ItemExecutionType
	{
		// Token: 0x04000617 RID: 1559
		[OriginalName("Live")]
		Live,
		// Token: 0x04000618 RID: 1560
		[OriginalName("Cache")]
		Cache,
		// Token: 0x04000619 RID: 1561
		[OriginalName("Snapshot")]
		Snapshot
	}
}
