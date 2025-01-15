using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x0200007C RID: 124
	public interface IMonitoredErrorSinkServices
	{
		// Token: 0x060003A7 RID: 935
		void Initialize(ISinkServices sinkServices);

		// Token: 0x060003A8 RID: 936
		bool TryGetWindowsEventLogMetadata(WireEventBase publishedEvent, out WindowsEventLogBaseAttribute attribute);

		// Token: 0x060003A9 RID: 937
		string GetFriendlyName(WireEventBase publishedEvent);

		// Token: 0x060003AA RID: 938
		bool DoesEventIndicateActivityCompletedSuccessfully(WireEventBase publishedEvent);

		// Token: 0x060003AB RID: 939
		bool DoesEventIndicateActivityCompletedWithFailure(WireEventBase publishedEvent);
	}
}
