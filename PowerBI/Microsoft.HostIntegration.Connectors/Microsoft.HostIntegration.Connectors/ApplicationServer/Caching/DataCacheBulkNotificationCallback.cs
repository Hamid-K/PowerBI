using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000022 RID: 34
	// (Invoke) Token: 0x060000F0 RID: 240
	public delegate void DataCacheBulkNotificationCallback(string cacheName, IEnumerable<DataCacheOperationDescriptor> operations, DataCacheNotificationDescriptor nd);
}
