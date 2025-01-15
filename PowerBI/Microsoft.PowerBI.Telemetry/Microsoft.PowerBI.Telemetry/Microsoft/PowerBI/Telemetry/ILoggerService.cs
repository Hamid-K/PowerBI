using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000017 RID: 23
	public interface ILoggerService
	{
		// Token: 0x06000063 RID: 99
		void Initialize(HostData hostData);

		// Token: 0x06000064 RID: 100
		void TraceInfo(ITelemetryTrace telemetryTrace);

		// Token: 0x06000065 RID: 101
		void TraceVerbose(ITelemetryTrace telemetryTrace);

		// Token: 0x06000066 RID: 102
		void TraceWarning(ITelemetryTrace telemetryTrace);

		// Token: 0x06000067 RID: 103
		void TraceError(ITelemetryTrace telemetryTrace);

		// Token: 0x06000068 RID: 104
		void Log(ITelemetryEvent telemetryEvent);

		// Token: 0x06000069 RID: 105
		void StartTimedEvent(ITelemetryEvent telemetryEvent);

		// Token: 0x0600006A RID: 106
		void EndTimedEvent(ITelemetryEvent telemetryEvent);

		// Token: 0x0600006B RID: 107
		bool UploadsRemaining();

		// Token: 0x0600006C RID: 108
		void Upload();
	}
}
