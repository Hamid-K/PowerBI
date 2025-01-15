using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x0200003B RID: 59
	internal class AggregationPeriodSummary
	{
		// Token: 0x06000227 RID: 551 RVA: 0x0000BA52 File Offset: 0x00009C52
		public AggregationPeriodSummary(IReadOnlyList<MetricAggregate> persistentAggregates, IReadOnlyList<MetricAggregate> nonpersistentAggregates)
		{
			this.PersistentAggregates = persistentAggregates;
			this.NonpersistentAggregates = nonpersistentAggregates;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000BA68 File Offset: 0x00009C68
		public IReadOnlyList<MetricAggregate> PersistentAggregates { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000BA70 File Offset: 0x00009C70
		public IReadOnlyList<MetricAggregate> NonpersistentAggregates { get; }
	}
}
