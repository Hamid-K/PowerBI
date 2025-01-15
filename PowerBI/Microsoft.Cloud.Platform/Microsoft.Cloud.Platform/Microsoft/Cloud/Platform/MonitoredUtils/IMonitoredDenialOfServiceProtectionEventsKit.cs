using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000128 RID: 296
	[EventsKit(8665033522184336491L)]
	[PerformanceCountersCategory("DenialOfServiceProtection")]
	[Trace(typeof(DenialOfServiceProtectionTrace))]
	public interface IMonitoredDenialOfServiceProtectionEventsKit
	{
		// Token: 0x060007EF RID: 2031
		[Event(7523570092378505529L, 1, Version = 1)]
		[PerformanceCounter(493514245, "NumberOfBlockings", CounterModifier.Increment, CounterType.CounterDelta)]
		void NotifyBlocking(string key, DateTime blockingEndTime);

		// Token: 0x060007F0 RID: 2032
		[Event(8611078779028050590L, 2, Level = EventLevel.Informational, Format = "DoSP added key '{0}' to blocking after reaching {1} violation events per {2}. Will be blocked till {3}.", Version = 1)]
		[PublishedEvent]
		[PerformanceCounter(1040152327, "NumberOfBlockedKeys", CounterModifier.Increment, CounterType.CounterDelta)]
		[PerformanceCounter(1046641076, "CurrentlyBlockedKeys", CounterModifier.Set, "currentlyBlockedKeys", CounterType.NumberOfItems)]
		void NotifyKeyAddedToBlocking(string key, int violationEventsPerProbation, string probationDuration, DateTime blockingEndTime, int currentlyBlockedKeys);

		// Token: 0x060007F1 RID: 2033
		[Event(8324713237909212632L, 3, Level = EventLevel.Informational, Format = "DoSP removed key '{0}' from blocking.", Version = 1)]
		[PublishedEvent]
		[PerformanceCounter(1040152327, "NumberOfUnblockedKeys", CounterModifier.Increment, CounterType.CounterDelta)]
		[PerformanceCounter(1046641076, "CurrentlyBlockedKeys", CounterModifier.Set, "currentlyBlockedKeys", CounterType.NumberOfItems)]
		void NotifyKeyRemovedFromBlocking(string key, int currentlyBlockedKeys);
	}
}
