using System;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000033 RID: 51
	internal abstract class Aggregator
	{
		// Token: 0x060001D1 RID: 465
		public abstract void Update(double measurement);

		// Token: 0x060001D2 RID: 466
		public abstract IAggregationStatistics Collect();
	}
}
