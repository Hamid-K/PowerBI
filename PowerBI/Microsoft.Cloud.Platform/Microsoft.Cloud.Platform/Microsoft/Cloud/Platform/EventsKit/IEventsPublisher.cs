using System;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000348 RID: 840
	public interface IEventsPublisher
	{
		// Token: 0x060018F1 RID: 6385
		void PublishEvent(WireEventBase firedEvent);
	}
}
