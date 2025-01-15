using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000030 RID: 48
	internal interface IClientProtocol
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600014A RID: 330
		NamedCacheConfiguration CacheConfiguration { get; }

		// Token: 0x0600014B RID: 331
		void SetRoutingStrategy(IRoutingStrategy routingStrategy);

		// Token: 0x0600014C RID: 332
		CacheServerProperties Initialize(IEnumerable<string> servers);

		// Token: 0x0600014D RID: 333
		CacheLookupTableTransfer GetLookupTable(EndpointID endpoint, CacheLookupTableRequest request, TimeSpan timeout);

		// Token: 0x0600014E RID: 334
		void AggregateProperties(IEnumerable<VelocityPacketProperty> properties, Action<VelocityPacketProperty, byte[]> callback);

		// Token: 0x0600014F RID: 335
		object Get(string key, out DataCacheItemVersion version, out TimeSpan timeout, out ErrStatus err, string region, IMonitoringListener listener);

		// Token: 0x06000150 RID: 336
		object GetIfNewer(string key, ref DataCacheItemVersion version, out TimeSpan timeout, out ErrStatus err, string region, IMonitoringListener listener);

		// Token: 0x06000151 RID: 337
		DataCacheItem GetCacheItem(string key, string region, out ErrStatus err, IMonitoringListener listener);

		// Token: 0x06000152 RID: 338
		DataCacheItemVersion Add(string key, object value, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener);

		// Token: 0x06000153 RID: 339
		DataCacheItemVersion Put(string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener);

		// Token: 0x06000154 RID: 340
		DataCacheItemVersion Replace(string key, object value, DataCacheItemVersion oldVersion, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener);

		// Token: 0x06000155 RID: 341
		bool Remove(string key, DataCacheItemVersion version, string region, IMonitoringListener listener);

		// Token: 0x06000156 RID: 342
		void Clear(IMonitoringListener listener);

		// Token: 0x06000157 RID: 343
		void ResetObjectTimeout(string key, TimeSpan newTimeout, string region, IMonitoringListener listener);

		// Token: 0x06000158 RID: 344
		object GetAndLock(string key, TimeSpan timeout, out DataCacheLockHandle lockHandle, string region, bool lockKey, IMonitoringListener listener);

		// Token: 0x06000159 RID: 345
		DataCacheItemVersion PutAndUnlock(string key, object value, DataCacheLockHandle lockHandle, TimeSpan timeout, DataCacheTag[] tags, string region, IMonitoringListener listener);

		// Token: 0x0600015A RID: 346
		bool LockedRemove(string key, DataCacheLockHandle lockHandle, string region, IMonitoringListener listener);

		// Token: 0x0600015B RID: 347
		void Unlock(string key, DataCacheLockHandle lockHandle, TimeSpan timeout, string region, IMonitoringListener listener);

		// Token: 0x0600015C RID: 348
		bool CreateRegion(string region, IMonitoringListener listener);

		// Token: 0x0600015D RID: 349
		bool RemoveRegion(string region, IMonitoringListener listener);

		// Token: 0x0600015E RID: 350
		void ClearRegion(string region, IMonitoringListener listener);

		// Token: 0x0600015F RID: 351
		IList<KeyValuePair<string, object>> GetNextBatch(string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener, ref object state, out bool more);

		// Token: 0x06000160 RID: 352
		IList<LocalCacheItem> BulkGet(Key[] keys, string region, IMonitoringListener listener);

		// Token: 0x06000161 RID: 353
		IList<LocalCacheItem> BulkGet(Key[] keys, IMonitoringListener listener);

		// Token: 0x06000162 RID: 354
		OperationResult SendNotificationRequestAsync(ReqType reqType, PollerRequestContext context, Action<PollerRequestContext> notifyCallback, Action<PollerRequestContext> errorCallback, out int notificationRequestId);

		// Token: 0x06000163 RID: 355
		bool DismissNotificationRequest(int notificationRequestId);

		// Token: 0x06000164 RID: 356
		long IncrementDecrement(string key, long value, long initialValue, TimeSpan timeout, string region, IMonitoringListener listener);

		// Token: 0x06000165 RID: 357
		void Concatenate(string key, string value, bool isAppend, TimeSpan timeout, string region, IMonitoringListener listener);

		// Token: 0x06000166 RID: 358
		bool ContainsKey(string key, string region, IMonitoringListener listener);

		// Token: 0x06000167 RID: 359
		IList<string> GetNextBatchOfKeys(string region, DataCacheTag[] tags, GetByTagsOperation op, IMonitoringListener listener, ref object state, out bool more);
	}
}
