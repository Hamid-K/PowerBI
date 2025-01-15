using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000036 RID: 54
	internal sealed class HistogramStatistics : IAggregationStatistics
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x00008424 File Offset: 0x00006624
		internal HistogramStatistics(QuantileValue[] quantiles)
		{
			this.Quantiles = quantiles;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00008433 File Offset: 0x00006633
		public QuantileValue[] Quantiles { get; }
	}
}
