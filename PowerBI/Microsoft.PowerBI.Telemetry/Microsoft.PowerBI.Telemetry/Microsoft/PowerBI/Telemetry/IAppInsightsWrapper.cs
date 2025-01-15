using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x02000006 RID: 6
	public interface IAppInsightsWrapper
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3
		// (set) Token: 0x06000004 RID: 4
		string UserId { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5
		// (set) Token: 0x06000006 RID: 6
		string SessionId { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7
		// (set) Token: 0x06000008 RID: 8
		bool IsReturningUser { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9
		// (set) Token: 0x0600000A RID: 10
		string DeviceId { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11
		// (set) Token: 0x0600000C RID: 12
		string ApplicationVersion { get; set; }

		// Token: 0x0600000D RID: 13
		void TrackEvent(string eventName, DateTime timestamp, Dictionary<string, string> propertiesToAdd);

		// Token: 0x0600000E RID: 14
		void TrackTrace(TraceType traceType, string message, Dictionary<string, string> propertiesToAdd);

		// Token: 0x0600000F RID: 15
		void Flush();
	}
}
