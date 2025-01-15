using System;
using System.Collections.Generic;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000045 RID: 69
	internal abstract class InstrumentState
	{
		// Token: 0x06000228 RID: 552
		[SecuritySafeCritical]
		public abstract void Update(double measurement, ReadOnlySpan<KeyValuePair<string, object>> labels);

		// Token: 0x06000229 RID: 553
		public abstract void Collect(Instrument instrument, Action<LabeledAggregationStatistics> aggregationVisitFunc);
	}
}
