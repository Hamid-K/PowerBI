using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Eventing.Base;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000345 RID: 837
	public interface IEventsKitExplorer
	{
		// Token: 0x17000351 RID: 849
		// (get) Token: 0x060018D1 RID: 6353
		IEnumerable<IEventsKitMetadata> EventKits { get; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060018D2 RID: 6354
		IEnumerable<IEventMetadata> Events { get; }

		// Token: 0x060018D3 RID: 6355
		IEnumerable<IEventsKitMetadata> GetEventKitsInClosure(string fileName);

		// Token: 0x060018D4 RID: 6356
		IEventsKitMetadata GetEventsKit(long id);

		// Token: 0x060018D5 RID: 6357
		IEventMetadata GetEventMetadata(EventsKitIdentifiers id);

		// Token: 0x060018D6 RID: 6358
		IEventMetadata GetEventMetadata(Guid id);

		// Token: 0x060018D7 RID: 6359
		bool TryGetEventMetadata(long eventId, out IEventMetadata evm);

		// Token: 0x060018D8 RID: 6360
		string GetFullEventsKitSchema();

		// Token: 0x060018D9 RID: 6361
		string GetFullEventsKitSchema(Type useOnlyEventsWithAttributeType);

		// Token: 0x060018DA RID: 6362
		IEnumerable<T> GetAttributes<T>(EventsKitIdentifiers id) where T : Attribute;
	}
}
