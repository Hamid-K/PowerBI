using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000520 RID: 1312
	[EventsKit(6577832637179675126L)]
	[Trace(typeof(CommonTrace))]
	[PublishedEvent]
	public interface IEntitySerializerEventsKit
	{
		// Token: 0x0600288B RID: 10379
		[Event(8379755413521640017L, 1, Level = EventLevel.Error, Format = "Serialization operation '{1}' on type '{0}' failed due to: {2}", Version = 1)]
		[FilteredWindowsEventLog(3901)]
		void NotifySerializationError(string typeName, string direction, MonitoredException exception);
	}
}
