using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000219 RID: 537
	internal interface IODataAtomReaderFeedState
	{
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000FCC RID: 4044
		ODataFeed Feed { get; }

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000FCD RID: 4045
		// (set) Token: 0x06000FCE RID: 4046
		bool FeedElementEmpty { get; set; }

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000FCF RID: 4047
		AtomFeedMetadata AtomFeedMetadata { get; }

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000FD0 RID: 4048
		// (set) Token: 0x06000FD1 RID: 4049
		bool HasCount { get; set; }

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000FD2 RID: 4050
		// (set) Token: 0x06000FD3 RID: 4051
		bool HasNextPageLink { get; set; }

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000FD4 RID: 4052
		// (set) Token: 0x06000FD5 RID: 4053
		bool HasReadLink { get; set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000FD6 RID: 4054
		// (set) Token: 0x06000FD7 RID: 4055
		bool HasDeltaLink { get; set; }
	}
}
