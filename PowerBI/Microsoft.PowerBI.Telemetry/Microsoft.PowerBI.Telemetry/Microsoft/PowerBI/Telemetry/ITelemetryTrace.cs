using System;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001E RID: 30
	public interface ITelemetryTrace
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000090 RID: 144
		string Message { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000091 RID: 145
		EventTarget EventTarget { get; }
	}
}
