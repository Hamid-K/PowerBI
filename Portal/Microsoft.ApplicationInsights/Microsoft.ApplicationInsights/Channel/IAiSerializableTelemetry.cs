using System;

namespace Microsoft.ApplicationInsights.Channel
{
	// Token: 0x020000E0 RID: 224
	internal interface IAiSerializableTelemetry
	{
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000831 RID: 2097
		string TelemetryName { get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000832 RID: 2098
		string BaseType { get; }
	}
}
