using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000059 RID: 89
	internal sealed class RateStatistics : IAggregationStatistics
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x0000AEBC File Offset: 0x000090BC
		public RateStatistics(double? delta)
		{
			this.Delta = delta;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000AECB File Offset: 0x000090CB
		public double? Delta { get; }
	}
}
