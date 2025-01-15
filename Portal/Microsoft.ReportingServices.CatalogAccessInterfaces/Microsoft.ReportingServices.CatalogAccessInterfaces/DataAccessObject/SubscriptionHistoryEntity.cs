using System;

namespace Microsoft.ReportingServices.CatalogAccess.DataAccessObject
{
	// Token: 0x02000020 RID: 32
	public sealed class SubscriptionHistoryEntity
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00002866 File Offset: 0x00000A66
		// (set) Token: 0x06000135 RID: 309 RVA: 0x0000286E File Offset: 0x00000A6E
		public Guid SubscriptionID { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00002877 File Offset: 0x00000A77
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000287F File Offset: 0x00000A7F
		public int SubscriptionHistoryID { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00002888 File Offset: 0x00000A88
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00002890 File Offset: 0x00000A90
		public SubscriptionExecutionType Type { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00002899 File Offset: 0x00000A99
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000028A1 File Offset: 0x00000AA1
		public DateTime StartTime { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000028AA File Offset: 0x00000AAA
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000028B2 File Offset: 0x00000AB2
		public DateTime EndTime { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000028BB File Offset: 0x00000ABB
		// (set) Token: 0x0600013F RID: 319 RVA: 0x000028C3 File Offset: 0x00000AC3
		public SubscriptionStatus Status { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000028CC File Offset: 0x00000ACC
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000028D4 File Offset: 0x00000AD4
		public string Message { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000028DD File Offset: 0x00000ADD
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000028E5 File Offset: 0x00000AE5
		public string Details { get; set; }
	}
}
