using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001F RID: 31
	public class TelemetryTrace : ITelemetryTrace
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00002D65 File Offset: 0x00000F65
		public TelemetryTrace(string message, EventTarget target)
		{
			this.Message = message;
			this.EventTarget = target;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002D7B File Offset: 0x00000F7B
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00002D83 File Offset: 0x00000F83
		public string Message { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002D8C File Offset: 0x00000F8C
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00002D94 File Offset: 0x00000F94
		public EventTarget EventTarget { get; private set; }
	}
}
