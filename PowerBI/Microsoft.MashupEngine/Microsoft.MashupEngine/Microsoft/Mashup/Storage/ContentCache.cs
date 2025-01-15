using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Storage
{
	// Token: 0x02002044 RID: 8260
	public abstract class ContentCache
	{
		// Token: 0x0600CA29 RID: 51753 RVA: 0x00286B67 File Offset: 0x00284D67
		public static ContentCache Create(int maxCacheSize)
		{
			return new ContentCache.SizedContentCache(maxCacheSize);
		}

		// Token: 0x0600CA2A RID: 51754 RVA: 0x00286B6F File Offset: 0x00284D6F
		public void AddContent(Guid contentID, byte[] content)
		{
			this.AddContent<byte[]>(contentID, content, content.Length);
		}

		// Token: 0x0600CA2B RID: 51755
		public abstract void AddContent<T>(Guid contentID, T content, int contentSize);

		// Token: 0x0600CA2C RID: 51756
		public abstract bool TryGetContent<T>(Guid contentID, out T content);

		// Token: 0x0600CA2D RID: 51757
		public abstract void DeleteContent(Guid contentID);

		// Token: 0x040066D3 RID: 26323
		public static readonly ContentCache None = new ContentCache.NoContentCache();

		// Token: 0x02002045 RID: 8261
		private class NoContentCache : ContentCache
		{
			// Token: 0x0600CA31 RID: 51761 RVA: 0x0000336E File Offset: 0x0000156E
			public override void AddContent<T>(Guid contentID, T content, int contentSize)
			{
			}

			// Token: 0x0600CA32 RID: 51762 RVA: 0x00286B90 File Offset: 0x00284D90
			public override bool TryGetContent<T>(Guid contentID, out T content)
			{
				content = default(T);
				return false;
			}

			// Token: 0x0600CA33 RID: 51763 RVA: 0x0000336E File Offset: 0x0000156E
			public override void DeleteContent(Guid contentID)
			{
			}
		}

		// Token: 0x02002046 RID: 8262
		private class SizedContentCache : ContentCache
		{
			// Token: 0x0600CA34 RID: 51764 RVA: 0x00286B9A File Offset: 0x00284D9A
			public SizedContentCache(int maxCacheSize)
			{
				this.entries = new Dictionary<Guid, ContentCache.SizedContentCache.CacheEntry>();
				this.cacheSize = 0;
				this.maxCacheSize = maxCacheSize;
				this.objectLock = new object();
			}

			// Token: 0x0600CA35 RID: 51765 RVA: 0x00286BC8 File Offset: 0x00284DC8
			public override void AddContent<T>(Guid contentID, T content, int contentSize)
			{
				object obj = this.objectLock;
				lock (obj)
				{
					ContentCache.SizedContentCache.CacheEntry cacheEntry;
					if (this.entries.TryGetValue(contentID, out cacheEntry))
					{
						this.cacheSize -= cacheEntry.ContentSize;
					}
					this.entries[contentID] = new ContentCache.SizedContentCache.CacheEntry(content, contentSize);
					this.cacheSize += contentSize;
					this.Trim();
				}
			}

			// Token: 0x0600CA36 RID: 51766 RVA: 0x00286C54 File Offset: 0x00284E54
			public override bool TryGetContent<T>(Guid contentID, out T content)
			{
				object obj = this.objectLock;
				bool flag2;
				lock (obj)
				{
					ContentCache.SizedContentCache.CacheEntry cacheEntry;
					if (this.entries.TryGetValue(contentID, out cacheEntry))
					{
						object obj2 = cacheEntry.AccessContent();
						if (obj2 is T)
						{
							content = (T)((object)obj2);
							return true;
						}
					}
					content = default(T);
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x0600CA37 RID: 51767 RVA: 0x00286CCC File Offset: 0x00284ECC
			public override void DeleteContent(Guid contentID)
			{
				object obj = this.objectLock;
				lock (obj)
				{
					ContentCache.SizedContentCache.CacheEntry cacheEntry;
					if (this.entries.TryGetValue(contentID, out cacheEntry))
					{
						this.cacheSize -= cacheEntry.ContentSize;
						this.entries.Remove(contentID);
					}
				}
			}

			// Token: 0x0600CA38 RID: 51768 RVA: 0x00286D38 File Offset: 0x00284F38
			private void Trim()
			{
				if (this.cacheSize > this.maxCacheSize)
				{
					foreach (KeyValuePair<Guid, ContentCache.SizedContentCache.CacheEntry> keyValuePair in this.entries.OrderBy((KeyValuePair<Guid, ContentCache.SizedContentCache.CacheEntry> x) => x.Value.LastAccessedAt))
					{
						this.entries.Remove(keyValuePair.Key);
						this.cacheSize -= keyValuePair.Value.ContentSize;
						if (this.cacheSize <= this.maxCacheSize)
						{
							break;
						}
					}
				}
			}

			// Token: 0x040066D4 RID: 26324
			private Dictionary<Guid, ContentCache.SizedContentCache.CacheEntry> entries;

			// Token: 0x040066D5 RID: 26325
			private int cacheSize;

			// Token: 0x040066D6 RID: 26326
			private int maxCacheSize;

			// Token: 0x040066D7 RID: 26327
			private object objectLock;

			// Token: 0x02002047 RID: 8263
			private class CacheEntry
			{
				// Token: 0x0600CA39 RID: 51769 RVA: 0x00286DF0 File Offset: 0x00284FF0
				public CacheEntry(object content, int contentSize)
				{
					this.content = content;
					this.contentSize = contentSize;
					this.lastAccessedAt = DateTime.UtcNow;
				}

				// Token: 0x170030A8 RID: 12456
				// (get) Token: 0x0600CA3A RID: 51770 RVA: 0x00286E11 File Offset: 0x00285011
				public int ContentSize
				{
					get
					{
						return this.contentSize;
					}
				}

				// Token: 0x0600CA3B RID: 51771 RVA: 0x00286E19 File Offset: 0x00285019
				public object AccessContent()
				{
					this.lastAccessedAt = DateTime.UtcNow;
					return this.content;
				}

				// Token: 0x170030A9 RID: 12457
				// (get) Token: 0x0600CA3C RID: 51772 RVA: 0x00286E2C File Offset: 0x0028502C
				public DateTime LastAccessedAt
				{
					get
					{
						return this.lastAccessedAt;
					}
				}

				// Token: 0x0600CA3D RID: 51773 RVA: 0x00286E34 File Offset: 0x00285034
				public override string ToString()
				{
					return this.contentSize.ToString() + "@" + this.lastAccessedAt.Ticks.ToString();
				}

				// Token: 0x040066D8 RID: 26328
				private object content;

				// Token: 0x040066D9 RID: 26329
				private int contentSize;

				// Token: 0x040066DA RID: 26330
				private DateTime lastAccessedAt;
			}
		}
	}
}
