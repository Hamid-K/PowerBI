using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000129 RID: 297
	[EventsKit(3298630767783105324L)]
	[PerformanceCountersCategory("Queues")]
	public interface IMonitoredManagedQueueEventsKit
	{
		// Token: 0x060007F2 RID: 2034
		[Event(9067503249673693829L, 1, Version = 1)]
		[PerformanceCounter(200010472, "UsedQueueCapacity", CounterModifier.Set, "count", CounterType.NumberOfItems)]
		[PerformanceCounter(1235322776, "UsedPercentOfQueueCapacity", CounterModifier.Set, "maxCapacity==0?0:(100*count)/maxCapacity", CounterType.NumberOfItems)]
		[PerformanceCounter(394721741, "AvailableQueueCapacity", CounterModifier.Set, "maxCapacity-count", CounterType.NumberOfItems)]
		[PerformanceCounter(2068997003, "AvailablePercentOfQueueCapacity", CounterModifier.Set, "maxCapacity==0?0:(100*(maxCapacity-count))/maxCapacity", CounterType.NumberOfItems)]
		void NotifyCapacityChanged(int maxCapacity, int count);

		// Token: 0x060007F3 RID: 2035
		[Event(8740067403144722428L, 2, Version = 1)]
		void NotifyQueueFull(string queueId, int capacity, IMonitoredError error);
	}
}
