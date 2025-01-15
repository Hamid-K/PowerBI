using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000293 RID: 659
	internal class SizeBasedScannerForTagIntersection : SizeBasedScannerForUser
	{
		// Token: 0x0600181D RID: 6173 RVA: 0x00048FBC File Offset: 0x000471BC
		internal SizeBasedScannerForTagIntersection(int size, DataCacheTag[] tags)
			: base(size)
		{
			this._tags = tags;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00048FCC File Offset: 0x000471CC
		internal SizeBasedScannerForTagIntersection(int size, DataCacheTag[] tags, bool isKeyOnly)
			: base(size, isKeyOnly)
		{
			this._tags = tags;
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00048FE0 File Offset: 0x000471E0
		public override bool Scan(object item)
		{
			AOMCacheItem aomcacheItem = item as AOMCacheItem;
			for (int i = 0; i < this._tags.Length; i++)
			{
				DataCacheTag dataCacheTag = this._tags[i];
				if (!SizeBasedScannerForTagIntersection.Contains(dataCacheTag, aomcacheItem.Tags))
				{
					return true;
				}
			}
			return base.Scan(item);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00049028 File Offset: 0x00047228
		private static bool Contains(DataCacheTag tag, object[] tags)
		{
			for (int i = 0; i < tags.Length; i++)
			{
				if (tags[i].Equals(tag))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000D46 RID: 3398
		private DataCacheTag[] _tags;
	}
}
