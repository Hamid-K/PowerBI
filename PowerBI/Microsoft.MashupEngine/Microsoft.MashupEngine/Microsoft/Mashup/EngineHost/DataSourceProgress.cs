using System;

namespace Microsoft.Mashup.EngineHost
{
	// Token: 0x0200195C RID: 6492
	public class DataSourceProgress
	{
		// Token: 0x0600A484 RID: 42116 RVA: 0x00221404 File Offset: 0x0021F604
		public DataSourceProgress(string dataSourceType, string dataSource)
		{
			this.dataSourceType = dataSourceType;
			this.dataSource = dataSource;
			this.percentComplete = -1;
		}

		// Token: 0x170029F1 RID: 10737
		// (get) Token: 0x0600A485 RID: 42117 RVA: 0x00221421 File Offset: 0x0021F621
		// (set) Token: 0x0600A486 RID: 42118 RVA: 0x00221429 File Offset: 0x0021F629
		public long LastProgressAt
		{
			get
			{
				return this.lastProgressAt;
			}
			set
			{
				this.lastProgressAt = value;
			}
		}

		// Token: 0x170029F2 RID: 10738
		// (get) Token: 0x0600A487 RID: 42119 RVA: 0x00221432 File Offset: 0x0021F632
		// (set) Token: 0x0600A488 RID: 42120 RVA: 0x0022143A File Offset: 0x0021F63A
		public int RequestCount
		{
			get
			{
				return this.requestCount;
			}
			set
			{
				this.requestCount = value;
			}
		}

		// Token: 0x170029F3 RID: 10739
		// (get) Token: 0x0600A489 RID: 42121 RVA: 0x00221443 File Offset: 0x0021F643
		// (set) Token: 0x0600A48A RID: 42122 RVA: 0x0022144B File Offset: 0x0021F64B
		public string DataSourceType
		{
			get
			{
				return this.dataSourceType;
			}
			set
			{
				this.dataSourceType = value;
			}
		}

		// Token: 0x170029F4 RID: 10740
		// (get) Token: 0x0600A48B RID: 42123 RVA: 0x00221454 File Offset: 0x0021F654
		// (set) Token: 0x0600A48C RID: 42124 RVA: 0x0022145C File Offset: 0x0021F65C
		public string DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				this.dataSource = value;
			}
		}

		// Token: 0x170029F5 RID: 10741
		// (get) Token: 0x0600A48D RID: 42125 RVA: 0x00221465 File Offset: 0x0021F665
		// (set) Token: 0x0600A48E RID: 42126 RVA: 0x0022146D File Offset: 0x0021F66D
		public long RowsRead
		{
			get
			{
				return this.rowsRead;
			}
			set
			{
				this.rowsRead = value;
			}
		}

		// Token: 0x170029F6 RID: 10742
		// (get) Token: 0x0600A48F RID: 42127 RVA: 0x00221476 File Offset: 0x0021F676
		// (set) Token: 0x0600A490 RID: 42128 RVA: 0x0022147E File Offset: 0x0021F67E
		public long RowsWritten
		{
			get
			{
				return this.rowsWritten;
			}
			set
			{
				this.rowsWritten = value;
			}
		}

		// Token: 0x170029F7 RID: 10743
		// (get) Token: 0x0600A491 RID: 42129 RVA: 0x00221487 File Offset: 0x0021F687
		// (set) Token: 0x0600A492 RID: 42130 RVA: 0x0022148F File Offset: 0x0021F68F
		public long BytesRead
		{
			get
			{
				return this.bytesRead;
			}
			set
			{
				this.bytesRead = value;
			}
		}

		// Token: 0x170029F8 RID: 10744
		// (get) Token: 0x0600A493 RID: 42131 RVA: 0x00221498 File Offset: 0x0021F698
		// (set) Token: 0x0600A494 RID: 42132 RVA: 0x002214A0 File Offset: 0x0021F6A0
		public long BytesWritten
		{
			get
			{
				return this.bytesWritten;
			}
			set
			{
				this.bytesWritten = value;
			}
		}

		// Token: 0x170029F9 RID: 10745
		// (get) Token: 0x0600A495 RID: 42133 RVA: 0x002214A9 File Offset: 0x0021F6A9
		// (set) Token: 0x0600A496 RID: 42134 RVA: 0x002214B1 File Offset: 0x0021F6B1
		public int PercentComplete
		{
			get
			{
				return this.percentComplete;
			}
			set
			{
				this.percentComplete = value;
			}
		}

		// Token: 0x040055B7 RID: 21943
		private string dataSourceType;

		// Token: 0x040055B8 RID: 21944
		private string dataSource;

		// Token: 0x040055B9 RID: 21945
		private long bytesRead;

		// Token: 0x040055BA RID: 21946
		private long bytesWritten;

		// Token: 0x040055BB RID: 21947
		private long rowsRead;

		// Token: 0x040055BC RID: 21948
		private long rowsWritten;

		// Token: 0x040055BD RID: 21949
		private int requestCount;

		// Token: 0x040055BE RID: 21950
		private int percentComplete;

		// Token: 0x040055BF RID: 21951
		private long lastProgressAt;
	}
}
