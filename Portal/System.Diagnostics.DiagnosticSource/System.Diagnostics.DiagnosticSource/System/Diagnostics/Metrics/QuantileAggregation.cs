using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000040 RID: 64
	internal sealed class QuantileAggregation
	{
		// Token: 0x060001FC RID: 508 RVA: 0x00008E5B File Offset: 0x0000705B
		public QuantileAggregation(params double[] quantiles)
		{
			this.Quantiles = quantiles;
			Array.Sort<double>(this.Quantiles);
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00008E84 File Offset: 0x00007084
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00008E8C File Offset: 0x0000708C
		public double[] Quantiles { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00008E95 File Offset: 0x00007095
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00008E9D File Offset: 0x0000709D
		public double MaxRelativeError { get; set; } = 0.001;
	}
}
