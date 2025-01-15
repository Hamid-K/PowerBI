using System;
using System.Collections.Generic;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200003B RID: 59
	// (Invoke) Token: 0x060001EA RID: 490
	internal delegate bool AggregatorLookupFunc<TAggregator>(ReadOnlySpan<KeyValuePair<string, object>> labels, out TAggregator aggregator);
}
