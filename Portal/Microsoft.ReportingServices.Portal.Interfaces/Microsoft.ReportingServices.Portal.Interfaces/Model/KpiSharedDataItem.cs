using System;
using System.Collections.Generic;

namespace Model
{
	// Token: 0x0200005F RID: 95
	public class KpiSharedDataItem : KpiDataItem
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000354B File Offset: 0x0000174B
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00003553 File Offset: 0x00001753
		public string Path { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000355C File Offset: 0x0000175C
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00003564 File Offset: 0x00001764
		public string Column { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000356D File Offset: 0x0000176D
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00003575 File Offset: 0x00001775
		public KpiSharedDataItemAggregation Aggregation { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000357E File Offset: 0x0000177E
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00003586 File Offset: 0x00001786
		public IEnumerable<DataSetParameter> Parameters { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000358F File Offset: 0x0000178F
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00003597 File Offset: 0x00001797
		public Guid Id { get; set; }
	}
}
