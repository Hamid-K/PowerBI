using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000D1 RID: 209
	public interface IEventProvider<T> : IEnumerable<T>, IEnumerable, IQueryProvider, IDisposable where T : PublishedEvent
	{
		// Token: 0x06000297 RID: 663
		T RetrieveEvent(EventLocator eventLocation);

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000298 RID: 664
		ReadOnlyCollection<IMetadataGeneration> MetadataGenerations { get; }

		// Token: 0x06000299 RID: 665
		void Stop();

		// Token: 0x0600029A RID: 666
		void SerializeEvent(IEventSerializer serializationContext, T serializableEvent);
	}
}
