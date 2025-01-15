using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200007D RID: 125
	public class PvDataSource
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000B0F2 File Offset: 0x000092F2
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000B0FA File Offset: 0x000092FA
		public string DataSourceName { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000B103 File Offset: 0x00009303
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000B10B File Offset: 0x0000930B
		public string DatabaseName { get; private set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000B114 File Offset: 0x00009314
		public string DatabaseFullName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.VirtualServerName))
				{
					return string.Format("{0}-{1}", this.VirtualServerName, this.DatabaseName);
				}
				return this.DatabaseName;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000B140 File Offset: 0x00009340
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000B148 File Offset: 0x00009348
		public string VirtualServerName { get; private set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000B151 File Offset: 0x00009351
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000B159 File Offset: 0x00009359
		public string ResolvedServerName { get; private set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000B162 File Offset: 0x00009362
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000B16A File Offset: 0x0000936A
		public bool IsInternal { get; private set; }

		// Token: 0x0600036B RID: 875 RVA: 0x0000B173 File Offset: 0x00009373
		public PvDataSource(string dataSourceName, string virtualServerName, string databaseName, bool isInternal)
		{
			this.DataSourceName = dataSourceName;
			this.VirtualServerName = virtualServerName;
			this.DatabaseName = databaseName;
			this.IsInternal = isInternal;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000B198 File Offset: 0x00009398
		public override string ToString()
		{
			return string.Format("DataSourceName={0}, VirtualServerName={1}, DatabaseName={2}, IsInternal={3}", new object[] { this.DataSourceName, this.VirtualServerName, this.DatabaseName, this.IsInternal });
		}
	}
}
