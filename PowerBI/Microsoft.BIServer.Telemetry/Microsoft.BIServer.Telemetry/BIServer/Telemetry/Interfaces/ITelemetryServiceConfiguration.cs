using System;
using System.Collections.Generic;

namespace Microsoft.BIServer.Telemetry.Interfaces
{
	// Token: 0x02000006 RID: 6
	public interface ITelemetryServiceConfiguration
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24
		string InstrumentationKey { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25
		bool IsTelemetryEnabled { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26
		string Build { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27
		string Product { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28
		Dictionary<string, string> AdditionalProperties { get; }
	}
}
