using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000018 RID: 24
	internal class PerDelegateInfo
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00004E78 File Offset: 0x00003078
		public PerDelegateInfo(int filterMask, object cacheDelegate, DataCacheNotificationDescriptor nd)
		{
			this._filterMask = filterMask;
			this._cacheDelegate = cacheDelegate;
			this._nd = nd;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004E95 File Offset: 0x00003095
		public int FilterMask
		{
			get
			{
				return this._filterMask;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004E9D File Offset: 0x0000309D
		public object CacheDelegate
		{
			get
			{
				return this._cacheDelegate;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004EA5 File Offset: 0x000030A5
		public DataCacheNotificationDescriptor Nd
		{
			get
			{
				return this._nd;
			}
		}

		// Token: 0x04000074 RID: 116
		private int _filterMask;

		// Token: 0x04000075 RID: 117
		private object _cacheDelegate;

		// Token: 0x04000076 RID: 118
		private DataCacheNotificationDescriptor _nd;
	}
}
