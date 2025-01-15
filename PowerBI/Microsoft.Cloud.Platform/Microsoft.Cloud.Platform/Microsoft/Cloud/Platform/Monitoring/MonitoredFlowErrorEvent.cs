using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000081 RID: 129
	internal class MonitoredFlowErrorEvent : MonitoredErrorEventBase
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x0000E333 File Offset: 0x0000C533
		protected internal MonitoredFlowErrorEvent(WireEventBase publishedEvent, IMonitoredError monitoredError, int windowsEventLogId, string friendlyName)
			: base(publishedEvent, windowsEventLogId, monitoredError, friendlyName)
		{
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000E340 File Offset: 0x0000C540
		internal bool HasValidWindowsEventLogId
		{
			get
			{
				return base.WindowsEventLogId != 0;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000E34B File Offset: 0x0000C54B
		internal bool IsInsideMonitoringScope
		{
			get
			{
				return new MonitoringScopeId(base.EventId.ElementId.ToString()).Equals(base.MonitoringScope);
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000E36D File Offset: 0x0000C56D
		public MonitoredFlowSuccessEvent ConvertToFlowSuccessEvent()
		{
			return new MonitoredFlowSuccessEvent(base.PublishedEvent, base.WindowsEventLogId, base.FriendlyName);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000E388 File Offset: 0x0000C588
		public static bool TryCreate(WireEventBase publishedEvent, IMonitoredErrorSinkServices monitoredErrorSinkServices, out IMonitoredEventHandlerVisitor newError)
		{
			newError = null;
			if (publishedEvent != null && monitoredErrorSinkServices.DoesEventIndicateActivityCompletedWithFailure(publishedEvent))
			{
				IMonitoredError monitoredError = publishedEvent as IMonitoredError;
				if (monitoredError != null)
				{
					WindowsEventLogBaseAttribute windowsEventLogBaseAttribute;
					monitoredErrorSinkServices.TryGetWindowsEventLogMetadata(publishedEvent, out windowsEventLogBaseAttribute);
					newError = new MonitoredFlowErrorEvent(publishedEvent, monitoredError, (windowsEventLogBaseAttribute != null) ? windowsEventLogBaseAttribute.WindowsEventLogId : 0, monitoredErrorSinkServices.GetFriendlyName(publishedEvent));
				}
				else
				{
					TraceSourceBase<MonitoringTrace>.Tracer.TraceWarning("The following event qualified as 'Activity Completed With Failure' event but is not an IMonitoredError: {0}", new object[] { publishedEvent });
				}
			}
			return newError != null;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		public override void Visit(IMonitoredEventHandler eventHandler)
		{
			eventHandler.HandleUncorrelatedFlowError(this);
		}
	}
}
