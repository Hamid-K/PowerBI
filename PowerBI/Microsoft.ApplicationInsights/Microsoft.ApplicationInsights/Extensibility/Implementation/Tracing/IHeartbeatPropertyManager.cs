using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing
{
	// Token: 0x020000AC RID: 172
	public interface IHeartbeatPropertyManager
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000536 RID: 1334
		// (set) Token: 0x06000537 RID: 1335
		bool IsHeartbeatEnabled { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000538 RID: 1336
		IList<string> ExcludedHeartbeatPropertyProviders { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000539 RID: 1337
		// (set) Token: 0x0600053A RID: 1338
		TimeSpan HeartbeatInterval { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600053B RID: 1339
		IList<string> ExcludedHeartbeatProperties { get; }

		// Token: 0x0600053C RID: 1340
		bool AddHeartbeatProperty(string propertyName, string propertyValue, bool isHealthy);

		// Token: 0x0600053D RID: 1341
		bool SetHeartbeatProperty(string propertyName, string propertyValue = null, bool? isHealthy = null);
	}
}
