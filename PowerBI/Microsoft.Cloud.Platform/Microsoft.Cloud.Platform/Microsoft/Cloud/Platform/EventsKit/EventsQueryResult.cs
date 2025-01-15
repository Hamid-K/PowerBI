using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200035A RID: 858
	public class EventsQueryResult<T> where T : WireEventBase
	{
		// Token: 0x06001967 RID: 6503 RVA: 0x0005E293 File Offset: 0x0005C493
		public EventsQueryResult(IEnumerable<T> events)
		{
			this.Events = events;
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x0005E2A2 File Offset: 0x0005C4A2
		// (set) Token: 0x06001969 RID: 6505 RVA: 0x0005E2AA File Offset: 0x0005C4AA
		public IEnumerable<T> Events { get; private set; }
	}
}
