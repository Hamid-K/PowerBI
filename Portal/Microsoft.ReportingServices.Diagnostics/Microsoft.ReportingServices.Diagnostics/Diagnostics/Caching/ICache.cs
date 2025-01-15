using System;

namespace Microsoft.ReportingServices.Diagnostics.Caching
{
	// Token: 0x0200007E RID: 126
	internal interface ICache
	{
		// Token: 0x06000413 RID: 1043
		bool TryGet<T>(string key, out CacheItem cacheItem);

		// Token: 0x06000414 RID: 1044
		ICacheItemVersion Add(string key, object value);

		// Token: 0x06000415 RID: 1045
		ICacheItemVersion Put(string key, object value);

		// Token: 0x06000416 RID: 1046
		ICacheItemVersion Put(string key, object value, ICacheItemVersion oldVersion);

		// Token: 0x06000417 RID: 1047
		bool TryPut(string key, object value, ICacheItemVersion oldVersion, out ICacheItemVersion newVersion);

		// Token: 0x06000418 RID: 1048
		bool TryRemove(string key);

		// Token: 0x06000419 RID: 1049
		bool TryRemove(string key, ICacheItemVersion version);
	}
}
