using System;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000353 RID: 851
	internal class GeneratedEventsKitInstances<T>
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600193C RID: 6460 RVA: 0x0005DE32 File Offset: 0x0005C032
		// (set) Token: 0x0600193D RID: 6461 RVA: 0x0005DE3A File Offset: 0x0005C03A
		public T EventsKitInstance { get; private set; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x0005DE43 File Offset: 0x0005C043
		// (set) Token: 0x0600193F RID: 6463 RVA: 0x0005DE4B File Offset: 0x0005C04B
		public EventSource EventSourceInstance { get; private set; }

		// Token: 0x06001940 RID: 6464 RVA: 0x0005DE54 File Offset: 0x0005C054
		public GeneratedEventsKitInstances(T eventsKit, EventSource eventSource)
		{
			this.EventsKitInstance = eventsKit;
			this.EventSourceInstance = eventSource;
		}
	}
}
