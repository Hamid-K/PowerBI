using System;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.Eventing.Etw
{
	// Token: 0x020003D8 RID: 984
	public interface IEtwSessionsManager
	{
		// Token: 0x06001E4D RID: 7757
		void RegisterEventSource(EventSource eventSource);

		// Token: 0x06001E4E RID: 7758
		void Initialize(IEtwSessionsManagerEventsKit eventsKit);

		// Token: 0x06001E4F RID: 7759
		void NotifyEventWriteFailure(string eventName, string exceptionType, string exceptionMessage, int failuresCount);
	}
}
