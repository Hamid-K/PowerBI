using System;

namespace Model
{
	// Token: 0x0200005C RID: 92
	public class KpiData
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000265 RID: 613 RVA: 0x000034F6 File Offset: 0x000016F6
		// (set) Token: 0x06000266 RID: 614 RVA: 0x000034FE File Offset: 0x000016FE
		public KpiDataItem Value { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00003507 File Offset: 0x00001707
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000350F File Offset: 0x0000170F
		public KpiDataItem Goal { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00003518 File Offset: 0x00001718
		// (set) Token: 0x0600026A RID: 618 RVA: 0x00003520 File Offset: 0x00001720
		public KpiDataItem Status { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00003529 File Offset: 0x00001729
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00003531 File Offset: 0x00001731
		public KpiDataItem TrendSet { get; set; }
	}
}
