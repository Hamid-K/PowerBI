using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000058 RID: 88
	internal sealed class RateAggregator : Aggregator
	{
		// Token: 0x0600029E RID: 670 RVA: 0x0000ADF8 File Offset: 0x00008FF8
		public override void Update(double value)
		{
			lock (this)
			{
				this._value = value;
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000AE34 File Offset: 0x00009034
		public override IAggregationStatistics Collect()
		{
			IAggregationStatistics aggregationStatistics;
			lock (this)
			{
				double? num = null;
				if (this._prevValue != null)
				{
					num = new double?(this._value - this._prevValue.Value);
				}
				RateStatistics rateStatistics = new RateStatistics(num);
				this._prevValue = new double?(this._value);
				aggregationStatistics = rateStatistics;
			}
			return aggregationStatistics;
		}

		// Token: 0x04000120 RID: 288
		private double? _prevValue;

		// Token: 0x04000121 RID: 289
		private double _value;
	}
}
