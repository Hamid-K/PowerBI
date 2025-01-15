using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200001C RID: 28
	public interface ITelemetryEvent
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000081 RID: 129
		string Name { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000082 RID: 130
		DateTime Time { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000083 RID: 131
		TelemetryUse Use { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000084 RID: 132
		string Id { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000085 RID: 133
		Dictionary<string, string> Properties { get; }
	}
}
