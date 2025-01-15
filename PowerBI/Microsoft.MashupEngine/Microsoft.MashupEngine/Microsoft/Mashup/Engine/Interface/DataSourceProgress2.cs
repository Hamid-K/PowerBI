using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200008B RID: 139
	public class DataSourceProgress2
	{
		// Token: 0x060001FB RID: 507 RVA: 0x0000324F File Offset: 0x0000144F
		public DataSourceProgress2(string dataSourceType, string dataSource)
		{
			this.DataSourceType = dataSourceType;
			this.DataSource = dataSource;
			this.PercentComplete = -1;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000326C File Offset: 0x0000146C
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00003274 File Offset: 0x00001474
		public string DataSourceType { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000327D File Offset: 0x0000147D
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00003285 File Offset: 0x00001485
		public string DataSource { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000328E File Offset: 0x0000148E
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00003296 File Offset: 0x00001496
		public long LastProgressAt { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000329F File Offset: 0x0000149F
		// (set) Token: 0x06000203 RID: 515 RVA: 0x000032A7 File Offset: 0x000014A7
		public int RequestCount { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000204 RID: 516 RVA: 0x000032B0 File Offset: 0x000014B0
		// (set) Token: 0x06000205 RID: 517 RVA: 0x000032B8 File Offset: 0x000014B8
		public long RowsRead { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000032C1 File Offset: 0x000014C1
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000032C9 File Offset: 0x000014C9
		public long RowsWritten { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000032D2 File Offset: 0x000014D2
		// (set) Token: 0x06000209 RID: 521 RVA: 0x000032DA File Offset: 0x000014DA
		public long BytesRead { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000032E3 File Offset: 0x000014E3
		// (set) Token: 0x0600020B RID: 523 RVA: 0x000032EB File Offset: 0x000014EB
		public long BytesWritten { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600020C RID: 524 RVA: 0x000032F4 File Offset: 0x000014F4
		// (set) Token: 0x0600020D RID: 525 RVA: 0x000032FC File Offset: 0x000014FC
		public int PercentComplete { get; set; }
	}
}
