using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200000B RID: 11
	public interface IEventTransmissionRule
	{
		// Token: 0x06000034 RID: 52
		bool ShouldAccept(ITelemetryEvent telemetryEvent);
	}
}
