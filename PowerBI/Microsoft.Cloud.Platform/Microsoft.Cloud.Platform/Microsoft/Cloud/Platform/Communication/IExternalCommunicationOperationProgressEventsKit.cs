using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004A8 RID: 1192
	[EventsKit(5783592344097960930L)]
	[Trace(typeof(CommunicationFrameworkTrace))]
	[PublishedEvent]
	public interface IExternalCommunicationOperationProgressEventsKit
	{
		// Token: 0x06002499 RID: 9369
		[Event(5643147924124456664L, 1, Level = EventLevel.Verbose, Version = 1)]
		void NotifyExternalOperationStarted(string serviceDetails);

		// Token: 0x0600249A RID: 9370
		[Event(2644330861243400523L, 2, Level = EventLevel.Verbose, Version = 1)]
		[PerformanceCounter(2016638994, "PercentageOfExternalConnectionFailures", CounterModifier.Increment, "0", CounterType.AverageDelta)]
		[PerformanceCounter(1496582592, "NumberOfExternalSuccessfulConnections", CounterModifier.Increment, CounterType.CounterDelta)]
		void NotifyExternalOperationEndedSuccessfully(string serviceDetails);

		// Token: 0x0600249B RID: 9371
		[Event(2192895439859665275L, 3, Level = EventLevel.Error, Format = "Communication timeout between element {2} and external service at address {4} using contract {1} due to: {5}", Version = 1)]
		[PerformanceCounter(2016638994, "PercentageOfExternalConnectionFailures", CounterModifier.Increment, "100", CounterType.AverageDelta)]
		[PerformanceCounter(1123786540, "NumberOfExternalConnectionFailures", CounterModifier.Increment, CounterType.CounterDelta)]
		[FilteredWindowsEventLog(204)]
		[Logging]
		void NotifyExternalOperationTimeoutFailure(string serviceName, string contract, string clientElementInstanceId, string serverElementInstanceId, string serverEndpointAddress, MonitoredException exception);

		// Token: 0x0600249C RID: 9372
		[Event(8524114379609859385L, 4, Level = EventLevel.Error, Format = "Communication error between element {2} and external service at address {4} using contract {1} due to: {5}", Version = 1)]
		[PerformanceCounter(2016638994, "PercentageOfExternalConnectionFailures", CounterModifier.Increment, "100", CounterType.AverageDelta)]
		[PerformanceCounter(1123786540, "NumberOfExternalConnectionFailures", CounterModifier.Increment, CounterType.CounterDelta)]
		[FilteredWindowsEventLog(203)]
		[Logging]
		void NotifyExternalOperationFailed(string serviceName, string contract, string clientElementInstanceId, string serverElementInstanceId, string serverEndpointAddress, MonitoredException exception);
	}
}
