using System;
using Microsoft.ApplicationServer.Caching;

// Token: 0x02000125 RID: 293
internal interface IMemcacheShimConfigProvider
{
	// Token: 0x170001AF RID: 431
	// (get) Token: 0x0600086F RID: 2159
	MemcachePortsCollection MemcachePorts { get; }

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x06000870 RID: 2160
	DataCacheNamedClientCollection NamedClientCollection { get; }

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x06000871 RID: 2161
	DataCacheLogSink LogSink { get; }

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x06000872 RID: 2162
	string NodeAddress { get; }

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x06000873 RID: 2163
	bool IsEmulatedEnvironment { get; }
}
