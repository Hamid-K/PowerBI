using System;
using System.Runtime.Caching;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000089 RID: 137
	public interface IInMemoryCache<T> : IDisposable
	{
		// Token: 0x06000506 RID: 1286
		bool TryGet(string key, out T item);

		// Token: 0x06000507 RID: 1287
		T AddOrGetExisting(string key, T item, TimeSpan slidingExpirationDuration);

		// Token: 0x06000508 RID: 1288
		void AddOrUpdate(string key, T item, TimeSpan slidingExpirationDuration);

		// Token: 0x06000509 RID: 1289
		void AddOrUpdate(string key, T item, CacheItemPolicy cacheItemPolicy);

		// Token: 0x0600050A RID: 1290
		void Remove(string key);

		// Token: 0x0600050B RID: 1291
		ISerializer<string> Serializer();
	}
}
