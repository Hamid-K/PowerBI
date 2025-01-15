using System;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000332 RID: 818
	[Trace(typeof(EventingOnBehalfOfTrace))]
	[PublishedEvent(PublishTo = PublishEventTo.PublishToEtw)]
	[EventsKit(3606054181238014444L)]
	public interface IOnBehalfOfEventsKit
	{
		// Token: 0x06001820 RID: 6176
		[Event(3040439000390277252L, 1, Version = 1)]
		[Logging]
		[Reporting]
		[Support]
		void FireOnBehalfOfJsonEvent(string jsonEvent);
	}
}
