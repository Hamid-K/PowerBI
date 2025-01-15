using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200024B RID: 587
	public enum ODataReaderState
	{
		// Token: 0x040006B7 RID: 1719
		Start,
		// Token: 0x040006B8 RID: 1720
		FeedStart,
		// Token: 0x040006B9 RID: 1721
		FeedEnd,
		// Token: 0x040006BA RID: 1722
		EntryStart,
		// Token: 0x040006BB RID: 1723
		EntryEnd,
		// Token: 0x040006BC RID: 1724
		NavigationLinkStart,
		// Token: 0x040006BD RID: 1725
		NavigationLinkEnd,
		// Token: 0x040006BE RID: 1726
		EntityReferenceLink,
		// Token: 0x040006BF RID: 1727
		Exception,
		// Token: 0x040006C0 RID: 1728
		Completed
	}
}
