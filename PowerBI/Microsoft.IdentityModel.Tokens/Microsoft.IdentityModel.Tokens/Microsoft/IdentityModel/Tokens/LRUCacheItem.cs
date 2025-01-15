using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x0200013A RID: 314
	internal class LRUCacheItem<TKey, TValue>
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0003D79C File Offset: 0x0003B99C
		internal TKey Key { get; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0003D7A4 File Offset: 0x0003B9A4
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x0003D7AC File Offset: 0x0003B9AC
		internal TValue Value { get; set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0003D7B5 File Offset: 0x0003B9B5
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x0003D7BD File Offset: 0x0003B9BD
		internal DateTime ExpirationTime { get; set; }

		// Token: 0x06000F60 RID: 3936 RVA: 0x0003D7C8 File Offset: 0x0003B9C8
		internal LRUCacheItem(TKey key, TValue value)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			this.Key = key;
			if (value == null)
			{
				throw LogHelper.LogArgumentNullException("value");
			}
			this.Value = value;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0003D814 File Offset: 0x0003BA14
		internal LRUCacheItem(TKey key, TValue value, DateTime expirationTime)
		{
			if (key == null)
			{
				throw LogHelper.LogArgumentNullException("key");
			}
			this.Key = key;
			if (value == null)
			{
				throw LogHelper.LogArgumentNullException("value");
			}
			this.Value = value;
			this.ExpirationTime = expirationTime;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0003D868 File Offset: 0x0003BA68
		public override bool Equals(object obj)
		{
			LRUCacheItem<TKey, TValue> lrucacheItem = obj as LRUCacheItem<TKey, TValue>;
			if (lrucacheItem != null)
			{
				TKey key = this.Key;
				return key.Equals(lrucacheItem.Key);
			}
			return false;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003D8A0 File Offset: 0x0003BAA0
		public override int GetHashCode()
		{
			return 990326508 + EqualityComparer<TKey>.Default.GetHashCode(this.Key);
		}
	}
}
