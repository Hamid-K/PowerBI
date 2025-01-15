using System;

namespace Microsoft.OData.Core
{
	// Token: 0x0200019A RID: 410
	public enum ODataReaderState
	{
		// Token: 0x040006BB RID: 1723
		Start,
		// Token: 0x040006BC RID: 1724
		FeedStart,
		// Token: 0x040006BD RID: 1725
		FeedEnd,
		// Token: 0x040006BE RID: 1726
		EntryStart,
		// Token: 0x040006BF RID: 1727
		EntryEnd,
		// Token: 0x040006C0 RID: 1728
		NavigationLinkStart,
		// Token: 0x040006C1 RID: 1729
		NavigationLinkEnd,
		// Token: 0x040006C2 RID: 1730
		EntityReferenceLink,
		// Token: 0x040006C3 RID: 1731
		Exception,
		// Token: 0x040006C4 RID: 1732
		Completed
	}
}
