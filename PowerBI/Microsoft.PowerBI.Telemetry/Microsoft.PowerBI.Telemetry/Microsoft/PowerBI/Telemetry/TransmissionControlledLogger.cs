using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000027 RID: 39
	public class TransmissionControlledLogger : ILoggerService
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003C5B File Offset: 0x00001E5B
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00003C63 File Offset: 0x00001E63
		public ILoggerService Logger { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003C6C File Offset: 0x00001E6C
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00003C74 File Offset: 0x00001E74
		public IEventTransmissionRule EventTransmissionRule { get; private set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003C7D File Offset: 0x00001E7D
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00003C85 File Offset: 0x00001E85
		public ITraceTransmissionRule TraceTransmissionRule { get; private set; }

		// Token: 0x060000EB RID: 235 RVA: 0x00003C8E File Offset: 0x00001E8E
		public TransmissionControlledLogger(ILoggerService logger, IEventTransmissionRule eventTransmissionRule, ITraceTransmissionRule traceTransmissionRule)
		{
			this.Logger = logger;
			this.EventTransmissionRule = eventTransmissionRule;
			this.TraceTransmissionRule = traceTransmissionRule;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003CAB File Offset: 0x00001EAB
		public void Initialize(HostData hostData)
		{
			this.Logger.Initialize(hostData);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003CB9 File Offset: 0x00001EB9
		public void TraceInfo(ITelemetryTrace telemetryTrace)
		{
			if (this.TraceTransmissionRule.ShouldAccept(telemetryTrace))
			{
				this.Logger.TraceInfo(telemetryTrace);
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003CD5 File Offset: 0x00001ED5
		public void TraceVerbose(ITelemetryTrace telemetryTrace)
		{
			if (this.TraceTransmissionRule.ShouldAccept(telemetryTrace))
			{
				this.Logger.TraceVerbose(telemetryTrace);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003CF1 File Offset: 0x00001EF1
		public void TraceWarning(ITelemetryTrace telemetryTrace)
		{
			if (this.TraceTransmissionRule.ShouldAccept(telemetryTrace))
			{
				this.Logger.TraceWarning(telemetryTrace);
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003D0D File Offset: 0x00001F0D
		public void TraceError(ITelemetryTrace telemetryTrace)
		{
			if (this.TraceTransmissionRule.ShouldAccept(telemetryTrace))
			{
				this.Logger.TraceError(telemetryTrace);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00003D29 File Offset: 0x00001F29
		public void Log(ITelemetryEvent telemetryEvent)
		{
			if (this.EventTransmissionRule.ShouldAccept(telemetryEvent))
			{
				this.Logger.Log(telemetryEvent);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003D45 File Offset: 0x00001F45
		public void StartTimedEvent(ITelemetryEvent telemetryEvent)
		{
			if (this.EventTransmissionRule.ShouldAccept(telemetryEvent))
			{
				this.Logger.StartTimedEvent(telemetryEvent);
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003D61 File Offset: 0x00001F61
		public void EndTimedEvent(ITelemetryEvent telemetryEvent)
		{
			if (this.EventTransmissionRule.ShouldAccept(telemetryEvent))
			{
				this.Logger.EndTimedEvent(telemetryEvent);
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003D7D File Offset: 0x00001F7D
		public bool UploadsRemaining()
		{
			return this.Logger.UploadsRemaining();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003D8A File Offset: 0x00001F8A
		public void Upload()
		{
			this.Logger.Upload();
		}
	}
}
