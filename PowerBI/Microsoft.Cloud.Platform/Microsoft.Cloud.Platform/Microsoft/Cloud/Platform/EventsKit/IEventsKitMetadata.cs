using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000346 RID: 838
	public interface IEventsKitMetadata
	{
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060018DB RID: 6363
		EventsKitIdentifiers Id { get; }

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060018DC RID: 6364
		IEnumerable<IEventMetadata> Events { get; }

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060018DD RID: 6365
		IEnumerable<IEventMetadata> PublishedEvents { get; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060018DE RID: 6366
		Type EventsKitType { get; }

		// Token: 0x060018DF RID: 6367
		IEventMetadata GetEvent(long eventAttributeId);

		// Token: 0x060018E0 RID: 6368
		string GetSchemaAsString(Type attributeFilter);
	}
}
