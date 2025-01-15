using System;
using Microsoft.Cloud.Platform.Common;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000124 RID: 292
	[EventsKit(1989245259474939597L)]
	[PublishedEvent]
	[Trace(typeof(MonitoredUtilsTrace))]
	public interface IActivityEvents
	{
		// Token: 0x060007D9 RID: 2009
		[Event(3185692291949662412L, 1, Level = EventLevel.Verbose, Version = 1)]
		void FireImplementationActivityStartedEvent();

		// Token: 0x060007DA RID: 2010
		[Event(2129617565985984332L, 2, Level = EventLevel.Verbose, Version = 1)]
		[PerformanceCounter(348207609, "AverageDurationOfTransactions", CounterModifier.Increment, "duration", CounterType.AverageDelta)]
		void FireImplementationActivityEndedEvent(long duration);

		// Token: 0x060007DB RID: 2011
		[Event(1663936593316370084L, 3, Level = EventLevel.Informational, Version = 1)]
		[Logging]
		[Support]
		void FireActivityCorrelationEvent([ParentActivity] Guid parentActivityId);

		// Token: 0x060007DC RID: 2012
		[Event(2114818236649158932L, 4, Level = EventLevel.Verbose, Format = "Extended Activity Correlation. otherActivityId:{0} otherRootActivityId:{1} otherClientActivityId:{2} relationshipToOther:{3}", Version = 1)]
		[Logging]
		[Support]
		void FireExtendedActivityCorrelationEvent(Guid otherActivityId, Guid otherRootActivityId, Guid otherClientActivityId, ActivityCorrelationKind relationshipToOther);
	}
}
