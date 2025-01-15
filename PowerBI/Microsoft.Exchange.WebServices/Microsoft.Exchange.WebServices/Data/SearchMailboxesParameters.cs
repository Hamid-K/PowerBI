using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000280 RID: 640
	public sealed class SearchMailboxesParameters
	{
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x0003DFEE File Offset: 0x0003CFEE
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x0003DFF6 File Offset: 0x0003CFF6
		public MailboxQuery[] SearchQueries { get; set; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x0003DFFF File Offset: 0x0003CFFF
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x0003E007 File Offset: 0x0003D007
		public SearchResultType ResultType { get; set; }

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0003E010 File Offset: 0x0003D010
		// (set) Token: 0x06001674 RID: 5748 RVA: 0x0003E018 File Offset: 0x0003D018
		public string SortBy { get; set; }

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x0003E021 File Offset: 0x0003D021
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x0003E029 File Offset: 0x0003D029
		public SortDirection SortOrder { get; set; }

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x0003E032 File Offset: 0x0003D032
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x0003E03A File Offset: 0x0003D03A
		public bool PerformDeduplication { get; set; }

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x0003E043 File Offset: 0x0003D043
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x0003E04B File Offset: 0x0003D04B
		public int PageSize { get; set; }

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x0003E054 File Offset: 0x0003D054
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x0003E05C File Offset: 0x0003D05C
		public SearchPageDirection PageDirection { get; set; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x0003E065 File Offset: 0x0003D065
		// (set) Token: 0x0600167E RID: 5758 RVA: 0x0003E06D File Offset: 0x0003D06D
		public string PageItemReference { get; set; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x0003E076 File Offset: 0x0003D076
		// (set) Token: 0x06001680 RID: 5760 RVA: 0x0003E07E File Offset: 0x0003D07E
		public PreviewItemResponseShape PreviewItemResponseShape { get; set; }

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x0003E087 File Offset: 0x0003D087
		// (set) Token: 0x06001682 RID: 5762 RVA: 0x0003E08F File Offset: 0x0003D08F
		public string Language { get; set; }
	}
}
