using System;
using System.Collections.Generic;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000037 RID: 55
	internal sealed class LabeledAggregationStatistics
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x0000843B File Offset: 0x0000663B
		public LabeledAggregationStatistics(IAggregationStatistics stats, params KeyValuePair<string, string>[] labels)
		{
			this.AggregationStatistics = stats;
			this.Labels = labels;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00008451 File Offset: 0x00006651
		public KeyValuePair<string, string>[] Labels { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00008459 File Offset: 0x00006659
		public IAggregationStatistics AggregationStatistics { get; }
	}
}
