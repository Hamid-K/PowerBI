using System;

namespace Model
{
	// Token: 0x02000025 RID: 37
	public sealed class ItemHistoryOptions
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000026D9 File Offset: 0x000008D9
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000026E1 File Offset: 0x000008E1
		public bool EnableManualSnapshotCreation { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000026EA File Offset: 0x000008EA
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000026F2 File Offset: 0x000008F2
		public bool KeepExecutionSnapshots { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000026FB File Offset: 0x000008FB
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00002703 File Offset: 0x00000903
		public ScheduleReference Schedule { get; set; }
	}
}
