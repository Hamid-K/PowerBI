using System;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x02000569 RID: 1385
	internal class PerformanceAggregatorSummary
	{
		// Token: 0x17001E1C RID: 7708
		// (get) Token: 0x0600509C RID: 20636 RVA: 0x00152BC7 File Offset: 0x00150DC7
		internal float Avg
		{
			get
			{
				if (this.Count <= 0f)
				{
					return 0f;
				}
				return this.Sum / this.Count;
			}
		}

		// Token: 0x0600509D RID: 20637 RVA: 0x00152BE9 File Offset: 0x00150DE9
		internal PerformanceAggregatorSummary(PerformanceMetricType metricType)
		{
			this.MetricType = metricType;
		}

		// Token: 0x04002890 RID: 10384
		internal readonly PerformanceMetricType MetricType;

		// Token: 0x04002891 RID: 10385
		internal float Sum;

		// Token: 0x04002892 RID: 10386
		internal float Count;

		// Token: 0x04002893 RID: 10387
		internal float Max = float.MinValue;

		// Token: 0x04002894 RID: 10388
		internal float Min = float.MaxValue;
	}
}
