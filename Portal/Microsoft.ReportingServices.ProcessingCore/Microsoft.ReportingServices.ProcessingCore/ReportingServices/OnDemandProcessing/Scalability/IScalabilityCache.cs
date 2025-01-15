using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000885 RID: 2181
	public interface IScalabilityCache : PersistenceHelper, IDisposable
	{
		// Token: 0x060077F3 RID: 30707
		IReference<T> Allocate<T>(T obj, int priority) where T : IStorable;

		// Token: 0x060077F4 RID: 30708
		IReference<T> Allocate<T>(T obj, int priority, int initialSize) where T : IStorable;

		// Token: 0x060077F5 RID: 30709
		IReference<T> AllocateAndPin<T>(T obj, int priority) where T : IStorable;

		// Token: 0x060077F6 RID: 30710
		IReference<T> AllocateAndPin<T>(T obj, int priority, int initialSize) where T : IStorable;

		// Token: 0x060077F7 RID: 30711
		IReference<T> GenerateFixedReference<T>(T obj) where T : IStorable;

		// Token: 0x060077F8 RID: 30712
		void Close();

		// Token: 0x060077F9 RID: 30713
		int StoreStaticReference(object item);

		// Token: 0x060077FA RID: 30714
		object FetchStaticReference(int id);

		// Token: 0x060077FB RID: 30715
		IReference PoolReference(IReference reference);

		// Token: 0x060077FC RID: 30716
		void DisableStorageUpdates();

		// Token: 0x170027E5 RID: 10213
		// (get) Token: 0x060077FD RID: 30717
		long SerializationDurationMs { get; }

		// Token: 0x170027E6 RID: 10214
		// (get) Token: 0x060077FE RID: 30718
		long DeserializationDurationMs { get; }

		// Token: 0x170027E7 RID: 10215
		// (get) Token: 0x060077FF RID: 30719
		long ScalabilityDurationMs { get; }

		// Token: 0x170027E8 RID: 10216
		// (get) Token: 0x06007800 RID: 30720
		long PeakMemoryUsageKBytes { get; }

		// Token: 0x170027E9 RID: 10217
		// (get) Token: 0x06007801 RID: 30721
		long CacheSizeKBytes { get; }

		// Token: 0x170027EA RID: 10218
		// (get) Token: 0x06007802 RID: 30722
		ComponentType OwnerComponent { get; }

		// Token: 0x170027EB RID: 10219
		// (get) Token: 0x06007803 RID: 30723
		IStorage Storage { get; }

		// Token: 0x170027EC RID: 10220
		// (get) Token: 0x06007804 RID: 30724
		ScalabilityCacheType CacheType { get; }
	}
}
