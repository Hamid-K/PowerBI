using System;

namespace Microsoft.ReportingServices.CatalogAccess.DataAccessObject
{
	// Token: 0x0200001C RID: 28
	public class EventEntity
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000027CD File Offset: 0x000009CD
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000027D5 File Offset: 0x000009D5
		public Guid EventID { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000027DE File Offset: 0x000009DE
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000027E6 File Offset: 0x000009E6
		public EventEntity.ReportScheduleActions EventType { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000027EF File Offset: 0x000009EF
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000027F7 File Offset: 0x000009F7
		public string EventData { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00002800 File Offset: 0x00000A00
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00002808 File Offset: 0x00000A08
		public DateTime TimeEntered { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00002811 File Offset: 0x00000A11
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00002819 File Offset: 0x00000A19
		public DateTime ProcessStart { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00002822 File Offset: 0x00000A22
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000282A File Offset: 0x00000A2A
		public DateTime ProcessHeartbeat { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00002833 File Offset: 0x00000A33
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000283B File Offset: 0x00000A3B
		public Guid BatchID { get; set; }

		// Token: 0x0200002C RID: 44
		public enum ReportScheduleActions
		{
			// Token: 0x0400011A RID: 282
			None,
			// Token: 0x0400011B RID: 283
			UpdateReportExecutionSnapshot,
			// Token: 0x0400011C RID: 284
			CreateReportHistorySnapshot,
			// Token: 0x0400011D RID: 285
			InvalidateCache,
			// Token: 0x0400011E RID: 286
			TimedSubscription,
			// Token: 0x0400011F RID: 287
			RefreshCache,
			// Token: 0x04000120 RID: 288
			SharedDatasetCacheUpdate,
			// Token: 0x04000121 RID: 289
			CommentAddedAlert,
			// Token: 0x04000122 RID: 290
			DataModelRefresh
		}
	}
}
