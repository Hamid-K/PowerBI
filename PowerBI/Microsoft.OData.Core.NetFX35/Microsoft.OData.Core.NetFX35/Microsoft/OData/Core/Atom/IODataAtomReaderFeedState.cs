using System;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000028 RID: 40
	internal interface IODataAtomReaderFeedState
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000175 RID: 373
		ODataFeed Feed { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000176 RID: 374
		// (set) Token: 0x06000177 RID: 375
		bool FeedElementEmpty { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000178 RID: 376
		AtomFeedMetadata AtomFeedMetadata { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000179 RID: 377
		// (set) Token: 0x0600017A RID: 378
		bool HasCount { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600017B RID: 379
		// (set) Token: 0x0600017C RID: 380
		bool HasNextPageLink { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600017D RID: 381
		// (set) Token: 0x0600017E RID: 382
		bool HasReadLink { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600017F RID: 383
		// (set) Token: 0x06000180 RID: 384
		bool HasDeltaLink { get; set; }
	}
}
