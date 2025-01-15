using System;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200012C RID: 300
	[EventsKit(7213312572748422382L)]
	[PerformanceCountersCategory("Serializers")]
	public interface IMonitoredSerializerEventsKit
	{
		// Token: 0x060007FA RID: 2042
		[Event(8306380215491959851L, 1, Version = 1)]
		[PerformanceCounter(1054125808, "QueuesInSerializer", CounterModifier.Set, "queueCount", CounterType.NumberOfItems)]
		[PerformanceCounter(1055391591, "PendingItemsInSerializer", CounterModifier.Set, "itemsCount", CounterType.NumberOfItems)]
		[PerformanceCounter(421902257, "PeakUsedQueueCapacityInSerializer", CounterModifier.Set, "peakQueueSize", CounterType.NumberOfItems)]
		void NotifyItemsQueuesCountChanged(int queueCount, int itemsCount, int peakQueueSize);
	}
}
