using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000020 RID: 32
	public interface ITraceTransmissionRule
	{
		// Token: 0x06000097 RID: 151
		bool ShouldAccept(ITelemetryTrace telemetryEvent);
	}
}
