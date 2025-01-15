using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000127 RID: 295
	public interface IMonitoredActivityContext
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060007EC RID: 2028
		IActivityFactory ActivityFactory { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060007ED RID: 2029
		IMonitoredActivityCompletionModelFactory MonitoredActivityCompletionModelFactory { get; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060007EE RID: 2030
		WorkTicketManager WorkTicketManager { get; }
	}
}
