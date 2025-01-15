using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004DF RID: 1247
	[EventsKit(2004382701632643839L)]
	[Trace(typeof(CommunicationFrameworkTrace))]
	[PublishedEvent]
	public interface IEcfListenerEventsKit
	{
		// Token: 0x060025DA RID: 9690
		[Event(9187439879130518456L, 1, Level = EventLevel.Error, Format = "Invalid operation context", Version = 1)]
		[FilteredWindowsEventLog(6006)]
		void NotifyInvalidOperationContext(MonitoredException exception);

		// Token: 0x060025DB RID: 9691
		[Event(361395201958223931L, 2, Level = EventLevel.Informational, Format = "New incoming request arrived to endpoint [{0}]. RequestLength: [{1}], RequestUri: [{2}], ClientAddress: [{3}], RequestHeaders: [{4}]", Version = 1)]
		[Reporting]
		void NotifyNewIncomingRequestEvent(string endpointIdentifier, long requestLength, string requestUri, string clientAddress, string requestHeaders);
	}
}
