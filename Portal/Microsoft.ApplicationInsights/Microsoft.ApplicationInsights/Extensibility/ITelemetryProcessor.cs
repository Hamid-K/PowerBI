using System;
using Microsoft.ApplicationInsights.Channel;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x02000059 RID: 89
	public interface ITelemetryProcessor
	{
		// Token: 0x060002A4 RID: 676
		void Process(ITelemetry item);
	}
}
