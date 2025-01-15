using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Storage.Local
{
	// Token: 0x020020AA RID: 8362
	public class LocalStorageCache
	{
		// Token: 0x0600CCBC RID: 52412 RVA: 0x0028B030 File Offset: 0x00289230
		public LocalStorageCache(int cacheSize)
		{
			this.cacheSize = cacheSize;
			this.cache = new List<LocalStorageCache.CacheEntry>();
		}

		// Token: 0x0600CCBD RID: 52413 RVA: 0x0028B04C File Offset: 0x0028924C
		public bool TryGet(string path, out object part)
		{
			int num;
			if (this.TryFindIndex(path, out num))
			{
				LocalStorageCache.CacheEntry cacheEntry = this.cache[num];
				part = cacheEntry.Part;
				return true;
			}
			part = null;
			return false;
		}

		// Token: 0x0600CCBE RID: 52414 RVA: 0x0028B080 File Offset: 0x00289280
		public bool TryRemove(string path)
		{
			object obj;
			return this.TryRemove(path, out obj);
		}

		// Token: 0x0600CCBF RID: 52415 RVA: 0x0028B098 File Offset: 0x00289298
		public bool TryRemove(string path, out object part)
		{
			int num;
			if (this.TryFindIndex(path, out num))
			{
				LocalStorageCache.CacheEntry cacheEntry = this.cache[num];
				this.cache.RemoveAt(num);
				part = cacheEntry.Part;
				return true;
			}
			part = null;
			return false;
		}

		// Token: 0x0600CCC0 RID: 52416 RVA: 0x0028B0D8 File Offset: 0x002892D8
		private bool TryFindIndex(string path, out int index)
		{
			for (int i = 0; i < this.cache.Count; i++)
			{
				if (string.CompareOrdinal(path, this.cache[i].Path) == 0)
				{
					index = i;
					return true;
				}
			}
			index = -1;
			return false;
		}

		// Token: 0x0600CCC1 RID: 52417 RVA: 0x0028B11D File Offset: 0x0028931D
		public void Set(string path, object part)
		{
			this.TryRemove(path);
			this.Add(path, part);
		}

		// Token: 0x0600CCC2 RID: 52418 RVA: 0x0028B130 File Offset: 0x00289330
		public void Add(string path, object part)
		{
			LocalStorageCache.CacheEntry cacheEntry = new LocalStorageCache.CacheEntry(path, part);
			int num;
			if (this.TryFindIndex(path, out num))
			{
				throw new InvalidOperationException();
			}
			if (this.cache.Count == this.cacheSize)
			{
				this.cache.RemoveAt(this.cache.Count - 1);
			}
			this.cache.Insert(0, cacheEntry);
		}

		// Token: 0x040067A6 RID: 26534
		private readonly List<LocalStorageCache.CacheEntry> cache;

		// Token: 0x040067A7 RID: 26535
		private readonly int cacheSize;

		// Token: 0x020020AB RID: 8363
		private class CacheEntry
		{
			// Token: 0x0600CCC3 RID: 52419 RVA: 0x0028B18E File Offset: 0x0028938E
			public CacheEntry(string path, object part)
			{
				this.part = part;
				this.path = path;
			}

			// Token: 0x17003148 RID: 12616
			// (get) Token: 0x0600CCC4 RID: 52420 RVA: 0x0028B1A4 File Offset: 0x002893A4
			public string Path
			{
				get
				{
					return this.path;
				}
			}

			// Token: 0x17003149 RID: 12617
			// (get) Token: 0x0600CCC5 RID: 52421 RVA: 0x0028B1AC File Offset: 0x002893AC
			public object Part
			{
				get
				{
					return this.part;
				}
			}

			// Token: 0x040067A8 RID: 26536
			private string path;

			// Token: 0x040067A9 RID: 26537
			private object part;
		}
	}
}
