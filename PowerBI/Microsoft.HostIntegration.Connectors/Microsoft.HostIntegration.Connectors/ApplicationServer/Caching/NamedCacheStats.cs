using System;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000341 RID: 833
	[DataContract]
	public class NamedCacheStats
	{
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x00059CB3 File Offset: 0x00057EB3
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x00059CBB File Offset: 0x00057EBB
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

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x00059CC4 File Offset: 0x00057EC4
		// (set) Token: 0x06001DCF RID: 7631 RVA: 0x00059CCC File Offset: 0x00057ECC
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

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x00059CD5 File Offset: 0x00057ED5
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x00059CDD File Offset: 0x00057EDD
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

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x00059CE6 File Offset: 0x00057EE6
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x00059CEE File Offset: 0x00057EEE
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

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x00059CF7 File Offset: 0x00057EF7
		// (set) Token: 0x06001DD5 RID: 7637 RVA: 0x00059CFF File Offset: 0x00057EFF
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

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x00059D08 File Offset: 0x00057F08
		// (set) Token: 0x06001DD7 RID: 7639 RVA: 0x00059D10 File Offset: 0x00057F10
		public long ReadRequestCount
		{
			get
			{
				return this._readRequestCount;
			}
			set
			{
				this._readRequestCount = value;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x00059D19 File Offset: 0x00057F19
		// (set) Token: 0x06001DD9 RID: 7641 RVA: 0x00059D21 File Offset: 0x00057F21
		public long WriteRequestCount
		{
			get
			{
				return this._writeRequestCount;
			}
			set
			{
				this._writeRequestCount = value;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x00059D2A File Offset: 0x00057F2A
		// (set) Token: 0x06001DDB RID: 7643 RVA: 0x00059D32 File Offset: 0x00057F32
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

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x00059D3B File Offset: 0x00057F3B
		// (set) Token: 0x06001DDD RID: 7645 RVA: 0x00059D43 File Offset: 0x00057F43
		public long IncomingBandwidth
		{
			get
			{
				return this._incomingBandwidth;
			}
			set
			{
				this._incomingBandwidth = value;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x00059D4C File Offset: 0x00057F4C
		// (set) Token: 0x06001DDF RID: 7647 RVA: 0x00059D54 File Offset: 0x00057F54
		public long OutgoingBandwidth
		{
			get
			{
				return this._outgoingBandwidth;
			}
			set
			{
				this._outgoingBandwidth = value;
			}
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00059D60 File Offset: 0x00057F60
		internal NamedCacheStats Copy()
		{
			return new NamedCacheStats
			{
				Size = this.Size,
				ItemCount = this.ItemCount,
				RegionCount = this.RegionCount,
				RequestCount = this.RequestCount,
				MissCount = this.MissCount,
				ReadRequestCount = this.ReadRequestCount,
				WriteRequestCount = this.WriteRequestCount,
				IncomingBandwidth = this.IncomingBandwidth,
				OutgoingBandwidth = this.OutgoingBandwidth,
				RestRequestCount = this.RestRequestCount
			};
		}

		// Token: 0x04001091 RID: 4241
		[DataMember]
		private long _size;

		// Token: 0x04001092 RID: 4242
		[DataMember]
		private long _itemCount;

		// Token: 0x04001093 RID: 4243
		[DataMember]
		private long _regionCount;

		// Token: 0x04001094 RID: 4244
		[DataMember]
		private long _requestCount;

		// Token: 0x04001095 RID: 4245
		[DataMember]
		private long _missCount;

		// Token: 0x04001096 RID: 4246
		[DataMember]
		private long _restRequestCount;

		// Token: 0x04001097 RID: 4247
		[DataMember]
		private long _readRequestCount;

		// Token: 0x04001098 RID: 4248
		[DataMember]
		private long _writeRequestCount;

		// Token: 0x04001099 RID: 4249
		[DataMember]
		private long _incomingBandwidth;

		// Token: 0x0400109A RID: 4250
		[DataMember]
		private long _outgoingBandwidth;
	}
}
