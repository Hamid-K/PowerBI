using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000D2 RID: 210
	public interface ISupportMetrics
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000743 RID: 1859
		IDictionary<string, double> Metrics { get; }
	}
}
