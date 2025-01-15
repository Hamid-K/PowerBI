using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000020 RID: 32
	// (Invoke) Token: 0x060000E8 RID: 232
	public delegate void DataCacheNotificationCallback(string cacheName, string regionName, string key, DataCacheItemVersion version, DataCacheOperations cacheOperation, DataCacheNotificationDescriptor nd);
}
