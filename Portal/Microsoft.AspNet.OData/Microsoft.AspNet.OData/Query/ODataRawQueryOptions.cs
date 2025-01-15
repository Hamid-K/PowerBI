using System;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000D1 RID: 209
	public class ODataRawQueryOptions
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00017B71 File Offset: 0x00015D71
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x00017B79 File Offset: 0x00015D79
		public string Filter { get; internal set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00017B82 File Offset: 0x00015D82
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x00017B8A File Offset: 0x00015D8A
		public string Apply { get; internal set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x00017B93 File Offset: 0x00015D93
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x00017B9B File Offset: 0x00015D9B
		public string OrderBy { get; internal set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x00017BA4 File Offset: 0x00015DA4
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x00017BAC File Offset: 0x00015DAC
		public string Top { get; internal set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00017BB5 File Offset: 0x00015DB5
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x00017BBD File Offset: 0x00015DBD
		public string Skip { get; internal set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00017BC6 File Offset: 0x00015DC6
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x00017BCE File Offset: 0x00015DCE
		public string Select { get; internal set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x00017BD7 File Offset: 0x00015DD7
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x00017BDF File Offset: 0x00015DDF
		public string Expand { get; internal set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x00017BE8 File Offset: 0x00015DE8
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x00017BF0 File Offset: 0x00015DF0
		public string Count { get; internal set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00017BF9 File Offset: 0x00015DF9
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x00017C01 File Offset: 0x00015E01
		public string Format { get; internal set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x00017C0A File Offset: 0x00015E0A
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x00017C12 File Offset: 0x00015E12
		public string SkipToken { get; internal set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x00017C1B File Offset: 0x00015E1B
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x00017C23 File Offset: 0x00015E23
		public string DeltaToken { get; internal set; }
	}
}
