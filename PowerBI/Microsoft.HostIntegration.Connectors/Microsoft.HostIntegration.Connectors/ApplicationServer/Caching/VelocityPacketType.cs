using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000182 RID: 386
	internal enum VelocityPacketType : byte
	{
		// Token: 0x040008CE RID: 2254
		None,
		// Token: 0x040008CF RID: 2255
		GetProperties,
		// Token: 0x040008D0 RID: 2256
		Get,
		// Token: 0x040008D1 RID: 2257
		GetIfNewer,
		// Token: 0x040008D2 RID: 2258
		GetCacheItem,
		// Token: 0x040008D3 RID: 2259
		Put,
		// Token: 0x040008D4 RID: 2260
		Add,
		// Token: 0x040008D5 RID: 2261
		Replace,
		// Token: 0x040008D6 RID: 2262
		Increment,
		// Token: 0x040008D7 RID: 2263
		Append,
		// Token: 0x040008D8 RID: 2264
		Prepend,
		// Token: 0x040008D9 RID: 2265
		ContainsKey,
		// Token: 0x040008DA RID: 2266
		Remove,
		// Token: 0x040008DB RID: 2267
		ResetTimeout,
		// Token: 0x040008DC RID: 2268
		GetAndLock,
		// Token: 0x040008DD RID: 2269
		GetAndLockForce,
		// Token: 0x040008DE RID: 2270
		PutAndUnlock,
		// Token: 0x040008DF RID: 2271
		Unlock,
		// Token: 0x040008E0 RID: 2272
		LockedRemove,
		// Token: 0x040008E1 RID: 2273
		CreateRegion,
		// Token: 0x040008E2 RID: 2274
		RemoveRegion,
		// Token: 0x040008E3 RID: 2275
		ClearRegion,
		// Token: 0x040008E4 RID: 2276
		GetBatchByNone,
		// Token: 0x040008E5 RID: 2277
		GetBatchByIntersection,
		// Token: 0x040008E6 RID: 2278
		GetBatchByUnion,
		// Token: 0x040008E7 RID: 2279
		Clear,
		// Token: 0x040008E8 RID: 2280
		ClearPartition,
		// Token: 0x040008E9 RID: 2281
		Notification,
		// Token: 0x040008EA RID: 2282
		NotificationLsn,
		// Token: 0x040008EB RID: 2283
		Memcache_CacheBulkGet = 240,
		// Token: 0x040008EC RID: 2284
		Memcache_Decrement,
		// Token: 0x040008ED RID: 2285
		Memcache_Noop,
		// Token: 0x040008EE RID: 2286
		Memcache_Stat
	}
}
