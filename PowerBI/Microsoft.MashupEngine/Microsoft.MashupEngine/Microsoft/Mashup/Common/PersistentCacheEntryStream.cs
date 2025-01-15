using System;
using System.IO;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C11 RID: 7185
	public class PersistentCacheEntryStream : DelegatingStream, IPersistentCacheEntry
	{
		// Token: 0x0600B342 RID: 45890 RVA: 0x002476DB File Offset: 0x002458DB
		public static Stream New(Stream stream, string cacheKey)
		{
			return PersistentCacheEntryStream.NewEntryStream(stream, cacheKey, true);
		}

		// Token: 0x0600B343 RID: 45891 RVA: 0x002476E5 File Offset: 0x002458E5
		public static Stream NewNonCached(Stream stream, string cacheKey)
		{
			return PersistentCacheEntryStream.NewEntryStream(stream, cacheKey, false);
		}

		// Token: 0x0600B344 RID: 45892 RVA: 0x002476F0 File Offset: 0x002458F0
		public static bool IsCachedStream(Stream stream, out string cacheKey)
		{
			IPersistentCacheEntry persistentCacheEntry = stream as IPersistentCacheEntry;
			if (persistentCacheEntry != null && persistentCacheEntry.IsCached)
			{
				cacheKey = persistentCacheEntry.CacheKey;
				return true;
			}
			cacheKey = null;
			return false;
		}

		// Token: 0x0600B345 RID: 45893 RVA: 0x00247720 File Offset: 0x00245920
		private static Stream NewEntryStream(Stream stream, string cacheKey, bool cached)
		{
			IPersistentCacheEntry persistentCacheEntry = stream as IPersistentCacheEntry;
			if (persistentCacheEntry != null && persistentCacheEntry.CacheKey == cacheKey && persistentCacheEntry.IsCached == cached)
			{
				return stream;
			}
			return new PersistentCacheEntryStream(stream, cacheKey, cached);
		}

		// Token: 0x0600B346 RID: 45894 RVA: 0x00247758 File Offset: 0x00245958
		private PersistentCacheEntryStream(Stream stream, string cacheKey, bool cached)
			: base(stream)
		{
			this.stream = stream;
			this.cacheKey = cacheKey;
			this.cached = cached;
		}

		// Token: 0x17002CE7 RID: 11495
		// (get) Token: 0x0600B347 RID: 45895 RVA: 0x00247776 File Offset: 0x00245976
		public string CacheKey
		{
			get
			{
				return this.cacheKey;
			}
		}

		// Token: 0x17002CE8 RID: 11496
		// (get) Token: 0x0600B348 RID: 45896 RVA: 0x0024777E File Offset: 0x0024597E
		public bool IsCached
		{
			get
			{
				return this.cached;
			}
		}

		// Token: 0x04005B75 RID: 23413
		private readonly Stream stream;

		// Token: 0x04005B76 RID: 23414
		private readonly string cacheKey;

		// Token: 0x04005B77 RID: 23415
		private readonly bool cached;
	}
}
