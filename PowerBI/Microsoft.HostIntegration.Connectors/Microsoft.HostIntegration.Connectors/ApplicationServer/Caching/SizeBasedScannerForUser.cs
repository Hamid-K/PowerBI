using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000292 RID: 658
	internal class SizeBasedScannerForUser : SizeBasedScanner
	{
		// Token: 0x06001816 RID: 6166 RVA: 0x00048EC0 File Offset: 0x000470C0
		internal SizeBasedScannerForUser(int size)
			: base(size)
		{
			this._list = new List<KeyValuePair<string, object>>();
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00048ED4 File Offset: 0x000470D4
		internal SizeBasedScannerForUser(int size, bool isKeyOnly)
			: base(size)
		{
			this.isKeyOnly = isKeyOnly;
			if (isKeyOnly)
			{
				this._keyList = new List<string>();
				return;
			}
			this._list = new List<KeyValuePair<string, object>>();
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00048F00 File Offset: 0x00047100
		protected override void AddToBatch(AOMCacheItem item)
		{
			if (!this.isKeyOnly)
			{
				this._list.Add(new KeyValuePair<string, object>(item.Key.ToString(), item.Value));
				return;
			}
			this._keyList.Add(item.Key.ToString());
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00048F50 File Offset: 0x00047150
		public override bool Scan(object item)
		{
			AOMCacheItem aomcacheItem = item as AOMCacheItem;
			if (!aomcacheItem.IsLockPlaceHolderObject && !aomcacheItem.IsItemExpired())
			{
				bool flag = base.Scan(aomcacheItem);
				if (flag)
				{
					aomcacheItem.UpdateLastAccessTime();
					aomcacheItem.UpdateTTLOnAccess();
				}
				return flag;
			}
			return true;
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x00048F8E File Offset: 0x0004718E
		internal List<KeyValuePair<string, object>> Batch
		{
			get
			{
				if (!this.isKeyOnly)
				{
					return this._list;
				}
				return this._list;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x00048FA5 File Offset: 0x000471A5
		internal List<string> KeyBatch
		{
			get
			{
				if (this.isKeyOnly)
				{
					return this._keyList;
				}
				return this._keyList;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool InvalidateOnChange
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000D43 RID: 3395
		private List<KeyValuePair<string, object>> _list;

		// Token: 0x04000D44 RID: 3396
		private List<string> _keyList;

		// Token: 0x04000D45 RID: 3397
		private bool isKeyOnly;
	}
}
