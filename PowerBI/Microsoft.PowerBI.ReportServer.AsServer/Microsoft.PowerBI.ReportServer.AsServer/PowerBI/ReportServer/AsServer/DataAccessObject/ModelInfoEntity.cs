using System;

namespace Microsoft.PowerBI.ReportServer.AsServer.DataAccessObject
{
	// Token: 0x02000026 RID: 38
	public class ModelInfoEntity
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004D07 File Offset: 0x00002F07
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00004D0F File Offset: 0x00002F0F
		public long ModelId { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004D18 File Offset: 0x00002F18
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004D20 File Offset: 0x00002F20
		public DateTime? LastModified { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004D29 File Offset: 0x00002F29
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004D31 File Offset: 0x00002F31
		public DateTime? LastQueried { get; set; }
	}
}
