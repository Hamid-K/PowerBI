using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000086 RID: 134
	internal class MonitoredFlowSuccessEvent : MonitoredEventBase
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x0000E6FB File Offset: 0x0000C8FB
		internal MonitoredFlowSuccessEvent(MonitoredEventBase otherEvent)
			: base(otherEvent)
		{
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000E704 File Offset: 0x0000C904
		internal MonitoredFlowSuccessEvent(WireEventBase publishedEvent, int windowsEventLogId, string friendlyName)
			: base(publishedEvent, windowsEventLogId, friendlyName)
		{
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000E710 File Offset: 0x0000C910
		public static bool TryCreate(WireEventBase publishedEvent, IMonitoredErrorSinkServices monitoredErrorSinkServices, out IMonitoredEventHandlerVisitor newEvent)
		{
			newEvent = null;
			if (publishedEvent != null && monitoredErrorSinkServices.DoesEventIndicateActivityCompletedSuccessfully(publishedEvent))
			{
				WindowsEventLogBaseAttribute windowsEventLogBaseAttribute;
				monitoredErrorSinkServices.TryGetWindowsEventLogMetadata(publishedEvent, out windowsEventLogBaseAttribute);
				newEvent = new MonitoredFlowSuccessEvent(publishedEvent, (windowsEventLogBaseAttribute != null) ? windowsEventLogBaseAttribute.WindowsEventLogId : 0, monitoredErrorSinkServices.GetFriendlyName(publishedEvent));
			}
			return newEvent != null;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000E756 File Offset: 0x0000C956
		public override void Visit(IMonitoredEventHandler eventHandler)
		{
			eventHandler.HandleFlowSuccess(this);
		}
	}
}
