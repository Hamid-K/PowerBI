using System;

namespace Model
{
	// Token: 0x0200006D RID: 109
	public class Kpi : CatalogItem
	{
		// Token: 0x0600031E RID: 798 RVA: 0x00003D0C File Offset: 0x00001F0C
		public Kpi()
			: base(CatalogItemType.Kpi)
		{
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00003D15 File Offset: 0x00001F15
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00003D1D File Offset: 0x00001F1D
		public KpiValueFormat ValueFormat { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00003D26 File Offset: 0x00001F26
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00003D2E File Offset: 0x00001F2E
		public KpiVisualization Visualization { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00003D37 File Offset: 0x00001F37
		// (set) Token: 0x06000324 RID: 804 RVA: 0x00003D3F File Offset: 0x00001F3F
		public DrillthroughTarget DrillthroughTarget { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00003D48 File Offset: 0x00001F48
		// (set) Token: 0x06000326 RID: 806 RVA: 0x00003D50 File Offset: 0x00001F50
		public string Currency { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00003D59 File Offset: 0x00001F59
		// (set) Token: 0x06000328 RID: 808 RVA: 0x00003D61 File Offset: 0x00001F61
		public KpiValues Values { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00003D6A File Offset: 0x00001F6A
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00003D72 File Offset: 0x00001F72
		public KpiData Data { get; set; }
	}
}
