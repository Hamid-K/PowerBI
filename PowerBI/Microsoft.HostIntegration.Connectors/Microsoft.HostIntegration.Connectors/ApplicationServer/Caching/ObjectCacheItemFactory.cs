using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000279 RID: 633
	internal class ObjectCacheItemFactory : ICacheItemFactory
	{
		// Token: 0x0600157E RID: 5502 RVA: 0x0004133E File Offset: 0x0003F53E
		public AOMCacheItem GetCacheItem(IOMRegion region, Key key)
		{
			return new ObjectCacheItem(region, key);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00041347 File Offset: 0x0003F547
		public AOMCacheItem GetCacheItem(Key key)
		{
			return new ObjectCacheItem(key);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0004134F File Offset: 0x0003F54F
		public AOMCacheItem GetCacheItem(IOMRegion region, Key key, object value, long TTL, long extnTimeout, object[] tags)
		{
			return new ObjectCacheItem(region, key, value, TTL, extnTimeout, tags);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0004135F File Offset: 0x0003F55F
		public AOMCacheItem GetCacheItem(IOMRegion region, Key key, object value, long TTL, long extnTimeout, object[] tags, ObjectType oType)
		{
			return new ObjectCacheItem(region, key, value, TTL, extnTimeout, tags, oType);
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00041371 File Offset: 0x0003F571
		public AOMCacheItem GetCacheItem(AOMCacheItem item)
		{
			return new ObjectCacheItem(item);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0004137C File Offset: 0x0003F57C
		public AOMCacheItem GetCacheItem(OMCacheItem item, OMRegion region)
		{
			ObjectCacheItem objectCacheItem = new ObjectCacheItem(region, (Key)item.Key, item.Value, item.TimeToLive, item.ExtensionTimeout, item.Tags, ObjectType.SerializedBufferRefType);
			objectCacheItem.Flags = item.Flags;
			objectCacheItem.LockExpirationTime = item.LockExpirationTime;
			objectCacheItem.Version = item.Version;
			objectCacheItem.SetLockHandle(item.GetDMLockHandle());
			objectCacheItem.LastAccess = item.LastAccess;
			return objectCacheItem;
		}
	}
}
