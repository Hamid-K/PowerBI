using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000133 RID: 307
	internal interface INamedCacheConfiguration : ISerializable
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060008DA RID: 2266
		// (set) Token: 0x060008DB RID: 2267
		string Name { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060008DC RID: 2268
		// (set) Token: 0x060008DD RID: 2269
		NamedCacheDeploymentType Type { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060008DE RID: 2270
		// (set) Token: 0x060008DF RID: 2271
		ConsistencyType Consistency { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060008E0 RID: 2272
		// (set) Token: 0x060008E1 RID: 2273
		long DefaultTTL { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060008E2 RID: 2274
		// (set) Token: 0x060008E3 RID: 2275
		ExpirationType ExpirationType { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060008E4 RID: 2276
		// (set) Token: 0x060008E5 RID: 2277
		EvictionType EvictionType { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060008E6 RID: 2278
		// (set) Token: 0x060008E7 RID: 2279
		bool IsExpirable { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060008E8 RID: 2280
		// (set) Token: 0x060008E9 RID: 2281
		ServerNotificationProperties Notification { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060008EA RID: 2282
		// (set) Token: 0x060008EB RID: 2283
		int Secondaries { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060008EC RID: 2284
		int Replicas { get; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060008ED RID: 2285
		// (set) Token: 0x060008EE RID: 2286
		int SystemRegionCount { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060008EF RID: 2287
		// (set) Token: 0x060008F0 RID: 2288
		int PartitionCount { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060008F1 RID: 2289
		// (set) Token: 0x060008F2 RID: 2290
		int MinWriteQuorum { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060008F3 RID: 2291
		// (set) Token: 0x060008F4 RID: 2292
		BackingStoreConfig BackingStore { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060008F5 RID: 2293
		// (set) Token: 0x060008F6 RID: 2294
		QuotaConfig Quota { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060008F7 RID: 2295
		// (set) Token: 0x060008F8 RID: 2296
		bool Enabled { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060008F9 RID: 2297
		// (set) Token: 0x060008FA RID: 2298
		long Version { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060008FB RID: 2299
		// (set) Token: 0x060008FC RID: 2300
		Uri RedirectUri { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060008FD RID: 2301
		// (set) Token: 0x060008FE RID: 2302
		DeploymentModeElement DeploymentMode { get; set; }

		// Token: 0x060008FF RID: 2303
		int GetPartitionCount(int defaultValue);

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000900 RID: 2304
		// (set) Token: 0x06000901 RID: 2305
		int MemcacheSocketPort { get; set; }
	}
}
