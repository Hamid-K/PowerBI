using System;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000AD RID: 173
	internal interface IHeartbeatProvider : IHeartbeatPropertyManager, IDisposable
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600053E RID: 1342
		// (set) Token: 0x0600053F RID: 1343
		string InstrumentationKey { get; set; }

		// Token: 0x06000540 RID: 1344
		void Initialize(TelemetryConfiguration configuration);

		// Token: 0x06000541 RID: 1345
		bool AddHeartbeatProperty(string propertyName, bool overrideDefaultField, string propertyValue, bool isHealthy);

		// Token: 0x06000542 RID: 1346
		bool SetHeartbeatProperty(string propertyName, bool overrideDefaultField, string propertyValue = null, bool? isHealthy = null);
	}
}
