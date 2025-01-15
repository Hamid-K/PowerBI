using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common.ClusterNames
{
	// Token: 0x0200015C RID: 348
	[EventsKit(4384026942613582231L)]
	[Trace(typeof(ANCommonTrace))]
	[PublishedEvent]
	public interface IClusterNamesEventsKit
	{
		// Token: 0x06001219 RID: 4633
		[Event(3018816719105234712L, 1, Level = EventLevel.Error, Format = "Cluster names are disabled due to {0}")]
		[FilteredWindowsEventLog(32011)]
		void NotifyClusterNamesDisabled(MonitoredException exception);
	}
}
