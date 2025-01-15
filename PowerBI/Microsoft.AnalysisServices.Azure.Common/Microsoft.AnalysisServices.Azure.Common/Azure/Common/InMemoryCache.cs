using System;
using System.Collections.Specialized;
using System.Runtime.Caching;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x0200008B RID: 139
	public sealed class InMemoryCache<T> : IInMemoryCache<T>, IDisposable
	{
		// Token: 0x0600050E RID: 1294 RVA: 0x00010BE5 File Offset: 0x0000EDE5
		public InMemoryCache(string cacheName, NameValueCollection cacheSettings)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(cacheName, "cacheName");
			this.cache = new MemoryCache(cacheName, cacheSettings);
			this.operationSerializer = new Serializer<string>();
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00010C10 File Offset: 0x0000EE10
		public bool TryGet(string key, out T item)
		{
			item = (T)((object)this.cache.Get(key, null));
			return item != null;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00010C38 File Offset: 0x0000EE38
		public T AddOrGetExisting(string key, T item, TimeSpan slidingExpirationDuration)
		{
			CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
			cacheItemPolicy.SlidingExpiration = slidingExpirationDuration;
			return (T)((object)(this.cache.AddOrGetExisting(key, item, cacheItemPolicy, null) ?? item));
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00010C78 File Offset: 0x0000EE78
		public void AddOrUpdate(string key, T item, TimeSpan slidingExpirationDuration)
		{
			this.AddOrUpdate(key, item, new CacheItemPolicy
			{
				SlidingExpiration = slidingExpirationDuration
			});
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00010C9B File Offset: 0x0000EE9B
		public void AddOrUpdate(string key, T item, CacheItemPolicy cacheItemPolicy)
		{
			this.cache.Set(new CacheItem(key, item), cacheItemPolicy);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00010CB5 File Offset: 0x0000EEB5
		public void Remove(string key)
		{
			this.cache.Remove(key, null);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00010CC5 File Offset: 0x0000EEC5
		public ISerializer<string> Serializer()
		{
			return this.operationSerializer;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00010CCD File Offset: 0x0000EECD
		public void Dispose()
		{
			this.cache.Dispose();
		}

		// Token: 0x04000201 RID: 513
		private readonly MemoryCache cache;

		// Token: 0x04000202 RID: 514
		private readonly ISerializer<string> operationSerializer;
	}
}
