using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002EF RID: 751
	[DataContract]
	internal class CacheRegionStats
	{
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00054ADF File Offset: 0x00052CDF
		// (set) Token: 0x06001C12 RID: 7186 RVA: 0x00054AE7 File Offset: 0x00052CE7
		public string RegionName
		{
			get
			{
				return this._regionName;
			}
			set
			{
				this._regionName = value;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x00054AF0 File Offset: 0x00052CF0
		// (set) Token: 0x06001C14 RID: 7188 RVA: 0x00054AF8 File Offset: 0x00052CF8
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

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x00054B01 File Offset: 0x00052D01
		// (set) Token: 0x06001C16 RID: 7190 RVA: 0x00054B09 File Offset: 0x00052D09
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

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x00054B12 File Offset: 0x00052D12
		// (set) Token: 0x06001C18 RID: 7192 RVA: 0x00054B1A File Offset: 0x00052D1A
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

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x00054B23 File Offset: 0x00052D23
		// (set) Token: 0x06001C1A RID: 7194 RVA: 0x00054B2B File Offset: 0x00052D2B
		public long RestRequestCount
		{
			get
			{
				return this._restRequestCount;
			}
			set
			{
				this._restRequestCount = value;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x00054B34 File Offset: 0x00052D34
		// (set) Token: 0x06001C1C RID: 7196 RVA: 0x00054B3C File Offset: 0x00052D3C
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

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x00054B45 File Offset: 0x00052D45
		// (set) Token: 0x06001C1E RID: 7198 RVA: 0x00054B4D File Offset: 0x00052D4D
		public RegionReplicaState Role
		{
			get
			{
				return this._replicaRole;
			}
			set
			{
				this._replicaRole = value;
			}
		}

		// Token: 0x04000EF8 RID: 3832
		[DataMember]
		private string _regionName;

		// Token: 0x04000EF9 RID: 3833
		[DataMember]
		private long _size;

		// Token: 0x04000EFA RID: 3834
		[DataMember]
		private long _itemCount;

		// Token: 0x04000EFB RID: 3835
		[DataMember]
		private long _requestCount;

		// Token: 0x04000EFC RID: 3836
		[DataMember]
		private long _restRequestCount;

		// Token: 0x04000EFD RID: 3837
		[DataMember]
		private long _missCount;

		// Token: 0x04000EFE RID: 3838
		[DataMember]
		private RegionReplicaState _replicaRole;
	}
}
