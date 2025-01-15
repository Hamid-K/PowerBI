using System;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000352 RID: 850
	internal class GeneratedEventsKitImplementationTypes
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x0005DDFA File Offset: 0x0005BFFA
		// (set) Token: 0x06001938 RID: 6456 RVA: 0x0005DE02 File Offset: 0x0005C002
		public Type EventSourceType { get; private set; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x0005DE0B File Offset: 0x0005C00B
		// (set) Token: 0x0600193A RID: 6458 RVA: 0x0005DE13 File Offset: 0x0005C013
		public Type EventsKitType { get; private set; }

		// Token: 0x0600193B RID: 6459 RVA: 0x0005DE1C File Offset: 0x0005C01C
		public GeneratedEventsKitImplementationTypes(Type eventSourceType, Type eventsKitType)
		{
			this.EventSourceType = eventSourceType;
			this.EventsKitType = eventsKitType;
		}
	}
}
