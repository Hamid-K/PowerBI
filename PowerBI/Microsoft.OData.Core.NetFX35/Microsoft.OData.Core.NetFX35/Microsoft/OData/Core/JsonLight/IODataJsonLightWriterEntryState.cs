using System;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000AB RID: 171
	internal interface IODataJsonLightWriterEntryState
	{
		// Token: 0x1700017B RID: 379
		// (get) Token: 0x0600063D RID: 1597
		ODataEntry Entry { get; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600063E RID: 1598
		IEdmEntityType EntityType { get; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600063F RID: 1599
		IEdmEntityType EntityTypeFromMetadata { get; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000640 RID: 1600
		ODataFeedAndEntrySerializationInfo SerializationInfo { get; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000641 RID: 1601
		// (set) Token: 0x06000642 RID: 1602
		bool EditLinkWritten { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000643 RID: 1603
		// (set) Token: 0x06000644 RID: 1604
		bool ReadLinkWritten { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000645 RID: 1605
		// (set) Token: 0x06000646 RID: 1606
		bool MediaEditLinkWritten { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000647 RID: 1607
		// (set) Token: 0x06000648 RID: 1608
		bool MediaReadLinkWritten { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000649 RID: 1609
		// (set) Token: 0x0600064A RID: 1610
		bool MediaContentTypeWritten { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600064B RID: 1611
		// (set) Token: 0x0600064C RID: 1612
		bool MediaETagWritten { get; set; }

		// Token: 0x0600064D RID: 1613
		ODataFeedAndEntryTypeContext GetOrCreateTypeContext(IEdmModel model, bool writingResponse);
	}
}
