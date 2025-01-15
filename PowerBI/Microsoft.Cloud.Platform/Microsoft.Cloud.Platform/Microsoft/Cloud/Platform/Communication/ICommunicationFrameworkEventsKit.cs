using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004A3 RID: 1187
	[EventsKit(963132599687448756L)]
	[Trace(typeof(CommunicationFrameworkTrace))]
	[PublishedEvent]
	public interface ICommunicationFrameworkEventsKit
	{
		// Token: 0x06002488 RID: 9352
		[Event(8482263503120691900L, 1, Level = EventLevel.Error, Format = "Invalid property '{1}' in '{0}' section in CommunicationConfiguration due to: '{2}'.", Version = 1)]
		[FilteredWindowsEventLog(6002)]
		void NotifyInvalidConfiguration(string configurationSection, string invalidProperty, MonitoredException exception);

		// Token: 0x06002489 RID: 9353
		[Event(1779683990092051695L, 2, Level = EventLevel.Error, Format = "Failed generating proxy for service '{0}' due to: '{1}'.", Version = 1)]
		[FilteredWindowsEventLog(6000)]
		void NotifyProxyInitializationError(string serviceId, MonitoredException exception);

		// Token: 0x0600248A RID: 9354
		[Event(3322766262216192584L, 3, Level = EventLevel.Error, Format = "Could not resolve service '{0}' in router '{1}' due to: '{2}'", Version = 1)]
		[FilteredWindowsEventLog(6004)]
		void NotifyRouterResolveError(string destinationService, string routerType, MonitoredException exception);

		// Token: 0x0600248B RID: 9355
		[Event(5196987506114518675L, 4, Level = EventLevel.Error, Format = "Service threw an unexpected fault and will be shut down. Details: contract: '{0}', operation: '{1}' due to: '{2}'.", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 6003)]
		void NotifyUnknownExceptionThrownByServer(string contract, string operation, MonitoredException exception);

		// Token: 0x0600248C RID: 9356
		[Event(5127333208459023790L, 5, Level = EventLevel.Error, Format = "Security error occurred due to: '{0}'.", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 6005)]
		void NotifySecurityError(MonitoredException exception);
	}
}
