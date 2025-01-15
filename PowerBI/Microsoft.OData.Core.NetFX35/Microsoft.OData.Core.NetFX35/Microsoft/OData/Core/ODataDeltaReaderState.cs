using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000168 RID: 360
	public enum ODataDeltaReaderState
	{
		// Token: 0x040005C2 RID: 1474
		Start,
		// Token: 0x040005C3 RID: 1475
		DeltaFeedStart,
		// Token: 0x040005C4 RID: 1476
		FeedEnd,
		// Token: 0x040005C5 RID: 1477
		DeltaEntryStart,
		// Token: 0x040005C6 RID: 1478
		DeltaEntryEnd,
		// Token: 0x040005C7 RID: 1479
		DeltaDeletedEntry,
		// Token: 0x040005C8 RID: 1480
		DeltaLink,
		// Token: 0x040005C9 RID: 1481
		DeltaDeletedLink,
		// Token: 0x040005CA RID: 1482
		Exception,
		// Token: 0x040005CB RID: 1483
		Completed,
		// Token: 0x040005CC RID: 1484
		ExpandedNavigationProperty
	}
}
