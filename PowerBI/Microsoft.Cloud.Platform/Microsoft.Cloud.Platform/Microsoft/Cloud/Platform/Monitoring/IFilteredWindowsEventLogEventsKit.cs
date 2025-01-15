using System;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.Monitoring
{
	// Token: 0x02000075 RID: 117
	[EventsKit(4031679967389987849L)]
	public interface IFilteredWindowsEventLogEventsKit
	{
		// Token: 0x0600037B RID: 891
		[Event(5360908063798671464L, 1, Version = 1)]
		[PerformanceCounter(588464089, "RateOfStoredFailureDetectionPointEvents", CounterModifier.Increment, "count", CounterType.RateOfCountsPerSecond)]
		void NotifyStoredFailureDetectionPointEventInMemory(int count);
	}
}
