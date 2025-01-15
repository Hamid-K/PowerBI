using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000082 RID: 130
	internal class MonitoredLowLevelErrorEvent : MonitoredErrorEventBase
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x0000E3FD File Offset: 0x0000C5FD
		protected internal MonitoredLowLevelErrorEvent(WireEventBase publishedEvent, int windowsEventLogId, IMonitoredError monitoredError, string friendlyName)
			: base(publishedEvent, windowsEventLogId, monitoredError, friendlyName)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(windowsEventLogId, "windowsEventLogId");
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000E418 File Offset: 0x0000C618
		public static bool TryCreate(WireEventBase publishedEvent, IMonitoredErrorSinkServices monitoredErrorSinkServices, out IMonitoredEventHandlerVisitor newError)
		{
			newError = null;
			if (publishedEvent != null)
			{
				IMonitoredError monitoredError = publishedEvent as IMonitoredError;
				WindowsEventLogBaseAttribute windowsEventLogBaseAttribute;
				if (monitoredError != null && monitoredError.ErrorCorrelationId.IsError && monitoredErrorSinkServices.TryGetWindowsEventLogMetadata(publishedEvent, out windowsEventLogBaseAttribute))
				{
					newError = new MonitoredLowLevelErrorEvent(publishedEvent, windowsEventLogBaseAttribute.WindowsEventLogId, monitoredError, monitoredErrorSinkServices.GetFriendlyName(publishedEvent));
				}
			}
			return newError != null;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000E468 File Offset: 0x0000C668
		public override void Visit(IMonitoredEventHandler eventHandler)
		{
			eventHandler.HandleUncorrelatedLowLevelError(this);
		}
	}
}
