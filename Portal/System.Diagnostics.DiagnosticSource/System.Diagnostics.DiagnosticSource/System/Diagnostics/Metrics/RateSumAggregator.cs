using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000057 RID: 87
	internal sealed class RateSumAggregator : Aggregator
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000AD54 File Offset: 0x00008F54
		public override void Update(double value)
		{
			lock (this)
			{
				this._sum += value;
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000AD98 File Offset: 0x00008F98
		public override IAggregationStatistics Collect()
		{
			IAggregationStatistics aggregationStatistics;
			lock (this)
			{
				RateStatistics rateStatistics = new RateStatistics(new double?(this._sum));
				this._sum = 0.0;
				aggregationStatistics = rateStatistics;
			}
			return aggregationStatistics;
		}

		// Token: 0x0400011F RID: 287
		private double _sum;
	}
}
