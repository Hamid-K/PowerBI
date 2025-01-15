using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000291 RID: 657
	internal class SizeBasedScannerForCopy : SizeBasedScanner
	{
		// Token: 0x06001811 RID: 6161 RVA: 0x00048E4B File Offset: 0x0004704B
		internal SizeBasedScannerForCopy(int size, InternalCacheItemVersion version)
			: base(size)
		{
			this._list = new List<AOMCacheItem>();
			this._version = version;
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00048E66 File Offset: 0x00047066
		protected override void AddToBatch(AOMCacheItem item)
		{
			this._list.Add(item);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00048E74 File Offset: 0x00047074
		public override bool Scan(object item)
		{
			AOMCacheItem aomcacheItem = item as AOMCacheItem;
			return aomcacheItem.Version.CompareTo(this._version) >= 0 || aomcacheItem.IsRtLockPlaceHolderObject || aomcacheItem.IsItemExpired() || base.Scan(aomcacheItem);
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001814 RID: 6164 RVA: 0x00048EB8 File Offset: 0x000470B8
		internal List<AOMCacheItem> Batch
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x00006F04 File Offset: 0x00005104
		public override bool InvalidateOnChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000D41 RID: 3393
		private List<AOMCacheItem> _list;

		// Token: 0x04000D42 RID: 3394
		private InternalCacheItemVersion _version;
	}
}
