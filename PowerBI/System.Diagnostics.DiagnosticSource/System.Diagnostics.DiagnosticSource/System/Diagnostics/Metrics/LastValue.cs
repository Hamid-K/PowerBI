using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000047 RID: 71
	internal sealed class LastValue : Aggregator
	{
		// Token: 0x0600022E RID: 558 RVA: 0x000098D3 File Offset: 0x00007AD3
		public override void Update(double value)
		{
			this._lastValue = new double?(value);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000098E4 File Offset: 0x00007AE4
		public override IAggregationStatistics Collect()
		{
			IAggregationStatistics aggregationStatistics;
			lock (this)
			{
				LastValueStatistics lastValueStatistics = new LastValueStatistics(this._lastValue);
				this._lastValue = null;
				aggregationStatistics = lastValueStatistics;
			}
			return aggregationStatistics;
		}

		// Token: 0x040000FD RID: 253
		private double? _lastValue;
	}
}
