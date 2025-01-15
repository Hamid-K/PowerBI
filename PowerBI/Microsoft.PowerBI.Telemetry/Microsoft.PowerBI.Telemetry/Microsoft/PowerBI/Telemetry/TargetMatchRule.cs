using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000021 RID: 33
	public class TargetMatchRule : ITraceTransmissionRule
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00002D9D File Offset: 0x00000F9D
		public TargetMatchRule(EventTarget target)
		{
			this.Target = target;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00002DAC File Offset: 0x00000FAC
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00002DB4 File Offset: 0x00000FB4
		private EventTarget Target { get; set; }

		// Token: 0x0600009B RID: 155 RVA: 0x00002DBD File Offset: 0x00000FBD
		public bool ShouldAccept(ITelemetryTrace telemetryTrace)
		{
			return telemetryTrace.EventTarget == this.Target;
		}
	}
}
