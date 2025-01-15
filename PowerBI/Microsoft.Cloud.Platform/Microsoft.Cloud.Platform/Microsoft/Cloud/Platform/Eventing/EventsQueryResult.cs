using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.Eventing
{
	// Token: 0x02000399 RID: 921
	public class EventsQueryResult
	{
		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x0006C1F2 File Offset: 0x0006A3F2
		// (set) Token: 0x06001C66 RID: 7270 RVA: 0x0006C1FA File Offset: 0x0006A3FA
		public IEventingRepositoryContinuation Continuation { get; private set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x0006C203 File Offset: 0x0006A403
		// (set) Token: 0x06001C68 RID: 7272 RVA: 0x0006C20B File Offset: 0x0006A40B
		public IEnumerable<EtwEvent> Events { get; private set; }

		// Token: 0x06001C69 RID: 7273 RVA: 0x0006C214 File Offset: 0x0006A414
		public EventsQueryResult(IEnumerable<EtwEvent> events, IEventingRepositoryContinuation continuation)
		{
			this.Continuation = continuation;
			this.Events = events;
		}
	}
}
