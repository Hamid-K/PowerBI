using System;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000168 RID: 360
	internal interface IODataJsonLightWriterEntryState
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060009BC RID: 2492
		ODataEntry Entry { get; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060009BD RID: 2493
		IEdmEntityType EntityType { get; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060009BE RID: 2494
		IEdmEntityType EntityTypeFromMetadata { get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060009BF RID: 2495
		ODataFeedAndEntrySerializationInfo SerializationInfo { get; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060009C0 RID: 2496
		// (set) Token: 0x060009C1 RID: 2497
		bool EditLinkWritten { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060009C2 RID: 2498
		// (set) Token: 0x060009C3 RID: 2499
		bool ReadLinkWritten { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060009C4 RID: 2500
		// (set) Token: 0x060009C5 RID: 2501
		bool MediaEditLinkWritten { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060009C6 RID: 2502
		// (set) Token: 0x060009C7 RID: 2503
		bool MediaReadLinkWritten { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060009C8 RID: 2504
		// (set) Token: 0x060009C9 RID: 2505
		bool MediaContentTypeWritten { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060009CA RID: 2506
		// (set) Token: 0x060009CB RID: 2507
		bool MediaETagWritten { get; set; }

		// Token: 0x060009CC RID: 2508
		ODataFeedAndEntryTypeContext GetOrCreateTypeContext(IEdmModel model, bool writingResponse);
	}
}
