using System;
using Microsoft.ApplicationInsights.Channel;

namespace Microsoft.ApplicationInsights.Extensibility
{
	// Token: 0x02000057 RID: 87
	public interface ITelemetryInitializer
	{
		// Token: 0x060002A2 RID: 674
		void Initialize(ITelemetry telemetry);
	}
}
