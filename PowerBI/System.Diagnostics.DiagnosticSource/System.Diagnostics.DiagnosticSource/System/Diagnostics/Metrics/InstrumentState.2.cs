using System;
using System.Collections.Generic;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000046 RID: 70
	internal sealed class InstrumentState<TAggregator> : InstrumentState where TAggregator : Aggregator
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00009886 File Offset: 0x00007A86
		public InstrumentState(Func<TAggregator> createAggregatorFunc)
		{
			this._aggregatorStore = new AggregatorStore<TAggregator>(createAggregatorFunc);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000989A File Offset: 0x00007A9A
		public override void Collect(Instrument instrument, Action<LabeledAggregationStatistics> aggregationVisitFunc)
		{
			this._aggregatorStore.Collect(aggregationVisitFunc);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000098A8 File Offset: 0x00007AA8
		[SecuritySafeCritical]
		public override void Update(double measurement, ReadOnlySpan<KeyValuePair<string, object>> labels)
		{
			TAggregator aggregator = this._aggregatorStore.GetAggregator(labels);
			TAggregator taggregator = aggregator;
			if (taggregator == null)
			{
				return;
			}
			taggregator.Update(measurement);
		}

		// Token: 0x040000FC RID: 252
		private AggregatorStore<TAggregator> _aggregatorStore;
	}
}
