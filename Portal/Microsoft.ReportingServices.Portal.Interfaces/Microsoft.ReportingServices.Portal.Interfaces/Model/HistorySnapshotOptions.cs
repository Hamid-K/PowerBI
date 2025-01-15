using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
	// Token: 0x0200003C RID: 60
	public class HistorySnapshotOptions
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00002CA1 File Offset: 0x00000EA1
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00002CA9 File Offset: 0x00000EA9
		[Key]
		public Guid CatalogItemId { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00002CB2 File Offset: 0x00000EB2
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00002CBA File Offset: 0x00000EBA
		public ReportHistorySnapshotsOptions HistorySnapshotsOptions { get; set; }
	}
}
