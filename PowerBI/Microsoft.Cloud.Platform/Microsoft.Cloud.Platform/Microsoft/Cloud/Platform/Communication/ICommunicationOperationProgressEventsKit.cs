using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004A4 RID: 1188
	[EventsKit(8694006561590654409L)]
	[PerformanceCountersCategory("Communication")]
	[Trace(typeof(CommunicationFrameworkTrace))]
	[PublishedEvent]
	public interface ICommunicationOperationProgressEventsKit
	{
		// Token: 0x0600248D RID: 9357
		[Event(2360799140050765817L, 1, Level = EventLevel.Verbose, Format = "Operation started, service details : '{0}' server endpoint : '{1}' endpoint identifier : '{2}'", Version = 1)]
		void NotifyOperationStarted(string serviceDetails, string serverEndpointAddress, string endpointIdentifier);

		// Token: 0x0600248E RID: 9358
		[Event(4815033024235765614L, 2, Level = EventLevel.Verbose, Format = "Operation ended successfully, service details : '{0}' server endpoint : '{1}' endpoint identifier : '{2}'", Version = 1)]
		[PerformanceCounter(336886572, "PercentageOfInternalConnectionFailures", CounterModifier.Increment, "0", CounterType.AverageDelta)]
		[PerformanceCounter(1865964133, "NumberOfInternalSuccessfulConnections", CounterModifier.Increment, CounterType.CounterDelta)]
		void NotifyOperationEndedSuccessfully(string serviceDetails, string serverEndpointAddress, string endpointIdentifier);

		// Token: 0x0600248F RID: 9359
		[Event(6476131244251790559L, 3, Level = EventLevel.Warning, Format = "Communication error on service '{0}' and contract '{1}' between element '{2}' to '{3}' due to: '{4}' endpoint identifier : '{5}'", Version = 1)]
		[PerformanceCounter(336886572, "PercentageOfInternalConnectionFailures", CounterModifier.Increment, "100", CounterType.AverageDelta)]
		[PerformanceCounter(1965203695, "NumberOfInternalConnectionFailures", CounterModifier.Increment, CounterType.CounterDelta)]
		[WindowsEventLog(EventLogEntryType.Warning, 6001)]
		[Logging]
		void NotifyOperationFailed(string serviceName, string contract, string clientElementInstanceId, string serverEndpointAddress, MonitoredException exception, string endpointIdentifier);

		// Token: 0x06002490 RID: 9360
		[Event(1879887946963661129L, 4, Level = EventLevel.Error, Version = 1)]
		[Logging]
		void NotifyRoutingOperationError(string contract, MonitoredException exception);

		// Token: 0x06002491 RID: 9361
		[Event(8541017272527601931L, 5, Level = EventLevel.Error, Version = 1)]
		[Logging]
		void NotifyOperationRetryCountExceeded(string serviceName, string contract, string clientElementInstanceId, MonitoredException exception);
	}
}
