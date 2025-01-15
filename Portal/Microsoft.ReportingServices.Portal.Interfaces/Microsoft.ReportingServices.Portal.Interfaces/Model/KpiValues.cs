using System;

namespace Model
{
	// Token: 0x02000061 RID: 97
	public sealed class KpiValues
	{
		// Token: 0x0600027F RID: 639 RVA: 0x000035B9 File Offset: 0x000017B9
		public KpiValues()
		{
			this.TrendSet = new double?[0];
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000280 RID: 640 RVA: 0x000035CD File Offset: 0x000017CD
		// (set) Token: 0x06000281 RID: 641 RVA: 0x000035D5 File Offset: 0x000017D5
		public string Value { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000282 RID: 642 RVA: 0x000035DE File Offset: 0x000017DE
		// (set) Token: 0x06000283 RID: 643 RVA: 0x000035E6 File Offset: 0x000017E6
		public double? Goal { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000284 RID: 644 RVA: 0x000035EF File Offset: 0x000017EF
		// (set) Token: 0x06000285 RID: 645 RVA: 0x000035F7 File Offset: 0x000017F7
		public double? Status { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00003600 File Offset: 0x00001800
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00003608 File Offset: 0x00001808
		public double?[] TrendSet { get; set; }
	}
}
