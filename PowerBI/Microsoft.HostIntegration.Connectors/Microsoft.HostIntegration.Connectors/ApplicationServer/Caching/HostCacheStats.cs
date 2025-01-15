using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002EA RID: 746
	[DataContract]
	public class HostCacheStats
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x000541B4 File Offset: 0x000523B4
		// (set) Token: 0x06001BF0 RID: 7152 RVA: 0x000541BC File Offset: 0x000523BC
		public long Size
		{
			get
			{
				return this._size;
			}
			set
			{
				this._size = value;
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x000541C5 File Offset: 0x000523C5
		// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x000541CD File Offset: 0x000523CD
		public long ItemCount
		{
			get
			{
				return this._itemCount;
			}
			set
			{
				this._itemCount = value;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x000541D6 File Offset: 0x000523D6
		// (set) Token: 0x06001BF4 RID: 7156 RVA: 0x000541DE File Offset: 0x000523DE
		public long RegionCount
		{
			get
			{
				return this._regionCount;
			}
			set
			{
				this._regionCount = value;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x000541E7 File Offset: 0x000523E7
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x000541EF File Offset: 0x000523EF
		public long NamedCacheCount
		{
			get
			{
				return this._namedCacheCount;
			}
			set
			{
				this._namedCacheCount = value;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x000541F8 File Offset: 0x000523F8
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x00054200 File Offset: 0x00052400
		public long RequestCount
		{
			get
			{
				return this._requestCount;
			}
			set
			{
				this._requestCount = value;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00054209 File Offset: 0x00052409
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x00054211 File Offset: 0x00052411
		public long MissCount
		{
			get
			{
				return this._missCount;
			}
			set
			{
				this._missCount = value;
			}
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0005421C File Offset: 0x0005241C
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentUICulture, "Host stats: Size = {0}, Item count = {1}, Request count = {2}", new object[] { this._size, this._itemCount, this._requestCount });
		}

		// Token: 0x04000EC6 RID: 3782
		[DataMember]
		private long _size;

		// Token: 0x04000EC7 RID: 3783
		[DataMember]
		private long _itemCount;

		// Token: 0x04000EC8 RID: 3784
		[DataMember]
		private long _namedCacheCount;

		// Token: 0x04000EC9 RID: 3785
		[DataMember]
		private long _regionCount;

		// Token: 0x04000ECA RID: 3786
		[DataMember]
		private long _requestCount;

		// Token: 0x04000ECB RID: 3787
		[DataMember]
		private long _missCount;
	}
}
