using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000018 RID: 24
	public interface ITelemetryService
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006D RID: 109
		ITelemetryEvent Root { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006E RID: 110
		string SessionId { get; }

		// Token: 0x0600006F RID: 111
		void TraceInfo(string message);

		// Token: 0x06000070 RID: 112
		void AccumulateSummaryBinForEvent(string eventName, int bin);

		// Token: 0x06000071 RID: 113
		void TraceAccumulatedSummary();

		// Token: 0x06000072 RID: 114
		void TraceVerbose(string message);

		// Token: 0x06000073 RID: 115
		void TraceWarning(string message);

		// Token: 0x06000074 RID: 116
		void TraceError(string message);

		// Token: 0x06000075 RID: 117
		void TraceInfo(string message, EventTarget eventTarget);

		// Token: 0x06000076 RID: 118
		void TraceVerbose(string message, EventTarget eventTarget);

		// Token: 0x06000077 RID: 119
		void TraceWarning(string message, EventTarget eventTarget);

		// Token: 0x06000078 RID: 120
		void TraceError(string message, EventTarget eventTarget);

		// Token: 0x06000079 RID: 121
		void Log(ITelemetryEvent telemetryEvent);

		// Token: 0x0600007A RID: 122
		void StartTimedEvent(ITelemetryEvent telemetryEvent);

		// Token: 0x0600007B RID: 123
		void EndTimedEvent(ITelemetryEvent telemetryEvent);

		// Token: 0x0600007C RID: 124
		void Upload();

		// Token: 0x0600007D RID: 125
		bool UploadsRemain();
	}
}
