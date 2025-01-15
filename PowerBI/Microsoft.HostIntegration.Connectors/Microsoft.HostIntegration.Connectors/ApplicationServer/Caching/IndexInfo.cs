using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000233 RID: 563
	internal sealed class IndexInfo
	{
		// Token: 0x060012B8 RID: 4792 RVA: 0x0003A28C File Offset: 0x0003848C
		internal IndexInfo(IMultiLevelHashTable multiHashTable, GetKeyFromCacheItemDelegate getKeyFromCacheItemDelegate)
		{
			this._multiHashTable = multiHashTable;
			this._getKeyFromCacheItemDelegate = getKeyFromCacheItemDelegate;
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00002061 File Offset: 0x00000261
		internal IndexInfo()
		{
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x0003A2A2 File Offset: 0x000384A2
		// (set) Token: 0x060012BB RID: 4795 RVA: 0x0003A2AA File Offset: 0x000384AA
		internal IMultiLevelHashTable MultiHashTable
		{
			get
			{
				return this._multiHashTable;
			}
			set
			{
				this._multiHashTable = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x0003A2B3 File Offset: 0x000384B3
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x0003A2BB File Offset: 0x000384BB
		internal GetKeyFromCacheItemDelegate KeyExtractDelegate
		{
			get
			{
				return this._getKeyFromCacheItemDelegate;
			}
			set
			{
				this._getKeyFromCacheItemDelegate = value;
			}
		}

		// Token: 0x04000B5C RID: 2908
		private IMultiLevelHashTable _multiHashTable;

		// Token: 0x04000B5D RID: 2909
		private GetKeyFromCacheItemDelegate _getKeyFromCacheItemDelegate;
	}
}
